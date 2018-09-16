print '' print '*** In script employee_SPROCS.sql'
print '' print '*** Using database crlandscaping'
GO
USE [crlandscaping]
GO


/* Tested by James McPherson 2/13/2018 */
print '' print '*** Creating sp_deactivate_certification_by_id'
GO
CREATE PROCEDURE [dbo].[sp_deactivate_certification_by_id]
	(
	@CertificationID		[int]
	)
AS
	BEGIN
		UPDATE [Certification]
			SET [Active] = 0
			WHERE [CertificationID] = @CertificationID
		RETURN @@ROWCOUNT
	END
GO

/* Tested by James McPherson 2/13/2018 */
print '' print '*** Creating sp_deactivate_employeecertification'
GO
CREATE PROCEDURE [dbo].[sp_deactivate_employeecertification]
	(
	@EmployeeID			[int],
	@CertificationID	[int]
	)
AS
	BEGIN
		UPDATE [EmployeeCertification]
			SET [Active] = 0
			WHERE	[EmployeeID] = @EmployeeID
			AND		[CertificationID] = @CertificationID
		RETURN @@ROWCOUNT
	END
GO

/* Tested by James McPherson 2/20/2018 */
print '' print '*** Creating sp_create_employeecertification'
GO
CREATE PROCEDURE [dbo].[sp_create_employeecertification]
	(
	@CertificationID			[int],
	@EmployeeID					[int],
	@EndDate					[datetime]
	)
AS
	BEGIN
		INSERT INTO [dbo].[EmployeeCertification]
			([CertificationID], [EmployeeID], [EndDate])
		VALUES
			(@CertificationID, @EmployeeID, @EndDate)
	END
GO

print '' print '*** Creating sp_retrieve_employeecertification_by_id'
GO
CREATE PROCEDURE [dbo].[sp_retrieve_employeecertification_by_id]
	(
	@CertificationID			[int],
	@EmployeeID					[int]
	)
AS
	BEGIN
		SELECT 		[CertificationID], [EmployeeID], [EndDate], [Active]
		FROM		[EmployeeCertification]
		WHERE		[CertificationID] = @CertificationID
		AND			[EmployeeID] = @EmployeeID
		ORDER BY 	[CertificationID]
	END
GO

print '' print '*** Creating sp_edit_employeecertification_by_id'
GO
CREATE PROCEDURE [dbo].[sp_edit_employeecertification_by_id]
	(
	@CertificationID			[int],
	@EmployeeID					[int],
	@EndDate					[datetime]
	)
AS
	BEGIN
		UPDATE [EmployeeCertification]
			SET		[CertificationID] = @CertificationID,
					[EmployeeID] = @EmployeeID,
					[EndDate] = @EndDate
			WHERE	[CertificationID] = @CertificationID
		RETURN @@ROWCOUNT
	END
GO

print '' print '*** Creating sp_delete_employeecertification_by_id'
GO
CREATE PROCEDURE [dbo].[sp_delete_employeecertification_by_id]
	(
	@CertificationID			[int]
	)
AS
	BEGIN
		DELETE FROM EmployeeCertification
		WHERE [CertificationID] = @CertificationID
	END
GO

print '' print '*** Creating sp_create_availability'
GO
CREATE PROCEDURE [dbo].[sp_create_availability]
	(
	@EmployeeID					[int],
	@StartTime					[datetime],
	@EndTime					[datetime]
	)
AS
	BEGIN
		INSERT INTO [dbo].[Availability]
			([EmployeeID], [StartTime], [EndTime])
		VALUES
			(@EmployeeID, @StartTime, @EndTime)
	END
GO

print '' print '*** Creating sp_retrieve_availability_by_id'
GO
CREATE PROCEDURE [dbo].[sp_retrieve_availability_by_id]
	(
	@AvailabilityID				[int]
	)
AS
	BEGIN
		SELECT 		[AvailabilityID], [EmployeeID], [StartTime], [EndTime]
		FROM		[Availability]
		WHERE		[AvailabilityID] = @AvailabilityID
		ORDER BY 	[AvailabilityID]
	END
GO

print '' print '*** Creating sp_delete_availability_by_id'
GO
CREATE PROCEDURE [dbo].[sp_delete_availability_by_id]
	(
	@AvailabilityID				[int]
	)
AS
	BEGIN
		DELETE FROM Availability
		WHERE [AvailabilityID] = @AvailabilityID
	END
GO
/* End Jacob S */

print '' print '*** Creating sp_create_employee'
GO
CREATE PROCEDURE [dbo].[sp_create_employee]
	(
	@FirstName			[nvarchar](100),
	@LastName			[nvarchar](100),
	@Address			[nvarchar](250),
	@PhoneNumber		[nvarchar](15),
	@Email				[nvarchar](100),
	@PasswordHash		[nvarchar](100),
	@Active				[bit]
	)
AS
	BEGIN
		INSERT INTO [dbo].[Employee]
			([FirstName], [LastName], [Address], [PhoneNumber]
			,[Email], [PasswordHash], [Active])
		VALUES
			(@FirstName, @LastName, @Address, @PhoneNumber
			,@Email, @PasswordHash, @Active)
	END
GO

/* Tested by James McPherson 2/3/2018 */
print '' print '*** Creating sp_retrieve_employee_list_by_active'
GO
CREATE PROCEDURE [dbo].[sp_retrieve_employee_list_by_active]
	(
	@Active				[bit]
	)
AS
	BEGIN
		SELECT	[EmployeeID], [FirstName], [LastName], [Address], [PhoneNumber]
			,[Email], [Active]
		FROM	[Employee]
		WHERE	[Active] = @Active
	END
GO

