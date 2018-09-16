print '' print '*** In script job_SPROCS.sql'
print '' print '*** Using database crlandscaping'
GO
USE [crlandscaping]
GO


/* Tested by James McPherson 2/20/2018 */
print '' print '*** Creating sp_edit_paymenttype_by_id'
GO
CREATE PROCEDURE [dbo].[sp_edit_paymenttype_by_id]
	(
	@PaymentTypeID		[nvarchar](100),
	@NewDescription		[nvarchar](1000),
	@OldDescription		[nvarchar](1000)
	)
AS
	BEGIN
		UPDATE 	[PaymentType]
		SET		[Description] = @NewDescription
		WHERE	[PaymentTypeID] = @PaymentTypeID
		AND		[Description] = @OldDescription
		RETURN @@ROWCOUNT
	END
GO

/* Tested by James McPherson 2/19/2018 */
print '' print '*** Creating sp_deactivate_paymenttype_by_id'
GO
CREATE PROCEDURE [dbo].[sp_deactivate_paymenttype_by_id]
	(
	@PaymentTypeID		[nvarchar](100)
	)
AS
	BEGIN
		UPDATE [PaymentType]
			SET [Active] = 0
			WHERE [PaymentTypeID] = @PaymentTypeID
		RETURN @@ROWCOUNT
	END
GO

/* Tested by James McPherson 2/19/2018 */
print '' print '*** Creating sp_retrieve_paymenttype_by_active'
GO
CREATE PROCEDURE [dbo].[sp_retrieve_paymenttype_by_active]
	(
	@Active					[bit]
	)
AS
	BEGIN
		SELECT	[PaymentTypeID], [Description], [Active]
		FROM	[PaymentType]
		WHERE	[Active] = @Active
	END
GO

/* Jacob S */
print '' print '*** Creating sp_retrieve_serviceitem_list'
GO
CREATE PROCEDURE [dbo].[sp_retrieve_serviceitem_list]
AS
	BEGIN
		SELECT [ServiceItemID],[Name],[Description],[Active]
		FROM [ServiceItem]
	END
GO

/* Tested by James McPherson 2/20/2018 */
print '' print '*** Creating sp_edit_customer_by_id'
GO
CREATE PROCEDURE [dbo].[sp_edit_customer_by_id]
	(
	@NewEmail					[nvarchar](100),
	@NewFirstName				[nvarchar](100),
	@NewLastName				[nvarchar](100),
	@NewPhoneNumber				[nvarchar](15),
	@OldEmail					[nvarchar](100),
	@OldFirstName				[nvarchar](100),
	@OldLastName				[nvarchar](100),
	@OldPhoneNumber				[nvarchar](15),
	@CustomerID					[nvarchar](100)
	)
AS
	BEGIN
		UPDATE [Customer]
			SET		[Email] = @NewEmail,
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

/* Tested by James McPherson 2/20/2018 */
print '' print '*** Creating sp_deactivate_customer_by_id'
GO
CREATE PROCEDURE [dbo].[sp_deactivate_customer_by_id]
	(
	@CustomerID					[nvarchar](100)
	)
AS
	BEGIN
		UPDATE [Customer]
			SET 	[Active] = 0
			WHERE 	[CustomerID] = @CustomerID
			RETURN @@ROWCOUNT
	END
GO

/* Tested by James McPherson 2/20/2018 */
print '' print '*** Creating sp_delete_customer_by_id'
GO
CREATE PROCEDURE [dbo].[sp_delete_customer_by_id]
	(
	@CustomerID					[nvarchar](100)
	)
AS
	BEGIN
		DELETE FROM Customer
		WHERE [CustomerID] = @CustomerID
		RETURN @@ROWCOUNT
	END
GO

/* Tested by James McPherson 2/20/2018 */
print '' print '*** Creating sp_create_customer'
GO
CREATE PROCEDURE [dbo].[sp_create_customer]
	(
	@FirstName					[nvarchar](100),
	@LastName					[nvarchar](100),
	@PhoneNumber				[nvarchar](15),
	@Email						[nvarchar](100),
	@CustomerTypeID				[nvarchar](100)
	)
AS
	BEGIN
		INSERT INTO [dbo].[Customer]
			([Email], [FirstName], [LastName], [PhoneNumber], [CustomerTypeID])
		VALUES
			(@Email, @FirstName, @LastName, @PhoneNumber, @CustomerTypeID)
		SELECT SCOPE_IDENTITY()
	END
GO

/* Tested by James McPherson 2/20/2018 */
print '' print '*** Creating sp_retrieve_customer_by_id'
GO
CREATE PROCEDURE [dbo].[sp_retrieve_customer_by_id]
	(
	@CustomerID					[int]
	)
AS
	BEGIN
		SELECT 		[CustomerID], [CustomerTypeID], [Email], [FirstName]
					, [LastName], [PhoneNumber], [Active]
		FROM		[Customer]
		WHERE		[CustomerID] = @CustomerID
	END
GO

/* Tested by James McPherson 2/20/2018 */
print '' print '*** Creating sp_retrieve_customer_list_by_active'
GO
CREATE PROCEDURE [dbo].[sp_retrieve_customer_list_by_active]
	(
	@Active						[bit]
	)
AS
	BEGIN
		SELECT 		[CustomerID], [CustomerTypeID], [Email], [FirstName]
					, [LastName], [PhoneNumber], [Active]
		FROM		[Customer]
		WHERE		[Active] = @Active
		ORDER BY 	[CustomerID]
	END
GO

/* Tested by James McPherson 2/20/2018 */
print '' print '*** Creating sp_edit_customertype_by_id'
GO
CREATE PROCEDURE [dbo].[sp_edit_customertype_by_id]
	(
	@NewCustomerTypeID			[nvarchar](100),
	@OldCustomerTypeID			[nvarchar](100)
	)
AS
	BEGIN
		UPDATE [CustomerType]
			SET		[CustomerTypeID] = @NewCustomerTypeID
			WHERE	[CustomerTypeID] = @OldCustomerTypeID
		RETURN @@ROWCOUNT
	END
GO

/* Tested by James McPherson 2/20/2018 */
print '' print '*** Creating sp_create_customertype'
GO
CREATE PROCEDURE [dbo].[sp_create_customertype]
	(
	@CustomerTypeID				[nvarchar](100)
	)
AS
	BEGIN
		INSERT INTO [dbo].[CustomerType]
			([CustomerTypeID])
		VALUES
			(@CustomerTypeID)
	END
GO

/* Tested by James McPherson 2/20/2018 */
print '' print '*** Creating sp_delete_customertype_by_id'
GO
CREATE PROCEDURE [dbo].[sp_delete_customertype_by_id]
	(
	@CustomerTypeID				[nvarchar](100)
	)
AS
	BEGIN
		DELETE FROM CustomerType
		WHERE [CustomerTypeID] = @CustomerTypeID
		RETURN @@ROWCOUNT
	END
GO

/* Noah D */
print '' print '*** Creating sp_retrieve_servicepackage_list_by_active'
GO
CREATE PROCEDURE [dbo].[sp_retrieve_servicepackage_list_by_active]
	(
	@Active					[bit] = 1 /* Default value - James McPherson */
	)
AS
	BEGIN
		SELECT  [ServicePackageID], [Name], [Description], [Active]
		FROM  [ServicePackage]
		WHERE [Active] = @Active
	END
GO

print '' print '*** Creating sp_create_job'
GO
CREATE PROCEDURE [dbo].[sp_create_job]
	(
	@CustomerID			[int],
	@DateTimeScheduled	[DateTime],
	@EmployeeID			[int],
	@JobLocationID		[int],
	@Active				[bit],
	@DateCompleted		[DateTime],
	@Comments			[nvarchar](1000)
	)
AS
	BEGIN
		INSERT INTO [dbo].[Job]
			([CustomerID], [DateTimeScheduled], [EmployeeID], [JobLocationID]
			, [Active], [DateCompleted], [Comments])
		VALUES
			(@CustomerID, @DateTimeScheduled, @EmployeeID, @JobLocationID
			, @Active, @DateCompleted, @Comments)
		SELECT SCOPE_IDENTITY()
	END
GO

print '' print '*** Creating sp_retrieve_job_list'
GO
CREATE PROCEDURE [dbo].[sp_retrieve_job_list]
AS
	BEGIN
		SELECT	[JobID], [CustomerID], [DateTimeScheduled],
		[EmployeeID], [JobLocationID], [Active], [DateCompleted], [Comments]
		FROM	[Job]
		ORDER BY [Active] DESC
	END
GO

print '' print '*** Creating sp_retrieve_job_by_customerid'
GO
CREATE PROCEDURE [dbo].[sp_retrieve_job_by_customerid]
	(
	@CustomerID 	[int]
	)
AS
	BEGIN
		SELECT	[JobID], [DateTimeScheduled], [EmployeeID],
		[JobLocationID], [Active], [DateCompleted], [Comments]
		FROM	[Job]
		WHERE	[CustomerID] = @CustomerID
	END
GO

print '' print '*** Creating sp_retrieve_job_by_jobid'
GO
CREATE PROCEDURE [dbo].[sp_retrieve_job_by_jobid]
	(
	@JobID 	[int]
	)
AS
	BEGIN
		SELECT	[JobID], [CustomerID], [DateTimeScheduled]
		, [EmployeeID], [JobLocationID], [Active]
		, [DateCompleted], [Comments]
		FROM	[Job]
		WHERE	[JobID] = @JobID
	END
GO

print '' print '*** Creating sp_retrieve_job_by_employeeid'
GO
CREATE PROCEDURE [dbo].[sp_retrieve_job_by_employeeid]
	(
	@EmployeeID 	[int]
	)
AS
	BEGIN
		SELECT	[JobID], [CustomerID], [DateTimeScheduled]
		, [EmployeeID], [JobLocationID], [Active]
		, [DateCompleted], [Comments]
		FROM	[Job]
		WHERE	[EmployeeID] = @EmployeeID
	END
GO

print '' print '*** Creating sp_retrieve_job_by_joblocationid'
GO
CREATE PROCEDURE [dbo].[sp_retrieve_job_by_joblocationid]
	(
	@JobLocationID 	[int]
	)
AS
	BEGIN
		SELECT	[JobID], [CustomerID], [DateTimeScheduled]
		, [EmployeeID], [JobLocationID], [Active]
		, [DateCompleted], [Comments]
		FROM	[Job]
		WHERE	[JobLocationID] = @JobLocationID
	END
GO

print '' print '*** Creating sp_retrieve_inactive_job'
GO
CREATE PROCEDURE [dbo].[sp_retrieve_inactive_job]
AS
	BEGIN
		SELECT	[JobID], [CustomerID], [DateTimeScheduled]
		, [EmployeeID], [JobLocationID], [Active]
		, [DateCompleted], [Comments]
		FROM	[Job]
		WHERE	[Active] = 0
	END
GO

print '' print '*** Creating sp_edit_job_by_id'
GO
CREATE PROCEDURE [dbo].[sp_edit_job_by_id]
	(
	@NewDateTimeScheduled	[DateTime],
	@OldDateTimeScheduled	[DateTime],
	@NewDateCompleted		[DateTime],
	@OldDateCompleted		[DateTime],
	@NewComments			[nvarchar](1000),
	@OldComments			[nvarchar](1000),
	@NewEmployeeID			[int],
	@OldEmployeeID			[int],
	@JobID					[int]
	)
AS
	BEGIN
		UPDATE [Job]
			SET 	[DateTimeScheduled] = @NewDateTimeScheduled,
					[DateCompleted] = @NewDateCompleted,
					[Comments] = @NewComments,
					[EmployeeID] = @NewEmployeeID
			WHERE 	[JobID] = @JobID
			AND		[DateTimeScheduled] = @NewDateTimeScheduled
			AND 	[DateCompleted] = @OldDateCompleted
			AND		[Comments] = @OldComments
			AND		[EmployeeID] = @OldEmployeeID
		RETURN @@ROWCOUNT
	END
GO

print '' print '*** Creating sp_deactivate_job_by_id'
GO
CREATE PROCEDURE [dbo].[sp_deactivate_job_by_id]
	(
	@JobID 	[int]
	)
AS
	BEGIN
		UPDATE [Job]
			SET [Active] = 0
			WHERE [JobID] = @JobID
		RETURN @@ROWCOUNT
	END
GO

print '' print '*** Creating sp_delete_job_by_id'
GO
CREATE PROCEDURE [dbo].[sp_delete_job_by_id]
	(
	@JobID		[int]
	)
AS
	BEGIN
			DELETE
			FROM 		[Job]
			WHERE 		[JobID] = @JobID
		RETURN @@ROWCOUNT
	END
GO

