print '' print '*** in file TaskTypeEmployeeNeedCRUD.sql ***'
print '' print '*** Using database crlandscaping'
GO
USE [crlandscaping]
GO

print '' print '*** Adding Active field to TaskTypeEmployeeNeed'
GO
ALTER TABLE [TaskTypeEmployeeNeed] ADD [Active] [bit] NOT NULL DEFAULT 1
GO

print '' print '*** Creating sp_retrieve_taskttypeemployeeneed_detail_list'
GO
CREATE PROCEDURE [dbo].[sp_retrieve_taskttypeemployeeneed_detail_list]
AS
	BEGIN
		SELECT 		[TaskTypeEmployeeNeed].[TaskTypeID], [TaskTypeEmployeeNeed].[HoursOfWork], [TaskTypeEmployeeNeed].[Active],
					[TaskType].[Name], [TaskType].[Quantity], [TaskType].[JobLocationAttributeTypeID]
		FROM		[TaskType], [TaskTypeEmployeeNeed]
		WHERE [TaskType].[TaskTypeID] = [TaskTypeEmployeeNeed].[TaskTypeID]
	END
GO


print '' print '*** Creating sp_edit_taskttypeemployeeneed_by_id'
GO
CREATE PROCEDURE [dbo].[sp_edit_taskttypeemployeeneed_by_id]
	(
		@TaskTypeID		[int],
		@NewHoursOfWork	[int],
		@OldHoursOfWork	[int],
		@NewActive		[bit],
		@OldActive		[bit]
	)
AS
	BEGIN
		UPDATE [TaskTypeEmployeeNeed]
			SET [HoursOfWork] = @NewHoursOfWork,
				[Active] = @NewActive
			WHERE [TaskTypeID] = @TaskTypeID
				AND [HoursOfWork] = @OldHoursOfWork
				AND [Active] = @OldActive
		RETURN @@ROWCOUNT
	END
GO


print '' print '*** Creating sp_deactivate_taskttypeemployeeneed_by_id'
GO
CREATE PROCEDURE [dbo].[sp_deactivate_taskttypeemployeeneed_by_id]
	(
		@TaskTypeID		[int]
	)
AS
	BEGIN
		UPDATE [TaskTypeEmployeeNeed]
			SET [Active] = 0
			WHERE [TaskTypeID] = @TaskTypeID
		RETURN @@ROWCOUNT
	END
GO


print '' print  '*** Creating procedure sp_create_taskttypeemployeeneed'
GO
CREATE PROCEDURE [dbo].[sp_create_taskttypeemployeeneed]
	(
	@TaskTypeID		[int],
	@HoursOfWork	[int],
	@Active			[bit]
	)
AS
	BEGIN
		INSERT INTO [TaskTypeEmployeeNeed]
			([TaskTypeID], [HoursOfWork], [Active])
		VALUES
			(@TaskTypeID, @HoursOfWork, @Active)
		RETURN @@ROWCOUNT
	END
GO