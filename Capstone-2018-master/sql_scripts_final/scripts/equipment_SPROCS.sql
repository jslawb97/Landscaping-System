print '' print '*** In script equipment_SPROCS.sql'
print '' print '*** Using database crlandscaping'
GO
USE [crlandscaping]
GO

/* James M */
/* Tested by James McPherson 3/23/2018 */
print '' print '*** Creating sp_create_inspectionrecord'
GO
CREATE PROCEDURE [dbo].[sp_create_inspectionrecord]
	(
	@EquipmentID			[int],
	@EmployeeID				[int],
	@Description			[nvarchar](1000),
	@Date					[date]
	)
AS
	BEGIN
		INSERT INTO [InspectionRecord]
			([EquipmentID], [EmployeeID], [Description], [Date])
		VALUES
			(@EquipmentID, @EmployeeID, @Description, @Date)
		SELECT SCOPE_IDENTITY()
	END
GO

/* Tested by James McPherson 3/23/2018 */
print '' print '*** Creating sp_retrieve_inspectionrecord_list_by_equipmentid'
GO
CREATE PROCEDURE [dbo].[sp_retrieve_inspectionrecord_list_by_equipmentid]
	(
	@EquipmentID			[int]
	)
AS
	BEGIN
		SELECT	[InspectionRecordID], [EquipmentID], [EmployeeID]
		, [Description], [Date]
		FROM	[InspectionRecord]
		WHERE	[EquipmentID] = @EquipmentID
	END
GO

/* Tested by James McPherson 3/23/2018 */
print '' print '*** Creating sp_retrieve_inspectionrecord_list'
GO
CREATE PROCEDURE [dbo].[sp_retrieve_inspectionrecord_list]
AS
	BEGIN
		SELECT	[InspectionRecordID], [EquipmentID], [EmployeeID]
		, [Description], [Date]
		FROM	[InspectionRecord]
	END
GO

/* Tested by James McPherson 3/23/2018 */
print '' print '*** Creating sp_retrieve_inspectionrecord_by_id'
GO
CREATE PROCEDURE [dbo].[sp_retrieve_inspectionrecord_by_id]
	(
	@InspectionRecordID			[int]
	)
AS
	BEGIN
		SELECT	[InspectionRecordID], [EquipmentID], [EmployeeID]
		, [Description], [Date]
		FROM	[InspectionRecord]
		WHERE	[InspectionRecordID] = @InspectionRecordID
	END
GO

/* Tested by James McPherson 3/23/2018 */
print '' print '*** Creating sp_edit_inspectionrecord_by_id'
GO
CREATE PROCEDURE [dbo].[sp_edit_inspectionrecord_by_id]
	(
	@InspectionRecordID			[int],
	@NewEquipmentID				[int],
	@NewEmployeeID				[int],
	@NewDescription				[nvarchar](1000),
	@NewDate					[date],
	@OldEquipmentID				[int],
	@OldEmployeeID				[int],
	@OldDescription				[nvarchar](1000),
	@OldDate					[date]
	)
AS
	BEGIN
		UPDATE	[InspectionRecord]
			SET [EquipmentID] = @NewEquipmentID
			, [EmployeeID] = @NewEmployeeID
			, [Description] = @NewDescription
			, [Date] = @NewDate
		WHERE	[InspectionRecordID] = @InspectionRecordID
		AND		[EquipmentID] = @OldEquipmentID
		AND		[EmployeeID] = @OldEmployeeID
		AND		[Description] = @OldDescription
		AND		[Date] = @OldDate
	RETURN @@ROWCOUNT
	END
GO

/* Tested by James McPherson 3/23/2018 */
print '' print '*** Creating sp_delete_inspectionrecord_by_id'
GO
CREATE PROCEDURE [dbo].[sp_delete_inspectionrecord_by_id]
	(
	@InspectionRecordID			[int]
	)
AS
	BEGIN
		DELETE FROM	[InspectionRecord]
		WHERE	[InspectionRecordID] = @InspectionRecordID
	RETURN @@ROWCOUNT
	END
GO

/* Tested by James McPherson 2/16/2018 */
print '' print '*** Creating sp_retrieve_makemodel_list_by_active'
GO
CREATE PROCEDURE [dbo].[sp_retrieve_makemodel_list_by_active]
	(
	@Active					[bit]
	)
AS
	BEGIN
		SELECT 	[MakeModelID], [Make], [Model], [MaintenanceChecklistID]
		FROM	[MakeModel]
		WHERE	[Active] = @Active
	END
GO

/* Tested by James McPherson 2/13/2018 */
print '' print '*** Creating sp_deactivate_equipmenttype_by_id'
GO
CREATE PROCEDURE [dbo].[sp_deactivate_equipmenttype_by_id]
	(
	@EquipmentTypeID		[nvarchar](100)
	)
AS
	BEGIN
		UPDATE [EquipmentType]
			SET [Active] = 0
			WHERE [EquipmentTypeID] = @EquipmentTypeID
		RETURN @@ROWCOUNT
	END
GO

/* Tested by James McPherson 2/13/2018 */
print '' print '*** Creating sp_deactivate_makemodel_by_id'
GO
CREATE PROCEDURE [dbo].[sp_deactivate_makemodel_by_id]
	(
	@MakeModelID		[int]
	)
AS
	BEGIN
		UPDATE [MakeModel]
			SET [Active] = 0
			WHERE [MakeModelID] = @MakeModelID
		RETURN @@ROWCOUNT
	END
GO

/* Tested by James McPherson 2/4/2018 */
print '' print '*** Creating sp_retrieve_maintenancechecklist_by_id'
GO
CREATE PROCEDURE [dbo].[sp_retrieve_maintenancechecklist_by_id]
	(
	@MaintenanceChecklistID		[int]
	)
