print '' print '*** In script supply_SPROCS.sql'
print '' print '*** Using database crlandscaping'
GO
USE [crlandscaping]
GO

/* Badis S */
/* Tested by James McPherson 2/3/2018 */
print '' print '*** Creating sp_create_supplyorder'
GO
CREATE PROCEDURE [dbo].[sp_create_supplyorder]
	(
	 @EmployeeID           		[int],
     @JobID                		[int],
     @SupplyStatusID       		[nvarchar](100),
     @Date      				[date]
	)
AS
	BEGIN
		INSERT INTO [dbo].[SupplyOrder]
			([EmployeeID], [JobID], [SupplyStatusID], [Date])
	VALUES
		(@EmployeeID, @JobID, @SupplyStatusID, @Date)
		SELECT SCOPE_IDENTITY()
	END
GO

/* Tested by James McPherson 2/3/2018 */
print '' print '*** Creating sp_retrieve_supplyorder_list'
GO
CREATE PROCEDURE [dbo].[sp_retrieve_supplyorder_list]
AS
	BEGIN
		SELECT 	[SupplyOrderID], [EmployeeID], [JobID], [SupplyStatusID], [Date]
		FROM 	[SupplyOrder]
	END
GO	

/* Tested by James McPherson 2/3/2018 */
print '' print '*** Creating sp_retrieve_supplyorder_by_id'
GO
CREATE PROCEDURE [dbo].[sp_retrieve_supplyorder_by_id]
	(
	@SupplyOrderID 				[int]
	)
AS
	BEGIN
		SELECT 	[SupplyOrderID], [EmployeeID], [JobID], [SupplyStatusID], [Date]
		FROM 	[SupplyOrder]
		WHERE 	[SupplyOrderID] = @SupplyOrderID
	END
GO	

/* Tested by James McPherson 2/3/2018 */
print '' print '*** Creating sp_retrieve_supplyorder_by_employeeid'
GO
CREATE PROCEDURE [dbo].[sp_retrieve_supplyorder_by_employeeid]
	(
	@EmployeeID 				[int]
	)
AS
	BEGIN
		SELECT 	[SupplyOrderID], [EmployeeID], [JobID], [SupplyStatusID], [Date]
		FROM 	[SupplyOrder]
		WHERE 	[EmployeeID] = @EmployeeID
	END
GO	

/* Tested by James McPherson 2/3/2018 */
print '' print '*** Creating sp_retrieve_supplyorder_by_jobid'
GO
CREATE PROCEDURE [dbo].[sp_retrieve_supplyorder_by_jobid]
	(
	@JobID 						[int]
	)
AS
	BEGIN
		SELECT 	[SupplyOrderID], [EmployeeID], [JobID], [SupplyStatusID], [Date]
		FROM 	[SupplyOrder]
		WHERE 	[JobID] = @JobID
	END
GO	

/* Untested */
print '' print '*** Creating sp_retrieve_supplyorder_by_job_supplystatusid'
GO
CREATE PROCEDURE [dbo].[sp_retrieve_supplyorder_by_job_supplystatusid]
	(
	@JobID 						[int],
    @SupplyStatusID    			[nvarchar](100)
	)
AS
	BEGIN
		SELECT 	[SupplyOrderID], [EmployeeID], [JobID], [SupplyStatusID], [Date]
		FROM 	[SupplyOrder]
		WHERE 	[JobID] = @JobID
        AND     [SupplyStatusID] = @SupplyStatusID
	END
GO	

/* Tested by James McPherson 2/3/2018 */
print '' print '*** Creating sp_edit_supplyorder_by_id'
GO
CREATE PROCEDURE [dbo].[sp_edit_supplyorder_by_id]
	(
	@SupplyOrderID       		[int],
    @OldEmployeeID           	[int],
    @OldJobID                	[int],
    @OldSupplyStatusID       	[nvarchar](100),
    @OldDate     	[date],
    @NewEmployeeID           	[int],
    @NewJobID               	[int],
    @NewSupplyStatusID       	[nvarchar](100),   
    @NewDate      	[date] 
	)
AS
	BEGIN
		UPDATE SupplyOrder 
        SET [EmployeeID] = @NewEmployeeID, [JobID] = @NewJobID,
        [Date] = @NewDate, [SupplyStatusID] = @NewSupplyStatusID
		where SupplyOrderID = @SupplyOrderID
        AND [EmployeeID] = @OldEmployeeID
        AND [JobID] = @OldJobID
        AND [Date] = @OldDate
        AND [SupplyStatusID] = @OldSupplyStatusID
		RETURN @@ROWCOUNT
	END
GO

/* Tested by James McPherson 2/3/2018 */
print '' print '*** Creating sp_delete_supplyorder_by_id'
GO
CREATE PROCEDURE [dbo].[sp_delete_supplyorder_by_id]
	(
	@SupplyOrderID				[int]
	)
AS
	BEGIN
		DELETE 
		FROM [SupplyOrder]
		WHERE [SupplyOrderID] = @SupplyOrderID
		RETURN @@ROWCOUNT
	END
GO

