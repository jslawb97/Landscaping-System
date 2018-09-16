/* James M */
print '' print '*** in file InspectionRecordDetail.sql ***'
USE [crlandscaping]
GO
print '' print '*** Creating sp_retrieve_inspectionrecord_detail_list'
GO
CREATE PROCEDURE [dbo].[sp_retrieve_inspectionrecord_detail_list]
AS
	BEGIN
		SELECT [InspectionRecordID], [InspectionRecord].[EquipmentID], [InspectionRecord].[EmployeeID]
		, [InspectionRecord].[Description], [InspectionRecord].[Date], [Equipment].[Name]
		, [Employee].[LastName], [Employee].[FirstName]
		FROM	[InspectionRecord], [Equipment], [Employee]
		WHERE	[InspectionRecord].[EquipmentID] = [Equipment].[EquipmentID]
		AND 	[InspectionRecord].[EmployeeID] = [Employee].[EmployeeID]
	END
GO