/* Amanda T */
print '' print '*** Creating sp_edit_serviceitem_by_id'
GO
CREATE PROCEDURE [dbo].[sp_edit_serviceitem_by_id]
	(
	@ServiceItemID		[int],
	@NewName			[nvarchar](100),
	@OldName			[nvarchar](100),
	@NewDescription		[nvarchar](100),
	@OldDescription		[nvarchar](100),
	@NewActive			[bit],
	@OldActive			[bit]
	)
AS
	BEGIN
		UPDATE [ServiceItem]
			SET 	[Name] = @NewName,
					[Description] = @NewDescription,
					[Active] = @NewActive
			WHERE 	[ServiceItemID] = @ServiceItemID
			  AND	[Name] = @OldName
			  AND 	[Description] = @OldDescription
			  AND 	[Active] = @OldActive
		RETURN
	END
GO

print '' print '*** Creating sp_deactivate_serviceitem_by_id'
GO
CREATE PROCEDURE [dbo].[sp_deactivate_serviceitem_by_id]
	(
	@ServiceItemID 	[int]
	)
AS
	BEGIN
		UPDATE [ServiceItem]
			SET [Active] = 0
			WHERE [ServiceItemID] = @ServiceItemID
		RETURN @@ROWCOUNT
	END
GO

print '' print '*** Creating sp_deactivate_service_package_by_id'
GO
CREATE PROCEDURE [dbo].[sp_deactivate_service_package_by_id]
	(
	@ServicePackageID			[int]
	)
AS
	BEGIN
		UPDATE [dbo].[ServicePackage]
			SET 	[Active] = 0
			WHERE 	[ServicePackageID] = @ServicePackageID
		RETURN @@ROWCOUNT
	END
GO

print '' print '*** Creating sp_create_serviceoffering'
GO
CREATE PROCEDURE [dbo].[sp_create_serviceoffering]
	(
	@Name				[nvarchar](100),
	@Description		[nvarchar](1000)
	)
AS
	BEGIN
		INSERT INTO [dbo].[ServiceOffering]
			([Name], [Description])
		VALUES
			(@Name, @Description)
		SELECT SCOPE_IDENTITY()
	END
GO

/* Tested by James McPherson 2/23/2018 */
print '' print '*** Creating sp_create_servicepackage'
GO
CREATE PROCEDURE [dbo].[sp_create_servicepackage]
	(
	@Name			[nvarchar](100),
	@Description	[nvarchar](1000),
	@Active			[bit]
	)
AS
	BEGIN
		INSERT INTO [dbo].[ServicePackage]
			([Name], [Description], [Active])
		VALUES
			(@Name, @Description, @Active)
		SELECT SCOPE_IDENTITY()
	END
GO

print '' print '*** Creating sp_create_joblocation'
GO
CREATE PROCEDURE [dbo].[sp_create_joblocation]
	(
	@CustomerID		[int],
	@Street			[nvarchar](100),
	@City			[nvarchar](100),
	@State			[nvarchar](2),
	@ZipCode		[nvarchar](15),
	@Comments		[nvarchar](1000)
	)
AS
	BEGIN
		INSERT INTO [dbo].[JobLocation]
			([CustomerID], [Street],[City],[State],[ZipCode], [Comments])
		VALUES
			(@CustomerID, @Street, @City, @State, @ZipCode, @Comments)
		SELECT SCOPE_IDENTITY()
	END
GO


print '' print '*** Creating sp_retrieve_task_by_serviceid'
GO
CREATE PROCEDURE [dbo].[sp_retrieve_task_by_serviceid]
	(
	@ServiceItemID 	[int]
	)
AS
	BEGIN
		SELECT	[TaskID], [TaskTypeID], [ServiceItemID], [Name], [Description], [Active]
		FROM	[Task]
		WHERE	[Task].[ServiceItemID] = @ServiceItemID
	END
GO

print '' print '*** Creating sp_retrieve_task_by_name'
GO
CREATE PROCEDURE [dbo].[sp_retrieve_task_by_name]
	(
	@Name	[nvarchar](100)
	)
AS
	BEGIN
		SELECT	[TaskID], [TaskTypeID], [ServiceItemID], [Name], [Description], [Active]
		FROM	[Task]
		WHERE	[Task].[Name] = @Name
	END
GO

print '' print '*** Creating sp_edit_task_by_id'
GO
CREATE PROCEDURE [dbo].[sp_edit_task_by_id]
	(
	@TaskID				[int],
	@NewTaskTypeID		[int],
	@NewServiceItemID	[int],
	@NewName			[nvarchar](100),
	@NewDescription		[nvarchar](1000),
	@NewActive			[bit],
	@OldServiceItemID	[int],
	@OldTaskTypeID		[int],
	@OldName			[nvarchar](100),
	@OldDescription		[nvarchar](1000),
	@OldActive			[bit]
	)
AS
	BEGIN
		UPDATE [Task]
			SET 	[TaskTypeID] = @NewTaskTypeID,
					[ServiceItemID] = @NewServiceItemID,
					[Name] = @NewName,
					[Description] = @NewDescription,
					[Active] = @NewActive
			WHERE 	[TaskID] = @TaskID
			  AND	[TaskTypeID] = @OldTaskTypeID
			  AND	[ServiceItemID] = @OldServiceItemID
			  AND	[Name] = @OldName
			  AND 	[Description] = @OldDescription
			  AND	[Active] = @OldActive
		RETURN @@ROWCOUNT
	END
GO

/* Created 2018/03/05 - John Miller */
print '' print '*** Creating sp_deactivate_task_by_id'
GO
CREATE PROCEDURE [dbo].[sp_deactivate_task_by_id]
	(
	@TaskID		[int]
	)
AS
	BEGIN
		UPDATE [Task]
		SET [Active] = 0
		WHERE [TaskID] = @TaskID
		RETURN @@ROWCOUNT
	END
GO

print '' print '*** Creating sp_delete_task_by_id'
GO
CREATE PROCEDURE [dbo].[sp_delete_task_by_id]
	(
	@TaskID		[int]
	)
AS
	BEGIN
		DELETE FROM 	[Task]
		WHERE 			[TaskID] = @TaskID
		RETURN @@ROWCOUNT
	END
GO

print '' print '*** Creating sp_create_task'
GO
CREATE PROCEDURE [dbo].[sp_create_task]
	(
	@TaskTypeID		[int],
	@ServiceItemID	[int],
	@Name			[nvarchar](100),
	@Description	[nvarchar](1000)
	)
AS
	BEGIN
		INSERT INTO [dbo].[Task]
			([TaskTypeID], [ServiceItemID], [Name], [Description])
		VALUES
			(@TaskTypeID, @ServiceItemID, @Name, @Description)
		SELECT SCOPE_IDENTITY()
	END
GO

print '' print '*** Creating sp_retrieve_task_by_id'
GO
CREATE PROCEDURE [dbo].[sp_retrieve_task_by_id]
	(
	@TaskID 	[int]
	)
AS
	BEGIN
		SELECT	[TaskID], [TaskTypeID], [ServiceItemID], [Name], [Description], [Active]
		FROM	[Task]
		WHERE	[Task].[TaskID] = @TaskID
	END
GO

/* Created 2018/03/05 - John Miller */
print '' print '*** Creating sp_retrieve_task_list'
GO
CREATE PROCEDURE [dbo].[sp_retrieve_task_list]
AS
	BEGIN
		SELECT	[TaskID], [TaskTypeID], [ServiceItemID], [Name], [Description], [Active]
		FROM	[Task]
	END
GO


print '' print '*** Creating sp_retrieve_joblocation_by_id'
GO
CREATE PROCEDURE [dbo].[sp_retrieve_joblocation_by_id]
	(
	@JobLocationID	[int]
	)
AS
	BEGIN
		SELECT	[JobLocationID], [CustomerID], [Street], [City], [State], [ZipCode], [Comments], [Active]
		FROM	[JobLocation]
		WHERE	[JobLocation].[JobLocationID] = @JobLocationID
	END
GO

print '' print '*** Creating sp_retrieve_joblocation_by_customer_id'
GO
CREATE PROCEDURE [dbo].[sp_retrieve_joblocation_by_customer_id]
	(
	@CustomerID	[int]
	)
AS
	BEGIN
		SELECT	[JobLocationID], [CustomerID], [Street], [City], [State], [ZipCode], [Active], [Comments]
		FROM	[JobLocation]
		WHERE	[JobLocation].[CustomerID] = @CustomerID
	END
GO

print '' print '*** Creating sp_retrieve_servicepackage_by_name'
GO
CREATE PROCEDURE [dbo].[sp_retrieve_servicepackage_by_name]
	(
	@Name	[nvarchar](100)
	)
AS
	BEGIN
		SELECT	[ServicePackageID], [Name], [Description], [Active]
		FROM	[ServicePackage]
		WHERE	[ServicePackage].[Name] = @Name
	END
GO

print '' print '*** Creating sp_retrieve_servicepackage_by_serviceitemid'
GO
CREATE PROCEDURE [dbo].[sp_retrieve_servicepackage_by_serviceitemid]
	(
	@ServiceItemID	[int]
	)
AS
	BEGIN
		SELECT DISTINCT	[ServicePackage].[Name], [ServicePackage].[Description], [ServicePackage].[Active]
		FROM	[ServicePackage], [ServiceItem]
		WHERE	[ServiceItem].[ServiceItemID] = @ServiceItemID
	END
GO

print '' print '*** Creating sp_retrieve_servicepackage_by_id'
GO
CREATE PROCEDURE [dbo].[sp_retrieve_servicepackage_by_id]
	(
	@ServicePackageID	[int]
	)
AS
	BEGIN
		SELECT	[ServicePackageID], [Name], [Description], [Active]
		FROM	[ServicePackage]
		WHERE	[ServicePackage].[ServicePackageID] = @ServicePackageID
	END
GO

print '' print '*** Creating sp_retrieve_serviceofferings'
GO
CREATE PROCEDURE [dbo].[sp_retrieve_serviceofferings]
AS
	BEGIN
		SELECT	[ServiceOfferingID], [Name], [Description]
		FROM	[ServiceOffering]
	END
GO

print '' print '*** Creating sp_retrieve_serviceoffering_by_id'
GO
CREATE PROCEDURE [dbo].[sp_retrieve_serviceoffering_by_id]
	(
	@ServiceOfferingID	[int]
	)
AS
	BEGIN
		SELECT	[ServiceOfferingID],[Name], [Description]
		FROM	[ServiceOffering]
		WHERE	[ServiceOffering].[ServiceOfferingID] = @ServiceOfferingID
	END
GO

print '' print '*** Creating sp_retrieve_serviceoffering_by_name'
GO
CREATE PROCEDURE [dbo].[sp_retrieve_serviceoffering_by_name]
	(
	@Name	[nvarchar](100)
	)
AS
	BEGIN
		SELECT	[ServiceOfferingID], [Name], [Description]
		FROM	[ServiceOffering]
		WHERE	[ServiceOffering].[Name] = @Name
	END
GO

print '' print '*** Creating sp_edit_serviceoffering_by_id'
GO
CREATE PROCEDURE [dbo].[sp_edit_serviceoffering_by_id]
	(
	@NewName			[nvarchar](50),
	@OldName			[nvarchar](50),
	@NewDescription		[nvarchar](100),
	@OldDescription		[nvarchar](100),
	@ServiceOfferingID	[int]
	)
AS
	BEGIN
		UPDATE [ServiceOffering]
			SET 	[Name] = @NewName,
					[Description] = @NewDescription
			WHERE 	[ServiceOfferingID] = @ServiceOfferingID
			  AND	[Name] = @OldName
			  AND 	[Description] = @OldDescription
		RETURN
	END
GO

/* Tested by James McPherson 2/23/2018 */
print '' print '*** Creating sp_edit_servicepackage_by_id'
GO
CREATE PROCEDURE [dbo].[sp_edit_servicepackage_by_id]
	(
	@NewName			[nvarchar](50),
	@OldName			[nvarchar](50),
	@NewDescription		[nvarchar](100),
	@OldDescription		[nvarchar](100),
	@NewActive			[bit],
	@OldActive			[bit],
	@ServicePackageID	[int]
	)
AS
	BEGIN
		UPDATE [ServicePackage]
			SET 	[Name] = @NewName
					, [Description] = @NewDescription
					, [Active] = @NewActive
			WHERE 	[ServicePackageID] = @ServicePackageID
			  AND	[Name] = @OldName
			  AND 	[Description] = @OldDescription
			  AND	[Active] = @OldActive
		RETURN @@ROWCOUNT
	END
GO

print '' print '*** Creating sp_edit_joblocation_by_id'
GO
CREATE PROCEDURE [dbo].[sp_edit_joblocation_by_id]
	(
	@NewStreet			[nvarchar](100),
	@OldStreet			[nvarchar](100),
	@NewCity			[nvarchar](100),
	@OldCity			[nvarchar](100),
	@NewState			[nvarchar](2),
	@OldState			[nvarchar](2),
	@NewZipCode			[nvarchar](15),
	@OldZipCode			[nvarchar](15),
	@NewComments		[nvarchar](1000),
	@OldComments		[nvarchar](1000),
	@NewActive			[bit],
	@OldActive			[bit],
	@JobLocationID		[int]
	)
