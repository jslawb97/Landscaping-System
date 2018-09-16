/* Badis Saidani */
print '' print '*** in file ServiceOfferingItemSPs.sql ***'
print '' print '*** Using database crlandscaping'
GO
USE [crlandscaping]
GO

/*Created by Badis Saidani 05/0/2018 */
print '' print '*** in file PrepRecordRecordDetailList.sql ***'
USE [crlandscaping]
GO

print '' print '*** Creating sp_retrieve_Prep_record_detail_list'
GO
CREATE PROCEDURE [dbo].[sp_retrieve_Prep_record_detail_list]
AS
	BEGIN
		SELECT [PrepRecord].[PrepRecordID], [PrepRecord].[EquipmentID], [PrepRecord].[EmployeeID], 
		[PrepRecord].[Description], [PrepRecord].[Date], [Equipment].[Name], [Employee].[FirstName], [Employee].[LastName]
		FROM [PrepRecord]
		INNER JOIN [Equipment] ON [Equipment].[EquipmentID] = [PrepRecord].[EquipmentID]
		INNER JOIN [Employee] ON [Employee].[EmployeeID] = [PrepRecord].[EmployeeID]
	END
GO
