/* Mike Mason */
/* CustomerTypeID was missing from sp_edit_customer_by_id */
print '' print '*** in file EditCustomerByIDWithCustomerTypeID.sql ***'
USE [crlandscaping]
GO


print '' print '*** Creating sp_edit_customer_by_id'
GO
CREATE PROCEDURE [dbo].[sp_edit_customer_by_id_with_customerTypeID]
	(
	@NewCustomerTypeID			[nvarchar](100),
	@NewEmail					[nvarchar](100),
	@NewFirstName				[nvarchar](100),
	@NewLastName				[nvarchar](100),
	@NewPhoneNumber				[nvarchar](15),
	@OldCustomerTypeID			[nvarchar](100),
	@OldEmail					[nvarchar](100),
	@OldFirstName				[nvarchar](100),
	@OldLastName				[nvarchar](100),
	@OldPhoneNumber				[nvarchar](15),
	@CustomerID					[nvarchar](100)
	)
AS
	BEGIN
		UPDATE [Customer]
			SET		[CustomerTypeID] = @NewCustomerTypeID,
					[Email] = @NewEmail,
					[FirstName] = @NewFirstName,
					[LastName] = @NewLastName,
					[PhoneNumber] = @NewPhoneNumber
			WHERE	[CustomerID] = @CustomerID
			AND		[FirstName] = @OldFirstName
			AND		[LastName] = @OldLastName
			AND		[PhoneNumber] = @OldPhoneNumber
			AND		[Email] = @OldEmail
		RETURN @@ROWCOUNT
	END
GO