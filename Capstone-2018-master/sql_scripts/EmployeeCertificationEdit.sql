/* Brady Feller */
print '' print '*** in file EmployeeCertificationEdit.sql ***'
USE [crlandscaping]
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