print '' print '*** Creating sp_retrieve_employee_by_id'
GO
CREATE PROCEDURE [dbo].[sp_retrieve_employee_by_id]
	(
		@EmployeeID		[int]
	)
AS
	BEGIN
		SELECT	[FirstName], [LastName], [Address], [PhoneNumber]
			,[Email], [Active]
		FROM	[Employee]
		WHERE	[EmployeeID] = @EmployeeID
	END
GO

print '' print '*** Creating sp_retrieve_employee_by_name'
GO
CREATE PROCEDURE [dbo].[sp_retrieve_employee_by_name]
	(
		@FirstName		[nvarchar](100),
		@LastName		[nvarchar](100)	
	)
AS
	BEGIN
		SELECT	[EmployeeID], [Address], [PhoneNumber]
			,[Email], [Active]
		FROM	[Employee]
		WHERE	[FirstName] = @FirstName
		AND		[LastName] = @LastName
	END
GO

print '' print '*** Creating sp_edit_employee_by_id'
GO
CREATE PROCEDURE [dbo].[sp_edit_employee_by_id]
	(
		@NewFirstName 	 	[nvarchar](100),
		@OldFirstName 	 	[nvarchar](100),
		@NewLastName 		[nvarchar](100),
		@OldLastName  		[nvarchar](100),
		@NewAddress  		[nvarchar](250),
		@OldAddress  		[nvarchar](250),
		@NewPhoneNumber		[nvarchar](15),
		@OldPhoneNumber		[nvarchar](15),
		@NewEmail			[nvarchar](100),
		@OldEmail			[nvarchar](100),
		@EmployeeID			[int]
	)
AS
	BEGIN
		UPDATE [Employee]
			SET 	[FirstName] = @NewFirstName,
					[LastName] = @NewLastName,
					[Address] = @NewAddress,
					[PhoneNumber] = @NewPhoneNumber,
					[Email] = @NewEmail
			WHERE 	[EmployeeID] = @EmployeeID
			AND		[FirstName] = @OldFirstName
			AND		[LastName] = @OldLastName
			AND		[Address] = @OldAddress
			AND		[PhoneNumber] = @OldPhoneNumber
			AND		[Email] = @OldEmail
		RETURN @@ROWCOUNT
	END
GO

print '' print '*** Creating sp_reactivate_employee_by_id'
GO
CREATE PROCEDURE [dbo].[sp_reactivate_employee_by_id]
	(
	@EmployeeID 	[int]
	)
AS
	BEGIN
		UPDATE [Employee]
			SET [Active] = 1
			WHERE [EmployeeID] = @EmployeeID
		RETURN @@ROWCOUNT
	END
GO

/* Tested by James McPherson 2/3/2018 */
print '' print '*** Creating sp_deactivate_employee_by_id'
GO
CREATE PROCEDURE [dbo].[sp_deactivate_employee_by_id]
	(
		@EmployeeID 	[int]
	)
AS
	BEGIN
		UPDATE [Employee]
			SET [Active] = 0
			WHERE [EmployeeID] = @EmployeeID
		RETURN @@ROWCOUNT
	END
GO

print '' print '*** Creating sp_delete_employee_by_id'
GO
CREATE PROCEDURE [dbo].[sp_delete_employee_by_id]
	(
		@EmployeeID		[int]
	)
AS
	BEGIN
			DELETE
			FROM 		[Employee]
			WHERE 		[EmployeeID] = @EmployeeID
		RETURN @@ROWCOUNT
	END
GO

print '' print '*** Creating sp_create_role'
GO
CREATE PROCEDURE [dbo].[sp_create_role]
	(
		@RoleID				[nvarchar](100),
		@Description		[nvarchar](1000)
	)
AS
	BEGIN
		INSERT INTO [dbo].[Role]
			([RoleID], [Description])
		VALUES
			(@RoleID, @Description)
		RETURN @@ROWCOUNT
	END
GO

print '' print '*** Creating sp_retrieve_role_list'
GO
CREATE PROCEDURE [dbo].[sp_retrieve_role_list]
AS
	BEGIN
		SELECT	[RoleID], [Description]
		FROM	[Role]
	END
GO

print '' print '*** Creating sp_retrieve_role_by_id'
GO
CREATE PROCEDURE [dbo].[sp_retrieve_role_by_id]
	(
	@RoleID			[nvarchar](100)
	)
AS
	BEGIN
		SELECT	[RoleID], [Description]
		FROM	[Role]
		WHERE	[RoleID] = @RoleID
	END
GO

print '' print '*** Creating sp_edit_role'
GO
CREATE PROCEDURE [dbo].[sp_edit_role]
	(
		@NewDescription		[nvarchar](1000),
		@OldDescription		[nvarchar](1000),
		@RoleID				[nvarchar](100)
	)
AS
	BEGIN
		UPDATE [Role]
			SET 	[Description] = @NewDescription
			WHERE 	[Description] = @OldDescription
			AND		[RoleID] = @RoleID
		RETURN @@ROWCOUNT
	END
GO

print '' print '*** Creating sp_delete_role_by_id'
GO
CREATE PROCEDURE [dbo].[sp_delete_role_by_id]
	(
		@RoleID		[nvarchar](100)
	)
AS
	BEGIN
			DELETE
			FROM 		[Role]
			WHERE 		[RoleID] = @RoleID
		RETURN @@ROWCOUNT
	END
GO

print '' print '*** Creating sp_create_certification'
GO
CREATE PROCEDURE [dbo].[sp_create_certification]
	(
		@Name			[nvarchar](100),
		@Description	[nvarchar](1000),
		@Active	[bit]
	)
