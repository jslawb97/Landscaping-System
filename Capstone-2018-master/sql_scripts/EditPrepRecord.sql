/*Created by Badis Saidani 05/04/2018 */
print '' print '*** in file EditPrepRecordRecordDetail.sql ***'
GO
print '' print '*** Using database crlandscaping'
USE [crlandscaping]
GO


print '' print '*** Creating sp_edit_preprecord_by_id'
GO
CREATE PROCEDURE [dbo].[sp_edit_preprecord_by_id]
	(
	@PrepRecordID			[int],
	@OldEquipmentID		[int],
	@NewEquipmentID		[int],
	@OldEmployeeID		[int],
	@NewEmployeeID		[int],
	@OldDescription 	[NVARCHAR] (1000),
	@NewDescription 	[NVARCHAR] (1000),
	@OldDate			[Date],
	@NewDate 			[Date]
	)
AS
	BEGIN
		UPDATE 	[PrepRecord]
			SET 	
					[EquipmentID] = @NewEquipmentID,
					[EmployeeID] = @NewEmployeeID,
					[Description] = @NewDescription,
					[Date] = @NewDate
					
			WHERE 	[PrepRecordID] = @PrepRecordID
			AND [EquipmentID] = @OldEquipmentID
			AND [EmployeeID] = @OldEmployeeID
			AND [Description] = @OldDescription
			AND [Date] = @OldDate
		RETURN @@ROWCOUNT
	END
GO