print '' print '*** In file RetrieveEmployeeRolesByActive.sql'
USE [crlandscaping]
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