AS
	BEGIN
		INSERT INTO [dbo].[certification]
			([Name], [Description],[Active])
		VALUES
			(@Name, @Description, @Active)
		SELECT SCOPE_IDENTITY()
	END
GO

print '' print '*** Creating sp_retrieve_certification_by_id'
GO
CREATE PROCEDURE [dbo].[sp_retrieve_certification_by_id]
	(
		@CertificationID		[int]
	)
AS
	BEGIN
		SELECT	[Name], [Description], [Active]
		FROM	[certification]
		WHERE	[CertificationID] = @CertificationID
	END
GO

print '' print '*** Creating sp_edit_certification'
GO
CREATE PROCEDURE [dbo].[sp_edit_certification]
	(
		@NewName			[nvarchar](100),
		@OldName			[nvarchar](100),
		@NewDescription		[nvarchar](1000),
		@OldDescription		[nvarchar](1000),
		@CertificationID	[int],
		@OldActive				[bit],
		@NewActive				[bit]
	)
AS
	BEGIN
		UPDATE [certification]
			SET 	[Name] = @NewName,
					[Description] = @NewDescription,
					[Active] = @NewActive
			WHERE 	[Name] = @OldName
			AND		[Description] = @OldDescription
			AND		[CertificationID] = @CertificationID
			AND   [Active] = @OldActive
		RETURN @@ROWCOUNT
	END
GO

print '' print '*** Creating sp_delete_certification_by_id'
GO
CREATE PROCEDURE [dbo].[sp_delete_certification_by_id]
	(
		@CertificationID		[int]
	)
AS
	BEGIN
			DELETE
			FROM 		[certification]
			WHERE 		[CertificationID] = @CertificationID
		RETURN @@ROWCOUNT
	END
GO
/* End Noah D */

print '' print '*** Creating sp_edit_availability'
GO
CREATE PROCEDURE [dbo].[sp_edit_availability]
	(
	@tvpNewAvailabilities	[dbo].[AvailabilityTableType] READONLY,
	@EmployeeID			[int]
	)
AS
	BEGIN
		DECLARE @ServicePackageDeleteErr [int]
		DECLARE @ServicePackageInsertErr [int]
		DECLARE @maxerr [int]
		DECLARE @err_message [nvarchar](255)
		
		SET @maxerr = 0
		
		BEGIN TRANSACTION
		
		/*Delete all current availability records*/
		DELETE FROM [Availability]
		WHERE [EmployeeID] = @EmployeeID
		
		/*Check for delete error*/
		SET @ServicePackageDeleteErr = @@error
		IF @ServicePackageDeleteErr > @maxerr
			SET @maxerr = @ServicePackageDeleteErr
		
		/*Insert data from the custom availability table type*/
		INSERT INTO [Availability]
			([EmployeeID], [StartTime], [EndTime])
			SELECT [CustomAvailability].[EmployeeID]
				, [CustomAvailability].[StartTime]
				, [CustomAvailability].[EndTime]
			FROM @tvpNewAvailabilities AS [CustomAvailability]
		
		/*Check for insert errors*/
		SET @ServicePackageInsertErr = @@error
		IF @ServicePackageInsertErr > @maxerr
			SET @maxerr = @ServicePackageInsertErr
		
		/*Check for errors and rollback if there are any. Else, commit*/
		IF @ServicePackageDeleteErr <> 0
			BEGIN
				ROLLBACK
				SET @err_message = 'Could not properly edit the availability records'
				RAISERROR (@err_message, 20, 1)
			END
		ELSE IF @ServicePackageInsertErr <> 0
			BEGIN
				ROLLBACK
				SET @err_message = 'Could not properly insert availability records'
				RAISERROR (@err_message, 20, 1)
			END
		ELSE
			BEGIN
				COMMIT
				RETURN @@ROWCOUNT
			END
	END
GO

/* Tested by James McPherson 2/23/2018 */
print '' print '*** Creating sp_retrieve_availability_list_by_employee_id'
GO
CREATE PROCEDURE [dbo].[sp_retrieve_availability_list_by_employee_id]
	(
	@EmployeeID	[int]
	)
AS
	BEGIN
		SELECT [AvailabilityID], [EmployeeID], [StartTime], [EndTime]
		FROM [Availability]
		WHERE [EmployeeID] = @EmployeeID		
	END
GO


/* Dan C */
/* Weston O modified*/
print '' print '*** Creating sp_create_timeoff'
GO
CREATE PROCEDURE [dbo].[sp_create_timeoff]
	(
	@EmployeeID			[int],
	@StartTime			[DATETIME],
	@EndTime			[DATETIME],
	@Active				[bit]
	)
AS
	BEGIN
		INSERT INTO [dbo].[TimeOff]
			([EmployeeID], [StartTime], [EndTime], [Active])
		VALUES
			(@EmployeeID, @StartTime, @EndTime, @Active)
		SELECT SCOPE_IDENTITY()
	END
GO

/* Updated to include Active fields 2018/03/02 - John Miller */
print '' print '*** Creating sp_retrieve_timeoff_by_id'
GO
CREATE PROCEDURE [dbo].[sp_retrieve_timeoff_by_id]
	(
	@TimeOffID		[int]
	)
AS
	BEGIN
		SELECT	[TimeOffID], [EmployeeID], [StartTime], [EndTime], [Approved], [Active]
		FROM	[TimeOff]
		WHERE	[TimeOffID] = @TimeOffID
	END
GO

/* Updated to include Active fields 2018/03/02 - John Miller */
print '' print '*** Creating sp_retrieve_timeoff_by_employeeid'
GO
CREATE PROCEDURE [dbo].[sp_retrieve_timeoff_by_employeeid]
	(
	@EmployeeID			[int]
	)
