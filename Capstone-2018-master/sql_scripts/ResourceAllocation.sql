/*Created by Zachary Hall 04/01/2018 */
print '' print '*** in file ResourceAllocation.sql ***'
USE [crlandscaping]
GO

print '' print '*** Creating Customer Table'
GO
CREATE TABLE [JobTask](
	[JobTaskID]				[int] IDENTITY(1000000, 1)		NOT NULL,
	[JobID]					[int]							NOT NULL,
	[TaskID]				[int]							NOT NULL,
	[IsDone]				[bit]							NOT NULL DEFAULT 0,
	CONSTRAINT [pk_JobTaskID] PRIMARY KEY([JobTaskID] ASC)
)
GO

print '' print '*** Creating JobTask JobID Foreign Key'
GO
ALTER TABLE [dbo].[JobTask] WITH NOCHECK
	ADD CONSTRAINT [fk_JobTask_JobID] FOREIGN KEY([JobID])
	REFERENCES [dbo].[Job] ([JobID])
	ON UPDATE CASCADE
GO

print '' print '*** Creating JobTask TaskID Foreign Key'
GO
ALTER TABLE [dbo].[JobTask] WITH NOCHECK
	ADD CONSTRAINT [fk_JobTask_TaskID] FOREIGN KEY([TaskID])
	REFERENCES [dbo].[Task] ([TaskID])
	ON UPDATE CASCADE
GO

print '' print '*** Creating JobServicePackge after insert trigger ***'
GO
CREATE TRIGGER [dbo].[TRIG_JobServicePackage_AFTERINSERT]
	ON [dbo].[JobServicePackage]
	AFTER INSERT
AS 
	INSERT INTO [TaskSupply]
	([TaskTypeSupplyNeedID], [SupplyItemID], [Quantity], [JobID])
	(
		SELECT DISTINCT [TaskTypeSupplyNeed].[TaskTypeSupplyNeedID], [TaskTypeSupplyNeed].[SupplyItemID], 0 AS Quantity, INSERTED.[JobID]
		FROM [ServicePackage], [ServicePackageOffering], [ServiceOffering], [ServiceOfferingItem],
			[ServiceItem], [Task], [TaskType], [TaskTypeSupplyNeed], INSERTED
		WHERE INSERTED.[ServicePackageID] = [ServicePackage].[ServicePackageID]
		AND [ServicePackage].[ServicePackageID] = [ServicePackageOffering].[ServicePackageID]
		AND [ServicePackageOffering].[ServiceOfferingID] = [ServiceOffering].[ServiceOfferingID]
		AND [ServiceOffering].[ServiceOfferingID] = [ServiceOfferingItem].[ServiceOfferingID]
		AND [ServiceOfferingItem].[ServiceItemID] = [ServiceItem].[ServiceItemID]
		AND [ServiceItem].[ServiceItemID] = [Task].[ServiceItemID]
		AND [Task].[TaskTypeID] = [TaskType].[TaskTypeID]
		AND [TaskType].[TaskTypeID] = [TaskTypeSupplyNeed].[TaskTypeID]
	)
GO

print '' print '*** Deleting all JobServicePackge records so they can be reinserted to support insert trigger ***'
GO
DELETE FROM [JobServicePackage]
GO

print '' print '*** Inserting JobServicePackage Test Records'
GO
INSERT INTO [dbo].[JobServicePackage]
		([JobID], [ServicePackageID])
	VALUES
		(1000000, 1000000),
		(1000000, 1000001),
		(1000000, 1000002),
		(1000001, 1000000),
		(1000001, 1000001),
		(1000002, 1000000),
		(1000003, 1000000),
		(1000004, 1000000),
		(1000005, 1000000),
		(1000006, 1000000),
		(1000007, 1000000),
		(1000008, 1000000),
		(1000009, 1000000),
		(1000010, 1000000)
GO


print '' print '*** Creating sp_retrieve_tasksupply_allocation_detail_list_by_job_id'
GO
CREATE PROCEDURE [dbo].[sp_retrieve_tasksupply_allocation_detail_list_by_job_id]
	(
		@JobID		[int]
	)
AS
	BEGIN
		SELECT		[Task].[Name], [SupplyItem].[Name], [TaskTypeSupplyNeed].[Quantity], 
					[SupplyItem].[QuantityInStock], [TaskSupply].[Quantity], [TaskSupply].[TaskSupplyID]
		FROM     	[TaskSupply], [TaskTypeSupplyNeed], [TaskType], [Task], [SupplyItem]
		WHERE		[TaskSupply].[TaskTypeSupplyNeedID] = [TaskTypeSupplyNeed].[TaskTypeSupplyNeedID]
		AND			[TaskTypeSupplyNeed].[TaskTypeID] = [TaskType].[TaskTypeID]
		AND			[TaskType].[TaskTypeID] = [Task].[TaskTypeID]
		AND			[SupplyItem].[SupplyItemID] = [TaskSupply].[SupplyItemID]
		AND 		[TaskSupply].[JobID] = @JobID

	END
GO

