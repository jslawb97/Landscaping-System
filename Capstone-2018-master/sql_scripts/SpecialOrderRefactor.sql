print '' print '*** Using database crlandscaping'
print '' print '*** In file SpecialOrderRefactor.sql'
GO
USE [crlandscaping]
GO

-- --------------------------TABLE CHANGES

print '' print 'Adding VendorID field to SpecialOrder'
GO
ALTER TABLE [SpecialOrder] ADD [VendorID] INT NULL
GO

print '' print 'Adding Vendor'

print '' print 'Inserting VendorID to current SpecialOrders' 
GO
UPDATE [SpecialOrder]
SET	[VendorID] = 1000000
GO

print '' print '*** Altering VendorID field to NOT NULL in SpecialOrder'
GO
ALTER TABLE [SpecialOrder] ALTER COLUMN [VendorID] INT NOT NULL
GO



print '' print '*** Creating SpecialOrder VendorID Foreign Key'
GO
ALTER TABLE [dbo].[SpecialOrder] WITH NOCHECK
	ADD CONSTRAINT [fk_SpecialOrder_VendorID] FOREIGN KEY ([VendorID])
	REFERENCES [dbo].[Vendor]([VendorID])
	ON UPDATE CASCADE
GO


print '' print '*** Creating sp_create_specialorder_with_vendor'
GO
CREATE PROCEDURE [dbo].[sp_create_specialorder_with_vendor]
	(
	 @EmployeeID           		[int],
     @JobID                		[int],
     @Date      				[date],
     @SupplyStatusID       		[nvarchar](100),
	 @VendorID					[int]
	)
AS
	BEGIN
		INSERT INTO [dbo].[SpecialOrder]
			([EmployeeID], [JobID], [Date], [SupplyStatusID], [VendorID])
	VALUES
		
		(@EmployeeID, @JobID, @Date, @SupplyStatusID, @VendorID)
		SELECT SCOPE_IDENTITY()
	END
GO


print '' print '*** Creating sp_retrieve_specialorder_list_with_vendor'
GO
CREATE PROCEDURE [dbo].[sp_retrieve_specialorder_list_with_vendor]
AS
	BEGIN
		SELECT 	[SpecialOrderID], [EmployeeID], [JobID], [Date], [SupplyStatusID], [VendorID]
		FROM 	[SpecialOrder]
	END
GO	


print '' print '*** Creating sp_retrieve_specialorder_by_id_with_vendor'
GO
CREATE PROCEDURE [dbo].[sp_retrieve_specialorder_by_id_with_vendor]
	(
	@SpecialOrderID 			[int]
	)
AS
	BEGIN
		SELECT 	[SpecialOrderID], [EmployeeID], [JobID], [SupplyStatusID], [Date], [VendorID]
		FROM 	[SpecialOrder]
		WHERE 	[SpecialOrderID] = @SpecialOrderID
	END
GO	

print '' print '*** Creating sp_edit_specialorder_with_vendor'
GO
CREATE PROCEDURE [dbo].[sp_edit_specialorder_with_vendor]
	(
	@SpecialOrderID        		[int],
    @OldEmployeeID           	[int],
    @OldJobID                	[int],
    @OldDate      				[date],
    @OldSupplyStatusID       	[nvarchar](100),
	@OldVendorID				[int],
    @NewEmployeeID           	[int],
    @NewJobID                	[int],
    @NewDate      				[date],
    @NewSupplyStatusID       	[nvarchar](100),
	@NewVendorID				[int]
	)
AS
	BEGIN
		UPDATE SpecialOrder 
        SET [EmployeeID] = @NewEmployeeID, [JobID] = @NewJobID,
        [Date] = @NewDate, [SupplyStatusID] = @NewSupplyStatusID
		where SpecialOrderID = @SpecialOrderID
        AND [EmployeeID] = @OldEmployeeID
        AND [Date] = @OldDate
        AND [SupplyStatusID] = @OldSupplyStatusID
		return @@rowcount
	END
GO


print '' print '*** Creating sp_retrieve_specialorderitem_detail'
GO
CREATE PROCEDURE [dbo].[sp_retrieve_specialorderitem_detail]
AS
	BEGIN
		SELECT	[SpecialOrderItem].[SpecialOrderItemID], [SpecialOrderItem].[Name], [SpecialOrderItem].[Active],
				[Source].[PriceEach], [Vendor].[Name], [Source].[SourceID], [Vendor].[VendorID]
		FROM	[SpecialOrderItem]
		INNER JOIN [Source]
		ON [SpecialOrderItem].[SpecialOrderItemID] = [Source].[SpecialOrderItemID]
		INNER JOIN [Vendor]
		ON [Source].[VendorID] = [Vendor].[VendorID]
	END
GO




