AS
	BEGIN
		SELECT	[TimeOffID], [EmployeeID], [StartTime], [EndTime], [Approved], [Active]
		FROM	[TimeOff]
		WHERE	[EmployeeID] = @EmployeeID
	END
GO

/* John Miller - 2018/03/02 */
/* Weston Olund retrieve all rather than only active*/
print '' print '*** Creating sp_retrieve_timeoff_list'
GO
CREATE PROCEDURE [dbo].[sp_retrieve_timeoff_list]
AS
	BEGIN
		SELECT	[TimeOffID],[EmployeeID], [StartTime], [EndTime], [Approved],[Active]
		FROM	[TimeOff]
	END
GO


/* Updated to include Active fields 2018/03/02 - John Miller */
print '' print '*** Creating sp_edit_timeoff_by_timeoffid'
GO
CREATE PROCEDURE [dbo].[sp_edit_timeoff_by_timeoffid]
	(
	@TimeOffID				[int],
	@NewStartTime			[datetime],
	@NewEndTime				[datetime],
	@NewApproved			[bit],
	@NewActive				[bit],
	@OldStartTime			[datetime],
	@OldEndTime				[datetime],
	@OldApproved			[bit],
	@OldActive				[bit]
	)
AS
	BEGIN
		UPDATE [TimeOff]
			SET [StartTime] = @NewStartTime
			,	[EndTime] 	= @NewEndTime
			, 	[Approved] 	= @NewApproved
			,	[Active] = @NewActive
			WHERE [TimeOffID] = @TimeOffID
			AND [StartTime] = @OldStartTime
			AND [EndTime] = @OldEndTime
			AND [Approved] = @OldApproved
			AND [Active] = @OldActive
		RETURN @@ROWCOUNT
	END
GO

/* Created 2018/03/02 - John Miller */
print '' print '*** Creating sp_deactivate_timeoffrequest_by_id'
GO
CREATE PROCEDURE [dbo].[sp_deactivate_timeoffrequest_by_id]
	(
	@TimeOffID 		[int]
	)
AS
	BEGIN
		UPDATE 	[TimeOff]
			SET 	[Active] = 0
			WHERE 	[TimeOffID] = @TimeOffID
		RETURN @@ROWCOUNT
	END
GO

print '' print '*** Creating sp_delete_timeoff_by_id'
GO
CREATE PROCEDURE [dbo].[sp_delete_timeoff_by_id]
	(
	@TimeOffID		[int]
	)
AS
	BEGIN
		DELETE FROM [TimeOff]
			WHERE [TimeOffID] = @TimeOffID
		RETURN @@ROWCOUNT
	END
GO

print '' print '*** Creating sp_create_employeerole'
GO
CREATE PROCEDURE [dbo].[sp_create_employeerole]
	(
	@EmployeeID		[int],
	@RoleID			[nvarchar](100)
	)
AS
	BEGIN
		INSERT INTO [dbo].[EmployeeRole]
			([EmployeeID], [RoleID])
		VALUES
			(@EmployeeID, @RoleID)
	END
GO

print '' print '*** Creating sp_retrieve_employeerole_by_employeeid'
GO
CREATE PROCEDURE [dbo].[sp_retrieve_employeerole_by_employeeid]
	(
	@EmployeeID		[int]
	)
AS
	BEGIN
		SELECT	[EmployeeID], [RoleID]
		FROM	[EmployeeRole]
		WHERE	[EmployeeID] = @EmployeeID
	END
GO

print '' print '*** Creating sp_edit_employeerole_by_employeeid'
GO
CREATE PROCEDURE [dbo].[sp_edit_employeerole_by_employeeid]
	(
	@EmployeeID				[int],
	@NewRoleID				[nvarchar](100),
	@OldRoleID				[nvarchar](100)
	)
AS
	BEGIN
		UPDATE [EmployeeRole]
			SET [RoleID] = @NewRoleID
			WHERE [EmployeeID] = @EmployeeID
			AND [RoleID] = @OldRoleID
		RETURN @@ROWCOUNT
	END
GO

/* Tested by James McPherson 2/3/2018 */
print '' print '*** Creating sp_authenticate_user'
GO
CREATE PROCEDURE [dbo].[sp_authenticate_user]
	(
	@Email			[nvarchar](100),
	@PasswordHash	[nvarchar](100)
	)
AS
	BEGIN
		SELECT COUNT([EmployeeID])
		FROM 	[Employee]
		WHERE 	[Email] = @Email
		AND 	[PasswordHash] = @PasswordHash
		AND		[Active] = 1
	END
GO

/* Tested by James McPherson 2/3/2018 */
print '' print '*** Creating sp_update_passwordHash'
GO
CREATE PROCEDURE [dbo].[sp_update_passwordHash]
	(
	@EmployeeID			[int],
	@OldPasswordHash	[nvarchar](100),
	@NewPasswordHash	[nvarchar](100)
	)
AS
	BEGIN
		UPDATE [Employee]
			SET [PasswordHash] = @NewPasswordHash
			WHERE [EmployeeID] = @EmployeeID
			AND [PasswordHash] = @OldPasswordHash
		RETURN @@ROWCOUNT
	END
GO

/* Tested by James McPherson 2/3/2018 */
print '' print '*** Creating sp_retrieve_employee_by_email'
GO
CREATE PROCEDURE [dbo].[sp_retrieve_employee_by_email]
	(
	@Email		[nvarchar](100)
	)
AS
	BEGIN
		SELECT 	[EmployeeID], [FirstName], [LastName], [Address]
				, [PhoneNumber], [Email], [Active]
		FROM 	[Employee]
		WHERE 	[Email] = @Email
	END
GO

