-- Created by Reuben Cassell

print '' print '*** In file EmployeeEquipmentAssignmentScript.sql'
GO
print '' print '*** Using database crlandscaping'
GO
USE [crlandscaping]
GO

-- *********************** CREATE TABLES ***************

print '' print '*** Creating PersonalEquipment Table'
GO
CREATE TABLE [PersonalEquipment](
	[PersonalEquipmentID] 				[int] IDENTITY(1000000, 1) 		NOT NULL,
	[PersonalEquipmentType]				[nvarchar](100)					NOT NULL,
	[Name]								[nvarchar](100)					NOT NULL,
	[Description]						[nvarchar](1000)				NOT NULL,
	[PersonalEquipmentStatus]			[nvarchar](50)					NOT NULL,
	[Assigned]							[bit]							NOT NULL DEFAULT 0,
	CONSTRAINT [pk_PersonalEquipmentID] PRIMARY KEY([PersonalEquipmentID] ASC)
)
GO

print '' print '*** Creating EmplpoyeePersonalEquipmentAssignment Table'
GO
CREATE TABLE [EmployeePersonalEquipmentAssignment] ( 
	[PersonalEquipmentID]	[int]	NOT NULL,
	[EmployeeID]			[int]	NOT NULL,
	CONSTRAINT	[pk_PersonalEquipmentID_EmployeeID] PRIMARY KEY([PersonalEquipmentID] ASC, [EmployeeID] ASC)
)
GO


-- *********************** SAMPLE DATA ***************

print '' print '*** Inserting PersonalEquipment sample data'
GO
INSERT INTO [dbo].[PersonalEquipment]
		([PersonalEquipmentType], [Name], [Description], [PersonalEquipmentStatus])
	VALUES
		('Tools','Toolbox','Box for holding tools', 'Ready'),
		('Tools','Toolbox','Box for holding tools', 'Damaged'),
		('Tools','Toolbox','Box for holding tools', 'Being Serviced'),
		('Tools','Tape Measure','Tape used for measuring', 'Ready'),
		('Tools','Tape Measure','Tape used for measuring', 'Damaged'),
		('Tools','Hammer','Used for hammering', 'Ready')
GO

-- *********************** STORED PROCEDURES ***************

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