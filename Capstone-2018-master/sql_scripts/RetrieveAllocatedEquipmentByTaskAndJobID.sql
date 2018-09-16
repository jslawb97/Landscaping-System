/*Created by Noah Davison 04/06/2018 */
print '' print '*** in file RetrieveAllocatedEquipmentByTaskAndJobID.sql ***'
USE [crlandscaping]
GO

print '' print '*** Creating sp_retrieve_assigned_equipment_by_task_id_and_job_id'
GO
CREATE PROCEDURE [dbo].[sp_retrieve_assigned_equipment_by_task_id_and_job_id]
	(
		@TaskID		[int],
		@JobID		[int]
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
		AND [TaskEquipment].[JobID] = @JobID
	END
GO