AS
	BEGIN
		UPDATE [JobLocation]
			SET 	[Street] = @NewStreet,
					[City] = @NewCity,
					[State] = @NewState,
					[ZipCode] = @NewZipCode,
					[Comments] = @NewComments,
					[Active] = @NewActive
			WHERE 	[JobLocationID] = @JobLocationID
			  AND	[Street] = @OldStreet
			  AND 	[City] = @OldCity
			  AND 	[State] = @OldState
			  AND 	[ZipCode] = @OldZipCode
			  AND 	[Comments] = @OldComments
			  AND 	[Active] = @OldActive
		RETURN @@ROWCOUNT
	END
GO


print '' print '*** Creating sp_delete_joblocation_by_id'
GO
CREATE PROCEDURE [dbo].[sp_delete_joblocation_by_id]
	(
	@JobLocationID		[int]
	)
AS
	BEGIN
		DELETE FROM 	[JobLocation]
		WHERE 			[JobLocationID] = @JobLocationID
		RETURN @@ROWCOUNT
	END
GO

print '' print '*** Creating sp_delete_serviceoffering_by_id'
GO
CREATE PROCEDURE [dbo].[sp_delete_serviceoffering_by_id]
	(
	@ServiceOfferingID		[int]
	)
AS
	BEGIN
		DELETE FROM 		[ServiceOffering]
		WHERE 		[ServiceOfferingID] = @ServiceOfferingID
		RETURN @@ROWCOUNT
	END
GO

print '' print '*** Creating sp_delete_servicepackage_by_id'
GO
CREATE PROCEDURE [dbo].[sp_delete_servicepackage_by_id]
	(
	@ServicePackageID		[int]
	)
AS
	BEGIN
		DELETE FROM 	[ServicePackage]
		WHERE 			[ServicePackageID] = @ServicePackageID
		RETURN @@ROWCOUNT
	END
GO

print '' print '*** Creating sp_create_serviceitem'
GO
CREATE PROCEDURE [dbo].[sp_create_serviceitem]
	(
	@Name					[nvarchar](100),
	@Description			[nvarchar](1000),
	@Active					[bit]
	)
AS
	BEGIN
		INSERT INTO [dbo].[ServiceItem]
			([Name], [Description], [Active])
		VALUES
			(@Name, @Description, @Active)
		SELECT SCOPE_IDENTITY()
	END
GO
/* End Dan C */

/* John M */
print '' print '*** Creating sp_delete_tasktype_by_id'
GO
CREATE PROCEDURE [dbo].[sp_delete_tasktype_by_id]
	(
	@TaskTypeID		[int]
	)
AS
	BEGIN
		DELETE FROM 	[TaskType]
		WHERE 			[TaskTypeID] = @TaskTypeID
		RETURN @@ROWCOUNT
	END
GO

print '' print '*** Creating sp_deactivate_tasktype_by_id'
GO
CREATE PROCEDURE [dbo].[sp_deactivate_tasktype_by_id]
	(
	@TaskTypeID		[int]
	)
AS
	BEGIN
		UPDATE [TaskType]
			SET [Active] = 0
			WHERE [TaskTypeID] = @TaskTypeID
		RETURN @@ROWCOUNT
	END
GO

print '' print '*** Creating sp_edit_tasktype_by_id'
GO
CREATE PROCEDURE [dbo].[sp_edit_tasktype_by_id]
	(
	@TaskTypeID						[int],
	@NewName						[nvarchar](100),
	@NewQuantity					[int],
	@NewJobLocationAttributeTypeID	[nvarchar](100),	
	@NewActive						[bit],
	
	@OldName						[nvarchar](100),
	@OldQuantity					[int],
	@OldJobLocationAttributeTypeID	[nvarchar](100),
	@OldActive						[bit]
	)
AS
	BEGIN
		UPDATE [TaskType]
			SET 	[Name] = @NewName,
					[Quantity] = @NewQuantity,
					[JobLocationAttributeTypeID] = @NewJobLocationAttributeTypeID,
					[Active] = @NewActive
					
			WHERE 	[TaskTypeID] = @TaskTypeID
			  AND	[Name] = @OldName
			  AND	[Quantity] = @OldQuantity
			  AND	[JobLocationAttributeTypeID] = @OldJobLocationAttributeTypeID
			  AND	[Active] = @OldActive
		RETURN @@ROWCOUNT
	END
GO

print '' print '*** Creating sp_retrieve_tasktype_by_joblocationattributetypeid'
GO
CREATE PROCEDURE [dbo].[sp_retrieve_tasktype_by_joblocationattributetypeid]
	(
	@JobLocationAttributeTypeID 	[nvarchar](100)
	)
AS
	BEGIN
		SELECT	[TaskTypeID], [Name], [Quantity], [JobLocationAttributeTypeID], [Active]
		FROM	[TaskType]
		WHERE	[TaskType].[JobLocationAttributeTypeID] = @JobLocationAttributeTypeID
	END
GO

print '' print '*** Creating sp_retrieve_tasktype_by_active'
GO
CREATE PROCEDURE [dbo].[sp_retrieve_tasktype_by_active]
AS
	BEGIN
		SELECT	[TaskTypeID], [Name], [Quantity], [JobLocationAttributeTypeID], [Active]
		FROM	[TaskType]
		WHERE	[TaskType].[Active] = 1
	END
GO

print '' print '*** Creating sp_retrieve_tasktype_by_name'
GO
CREATE PROCEDURE [dbo].[sp_retrieve_tasktype_by_name]
	(
	@Name	[nvarchar](100)
	)
AS
	BEGIN
		SELECT	[TaskTypeID], [Name], [Quantity], [JobLocationAttributeTypeID], [Active]
		FROM	[TaskType]
		WHERE	[TaskType].[Name] = @Name
	END
GO

print '' print '*** Creating sp_retrieve_tasktype_by_id'
GO
CREATE PROCEDURE [dbo].[sp_retrieve_tasktype_by_id]
	(
	@TaskTypeID [int]
	)
AS
	BEGIN
		SELECT	[TaskTypeID], [Name], [Quantity], [JobLocationAttributeTypeID], [Active]
		FROM	[TaskType]
		WHERE	[TaskType].[TaskTypeID] = @TaskTypeID
	END
GO

print '' print '*** Creating sp_retrieve_tasktype_list'
GO
CREATE PROCEDURE [dbo].[sp_retrieve_tasktype_list]
AS
	BEGIN
		SELECT	[TaskTypeID], [Name], [Quantity], [JobLocationAttributeTypeID], [Active]
		FROM	[TaskType]
	END
GO

print '' print '*** Creating sp_create_tasktype'
GO
CREATE PROCEDURE [dbo].[sp_create_tasktype]
	(
	@Name						[nvarchar](100),
	@Quantity					[int],
	@JobLocationAttributeTypeID [nvarchar](100)	
	)
AS
	BEGIN
		INSERT INTO [dbo].[TaskType]
			([Name], [Quantity], [JobLocationAttributeTypeID])
		VALUES
			(@Name, @Quantity, @JobLocationAttributeTypeID)
		Return SCOPE_IDENTITY()
	END
GO

print '' print '*** Creating sp_retrieve_customertype_list'
GO
CREATE PROCEDURE [dbo].[sp_retrieve_customertype_list]
AS
	BEGIN
		SELECT 		[CustomerTypeID]
		FROM		[CustomerType]
	END
GO

/* Tested by James McPherson 2/23/2018 */
print '' print '*** Creating sp_retrieve_servicepackage_list'
GO
CREATE PROCEDURE [dbo].[sp_retrieve_servicepackage_list]
AS
	BEGIN
		SELECT	[ServicePackageID], [Name], [Description], [Active]
		FROM	[ServicePackage]
	END
GO

/* Mike M */
print '' print '*** Creating sp_retrieve_customer_by_active'
GO
CREATE PROCEDURE [dbo].[sp_retrieve_customer_by_active]
	(
	@Active						[bit]
	)
AS
	BEGIN
		SELECT 		[CustomerID], [CustomerTypeID], [Email],[FirstName],[LastName], [PhoneNumber], [PasswordHash], [Active]
		FROM		[Customer]
		WHERE		[Active] = @Active
		ORDER BY 	[CustomerID]
	END
GO


/* Zach Hall */

print '' print '*** Creating sp_retrieve_servicepackage_list_by_jobid'
GO
CREATE PROCEDURE [dbo].[sp_retrieve_servicepackage_list_by_jobid]
	(
		@JobID	[int]
	)
AS
	BEGIN
		SELECT	[ServicePackage].[ServicePackageID], [ServicePackage].[Name], [ServicePackage].[Description], [ServicePackage].[Active]
		FROM	[ServicePackage]
		INNER JOIN [JobServicePackage] ON [ServicePackage].[ServicePackageID] = [JobServicePackage].[ServicePackageID]
		WHERE [JobServicePackage].[JobID] = @JobID
	END
GO

print '' print '*** Creating sp_retrieve_joblocationattribute_list_by_joblocationid'
GO
CREATE PROCEDURE [dbo].[sp_retrieve_joblocationattribute_list_by_joblocationid]
	(
		@JobLocationID	[int]
	)
AS
	BEGIN
		SELECT [JobLocationID], [JobLocationAttributeTypeID], [Value]
		FROM [JobLocationAttribute]
		WHERE [JobLocationID] = @JobLocationID
	END
GO

print '' print '*** Creating sp_retrieve_customer_list'
GO
CREATE PROCEDURE [dbo].[sp_retrieve_customer_list]
AS
	BEGIN
		SELECT 		[CustomerID], [CustomerTypeID], [Email], [FirstName]
					, [LastName], [PhoneNumber], [Active]
		FROM		[Customer]
		ORDER BY 	[LastName]
	END
GO

print '' print '*** Creating sp_retrieve_joblocationattributetypeid_list_by_servicepackage_id'
GO
CREATE PROCEDURE [dbo].[sp_retrieve_joblocationattributetypeid_list_by_servicepackage_id]
	(
		@ServicePackageID	[int]
	)
AS
	BEGIN
		SELECT 		DISTINCT [JobLocationAttributeType].[JobLocationAttributeTypeID]
		FROM		[JobLocationAttributeType]
		
	END
GO

print '' print '*** Creating sp_retrieve_joblocation_detail_by_id'
GO
CREATE PROCEDURE [dbo].[sp_retrieve_joblocation_detail_by_id]
	(
		@JobLocationID	[int]
	)
AS
	BEGIN
		SELECT [JobLocationID], [CustomerID], [Street], [City], [State], [ZipCode], [Comments], [Active]
		FROM	[JobLocation]
		WHERE	[JobLocation].[JobLocationID] = @JobLocationID
		
		SELECT [JobLocationID], [JobLocationAttributeTypeID], [Value]
		FROM [JobLocationAttribute]
		WHERE [JobLocationID] = @JobLocationID
	END
GO

print '' print '*** Creating sp_create_edit_jobservicepackage_updated'
GO
CREATE PROCEDURE [dbo].[sp_create_edit_jobservicepackage_updated]
	(
		@tvpServicePackages        [JobServicePackageType] READONLY
	)
AS
	BEGIN
		MERGE INTO [dbo].[JobServicePackage] AS [Target]
			USING @tvpServicePackages AS [Source]
			ON [Target].[JobID] = [Source].[JobID] AND [Target].[ServicePackageID] = [Source].[ServicePackageID]
		WHEN NOT MATCHED BY TARGET THEN 
			INSERT ([JobID], [ServicePackageID])
			VALUES ([Source].[JobID], [Source].[ServicePackageID]);
	END
GO

print '' print '*** Creating sp_create_edit_jobservicepackage'
GO
CREATE PROCEDURE [dbo].[sp_create_edit_jobservicepackage]
	(
		@tvpServicePackages        [JobServicePackageType] READONLY
	)
AS
	BEGIN
		MERGE INTO [dbo].[JobServicePackage] AS [Target]
			USING @tvpServicePackages AS [Source]
			ON [Target].[JobID] = [Source].[JobID] AND [Target].[ServicePackageID] = [Source].[ServicePackageID]
		WHEN NOT MATCHED BY TARGET THEN 
			INSERT ([JobID], [ServicePackageID])
			VALUES ([Source].[JobID], [Source].[ServicePackageID])
		WHEN NOT MATCHED BY SOURCE THEN 
			DELETE;
	END
GO

print '' print '*** Creating sp_update_job'
GO
CREATE PROCEDURE [dbo].[sp_update_job]
	(
	@JobID 				[int],
	@OldCustomerID		[int],
	@NewCustomerID		[int],
	@OldEmployeeID		[int],
	@NewEmployeeID		[int],
	@OldJobLocationID	[int],
	@NewJobLocationID	[int],
	@OldActive			[bit],
	@NewActive			[bit],
	@OldDateCompleted	[DateTime] = null,
	@NewDateCompleted	[DateTime] = null,
	@OldDateScheduled	[DateTime],
	@NewDateScheduled	[DateTime],
	@OldComments		[nvarchar](1000),
	@NewComments		[nvarchar](1000)
	)