print '' print '*** Creating sp_retrieve_taskemployee_allocation_detail_list_by_job_id'
GO
CREATE PROCEDURE [dbo].[sp_retrieve_taskemployee_allocation_detail_list_by_job_id]
	(
		@JobID		[int]
	)
AS
	BEGIN
		SELECT DISTINCT [Task].[Name], (([TaskTypeEmployeeNeed].[HoursOfWork] * [JobLocationAttribute].[Value]) / [TaskType].[Quantity]) AS TotalHours, [TaskTypeEmployeeNeed].[TaskTypeEmployeeNeedID], COUNT(DISTINCT [TaskEmployee].[EmployeeID]) as EmployeesAssigned

		FROM [Job]
		INNER JOIN [JobServicePackage] ON [Job].[JobID] = [JobServicePackage].[JobID]
		INNER JOIN [ServicePackage] ON [JobServicePackage].[ServicePackageID] = [ServicePackage].[ServicePackageID]
		INNER JOIN [ServicePackageOffering] ON [ServicePackage].[ServicePackageID] = [ServicePackageOffering].[ServicePackageID]
		INNER JOIN [ServiceOffering] ON [ServicePackageOffering].[ServiceOfferingID] = [ServiceOffering].[ServiceOfferingID]
		INNER JOIN [ServiceOfferingItem] ON [ServiceOffering].[ServiceOfferingID] = [ServiceOfferingItem].[ServiceOfferingID]
		INNER JOIN [ServiceItem] ON [ServiceOfferingItem].[ServiceItemID] = [ServiceItem].[ServiceItemID]
		INNER JOIN [Task] ON [ServiceItem].[ServiceItemID] = [Task].[ServiceItemID]
		INNER JOIN [TaskType] ON [Task].[TaskTypeID] = [TaskType].[TaskTypeID]
		INNER JOIN [TaskTypeEmployeeNeed] ON [TaskType].[TaskTypeID] = [TaskTypeEmployeeNeed].[TaskTypeID]
		INNER JOIN [JobLocation] ON [Job].[JobLocationID] = [JobLocation].[JobLocationID]
		INNER JOIN [JobLocationAttribute] ON [JobLocation].[JobLocationID] = [JobLocationAttribute].[JobLocationID] 
			AND [TaskType].[JobLocationAttributeTypeID] = [JobLocationAttribute].[JobLocationAttributeTypeID]
		LEFT JOIN [TaskEmployee] ON [TaskEmployee].[TaskTypeEmployeeNeedID] = [TaskTypeEmployeeNeed].[TaskTypeEmployeeNeedID]
		WHERE [Job].[JobID] = @JobID
		Group BY [Task].[Name], [TaskTypeEmployeeNeed].[HoursOfWork], [TaskTypeEmployeeNeed].[TaskTypeEmployeeNeedID], [JobLocationAttribute].[Value], [TaskType].[Quantity]

	END
GO


print '' print '*** Creating sp_retrieve_taskequipment_allocation_detail_list_by_job_id'
GO
CREATE PROCEDURE [dbo].[sp_retrieve_taskequipment_allocation_detail_list_by_job_id]
	(
		@JobID		[int]
	)
AS
	BEGIN
		SELECT DISTINCT [Task].[Name], [TaskTypeEquipmentNeed].[HoursOfWork], [TaskTypeEquipmentNeed].[TaskTypeEquipmentNeedID], COUNT(DISTINCT [TaskEquipment].[EquipmentID]) as EquipmentAssigned
		FROM [Job]
		INNER JOIN [JobServicePackage] ON [Job].[JobID] = [JobServicePackage].[JobID]
		INNER JOIN [ServicePackage] ON [JobServicePackage].[ServicePackageID] = [ServicePackage].[ServicePackageID]
		INNER JOIN [ServicePackageOffering] ON [ServicePackage].[ServicePackageID] = [ServicePackageOffering].[ServicePackageID]
		INNER JOIN [ServiceOffering] ON [ServicePackageOffering].[ServiceOfferingID] = [ServiceOffering].[ServiceOfferingID]
		INNER JOIN [ServiceOfferingItem] ON [ServiceOffering].[ServiceOfferingID] = [ServiceOfferingItem].[ServiceOfferingID]
		INNER JOIN [ServiceItem] ON [ServiceOfferingItem].[ServiceItemID] = [ServiceItem].[ServiceItemID]
		INNER JOIN [Task] ON [ServiceItem].[ServiceItemID] = [Task].[ServiceItemID]
		INNER JOIN [TaskType] ON [Task].[TaskTypeID] = [TaskType].[TaskTypeID]
		INNER JOIN [TaskTypeEquipmentNeed] ON [TaskType].[TaskTypeID] = [TaskTypeEquipmentNeed].[TaskTypeID]
		LEFT JOIN [TaskEquipment] ON [TaskEquipment].[TaskTypeEquipmentNeedID] = [TaskTypeEquipmentNeed].[TaskTypeEquipmentNeedID]
		WHERE [Job].[JobID] = @JobID
		Group BY [Task].[Name], [TaskTypeEquipmentNeed].[HoursOfWork], [TaskTypeEquipmentNeed].[TaskTypeEquipmentNeedID]

	END
GO

