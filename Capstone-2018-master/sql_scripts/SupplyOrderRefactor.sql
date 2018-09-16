print '' print '*** Using database crlandscaping'
print '' print '*** In file SupplyOrderRefactor.sql'
GO
USE [crlandscaping]
GO

print '' print '*** Creating VendorID SupplyItemID Unique Key'
GO
SET QUOTED_IDENTIFIER ON
CREATE UNIQUE NONCLUSTERED INDEX [ix_VendorID_SupplyItemID]
ON [Source] ([VendorID], [SupplyItemID])
WHERE [SupplyItemID] IS NOT NULL
GO

print '' print '*** Creating VendorID SpecialOrderItemID Unique Key'
GO
SET QUOTED_IDENTIFIER ON
CREATE UNIQUE NONCLUSTERED INDEX [ix_VendorID_SpecialOrderItemID]
ON [Source] ([VendorID], [SpecialOrderItemID])
WHERE [SpecialOrderItemID] IS NOT NULL
GO


