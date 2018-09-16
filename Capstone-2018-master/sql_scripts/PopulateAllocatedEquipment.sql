/*Created by Noah Davison 04/06/2018 */
print '' print '*** in file PopulateAllocatedEquipment.sql ***'
USE [crlandscaping]
GO

print '' print '*** Creating sp_retrieve_assigned_equipment_by_task_id'
GO
CREATE PROCEDURE [dbo].[sp_retrieve_assigned_equipment_by_task_id]
	(
		@TaskID		[int]
	)
AS
	BEGIN
		SELECT [Equipment].[EquipmentID], [Equipment].[EquipmentTypeID], [Equipment].[Name], [Equipment].[MakeModelID], [Equipment].[DatePurchased], [Equipment].[DateLastRepaired], [Equipment].[PriceAtPurchase], [Equipment].[CurrentValue], [Equipment].[WarrantyUntil], [Equipment].[EquipmentStatusID], [Equipment].[EquipmentDetails]
		FROM [Equipment]
		INNER JOIN [TaskEquipment] ON [Equipment].[EquipmentID] = [TaskEquipment].[EquipmentID]
		INNER JOIN [TaskTypeEquipmentNeed] ON [TaskEquipment].[TaskTypeEquipmentNeedID] = [TaskTypeEquipmentNeed].[TaskTypeEquipmentNeedID]
		INNER JOIN [TaskType] ON [TaskTypeEquipmentNeed].[TaskTypeID] = [TaskType].[TaskTypeID]
		INNER JOIN [Task] ON [TaskType].[TaskTypeID] = [Task].[TaskTypeID]
		WHERE [Task].[TaskID] = @TaskID
	END
GO

/* Sam Dramstad, 2018/04/11 */
print '' print '*** Creating sp_create_taskequipment_assignment'
GO
CREATE PROCEDURE [dbo].[sp_create_taskequipment_assignment]
	(
	@EquipmentID             [int],
	@JobID                   [int],
    @TaskTypeEquipmentNeedID [int]
	)
AS
	BEGIN
		INSERT INTO [dbo].[TaskEquipment]
			([EquipmentID], [JobID], [TaskTypeEquipmentNeedID])
		VALUES
			(@EquipmentID, @JobID, @TaskTypeEquipmentNeedID)
	END
GO
