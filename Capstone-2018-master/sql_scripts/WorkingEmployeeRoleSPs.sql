/* John Miller */
print '' print '*** in file WorkingEmployeeRoleSPs.sql ***'
print '' print '*** Using database crlandscaping'
GO
USE [crlandscaping]
GO

ALTER TABLE [EmployeeRole]
ADD [Active]	[bit]	NOT NULL DEFAULT 1;

print '' print '*** Inserting EmployeeRole Records'
GO
INSERT INTO [dbo].[EmployeeRole]
		([EmployeeID], [RoleID])
	VALUES
		(1000000, "Mechanic"),
		(1000000, "Manager"),
		(1000001, "Job Scheduler"),
		(1000001, "Manager")
GO

print '' print '*** Creating sp_edit_employeerole_by_employeeid_V2'
GO
CREATE PROCEDURE [dbo].[sp_edit_employeerole_by_employeeid_V2]
	(
	@EmployeeID				[int],
	@NewRoleID				[nvarchar](100),
	@OldRoleID				[nvarchar](100),
	@OldActive				[bit],
	@NewActive				[bit]
	)
AS
	BEGIN
		UPDATE [EmployeeRole]
			SET [RoleID] = @NewRoleID,
			 [Active] = @NewActive
			WHERE [EmployeeID] = @EmployeeID
			AND [RoleID] = @OldRoleID
			AND [Active] = @OldActive
		RETURN @@ROWCOUNT
	END
GO

print '' print '*** Creating sp_retrieve_employeerole_list_by_employeeid'
GO
CREATE PROCEDURE [dbo].[sp_retrieve_employeerole_list_by_employeeid]
	(
		@EmployeeID		[int]
	)
AS
	BEGIN
		SELECT	[Employee].[EmployeeID], [EmployeeRole].[RoleID]
				FROM	[Employee], [EmployeeRole]
				WHERE	[Employee].[EmployeeID] = @EmployeeID
				AND 	[EmployeeRole].[EmployeeID] = @EmployeeID
	END		
GO

print '' print '*** Creating sp_deactivate_employeerole'
GO
CREATE PROCEDURE [dbo].[sp_deactivate_employeerole]
	(
		@EmployeeID 	[int],
		@RoleID			[nvarchar](100)
	)
AS
	BEGIN 
		UPDATE	[dbo].[EmployeeRole]
			Set		[Active] = 0
			WHERE 	[EmployeeID] = @EmployeeID
			AND		[RoleID] = @RoleID
			AND 	[Active] = 1
		RETURN @@ROWCOUNT
	END
GO

print '' print '*** Creating sp_create_new_employeerole'
GO
CREATE PROCEDURE [dbo].[sp_create_new_employeerole]
	(
	@EmployeeID		[int],
	@RoleID			[nvarchar](100)
	)
AS
	BEGIN
		INSERT INTO [dbo].[EmployeeRole]
			([EmployeeID], [RoleID])
		VALUES
			(@EmployeeID, @RoleID)
		Return @@ROWCOUNT
	END
GO
/*
print '' print '*** Creating sp_retrieve_corrected_employeerole_detail_list'
GO
CREATE PROCEDURE [dbo].[sp_retrieve_corrected_employeerole_detail_list]
AS
	BEGIN
		SELECT 		[Employee].[EmployeeID],[Employee].[FirstName], [Employee].[LastName]
				,[EmployeeRole].[RoleID], [EmployeeRole].[Active]
		FROM		[Employee],[EmployeeRole]
		WHERE [Employee].[EmployeeID] = [EmployeeRole].[EmployeeID]
	END
GO
*/

print '' print '*** Creating sp_retrieve_corrected_employeerole_detail_list'
GO
CREATE PROCEDURE [dbo].[sp_retrieve_corrected_employeerole_detail_list]
AS
	BEGIN
		SELECT 		[Employee].[EmployeeID],[Employee].[FirstName], [Employee].[LastName]
					,[EmployeeRole].[RoleID], [EmployeeRole].[Active]
		FROM		([Employee]
		INNER JOIN 	[EmployeeRole] ON [Employee].[EmployeeID]
				= [EmployeeRole].[EmployeeID])
		WHERE [Employee].[EmployeeID] = [EmployeeRole].[EmployeeID]
	END
GO
		
		
print '' print '*** Creating sp_retrieve_tasktype_detail_list_ by_joblocationattributeid'
GO
CREATE PROCEDURE [dbo].[sp_retrieve_tasktype_detail_list_by_joblocationattributeid]
	(
		@JobLocationAttributeTypeID [nvarchar](100)
	)
AS
	BEGIN
		SELECT	[TaskType].[TaskTypeID], [TaskType].[Name], [TaskType].[Quantity]
				, [TaskType].[JobLocationAttributeTypeID], [TaskType].[Active]
		FROM	([TaskType]
		INNER JOIN [JobLocationAttributeType] ON [TaskType].[JobLocationAttributeTypeID] 
			= [JobLocationAttributeType].[JobLocationAttributeTypeID])	
		WHERE [TaskType].[JobLocationAttributeTypeID] = @JobLocationAttributeTypeID	
	END
GO

print '' print '*** Creating sp_retrieve_tasktype_detail_list'
GO
CREATE PROCEDURE [dbo].[sp_retrieve_tasktype_detail_list]
AS
	BEGIN
		SELECT	[TaskType].[TaskTypeID], [TaskType].[Name], [TaskType].[Quantity]
				, [TaskType].[Active], [TaskType].[JobLocationAttributeTypeID]
		FROM	([TaskType]
		INNER JOIN [JobLocationAttributeType] ON [TaskType].[JobLocationAttributeTypeID]
			= [JobLocationAttributeType].[JobLocationAttributeTypeID]) 
	END	
GO