AS
	BEGIN
		UPDATE 	[Job]
			SET 	[CustomerID] = @NewCustomerID,
					[EmployeeID] = @NewEmployeeID,
					[JobLocationID] = @NewJobLocationID,
					[Active] = @NewActive,
					[DateCompleted] = @NewDateCompleted,
					[DateTimeScheduled] = @NewDateScheduled,
					[Comments] = @NewComments
			WHERE 	[JobID] = @JobID
			AND [CustomerID] = @OldCustomerID
			AND [EmployeeID] = @OldEmployeeID
			AND [JobLocationID] = @OldJobLocationID
			AND [Active] = @OldActive
			AND [DateTimeScheduled] = @OldDateScheduled
			AND [Comments] = @OldComments
		RETURN @@ROWCOUNT
	END
GO

print '' print '*** Creating sp_retrieve_job_detail_list'
GO
CREATE PROCEDURE [dbo].[sp_retrieve_job_detail_list]
AS
	BEGIN
		SELECT [Job].[JobID], [Job].[CustomerID], [Job].[DateTimeScheduled], [Job].[EmployeeID], 
				[Job].[JobLocationID], [Job].[Active], [Job].[DateCompleted], [Job].[Comments],
				[Customer].[CustomerTypeID], [Customer].[Email], [Customer].[FirstName], [Customer].[LastName],
				[Customer].[PhoneNumber], [Customer].[Active],
				[JobLocation].[JobLocationID], [JobLocation].[Street], [JobLocation].[City], [JobLocation].[State], 
				[JobLocation].[ZipCode], [JobLocation].[Comments], [JobLocation].[Active]
		FROM [Job]
		INNER JOIN [Customer] ON [Job].[CustomerID] = [Customer].[CustomerID]
		INNER JOIN [JobLocation] ON [JobLocation].[JobLocationID] = [Job].[JobLocationID]
		
		SELECT [JobLocationAttribute].[JobLocationID], [JobLocationAttribute].[JobLocationAttributeTypeID], [JobLocationAttribute].[Value]
		FROM [JobLocationAttribute]
		
		SELECT [JobServicePackage].[JobID], [JobServicePackage].[ServicePackageID],  [ServicePackage].[Name],  [ServicePackage].[Description],  [ServicePackage].[Active]
		FROM [JobServicePackage]
		INNER JOIN [ServicePackage] ON [JobServicePackage].[ServicePackageID] = [ServicePackage].[ServicePackageID]
	END
GO

print '' print '*** Creating sp_create_edit_joblocationattribute'
GO
CREATE PROCEDURE [dbo].[sp_create_edit_joblocationattribute]
	(
		@tvpJobLocationAttributes	[JobLocationAttributeTableType] READONLY
	)
AS
	BEGIN
		MERGE INTO [dbo].[JobLocationAttribute] AS [Target]
			USING @tvpJobLocationAttributes AS [Source]
			ON [Target].[JobLocationID] = [Source].[JobLocationID] AND [Target].[JobLocationAttributeTypeID] = [Source].[JobLocationAttributeTypeID]
		WHEN NOT MATCHED BY TARGET THEN 
			INSERT ([JobLocationID], [JobLocationAttributeTypeID], [Value])
			VALUES ([Source].[JobLocationID], [Source].[JobLocationAttributeTypeID], [Source].[Value])
		WHEN MATCHED THEN 
			UPDATE SET [Value] = [Source].[Value];
	END
GO

print '' print '*** Creating sp_retrieve_joblocation_list_by_customer_id'
GO
CREATE PROCEDURE [dbo].[sp_retrieve_joblocation_list_by_customer_id]
	(
	@CustomerID	[int]
	)
AS
	BEGIN
		SELECT	[JobLocationID], [CustomerID], [Street], [City], [State], [ZipCode], [Comments], [Active]
		FROM	[JobLocation]
		WHERE	[JobLocation].[CustomerID] = @CustomerID
	END
GO

print '' print '*** Creating sp_retrieve_joblocation_detail_list'
GO
CREATE PROCEDURE [dbo].[sp_retrieve_joblocation_detail_list]
AS
	BEGIN
		SELECT [JobLocation].[JobLocationID], [JobLocation].[Street], [JobLocation].[City], [JobLocation].[State], 
				[JobLocation].[ZipCode], [JobLocation].[Comments], [JobLocation].[Active],
				[JobLocationAttribute].[JobLocationAttributeTypeID], [JobLocationAttribute].[Value],
				[Customer].[CustomerID], [Customer].[CustomerTypeID], [Customer].[Email], [Customer].[FirstName], [Customer].[LastName],
				[Customer].[PhoneNumber], [Customer].[Active]
		FROM [JobLocation]
		INNER JOIN [Customer] ON [JobLocation].[CustomerID] = [Customer].[CustomerID]
		LEFT JOIN [JobLocationAttribute] ON [JobLocation].[JobLocationID] = [JobLocationAttribute].[JobLocationID]
	END
GO

print '' print '*** Creating sp_deactivate_joblocation_by_id'
GO
CREATE PROCEDURE [dbo].[sp_deactivate_joblocation_by_id]
	(
	@JobLocationID		[int]
	)
AS
	BEGIN
		UPDATE [JobLocation]
			SET [Active] = 0
			WHERE [JobLocationID] = @JobLocationID
		RETURN @@ROWCOUNT
	END
GO

print '' print '*** Creating sp_retrieve_joblocationattributetype_list'
GO
CREATE PROCEDURE [dbo].[sp_retrieve_joblocationattributetype_list]
AS
	BEGIN
		SELECT 		[JobLocationAttributeTypeID]
		FROM		[JobLocationAttributeType]
	END
GO
/* End Zach Hall */


print '' print '*** Creating sp_create_tasktypesupply'
GO
CREATE PROCEDURE [dbo].[sp_create_tasktypesupply]
	(
	@TaskTypeID					[int],
	@SupplyItemID			[int],
	@Quantity				[int]
	)
AS
	BEGIN
		INSERT INTO [dbo].[TaskTypeSupplyNeed]
			([TaskTypeID],[SupplyItemID], [Quantity])
		VALUES
			(@TaskTypeID, @SupplyItemID, @Quantity)
		RETURN @@ROWCOUNT
	END
GO	

print '' print '*** Creating sp_retrieve_tasktypesupplies'
GO
CREATE PROCEDURE [dbo].[sp_retrieve_tasktypesupplies]
AS
	BEGIN
		SELECT	[TaskTypeID],[SupplyItemID],[Quantity]
		FROM	[TaskTypeSupplyNeed]
	END
GO

print '' print '*** Creating sp_retrieve_tasktypesupplies_by_task_and_supplyitemid'
GO
CREATE PROCEDURE [dbo].[sp_retrieve_tasktypesupplies_by_task_and_supplyitemid]
	(
	@TaskTypeID					[int],
	@SupplyItemID			[int]
	)
AS
	BEGIN
		SELECT	[TaskTypeID],[SupplyItemID],[Quantity]
		FROM	[TaskTypeSupplyNeed]
		WHERE	[TaskTypeID] = @TaskTypeID
		AND 	[SupplyItemID] = @SupplyItemID
	END
GO

print '' print '*** Creating sp_edit_tasktypesupply_by_task_and_supplyitemid'
GO
CREATE PROCEDURE [dbo].[sp_edit_tasktypesupply_by_task_and_supplyitemid]
	(
	@TaskTypeID				[int],
	@SupplyItemID		[int],
	@OldQuantity		[int],
	@NewQuantity		[int]
	)
AS
	BEGIN
		UPDATE	[TaskTypeSupplyNeed]
			SET		[Quantity] = @NewQuantity
			WHERE	[TaskTypeID] = @TaskTypeID
			AND 	[SupplyItemID] = @SupplyItemID
			AND		[Quantity] = @OldQuantity
		RETURN @@ROWCOUNT
	END
GO

print '' print '*** Creating sp_delete_tasktypesupply'
GO
CREATE PROCEDURE [dbo].[sp_delete_tasktypesupply]
	(
	@TaskTypeID			[int],
	@SupplyItemID		[int]
	)
AS
	BEGIN
		DELETE FROM	[TaskTypeSupplyNeed]
			WHERE	[TaskTypeID] = @TaskTypeID
			AND 	[SupplyItemID] = @SupplyItemID
		RETURN @@ROWCOUNT
	END
GO

print '' print '*** Creating sp_retrieve_job_location_attribute_type_by_id'
GO
CREATE PROCEDURE [dbo].[sp_retrieve_job_location_attribute_type_by_id]
	(
	@JobLocationAttributeTypeID	[nvarchar](100)
	)
AS
	BEGIN
		SELECT	[JobLocationAttributeTypeID]
		FROM	[JobLocationAttributeType]
		WHERE	[JobLocationAttributeTypeID] = @JobLocationAttributeTypeID
	END
GO

print '' print '*** Creating sp_edit_job_location_attribute_type'
GO
CREATE PROCEDURE [dbo].[sp_edit_job_location_attribute_type]
	(
	@NewJobLocationAttributeTypeID		 [nvarchar](100),
	@OldJobLocationAttributeTypeID		 [nvarchar](100)
	)
AS
	BEGIN
		UPDATE [JobLocationAttributeType]
			SET [JobLocationAttributeTypeID] = @NewJobLocationAttributeTypeID
			WHERE [JobLocationAttributeTypeID] = @OldJobLocationAttributeTypeID
		SELECT SCOPE_IDENTITY()	
	END
GO

print '' print '*** Creating sp_create_job_location_attribute_type'
GO
CREATE PROCEDURE [dbo].[sp_create_job_location_attribute_type]
	(
	@JobLocationAttributeTypeID		[nvarchar](100)
	)
AS
	BEGIN
		INSERT INTO	[dbo].[JobLocationAttributeType]
			([JobLocationAttributeTypeID])
		VALUES
			(@JobLocationAttributeTypeID)
		SELECT SCOPE_IDENTITY()
	END
GO

print '' print '*** Creating sp_retrieve_job_location_attribute_type_list'
GO
CREATE PROCEDURE [dbo].[sp_retrieve_job_location_attribute_type_list]
AS
	BEGIN
		SELECT	[JobLocationAttributeTypeID]
		FROM	[JobLocationAttributeType]
	END
GO


print '' print '*** Creating sp_retrieve_tasktype_detail_list_ by_joblocationattributeid'
GO
CREATE PROCEDURE [dbo].[sp_retrieve_tasktype_detail_list_by_joblocationattributeid]
	(
		@JobLocationAttributeTypeID [nvarchar](100)
	)
AS
	BEGIN
		SELECT	[TaskType].[TaskTypeID], [TaskType].[Name], [TaskType].[Quantity]
				, [TaskType].[JobLocationAttributeTypeID], [TaskType].[Active]
		FROM	([TaskType]
		INNER JOIN [JobLocationAttributeType] ON [TaskType].[JobLocationAttributeTypeID] 
			= [JobLocationAttributeType].[JobLocationAttributeTypeID])	
		WHERE [TaskType].[JobLocationAttributeTypeID] = @JobLocationAttributeTypeID	
	END
GO

print '' print '*** Creating sp_retrieve_tasktype_detail_list'
GO
CREATE PROCEDURE [dbo].[sp_retrieve_tasktype_detail_list]
AS
	BEGIN
		SELECT	[TaskType].[TaskTypeID], [TaskType].[Name], [TaskType].[Quantity]
				, [TaskType].[Active], [TaskType].[JobLocationAttributeTypeID]
		FROM	([TaskType]
		INNER JOIN [JobLocationAttributeType] ON [TaskType].[JobLocationAttributeTypeID]
			= [JobLocationAttributeType].[JobLocationAttributeTypeID]) 
	END	
GO


print '' print '*** Creating JobServicePackge after insert trigger ***'
GO
CREATE TRIGGER [dbo].[TRIG_JobServicePackage_AFTERINSERT]
	ON [dbo].[JobServicePackage]
	AFTER INSERT