/* Tested by James McPherson 2/20/2018 */
print '' print '*** Creating sp_create_specialorder'
GO
CREATE PROCEDURE [dbo].[sp_create_specialorder]
	(
	 @EmployeeID           		[int],
     @JobID                		[int],
     @Date      				[date],
     @SupplyStatusID       		[nvarchar](100)
	)
AS
	BEGIN
		INSERT INTO [dbo].[SpecialOrder]
			([EmployeeID], [JobID], [Date], [SupplyStatusID])
	VALUES
		
		(@EmployeeID, @JobID, @Date, @SupplyStatusID)
		SELECT SCOPE_IDENTITY()
	END
GO

/* Tested by James McPherson 2/20/2018 */
print '' print '*** Creating sp_retrieve_specialorder_list'
GO
CREATE PROCEDURE [dbo].[sp_retrieve_specialorder_list]
	
AS
	BEGIN
		SELECT 	[SpecialOrderID], [EmployeeID], [JobID], [Date], [SupplyStatusID]
		FROM 	[SpecialOrder]
	END
GO	

/* Tested by James McPherson 2/20/2018 */
print '' print '*** Creating sp_retrieve_specialorder_by_id'
GO
CREATE PROCEDURE [dbo].[sp_retrieve_specialorder_by_id]
	(
	@SpecialOrderID 			[int]
	)
AS
	BEGIN
		SELECT 	[SpecialOrderID], [EmployeeID], [JobID], [SupplyStatusID], [Date]
		FROM 	[SpecialOrder]
		WHERE 	[SpecialOrderID] = @SpecialOrderID
	END
GO	

/* Tested by James McPherson 2/20/2018 */
print '' print '*** Creating sp_retrieve_specialorder_by_employeeid'
GO
CREATE PROCEDURE [dbo].[sp_retrieve_specialorder_by_employeeid]
	(
	@EmployeeID 				[int]
	)
AS
	BEGIN
		SELECT 	[SpecialOrderID], [EmployeeID], [JobID], [Date], [SupplyStatusID]
		FROM 	[SpecialOrder]
		WHERE 	[EmployeeID] = @EmployeeID
	END
GO	

/* Tested by James McPherson 2/20/2018 */
print '' print '*** Creating sp_retrieve_specialorder_by_jobid'
GO
CREATE PROCEDURE [dbo].[sp_retrieve_specialorder_by_jobid]
	(
	@JobID 						[int]
	)
AS
	BEGIN
		SELECT 	[SpecialOrderID], [EmployeeID], [JobID], [SupplyStatusID], [Date]
		FROM 	[SpecialOrder]
		WHERE 	[JobID] = @JobID
	END
GO	

/* Untested */
print '' print '*** Creating sp_retrieve_specialorder_by_job_supplystatusid'
GO
CREATE PROCEDURE [dbo].[sp_retrieve_specialorder_by_job_supplystatusid]
	(
	@JobID 	int,
    @SupplyStatusID    			[nvarchar](100)
	)
AS
	BEGIN
		SELECT 	[SpecialOrderID], [EmployeeID], [JobID], [SupplyStatusID], [Date]
		FROM 	[SpecialOrder]
		WHERE 	[JobID] = @JobID
        AND     [SupplyStatusID] = @SupplyStatusID
	END
GO	

