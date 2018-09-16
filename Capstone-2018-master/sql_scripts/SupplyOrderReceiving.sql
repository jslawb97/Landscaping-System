/* Created by James McPherson 4/20/2018 */
print '' print '*** In file SupplyOrderReceiving.sql'
USE [crlandscaping]
GO

print '' print '*** Adding QuantityReceived column to SupplyOrderLine'
ALTER TABLE [SupplyOrderLine] ADD [QuantityReceived] INT DEFAULT 0
GO

print '' print '*** Altering sp_retrieve_supplyorderline_by_supplyorderid to include QuantityReceived'
GO
ALTER PROCEDURE [sp_retrieve_supplyorderline_by_supplyorderid]
	(
	@SupplyOrderID				[int]
	)
AS
	BEGIN
		SELECT	[SupplyOrderLineID],[SupplyItemID],[Quantity],[QuantityReceived]
		FROM	[SupplyOrderLine]
		WHERE	[SupplyOrderID] = @SupplyOrderID
	END
GO

print '' print '*** Creating custom table type SupplyOrderLineTableType'
GO
CREATE TYPE [dbo].[SupplyOrderLineReceivedTableType] AS TABLE
	([SupplyOrderLineID]	[int], [QuantityReceived]	[int])
GO

print '' print '*** Creating sp_edit_supplyorderline_quantityreceived'
GO
CREATE PROCEDURE [sp_edit_supplyorderline_quantityreceived]
	(
	@SupplyOrderID				[int],
	@tvpNewQuantitiesReceived	[dbo].[SupplyOrderLineReceivedTableType] READONLY,
	@tvpOldQuantitiesReceived	[dbo].[SupplyOrderLineReceivedTableType] READONLY
	)
AS
	BEGIN
		DECLARE @SupplyOrderLineUpdateErr [int]
		DECLARE @SupplyItemUpdateErr [int]
		DECLARE @SupplyOrderUpdateErr [int]
		DECLARE @DifferentRows [int]
		DECLARE @err_message [nvarchar](255)
	
		BEGIN TRANSACTION
		/* Check for concurrency problems */
		SELECT 	[SupplyOrderLineID], [QuantityReceived]
		FROM	[SupplyOrderLine]
		WHERE	[SupplyOrderID] = @SupplyOrderID
		EXCEPT 
		SELECT	[SupplyOrderLineID], [QuantityReceived]
		FROM @tvpOldQuantitiesReceived AS [OldQuantities]
		SET @DifferentRows = @@ROWCOUNT
		
		/* Update SupplyOrderLine QuantityReceived, making sure to update only rows that were concurrency checked */
		UPDATE 	[SupplyOrderLine]
		SET		[SupplyOrderLine].[QuantityReceived] = [NewQuantities].[QuantityReceived]
		FROM	[SupplyOrderLine], @tvpNewQuantitiesReceived AS [NewQuantities], @tvpOldQuantitiesReceived AS [OldQuantities]
		WHERE	[SupplyOrderLine].[SupplyOrderLineID] = [NewQuantities].[SupplyOrderLineID]
		AND		[SupplyOrderLine].[SupplyOrderLineID] = [OldQuantities].[SupplyOrderLineID]
		AND		[SupplyOrderID] = @SupplyOrderID
		
		/* Check for update error */
		SET @SupplyOrderLineUpdateErr = @@error
		
		/* Update SupplyItem Quantity */
		UPDATE	[SupplyItem]
		SET		[QuantityInStock] = [QuantityInStock] + ([NewQuantities].[QuantityReceived]
											- [OldQuantities].[QuantityReceived])
		FROM	[SupplyItem], [SupplyOrderLine], @tvpNewQuantitiesReceived AS [NewQuantities], @tvpOldQuantitiesReceived AS [OldQuantities]
		WHERE	[SupplyItem].[SupplyItemID] = [SupplyOrderLine].[SupplyItemID]
		AND		[SupplyOrderLine].[SupplyOrderLineID] = [NewQuantities].[SupplyOrderLineID]
		AND		[SupplyOrderLine].[SupplyOrderLineID] = [OldQuantities].[SupplyOrderLineID]
		
		/* Check for update error */
		SET @SupplyItemUpdateErr = @@error
		
		UPDATE 	[SupplyOrder]
		SET		[SupplyStatusID] = "Received"
		FROM	[SupplyOrder]
		WHERE	[SupplyOrder].[SupplyOrderID] = @SupplyOrderID
		
		/* Check for update error */
		SET @SupplyOrderUpdateErr = @@error
		
		IF @DifferentRows <> 0
			BEGIN
				ROLLBACK
				SET @err_message = 'Could not properly edit the SupplyOrderLine records, concurrency problem'
				RAISERROR (@err_message, 18, 1)
			END
		ELSE IF @SupplyOrderLineUpdateErr <> 0
			BEGIN
				ROLLBACK
				SET @err_message = 'Could not properly update the SupplyOrderLine records'
				RAISERROR (@err_message, 18, 1)
			END
		ELSE IF @SupplyItemUpdateErr <> 0
			BEGIN
				ROLLBACK
				SET @err_message = 'Could not properly update the SupplyItem records'
				RAISERROR (@err_message, 18, 1)
			END
		ELSE IF @SupplyOrderUpdateErr <> 0
			BEGIN
				ROLLBACK
				SET @err_message = 'Could not properly update the SupplyOrder record'
				RAISERROR (@err_message, 18, 1)
			END
		ELSE
			BEGIN
				COMMIT
				RETURN @@ROWCOUNT
			END
	END
GO

print '' print '*** Create sp_edit_supplyorder_received'