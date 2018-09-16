print '' print '*** Using database crlandscaping'
GO
USE [crlandscaping]
GO


print '' print '*** Creating sp_get_available_employee_for_job'
GO
CREATE PROCEDURE [dbo].[sp_get_available_employee_for_job]
	(
		@JobID int
	)
AS
	BEGIN
		SELECT DISTINCT [Availability].[EmployeeID]
		INTO #AvailableEmployees
		FROM [Availability], [Job]
		WHERE CAST([Job].[DateTimeScheduled] as TIME) BETWEEN CAST([Availability].[StartTime] as TIME) AND CAST([Availability].[EndTime] as TIME)
		AND DATEPART(DW, [Job].[DateTimeScheduled]) = DATEPART(DW, [Availability].[StartTime])
		AND [Job].[JobID] = @JobID
		
		-- Delete where they have time off
		DELETE FROM #AvailableEmployees
		WHERE [EmployeeID] IN 
			(
				SELECT [TimeOff].[EmployeeID]
				FROM [TimeOff], [Job]
				WHERE [Job].[DateTimeScheduled] BETWEEN [TimeOff].[StartTime] AND [TimeOff].[EndTime]
				AND [Job].[JobID] = @JobID
			)
		
		-- Delete invalid roles
		DELETE FROM #AvailableEmployees
		WHERE [EmployeeID] IN
			(
				SELECT DISTINCT [EmployeeRole].[EmployeeID]
				FROM [EmployeeRole]
				WHERE [RoleID] NOT IN ('Worker', 'Foreman', 'Temp')
			)
		
		-- remove employees that are already scheduled around that time according to the half day increment
		DELETE FROM #AvailableEmployees
		WHERE [EmployeeID] IN
			(
			
				SELECT [TaskEmployee].[EmployeeID]
				FROM [TaskEmployee], [Job]
				WHERE [TaskEmployee].[JobID] = [Job].[JobID]
				AND (SELECT [DateTimeScheduled] FROM [Job] WHERE [JobID] = @JobID) 
					BETWEEN [Job].[DateTimeScheduled] AND DATEADD(HOUR, 12, [Job].[DateTimeScheduled])
				AND [Job].[JobID] != @JobID
			)
		
		-- get available
		SELECT [Employee].[EmployeeID], [Employee].[FirstName], [Employee].[LastName]
		FROM [Employee], #AvailableEmployees
		WHERE [Employee].[EmployeeID] = #AvailableEmployees.[EmployeeID]
		AND [Employee].[Active] = 1
		
	END
GO


print '' print '*** Creating sp_get_task_employees_by_job_id'
GO
CREATE PROCEDURE [dbo].[sp_get_task_employees_by_job_id]
	(
		@JobID int
	)
AS
	BEGIN
		SELECT [Task].[Name], [TaskEmployee].[EmployeeID], [Employee].[FirstName], [Employee].[LastName], [TaskEmployee].[TaskEmployeeID]
		FROM [Task]
		INNER JOIN [TaskType] ON [Task].[TaskTypeID] = [TaskType].[TaskTypeID]
		INNER JOIN [TaskTypeEmployeeNeed] ON [TaskType].[TaskTypeID] = [TaskTypeEmployeeNeed].[TaskTypeID]
		INNER JOIN [TaskEmployee] ON [TaskTypeEmployeeNeed].[TaskTypeEmployeeNeedID] = [TaskEmployee].[TaskTypeEmployeeNeedID]
		LEFT JOIN [Employee] ON [TaskEmployee].[EmployeeID] = [Employee].[EmployeeID]
		WHERE [TaskEmployee].[JobID] = @JobID
	END
GO


print '' print '*** Creating sp_get_task_equipment_by_job_id'
GO
CREATE PROCEDURE [dbo].[sp_get_task_equipment_by_job_id]
	(
		@JobID int
	)
