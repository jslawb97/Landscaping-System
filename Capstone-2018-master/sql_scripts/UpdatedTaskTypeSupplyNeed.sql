/* Jacob Conley */
print '' print '*** in file EmployeeCertificationDetail.sql ***'
USE [crlandscaping]
GO

print '' print '*** Adding Active column to TaskTypeSupplyNeed'
ALTER TABLE [TaskTypeSupplyNeed] ADD [Active] bit NOT NULL DEFAULT 1
GO

print '' print '*** Creating sp_retrieve_updated_tasktypesupplies'
GO
CREATE PROCEDURE [dbo].[sp_retrieve_updated_tasktypesupplies]
AS
	BEGIN
		SELECT	[TaskTypeSupplyNeedID],[TaskTypeID],[SupplyItemID],[Quantity], [Active]
		FROM	[TaskTypeSupplyNeed]
		WHERE	[Active] = 1
	END
GO

print '' print '*** Creating sp_deactivate_tasktypesupplyneed'
GO
CREATE PROCEDURE [dbo].[sp_deactivate_tasktypesupplyneed]
	(
		@TaskTypeSupplyNeedID			[int]
	)
AS
	BEGIN
		UPDATE 	[TaskTypeSupplyNeed]
		SET [Active] = 0
		WHERE	[TaskTypeSupplyNeedID] = @TaskTypeSupplyNeedID
		AND [Active] = 1
		RETURN @@ROWCOUNT
	END
GO