AS 
	INSERT INTO [TaskSupply]
	([TaskTypeSupplyNeedID], [SupplyItemID], [Quantity], [JobID])
	(
		SELECT DISTINCT [TaskTypeSupplyNeed].[TaskTypeSupplyNeedID], [TaskTypeSupplyNeed].[SupplyItemID], 0 AS Quantity, INSERTED.[JobID]
		FROM [ServicePackage], [ServicePackageOffering], [ServiceOffering], [ServiceOfferingItem],
			[ServiceItem], [Task], [TaskType], [TaskTypeSupplyNeed], INSERTED
		WHERE INSERTED.[ServicePackageID] = [ServicePackage].[ServicePackageID]
		AND [ServicePackage].[ServicePackageID] = [ServicePackageOffering].[ServicePackageID]
		AND [ServicePackageOffering].[ServiceOfferingID] = [ServiceOffering].[ServiceOfferingID]
		AND [ServiceOffering].[ServiceOfferingID] = [ServiceOfferingItem].[ServiceOfferingID]
		AND [ServiceOfferingItem].[ServiceItemID] = [ServiceItem].[ServiceItemID]
		AND [ServiceItem].[ServiceItemID] = [Task].[ServiceItemID]
		AND [Task].[TaskTypeID] = [TaskType].[TaskTypeID]
		AND [TaskType].[TaskTypeID] = [TaskTypeSupplyNeed].[TaskTypeID]
	)
GO

print '' print '*** Creating sp_retrieve_tasksupply_allocation_detail_list_by_job_id'
GO
CREATE PROCEDURE [dbo].[sp_retrieve_tasksupply_allocation_detail_list_by_job_id]
	(
		@JobID		[int]
	)
AS
	BEGIN
		SELECT		[Task].[Name], [SupplyItem].[Name], [TaskTypeSupplyNeed].[Quantity], 
					[SupplyItem].[QuantityInStock], [TaskSupply].[Quantity], [TaskSupply].[TaskSupplyID]
		FROM     	[TaskSupply], [TaskTypeSupplyNeed], [TaskType], [Task], [SupplyItem]
		WHERE		[TaskSupply].[TaskTypeSupplyNeedID] = [TaskTypeSupplyNeed].[TaskTypeSupplyNeedID]
		AND			[TaskTypeSupplyNeed].[TaskTypeID] = [TaskType].[TaskTypeID]
		AND			[TaskType].[TaskTypeID] = [Task].[TaskTypeID]
		AND			[SupplyItem].[SupplyItemID] = [TaskSupply].[SupplyItemID]
		AND 		[TaskSupply].[JobID] = @JobID

	END
GO

print '' print '*** Creating sp_retrieve_taskemployee_allocation_detail_list_by_job_id'
GO
CREATE PROCEDURE [dbo].[sp_retrieve_taskemployee_allocation_detail_list_by_job_id]
	(
		@JobID		[int]
	)
AS
	BEGIN
		SELECT DISTINCT [Task].[Name], (([TaskTypeEmployeeNeed].[HoursOfWork] * [JobLocationAttribute].[Value]) / [TaskType].[Quantity]) AS TotalHours, [TaskTypeEmployeeNeed].[TaskTypeEmployeeNeedID], COUNT(DISTINCT [TaskEmployee].[EmployeeID]) as EmployeesAssigned

		FROM [Job]
		INNER JOIN [JobServicePackage] ON [Job].[JobID] = [JobServicePackage].[JobID]
		INNER JOIN [ServicePackage] ON [JobServicePackage].[ServicePackageID] = [ServicePackage].[ServicePackageID]
		INNER JOIN [ServicePackageOffering] ON [ServicePackage].[ServicePackageID] = [ServicePackageOffering].[ServicePackageID]
		INNER JOIN [ServiceOffering] ON [ServicePackageOffering].[ServiceOfferingID] = [ServiceOffering].[ServiceOfferingID]
		INNER JOIN [ServiceOfferingItem] ON [ServiceOffering].[ServiceOfferingID] = [ServiceOfferingItem].[ServiceOfferingID]
		INNER JOIN [ServiceItem] ON [ServiceOfferingItem].[ServiceItemID] = [ServiceItem].[ServiceItemID]
		INNER JOIN [Task] ON [ServiceItem].[ServiceItemID] = [Task].[ServiceItemID]
		INNER JOIN [TaskType] ON [Task].[TaskTypeID] = [TaskType].[TaskTypeID]
		INNER JOIN [TaskTypeEmployeeNeed] ON [TaskType].[TaskTypeID] = [TaskTypeEmployeeNeed].[TaskTypeID]
		INNER JOIN [JobLocation] ON [Job].[JobLocationID] = [JobLocation].[JobLocationID]
		INNER JOIN [JobLocationAttribute] ON [JobLocation].[JobLocationID] = [JobLocationAttribute].[JobLocationID] 
			AND [TaskType].[JobLocationAttributeTypeID] = [JobLocationAttribute].[JobLocationAttributeTypeID]
		LEFT JOIN [TaskEmployee] ON [TaskEmployee].[TaskTypeEmployeeNeedID] = [TaskTypeEmployeeNeed].[TaskTypeEmployeeNeedID]
		WHERE [Job].[JobID] = @JobID
		Group BY [Task].[Name], [TaskTypeEmployeeNeed].[HoursOfWork], [TaskTypeEmployeeNeed].[TaskTypeEmployeeNeedID], [JobLocationAttribute].[Value], [TaskType].[Quantity]

	END
GO


print '' print '*** Creating sp_retrieve_taskequipment_allocation_detail_list_by_job_id'
GO
CREATE PROCEDURE [dbo].[sp_retrieve_taskequipment_allocation_detail_list_by_job_id]
	(
		@JobID		[int]
	)
AS
	BEGIN
		SELECT DISTINCT [Task].[Name], [TaskTypeEquipmentNeed].[HoursOfWork], [TaskTypeEquipmentNeed].[TaskTypeEquipmentNeedID], COUNT(DISTINCT [TaskEquipment].[EquipmentID]) as EquipmentAssigned
		FROM [Job]
		INNER JOIN [JobServicePackage] ON [Job].[JobID] = [JobServicePackage].[JobID]
		INNER JOIN [ServicePackage] ON [JobServicePackage].[ServicePackageID] = [ServicePackage].[ServicePackageID]
		INNER JOIN [ServicePackageOffering] ON [ServicePackage].[ServicePackageID] = [ServicePackageOffering].[ServicePackageID]
		INNER JOIN [ServiceOffering] ON [ServicePackageOffering].[ServiceOfferingID] = [ServiceOffering].[ServiceOfferingID]
		INNER JOIN [ServiceOfferingItem] ON [ServiceOffering].[ServiceOfferingID] = [ServiceOfferingItem].[ServiceOfferingID]
		INNER JOIN [ServiceItem] ON [ServiceOfferingItem].[ServiceItemID] = [ServiceItem].[ServiceItemID]
		INNER JOIN [Task] ON [ServiceItem].[ServiceItemID] = [Task].[ServiceItemID]
		INNER JOIN [TaskType] ON [Task].[TaskTypeID] = [TaskType].[TaskTypeID]
		INNER JOIN [TaskTypeEquipmentNeed] ON [TaskType].[TaskTypeID] = [TaskTypeEquipmentNeed].[TaskTypeID]
		LEFT JOIN [TaskEquipment] ON [TaskEquipment].[TaskTypeEquipmentNeedID] = [TaskTypeEquipmentNeed].[TaskTypeEquipmentNeedID]
		WHERE [Job].[JobID] = @JobID
		Group BY [Task].[Name], [TaskTypeEquipmentNeed].[HoursOfWork], [TaskTypeEquipmentNeed].[TaskTypeEquipmentNeedID]

	END
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


print '' print '*** Creating sp_create_task_type_equipment_need'
GO
CREATE PROCEDURE [dbo].[sp_create_task_type_equipment_need]
	(
	@TaskTypeID					[int],
	@EquipmentTypeID			[nvarchar](100),
	@HoursOfWork				[int]
	)
AS
	BEGIN
		INSERT INTO	[dbo].[TaskTypeEquipmentNeed]
			([TaskTypeID], [EquipmentTypeID], [HoursOfWork])
		VALUES
			(@TaskTypeID, @EquipmentTypeID, @HoursOfWork)
		SELECT SCOPE_IDENTITY()
	END
GO

print '' print '*** Creating sp_edit_task_type_equipment_need'
GO
CREATE PROCEDURE [dbo].[sp_edit_task_type_equipment_need]
	(
	@NewTaskTypeID		 		[int],
	@OldTaskTypeID		 		[int],
	@NewEquipmentTypeID	 		[nvarchar](100),
	@OldEquipmentTypeID	 		[nvarchar](100),
	@NewHoursOfWork		 		[int],
	@OldHoursOfWork		 		[int]
	)
AS
	BEGIN
		UPDATE [TaskTypeEquipmentNeed]
			SET [TaskTypeID] = @NewTaskTypeID,
				[EquipmentTypeID] = @NewEquipmentTypeID,
				[HoursOfWork] = @NewHoursOfWork
			WHERE [TaskTypeID] = @OldTaskTypeID
            AND [EquipmentTypeID] = @OldEquipmentTypeID
            AND [HoursOfWork] = @OldHoursOfWork
		SELECT SCOPE_IDENTITY()	
	END
GO

print '' print '*** Creating sp_retrieve_task_type_equipment_need_by_id'
GO
CREATE PROCEDURE [dbo].[sp_retrieve_task_type_equipment_need_by_id]
	(
	@TaskTypeEquipmentNeedID	[int]
	)
AS
	BEGIN
		SELECT	[TaskTypeEquipmentNeedID], [TaskTypeID], [EquipmentTypeID], [HoursOfWork]
		FROM	[TaskTypeEquipmentNeed]
		WHERE	[TaskTypeEquipmentNeedID] = @TaskTypeEquipmentNeedID
	END
GO

print '' print '*** Creating sp_retrieve_tasktypeequipmentneed_detail_list'
GO
CREATE PROCEDURE [dbo].[sp_retrieve_tasktypeequipmentneed_detail_list]
AS
	BEGIN
		SELECT	[TaskTypeEquipmentNeedID],[TaskTypeID],[EquipmentTypeID],[HoursOfWork]
		FROM	[TaskTypeEquipmentNeed]
	END
GO

/* End Brady Feller */

/* Jacob Slaubaugh */
print '' print '*** Creating sp_delete_tasktypeequipmentneed_by_id'
GO
CREATE PROCEDURE [dbo].[sp_delete_tasktypeequipmentneed_by_id]
	(
	@TaskTypeEquipmentNeedID	[int]
	)
AS
	BEGIN
		DELETE FROM	[TaskTypeEquipmentNeed]
		WHERE	[TaskTypeEquipmentNeedID] = @TaskTypeEquipmentNeedID
	RETURN @@ROWCOUNT
	END
GO
/* End Jacob Slaubaugh */


print '' print '*** Creating sp_remove_taskemployee_by_tasktypeemployeeneed_id'
GO
CREATE PROCEDURE [dbo].[sp_remove_taskemployee_by_tasktypeemployeeneed_id]
	(
		@TaskTypeEmployeeNeedID			[int]
	)	
AS
	BEGIN
		DELETE FROM [TaskEmployee]
		WHERE [TaskEmployee].[TaskTypeEmployeeNeedID] = @TaskTypeEmployeeNeedID
		
		RETURN @@ROWCOUNT
	END
GO

print '' print '*** Creating sp_create_taskemployee_assignment'
GO
CREATE PROCEDURE [dbo].[sp_create_taskemployee_assignment]
	(
	@EmployeeID	            [int],
	@JobID              	[int],
    @TaskTypeEmployeeNeedID [int]
	)
AS
	BEGIN
		INSERT INTO [dbo].[TaskEmployee]
			([EmployeeID], [JobID], [TaskTypeEmployeeNeedID])
		VALUES
			(@EmployeeID, @JobID, @TaskTypeEmployeeNeedID)
	END
GO


print '' print '*** Creating sp_delete_taskemployee_assignment'
GO
CREATE PROCEDURE [dbo].[sp_delete_taskemployee_assignment]
	(
		@EmployeeID		[int],
        @JobID          [int]
	)
AS
	BEGIN
			DELETE
			FROM 		[TaskEmployee]
			WHERE 		[EmployeeID] = @EmployeeID
            AND         [JobID] = @JobID
		RETURN @@ROWCOUNT
	END
GO


print '' print '*** Creating sp_create_serviceofferingitem'
GO
CREATE PROCEDURE [dbo].[sp_create_serviceofferingitem]
	(
	@ServiceItemID			[int],
	@ServiceOfferingID		[int]
	)
AS
	BEGIN
		INSERT INTO [dbo].[ServiceOfferingItem]
			([ServiceItemID], [ServiceOfferingID])
		VALUES
			(@ServiceItemID, @ServiceOfferingID)
		RETURN @@ROWCOUNT
	END
GO

print '' print '*** Creating sp_retrieve_serviceofferingitems_by_serviceofferingid'
GO
CREATE PROCEDURE [dbo].[sp_retrieve_serviceofferingitems_by_serviceofferingid]
(
	@ServiceOfferingID		[int]
)
AS
	BEGIN
		SELECT [ServiceItemID], [ServiceOfferingID]
		FROM	[ServiceOfferingItem]
		WHERE	ServiceOfferingID = @ServiceOfferingID
	END
GO

