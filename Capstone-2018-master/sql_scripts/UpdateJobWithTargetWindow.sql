print '' print '*** Using database crlandscaping'
GO
USE [crlandscaping]
GO


print '' print '*** Setting Target Window'
GO
UPDATE [Job]
SET [DateTimeTargetWindow] = '2018-06-01 01:00'
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