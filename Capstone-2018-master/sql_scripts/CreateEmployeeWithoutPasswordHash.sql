/* Mike Mason */
print '' print '*** in file EmployeeCertificationDetail.sql ***'
USE [crlandscaping]
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