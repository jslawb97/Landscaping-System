/*Created by Noah Davison 04/13/2018 */
print '' print '*** in file EquipmentAllocationPopulate.sql ***'
USE [crlandscaping]
GO

print '' print '*** Creating sp_retrieve_taskequipment_allocation_detail_list_by_job_id_updated'
GO
CREATE PROCEDURE [dbo].[sp_retrieve_taskequipment_allocation_detail_list_by_job_id_updated]
	(
		@JobID		[int]
	)
AS
	BEGIN
		SELECT DISTINCT [Task].[Name], [TaskTypeEquipmentNeed].[HoursOfWork], [TaskTypeEquipmentNeed].[TaskTypeEquipmentNeedID], COUNT(DISTINCT [TaskEquipment].[EquipmentID]) as EquipmentAssigned, [Task].[TaskID]
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
		Group BY [Task].[Name], [TaskTypeEquipmentNeed].[HoursOfWork], [TaskTypeEquipmentNeed].[TaskTypeEquipmentNeedID],[Task].[TaskID]

	END
GO