print '' print '*** Creating sp_retrieve_serviceofferingitems_by_serviceitemid'
GO
CREATE PROCEDURE [dbo].[sp_retrieve_serviceofferingitems_by_serviceitemid]
(
	@ServiceItemID		[int]
)
AS
	BEGIN
		SELECT [ServiceItemID], [ServiceOfferingID]
		FROM	[ServiceOfferingItem]
		WHERE	ServiceItemID = @ServiceItemID
	END
GO

print '' print '*** Creating sp_delete_serviceofferingitem_by_ids'
GO
CREATE PROCEDURE [dbo].[sp_delete_serviceofferingitem_by_ids]
	(
	@ServiceItemID			[int],
	@ServiceOfferingID		[int]
	)
AS
	BEGIN
		DELETE FROM 		[ServiceOfferingItem]
		WHERE 		[ServiceItemID] = @ServiceItemID
		AND 		[ServiceOfferingID] = @ServiceOfferingID
		RETURN @@ROWCOUNT
	END
GO

/* Sam Dramstad, 2018/04/11 */
print '' print '*** Creating sp_create_taskequipment_assignment'
GO
CREATE PROCEDURE [dbo].[sp_create_taskequipment_assignment]
	(
	@EquipmentID             [int],
	@JobID                   [int],
    @TaskTypeEquipmentNeedID [int]
	)
AS
	BEGIN
		INSERT INTO [dbo].[TaskEquipment]
			([EquipmentID], [JobID], [TaskTypeEquipmentNeedID])
		VALUES
			(@EquipmentID, @JobID, @TaskTypeEquipmentNeedID)
	END
GO

print '' print '*** Creating sp_create_job_with_target_window'
GO
CREATE PROCEDURE [dbo].[sp_create_job_with_target_window]
	(
	@CustomerID			[int],
	@DateTimeTarget		[DateTime],
	@EmployeeID			[int],
	@JobLocationID		[int],
	@Active				[bit],
	@DateCompleted		[DateTime],
	@Comments			[nvarchar](1000)
	)
AS
	BEGIN
		INSERT INTO [dbo].[Job]
			([CustomerID], [DateTimeTargetWindow], [EmployeeID], [JobLocationID]
			, [Active], [DateCompleted], [Comments])
		VALUES
			(@CustomerID, @DateTimeTarget, @EmployeeID, @JobLocationID
			, @Active, @DateCompleted, @Comments)
		SELECT SCOPE_IDENTITY()
	END
GO


print '' print '*** Creating sp_update_job_with_target_window'
GO
CREATE PROCEDURE [dbo].[sp_update_job_with_target_window]
	(
	@JobID 				[int],
	@OldCustomerID		[int],
	@NewCustomerID		[int],
	@OldEmployeeID		[int],
	@NewEmployeeID		[int],
	@OldJobLocationID	[int],
	@NewJobLocationID	[int],
	@OldActive			[bit],
	@NewActive			[bit],
	@OldDateCompleted	[DateTime] = null,
	@NewDateCompleted	[DateTime] = null,
	@OldDateScheduled	[DateTime] = null,
	@NewDateScheduled	[DateTime] = null,
	@OldTargetWindow	[DateTime],
	@NewTargetWindow	[DateTime],
	@OldComments		[nvarchar](1000),
	@NewComments		[nvarchar](1000)
	)
AS
	BEGIN
		UPDATE 	[Job]
			SET 	[CustomerID] = @NewCustomerID,
					[EmployeeID] = @NewEmployeeID,
					[JobLocationID] = @NewJobLocationID,
					[Active] = @NewActive,
					[DateCompleted] = @NewDateCompleted,
					[DateTimeScheduled] = @NewDateScheduled,
					[Comments] = @NewComments,
					[DateTimeTargetWindow] = @NewTargetWindow
			WHERE 	[JobID] = @JobID
			AND [CustomerID] = @OldCustomerID
			AND [EmployeeID] = @OldEmployeeID
			AND [JobLocationID] = @OldJobLocationID
			AND [Active] = @OldActive
			AND [DateTimeScheduled] = @OldDateScheduled
			AND [Comments] = @OldComments
			AND [DateTimeTargetWindow] = @OldTargetWindow
		RETURN @@ROWCOUNT
	END
GO


print '' print '*** Creating sp_retrieve_job_detail_list_with_target_window'
GO
CREATE PROCEDURE [dbo].[sp_retrieve_job_detail_list_with_target_window]
AS
	BEGIN
		SELECT [Job].[JobID], [Job].[CustomerID], [Job].[DateTimeScheduled], [Job].[EmployeeID], 
				[Job].[JobLocationID], [Job].[Active], [Job].[DateCompleted], [Job].[Comments],
				[Customer].[CustomerTypeID], [Customer].[Email], [Customer].[FirstName], [Customer].[LastName],
				[Customer].[PhoneNumber], [Customer].[Active],
				[JobLocation].[JobLocationID], [JobLocation].[Street], [JobLocation].[City], [JobLocation].[State], 
				[JobLocation].[ZipCode], [JobLocation].[Comments], [JobLocation].[Active], [Job].[DateTimeTargetWindow]
		FROM [Job]
		INNER JOIN [Customer] ON [Job].[CustomerID] = [Customer].[CustomerID]
		INNER JOIN [JobLocation] ON [JobLocation].[JobLocationID] = [Job].[JobLocationID]
		
		SELECT [JobLocationAttribute].[JobLocationID], [JobLocationAttribute].[JobLocationAttributeTypeID], [JobLocationAttribute].[Value]
		FROM [JobLocationAttribute]
		
		SELECT [JobServicePackage].[JobID], [JobServicePackage].[ServicePackageID],  [ServicePackage].[Name],  [ServicePackage].[Description],  [ServicePackage].[Active]
		FROM [JobServicePackage]
		INNER JOIN [ServicePackage] ON [JobServicePackage].[ServicePackageID] = [ServicePackage].[ServicePackageID]
	END
GO

print '' print '*** Creating sp_retrieve_employee_list_tasktypeemployeeneedid_and_jobid'
GO
CREATE PROCEDURE [dbo].[sp_retrieve_employee_list_tasktypeemployeeneedid_and_jobid]
	(
		@JobID	 					[int],
		@TaskTypeEmployeeNeedID		[int]
	)
AS
	BEGIN
		SELECT	[Employee].[EmployeeID], [Employee].[FirstName], [Employee].[LastName], [Employee].[Address], [Employee].[PhoneNumber]
			,[Employee].[Email], [Employee].[Active]
		FROM	[Employee], [TaskEmployee]
		WHERE	[TaskEmployee].[EmployeeID] = [Employee].[EmployeeID]
		AND [TaskEmployee].[JobID] = @JobID
		AND [TaskEmployee].[TaskTypeEmployeeNeedID] = @TaskTypeEmployeeNeedID
	END
GO

print '' print '*** Creating sp_retrieve_taskttypeemployeeneed_detail_list'
GO
CREATE PROCEDURE [dbo].[sp_retrieve_taskttypeemployeeneed_detail_list]
AS
	BEGIN
		SELECT 		[TaskTypeEmployeeNeed].[TaskTypeID], [TaskTypeEmployeeNeed].[HoursOfWork], [TaskTypeEmployeeNeed].[Active],
					[TaskType].[Name], [TaskType].[Quantity], [TaskType].[JobLocationAttributeTypeID]
		FROM		[TaskType], [TaskTypeEmployeeNeed]
		WHERE [TaskType].[TaskTypeID] = [TaskTypeEmployeeNeed].[TaskTypeID]
	END
GO


print '' print '*** Creating sp_edit_taskttypeemployeeneed_by_id'
GO
CREATE PROCEDURE [dbo].[sp_edit_taskttypeemployeeneed_by_id]
	(
		@TaskTypeID		[int],
		@NewHoursOfWork	[int],
		@OldHoursOfWork	[int],
		@NewActive		[bit],
		@OldActive		[bit]
	)
AS
	BEGIN
		UPDATE [TaskTypeEmployeeNeed]
			SET [HoursOfWork] = @NewHoursOfWork,
				[Active] = @NewActive
			WHERE [TaskTypeID] = @TaskTypeID
				AND [HoursOfWork] = @OldHoursOfWork
				AND [Active] = @OldActive
		RETURN @@ROWCOUNT
	END
GO


print '' print '*** Creating sp_deactivate_taskttypeemployeeneed_by_id'
GO
CREATE PROCEDURE [dbo].[sp_deactivate_taskttypeemployeeneed_by_id]
	(
		@TaskTypeID		[int]
	)
AS
	BEGIN
		UPDATE [TaskTypeEmployeeNeed]
			SET [Active] = 0
			WHERE [TaskTypeID] = @TaskTypeID
		RETURN @@ROWCOUNT
	END
GO


print '' print  '*** Creating procedure sp_create_taskttypeemployeeneed'
GO
CREATE PROCEDURE [dbo].[sp_create_taskttypeemployeeneed]
	(
	@TaskTypeID		[int],
	@HoursOfWork	[int],
	@Active			[bit]
	)
AS
	BEGIN
		INSERT INTO [TaskTypeEmployeeNeed]
			([TaskTypeID], [HoursOfWork], [Active])
		VALUES
			(@TaskTypeID, @HoursOfWork, @Active)
		RETURN @@ROWCOUNT
	END
GO



print '' print '*** Creating sp_retrieve_taskequipment_allocation_detail_list_by_job_id_updated'
GO
CREATE PROCEDURE [dbo].[sp_retrieve_taskequipment_allocation_detail_list_by_job_id_updated]
	(
		@JobID		[int]
	)
AS
	BEGIN
		SELECT DISTINCT [Task].[Name], [TaskTypeEquipmentNeed].[HoursOfWork], [TaskTypeEquipmentNeed].[TaskTypeEquipmentNeedID], COUNT(DISTINCT [TaskEquipment].[EquipmentID]) as EquipmentAssigned, [Task].[TaskID]
		FROM [Job]
		INNER JOIN [JobServicePackage] ON [Job].[JobID] = [JobServicePackage].[JobID]
		INNER JOIN [ServicePackage] ON [JobServicePackage].[ServicePackageID] = [ServicePackage].[ServicePackageID]
		INNER JOIN [ServicePackageOffering] ON [ServicePackage].[ServicePackageID] = [ServicePackageOffering].[ServicePackageID]
		INNER JOIN [ServiceOffering] ON [ServicePackageOffering].[ServiceOfferingID] = [ServiceOffering].[ServiceOfferingID]
		INNER JOIN [ServiceOfferingItem] ON [ServiceOffering].[ServiceOfferingID] = [ServiceOfferingItem].[ServiceOfferingID]
		INNER JOIN [ServiceItem] ON [ServiceOfferingItem].[ServiceItemID] = [ServiceItem].[ServiceItemID]
		INNER JOIN [Task] ON [ServiceItem].[ServiceItemID] = [Task].[ServiceItemID]
		INNER JOIN [TaskType] ON [Task].[TaskTypeID] = [TaskType].[TaskTypeID]
		INNER JOIN [TaskTypeEquipmentNeed] ON [TaskType].[TaskTypeID] = [TaskTypeEquipmentNeed].[TaskTypeID]
		LEFT JOIN [TaskEquipment] ON [TaskEquipment].[TaskTypeEquipmentNeedID] = [TaskTypeEquipmentNeed].[TaskTypeEquipmentNeedID]
		WHERE [Job].[JobID] = @JobID
		Group BY [Task].[Name], [TaskTypeEquipmentNeed].[HoursOfWork], [TaskTypeEquipmentNeed].[TaskTypeEquipmentNeedID],[Task].[TaskID]

	END
GO

print '' print '*** Creating sp_delete_taskequipment_assignment'
GO
CREATE PROCEDURE [dbo].[sp_delete_taskequipment_assignment]
	(
		@JobID				[int],
		@EquipmentID		[int]
	)
AS
	BEGIN
		DELETE FROM	[TaskEquipment]
			WHERE	[JobID] = @JobID
			AND 	[EquipmentID] = @EquipmentID
		RETURN @@ROWCOUNT
	END
GO


print '' print '*** Creating sp_delete_all_assigned_equipment_by_task_id_and_job_id'
GO
CREATE PROCEDURE [dbo].[sp_delete_all_assigned_equipment_by_task_id_and_job_id]
	(
		@TaskID		[int],
		@JobID		[int]
	)
AS
	BEGIN
		DELETE [TaskEquipment]
		FROM [Equipment]
		INNER JOIN [TaskEquipment] ON [Equipment].[EquipmentID] = [TaskEquipment].[EquipmentID]
		INNER JOIN [TaskTypeEquipmentNeed] ON [TaskEquipment].[TaskTypeEquipmentNeedID] = [TaskTypeEquipmentNeed].[TaskTypeEquipmentNeedID]
		INNER JOIN [TaskType] ON [TaskTypeEquipmentNeed].[TaskTypeID] = [TaskType].[TaskTypeID]
		INNER JOIN [Task] ON [TaskType].[TaskTypeID] = [Task].[TaskTypeID]
		WHERE [Task].[TaskID] = @TaskID
		AND [TaskEquipment].[JobID] = @JobID
	END
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