AS
	BEGIN
		SELECT	[MaintenanceChecklistID], [Name], [Description], [Active]
		FROM	[MaintenanceChecklist]
		WHERE	[MaintenanceChecklistID] = @MaintenanceChecklistID
	END
GO

/* Tested by James McPherson 2/4/2018 */
print '' print '*** Creating sp_retrieve_maintenancechecklist_list'
GO
CREATE PROCEDURE [dbo].[sp_retrieve_maintenancechecklist_list]
AS
	BEGIN
		SELECT 	[MaintenanceChecklistID], [Name], [Description], [Active]
		FROM	[MaintenanceChecklist]	
	END
GO

/* Tested by James McPherson 2/4/2018 */
print '' print '*** Creating sp_retrieve_makemodel_list'
GO
CREATE PROCEDURE [dbo].[sp_retrieve_makemodel_list]
AS
	BEGIN
		SELECT 	[MakeModelID], [Make], [Model]
		FROM	[MakeModel]
	END
GO

/* Tested by James McPherson 2/4/2018 */
print '' print '*** Creating sp_retrieve_makemodel_by_id'
GO
CREATE PROCEDURE [dbo].[sp_retrieve_makemodel_by_id]
	(
	@MakeModelID		[int]
	)
AS
	BEGIN
		SELECT	[MakeModelID], [Make], [Model], [MaintenanceChecklistID]
		FROM	[MakeModel]
		WHERE	[MakeModelID] = @MakeModelID
	END
GO

/* Tested by James McPherson 2/4/2018 */
print '' print '*** Creating sp_retrieve_makemodel_list_by_make'
GO
CREATE PROCEDURE [dbo].[sp_retrieve_makemodel_list_by_make]
	(
	@Make		[nvarchar](100)
	)
AS
	BEGIN
		SELECT	[MakeModelID], [Make], [Model], [MaintenanceChecklistID]
		FROM	[MakeModel]
		WHERE	[Make] = @Make
	END
GO

/* Tested by James McPherson 2/4/2018 */
print '' print '*** Creating sp_create_makemodel'
GO
CREATE PROCEDURE [dbo].[sp_create_makemodel]
	(
	@Make						[nvarchar](100),
	@Model						[nvarchar](100),
	@MaintenanceChecklistID		[int]
	)
AS
	BEGIN
		INSERT INTO [MakeModel]
			([Make], [Model], [MaintenanceChecklistID])
		VALUES
			(@Make, @Model, @MaintenanceChecklistID)
		SELECT SCOPE_IDENTITY()
	END
GO

/* Tested by James McPherson 2/4/2018 */
print '' print '*** Creating sp_edit_makemodel_by_id'
GO
CREATE PROCEDURE [dbo].[sp_edit_makemodel_by_id]
	(
	@MakeModelID				[int],
	@NewMake					[nvarchar](100),
	@NewModel					[nvarchar](100),
	@NewMaintenanceChecklistID	[int],
	@OldMake					[nvarchar](100),
	@OldModel					[nvarchar](100),
	@OldMaintenanceChecklistID	[int]
	)
AS
	BEGIN
		UPDATE 	[MakeModel]
		SET		[Make] = @NewMake,
				[Model] = @NewModel,
				[MaintenanceChecklistID] = @NewMaintenanceChecklistID
		WHERE	[Make] = @OldMake
		AND		[Model] = @OldModel
		AND		[MaintenanceChecklistID] = @OldMaintenanceChecklistID
		RETURN @@ROWCOUNT
	END
GO

/* Tested by James McPherson 2/4/2018 */
print '' print '*** Creating sp_delete_makemodel_by_id'
GO
CREATE PROCEDURE [dbo].[sp_delete_makemodel_by_id]
	(
	@MakeModelID				[int]
	)
AS
	BEGIN
	DELETE FROM [MakeModel]
	WHERE		[MakeModelID] = @MakeModelID
	RETURN @@ROWCOUNT
	END
GO

/* Tested by James McPherson 2/3/2018 */
print '' print '*** Creating sp_create_equipmentstatus'
GO
CREATE PROCEDURE [dbo].[sp_create_equipmentstatus]
	(
	@EquipmentStatusID		[nvarchar](100)
	)
AS
	BEGIN
		INSERT INTO [dbo].[EquipmentStatus]
			([EquipmentStatusID])
		VALUES
			(@EquipmentStatusID)
	END
GO

print '' print '*** Creating sp_retrieve_maintenancerecord_list'
GO
CREATE PROCEDURE [dbo].[sp_retrieve_maintenancerecord_list]
AS
	BEGIN
		SELECT	[MaintenanceRecordID],[EquipmentID],[EmployeeID],[Description]
				, [Date]
		FROM	[MaintenanceRecord]
	END
GO

print '' print '*** Creating sp_deactivate_equipment_by_id'
GO
CREATE PROCEDURE [dbo].[sp_deactivate_equipment_by_id]
	(
	@EquipmentID	[int]
	)
AS
	BEGIN
		UPDATE 		[Equipment]
			SET 	[Active] = 0					
			WHERE 	[EquipmentID] = @EquipmentID
			AND		[Active] = 1		
		RETURN @@ROWCOUNT		
	END
GO

print '' print '*** Creating sp_reactivate_equipment_by_id'
GO
CREATE PROCEDURE [dbo].[sp_reactivate_equipment_by_id]
	(
	@EquipmentID	[int]
	)
AS
	BEGIN
		UPDATE 		[Equipment]
			SET 	[Active] = 1				
			WHERE 	[EquipmentID] = @EquipmentID
			AND		[Active] = 0	
		RETURN @@ROWCOUNT		
	END
GO

print '' print '*** Creating sp_retrieve_prepchecklist_by_active'
GO
CREATE PROCEDURE [dbo].[sp_retrieve_prepchecklist_by_active]
	(
	@Active 	[bit]
	)
