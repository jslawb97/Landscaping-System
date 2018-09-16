/* Jacob Conley */
print '' print '*** in file ServiceOfferingItemSPs.sql ***'
print '' print '*** Using database crlandscaping'
GO
USE [crlandscaping]
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