/* Tested by James McPherson 2/17/2018 */
/* Added a join to allow the creation of role objects */
print '' print '*** Creating sp_retrieve_employee_roles'
GO
CREATE PROCEDURE [dbo].[sp_retrieve_employee_roles]
	(
	@EmployeeID		[int]
	)
AS
	BEGIN
		SELECT 	[EmployeeRole].[RoleID], [Description] 
		FROM 	[EmployeeRole], [Role]
		WHERE 	[EmployeeRole].[EmployeeID] = @EmployeeID
		AND		[EmployeeRole].[RoleID] = [Role].[RoleID]
	END
GO
/* End Reuben C */


/* David X */
print '' print  '*** Creating procedure sp_delete_employeerole'
GO
CREATE PROCEDURE [dbo].[sp_delete_employeerole]
	(
	@EmployeeID			[int],
	@RoleID				[nvarchar](100)
	)
AS
	BEGIN
		DELETE FROM [EmployeeRole]
		WHERE [EmployeeID] = @EmployeeID
		AND [RoleID] = @RoleID
	END
GO

print '' print  '*** Creating procedure sp_retrieve_employeerole_list'
GO
CREATE PROCEDURE [dbo].[sp_retrieve_employeerole_list]
AS
	BEGIN
		SELECT [EmployeeID], [RoleID]
		FROM [EmployeeRole]
	END
GO

print '' print  '*** Creating procedure sp_retrieve_employee_list'
GO
CREATE PROCEDURE [dbo].[sp_retrieve_employee_list]
AS
	BEGIN
		SELECT [EmployeeID], [FirstName], [LastName], [Address]
		, [PhoneNumber], [Email], [Active]
		FROM [Employee]
	END
GO
/* End David X */

/* Weston O */
print '' print '*** Creating sp_retrieve_certification_list'
GO
CREATE PROCEDURE [dbo].[sp_retrieve_certification_list]
AS
	BEGIN
		SELECT	[CertificationID], [Name], [Description], [Active]
		FROM	[Certification]
	END
GO

print '' print '*** Creating sp_retrieve_resupplyorder_list'
GO
CREATE PROCEDURE [dbo].[sp_retrieve_resupplyorder_list]
AS
	BEGIN
		SELECT	[ResupplyOrderID], [EmployeeID], [Date], [SupplyStatusID], [VendorID]
		FROM	[ResupplyOrder]
	END
GO
/* End Weston O */


/* Zach M */
print '' print '*** Creating sp_create_maintenancechecklist'
GO
CREATE PROCEDURE [dbo].[sp_create_maintenancechecklist]
	(
	@Name				[nvarchar](100),
	@Description		[nvarchar](1000)
	)
AS
	BEGIN
		INSERT INTO [dbo].[MaintenanceChecklist]
			([Name], [Description])
		VALUES
			(@Name, @Description)
		SELECT SCOPE_IDENTITY()
	END
GO

/* Tested by James McPherson 2/16/2018 */
print '' print '*** Creating sp_retrieve_inspectionchecklist_list'
GO
CREATE PROCEDURE [dbo].[sp_retrieve_inspectionchecklist_list]
AS
    BEGIN
        SELECT  [InspectionChecklistID], [Name], [Description], [Active]
        FROM    [InspectionChecklist]
    END
GO

/* Tested by James McPherson 2/16/2018 */
print '' print '*** Creating sp_edit_inspectionchecklist'
GO
CREATE PROCEDURE [dbo].[sp_edit_inspectionchecklist]
    (
    @InspectionChecklistID  [int],
	@NewName			[nvarchar](100),
	@OldName			[nvarchar](100),
    @NewDescription     [nvarchar](1000),
    @OldDescription     [nvarchar](1000),
    @NewActive 			[bit],
	@OldActive			[bit]
    )
AS
    BEGIN
        UPDATE      [InspectionChecklist]
            SET     [Description] = @NewDescription
					, [Active] = @NewActive
					, [Name] = @NewName
            WHERE   [InspectionChecklistID] = @InspectionChecklistID
            AND     [Description] = @OldDescription
			AND		[Active] = @OldActive
        RETURN @@ROWCOUNT   
    END
GO

/* Tested by James McPherson 2/16/2018 */
print '' print '*** Creating sp_edit_maintenancechecklist'
GO
CREATE PROCEDURE [dbo].[sp_edit_maintenancechecklist]
	(
	@MaintenanceChecklistID	[int],
	@NewName			[nvarchar](100),
	@OldName			[nvarchar](100),
	@NewDescription		[nvarchar](1000),
	@OldDescription		[nvarchar](1000),
    @NewActive 			[bit],
    @OldActive 			[bit]
	)
AS
	BEGIN
		UPDATE 		[MaintenanceChecklist]
			SET 	[Description] = @NewDescription
			, [Active] = @NewActive
			, [Name] = @NewName
			WHERE 	[MaintenanceChecklistID] = @MaintenanceChecklistID
			AND		[Name] = @OldName
			AND     [Description] = @OldDescription
			AND		[Active] = @OldActive
		RETURN @@ROWCOUNT		
	END
GO

/* Tested by James McPherson 2/16/2018 */
print '' print '*** Creating sp_delete_maintenancechecklist_by_id'
GO
CREATE PROCEDURE [dbo].[sp_delete_maintenancechecklist_by_id]
	(
	@MaintenanceChecklistID 		[int]
	)
AS
	BEGIN
		DELETE FROM 	[MaintenanceChecklist]
		WHERE 			[MaintenanceChecklistID] = @MaintenanceChecklistID
		RETURN @@ROWCOUNT
	END
GO