AS
	BEGIN
		SELECT [Task].[Name], [TaskEquipment].[EquipmentID], [Equipment].[Name], [Equipment].[EquipmentTypeID], [TaskEquipment].[TaskEquipmentID]
		FROM [Task]
		INNER JOIN [TaskType] ON [Task].[TaskTypeID] = [TaskType].[TaskTypeID]
		INNER JOIN [TaskTypeEquipmentNeed] ON [TaskType].[TaskTypeID] = [TaskTypeEquipmentNeed].[TaskTypeID]
		INNER JOIN [TaskEquipment] ON [TaskTypeEquipmentNeed].[TaskTypeEquipmentNeedID] = [TaskEquipment].[TaskTypeEquipmentNeedID]
		LEFT JOIN [Equipment] ON [TaskEquipment].[EquipmentID] = [Equipment].[EquipmentID]
		WHERE [TaskEquipment].[JobID] = @JobID
		
	END
GO


print '' print '*** Creating sp_get_available_equipment_for_job'
GO
CREATE PROCEDURE [dbo].[sp_get_available_equipment_for_job]
	(
		@JobID int
	)
AS
	BEGIN
		-- get active and status=available equipment
		SELECT [EquipmentID]
		INTO #AvailableEquipment
		FROM [Equipment]
		WHERE [Active] = 1
		AND [EquipmentStatusID] = 'Available'
		
		-- remove equipment that is already scheduled around that time according to the half day increment
		DELETE FROM #AvailableEquipment
		WHERE [EquipmentID] IN
			(
				SELECT [TaskEquipment].[EquipmentID]
				FROM [TaskEquipment], [Job]
				WHERE [TaskEquipment].[JobID] = [Job].[JobID]
				AND (SELECT [DateTimeScheduled] FROM [Job] WHERE [JobID] = @JobID)
					BETWEEN [Job].[DateTimeScheduled] AND DATEADD(HOUR, 12, [Job].[DateTimeScheduled])
				AND [Job].[JobID] != @JobID
			)
		
		-- get available
		SELECT [Equipment].[EquipmentID], [Equipment].[EquipmentTypeID], [Equipment].[Name]
		FROM [Equipment], #AvailableEquipment
		WHERE [Equipment].[EquipmentID] = #AvailableEquipment.[EquipmentID]
		
	END
GO


print '' print '*** Creating sp_get_task_supply_by_job_id'
GO
CREATE PROCEDURE [dbo].[sp_get_task_supply_by_job_id]
	(
		@JobID int
	)
AS
	BEGIN
		SELECT [Task].[Name], [TaskSupply].[SupplyItemID], [TaskSupply].[Quantity]
		, [SupplyItem].[Name]
		FROM [Task], [TaskType], [TaskTypeSupplyNeed], [TaskSupply], [SupplyItem]
		WHERE [TaskSupply].[JobID] = @JobID
		AND [Task].[TaskTypeID] = [TaskType].[TaskTypeID]
		AND [TaskType].[TaskTypeID] = [TaskTypeSupplyNeed].[TaskTypeID]
		AND [TaskTypeSupplyNeed].[TaskTypeSupplyNeedID] = [TaskSupply].[TaskTypeSupplyNeedID]
		AND [TaskSupply].[SupplyItemID] = [SupplyItem].[SupplyItemID]
		
	END
GO


print '' print '*** Creating sp_update_taskemployee_employee_id'
GO
CREATE PROCEDURE [dbo].[sp_update_taskemployee_employee_id]
	(
		@TaskEmployeeID int,
		@EmployeeID int
	)
AS
	BEGIN
		UPDATE [TaskEmployee]
			SET [EmployeeID] = @EmployeeID
		WHERE [TaskEmployeeID] = @TaskEmployeeID
		
		RETURN @@ROWCOUNT
	END
GO

print '' print '*** Creating sp_update_taskemployee_employee_id_null'
GO
CREATE PROCEDURE [dbo].[sp_update_taskemployee_employee_id_null]
	(
		@TaskEmployeeID int
	)
AS
	BEGIN
		UPDATE [TaskEmployee]
			SET [EmployeeID] = NULL
		WHERE [TaskEmployeeID] = @TaskEmployeeID
		
		RETURN @@ROWCOUNT
	END
GO



