/* Jacob Conley */
print '' print '*** in file SupplyOrderAndTaskTypeSupplyNeedSPs.sql ***'
print '' print '*** Using database crlandscaping'
GO
USE [crlandscaping]
GO

print '' print '*** Inserting into SupplyStatus Test Records'
GO
INSERT INTO [dbo].[SupplyStatus]
		([SupplyStatusID])
	VALUES
		('Cancelled')
GO

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

print '' print '*** Creating sp_create_tasktypesupply'
GO
CREATE PROCEDURE [dbo].[sp_create_tasktypesupply]
	(
	@TaskTypeID					[int],
	@SupplyItemID			[int],
	@Quantity				[int]
	)
AS
	BEGIN
		INSERT INTO [dbo].[TaskTypeSupplyNeed]
			([TaskTypeID],[SupplyItemID], [Quantity])
		VALUES
			(@TaskTypeID, @SupplyItemID, @Quantity)
		RETURN @@ROWCOUNT
	END
GO	

print '' print '*** Creating sp_retrieve_tasktypesupplies'
GO
CREATE PROCEDURE [dbo].[sp_retrieve_tasktypesupplies]
AS
	BEGIN
		SELECT	[TaskTypeID],[SupplyItemID],[Quantity]
		FROM	[TaskTypeSupplyNeed]
	END
GO

print '' print '*** Creating sp_retrieve_tasktypesupplies_by_task_and_supplyitemid'
GO
CREATE PROCEDURE [dbo].[sp_retrieve_tasktypesupplies_by_task_and_supplyitemid]
	(
	@TaskTypeID					[int],
	@SupplyItemID			[int]
	)
AS
	BEGIN
		SELECT	[TaskTypeID],[SupplyItemID],[Quantity]
		FROM	[TaskTypeSupplyNeed]
		WHERE	[TaskTypeID] = @TaskTypeID
		AND 	[SupplyItemID] = @SupplyItemID
	END
GO

print '' print '*** Creating sp_edit_tasktypesupply_by_task_and_supplyitemid'
GO
CREATE PROCEDURE [dbo].[sp_edit_tasktypesupply_by_task_and_supplyitemid]
	(
	@TaskTypeID				[int],
	@SupplyItemID		[int],
	@OldQuantity		[int],
	@NewQuantity		[int]
	)
AS
	BEGIN
		UPDATE	[TaskTypeSupplyNeed]
			SET		[Quantity] = @NewQuantity
			WHERE	[TaskTypeID] = @TaskTypeID
			AND 	[SupplyItemID] = @SupplyItemID
			AND		[Quantity] = @OldQuantity
		RETURN @@ROWCOUNT
	END
GO

print '' print '*** Creating sp_delete_tasktypesupply'
GO
CREATE PROCEDURE [dbo].[sp_delete_tasktypesupply]
	(
	@TaskTypeID			[int],
	@SupplyItemID		[int]
	)
AS
	BEGIN
		DELETE FROM	[TaskTypeSupplyNeed]
			WHERE	[TaskTypeID] = @TaskTypeID
			AND 	[SupplyItemID] = @SupplyItemID
		RETURN @@ROWCOUNT
	END
GO