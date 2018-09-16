/*Created by Zachary Hall 04/01/2018 */
print '' print '*** in file OrderLineReceivedTriggers.sql ***'
USE [crlandscaping]
GO

print '' print '*** Adding QuantityInStock column to SpecialOrderItem'
ALTER TABLE [SpecialOrderItem] ADD [QuantityInStock] int NOT NULL DEFAULT 0
GO

print '' print '*** Adding QuantityPulled column to TaskSupply'
ALTER TABLE [TaskSupply] ADD [QuantityPulled] int NOT NULL DEFAULT 0
GO

print '' print '*** Adding JobSiteQuantityReceived column to TaskSupply'
ALTER TABLE [TaskSupply] ADD [JobSiteQuantityReceived] int NOT NULL DEFAULT 0
GO

print '' print '*** Adding JobSiteQuantityReturned column to TaskSupply'
ALTER TABLE [TaskSupply] ADD [JobSiteQuantityReturned] int NOT NULL DEFAULT 0
GO

print '' print '*** Creating ResupplyOrderLine after update trigger ***'
GO
CREATE TRIGGER [dbo].[TRIG_ResupplyOrderLine_AFTERUPDATE]
	ON [dbo].[ResupplyOrderLine]
	AFTER UPDATE
AS 
	BEGIN
		
		SELECT INSERTED.[SupplyItemID], (INSERTED.[QtyReceived] - DELETED.[QtyReceived]) as [QuantityChanged]
		INTO #SupplyItemQtyChanged
		FROM INSERTED, DELETED
		WHERE INSERTED.[ResupplyOrderLineID] = DELETED.[ResupplyOrderLineID]
		
		UPDATE [SupplyItem]
			SET [SupplyItem].[QuantityInStock] = (#SupplyItemQtyChanged.[QuantityChanged] + [SupplyItem].[QuantityInStock])
		FROM #SupplyItemQtyChanged
		WHERE #SupplyItemQtyChanged.[SupplyItemID] = [SupplyItem].[SupplyItemID]
		
	END
GO

print '' print '*** Creating SpecialOrderLine after update trigger ***'
GO
CREATE TRIGGER [dbo].[TRIG_SpecialOrderLine_AFTERUPDATE]
	ON [dbo].[SpecialOrderLine]
	AFTER UPDATE
AS 
	BEGIN
		
		SELECT INSERTED.[SpecialOrderItemID], (INSERTED.[QtyReceived] - DELETED.[QtyReceived]) as [QuantityChanged]
		INTO #SpecialOrderItemQtyChanged
		FROM INSERTED, DELETED
		WHERE INSERTED.[SpecialOrderLineID] = DELETED.[SpecialOrderLineID]
		
		UPDATE [SpecialOrderItem]
			SET [SpecialOrderItem].[QuantityInStock] = (#SpecialOrderItemQtyChanged.[QuantityChanged] + [SpecialOrderItem].[QuantityInStock])
		FROM #SpecialOrderItemQtyChanged
		WHERE #SpecialOrderItemQtyChanged.[SpecialOrderItemID] = [SpecialOrderItem].[SpecialOrderItemID]
		
	END
GO

print '' print '*** Creating TaskSupply after update trigger ***'
GO
CREATE TRIGGER [dbo].[TRIG_TaskSupply_AFTERUPDATE]
	ON [dbo].[TaskSupply]
	AFTER UPDATE
AS 
	BEGIN
		
		SELECT INSERTED.[SupplyItemID], (DELETED.[QuantityPulled] - INSERTED.[QuantityPulled]) as [QuantityPulledChanged]
		INTO #TaskSupplyPulledChanged
		FROM INSERTED, DELETED
		WHERE INSERTED.[TaskSupplyID] = DELETED.[TaskSupplyID]
		
		SELECT INSERTED.[SupplyItemID], (INSERTED.[JobSiteQuantityReturned] - DELETED.[JobSiteQuantityReturned]) as [QuantityReturnedChanged]
		INTO #TaskSupplyJobSiteReturnedChanged
		FROM INSERTED, DELETED
		WHERE INSERTED.[TaskSupplyID] = DELETED.[TaskSupplyID]
		
		UPDATE [SupplyItem]
			SET [SupplyItem].[QuantityInStock] = (#TaskSupplyPulledChanged.[QuantityPulledChanged] + [SupplyItem].[QuantityInStock])
		FROM #TaskSupplyPulledChanged
		WHERE #TaskSupplyPulledChanged.[SupplyItemID] = [SupplyItem].[SupplyItemID]
		
		UPDATE [SupplyItem]
			SET [SupplyItem].[QuantityInStock] = (#TaskSupplyJobSiteReturnedChanged.[QuantityReturnedChanged] + [SupplyItem].[QuantityInStock])
		FROM #TaskSupplyJobSiteReturnedChanged
		WHERE #TaskSupplyJobSiteReturnedChanged.[SupplyItemID] = [SupplyItem].[SupplyItemID]
		
	END
GO