/* Tested by James McPherson 2/20/2018 */
print '' print '*** Creating sp_edit_specialorder'
GO
CREATE PROCEDURE [dbo].[sp_edit_specialorder]
	(
	@SpecialOrderID        		[int],
    @OldEmployeeID           	[int],
    @OldJobID                	[int],
    @OldDate      				[date],
    @OldSupplyStatusID       	[nvarchar](100),
    @NewEmployeeID           	[int],
    @NewJobID                	[int],
    @NewDate      				[date],
    @NewSupplyStatusID       	[nvarchar](100)
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

/* Tested by James McPherson 2/20/2018 */
print '' print '*** Creating sp_delete_specialorder_by_id'
GO
CREATE PROCEDURE [dbo].[sp_delete_specialorder_by_id]
	(
	@SpecialOrderID				[int]
	)
AS
	BEGIN
		DELETE 
		FROM [SpecialOrder]
		WHERE [SpecialOrderID] = @SpecialOrderID
		RETURN @@ROWCOUNT
	END
GO

/* Untested */
print '' print '*** Creating sp_create_delivery'
GO
CREATE PROCEDURE [dbo].sp_create_delivery
	(
	 @Address           		[nvarchar](250),
     @Time                		[datetime],
     @SupplyStatusID      		[nvarchar](100)
	)
AS
	BEGIN
		INSERT INTO [dbo].[Delivery]
			([Address], [Time], [SupplyStatus].[SupplyStatusID])
	VALUES
		(@Address, @Time, @SupplyStatusID)
		SELECT SCOPE_IDENTITY()
	END
GO

/* Tested by James McPherson 2/20/2018 */
print '' print '*** Creating sp_create_supplystatus'
GO
CREATE PROCEDURE [dbo].[sp_create_supplystatus]
	(
	 @SupplyStatusID          	[nvarchar](100)
	)
AS
	BEGIN
		INSERT INTO [dbo].[SupplyStatus]
			([SupplyStatusID])
		VALUES
			(@SupplyStatusID)
	END
GO

/* Tested by James McPherson 2/20/2018 */
print '' print '*** Creating sp_retrieve_supplystatus_list'
GO
CREATE PROCEDURE [dbo].[sp_retrieve_supplystatus_list]
	
AS
	BEGIN
		SELECT 	[SupplyStatusID]
		FROM 	[SupplyStatus]
	END
GO	

/* Tested by James McPherson 2/20/2018 */
print '' print '*** Creating sp_edit_supplystatus'
GO
CREATE PROCEDURE [dbo].[sp_edit_supplystatus]
	(
	@NewSupplyStatusID        		[nvarchar](100),
	@OldSupplyStatusID        		[nvarchar](100)
	)
AS
	BEGIN
		UPDATE SupplyStatus 
        SET [SupplyStatusID] = @NewSupplyStatusID
		WHERE [SupplyStatusID] = @OldSupplyStatusID
		RETURN @@ROWCOUNT
	END
GO

/* Tested by James McPherson 2/20/2018 */
print '' print '*** Creating sp_delete_supplystatus'
GO
CREATE PROCEDURE [dbo].[sp_delete_supplystatus]
	(
	@SupplyStatusID				[nvarchar](100) 
	)
AS
	BEGIN
		DELETE 
		FROM [SupplyStatus]
		WHERE [SupplyStatusID] = @SupplyStatusID
		RETURN @@ROWCOUNT
	END
GO
/* End Badis S */

print '' print '*** Creating sp_edit_specialorderitem'
GO
CREATE PROCEDURE [dbo].[sp_edit_specialorderitem]
	(
	@SpecialOrderItemID	[int],
	@OldName			[nvarchar](100),
	@NewName			[nvarchar](100),
	@OldActive			[bit],
	@NewActive			[bit]
	)
AS
	BEGIN
		UPDATE	[SpecialOrderItem]
			SET		[Name] = @NewName
			, [Active] = @NewActive
			WHERE	[SpecialOrderItemID] = @SpecialOrderItemID
			AND		[Name] = @OldName
			AND [Active] = @OldActive
		RETURN @@ROWCOUNT
	END
GO

print '' print '*** Creating sp_create_specialorderitem'
GO
CREATE PROCEDURE [dbo].[sp_create_specialorderitem]
	(
	@Name	[nvarchar](100),
	@Active	[bit]
	)
AS
	BEGIN
		INSERT INTO [dbo].[SpecialOrderItem]
			([Name], [Active])
		VALUES
			(@Name, @Active)
		SELECT SCOPE_IDENTITY()
	END
GO

print '' print '*** Creating sp_retrieve_specialorderitem_list'
GO
CREATE PROCEDURE [dbo].[sp_retrieve_specialorderitem_list]
AS
	BEGIN
		SELECT [SpecialOrderItemID], [Name], [Active]
		FROM [SpecialOrderItem]
	END
GO

print '' print '*** Creating sp_edit_supplyitem_by_id'
GO
CREATE PROCEDURE [dbo].[sp_edit_supplyitem_by_id]
	(
	@SupplyItemID				[int],
	@OldName			        nvarchar(100),
	@NewName			        nvarchar(100),
	@OldDescription		        nvarchar(1000),
	@NewDescription		        nvarchar(1000),
    @OldLocation                nvarchar(100),
    @NewLocation                nvarchar(100),
    @OldQuantityInStock         int,
    @NewQuantityInStock         int,
    @OldReorderLevel            int,
    @NewReorderLevel            int,
    @OldReorderQuantity         int,
    @NewReorderQuantity         int,
	@OldActive					bit,
	@NewActive					bit
	)
AS
	BEGIN
		UPDATE [SupplyItem]
			SET [Name] = @NewName 
			, [Description] = @NewDescription
            , [Location] = @NewLocation
            , [QuantityInStock] = @NewQuantityInStock
            , [ReorderLevel] = @NewReorderLevel
            , [ReorderQuantity] = @NewReorderQuantity
			, [Active] = @NewActive
			WHERE [Name] = @OldName 
			AND [Description] = @OldDescription
            AND [Location] = @OldLocation
            AND [QuantityInStock] = @OldQuantityInStock
            AND [ReorderLevel] = @OldReorderLevel
            AND [ReorderQuantity] = @OldReorderQuantity
            AND [SupplyItemID] = @SupplyItemID
			AND [Active] = @OldActive
		RETURN @@ROWCOUNT
	END
GO

print '' print '*** Creating sp_create_supplyitem'
GO
CREATE PROCEDURE [dbo].[sp_create_supplyitem]
	(
	@Name			    [nvarchar](100),
	@Description	    [nvarchar](1000),
    @Location           [nvarchar](100),
    @QuantityInStock    [int],
    @ReorderLevel       [int],
    @ReorderQuantity    [int],
	@Active				[bit]
	)
AS
	BEGIN
		INSERT INTO [dbo].[SupplyItem]
		(Name, Description, Location, QuantityInStock, ReorderLevel, ReorderQuantity, Active)
		VALUES	(
			@Name, @Description, @Location, @QuantityInStock, @ReorderLevel, @ReorderQuantity, @Active
		)
		SELECT SCOPE_IDENTITY()
	END
GO

print '' print '*** Creating sp_retrieve_supplyitem_list'
GO
CREATE PROCEDURE [dbo].[sp_retrieve_supplyitem_list]
AS
	BEGIN
		SELECT [SupplyItemID], [Name], [Description], [Location], [QuantityInStock], [ReorderLevel], [ReorderQuantity], [Active]
		FROM [SupplyItem]
	END
GO


/* Jayden T */
/* fixed by Badis Saidani */
print '' print '*** Creating sp_retrieve_source_detail_list'
GO
CREATE PROCEDURE [dbo].[sp_retrieve_source_detail_list]
AS
	BEGIN
		SELECT [SourceID], [SupplyItem].[SupplyItemID], [SpecialOrderItem].[SpecialOrderItemID], [Vendor].[VendorId], [MinimumOrderQTY], [PriceEach], [LeadTime], [Source].[Active], [SpecialOrderItem].[Name],
		[Vendor].[Name], [SupplyItem].[Name]
		FROM [SpecialOrderItem], [Vendor], [SupplyItem], [Source]
		WHERE [Source].[VendorID] = [Vendor].[VendorID]
		AND [Source].[SpecialOrderItemID] = [SpecialOrderItem].[SpecialOrderItemID]
		AND [Source].[SupplyItemID] = [SupplyItem].[SupplyItemID]
	END
GO

print '' print '*** Creating sp_create_vendor'
GO
CREATE PROCEDURE [dbo].[sp_create_vendor]
	(
		@Name					[nvarchar](100),
		@Rep					[nvarchar](100),
		@Address				[nvarchar](250),
		@Website				[nvarchar](250),
		@Phone					[nvarchar](15)
	)
AS
	BEGIN
		INSERT INTO [dbo].[Vendor]
		([Name], [Rep], [Address], [Website], [Phone])
		VALUES
		(@Name, @Rep, @Address, @Website, @Phone)
		RETURN @@ROWCOUNT
	END
GO


print '' print '*** Creating sp_retrieve_vendor_list'
GO
CREATE PROCEDURE [dbo].[sp_retrieve_vendor_list]
AS
	BEGIN
		SELECT 	[VendorID], [Name], [Rep],
				[Address], [Website], [Phone], [Active]
		FROM 	[Vendor]
	END
GO


print '' print '*** Creating sp_retrieve_vendor_by_id'
GO
CREATE PROCEDURE [dbo].[sp_retrieve_vendor_by_id]
	(
		@VendorID		[int]
	)
AS
	BEGIN
		SELECT 	[VendorID], [Name], [Rep],
				[Address], [Website], [Phone], [Active]
		FROM 	[Vendor]
		WHERE 	[VendorID] = @VendorID
	END
GO


print '' print '*** Creating sp_retrieve_vendor_by_active'
GO
CREATE PROCEDURE [dbo].[sp_retrieve_vendor_by_active]
AS
	BEGIN
		SELECT 	[VendorID], [Name], [Rep],
				[Address], [Website], [Phone], [Active]
		FROM 	[Vendor]
		WHERE 	[Active] = 1
	END
GO


print '' print '*** Creating sp_retrieve_vendor_by_name'
GO
CREATE PROCEDURE [dbo].[sp_retrieve_vendor_by_name]
	(
		@Name		[nvarchar](100)
	)
AS
	BEGIN
		SELECT 	[VendorID], [Name], [Rep],
				[Address], [Website], [Phone], [Active]
		FROM 	[Vendor]
		WHERE 	[Name] = @Name
	END
GO


print '' print '*** Creating sp_retrieve_vendor_by_rep'
GO
CREATE PROCEDURE [dbo].[sp_retrieve_vendor_by_rep]
	(
		@Rep		[nvarchar](100)
	)
AS
	BEGIN
		SELECT 	[VendorID], [Name], [Rep],
				[Address], [Website], [Phone], [Active]
		FROM 	[Vendor]
		WHERE 	[Rep] = @Rep
	END
GO



print '' print '*** Creating sp_edit_vendor'
GO
CREATE PROCEDURE [dbo].[sp_edit_vendor]
	(
		@VendorID			[int],
		@OldName			[nvarchar](100),
		@NewName			[nvarchar](100),
		@OldRep				[nvarchar](100),
		@NewRep				[nvarchar](100),
		@OldAddress			[nvarchar](250),
		@NewAddress			[nvarchar](250),
		@OldWebsite			[nvarchar](250),
		@NewWebsite			[nvarchar](250),
		@OldPhone			[nvarchar](15),
		@NewPhone			[nvarchar](15),
		@OldActive			[bit],
		@NewActive			[bit]
	)
AS
	BEGIN
		UPDATE [dbo].[Vendor]
			SET 	[Name] = @NewName,
					[Rep] = @NewRep,
					[Address] = @NewAddress,
					[Website] = @NewWebsite,
					[Phone] = @NewPhone,
					[Active] = @NewActive
			WHERE 	[VendorID] = @VendorID
			AND		[Name] = @OldName
			AND 	[Rep] = @OldRep
			AND		[Address] = @OldAddress
			AND		[Website] = @OldWebsite
			AND		[Phone] = @OldPhone
			AND		[Active] = @OldActive
		RETURN @@ROWCOUNT
	END
GO

print '' print '*** Creating sp_edit_vendor_active'
GO
CREATE PROCEDURE [dbo].[sp_edit_vendor_active]
	(
		@VendorID			[int]
	)
AS
	BEGIN
		UPDATE [dbo].[Vendor]
			SET 	[Active] = 1
			WHERE 	[VendorID] = @VendorID
			AND		[Active] = 0
		RETURN @@ROWCOUNT
	END
GO


print '' print '*** Creating sp_deactivate_vendor_by_id'
GO
CREATE PROCEDURE [dbo].[sp_deactivate_vendor_by_id]
	(
		@VendorID			[int]
	)
AS
	BEGIN
		UPDATE [dbo].[Vendor]
			SET 	[Active] = 0
			WHERE 	[VendorID] = @VendorID
			AND		[Active] = 1
		RETURN @@ROWCOUNT
	END
GO


print '' print '*** Creating sp_delete_vendor_by_id'
GO
CREATE PROCEDURE [dbo].[sp_delete_vendor_by_id]
	(
		@VendorID				[int]
	)
AS
	BEGIN
		DELETE FROM [Vendor]
			WHERE [VendorID] = @VendorID
		RETURN @@ROWCOUNT
	END
GO
/* End Jacob C */

/* Reuben C */
print '' print '*** Creating sp_create_payment_type'
GO
CREATE PROCEDURE [dbo].[sp_create_payment_type]
	(
	@PaymentTypeID	[nvarchar](100),
	@Description	[nvarchar](1000)
	)
AS
	BEGIN
		INSERT INTO [dbo].[PaymentType]
			([PaymentTypeID],[Description])
		VALUES
			(@PaymentTypeID, @Description)
		RETURN @@ROWCOUNT
	END
GO

print '' print '*** Creating sp_deactivate_specialorderitem_by_id'
GO
CREATE PROCEDURE [dbo].[sp_deactivate_specialorderitem_by_id]
	(
	@SpecialOrderItemID	[int]
	)
AS
	BEGIN
		UPDATE	[SpecialOrderItem]
			SET		[Active] = 0
			WHERE	[SpecialOrderItemID] = @SpecialOrderItemID
		RETURN @@ROWCOUNT
	END
GO

GO

GO
SET QUOTED_IDENTIFIER ON
GO

print '' print '*** Creating sp_create_source'
GO
CREATE PROCEDURE [dbo].[sp_create_source]
	(
	@SupplyItemID		[int],
	@SpecialOrderItemID	[int],
	@VendorID			[int],
	@MinimumOrderQTY	[int],
	@PriceEach			[money],
	@LeadTime			[int],
	@Active				[bit]
	)
AS
	BEGIN
		INSERT INTO	[dbo].[Source]
			([SupplyItemID],[SpecialOrderItemID],[VendorID],[MinimumOrderQTY],[PriceEach],
			[LeadTime],[Active])
		VALUES
			(@SupplyItemID,@SpecialOrderItemID,@VendorID,@MinimumOrderQTY,@PriceEach,
			@LeadTime,@Active)
		SELECT SCOPE_IDENTITY()
	END
GO

print '' print '*** Creating sp_retrieve_source_list'
GO
CREATE PROCEDURE [dbo].[sp_retrieve_source_list]
AS
	BEGIN
		SELECT 	[SourceID],[SupplyItemID],[SpecialOrderItemID],[VendorID],
				[MinimumOrderQTY],[PriceEach],[LeadTime],[Active]
		FROM	[Source]
	END
GO

print '' print '*** Creating sp_retrieve_source_by_sourceid'
GO
CREATE PROCEDURE [dbo].[sp_retrieve_source_list_by_sourceid]
	(
	@SourceID	[int]
	)
AS
	BEGIN
		SELECT 	[SourceID],[SupplyItemID],[SpecialOrderItemID],[VendorID],
				[MinimumOrderQTY],[PriceEach],[LeadTime],[Active]
		FROM	[Source]
		WHERE	[SourceID] = @SourceID
	END
GO

print '' print '*** Creating sp_retrieve_source_by_supplyitemid'
GO
CREATE PROCEDURE [dbo].[sp_retrieve_source_list_by_supplyitemid]
	(
	@SupplyItemID	[int]
	)
AS
	BEGIN
		SELECT 	[SourceID],[SupplyItemID],[SpecialOrderItemID],[VendorID],
				[MinimumOrderQTY],[PriceEach],[LeadTime],[Active]
		FROM	[Source]
		WHERE	[SupplyItemID] = @SupplyItemID
	END
GO

print '' print '*** Creating sp_retrieve_source_by_specialorderitemid'
GO
CREATE PROCEDURE [dbo].[sp_retrieve_source_list_by_specialorderitemid]
	(
	@SpecialOrderItemID	[int]
	)
AS
	BEGIN
		SELECT 	[SourceID],[SupplyItemID],[SpecialOrderItemID],[VendorID],
				[MinimumOrderQTY],[PriceEach],[LeadTime],[Active]
		FROM	[Source]
		WHERE	[SpecialOrderItemID] = @SpecialOrderItemID
	END
GO

print '' print '*** Creating sp_retrieve_source_by_active'
GO
CREATE PROCEDURE [dbo].[sp_retrieve_source_by_active]
	(
	@Active	[bit]
	)
AS
	BEGIN
		SELECT 	[SourceID],[SupplyItemID],[SpecialOrderItemID],[VendorID],
				[MinimumOrderQTY],[PriceEach],[LeadTime],[Active]
		FROM	[Source]
		WHERE	[Active] = @Active
	END
GO

print '' print '*** Creating sp_update_source'
GO
CREATE PROCEDURE [dbo].[sp_update_source]
	(
	@SourceID				[int],
	@NewSupplyItemID		[int],
	@NewSpecialOrderItemID	[int],
	@NewVendorID			[int],
	@NewMinimumOrderQTY		[int],
	@NewPriceEach			[money],
	@NewLeadTime			[int],
	@NewActive				[bit],
	@OldSupplyItemID		[int],
	@OldSpecialOrderItemID	[int],
	@OldVendorID			[int],
	@OldMinimumOrderQTY		[int],
	@OldPriceEach			[money],
	@OldLeadTime			[int],
	@OldActive				[bit]
	)
AS
	BEGIN
		UPDATE [Source]
			SET 	[SupplyItemID] = @NewSupplyItemID,
					[SpecialOrderItemID] = @NewSpecialOrderItemID,
					[VendorID] = @NewVendorID,
					[MinimumOrderQTY] = @NewMinimumOrderQTY,
					[PriceEach] = @NewPriceEach,
					[LeadTime] = @NewLeadTime,
					[Active] = @NewActive
			WHERE	[SourceID] = @SourceID
			AND		[SupplyItemID] = @OldSupplyItemID
			AND		[SpecialOrderItemID] = @OldSpecialOrderItemID
			AND		[VendorID] = @OldVendorID
			AND		[MinimumOrderQTY] = @OldMinimumOrderQTY
			AND		[PriceEach] = @OldPriceEach
			AND		[LeadTime] = @OldLeadTime
			AND		[Active] = @OldActive
		RETURN @@ROWCOUNT
	END
GO

print '' print '*** Creating sp_update_source_active'
GO
CREATE PROCEDURE [dbo].[sp_update_source_active]
	(
	@SourceID	[int],
	@Active 	[bit]
	)
AS
	BEGIN
		UPDATE [Source]
			SET	[Active] = @Active
			WHERE	[SourceID] = @SourceID
		RETURN @@ROWCOUNT
	END
GO

print '' print '*** Creating sp_deactivate_source_by_sourceid'
GO
CREATE PROCEDURE [dbo].[sp_deactivate_source_by_sourceid]
	(
	@SourceID	[int]	
	)
AS
	BEGIN
		UPDATE [Source]
			SET		[Active] = 0
			WHERE	[SourceID] = @SourceID
		RETURN @@ROWCOUNT
	END
GO

print '' print '*** Creating sp_delete_source_by_sourceid'
GO
CREATE PROCEDURE [dbo].[sp_delete_source_by_sourceid]
	(
	@SourceID	[int]
	)
AS
	BEGIN
		DELETE FROM [Source]
			WHERE	[SourceID] = @SourceID
		RETURN @@ROWCOUNT
	END
GO

print '' print '*** Creating sp_retrieve_specialorderitem_by_id'
GO
CREATE PROCEDURE [dbo].[sp_retrieve_specialorderitem_by_id]
	(
	@SpecialOrderItemID [int]
	)
AS
	BEGIN
		SELECT 	[SpecialOrderItemID], [Name]
		FROM	[SpecialOrderItem]
		WHERE	[SpecialOrderItemID] = @SpecialOrderItemID
	END
GO

print '' print '*** Creating sp_retrieve_specialorderitem_by_name'
GO
CREATE PROCEDURE [dbo].[sp_retrieve_specialorderitem_by_name]
	(
	@Name [nvarchar](100)
	)
AS
	BEGIN
		SELECT 	[SpecialOrderItemID],[Name]
		FROM	[SpecialOrderItem]
		WHERE	[Name] = @Name
	END
GO

print '' print '*** Creating sp_delete_specialorderitem_by_id'
GO
CREATE PROCEDURE [dbo].[sp_delete_specialorderitem_by_id]
	(
	@SpecialOrderItemID	[int]
	)
AS
	BEGIN
		DELETE FROM	[SpecialOrderItem]
			WHERE [SpecialOrderItemID] = @SpecialOrderItemID
		RETURN @@ROWCOUNT
	END
GO

print '' print '*** Creating sp_create_specialorderline'
GO
CREATE PROCEDURE [dbo].[sp_create_specialorderline]
	(
	
	@SpecialOrderItemID	[int],
	@Quantity			[int]
	)
AS
	BEGIN
		INSERT INTO [dbo].[SpecialOrderLine]
			([SpecialOrderItemID],[Quantity])
		VALUES
			(@SpecialOrderItemID,@Quantity)
		RETURN @@ROWCOUNT
	END
GO	

print '' print '*** Creating sp_retrieve_specialorderline_by_id'
GO
CREATE PROCEDURE [dbo].[sp_retrieve_specialorderline_by_id]
	(
	@SpecialOrderLineID	[int]
	)
AS
	BEGIN
		SELECT	[SpecialOrderLineID],[SpecialOrderItemID],[Quantity]
		FROM	[SpecialOrderLine]
		WHERE	[SpecialOrderLineID] = @SpecialOrderLineID
	END
GO

print '' print '*** Creating sp_edit_specialorderline'
GO
CREATE PROCEDURE [dbo].[sp_edit_specialorderline]
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
CREATE PROCEDURE [dbo].[sp_delete_specialorderline]
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


/* Sam D */
print '' print '*** Creating sp_retrieve_supplyitem_by_id'
GO
CREATE PROCEDURE [dbo].[sp_retrieve_supplyitem_by_id]
	(
	@SupplyItemID		[int]
	)
AS
	BEGIN
		SELECT 	[SupplyItemID], [Name], [Description], [Location], [QuantityInStock], [ReorderLevel], [ReorderQuantity]
		FROM 	[SupplyItem]
		WHERE 	[SupplyItemID] = @SupplyItemID
	END
GO

print '' print '*** Creating sp_retrieve_supplyitem_by_active'
GO
CREATE PROCEDURE [dbo].[sp_retrieve_supplyitem_by_active]
	(
	@Active		[bit]
	)
AS
	BEGIN
		SELECT 	[SupplyItemID], [Name], [Description], [Location], [QuantityInStock], [ReorderLevel], [ReorderQuantity]
		FROM 	[SupplyItem]
		WHERE 	[Active] = @Active
        ORDER BY [SupplyItemID]
	END
GO

print '' print '*** Creating sp_retrieve_supplyitem_by_reorderlevel'
GO
CREATE PROCEDURE [dbo].[sp_retrieve_supplyitem_by_reorderlevel]
	(
	@ReorderLevel		[bit]
	)
AS
	BEGIN
		SELECT 	[SupplyItemID], [Name], [Description], [Location], [QuantityInStock], [ReorderLevel], [ReorderQuantity]
		FROM 	[SupplyItem]
		WHERE 	[QuantityInStock] < [ReorderLevel]
        ORDER BY [SupplyItemID]
	END
GO

print '' print '*** Creating sp_retrieve_supplyitem_by_vendorid'

GO
CREATE PROCEDURE [dbo].[sp_retrieve_supplyitem_by_vendorid]
	(
	@VendorID		[int]
	)
AS
	BEGIN
		SELECT 	[SupplyItem].[SupplyItemID], [SupplyItem].[Name], [SupplyItem].[Description], [SupplyItem].[Location], [SupplyItem].[QuantityInStock], [SupplyItem].[ReorderLevel], [SupplyItem].[ReorderQuantity]
		FROM 	[SupplyItem], [Source]
		WHERE 	[SupplyItem].[SupplyItemID] = [Source].[SupplyItemID]
        AND     [Source].[VendorID] = @VendorID
		AND		[Source].[Active] = 1
	END
GO

print '' print '*** Creating sp_deactivate_supplyitem_by_id'
GO
CREATE PROCEDURE [dbo].[sp_deactivate_supplyitem_by_id]
	(
	@SupplyItemID				[int]
	)
AS
	BEGIN
		UPDATE [SupplyItem]
			SET [Active] = 0
			WHERE [SupplyItemID] = @SupplyItemID 
		RETURN @@ROWCOUNT
	END
GO

print '' print '*** Creating sp_delete_supplyitem_by_id'
GO
CREATE PROCEDURE [dbo].[sp_delete_supplyitem_by_id]
	(
	@SupplyItemID				[int]
	)
AS
	BEGIN
		DELETE FROM [SupplyItem]
			WHERE [SupplyItemID] = @SupplyItemID 
		RETURN @@ROWCOUNT
	END
GO

print '' print '*** Creating sp_create_resupplyorder'
GO
CREATE PROCEDURE [dbo].[sp_create_resupplyorder]
	(
	@EmployeeID			    [int],
	@Date	      			[date],
  @SupplyStatusID         [nvarchar](100),
  @VendorID					[int]
	)
AS
	BEGIN
		INSERT INTO [dbo].[ResupplyOrder]
		([EmployeeID], [Date], [SupplyStatusID], [VendorID])
		VALUES	(
			@EmployeeID, @Date, @SupplyStatusID, @VendorID
		)
		SELECT SCOPE_IDENTITY()
	END
GO

print '' print '*** Creating sp_retrieve_resupplyorder_by_id'

GO
CREATE PROCEDURE [dbo].[sp_retrieve_resupplyorder_by_id]
	(
	@ResupplyOrderID		[int]
	)
AS
	BEGIN
		SELECT 	[ResupplyOrderID], [EmployeeID], [Date], [SupplyStatusID], [VendorID]
		FROM 	[ResupplyOrder]
		WHERE 	[ResupplyOrderID] = @ResupplyOrderID
	END
GO

print '' print '*** Creating sp_edit_resupplyorder_by_id'

GO
CREATE PROCEDURE [dbo].[sp_edit_resupplyorder_by_id]
	(
	@ResupplyOrderID		[int],
    @OldEmployeeID          [int],
    @OldDate   				[date],
    @OldSupplyStatusID      [nvarchar](100),
    @NewEmployeeID          [int],
    @NewDate   				[date],
    @NewSupplyStatusID      [nvarchar](100)
	)
AS
	BEGIN
		UPDATE [ResupplyOrder]
			SET [EmployeeID] = @NewEmployeeID 
			, [Date] = @NewDate
            , [SupplyStatusID] = @NewSupplyStatusID
			WHERE [EmployeeID] = @OldEmployeeID 
            AND [Date] = @OldDate
            AND [SupplyStatusID] = @OldSupplyStatusID
            AND [ResupplyOrderID] = @ResupplyOrderID
		RETURN @@ROWCOUNT
	END
GO
/* End Sam D */


print '' print '*** Creating sp_create_supplyorder_nojob'
GO
CREATE PROCEDURE [dbo].[sp_create_supplyorder_nojob]
	(
	 @EmployeeID           		[int],
     @SupplyStatusID       		[nvarchar](100),
     @Date      				[date]
	)
AS
	BEGIN
		INSERT INTO [dbo].[SupplyOrder]
			([EmployeeID], [SupplyStatusID], [Date])
	VALUES
		(@EmployeeID, @SupplyStatusID, @Date)
		SELECT SCOPE_IDENTITY()
	END
GO

print '' print '*** Creating sp_retrieve_supplyorder_list_by_active'
GO
CREATE PROCEDURE [dbo].[sp_retrieve_supplyorder_list_by_active]
AS
	BEGIN
		SELECT 	[SupplyOrderID], [EmployeeID], [JobID], [SupplyStatusID], [Date]
		FROM 	[SupplyOrder]
		WHERE	[SupplyStatusID] != 'Cancelled'
	END
GO	


print '' print '*** Creating sp_edit_supplyorder_by_id_no_job'
GO
CREATE PROCEDURE [dbo].[sp_edit_supplyorder_by_id_no_job]
	(
	@SupplyOrderID       		[int],
    @OldEmployeeID           	[int],
    @OldSupplyStatusID       	[nvarchar](100),
    @OldDate     	[date],
    @NewEmployeeID           	[int],
    @NewSupplyStatusID       	[nvarchar](100),   
    @NewDate      	[date] 
	)
AS
	BEGIN
		UPDATE SupplyOrder 
        SET [EmployeeID] = @NewEmployeeID, [Date] = @NewDate,
		[SupplyStatusID] = @NewSupplyStatusID
		where SupplyOrderID = @SupplyOrderID
        AND [EmployeeID] = @OldEmployeeID
        AND [Date] = @OldDate
        AND [SupplyStatusID] = @OldSupplyStatusID
		RETURN @@ROWCOUNT
	END
GO

print '' print '*** Creating sp_deactivate_supplyorder_by_id'
GO
CREATE PROCEDURE [dbo].[sp_deactivate_supplyorder_by_id]
	(
	@SupplyOrderID				[int],
	@OldSupplyStatusID			[nvarchar](100)
	)
AS
	BEGIN
		UPDATE 	[SupplyOrder]
		SET		[SupplyStatusID] = 'Cancelled'
		WHERE 	[SupplyOrderID] = @SupplyOrderID
		AND 	[SupplyStatusID] = @OldSupplyStatusID
		RETURN @@ROWCOUNT
	END
GO

print '' print '*** Creating sp_create_supplyorderline'
GO
CREATE PROCEDURE [dbo].[sp_create_supplyorderline]
	(
	
	@SupplyItemID		[int],
	@Quantity			[int],
	@SupplyOrderID		[int]
	)
AS
	BEGIN
		INSERT INTO [dbo].[SupplyOrderLine]
			([SupplyItemID],[Quantity], [SupplyOrderID])
		VALUES
			(@SupplyItemID, @Quantity, @SupplyOrderID)
		RETURN @@ROWCOUNT
	END
GO	

print '' print '*** Creating sp_retrieve_supplyorderline_by_supplyorderid'
GO
CREATE PROCEDURE [dbo].[sp_retrieve_supplyorderline_by_supplyorderid]
	(
	@SupplyOrderID	[int]
	)
AS
	BEGIN
		SELECT	[SupplyOrderLineID],[SupplyItemID],[Quantity]
		FROM	[SupplyOrderLine]
		WHERE	[SupplyOrderID] = @SupplyOrderID
	END
GO

print '' print '*** Creating sp_edit_supplyorderline_by_supplyorderlineid'
GO
CREATE PROCEDURE [dbo].[sp_edit_supplyorderline_by_supplyorderlineid]
	(
	@SupplyOrderLineID	[int],
	@OldQuantity		[int],
	@NewQuantity		[int]
	)
AS
	BEGIN
		UPDATE	[SupplyOrderLine]
			SET		[Quantity] = @NewQuantity
			WHERE	[SupplyOrderLineID] = @SupplyOrderLineID
			AND		[Quantity] = @OldQuantity
		RETURN @@ROWCOUNT
	END
GO

print '' print '*** Creating sp_delete_supplyorderline'
GO
CREATE PROCEDURE [dbo].[sp_delete_supplyorderline]
	(
	@SupplyOrderLineID	[int]
	)
AS
	BEGIN
		DELETE FROM	[SupplyOrderLine]
			WHERE	[SupplyOrderLineID] = @SupplyOrderLineID
		RETURN @@ROWCOUNT
	END
GO

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
		SET		[SupplyStatusID] = 'Received'
		WHERE	[SupplyOrderID] = @SupplyOrderID
		
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