print '' print '*** Creating Supply qunatity function ***'
GO
CREATE FUNCTION [dbo].[fn_calculate_supply_quantity_for_job_supply_need]
(
	@TaskTypeSupplyNeedID [int],
	@JobID [int]
)RETURNS int
AS
BEGIN
	DECLARE @Result int, @TaskTypeSupplyNeed_Quantity int, @JobLocationAttribute_Value int,  @TaskType_Value int
	
	SELECT @TaskTypeSupplyNeed_Quantity = [TaskTypeSupplyNeed].[Quantity]
	FROM [TaskTypeSupplyNeed]
	WHERE [TaskTypeSupplyNeedID] = @TaskTypeSupplyNeedID
	
	SELECT @JobLocationAttribute_Value = [JobLocationAttribute].[Value]
	FROM [JobLocationAttribute], [TaskType], [TaskTypeSupplyNeed], [JobLocation], [Job]
	WHERE [JobLocationAttribute].[JobLocationAttributeTypeID] = [TaskType].[JobLocationAttributeTypeID]
	AND [TaskType].[TaskTypeID] = [TaskTypeSupplyNeed].[TaskTypeID]
	AND [TaskTypeSupplyNeed].[TaskTypeSupplyNeedID] = @TaskTypeSupplyNeedID
	AND [JobLocationAttribute].[JobLocationID] = [JobLocation].[JobLocationID]
	AND [JobLocation].[JobLocationID] = [Job].[JobLocationID]
	AND [Job].[JobID] = @JobID
	
	SELECT @TaskType_Value = [TaskType].[Quantity]
	FROM [TaskType], [TaskTypeSupplyNeed]
	WHERE [TaskTypeSupplyNeed].[TaskTypeID] = [TaskType].[TaskTypeID]
	AND [TaskTypeSupplyNeed].[TaskTypeSupplyNeedID] = @TaskTypeSupplyNeedID
	
	SELECT @Result = CEILING(@TaskTypeSupplyNeed_Quantity * ((@JobLocationAttribute_Value * 1.0) / @TaskType_Value))
	
	RETURN @Result
END
GO

print '' print '*** Creating calc employee hours function ***'
GO
CREATE FUNCTION [dbo].[fn_calculate_hours_of_work_for_job_employee_need]
(
	@TaskTypeEmployeeNeedID [int],
	@JobID [int]
)RETURNS int
AS
BEGIN
	DECLARE @Result int, @TaskTypeEmployeeNeed_HoursOfWork int, @JobLocationAttribute_Value int,  @TaskType_Value int
	
	SELECT @TaskTypeEmployeeNeed_HoursOfWork = [TaskTypeEmployeeNeed].[HoursOfWork]
	FROM [TaskTypeEmployeeNeed]
	WHERE [TaskTypeEmployeeNeedID] = @TaskTypeEmployeeNeedID
	
	SELECT @JobLocationAttribute_Value = [JobLocationAttribute].[Value]
	FROM [JobLocationAttribute], [TaskType], [TaskTypeEmployeeNeed], [JobLocation], [Job]
	WHERE [JobLocationAttribute].[JobLocationAttributeTypeID] = [TaskType].[JobLocationAttributeTypeID]
	AND [TaskType].[TaskTypeID] = [TaskTypeEmployeeNeed].[TaskTypeID]
	AND [TaskTypeEmployeeNeed].[TaskTypeEmployeeNeedID] = @TaskTypeEmployeeNeedID
	AND [JobLocationAttribute].[JobLocationID] = [JobLocation].[JobLocationID]
	AND [JobLocation].[JobLocationID] = [Job].[JobLocationID]
	AND [Job].[JobID] = @JobID
	
	SELECT @TaskType_Value = [TaskType].[Quantity]
	FROM [TaskType], [TaskTypeEmployeeNeed]
	WHERE [TaskTypeEmployeeNeed].[TaskTypeID] = [TaskType].[TaskTypeID]
	AND [TaskTypeEmployeeNeed].[TaskTypeEmployeeNeedID] = @TaskTypeEmployeeNeedID
	
	SELECT @Result = CEILING(@TaskTypeEmployeeNeed_HoursOfWork * ((@JobLocationAttribute_Value * 1.0) / @TaskType_Value))
	
	RETURN @Result
END
GO

print '' print '*** Creating calc employee hours function ***'
GO
CREATE FUNCTION [dbo].[fn_calculate_hours_of_work_for_job_equipment_need]
(
	@TaskTypeEquipmentNeedID [int],
	@JobID [int]
)RETURNS int
AS
BEGIN
	DECLARE @Result int, @TaskTypeEquipmentNeed_HoursOfWork int, @JobLocationAttribute_Value int,  @TaskType_Value int
	
	SELECT @TaskTypeEquipmentNeed_HoursOfWork = [TaskTypeEquipmentNeed].[HoursOfWork]
	FROM [TaskTypeEquipmentNeed]
	WHERE [TaskTypeEquipmentNeedID] = @TaskTypeEquipmentNeedID
	
	SELECT @JobLocationAttribute_Value = [JobLocationAttribute].[Value]
	FROM [JobLocationAttribute], [TaskType], [TaskTypeEquipmentNeed], [JobLocation], [Job]
	WHERE [JobLocationAttribute].[JobLocationAttributeTypeID] = [TaskType].[JobLocationAttributeTypeID]
	AND [TaskType].[TaskTypeID] = [TaskTypeEquipmentNeed].[TaskTypeID]
	AND [TaskTypeEquipmentNeed].[TaskTypeEquipmentNeedID] = @TaskTypeEquipmentNeedID
	AND [JobLocationAttribute].[JobLocationID] = [JobLocation].[JobLocationID]
	AND [JobLocation].[JobLocationID] = [Job].[JobLocationID]
	AND [Job].[JobID] = @JobID
	
	SELECT @TaskType_Value = [TaskType].[Quantity]
	FROM [TaskType], [TaskTypeEquipmentNeed]
	WHERE [TaskTypeEquipmentNeed].[TaskTypeID] = [TaskType].[TaskTypeID]
	AND [TaskTypeEquipmentNeed].[TaskTypeEquipmentNeedID] = @TaskTypeEquipmentNeedID
	
	SELECT @Result = CEILING(@TaskTypeEquipmentNeed_HoursOfWork * ((@JobLocationAttribute_Value * 1.0) / @TaskType_Value))
	
	RETURN @Result
END
GO


print '' print '*** Creating JobServicePackge after insert trigger ***'
GO
DROP TRIGGER [dbo].[TRIG_JobServicePackage_AFTERINSERT]
GO
CREATE TRIGGER [dbo].[TRIG_JobServicePackage_AFTERINSERT]
	ON [dbo].[JobServicePackage]
	AFTER INSERT