AS
	BEGIN
		SELECT 		[PrepChecklistID], [Name], [Description], [Active]
		FROM 		[PrepChecklist]
		WHERE 		[Active] = @Active
		ORDER BY 	[PrepChecklistID]
	END
GO

print '' print '*** Creating sp_retrieve_prepchecklist_list'
GO
CREATE PROCEDURE [dbo].[sp_retrieve_prepchecklist_list]
AS
	BEGIN
		SELECT 	[PrepChecklistID], [Name], [Description], [Active]
		FROM 	[PrepChecklist]
	END
GO

print '' print '*** Creating sp_delete_prepchecklist_list'
GO
CREATE PROCEDURE [dbo].[sp_delete_prepchecklist_by_id]
	(
	@PrepChecklistID 		[int]
	)
AS
	BEGIN
		DELETE FROM 	[PrepChecklist]
		WHERE 			[PrepChecklistID] = @PrepChecklistID
		RETURN @@ROWCOUNT
	END
GO

print '' print '*** Creating sp_deactivate_prepchecklist_by_id'
GO
CREATE PROCEDURE [dbo].[sp_deactivate_prepchecklist_by_id]
	(
	@PrepChecklistID				[int]
	)
AS
	BEGIN
		UPDATE [PrepChecklist]
			SET [Active] = 0
			WHERE [PrepChecklistID] = @PrepChecklistID 
		RETURN @@ROWCOUNT
	END
GO

print '' print '*** Creating sp_retrieve_prepchecklist_by_id'
GO
CREATE PROCEDURE [dbo].[sp_retrieve_prepchecklist_by_id]
	(
	@PrepChecklistID 		[int]
	)
AS
	BEGIN
		SELECT 		[PrepChecklistID], [Description], [Active]
		FROM 		[PrepChecklist]
		WHERE 		[PrepChecklistID] = @PrepChecklistID
		ORDER BY 	[PrepChecklistID]
	END
GO

print '' print '*** Creating sp_create_equipment'
GO
CREATE PROCEDURE [dbo].[sp_create_equipment]
	(
	@EquipmentTypeID	[nvarchar](100),
	@Name				[nvarchar](100),
	@MakeModelID		[int],
	@DatePurchased		[date],
	@DateLastRepaired	[date],
	@PriceAtPurchase	[money],
	@CurrentValue		[money],
	@EquipmentStatusID	[nvarchar](100),
	@EquipmentDetails	[nvarchar](1000)
	)
AS
	BEGIN
		INSERT INTO [dbo].[Equipment]
			([EquipmentTypeID], [Name], [MakeModelID], [DatePurchased], [DateLastRepaired], [PriceAtPurchase], [CurrentValue], [EquipmentStatusID], [EquipmentDetails])
		VALUES
			(@EquipmentTypeID, @Name, @MakeModelID, @DatePurchased, @DateLastRepaired, @PriceAtPurchase, @CurrentValue, @EquipmentStatusID, @EquipmentDetails  )
		SELECT SCOPE_IDENTITY()
	END
GO

print '' print '*** Creating sp_delete_equipment_by_id'
GO
CREATE PROCEDURE [dbo].[sp_delete_equipment_by_id]
	(
	@EquipmentID 		[int]
	)
AS
	BEGIN
		DELETE FROM [Equipment]
		WHERE 		[EquipmentID] = @EquipmentID
		RETURN @@ROWCOUNT
	END
GO

print '' print '*** Creating sp_delete_preprecord_by_id'
GO
CREATE PROCEDURE [dbo].[sp_delete_preprecord_by_id]
	(
	@PrepRecordID 		[int]
	)
AS
	BEGIN
		DELETE FROM [PrepRecord]
		WHERE 		[PrepRecordID] = @PrepRecordID
		RETURN @@ROWCOUNT
	END
GO

print '' print '*** Creating sp_create_prepchecklist'
GO
CREATE PROCEDURE [dbo].[sp_create_prepchecklist]
	(
	@Name				[nvarchar](100),
	@Description		[nvarchar](1000)
	)
AS
	BEGIN
		INSERT INTO [dbo].[PrepChecklist]
			([Name], [Description])
		VALUES
			(@Name, @Description)
		SELECT SCOPE_IDENTITY()
	END
GO

print '' print '*** Creating sp_edit_prepchecklist'
GO
CREATE PROCEDURE [dbo].[sp_edit_prepchecklist]
	(
	
	@PrepChecklistID		[int],
	@NewName				[nvarchar](100),
	@NewDescription			[nvarchar](1000),
	@NewActive				[bit],
	@OldName				[nvarchar](100),
	@OldDescription			[nvarchar](1000),
	@OldActive				[bit]
	)
AS
	BEGIN
		UPDATE 		[PrepChecklist]
			SET 	[Name] = @NewName
					, [Description] = @NewDescription
					, [Active] = @NewActive
			WHERE 	[PrepChecklistID] = @PrepChecklistID
			AND		[Name] = @OldName
			AND		[Description] = @OldDescription
			AND		[Active] = @OldActive
		RETURN @@ROWCOUNT		
	END
GO

print '' print '*** Creating sp_create_inspectionchecklist'
GO
CREATE PROCEDURE [dbo].[sp_create_inspectionchecklist]
	(
	@Name				[nvarchar](100),
	@Description		[nvarchar](1000)
	)
AS
	BEGIN
		INSERT INTO [dbo].[InspectionChecklist]
			([Name], [Description])
		VALUES
			(@Name, @Description)
		SELECT SCOPE_IDENTITY()
	END
GO
/* End Amanda T */

/* Brady F */
print '' print '*** Creating sp_retrieve_maintenance_record_by_id'
GO
CREATE PROCEDURE [dbo].[sp_retrieve_maintenance_record_by_id]
	(
	@MaintenanceRecordID	[int]
	)
