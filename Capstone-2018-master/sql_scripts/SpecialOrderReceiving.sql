print '' print '*** in file SpecialOrderReceiving.sql ***'
USE [crlandscaping]
GO

print '' print '*** Adding QtyReceived int field to SpecialOrderLine'
GO
ALTER TABLE [SpecialOrderLine] ADD [QtyReceived] INT NOT NULL DEFAULT 0
GO


print '' print '*** Creating sp_retrieve_special_order_line_detail_list_by_special_order_id_with_received'
GO
CREATE PROCEDURE [dbo].[sp_retrieve_special_order_line_detail_list_by_special_order_id_with_received]
	(
	@SpecialOrderID	[int]
	)
AS
	BEGIN
		SELECT DISTINCT [SpecialOrderLine].[SpecialOrderLineID],[SpecialOrderLine].[SpecialOrderItemID]
						,[SpecialOrderLine].[Quantity], [Source].[PriceEach]
						,[SpecialOrderItem].[Name], [SpecialOrderLine].[QtyReceived]
		FROM 		[SpecialOrderLine],[Source],[SpecialOrderItem]
		WHERE 	[SpecialOrderID] = @SpecialOrderID
		AND 		[SpecialOrderItem].[SpecialOrderItemID] = [SpecialOrderLine].[SpecialOrderItemID]
		AND 		[Source].[SpecialOrderItemID] = [SpecialOrderLine].[SpecialOrderItemID]
	END
GO

print '' print '*** Creating sp_edit_specialorder_status_by_id'
GO
CREATE PROCEDURE [dbo].[sp_edit_specialorder_status_by_id]
	(
		@SpecialOrderID		[int],
		@OldStatus			[nvarchar](100),
		@NewStatus			[nvarchar](100)
	)
AS
	BEGIN
		UPDATE [SpecialOrder]
		SET [SupplyStatusID] = @NewStatus
		WHERE [SpecialOrderID] = @SpecialOrderID
		AND [SupplyStatusID] = @OldStatus
		
		RETURN @@ROWCOUNT
	END
GO


print '' print '*** Creating sp_edit_specialorderline_qtyreceived_by_id'
GO
CREATE PROCEDURE [dbo].[sp_edit_specialorderline_qtyreceived_by_id]
	(
		@SpecialOrderLineID	[int],
		@OldQtyReceived		[int],
		@NewQtyReceived		[int]
	)
AS
	BEGIN
		UPDATE [SpecialOrderLine]
		SET [QtyReceived] = @NewQtyReceived
		WHERE [SpecialOrderLineID] = @SpecialOrderLineID
		AND [QtyReceived] = @OldQtyReceived
		
		RETURN @@ROWCOUNT
	END
GO


print '' print '*** Creating sp_edit_specialorderlines_qtyreceived_to_quantityordered_by_specialorderid'
GO
CREATE PROCEDURE [dbo].[sp_edit_specialorderlines_qtyreceived_to_quantityordered_by_specialorderid]
	(
		@SpecialOrderID	[int]
	)
AS
	BEGIN
		
		UPDATE [SpecialOrder]
		SET [SupplyStatusID] = 'Received'
		WHERE [SpecialOrderID] = @SpecialOrderID
		
		UPDATE [SpecialOrderLine]
		SET [QtyReceived] = [Quantity]
		WHERE [SpecialOrderID] = @SpecialOrderID
		
		
		RETURN @@ROWCOUNT
	END
GO



