print '' print '*** in file EmployeeCertificationDetail.sql ***'
USE [crlandscaping]
GO

print '' print '*** Adding QtyReceived bit field to ResupplyOrderLine'
GO
ALTER TABLE [ResupplyOrderLine] ADD [QtyReceived] INT NOT NULL DEFAULT 0
GO


print '' print '*** Creating sp_retrieve_resupply_order_line_detail_list_by_resupply_order_id_with_received'
GO
CREATE PROCEDURE [dbo].[sp_retrieve_resupply_order_line_detail_list_by_resupply_order_id_with_received]
	(
	@ResupplyOrderID	[int]
	)
AS
	BEGIN
		SELECT DISTINCT	[ResupplyOrderLine].[ResupplyOrderLineID],[ResupplyOrderLine].[SupplyItemID]
						,[ResupplyOrderLine].[Quantity]
						,[SupplyItem].[Name], [ResupplyOrderLine].[QtyReceived]
		FROM 		[ResupplyOrderLine],[Source],[SupplyItem], [ResupplyOrder]
		WHERE 		[ResupplyOrderLine].[ResupplyOrderID] = @ResupplyOrderID
		AND 		[SupplyItem].[SupplyItemID] = [ResupplyOrderLine].[SupplyItemID]
		AND 		[Source].[SupplyItemID] = [ResupplyOrderLine].[SupplyItemID]
		AND 		[ResupplyOrder].[VendorID] = [Source].[VendorID]
	END
GO


print '' print '*** Inserting into SupplyStatus Test Records'
GO
INSERT INTO [dbo].[SupplyStatus]
		([SupplyStatusID])
	VALUES
		('Ordered'),
		('Received')
GO

print '' print '*** Creating sp_edit_resupplyorder_status_by_id'
GO
CREATE PROCEDURE [dbo].[sp_edit_resupplyorder_status_by_id]
	(
		@ResupplyOrderID	[int],
		@OldStatus			[nvarchar](100),
		@NewStatus			[nvarchar](100)
	)
AS
	BEGIN
		UPDATE [ResupplyOrder]
		SET [SupplyStatusID] = @NewStatus
		WHERE [ResupplyOrderID] = @ResupplyOrderID
		AND [SupplyStatusID] = @OldStatus
		
		RETURN @@ROWCOUNT
	END
GO


print '' print '*** Creating sp_edit_resupplyorderline_qtyreceived_by_id'
GO
CREATE PROCEDURE [dbo].[sp_edit_resupplyorderline_qtyreceived_by_id]
	(
		@ResupplyOrderLineID	[int],
		@OldQtyReceived		[int],
		@NewQtyReceived		[int]
	)
AS
	BEGIN
		UPDATE [ResupplyOrderLine]
		SET [QtyReceived] = @NewQtyReceived
		WHERE [ResupplyOrderLineID] = @ResupplyOrderLineID
		AND [QtyReceived] = @OldQtyReceived		
		RETURN @@ROWCOUNT
	END
GO

print '' print '*** Creating sp_edit_resupplyorderlines_qtyreceived_to_quantityordered_by_resupplyorderid'
GO
CREATE PROCEDURE [dbo].[sp_edit_resupplyorderlines_qtyreceived_to_quantityordered_by_resupplyorderid]
	(
		@ResupplyOrderID	[int]
	)
AS
	BEGIN
		
		UPDATE [ResupplyOrder]
		SET [SupplyStatusID] = 'Received'
		WHERE [ResupplyOrderID] = @ResupplyOrderID		
		UPDATE [ResupplyOrderLine]
		SET [QtyReceived] = [Quantity]
		WHERE [ResupplyOrderID] = @ResupplyOrderID
		RETURN @@ROWCOUNT
	END
GO