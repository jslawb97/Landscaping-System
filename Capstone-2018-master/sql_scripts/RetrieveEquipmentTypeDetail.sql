/* Noah Davison */
print '' print '*** in file RetrieveEquipmentTypeDetail.sql ***'
print '' print '*** Using database crlandscaping'
GO
USE [crlandscaping]
GO

print '' print '*** Creating sp_retrieve_equipment_type_detail_list'
GO
CREATE PROCEDURE [dbo].[sp_retrieve_equipment_type_detail_list]
AS
	BEGIN
		SELECT	[EquipmentType].[EquipmentTypeID], [EquipmentType].[InspectionChecklistID], [EquipmentType].[PrepChecklistID], [EquipmentType].[Active],
				[InspectionChecklist].[Name], [PrepChecklist].[Name]
		FROM	[EquipmentType]
		LEFT JOIN [InspectionChecklist] ON [EquipmentType].[InspectionChecklistID] = [InspectionChecklist].[InspectionChecklistID]
		LEFT JOIN [PrepChecklist] ON [EquipmentType].[PrepChecklistID] = [PrepChecklist].[PrepChecklistID]
	END
GO
