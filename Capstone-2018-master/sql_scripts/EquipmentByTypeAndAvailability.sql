/* James McPherson */
print '' print '*** in file EquipmentByTypeAndAvailability.sql ***'
USE [crlandscaping]
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

print '' print '*** Creating sp_retrieve_employee_list_by_certification_and_schedule'
GO
CREATE PROCEDURE [dbo].[sp_retrieve_employee_list_by_certification_and_schedule]
	(
	@certificationID			[nvarchar](100),
	@StartDate					[DateTime],
	@EndDate					[DateTime]
	)
AS
	BEGIN
		SELECT DISTINCT	[Employee].[EmployeeID], [FirstName], [LastName]
						[Address], [PhoneNumber], [Email], [Active]
		FROM 			[Employee]
		JOIN [EmployeeCertification] ON [EmployeeCertification].[EmployeeID] = [Employee].[EmployeeID]
		LEFT OUTER JOIN [EmployeeSchedule] ON [EmployeeSchedule].[EmployeeID] = [Employee].[EmployeeID]
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
		AND				[Employee].[Active] = 1
		AND				[CertificationID] = @certificationID
	END
GO