AS
	BEGIN
		SELECT	[MaintenanceRecordID], [EquipmentID], [EmployeeID], [Description], [Date]
		FROM	[MaintenanceRecord]
		WHERE	[MaintenanceRecordID] = @MaintenanceRecordID
	END
GO

print '' print '*** Creating sp_edit_maintenance_record'
GO
CREATE PROCEDURE [dbo].[sp_edit_maintenance_record]
	(
	@MaintenanceRecordID [int],
	@NewEquipmentID		 [int],
	@OldEquipmentID		 [int],
	@NewEmployeeID		 [int],
	@OldEmployeeID		 [int],
	@NewDescription		 [nvarchar](1000),
	@OldDescription		 [nvarchar](1000),
	@NewDate			 [Date],
	@OldDate			 [Date]
	)
AS
	BEGIN
		UPDATE [MaintenanceRecord]
			SET [EquipmentID] = @NewEquipmentID,
				[EmployeeID] = @NewEmployeeID,
				[Description] = @NewDescription,
				[Date] = @NewDate
			WHERE [MaintenanceRecordID] = @MaintenanceRecordID
            AND [EquipmentID] = @OldEquipmentID
            AND [EmployeeID] = @OldEmployeeID
			AND [Description] = @OldDescription
			AND [Date] = @OldDate
		RETURN @@ROWCOUNT	
	END
GO

print '' print '*** Creating sp_create_maintenance_record'
GO
CREATE PROCEDURE [dbo].[sp_create_maintenance_record]
	(
	@EquipmentID		[int],
	@EmployeeID			[int],
	@Description		[nvarchar](1000),
	@Date 				[Date]
	)
AS
	BEGIN
		INSERT INTO	[dbo].[MaintenanceRecord]
			([EquipmentID], [EmployeeID], [Description], [Date])
		VALUES
			(@EquipmentID, @EmployeeID, @Description, @Date)
		RETURN @@ROWCOUNT
	END
GO

print '' print '*** Creating sp_edit_equipment_type'
GO
CREATE PROCEDURE [dbo].[sp_edit_equipment_type]
	(
	@EquipmentTypeID			[nvarchar](100),
    @OldInspectionChecklistID  	[int],
    @OldPrepChecklistID   		[int],
    @NewInspectionChecklistID   [int],
    @NewPrepChecklistID      	[int]
	)
AS
	BEGIN
		UPDATE [EquipmentType]
			SET [InspectionChecklistID] = @NewInspectionChecklistID,
				[PrepChecklistID] = @NewPrepChecklistID
			WHERE [EquipmentTypeID] = @EquipmentTypeID 
            AND [InspectionChecklistID] = @OldInspectionChecklistID
            AND [PrepChecklistID] = @OldPrepChecklistID
		RETURN @@ROWCOUNT
	END
GO

print '' print '*** Creating sp_retrieve_equipment_list'
GO
CREATE PROCEDURE [dbo].[sp_retrieve_equipment_list]

AS
	BEGIN
		SELECT 		[EquipmentID], [EquipmentTypeID], [Name], [MakeModelID], 
					[DatePurchased], [DateLastRepaired],[PriceAtPurchase], 
					[CurrentValue], [WarrantyUntil], [EquipmentStatusID], 
					[EquipmentDetails], [Active]
		FROM 		[Equipment]
		ORDER BY 	[EquipmentID]
	END
GO

print '' print '*** Creating sp_retrieve_equipment_by_id'
GO
CREATE PROCEDURE [dbo].[sp_retrieve_equipment_by_id]
	(
	@EquipmentID 	[nvarchar](100)
	)
AS
	BEGIN
		SELECT 		[EquipmentID], [EquipmentTypeID], [Name], [MakeModelID], 
					[DatePurchased], [DateLastRepaired],[PriceAtPurchase], 
					[CurrentValue], [WarrantyUntil], [EquipmentStatusID], 
					[EquipmentDetails], [Active]
		FROM 		[Equipment]
		WHERE 		[EquipmentID] = @EquipmentID
		ORDER BY 	[EquipmentID]
	END
GO

print '' print '*** Creating sp_retrieve_equipment_by_name'
GO
CREATE PROCEDURE [dbo].[sp_retrieve_equipment_by_name]
	(
	@Name 	[nvarchar](100)
	)
AS
	BEGIN
		SELECT 		[EquipmentID], [EquipmentTypeID], [Name], [MakeModelID], 
					[DatePurchased], [DateLastRepaired],[PriceAtPurchase], 
					[CurrentValue], [WarrantyUntil], [EquipmentStatusID], 
					[EquipmentDetails], [Active]
		FROM 		[Equipment]
		WHERE 		[Name] = @Name
		ORDER BY 	[EquipmentID]
	END
GO

print '' print '*** Creating sp_retrieve_equipment_by_status'
GO
CREATE PROCEDURE [dbo].[sp_retrieve_equipment_by_status]
	(
	@EquipmentStatusID 	[nvarchar](100)
	)
AS
	BEGIN
		SELECT 		[EquipmentID], [EquipmentTypeID], [Name], [MakeModelID], 
					[DatePurchased], [DateLastRepaired],[PriceAtPurchase], 
					[CurrentValue], [WarrantyUntil], [EquipmentStatusID], 
					[EquipmentDetails], [Active]
		FROM 		[Equipment]
		WHERE 		[EquipmentStatusID] = @EquipmentStatusID
		ORDER BY 	[EquipmentID]
	END
GO

print '' print '*** Creating sp_retrieve_equipment_by_type'
GO
CREATE PROCEDURE [dbo].[sp_retrieve_equipment_by_type]
	(
	@EquipmentTypeID 	[nvarchar](100)
	)
