/* Jacob Conley */
print '' print '*** in file TaskEquipmentAllocation.sql ***'
USE [crlandscaping]
GO

print '' print '*** Altering JobID and EquipmentID to Unique in TaskEquipment'
ALTER TABLE TaskEquipment
ADD CONSTRAINT UC_JobID_EquipmentID UNIQUE (JobID,EquipmentID);

print '' print '*** Creating sp_delete_taskequipment_assignment'
GO
CREATE PROCEDURE [dbo].[sp_delete_taskequipment_assignment]
	(
		@JobID				[int],
		@EquipmentID		[int]
	)
AS
	BEGIN
		DELETE FROM	[TaskEquipment]
			WHERE	[JobID] = @JobID
			AND 	[EquipmentID] = @EquipmentID
		RETURN @@ROWCOUNT
	END
GO