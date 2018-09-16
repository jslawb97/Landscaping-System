print '' print '*** in file EditTaskSupplyQuantity.sql ***'
/* Tested by James McPherson 4/6/2018 */
/* Created by James McPherson 4/6/2018 */
USE [crlandscaping]
GO
print '' print '*** Creating sp_edit_tasksupply_quantity'
GO
CREATE PROCEDURE [dbo].[sp_edit_tasksupply_quantity]
	(
	@TaskSupplyID		[int],
	@OldQuantity		[int],
	@NewQuantity		[int]
	)
AS
	BEGIN
		DECLARE @CurrentQuantity		[int]
		DECLARE @SupplyAvailable		[int]
		DECLARE @SupplyDifference		[int]
		DECLARE @TaskSupplyUpdateErr	[int]
		DECLARE @SupplyItemUpdateErr	[int]
		DECLARE @InsufficientSupplyErr	[int]
		DECLARE @ConcurrencyErr			[int]
		DECLARE @err_message 			[nvarchar](255)
		
		/* Check for concurrency problems */
		SELECT @CurrentQuantity = [Quantity]
			FROM	[TaskSupply]
			WHERE	[TaskSupplyID] = @TaskSupplyID
			
		IF @CurrentQuantity <> @OldQuantity
			SET @ConcurrencyErr = 1
			
		/* Find the available supply */
		SELECT	@SupplyAvailable = [QuantityInStock]
			FROM	[SupplyItem]
			INNER JOIN [TaskSupply] ON [SupplyItem].[SupplyItemID] = [TaskSupply].[SupplyItemID]
			WHERE	[TaskSupplyID] = @TaskSupplyID
		
		/* Error if not enough supply */
		SET @SupplyDifference = @NewQuantity - @OldQuantity
		IF @SupplyAvailable - @SupplyDifference < 0
			SET @InsufficientSupplyErr = 1
			
		BEGIN TRANSACTION
		/* Set the new TaskSupply quantity */
		UPDATE [TaskSupply]
			SET	[Quantity] = @NewQuantity
			WHERE	[TaskSupplyID] = @TaskSupplyID
			AND		[Quantity] = @OldQuantity
		
		/* Check for update error */
		SET @TaskSupplyUpdateErr = @@error
		
		/* Set the new SupplyItem QuantityInStock */
		UPDATE [SupplyItem]
			SET [QuantityInStock] = [QuantityInStock] - @SupplyDifference
				FROM [SupplyItem]
				INNER JOIN [TaskSupply] ON [SupplyItem].[SupplyItemID] = [TaskSupply].[SupplyItemID]
				WHERE	[TaskSupplyID] = @TaskSupplyID
				
		/* Check for update error */
		SET @SupplyItemUpdateErr = @@error
		
		/* Rollback on error, else commit */
		IF @TaskSupplyUpdateErr <> 0
			BEGIN
				ROLLBACK
				SET @err_message = 'Could not properly edit the TaskSupply quantity'
				RAISERROR(@err_message, 18, 1)
			END
		ELSE IF @SupplyItemUpdateErr <> 0
			BEGIN
				ROLLBACK
				SET @err_message = 'Could not properly edit the SupplyItem quantity in stock'
				RAISERROR(@err_message, 18, 1)
			END
		ELSE IF @ConcurrencyErr <> 0
			BEGIN
				ROLLBACK
				SET @err_message = '@OldQuantity does not equal current quantity'
				RAISERROR (@err_message, 18, 1)
			END
		ELSE IF @InsufficientSupplyErr <> 0
			BEGIN
				ROLLBACK
				SET @err_message = 'Cannot allocate more supply than is available'
				RAISERROR (@err_message, 18, 1)
			END
		ELSE
			BEGIN
				COMMIT
				RETURN @@ROWCOUNT
			END
	END
GO
