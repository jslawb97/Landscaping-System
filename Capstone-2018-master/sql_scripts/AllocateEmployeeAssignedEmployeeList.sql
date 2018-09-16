print '' print '*** in file AllocateEmployeeAssignedEmployeeList.sql ***'
USE [crlandscaping]
GO



print '' print '*** Creating sp_retrieve_employee_list_tasktypeemployeeneedid_and_jobid'
GO
CREATE PROCEDURE [dbo].[sp_retrieve_employee_list_tasktypeemployeeneedid_and_jobid]
	(
		@JobID	 					[int],
		@TaskTypeEmployeeNeedID		[int]
	)
AS
	BEGIN
		SELECT	[Employee].[EmployeeID], [Employee].[FirstName], [Employee].[LastName], [Employee].[Address], [Employee].[PhoneNumber]
			,[Employee].[Email], [Employee].[Active]
		FROM	[Employee], [TaskEmployee]
		WHERE	[TaskEmployee].[EmployeeID] = [Employee].[EmployeeID]
		AND [TaskEmployee].[JobID] = @JobID
		AND [TaskEmployee].[TaskTypeEmployeeNeedID] = @TaskTypeEmployeeNeedID
	END
GO