/* Tested by James McPherson 2/16/2018 */
print '' print '*** Creating sp_deactivate_maintenancechecklist_by_id'
GO
CREATE PROCEDURE [dbo].[sp_deactivate_maintenancechecklist_by_id]
	(
	@MaintenanceChecklistID 		[int]
	)
AS
	BEGIN
		UPDATE 	[MaintenanceChecklist]
			SET 	[Active] = 0
			WHERE 	[MaintenanceChecklistID] = @MaintenanceChecklistID
		RETURN @@ROWCOUNT
	END
GO
/* End Zach M */

print '' print '*** Creating sp_retrieve_employeecertification_list'
GO
CREATE PROCEDURE [dbo].[sp_retrieve_employeecertification_list]
AS
	BEGIN
		SELECT	[CertificationID], [EmployeeID], [EndDate], [Active]
		FROM	[EmployeeCertification]
	END
GO

print '' print '*** Creating sp_retrieve_EmployeeRole_detail_list'
GO
CREATE PROCEDURE [dbo].[sp_retrieve_EmployeeRole_detail_list]
AS
	BEGIN
		SELECT 		[Employee].[EmployeeID],[Employee].[FirstName],[Employee].[LastName],[EmployeeRole].[RoleID]
		FROM		[Employee],[EmployeeRole]
		WHERE [Employee].[EmployeeID] = [EmployeeRole].[EmployeeID]
	END
GO

print '' print '*** Creating sp_retrieve_employee_certificationdetails_list'
GO
CREATE PROCEDURE [dbo].[sp_retrieve_employeecertificationdetails_list]
AS
		BEGIN
			SELECT Employee.EmployeeID,Employee.Email, Certification.CertificationID,
			Certification.Name, EmployeeCertification.EndDate, EmployeeCertification.Active
			FROM   Employee, EmployeeCertification, Certification
			WHERE Employee.EmployeeID = EmployeeCertification.EmployeeID
			AND EmployeeCertification.CertificationID = Certification.CertificationID
		END
GO

print '' print '*** Creating sp_edit_employeerole_by_employeeid_V2'
GO
CREATE PROCEDURE [dbo].[sp_edit_employeerole_by_employeeid_V2]
	(
	@EmployeeID				[int],
	@NewRoleID				[nvarchar](100),
	@OldRoleID				[nvarchar](100),
	@OldActive				[bit],
	@NewActive				[bit]
	)
AS
	BEGIN
		UPDATE [EmployeeRole]
			SET [RoleID] = @NewRoleID,
			 [Active] = @NewActive
			WHERE [EmployeeID] = @EmployeeID
			AND [RoleID] = @OldRoleID
			AND [Active] = @OldActive
		RETURN @@ROWCOUNT
	END
GO

print '' print '*** Creating sp_retrieve_employeerole_list_by_employeeid'
GO
CREATE PROCEDURE [dbo].[sp_retrieve_employeerole_list_by_employeeid]
	(
		@EmployeeID		[int]
	)
AS
	BEGIN
		SELECT	[Employee].[EmployeeID], [EmployeeRole].[RoleID]
				FROM	[Employee], [EmployeeRole]
				WHERE	[Employee].[EmployeeID] = @EmployeeID
				AND 	[EmployeeRole].[EmployeeID] = @EmployeeID
	END		
GO

print '' print '*** Creating sp_deactivate_employeerole'
GO
CREATE PROCEDURE [dbo].[sp_deactivate_employeerole]
	(
		@EmployeeID 	[int],
		@RoleID			[nvarchar](100)
	)
AS
	BEGIN 
		UPDATE	[dbo].[EmployeeRole]
			Set		[Active] = 0
			WHERE 	[EmployeeID] = @EmployeeID
			AND		[RoleID] = @RoleID
			AND 	[Active] = 1
		RETURN @@ROWCOUNT
	END
GO

print '' print '*** Creating sp_create_new_employeerole'
GO
CREATE PROCEDURE [dbo].[sp_create_new_employeerole]
	(
	@EmployeeID		[int],
	@RoleID			[nvarchar](100)
	)
AS
	BEGIN
		INSERT INTO [dbo].[EmployeeRole]
			([EmployeeID], [RoleID])
		VALUES
			(@EmployeeID, @RoleID)
		Return @@ROWCOUNT
	END
GO

print '' print '*** Creating sp_retrieve_corrected_employeerole_detail_list'
GO
CREATE PROCEDURE [dbo].[sp_retrieve_corrected_employeerole_detail_list]
AS
	BEGIN
		SELECT 		[Employee].[EmployeeID],[Employee].[FirstName], [Employee].[LastName]
					,[EmployeeRole].[RoleID], [EmployeeRole].[Active]
		FROM		([Employee]
		INNER JOIN 	[EmployeeRole] ON [Employee].[EmployeeID]
				= [EmployeeRole].[EmployeeID])
		WHERE [Employee].[EmployeeID] = [EmployeeRole].[EmployeeID]
	END
GO

print '' print '*** Creating sp_create_employee_without_passwordhash_return_scope_identity'
GO
CREATE PROCEDURE [dbo].[sp_create_employee_without_passwordhash_return_scope_identity]
	(
	@FirstName			[nvarchar](100),
	@LastName			[nvarchar](100),
	@Address			[nvarchar](250),
	@PhoneNumber		[nvarchar](15),
	@Email				[nvarchar](100),
	@Active				[bit]
	)
AS
	BEGIN
		INSERT INTO [dbo].[Employee]
			([FirstName], [LastName], [Address], [PhoneNumber]
			,[Email], [Active])
		VALUES
			(@FirstName, @LastName, @Address, @PhoneNumber
			,@Email, @Active)
		SELECT SCOPE_IDENTITY()	
		
	END
GO