print '' print '*** Creating sp_update_job_scheduled_date'
GO
CREATE PROCEDURE [dbo].[sp_update_job_scheduled_date]
	(
		@JobID int,
		@NewScheduledDate datetime
	)
AS
	BEGIN
		UPDATE [Job]
			SET [DateTimeScheduled] = @NewScheduledDate
		WHERE [JobID] = @JobID
		
		RETURN @@ROWCOUNT
	END
GO


print '' print '*** Creating sp_retrieve_job_detail_by_jobid'
GO
CREATE PROCEDURE [dbo].[sp_retrieve_job_detail_by_jobid]
(
	@JobID int
)
AS
	BEGIN
		SELECT [Job].[JobID], [Job].[CustomerID], [Job].[DateTimeScheduled], [Job].[EmployeeID], 
				[Job].[JobLocationID], [Job].[Active], [Job].[DateCompleted], [Job].[Comments],
				[Customer].[CustomerTypeID], [Customer].[Email], [Customer].[FirstName], [Customer].[LastName],
				[Customer].[PhoneNumber], [Customer].[Active],
				[JobLocation].[JobLocationID], [JobLocation].[Street], [JobLocation].[City], [JobLocation].[State], 
				[JobLocation].[ZipCode], [JobLocation].[Comments], [JobLocation].[Active], [Job].[DateTimeTargetWindow]
		FROM [Job]
		INNER JOIN [Customer] ON [Job].[CustomerID] = [Customer].[CustomerID]
		INNER JOIN [JobLocation] ON [JobLocation].[JobLocationID] = [Job].[JobLocationID]
		WHERE [Job].[JobID] = @JobID
		
		SELECT [JobLocationAttribute].[JobLocationID], [JobLocationAttribute].[JobLocationAttributeTypeID], [JobLocationAttribute].[Value]
		FROM [JobLocationAttribute], [JobLocation], [Job]
		WHERE [JobLocationAttribute].[JobLocationID] = [JobLocation].[JobLocationID]
		AND [JobLocation].[JobLocationID] = [Job].[JobLocationID]
		AND [Job].[JobID] = @JobID
		
		SELECT [JobServicePackage].[JobID], [JobServicePackage].[ServicePackageID],  [ServicePackage].[Name],  [ServicePackage].[Description],  [ServicePackage].[Active]
		FROM [JobServicePackage]
		INNER JOIN [ServicePackage] ON [JobServicePackage].[ServicePackageID] = [ServicePackage].[ServicePackageID]
		WHERE [JobServicePackage].[JobID] = @JobID
	END
GO



print '' print '*** Inserting Employee Test Records'
GO
INSERT INTO [dbo].[Employee]
		([FirstName], [LastName], [Address], [PhoneNumber]
			,[Email])
	VALUES
		('John20', 'Smith20', '123 4th st', '000-000-0001', 'Example20@email.com'),
		('John21', 'Smith21', '123 5th st', '000-000-0002', 'Example21@email.com'),
		('John32', 'Smith32', '123 6th st', '000-000-0003', 'Example32@email.com'),
		('John42', 'Smith42', '123 7th st', '000-000-0004', 'Example42@email.com'),
		('John52', 'Smith52', '123 8th st', '000-000-0005', 'Example52@email.com')
GO

print '' print '*** Inserting Role Test Records'
GO
INSERT INTO [dbo].[Role]
		([RoleID], [Description])
	VALUES
		('Worker', 'Description1'),
		('Foreman', 'Description2'),
		('Temp', 'Description7')
GO

print '' print '*** Inserting EmployeeRole Test Records'
GO
INSERT INTO [dbo].[EmployeeRole] ([EmployeeID], [RoleID], [Active]) VALUES (1000010, N'Worker', 1)
INSERT INTO [dbo].[EmployeeRole] ([EmployeeID], [RoleID], [Active]) VALUES (1000011, N'Worker', 1)
INSERT INTO [dbo].[EmployeeRole] ([EmployeeID], [RoleID], [Active]) VALUES (1000012, N'Worker', 1)
INSERT INTO [dbo].[EmployeeRole] ([EmployeeID], [RoleID], [Active]) VALUES (1000013, N'Worker', 1)
INSERT INTO [dbo].[EmployeeRole] ([EmployeeID], [RoleID], [Active]) VALUES (1000014, N'Worker', 1)
GO

