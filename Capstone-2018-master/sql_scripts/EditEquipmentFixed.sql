print '' print '*** in file EditEquipmentFixed.sql ***'
/* Created by Noah Davison 5/3/2018 */
USE [crlandscaping]
GO
print '' print '*** Creating sp_edit_equipment_by_id_fixed'
GO
CREATE PROCEDURE [dbo].[sp_edit_equipment_by_id_fixed]
	(
	@NewEquipmentTypeID		[nvarchar](100),
	@NewName				[nvarchar](100),
	@NewMakeModelID			[int],
	@NewDatePurchased		[date],
	@NewDateLastRepaired	[date],
	@NewPriceAtPurchase		[Money],
	@NewCurrentValue		[Money],
	@NewWarrantyUntil		[date],
	@NewEquipmentStatusID	[nvarchar](100),
	@NewEquipmentDetails	[nvarchar](1000),
	
	@OldEquipmentTypeID		[nvarchar](100),
	@OldName				[nvarchar](100),
	@OldMakeModelID			[int],
	@OldDatePurchased		[date],
	@OldDateLastRepaired	[date],
	@OldPriceAtPurchase		[Money],
	@OldCurrentValue		[Money],
	@OldWarrantyUntil		[date],
	@OldEquipmentStatusID	[nvarchar](100),
	@OldEquipmentDetails	[nvarchar](1000),
	@EquipmentID			[int]
	)
AS
	BEGIN
		UPDATE 		[Equipment]
			SET 	[EquipmentTypeID] = @NewEquipmentTypeID,
					[Name] = @NewName,
					[MakeModelID] = @NewMakeModelID,
					[DatePurchased] = @NewDatePurchased,
					[DateLastRepaired] = @NewDateLastRepaired,
					[PriceAtPurchase] = @NewPriceAtPurchase,
					[CurrentValue] = @NewCurrentValue,
					[WarrantyUntil] = @NewWarrantyUntil,
					[EquipmentStatusID] = @NewEquipmentStatusID,
					[EquipmentDetails] = @NewEquipmentDetails
					
			WHERE 	[EquipmentID] = @EquipmentID
			AND		[EquipmentTypeID] = @OldEquipmentTypeID
			AND		[Name] = @OldName
			AND		[MakeModelID] = @OldMakeModelID
			AND		[DatePurchased] = @OldDatePurchased
			AND		[DateLastRepaired] = @OldDateLastRepaired
			AND		[PriceAtPurchase] = @OldPriceAtPurchase
			AND		[CurrentValue] = @OldCurrentValue
			AND		[WarrantyUntil] = @OldWarrantyUntil
			AND		[EquipmentStatusID] = @OldEquipmentStatusID
			AND		[EquipmentDetails] = @OldEquipmentDetails
		RETURN @@ROWCOUNT		
	END
GO