/* Mike Mason */
print '' print '*** in file EmployeeCertificationDetail.sql ***'
USE [crlandscaping]
GO
print '' print '*** Creating sp_create_employee_without_passwordhash'
GO
CREATE PROCEDURE [dbo].[sp_create_employee_without_passwordhash]
	(
	@FirstName			[nvarchar](100),
	@LastName			[nvarchar](100),
	@Address			[nvarchar](250),
	@PhoneNumber		[nvarchar](15),
	@Email				[nvarchar](100),
	@Active				[bit]
	)
AS
	BEGIN
		INSERT INTO [dbo].[Employee]
			([FirstName], [LastName], [Address], [PhoneNumber]
			,[Email], [Active])
		VALUES
			(@FirstName, @LastName, @Address, @PhoneNumber
			,@Email, @Active)
		
	END
GO

print '' print '*** Creating sp_edit_employeecertification_by_id'
GO
CREATE PROCEDURE [dbo].[sp_edit_employee_certification_by_id]
	(
	@OldCertificationID			[int],
	@OldEmployeeID				[int],
	@OldEndDate					[datetime],
	@NewCertificationID			[int],
	@NewEmployeeID				[int],
	@NewEndDate					[datetime],
	@OldActive					[bit],
	@NewActive					[bit]
	)
AS
	BEGIN
		UPDATE [EmployeeCertification]
			SET [CertificationID] = @NewCertificationID,
				[EmployeeID] = @NewEmployeeID,
				[EndDate] = @NewEndDate,
				[Active] = @NewActive
			WHERE [CertificationID] = @OldCertificationID
            AND [EmployeeID] = @OldEmployeeID
			AND [EndDate] = @OldEndDate
			AND	[Active] = @OldActive
		RETURN @@ROWCOUNT	
	END
GO

print '' print '*** Creating sp_create_employee_certification'
GO
CREATE PROCEDURE [dbo].[sp_create_employee_certification]
	(
	@CertificationID			[int],
	@EmployeeID					[int],
	@EndDate					[datetime],
	@Active						[bit]
	)
AS
	BEGIN
		INSERT INTO [dbo].[EmployeeCertification]
			([CertificationID], [EmployeeID], [EndDate], [Active])
		VALUES
			(@CertificationID, @EmployeeID, @EndDate, @Active)
	END
GO
/* Brady Feller */


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

print '' print '*** Creating sp_get_available_employee_for_job'
GO
CREATE PROCEDURE [dbo].[sp_get_available_employee_for_job]
	(
		@JobID int
	)
AS
	BEGIN
		SELECT DISTINCT [Availability].[EmployeeID]
		INTO #AvailableEmployees
		FROM [Availability], [Job]
		WHERE CAST([Job].[DateTimeScheduled] as TIME) BETWEEN CAST([Availability].[StartTime] as TIME) AND CAST([Availability].[EndTime] as TIME)
		AND DATEPART(DW, [Job].[DateTimeScheduled]) = DATEPART(DW, [Availability].[StartTime])
		AND [Job].[JobID] = @JobID
		
		-- Delete where they have time off
		DELETE FROM #AvailableEmployees
		WHERE [EmployeeID] IN 
			(
				SELECT [TimeOff].[EmployeeID]
				FROM [TimeOff], [Job]
				WHERE [Job].[DateTimeScheduled] BETWEEN [TimeOff].[StartTime] AND [TimeOff].[EndTime]
				AND [Job].[JobID] = @JobID
			)
		
		-- Delete invalid roles
		DELETE FROM #AvailableEmployees
		WHERE [EmployeeID] IN
			(
				SELECT DISTINCT [EmployeeRole].[EmployeeID]
				FROM [EmployeeRole]
				WHERE [RoleID] NOT IN ('Worker', 'Foreman', 'Temp')
			)
		
		-- remove employees that are already scheduled around that time according to the half day increment
		DELETE FROM #AvailableEmployees
		WHERE [EmployeeID] IN
			(
			
				SELECT [TaskEmployee].[EmployeeID]
				FROM [TaskEmployee], [Job]
				WHERE [TaskEmployee].[JobID] = [Job].[JobID]
				AND (SELECT [DateTimeScheduled] FROM [Job] WHERE [JobID] = @JobID) 
					BETWEEN [Job].[DateTimeScheduled] AND DATEADD(HOUR, 12, [Job].[DateTimeScheduled])
				AND [Job].[JobID] != @JobID
			)
		
		-- get available
		SELECT [Employee].[EmployeeID], [Employee].[FirstName], [Employee].[LastName]
		FROM [Employee], #AvailableEmployees
		WHERE [Employee].[EmployeeID] = #AvailableEmployees.[EmployeeID]
		AND [Employee].[Active] = 1
		
	END
GO


print '' print '*** Creating sp_create_employee_job_post'
GO
CREATE PROCEDURE [dbo].[sp_create_employee_job_post]
	(
	@EmployeeID			[int],
	@JobID				[int]
	)
AS
	BEGIN
		INSERT INTO [dbo].[EmployeeJobPost]
			([PostEmployeeID], [JobID])
		VALUES
			(@EmployeeID, @JobID)
	END
GO

print '' print '*** Creating sp_accept_employee_job_post'
GO
CREATE PROCEDURE [dbo].[sp_employee_accept_employee_job_post]
	(
	@EmployeeJobPostID			[int],
	@EmployeeID					[int]
	)
AS
	BEGIN
		UPDATE [EmployeeJobPost]
			SET [AcceptEmployeeID] = @EmployeeID
		WHERE [EmployeeJobPostID] = @EmployeeJobPostID
	END
GO

