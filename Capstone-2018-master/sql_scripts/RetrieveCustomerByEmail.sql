print '' print '*** in file RetrieveCustomerbyEmail.sql ***'
USE [crlandscaping]
GO

print '' print '*** Creating sp_retrieve_customer_by_email'
GO
CREATE PROCEDURE [dbo].[sp_retrieve_customer_by_email]
	(
	@Email		[nvarchar](50)
	)
AS
	BEGIN
		SELECT 		[CustomerID], [CustomerTypeID], [Email], [FirstName]
					, [LastName], [PhoneNumber], [Active]
		FROM		[Customer]
		WHERE		[Email] = @Email
	END
GO