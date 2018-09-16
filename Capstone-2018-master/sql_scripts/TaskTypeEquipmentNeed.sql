/* Brady Feller */
print '' print '*** in file TaskTypeEquipmentNeed.sql ***'
USE [crlandscaping]
GO

print '' print '*** Creating sp_create_task_type_equipment_need'
GO
CREATE PROCEDURE [dbo].[sp_create_task_type_equipment_need]
	(
	@TaskTypeID					[int],
	@EquipmentTypeID			[nvarchar](100),
	@HoursOfWork				[int]
	)
AS
	BEGIN
		INSERT INTO	[dbo].[TaskTypeEquipmentNeed]
			([TaskTypeID], [EquipmentTypeID], [HoursOfWork])
		VALUES
			(@TaskTypeID, @EquipmentTypeID, @HoursOfWork)
		SELECT SCOPE_IDENTITY()
	END
GO

print '' print '*** Creating sp_edit_task_type_equipment_need'
GO
CREATE PROCEDURE [dbo].[sp_edit_task_type_equipment_need]
	(
	@NewTaskTypeID		 		[int],
	@OldTaskTypeID		 		[int],
	@NewEquipmentTypeID	 		[nvarchar](100),
	@OldEquipmentTypeID	 		[nvarchar](100),
	@NewHoursOfWork		 		[int],
	@OldHoursOfWork		 		[int]
	)
AS
	BEGIN
		UPDATE [TaskTypeEquipmentNeed]
			SET [TaskTypeID] = @NewTaskTypeID,
				[EquipmentTypeID] = @NewEquipmentTypeID,
				[HoursOfWork] = @NewHoursOfWork
			WHERE [TaskTypeID] = @OldTaskTypeID
            AND [EquipmentTypeID] = @OldEquipmentTypeID
            AND [HoursOfWork] = @OldHoursOfWork
		SELECT SCOPE_IDENTITY()	
	END
GO

print '' print '*** Creating sp_retrieve_task_type_equipment_need_by_id'
GO
CREATE PROCEDURE [dbo].[sp_retrieve_task_type_equipment_need_by_id]
	(
	@TaskTypeEquipmentNeedID	[int]
	)
AS
	BEGIN
		SELECT	[TaskTypeEquipmentNeedID], [TaskTypeID], [EquipmentTypeID], [HoursOfWork]
		FROM	[TaskTypeEquipmentNeed]
		WHERE	[TaskTypeEquipmentNeedID] = @TaskTypeEquipmentNeedID
	END
GO

print '' print '*** Creating sp_retrieve_tasktypeequipmentneed_detail_list'
GO
CREATE PROCEDURE [dbo].[sp_retrieve_tasktypeequipmentneed_detail_list]
AS
	BEGIN
		SELECT	[TaskTypeEquipmentNeedID],[TaskTypeID],[EquipmentTypeID],[HoursOfWork]
		FROM	[TaskTypeEquipmentNeed]
	END
GO

/* End Brady Feller */

/* Jacob Slaubaugh */
print '' print '*** Creating sp_delete_tasktypeequipmentneed_by_id'
GO
CREATE PROCEDURE [dbo].[sp_delete_tasktypeequipmentneed_by_id]
	(
	@TaskTypeEquipmentNeedID	[int]
	)
AS
	BEGIN
		DELETE FROM	[TaskTypeEquipmentNeed]
		WHERE	[TaskTypeEquipmentNeedID] = @TaskTypeEquipmentNeedID
	RETURN @@ROWCOUNT
	END
GO
/* End Jacob Slaubaugh */