print '' print '*** Creating sp_retreive_employee_job_post_by_employee_certification'
GO
CREATE PROCEDURE [dbo].[sp_retreive_employee_job_post_by_employee_certification]
	(
		@EmployeeID					[int]
	)
AS
	BEGIN
		
		-- get the certifications of the employee wanting to view the job board listings
		SELECT [Certification].[CertificationID]
		INTO #EmployeeCerts
		FROM [Certification], [EmployeeCertification], [Employee]
		WHERE [Certification].[CertificationID] = [EmployeeCertification].[CertificationID]
		AND [EmployeeCertification].[EmployeeID] = [Employee].[EmployeeID]
		AND [Employee].[EmployeeID] = @EmployeeID
		
		-- get all employee job posts
		SELECT [EmployeeJobPost].[EmployeeJobPostID]
		INTO #EmployeeJobPosts
		FROM [EmployeeJobPost], [Job]
		WHERE [EmployeeJobPost].[JobID] = [Job].[JobID]
		AND [Job].[Active] = 1
		AND [Job].[DateTimeScheduled] = NULL
		OR [Job].[DateTimeScheduled] > GETDATE()
		
		DELETE FROM #EmployeeJobPosts
		WHERE [EmployeeJobPostID] IN 
			(
				SELECT [EmployeeJobPostID]
				FROM [EmployeeJobPost]
				WHERE [AcceptEmployeeID] IS NOT NULL
			)
		
		
		
		
		-- dont allow them to pick a job they are already on
		DELETE FROM #EmployeeJobPosts
		WHERE [EmployeeJobPostID] IN
			(
				SELECT [EmployeeJobPost].[EmployeeJobPostID]
				FROM [EmployeeJobPost], [TaskEmployee]
				WHERE [EmployeeJobPost].[JobID] = [TaskEmployee].[JobID]
				AND [TaskEmployee].[EmployeeID] = @EmployeeID
			)
			
		SELECT [EmployeeJobPost].[EmployeeJobPostID], [EmployeeJobPost].[PostEmployeeID], [Employee].[FirstName], [Employee].[LastName],
			[JobLocation].[Street], [JobLocation].[City], [JobLocation].[State], [JobLocation].[ZipCode], [Job].[DateTimeScheduled]
		FROM [EmployeeJobPost], #EmployeeJobPosts, [Employee], [Job], [JobLocation]
		WHERE [EmployeeJobPost].[EmployeeJobPostID] = #EmployeeJobPosts.[EmployeeJobPostID]
		AND [EmployeeJobPost].[PostEmployeeID] = [Employee].[EmployeeID]
		AND [EmployeeJobPost].[JobID] = [Job].[JobID]
		AND [Job].[JobLocationID] = [JobLocation].[JobLocationID]
	END
GO


print '' print '*** Creating EmployeeJobPost after update trigger ***'
GO
CREATE TRIGGER [dbo].[TRIG_EmployeeJobPost_AFTERUPDATE]
	ON [dbo].[EmployeeJobPost]
	AFTER UPDATE
AS 
	BEGIN
		DECLARE @EmployeeID int
		
		SELECT @EmployeeID = INSERTED.[AcceptEmployeeID]
		FROM INSERTED
		
		SELECT [TaskEmployee].[TaskEmployeeID]
		INTO #TaskEmployees
		FROM [TaskEmployee], DELETED
		WHERE [TaskEmployee].[JobID] = DELETED.[JobID]
		AND [TaskEmployee].[EmployeeID] = DELETED.[PostEmployeeID]
		
		UPDATE [TaskEmployee]
			SET [EmployeeID] = @EmployeeID
		WHERE [TaskEmployeeID] IN
			(
				SELECT [TaskEmployeeID]
				FROM #TaskEmployees
			)
		
	END
GO


print '' print '*** Creating sp_retreive_job_list_by_employee'
GO
CREATE PROCEDURE [dbo].[sp_retreive_job_list_by_employee]
	(
	@EmployeeID					[int]
	)
AS
	BEGIN
		SELECT DISTINCT [Job].[JobID], [JobLocation].[Street], [JobLocation].[City], [JobLocation].[State], [JobLocation].[ZipCode], [Job].[DateTimeScheduled], [Employee].[Address]
		FROM [TaskEmployee], [Job], [JobLocation], [Employee]
		WHERE [TaskEmployee].[JobID] = [Job].[JobID]
		AND [Job].[JobLocationID] = [JobLocation].[JobLocationID]
		AND [Employee].[EmployeeID] = @EmployeeID
		AND [TaskEmployee].[EmployeeID] = @EmployeeID
		AND CONVERT(date, GETDATE()) <= CONVERT(date, [Job].[DateTimeScheduled])
		ORDER BY [Job].[DateTimeScheduled] ASC
	END
GO


print '' print '*** Creating sp_retreive_employee_id_by_email'
GO
CREATE PROCEDURE [dbo].[sp_retreive_employee_id_by_email]
	(
	@Email					[nvarchar](100)
	)
AS
	BEGIN
		SELECT [EmployeeID]
		FROM [Employee]
		WHERE [Email] = @Email
	END
GO


print '' print '*** Creating procedure sp_retrieve_employee_roles_by_active'
GO
CREATE PROCEDURE [dbo].[sp_retrieve_employee_roles_by_active]
	(
	@Active				[bit],
	@EmployeeID			[int]
	)
AS
	BEGIN
		SELECT 	[EmployeeRole].[RoleID], [Description] 
		FROM 	[EmployeeRole], [Role]
		WHERE 	[EmployeeRole].[EmployeeID] = @EmployeeID
		AND		[EmployeeRole].[RoleID] = [Role].[RoleID]
		AND		[EmployeeRole].Active = @Active
	END
GO