/* Noah Davison */
print '' print '*** in file ServiceOfferingItemSPs.sql ***'
print '' print '*** Using database crlandscaping'
GO
USE [crlandscaping]
GO

/*Created by Noah Davison 04/27/2018 */
print '' print '*** in file RetrieveMaintenanceRecordDetailList.sql ***'
USE [crlandscaping]
GO

print '' print '*** Creating sp_retrieve_maintenance_record_detail_list'
GO
CREATE PROCEDURE [dbo].[sp_retrieve_maintenance_record_detail_list]
AS
	BEGIN
		SELECT [MaintenanceRecord].[MaintenanceRecordID], [MaintenanceRecord].[EquipmentID], [MaintenanceRecord].[EmployeeID], 
		[MaintenanceRecord].[Description], [MaintenanceRecord].[Date], [Equipment].[Name], [Employee].[FirstName], [Employee].[LastName]
		FROM [MaintenanceRecord]
		INNER JOIN [Equipment] ON [Equipment].[EquipmentID] = [MaintenanceRecord].[EquipmentID]
		INNER JOIN [Employee] ON [Employee].[EmployeeID] = [MaintenanceRecord].[EmployeeID]
	END
GO
