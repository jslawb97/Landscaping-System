/* Weston Olund*/
print '' print '*** in file ResupplyOrder.sql ***'
GO
USE [crlandscaping]
GO

print '' print '*** Creating sp_retrieve_supply_item_detail'
GO
CREATE PROCEDURE [dbo].[sp_retrieve_supply_item_detail]
AS
	BEGIN
		SELECT  [SupplyItem].[SupplyItemID],[SupplyItem].[Name],
				[SupplyItem].[Description],[SupplyItem].[Location],
				[SupplyItem].[QuantityInStock],[SupplyItem].[ReorderLevel],
				[SupplyItem].[ReorderQuantity],[SupplyItem].[Active],
				[Source].[PriceEach], [Vendor].[Name], [Source].[SourceID]
				, [Vendor].[VendorID]
		FROM [SupplyItem]
		INNER JOIN [Source]
		ON [SupplyItem].[SupplyItemID] = [Source].[SupplyItemID]
		INNER JOIN [Vendor]
		ON [Source].[VendorID] = [Vendor].[VendorID]
	END
GO

print '' print '*** Creating sp_create_resupply_order_line'
GO
CREATE PROCEDURE [dbo].[sp_create_resupply_order_line]
	(
	@ResupplyOrderID			    [int],
	@SupplyItemID	      			[int],
  @Quantity         				[int],
	@Price         					[money]
	)
AS
	BEGIN
		INSERT INTO [dbo].[ResupplyOrderLine]
		([ResupplyOrderID], [SupplyItemID], [Quantity],[Price])
		VALUES	(
			@ResupplyOrderID, @SupplyItemID, @Quantity, @Price
		)
		SELECT SCOPE_IDENTITY()
	END
GO

print '' print '*** Creating sp_edit_resupply_order_line'
GO
CREATE PROCEDURE [dbo].[sp_edit_resupply_order_line]
	(
		@ResupplyOrderLineID		[int]
    , @OldQuantity      			[int]
    , @NewQuantity      			[int]
    , @NewPrice          			[money]
	)
AS
	BEGIN
		UPDATE [ResupplyOrderLine]
			SET  [Quantity] = @NewQuantity
          , [Price] = @NewPrice
			WHERE [ResupplyOrderLineID] = @ResupplyOrderLineID
      AND [Quantity] = @OldQuantity
			RETURN @@ROWCOUNT
	END
GO

print '' print '*** Creating sp_delete_resupply_order_line_by_resupply_order_line_id'
GO
CREATE PROCEDURE [dbo].[sp_delete_resupply_order_line_by_resupply_order_line_id]
	(
	@ResupplyOrderLineID				[int]
	)
AS
	BEGIN
	DELETE FROM [ResupplyOrderLine]
	WHERE		[ResupplyOrderLineID] = @ResupplyOrderLineID
	RETURN @@ROWCOUNT
	END
GO

print '' print '*** Creating sp_delete_resupply_order_line_by_resupply_order_id'
GO 
CREATE PROCEDURE [dbo].[sp_delete_resupply_order_line_by_resupply_order_id]
	(
	@ResupplyOrderID	[int]
	)
AS
	BEGIN
		DELETE FROM[ResupplyOrderLine]
		WHERE [ResupplyOrderID] = @ResupplyOrderID
		RETURN @@ROWCOUNT
	END
GO	

print '' print '*** Creating sp_delete_resupply_order_by_id'
GO 
CREATE PROCEDURE [dbo].[sp_delete_resupply_order_by_id]
	(
	@ResupplyOrderID	[int]
	)
AS
	BEGIN
		DELETE FROM[ResupplyOrder]
		WHERE [ResupplyOrderID] = @ResupplyOrderID
		RETURN @@ROWCOUNT
	END
GO	



print '' print '*** Creating retrieve_supply_item_detail_by_vendor_id'
GO
CREATE PROCEDURE [dbo].[retrieve_supply_item_detail_by_vendor_id]
	(
	@VendorID		[int]
	)
AS
	BEGIN
		SELECT  [SupplyItem].[SupplyItemID],[SupplyItem].[Name],
				[SupplyItem].[Description],[SupplyItem].[Location],
				[SupplyItem].[QuantityInStock],[SupplyItem].[ReorderLevel],
				[SupplyItem].[ReorderQuantity],[SupplyItem].[Active],
				[Source].[PriceEach], [Vendor].[Name], [Source].[SourceID]
		FROM [SupplyItem]
		INNER JOIN [Source]
		ON [SupplyItem].[SupplyItemID] = [Source].[SupplyItemID]
		INNER JOIN [Vendor]
		ON [Source].[VendorID] = [Vendor].[VendorID]
		WHERE [Source].[VendorID] = @VendorID		
	END
GO



print '' print '*** Creating ResupplyOrder VendorID Foreign Key'
GO
ALTER TABLE [dbo].[ResupplyOrder] WITH NOCHECK
	ADD CONSTRAINT [fk_ResupplyOrder_VendorID] FOREIGN KEY([VendorID])
	REFERENCES [dbo].[Vendor] ([VendorID])
	ON UPDATE NO ACTION
GO

 print '' print '*** Creating sp_retrieve_resupply_order_line_detail_list_by_resupply_order_id'
GO
CREATE PROCEDURE [dbo].[sp_retrieve_resupply_order_line_detail_list_by_resupply_order_id]
	(
	@ResupplyOrderID	[int]
	)
AS
	BEGIN
		SELECT  DISTINCT [ResupplyOrderLine].[ResupplyOrderLineID],[ResupplyOrderLine].[SupplyItemID]
				, [ResupplyOrderLine].[Quantity], [ResupplyOrderLine].[Price], [Vendor].[Name]
				, [Vendor].[VendorID], [SupplyItem].[Name]
		FROM 		[ResupplyOrderLine]										
		INNER JOIN [ResupplyOrder]
		ON			[ResupplyOrderLine].[ResupplyOrderID] = [ResupplyOrder].[ResupplyOrderID]										
		INNER JOIN [Vendor]
		ON			[ResupplyOrder].[VendorID] = [Vendor].[VendorID]
		INNER JOIN [Source]
		ON 			[Vendor].[VendorID] = [Source].[VendorID]
		INNER JOIN [SupplyItem]
		ON 			[ResupplyOrderLine].[SupplyItemID] = [SupplyItem].[SupplyItemID]
		WHERE		[ResupplyOrderLine].[ResupplyOrderID] = @ResupplyOrderID
	END
GO 