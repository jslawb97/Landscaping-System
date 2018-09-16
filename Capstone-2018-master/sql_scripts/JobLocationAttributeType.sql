/* Brady Feller */
print '' print '*** in file JobLocationAttributeType.sql ***'
USE [crlandscaping]
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
/* End Brady Feller */