print '' print '*** Inserting Availability Test Records'
GO
INSERT INTO [dbo].[Availability] ([EmployeeID], [StartTime], [EndTime]) VALUES (1000010, N'2018-04-22 05:00:00', N'2018-04-22 17:00:00')
INSERT INTO [dbo].[Availability] ([EmployeeID], [StartTime], [EndTime]) VALUES (1000010, N'2018-04-23 05:00:00', N'2018-04-23 17:00:00')
INSERT INTO [dbo].[Availability] ([EmployeeID], [StartTime], [EndTime]) VALUES (1000010, N'2018-04-24 05:00:00', N'2018-04-24 17:00:00')
INSERT INTO [dbo].[Availability] ([EmployeeID], [StartTime], [EndTime]) VALUES (1000010, N'2018-04-25 05:00:00', N'2018-04-25 17:00:00')
INSERT INTO [dbo].[Availability] ([EmployeeID], [StartTime], [EndTime]) VALUES (1000010, N'2018-04-26 05:00:00', N'2018-04-26 17:00:00')
INSERT INTO [dbo].[Availability] ([EmployeeID], [StartTime], [EndTime]) VALUES (1000010, N'2018-04-27 05:00:00', N'2018-04-27 17:00:00')
INSERT INTO [dbo].[Availability] ([EmployeeID], [StartTime], [EndTime]) VALUES (1000010, N'2018-04-28 05:00:00', N'2018-04-28 17:00:00')
INSERT INTO [dbo].[Availability] ([EmployeeID], [StartTime], [EndTime]) VALUES (1000011, N'2018-04-22 05:00:00', N'2018-04-22 17:00:00')
INSERT INTO [dbo].[Availability] ([EmployeeID], [StartTime], [EndTime]) VALUES (1000011, N'2018-04-23 05:00:00', N'2018-04-23 17:00:00')
INSERT INTO [dbo].[Availability] ([EmployeeID], [StartTime], [EndTime]) VALUES (1000011, N'2018-04-24 05:00:00', N'2018-04-24 17:00:00')
INSERT INTO [dbo].[Availability] ([EmployeeID], [StartTime], [EndTime]) VALUES (1000011, N'2018-04-25 05:00:00', N'2018-04-25 17:00:00')
INSERT INTO [dbo].[Availability] ([EmployeeID], [StartTime], [EndTime]) VALUES (1000011, N'2018-04-26 05:00:00', N'2018-04-26 17:00:00')
INSERT INTO [dbo].[Availability] ([EmployeeID], [StartTime], [EndTime]) VALUES (1000011, N'2018-04-27 05:00:00', N'2018-04-27 17:00:00')
INSERT INTO [dbo].[Availability] ([EmployeeID], [StartTime], [EndTime]) VALUES (1000011, N'2018-04-28 05:00:00', N'2018-04-28 17:00:00')
INSERT INTO [dbo].[Availability] ([EmployeeID], [StartTime], [EndTime]) VALUES (1000012, N'2018-04-22 05:00:00', N'2018-04-22 17:00:00')
INSERT INTO [dbo].[Availability] ([EmployeeID], [StartTime], [EndTime]) VALUES (1000012, N'2018-04-23 05:00:00', N'2018-04-23 17:00:00')
INSERT INTO [dbo].[Availability] ([EmployeeID], [StartTime], [EndTime]) VALUES (1000012, N'2018-04-24 05:00:00', N'2018-04-24 17:00:00')
INSERT INTO [dbo].[Availability] ([EmployeeID], [StartTime], [EndTime]) VALUES (1000012, N'2018-04-25 05:00:00', N'2018-04-25 17:00:00')
INSERT INTO [dbo].[Availability] ([EmployeeID], [StartTime], [EndTime]) VALUES (1000012, N'2018-04-26 05:00:00', N'2018-04-26 17:00:00')
INSERT INTO [dbo].[Availability] ([EmployeeID], [StartTime], [EndTime]) VALUES (1000012, N'2018-04-27 05:00:00', N'2018-04-27 17:00:00')
INSERT INTO [dbo].[Availability] ([EmployeeID], [StartTime], [EndTime]) VALUES (1000012, N'2018-04-28 05:00:00', N'2018-04-28 17:00:00')
INSERT INTO [dbo].[Availability] ([EmployeeID], [StartTime], [EndTime]) VALUES (1000013, N'2018-04-22 05:00:00', N'2018-04-22 17:00:00')
INSERT INTO [dbo].[Availability] ([EmployeeID], [StartTime], [EndTime]) VALUES (1000013, N'2018-04-23 05:00:00', N'2018-04-23 17:00:00')
INSERT INTO [dbo].[Availability] ([EmployeeID], [StartTime], [EndTime]) VALUES (1000013, N'2018-04-24 05:00:00', N'2018-04-24 17:00:00')
INSERT INTO [dbo].[Availability] ([EmployeeID], [StartTime], [EndTime]) VALUES (1000013, N'2018-04-25 05:00:00', N'2018-04-25 17:00:00')
INSERT INTO [dbo].[Availability] ([EmployeeID], [StartTime], [EndTime]) VALUES (1000013, N'2018-04-26 05:00:00', N'2018-04-26 17:00:00')
INSERT INTO [dbo].[Availability] ([EmployeeID], [StartTime], [EndTime]) VALUES (1000013, N'2018-04-27 05:00:00', N'2018-04-27 17:00:00')
INSERT INTO [dbo].[Availability] ([EmployeeID], [StartTime], [EndTime]) VALUES (1000013, N'2018-04-28 05:00:00', N'2018-04-28 17:00:00')
INSERT INTO [dbo].[Availability] ([EmployeeID], [StartTime], [EndTime]) VALUES (1000014, N'2018-04-22 05:00:00', N'2018-04-22 17:00:00')
INSERT INTO [dbo].[Availability] ([EmployeeID], [StartTime], [EndTime]) VALUES (1000014, N'2018-04-23 05:00:00', N'2018-04-23 17:00:00')
INSERT INTO [dbo].[Availability] ([EmployeeID], [StartTime], [EndTime]) VALUES (1000014, N'2018-04-24 05:00:00', N'2018-04-24 17:00:00')
INSERT INTO [dbo].[Availability] ([EmployeeID], [StartTime], [EndTime]) VALUES (1000014, N'2018-04-25 05:00:00', N'2018-04-25 17:00:00')
INSERT INTO [dbo].[Availability] ([EmployeeID], [StartTime], [EndTime]) VALUES (1000014, N'2018-04-26 05:00:00', N'2018-04-26 17:00:00')
INSERT INTO [dbo].[Availability] ([EmployeeID], [StartTime], [EndTime]) VALUES (1000014, N'2018-04-27 05:00:00', N'2018-04-27 17:00:00')
INSERT INTO [dbo].[Availability] ([EmployeeID], [StartTime], [EndTime]) VALUES (1000014, N'2018-04-28 05:00:00', N'2018-04-28 17:00:00')
GO



print '' print '*** Creating sp_update_taskequipment_equipment_id'
GO
CREATE PROCEDURE [dbo].[sp_update_taskequipment_equipment_id]
	(
		@TaskEquipmentID int,
		@EquipmentID int
	)
AS
	BEGIN
		UPDATE [TaskEquipment]
			SET [EquipmentID] = @EquipmentID
		WHERE [TaskEquipmentID] = @TaskEquipmentID
		
		RETURN @@ROWCOUNT
	END
GO

print '' print '*** Creating sp_update_taskequipment_equipment_id_null'
GO
CREATE PROCEDURE [dbo].[sp_update_taskequipment_equipment_id_null]
	(
		@TaskEquipmentID int
	)
AS
	BEGIN
		UPDATE [TaskEquipment]
			SET [EquipmentID] = NULL
		WHERE [TaskEquipmentID] = @TaskEquipmentID
		
		RETURN @@ROWCOUNT
	END
GO