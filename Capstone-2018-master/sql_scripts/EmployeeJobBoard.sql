/*Created by Zachary Hall 05/01/2018 */
print '' print '*** in file EmployeeJobBoard.sql ***'
USE [crlandscaping]
GO

print '' print '*** Creating EmployeeJobPost Table'
GO
CREATE TABLE [EmployeeJobPost](
	[EmployeeJobPostID]		[int] IDENTITY(1000000, 1)		NOT NULL,
	[PostEmployeeID]		[int]							NOT NULL,
	[JobID]					[int]							NOT NULL,
	[AcceptEmployeeID]		[int]							NULL,
	CONSTRAINT [pk_EmployeeJobPostID] PRIMARY KEY([EmployeeJobPostID] ASC),
	CONSTRAINT [ak_PostEmployeeID_JobID] UNIQUE ([PostEmployeeID], [JobID])
)
GO

print '' print '*** Creating EmployeeJobPost PostEmployeeID Foreign Key'
GO
ALTER TABLE [dbo].[EmployeeJobPost] WITH NOCHECK
	ADD CONSTRAINT [fk_EmployeeJobPost_PostEmployeeID] FOREIGN KEY([PostEmployeeID])
	REFERENCES [dbo].[Employee] ([EmployeeID])
	ON DELETE NO ACTION
	ON UPDATE NO ACTION
GO

print '' print '*** Creating EmployeeJobPost AcceptEmployeeID Foreign Key'
GO
ALTER TABLE [dbo].[EmployeeJobPost] WITH NOCHECK
	ADD CONSTRAINT [fk_EmployeeJobPost_AcceptEmployeeID] FOREIGN KEY([AcceptEmployeeID])
	REFERENCES [dbo].[Employee] ([EmployeeID])
	ON DELETE NO ACTION
	ON UPDATE NO ACTION
GO

print '' print '*** Creating EmployeeJobPost JobID Foreign Key'
GO
ALTER TABLE [dbo].[EmployeeJobPost] WITH NOCHECK
	ADD CONSTRAINT [fk_EmployeeJobPost_JobID] FOREIGN KEY([JobID])
	REFERENCES [dbo].[Job] ([JobID])
	ON DELETE NO ACTION
	ON UPDATE NO ACTION
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
