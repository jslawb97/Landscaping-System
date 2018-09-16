/* Weston Olund*/
print '' print '*** in file SupplyItemReorderFunctionality ***'
GO
USE [crlandscaping]
GO

print '' print '*** Creating sp_retrieve_supply_item_detail_where_quanity_in_stock_and_on_order_less_than_reorder_level'
GO 
CREATE PROCEDURE
[dbo].[Sp_retrieve_supply_item_detail_where_quanity_in_stock_and_on_order_less_than_reorder_level]
AS
	BEGIN
		 SELECT DISTINCT
			  [supplyitem].[supplyitemid],
			  [supplyitem].[name],
			  [supplyitem].[description],
			  [supplyitem].[location],
			  [supplyitem].[quantityinstock],
			  [supplyitem].[reorderlevel],
			  [supplyitem].[reorderquantity],
			  [supplyitem].[active],
			  SmallerSet.[priceeach],
			  SmallerSet.[name],
			  SmallerSet.[sourceid],
			  SmallerSet.[vendorid]
		 FROM supplyitem,
			  (SELECT DISTINCT
				   supplyitem.supplyitemid,
				   supplyitem.NAME AS "SupplyItemName",
				   source.priceeach,
				   source.sourceid,
				   source.vendorid,
				   vendor.vendorid AS "VendorName",
				   vendor.NAME
			  FROM supplyitem
			  INNER JOIN source
				   ON supplyitem.supplyitemid = source.supplyitemid
			  INNER JOIN vendor
				   ON source.vendorid = vendor.vendorid) AS SmallerSet,
			  (SELECT
				   SUM(resupplyorderline.quantity) AS ON_ORDER,
				   resupplyorderline.supplyitemid AS ID
			  FROM resupplyorderline,
				   supplyitem
			  WHERE resupplyorderline.supplyitemid = supplyitem.supplyitemid
			  GROUP BY resupplyorderline.supplyitemid) AS VAL
		 WHERE supplyitem.quantityinstock < (
		 supplyitem.reorderlevel - VAL.on_order
		 )
		 AND supplyitem.supplyitemid = VAL.id
		 AND VAL.id = SmallerSet.supplyitemid
	END
GO





print '' print '*** Creating sp_retrieve_supply_item_detail_where_quanity_in_stock_and_item_not_on_order_less_than_reorder_level'
GO
CREATE PROCEDURE [dbo].[sp_retrieve_supply_item_detail_where_quanity_in_stock_and_item_not_on_order_less_than_reorder_level]
AS
BEGIN
     SELECT DISTINCT
          [SupplyItem].[SupplyItemID],
          [SupplyItem].[Name],
          [SupplyItem].[Description],
          [SupplyItem].[Location],
          [SupplyItem].[QuantityInStock],
          [SupplyItem].[ReorderLevel],
          [SupplyItem].[ReorderQuantity],
          [SupplyItem].[Active],
          SmallerSet.[PriceEach],
          SmallerSet.[Name],
          SmallerSet.[SourceID],
          SmallerSet.[VendorID]
     FROM SupplyItem,
          (SELECT DISTINCT
               SupplyItem.SupplyItemID,
               SupplyItem.Name AS "SupplyItemName",
               Source.PriceEach,
               Source.SourceID,
               Source.VendorID,
               Vendor.VendorID AS "VendorName",
               Vendor.Name
          FROM SupplyItem
          INNER JOIN Source
               ON SupplyItem.SupplyItemID = Source.SupplyItemID
          INNER JOIN Vendor
               ON Source.VendorID = Vendor.VendorID) AS SmallerSet
     WHERE SupplyItem.SupplyItemID NOT IN (SELECT DISTINCT
          ReSupplyOrderLine.SupplyItemID AS ID
     FROM ResupplyOrderLine,
          ResupplyOrder,
          (SELECT DISTINCT
               SupplyItem.SupplyItemID,
               SupplyItem.Name AS "SupplyItemName",
               Source.PriceEach,
               Source.SourceID,
               Source.VendorID,
               Vendor.VendorID AS "VendorName",
               Vendor.Name
          FROM SupplyItem
          INNER JOIN Source
               ON SupplyItem.SupplyItemID = Source.SupplyItemID
          INNER JOIN Vendor
               ON Source.VendorID = Vendor.VendorID) AS SmallerSet
     WHERE ResupplyOrderLine.ResupplyOrderID = ResupplyOrder.ResupplyOrderID
     AND ResupplyOrder.SupplyStatusID = 'Ordered')
     AND SupplyItem.QuantityInStock < SupplyItem.ReorderLevel
     AND SmallerSet.SupplyItemID = SupplyItem.SupplyItemID
     AND SupplyItem.SupplyItemID NOT IN (SELECT DISTINCT
          SupplyItem.SupplyItemID
     FROM SupplyItem,
          ReSupplyOrderLine,
          (SELECT
               SUM(ResupplyOrderLine.Quantity) AS ON_ORDER,
               ReSupplyOrderLine.SupplyItemID AS ID
          FROM ResupplyOrderLine,
               SupplyItem
          WHERE ResupplyOrderLine.SupplyItemID = SupplyItem.SupplyItemID
          GROUP BY ReSupplyOrderLine.SupplyItemID) AS VAL
     WHERE SupplyItem.QuantityInStock < (
     SupplyItem.ReorderLevel - VAL.ON_ORDER)
     AND SupplyItem.SupplyItemID = VAL.ID)
END
GO


print '' print '*** Creating sp_retrieve_supply_status_list'
GO
CREATE PROCEDURE [dbo].[sp_retrieve_supply_status_list]
AS
	BEGIN
		SELECT SupplyStatusID
		FROM SupplyStatus
	END
GO