AS 
	DECLARE @TableID int, @LinesNeeded int
	

	-- Supply Lines
	INSERT INTO [TaskSupply]
	([TaskTypeSupplyNeedID], [SupplyItemID], [Quantity], [JobID])
	(
		SELECT DISTINCT [TaskTypeSupplyNeed].[TaskTypeSupplyNeedID], [TaskTypeSupplyNeed].[SupplyItemID]
		, dbo.fn_calculate_supply_quantity_for_job_supply_need([TaskTypeSupplyNeed].[TaskTypeSupplyNeedID], INSERTED.[JobID]), INSERTED.[JobID]
		FROM [ServicePackage], [ServicePackageOffering], [ServiceOffering], [ServiceOfferingItem],
			[ServiceItem], [Task], [TaskType], [TaskTypeSupplyNeed], INSERTED
		WHERE INSERTED.[ServicePackageID] = [ServicePackage].[ServicePackageID]
		AND [ServicePackage].[ServicePackageID] = [ServicePackageOffering].[ServicePackageID]
		AND [ServicePackageOffering].[ServiceOfferingID] = [ServiceOffering].[ServiceOfferingID]
		AND [ServiceOffering].[ServiceOfferingID] = [ServiceOfferingItem].[ServiceOfferingID]
		AND [ServiceOfferingItem].[ServiceItemID] = [ServiceItem].[ServiceItemID]
		AND [ServiceItem].[ServiceItemID] = [Task].[ServiceItemID]
		AND [Task].[TaskTypeID] = [TaskType].[TaskTypeID]
		AND [TaskType].[TaskTypeID] = [TaskTypeSupplyNeed].[TaskTypeID]
	)
	
	
	-- Employee Lines
	
	-- get initial data
	SELECT DISTINCT [TaskTypeEmployeeNeed].[TaskTypeEmployeeNeedID], [TaskTypeEmployeeNeed].[TaskTypeID], [TaskTypeEmployeeNeed].[HoursOfWork]
	, dbo.fn_calculate_hours_of_work_for_job_employee_need([TaskTypeEmployeeNeed].[TaskTypeEmployeeNeedID], INSERTED.[JobID]) as HoursTotal, INSERTED.[JobID] as JobID
	INTO #TmpTaskEmployeeData
	FROM [ServicePackage], [ServicePackageOffering], [ServiceOffering], [ServiceOfferingItem],
		[ServiceItem], [Task], [TaskType], [TaskTypeEmployeeNeed], INSERTED
	WHERE INSERTED.[ServicePackageID] = [ServicePackage].[ServicePackageID]
	AND [ServicePackage].[ServicePackageID] = [ServicePackageOffering].[ServicePackageID]
	AND [ServicePackageOffering].[ServiceOfferingID] = [ServiceOffering].[ServiceOfferingID]
	AND [ServiceOffering].[ServiceOfferingID] = [ServiceOfferingItem].[ServiceOfferingID]
	AND [ServiceOfferingItem].[ServiceItemID] = [ServiceItem].[ServiceItemID]
	AND [ServiceItem].[ServiceItemID] = [Task].[ServiceItemID]
	AND [Task].[TaskTypeID] = [TaskType].[TaskTypeID]
	AND [TaskType].[TaskTypeID] = [TaskTypeEmployeeNeed].[TaskTypeID]
	
	-- loop through creating a TaskEmployee for each line
	WHILE EXISTS (SELECT * FROM #TmpTaskEmployeeData)
	BEGIN
		
		SELECT TOP 1 @TableID = TaskTypeEmployeeNeedID
		FROM #TmpTaskEmployeeData
		
		SELECT @LinesNeeded = CEILING(HoursTotal / 12.0)
		FROM #TmpTaskEmployeeData
		WHERE TaskTypeEmployeeNeedID = @TableID
		
		WHILE @LinesNeeded > 0
		BEGIN
			INSERT INTO [TaskEmployee]
				([JobID], [TaskTypeEmployeeNeedID], [EmployeeID])	
			(
				SELECT [JobID], [TaskTypeEmployeeNeedID], NULL
				FROM #TmpTaskEmployeeData
				WHERE TaskTypeEmployeeNeedID = @TableID
			)
			SET @LinesNeeded = @LinesNeeded - 1;
		END
		
		DELETE #TmpTaskEmployeeData
		WHERE TaskTypeEmployeeNeedID = @TableID
	END
	
	
	-- Equipment Lines
	
	SELECT DISTINCT [TaskTypeEquipmentNeed].[TaskTypeEquipmentNeedID], [TaskTypeEquipmentNeed].[TaskTypeID], [TaskTypeEquipmentNeed].[HoursOfWork]
	, [TaskTypeEquipmentNeed].[EquipmentTypeID]
	, [dbo].[fn_calculate_hours_of_work_for_job_equipment_need]([TaskTypeEquipmentNeed].[TaskTypeEquipmentNeedID], INSERTED.[JobID]) as HoursTotal, INSERTED.[JobID] as JobID
	INTO #TmpTaskEquipmentData
	FROM [ServicePackage], [ServicePackageOffering], [ServiceOffering], [ServiceOfferingItem],
		[ServiceItem], [Task], [TaskType], [TaskTypeEquipmentNeed], INSERTED
	WHERE INSERTED.[ServicePackageID] = [ServicePackage].[ServicePackageID]
	AND [ServicePackage].[ServicePackageID] = [ServicePackageOffering].[ServicePackageID]
	AND [ServicePackageOffering].[ServiceOfferingID] = [ServiceOffering].[ServiceOfferingID]
	AND [ServiceOffering].[ServiceOfferingID] = [ServiceOfferingItem].[ServiceOfferingID]
	AND [ServiceOfferingItem].[ServiceItemID] = [ServiceItem].[ServiceItemID]
	AND [ServiceItem].[ServiceItemID] = [Task].[ServiceItemID]
	AND [Task].[TaskTypeID] = [TaskType].[TaskTypeID]
	AND [TaskType].[TaskTypeID] = [TaskTypeEquipmentNeed].[TaskTypeID]
	
	WHILE EXISTS (SELECT * FROM #TmpTaskEquipmentData)
	BEGIN
		
		SELECT TOP 1 @TableID = TaskTypeEquipmentNeedID
		FROM #TmpTaskEquipmentData
		
		SELECT @LinesNeeded = CEILING(HoursTotal / 12.0)
		FROM #TmpTaskEquipmentData
		WHERE TaskTypeEquipmentNeedID = @TableID
		
		WHILE @LinesNeeded > 0
		BEGIN
			INSERT INTO [TaskEquipment]
				([JobID], [TaskTypeEquipmentNeedID], [EquipmentID])	
			(
				SELECT [JobID], [TaskTypeEquipmentNeedID], NULL
				FROM #TmpTaskEquipmentData
				WHERE TaskTypeEquipmentNeedID = @TableID
			)
			SET @LinesNeeded = @LinesNeeded - 1;
		END
		
		DELETE #TmpTaskEquipmentData
		WHERE TaskTypeEquipmentNeedID = @TableID
	END
	
	
	-- Create JobTasks
	SELECT DISTINCT [Task].[TaskID], Inserted.[JobID]
	INTO #Tasks
	FROM INSERTED, [ServicePackageOffering], [ServiceOfferingItem], [Task]
	WHERE INSERTED.[ServicePackageID] = [ServicePackageOffering].[ServicePackageID]
	AND [ServicePackageOffering].[ServiceOfferingID] = [ServiceOfferingItem].[ServiceOfferingID]
	AND [ServiceOfferingItem].[ServiceItemID] = [Task].[ServiceItemID]
	
	INSERT INTO [JobTask]
		([JobID], [TaskID])
	(
		SELECT #Tasks.[JobID], #Tasks.[TaskID]
		FROM #Tasks
	)
GO


print '' print '*** Creating sp_get_task_employees_by_job_id'
GO
CREATE PROCEDURE [dbo].[sp_get_task_employees_by_job_id]
	(
		@JobID int
	)
AS
	BEGIN
		SELECT [Task].[Name], [TaskEmployee].[EmployeeID], [Employee].[FirstName], [Employee].[LastName], [TaskEmployee].[TaskEmployeeID]
		FROM [Task]
		INNER JOIN [TaskType] ON [Task].[TaskTypeID] = [TaskType].[TaskTypeID]
		INNER JOIN [TaskTypeEmployeeNeed] ON [TaskType].[TaskTypeID] = [TaskTypeEmployeeNeed].[TaskTypeID]
		INNER JOIN [TaskEmployee] ON [TaskTypeEmployeeNeed].[TaskTypeEmployeeNeedID] = [TaskEmployee].[TaskTypeEmployeeNeedID]
		LEFT JOIN [Employee] ON [TaskEmployee].[EmployeeID] = [Employee].[EmployeeID]
		WHERE [TaskEmployee].[JobID] = @JobID
	END
GO


print '' print '*** Creating sp_get_task_equipment_by_job_id'
GO
CREATE PROCEDURE [dbo].[sp_get_task_equipment_by_job_id]
	(
		@JobID int
	)
AS
	BEGIN
		SELECT [Task].[Name], [TaskEquipment].[EquipmentID], [Equipment].[Name], [Equipment].[EquipmentTypeID], [TaskEquipment].[TaskEquipmentID]
		FROM [Task]
		INNER JOIN [TaskType] ON [Task].[TaskTypeID] = [TaskType].[TaskTypeID]
		INNER JOIN [TaskTypeEquipmentNeed] ON [TaskType].[TaskTypeID] = [TaskTypeEquipmentNeed].[TaskTypeID]
		INNER JOIN [TaskEquipment] ON [TaskTypeEquipmentNeed].[TaskTypeEquipmentNeedID] = [TaskEquipment].[TaskTypeEquipmentNeedID]
		LEFT JOIN [Equipment] ON [TaskEquipment].[EquipmentID] = [Equipment].[EquipmentID]
		WHERE [TaskEquipment].[JobID] = @JobID
		
	END
GO


print '' print '*** Creating sp_get_task_supply_by_job_id'
GO
CREATE PROCEDURE [dbo].[sp_get_task_supply_by_job_id]
	(
		@JobID int
	)
AS
	BEGIN
		SELECT [Task].[Name], [TaskSupply].[SupplyItemID], [TaskSupply].[Quantity]
		, [SupplyItem].[Name]
		FROM [Task], [TaskType], [TaskTypeSupplyNeed], [TaskSupply], [SupplyItem]
		WHERE [TaskSupply].[JobID] = @JobID
		AND [Task].[TaskTypeID] = [TaskType].[TaskTypeID]
		AND [TaskType].[TaskTypeID] = [TaskTypeSupplyNeed].[TaskTypeID]
		AND [TaskTypeSupplyNeed].[TaskTypeSupplyNeedID] = [TaskSupply].[TaskTypeSupplyNeedID]
		AND [TaskSupply].[SupplyItemID] = [SupplyItem].[SupplyItemID]
		
	END
GO


print '' print '*** Creating sp_update_taskemployee_employee_id'
GO
CREATE PROCEDURE [dbo].[sp_update_taskemployee_employee_id]
	(
		@TaskEmployeeID int,
		@EmployeeID int
	)
AS
	BEGIN
		UPDATE [TaskEmployee]
			SET [EmployeeID] = @EmployeeID
		WHERE [TaskEmployeeID] = @TaskEmployeeID
		
		RETURN @@ROWCOUNT
	END
GO

print '' print '*** Creating sp_update_taskemployee_employee_id_null'
GO
CREATE PROCEDURE [dbo].[sp_update_taskemployee_employee_id_null]
	(
		@TaskEmployeeID int
	)
AS
	BEGIN
		UPDATE [TaskEmployee]
			SET [EmployeeID] = NULL
		WHERE [TaskEmployeeID] = @TaskEmployeeID
		
		RETURN @@ROWCOUNT
	END
GO



print '' print '*** Creating sp_update_job_scheduled_date'
GO
CREATE PROCEDURE [dbo].[sp_update_job_scheduled_date]
	(
		@JobID int,
		@NewScheduledDate datetime
	)
AS
	BEGIN
		UPDATE [Job]
			SET [DateTimeScheduled] = @NewScheduledDate
		WHERE [JobID] = @JobID
		
		RETURN @@ROWCOUNT
	END
GO


print '' print '*** Creating sp_retrieve_job_detail_by_jobid'
GO
CREATE PROCEDURE [dbo].[sp_retrieve_job_detail_by_jobid]
(
	@JobID int
)
AS
	BEGIN
		SELECT [Job].[JobID], [Job].[CustomerID], [Job].[DateTimeScheduled], [Job].[EmployeeID], 
				[Job].[JobLocationID], [Job].[Active], [Job].[DateCompleted], [Job].[Comments],
				[Customer].[CustomerTypeID], [Customer].[Email], [Customer].[FirstName], [Customer].[LastName],
				[Customer].[PhoneNumber], [Customer].[Active],
				[JobLocation].[JobLocationID], [JobLocation].[Street], [JobLocation].[City], [JobLocation].[State], 
				[JobLocation].[ZipCode], [JobLocation].[Comments], [JobLocation].[Active], [Job].[DateTimeTargetWindow]
		FROM [Job]
		INNER JOIN [Customer] ON [Job].[CustomerID] = [Customer].[CustomerID]
		INNER JOIN [JobLocation] ON [JobLocation].[JobLocationID] = [Job].[JobLocationID]
		WHERE [Job].[JobID] = @JobID
		
		SELECT [JobLocationAttribute].[JobLocationID], [JobLocationAttribute].[JobLocationAttributeTypeID], [JobLocationAttribute].[Value]
		FROM [JobLocationAttribute], [JobLocation], [Job]
		WHERE [JobLocationAttribute].[JobLocationID] = [JobLocation].[JobLocationID]
		AND [JobLocation].[JobLocationID] = [Job].[JobLocationID]
		AND [Job].[JobID] = @JobID
		
		SELECT [JobServicePackage].[JobID], [JobServicePackage].[ServicePackageID],  [ServicePackage].[Name],  [ServicePackage].[Description],  [ServicePackage].[Active]
		FROM [JobServicePackage]
		INNER JOIN [ServicePackage] ON [JobServicePackage].[ServicePackageID] = [ServicePackage].[ServicePackageID]
		WHERE [JobServicePackage].[JobID] = @JobID
	END
GO


print '' print '*** Creating sp_update_taskequipment_equipment_id'
GO
CREATE PROCEDURE [dbo].[sp_update_taskequipment_equipment_id]
	(
		@TaskEquipmentID int,
		@EquipmentID int
	)
AS
	BEGIN
		UPDATE [TaskEquipment]
			SET [EquipmentID] = @EquipmentID
		WHERE [TaskEquipmentID] = @TaskEquipmentID
		
		RETURN @@ROWCOUNT
	END
GO

print '' print '*** Creating sp_update_taskequipment_equipment_id_null'
GO
CREATE PROCEDURE [dbo].[sp_update_taskequipment_equipment_id_null]
	(
		@TaskEquipmentID int
	)
AS
	BEGIN
		UPDATE [TaskEquipment]
			SET [EquipmentID] = NULL
		WHERE [TaskEquipmentID] = @TaskEquipmentID
		
		RETURN @@ROWCOUNT
	END
GO


print '' print '*** Creating sp_retrieve_updated_tasktypesupplies'
GO
CREATE PROCEDURE [dbo].[sp_retrieve_updated_tasktypesupplies]
AS
	BEGIN
		SELECT	[TaskTypeSupplyNeedID],[TaskTypeID],[SupplyItemID],[Quantity], [Active]
		FROM	[TaskTypeSupplyNeed]
		WHERE	[Active] = 1
	END
GO

print '' print '*** Creating sp_deactivate_tasktypesupplyneed'
GO
CREATE PROCEDURE [dbo].[sp_deactivate_tasktypesupplyneed]
	(
		@TaskTypeSupplyNeedID			[int]
	)
AS
	BEGIN
		UPDATE 	[TaskTypeSupplyNeed]
		SET [Active] = 0
		WHERE	[TaskTypeSupplyNeedID] = @TaskTypeSupplyNeedID
		AND [Active] = 1
		RETURN @@ROWCOUNT
	END
GO

print '' print '*** in file CreateJobTask.sql ***'
USE [crlandscaping]
GO

print '' print '*** Creating sp_retrieve_job_task'

GO
CREATE PROCEDURE [dbo].[sp_retrieve_job_task]
AS
	BEGIN
		SELECT [JobTask].[JobTaskID], [Task].[Name], [Task].[Description], [JobTask].[IsDone]
		FROM [JobTask], [Task]
		WHERE [JobTask].[TaskID] = [Task].[TaskID]
	END
GO

print '' print '*** Creating sp_edit_is_done'

GO
CREATE PROCEDURE [dbo].[sp_edit_is_done]
	(
	@JobTaskID			[int],
	@OldIsDone			[bit],
	@NewIsDone			[bit]
	)
AS
	BEGIN
		UPDATE 	[JobTask]
		SET 	[IsDone] = @NewIsDone
		WHERE 	[JobTaskID] = @JobTaskID
		AND		[IsDone] = @OldIsDone
		SELECT SCOPE_IDENTITY()
	END
GO

print '' print '*** Creating sp_create_job_web'
GO
CREATE PROCEDURE [dbo].[sp_create_job_web]
	(
	@CustomerID			[int],
	@DateTimeScheduled	[DateTime],
	@DateTimeTarget		[DateTime],
	@JobLocationID		[int],
	@DateCompleted		[DateTime],
	@Comments			[nvarchar](1000)
	)
AS
	BEGIN
		INSERT INTO [dbo].[Job]
			([CustomerID], [DateTimeScheduled], [DateTimeTargetWindow], [JobLocationID]
			,[DateCompleted], [Comments])
		VALUES
			(@CustomerID, @DateTimeScheduled, @DateTimeTarget, @JobLocationID
			,@DateCompleted, @Comments)
		SELECT SCOPE_IDENTITY()
	END
GO

print '' print '*** Creating JobLocation after insert trigger ***'
GO
CREATE TRIGGER [dbo].[TRIG_JobLocation_AFTERINSERT]
	ON [dbo].[JobLocation]
	AFTER INSERT
AS 
	BEGIN
		DECLARE @JobLocationID		[int]
		
		SELECT @JobLocationID = INSERTED.[JobLocationID]
		FROM INSERTED
		
		SELECT @JobLocationID as [JobLocationID], [JobLocationAttributeType].[JobLocationAttributeTypeID], 1 as [Value]
		INTO #JobLocationAttributes
		FROM [JobLocationAttributeType]
		
		
		INSERT INTO [JobLocationAttribute]
			([JobLocationID], [JobLocationAttributeTypeID], [Value])
		(SELECT  [JobLocationID], [JobLocationAttributeTypeID], [Value]
		FROM #JobLocationAttributes)
	
	
	END
GO
