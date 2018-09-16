print '' print '*** In file RetrieveMakeModelDetail.sql'
USE crlandscaping

print '' print '*** Creating sp_retrieve_makemodel_detail_list'
GO
CREATE PROCEDURE [dbo].[sp_retrieve_makemodel_detail_list]
AS
	BEGIN
		SELECT	[MakeModelID], [Make], [Model], [MakeModel].[Active]
		, [MakeModel].[MaintenanceChecklistID], [MaintenanceChecklist].[Name]
		FROM	[MakeModel] LEFT OUTER JOIN [MaintenanceChecklist]
		ON [MakeModel].[MaintenanceChecklistID] = [MaintenanceChecklist].[MaintenanceChecklistID]
	END
GO