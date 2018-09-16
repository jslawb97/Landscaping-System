/* Created by Badis SAIDANI  4-5-18  */
print '' print '*** in file TaskEmployee.sql ***'
USE [crlandscaping]
GO

print '' print '*** Allocate Employees to Job Tasks functionality ****'
GO

print '' print '*** Creating sp_remove_taskemployee_by_tasktypeemployeeneed_id'
GO
CREATE PROCEDURE [dbo].[sp_remove_taskemployee_by_tasktypeemployeeneed_id]
	(
		@TaskTypeEmployeeNeedID			[int]
	)	
AS
	BEGIN
		DELETE FROM [TaskEmployee]
		WHERE [TaskEmployee].[TaskTypeEmployeeNeedID] = @TaskTypeEmployeeNeedID
		
		RETURN @@ROWCOUNT
	END
GO

print '' print '*** Inserting some data to TaskEmployee'
GO
INSERT INTO [dbo].[TaskEmployee]
		([EmployeeID], [JobID], TaskTypeEmployeeNeedID)
	VALUES
		(1000006, 1000000, 1000000),
		(1000001, 1000001, 1000001)
GO
/* end Badis SAIDANI */
/* Created by Sam Dramstad  4-6-18  */
print '' print '*** Creating sp_create_taskemployee_assignment'
GO
CREATE PROCEDURE [dbo].[sp_create_taskemployee_assignment]
	(
	@EmployeeID	            [int],
	@JobID              	[int],
    @TaskTypeEmployeeNeedID [int]
	)
AS
	BEGIN
		INSERT INTO [dbo].[TaskEmployee]
			([EmployeeID], [JobID], [TaskTypeEmployeeNeedID])
		VALUES
			(@EmployeeID, @JobID, @TaskTypeEmployeeNeedID)
	END
GO


print '' print '*** Creating sp_delete_taskemployee_assignment'
GO
CREATE PROCEDURE [dbo].[sp_delete_taskemployee_assignment]
	(
		@EmployeeID		[int],
        @JobID          [int]
	)
AS
	BEGIN
			DELETE
			FROM 		[TaskEmployee]
			WHERE 		[EmployeeID] = @EmployeeID
            AND         [JobID] = @JobID
		RETURN @@ROWCOUNT
	END
GO