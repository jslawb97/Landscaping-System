print '' print '*** Using database crlandscaping'
GO
USE [crlandscaping]
GO

print '' print '*** In file SpecialOrderLineCorrections.sql'
GO

/* print '' print '*** Adding SpecialOrderID field to SpecialOrderLine'
GO
ALTER TABLE [SpecialOrderLine] ADD [SpecialOrderID] INT NOT NULL
GO

print '' print '*** Creating SpecialOrderLine SpecialOrderID Foreign Key'
GO
ALTER TABLE [dbo].[SpecialOrderLine] WITH NOCHECK
	ADD CONSTRAINT [fk_SpecialOrderLine_SpecialOrderID] FOREIGN KEY([SpecialOrderID]) 
	REFERENCES [dbo].[SpecialOrder]([SpecialOrderID])
	ON UPDATE CASCADE
GO */

print '' print '*** Creating sp_edit_specialorderline'
GO
CREATE PROCEDURE [dbo].[sp_edit_specialorderline_correct]
	(
	@SpecialOrderLineID	[int],
	@OldQuantity		[int],
	@NewQuantity		[int]
	)
AS
	BEGIN
		UPDATE	[SpecialOrderLine]
			SET		[Quantity] = @NewQuantity
			WHERE	[SpecialOrderLineID] = @SpecialOrderLineID
			AND		[Quantity] = @OldQuantity
		RETURN @@ROWCOUNT
	END
GO

print '' print '*** Creating sp_delete_specialorderline'
GO
CREATE PROCEDURE [dbo].[sp_delete_specialorderline_correct]
	(
	@SpecialOrderLineID	[int]
	)
AS
	BEGIN
		DELETE FROM	[SpecialOrderLine]
			WHERE	[SpecialOrderLineID] = @SpecialOrderLineID
		RETURN @@ROWCOUNT
	END
GO

print '' print '*** Creating sp_create_specialorderline'
GO
CREATE PROCEDURE [dbo].[sp_create_specialorderline_correct]
	(
	@SpecialOrderID		[int],
	@SpecialOrderItemID	[int],
	@Quantity			[int]
	)
AS
	BEGIN
		INSERT INTO [dbo].[SpecialOrderLine]
			([SpecialOrderItemID],[SpecialOrderID],[Quantity])
		VALUES
			(@SpecialOrderItemID,@SpecialOrderID,@Quantity)
		SELECT SCOPE_IDENTITY() 
	END
GO	

print '' print '*** Creating sp_retrieve_specialorderline_by_id'
GO
CREATE PROCEDURE [dbo].[sp_retrieve_specialorderline_by_id_correct]
	(
	@SpecialOrderLineID	[int]
	)
AS
	BEGIN
		SELECT	[SpecialOrderLineID],[SpecialOrderID],[SpecialOrderItemID],[Quantity]
		FROM	[SpecialOrderLine]
		WHERE	[SpecialOrderLineID] = @SpecialOrderLineID
	END
GO

print '' print '*** Creating sp_retrieve_specialorderline_by_specialorderid'
GO
CREATE PROCEDURE [dbo].[sp_retrieve_specialorderline_by_specialorderid_correct]
	(
	@SpecialOrderID	[int]
	)
AS
	BEGIN
		SELECT	[SpecialOrderLineID],[SpecialOrderID],[SpecialOrderItemID],[Quantity]
		FROM	[SpecialOrderLine]
		WHERE	[SpecialOrderID] = @SpecialOrderID
	END
GO