AS
	BEGIN
		SELECT 		[EquipmentID], [EquipmentTypeID], [Name], [MakeModelID], 
					[DatePurchased], [DateLastRepaired],[PriceAtPurchase], 
					[CurrentValue], [WarrantyUntil], [EquipmentStatusID], 
					[EquipmentDetails], [Active]
		FROM 		[Equipment]
		WHERE 		[EquipmentTypeID] = @EquipmentTypeID
		ORDER BY 	[EquipmentID]
	END
GO



print '' print '*** Creating sp_retrieve_equipment_by_active'
GO
CREATE PROCEDURE [dbo].[sp_retrieve_equipment_by_active]
	(
	@Active 	[bit]
	)
AS
	BEGIN
		SELECT 		[EquipmentID], [EquipmentTypeID], [Name], [MakeModelID], 
					[DatePurchased], [DateLastRepaired],[PriceAtPurchase], 
					[CurrentValue], [WarrantyUntil], [EquipmentStatusID], 
					[EquipmentDetails], [Active]
		FROM 		[Equipment]
		WHERE 		[Active] = @Active
		ORDER BY 	[EquipmentID]
	END
GO

print '' print '*** Creating sp_edit_equipment_by_id'
GO
CREATE PROCEDURE [dbo].[sp_edit_equipment_by_id]
	(
	@NewEquipmentTypeID		[nvarchar](100),
	@NewName				[nvarchar](100),
	@NewMakeModelID			[int],
	@NewDatePurchased		[date],
	@NewDateLastRepaired	[date],
	@NewPriceAtPurchase		[decimal],
	@NewCurrentValue		[decimal],
	@NewWarrantyUntil		[date],
	@NewEquipmentStatusID	[nvarchar](100),
	@NewEquipmentDetails	[nvarchar](1000),
	
	@OldEquipmentTypeID		[nvarchar](100),
	@OldName				[nvarchar](100),
	@OldMakeModelID			[int],
	@OldDatePurchased		[date],
	@OldDateLastRepaired	[date],
	@OldPriceAtPurchase		[decimal],
	@OldCurrentValue		[decimal],
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

print '' print '*** Creating sp_retrieve_inspectionchecklist_by_id'
GO
CREATE PROCEDURE [dbo].[sp_retrieve_inspectionchecklist_by_id]
	(
	@InspectionChecklistID 		[int]
	)
AS
	BEGIN
		SELECT 		[InspectionChecklistID], [Name], [Description], [Active]
		FROM 		[InspectionChecklist]
		WHERE 		[InspectionChecklistID] = @InspectionChecklistID
		ORDER BY 	[InspectionChecklistID]
	END
GO

print '' print '*** Creating sp_delete_inspectionchecklist_by_id'
GO
CREATE PROCEDURE [dbo].[sp_delete_inspectionchecklist_by_id]
	(
	@InspectionChecklistID 		[int]
	)
AS
	BEGIN
		DELETE FROM 	[InspectionChecklist]
		WHERE 			[InspectionChecklistID] = @InspectionChecklistID
		RETURN @@ROWCOUNT
	END
GO

print '' print '*** Creating sp_deactivate_inspectionchecklist_by_id'
GO
CREATE PROCEDURE [dbo].[sp_deactivate_inspectionchecklist_by_id]
	(
	@InspectionChecklistID 		[int]
	)
AS
	BEGIN
		UPDATE 	[InspectionChecklist]
			SET 	[Active] = 0
			WHERE 	[InspectionChecklistID] = @InspectionChecklistID
		RETURN @@ROWCOUNT
	END
GO

print '' print '*** Creating sp_create_preprecord'
GO
CREATE PROCEDURE [dbo].[sp_create_preprecord]
	(
	@EquipmentID		[int],
	@EmployeeID			[int],
	@Description		[nvarchar](1000),
	@Date				[date]
	)
AS
	BEGIN
		INSERT INTO [dbo].[PrepRecord]
			([EquipmentID], [EmployeeID], [Description], [Date])
		VALUES
			(@EquipmentID, @EmployeeID, @Description, @Date)
			
		SELECT SCOPE_IDENTITY()
	END
GO

/* Brady Feller */
print '' print '*** Creating sp_retrieve_equipment_type_by_type'
GO
CREATE PROCEDURE [dbo].[sp_retrieve_equipment_type_by_type]
	(
	@EquipmentTypeID	[nvarchar](100)
	)
AS
	BEGIN
		SELECT	[EquipmentTypeID], [InspectionChecklistID], [PrepChecklistID], [Active]
		FROM	[EquipmentType]
		WHERE	[EquipmentTypeID] = @EquipmentTypeID
	END
GO

print '' print '*** Creating sp_retrieve_equipment_type'
GO
CREATE PROCEDURE [dbo].[sp_retrieve_equipment_type]
AS
	BEGIN
		SELECT	[EquipmentTypeID], [InspectionChecklistID], [PrepChecklistID], [Active]
		FROM	[EquipmentType]
	END
GO

print '' print  '*** Creating procedure sp_create_equipment_type'
GO
CREATE PROCEDURE [dbo].[sp_create_equipment_type]
	(
	@EquipmentTypeID		[nvarchar](100),
	@InspectionChecklistID	[int],
	@PrepChecklistID		[int],
	@Active					[bit]
	)
AS
	BEGIN
		INSERT INTO [EquipmentType]
			([EquipmentTypeID], [InspectionChecklistID], [PrepChecklistID], [Active])
		VALUES
			(@EquipmentTypeID, @InspectionChecklistID, @PrepChecklistID, @Active)
		SELECT SCOPE_IDENTITY()
	END
GO

/* End Brady Feller */

/* Jacob Slaubaugh */

print '' print '*** Creating sp_retrieve_equipmentstatus_list'
GO
CREATE PROCEDURE [dbo].[sp_retrieve_equipmentstatus_list]
AS
	BEGIN
		SELECT [EquipmentStatusID]
		FROM [EquipmentStatus]
	END
GO

print '' print '*** Creating sp_retrieve_equipmentstatus_by_id'
GO
CREATE PROCEDURE [dbo].[sp_retrieve_equipmentstatus_by_id]
	(
		@EquipmentStatusID		[nvarchar](100)
	)
AS
	BEGIN
		SELECT 		[EquipmentStatusID]
		FROM		[EquipmentStatus]
		WHERE		[EquipmentStatusID] = @EquipmentStatusID
	END
GO

print '' print '*** Creating sp_delete_equipmentstatus_by_id'
GO
CREATE PROCEDURE [dbo].[sp_delete_equipmentstatus_by_id]
	(
	@EquipmentStatusID		[nvarchar](100)
	)
AS
	BEGIN
		DELETE FROM [EquipmentStatus]
			WHERE [EquipmentStatusID] = @EquipmentStatusID
		RETURN @@ROWCOUNT
	END
GO

print '' print '*** Creating sp_edit_equipmentstatus_by_id'
GO
CREATE PROCEDURE [dbo].[sp_edit_equipmentstatus_by_id]
	(
    @OldEquipmentStatusID      	[nvarchar](100),
    @NewEquipmentStatusID      	[nvarchar](100)
	)
AS
	BEGIN
		UPDATE [EquipmentStatus]
			SET [EquipmentStatusID] = @NewEquipmentStatusID 
			WHERE [EquipmentStatusID] = @OldEquipmentStatusID 
		RETURN @@ROWCOUNT
	END
GO

print '' print '*** Creating sp_delete_maintenancerecord_by_id'
GO
CREATE PROCEDURE [dbo].[sp_delete_maintenancerecord_by_id]
	(
	@MaintenanceRecordID	[int]
	)
AS
	BEGIN
		DELETE FROM [MaintenanceRecord]
			WHERE [MaintenanceRecordID] = @MaintenanceRecordID
		RETURN @@ROWCOUNT
	END
GO
/* End Jacob Slaubaugh */

/* Noah Davison */
print '' print '*** Creating sp_retrieve_equipmenttype_list_by_active'
GO
CREATE PROCEDURE [dbo].[sp_retrieve_equipmenttype_list_by_active]
	(
		@Active					[bit]
	)
AS
	BEGIN
		SELECT		[EquipmentTypeID],[InspectionChecklistID],[PrepChecklistID]
		FROM     	[EquipmentType]
		WHERE		[Active] = @Active
	END
GO

/* sp_create_equipment was missing WarrantyUntil - Noah Davison*/
print '' print '*** Creating sp_create_equipment_fixed'
GO
CREATE PROCEDURE [dbo].[sp_create_equipment_fixed]
	(
	@EquipmentTypeID	[nvarchar](100),
	@Name				[nvarchar](100),
	@MakeModelID		[int],
	@DatePurchased		[date],
	@DateLastRepaired	[date],
	@PriceAtPurchase	[money],
	@CurrentValue		[money],
	@WarrantyUntil		[date],
	@EquipmentStatusID	[nvarchar](100),
	@EquipmentDetails	[nvarchar](1000)
	)
AS
	BEGIN
		INSERT INTO [dbo].[Equipment]
			([EquipmentTypeID], [Name], [MakeModelID], [DatePurchased], [DateLastRepaired], [PriceAtPurchase], [CurrentValue], [WarrantyUntil], [EquipmentStatusID], [EquipmentDetails])
		VALUES
			(@EquipmentTypeID, @Name, @MakeModelID, @DatePurchased, @DateLastRepaired, @PriceAtPurchase, @CurrentValue, @WarrantyUntil, @EquipmentStatusID, @EquipmentDetails  )
		SELECT SCOPE_IDENTITY()
	END
GO

print '' print '*** Creating sp_retrieve_inspectionrecord_detail_list'
GO
CREATE PROCEDURE [dbo].[sp_retrieve_inspectionrecord_detail_list]
AS
	BEGIN
		SELECT [InspectionRecordID], [InspectionRecord].[EquipmentID], [InspectionRecord].[EmployeeID]
		, [InspectionRecord].[Description], [InspectionRecord].[Date], [Equipment].[Name]
		, [Employee].[LastName], [Employee].[FirstName]
		FROM	[InspectionRecord], [Equipment], [Employee]
		WHERE	[InspectionRecord].[EquipmentID] = [Equipment].[EquipmentID]
		AND 	[InspectionRecord].[EmployeeID] = [Employee].[EmployeeID]
	END
GO


print '' print '*** Creating sp_retrieve_equipment_list_by_type_and_schedule'
GO
CREATE PROCEDURE [dbo].[sp_retrieve_equipment_list_by_type_and_schedule]
	(
	@EquipmentTypeID			[nvarchar](100),
	@StartDate					[DateTime],
	@EndDate					[DateTime]
	)
AS
	BEGIN
		SELECT DISTINCT	[Equipment].[EquipmentID], [EquipmentTypeID], [Name]
						[MakeModelID], [DatePurchased], [DateLastRepaired], [PriceAtPurchase], 
						[CurrentValue], [WarrantyUntil], [EquipmentStatusID], 
						[EquipmentDetails], [Active]
		FROM 			[Equipment]
		LEFT OUTER JOIN [EquipmentSchedule] ON [EquipmentSchedule].[EquipmentID] = [Equipment].[EquipmentID]
		AND 			(
								/* The proposed schedule must not be inside an existing schedule,
									or surround an existing schedule */
								((@StartDate > [StartDate] AND	@StartDate < [EndDate])
							OR	(@EndDate > [StartDate] AND	@EndDate < [EndDate]))
						OR
								(@StartDate < [StartDate] AND @EndDate > [EndDate])
						)
						/* If the outer join finds a row that breaks the aforementioned rule,
							the row that gets returned will have non-null start/end dates.
							This filters those out. */
		WHERE 			[StartDate] IS NULL AND [EndDate] IS NULL
		AND				[Active] = 1
		AND				[EquipmentTypeID] = @EquipmentTypeID
		AND				[EquipmentStatusID] = 'Available'
	END
GO

print '' print '*** Creating sp_retrieve_assigned_equipment_by_task_id'
GO
CREATE PROCEDURE [dbo].[sp_retrieve_assigned_equipment_by_task_id]
	(
		@TaskID		[int]
	)
AS
	BEGIN
		SELECT [Equipment].[EquipmentID], [Equipment].[EquipmentTypeID], [Equipment].[Name], [Equipment].[MakeModelID], [Equipment].[DatePurchased], [Equipment].[DateLastRepaired], [Equipment].[PriceAtPurchase], [Equipment].[CurrentValue], [Equipment].[WarrantyUntil], [Equipment].[EquipmentStatusID], [Equipment].[EquipmentDetails]
		FROM [Equipment]
		INNER JOIN [TaskEquipment] ON [Equipment].[EquipmentID] = [TaskEquipment].[EquipmentID]
		INNER JOIN [TaskTypeEquipmentNeed] ON [TaskEquipment].[TaskTypeEquipmentNeedID] = [TaskTypeEquipmentNeed].[TaskTypeEquipmentNeedID]
		INNER JOIN [TaskType] ON [TaskTypeEquipmentNeed].[TaskTypeID] = [TaskType].[TaskTypeID]
		INNER JOIN [Task] ON [TaskType].[TaskTypeID] = [Task].[TaskTypeID]
		WHERE [Task].[TaskID] = @TaskID
	END
GO

print '' print '*** Creating sp_retrieve_assigned_equipment_by_task_id_and_job_id'
GO
CREATE PROCEDURE [dbo].[sp_retrieve_assigned_equipment_by_task_id_and_job_id]
	(
		@TaskID		[int],
		@JobID		[int]
	)
AS
	BEGIN
		SELECT [Equipment].[EquipmentID], [Equipment].[EquipmentTypeID], [Equipment].[Name], [Equipment].[MakeModelID], [Equipment].[DatePurchased], [Equipment].[DateLastRepaired], [Equipment].[PriceAtPurchase], [Equipment].[CurrentValue], [Equipment].[WarrantyUntil], [Equipment].[EquipmentStatusID], [Equipment].[EquipmentDetails]
		FROM [Equipment]
		INNER JOIN [TaskEquipment] ON [Equipment].[EquipmentID] = [TaskEquipment].[EquipmentID]
		INNER JOIN [TaskTypeEquipmentNeed] ON [TaskEquipment].[TaskTypeEquipmentNeedID] = [TaskTypeEquipmentNeed].[TaskTypeEquipmentNeedID]
		INNER JOIN [TaskType] ON [TaskTypeEquipmentNeed].[TaskTypeID] = [TaskType].[TaskTypeID]
		INNER JOIN [Task] ON [TaskType].[TaskTypeID] = [Task].[TaskTypeID]
		WHERE [Task].[TaskID] = @TaskID
		AND [TaskEquipment].[JobID] = @JobID
	END
GO


print '' print '*** Creating sp_get_available_equipment_for_job'
GO
CREATE PROCEDURE [dbo].[sp_get_available_equipment_for_job]
	(
		@JobID int
	)
AS
	BEGIN
		-- get active and status=available equipment
		SELECT [EquipmentID]
		INTO #AvailableEquipment
		FROM [Equipment]
		WHERE [Active] = 1
		AND [EquipmentStatusID] = 'Available'
		
		-- remove equipment that is already scheduled around that time according to the half day increment
		DELETE FROM #AvailableEquipment
		WHERE [EquipmentID] IN
			(
				SELECT [TaskEquipment].[EquipmentID]
				FROM [TaskEquipment], [Job]
				WHERE [TaskEquipment].[JobID] = [Job].[JobID]
				AND (SELECT [DateTimeScheduled] FROM [Job] WHERE [JobID] = @JobID)
					BETWEEN [Job].[DateTimeScheduled] AND DATEADD(HOUR, 12, [Job].[DateTimeScheduled])
				AND [Job].[JobID] != @JobID
			)
		
		-- get available
		SELECT [Equipment].[EquipmentID], [Equipment].[EquipmentTypeID], [Equipment].[Name]
		FROM [Equipment], #AvailableEquipment
		WHERE [Equipment].[EquipmentID] = #AvailableEquipment.[EquipmentID]
		
	END
GO


print '' print '*** Creating sp_retrieve_makemodel_detail_list'
GO
CREATE PROCEDURE [dbo].[sp_retrieve_makemodel_detail_list]
AS
	BEGIN
		SELECT	[MakeModelID], [Make], [Model], [MakeModel].[Active]
		, [MakeModel].[MaintenanceChecklistID], [MaintenanceChecklist].[Name]
		FROM	[MakeModel] LEFT OUTER JOIN [MaintenanceChecklist]
		ON [MakeModel].[MaintenanceChecklistID] = [MaintenanceChecklist].[MaintenanceChecklistID]
	END
GO


print '' print '*** Creating sp_retrieve_maintenance_record_detail_list'
GO
CREATE PROCEDURE [dbo].[sp_retrieve_maintenance_record_detail_list]
AS
	BEGIN
		SELECT [MaintenanceRecord].[MaintenanceRecordID], [MaintenanceRecord].[EquipmentID], [MaintenanceRecord].[EmployeeID], 
		[MaintenanceRecord].[Description], [MaintenanceRecord].[Date], [Equipment].[Name], [Employee].[FirstName], [Employee].[LastName]
		FROM [MaintenanceRecord]
		INNER JOIN [Equipment] ON [Equipment].[EquipmentID] = [MaintenanceRecord].[EquipmentID]
		INNER JOIN [Employee] ON [Employee].[EmployeeID] = [MaintenanceRecord].[EmployeeID]
	END
GO


print '' print '*** Creating sp_retrieve_personal_equipment_by_assigned'
GO
CREATE PROCEDURE [dbo].[sp_retrieve_personal_equipment_by_assigned]
	(
	@Assigned	[bit]
	)
AS
	BEGIN
		SELECT	[PersonalEquipmentID], [PersonalEquipmentType], [Name], [Description], [PersonalEquipmentStatus], [Assigned]
		FROM  	[PersonalEquipment]
		WHERE	[Assigned] = @Assigned
	END
GO

print '' print '*** Creating sp_retrieve_assigned_personal_equipment_by_employee_id'
GO
CREATE PROCEDURE [dbo].[sp_retrieve_assigned_personal_equipment_by_employee_id]
	(
	@EmployeeID		[int]
	)
AS
	BEGIN
		SELECT 	[EmployeePersonalEquipmentAssignment].[PersonalEquipmentID], [PersonalEquipment].[PersonalEquipmentType], [PersonalEquipment].[Name],
				[PersonalEquipment].[Description], [PersonalEquipment].[PersonalEquipmentStatus], [PersonalEquipment].[Assigned]
		FROM	[EmployeePersonalEquipmentAssignment]
		INNER JOIN	[PersonalEquipment] ON [EmployeePersonalEquipmentAssignment].[PersonalEquipmentID] = [PersonalEquipment].[PersonalEquipmentID]
		WHERE	[EmployeePersonalEquipmentAssignment].[EmployeeID] = @EmployeeID
	END
GO


print '' print '*** Creating sp_update_personal_equipment_assignment'
GO
CREATE PROCEDURE [dbo].[sp_update_personal_equipment_assignment]
	(
	@PersonalEquipmentID	[int],
	@Assigned				[bit]
	)
AS
	BEGIN
		UPDATE [PersonalEquipment]
			SET		[Assigned] = @Assigned
			WHERE	[PersonalEquipmentID] = @PersonalEquipmentID
		RETURN @@ROWCOUNT
	END
GO

print '' print '*** Creating sp_delete_personal_equipment_assignment'
GO
CREATE PROCEDURE [dbo].[sp_delete_personal_equipment_assignment]
	(
	@EmployeeID				[int],
	@PersonalEquipmentID	[int]
	)
AS
	BEGIN
		DELETE FROM [EmployeePersonalEquipmentAssignment]
			WHERE	[EmployeeID] = @EmployeeID
			AND		[PersonalEquipmentID] = @PersonalEquipmentID
		RETURN @@ROWCOUNT
	END
GO

print '' print '*** Creating sp_add_personal_equipment_assignment'
GO
CREATE PROCEDURE [dbo].[sp_add_personal_equipment_assignment]
	(
	@EmployeeID				[int],
	@PersonalEquipmentID	[int]
	)
AS
	BEGIN
		INSERT INTO [dbo].[EmployeePersonalEquipmentAssignment]
			([EmployeeID], [PersonalEquipmentID])
		VALUES
			(@EmployeeID, @PersonalEquipmentID)
		RETURN @@ROWCOUNT
	END
GO

print '' print '*** Creating sp_retrieve_Prep_record_detail_list'
GO
CREATE PROCEDURE [dbo].[sp_retrieve_Prep_record_detail_list]
AS
	BEGIN
		SELECT [PrepRecord].[PrepRecordID], [PrepRecord].[EquipmentID], [PrepRecord].[EmployeeID], 
		[PrepRecord].[Description], [PrepRecord].[Date], [Equipment].[Name], [Employee].[FirstName], [Employee].[LastName]
		FROM [PrepRecord]
		INNER JOIN [Equipment] ON [Equipment].[EquipmentID] = [PrepRecord].[EquipmentID]
		INNER JOIN [Employee] ON [Employee].[EmployeeID] = [PrepRecord].[EmployeeID]
	END
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


print '' print '*** Creating sp_edit_preprecord_by_id'
GO
CREATE PROCEDURE [dbo].[sp_edit_preprecord_by_id]
	(
	@PrepRecordID			[int],
	@OldEquipmentID		[int],
	@NewEquipmentID		[int],
	@OldEmployeeID		[int],
	@NewEmployeeID		[int],
	@OldDescription 	[NVARCHAR] (1000),
	@NewDescription 	[NVARCHAR] (1000),
	@OldDate			[Date],
	@NewDate 			[Date]
	)
AS
	BEGIN
		UPDATE 	[PrepRecord]
			SET 	
					[EquipmentID] = @NewEquipmentID,
					[EmployeeID] = @NewEmployeeID,
					[Description] = @NewDescription,
					[Date] = @NewDate
					
			WHERE 	[PrepRecordID] = @PrepRecordID
			AND [EquipmentID] = @OldEquipmentID
			AND [EmployeeID] = @OldEmployeeID
			AND [Description] = @OldDescription
			AND [Date] = @OldDate
		RETURN @@ROWCOUNT
	END
GO

print '' print '*** Creating sp_retrieve_equipment_type_detail_list'
GO
CREATE PROCEDURE [dbo].[sp_retrieve_equipment_type_detail_list]
AS
	BEGIN
		SELECT	[EquipmentType].[EquipmentTypeID], [EquipmentType].[InspectionChecklistID], [EquipmentType].[PrepChecklistID], [EquipmentType].[Active],
				[InspectionChecklist].[Name], [PrepChecklist].[Name]
		FROM	[EquipmentType]
		LEFT JOIN [InspectionChecklist] ON [EquipmentType].[InspectionChecklistID] = [InspectionChecklist].[InspectionChecklistID]
		LEFT JOIN [PrepChecklist] ON [EquipmentType].[PrepChecklistID] = [PrepChecklist].[PrepChecklistID]
	END
GO