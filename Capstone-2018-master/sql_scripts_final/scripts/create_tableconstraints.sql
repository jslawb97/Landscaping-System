print '' print '*** In script create_tableconstraints.sql'
print '' print '*** Using database crlandscaping'
GO
USE [crlandscaping]
GO

/* ****************************************************************************************************************
   ************************************************* Foreign Keys *************************************************
   **************************************************************************************************************** */

print '' print '*** Creating Customer CustomerTypeID Foreign Key'
GO
ALTER TABLE [dbo].[Customer] WITH NOCHECK
	ADD CONSTRAINT [fk_Customer_CustomerTypeID] FOREIGN KEY([CustomerTypeID])
	REFERENCES [dbo].[CustomerType] ([CustomerTypeID])
	ON UPDATE CASCADE
GO

 
print '' print '*** Creating JobLocation CustomerID Foreign Key'
GO
ALTER TABLE [dbo].[JobLocation] WITH NOCHECK
	ADD CONSTRAINT [fk_JobLocation_CustomerID] FOREIGN KEY([CustomerID])
	REFERENCES [dbo].[Customer] ([CustomerID])
	ON UPDATE CASCADE
GO

print '' print '*** Creating MakeModel MaintenanceChecklistID Foreign Key'
GO
ALTER TABLE [dbo].[MakeModel] WITH NOCHECK
	ADD CONSTRAINT [fk_MakeModel_MaintenanceChecklistID] FOREIGN KEY([MaintenanceChecklistID])
	REFERENCES [dbo].[MaintenanceChecklist] ([MaintenanceChecklistID])
	ON UPDATE CASCADE
GO


print '' print '*** Creating SupplyOrderLine SupplyOrderID Foreign Key'
GO
ALTER TABLE [dbo].[SupplyOrderLine] WITH NOCHECK
	ADD CONSTRAINT [fk_SupplyOrderLine_SupplyOrderID] FOREIGN KEY([SupplyOrderID])
	REFERENCES [dbo].[SupplyOrder] ([SupplyOrderID])
	ON UPDATE CASCADE
GO


print '' print '*** Creating MaintenanceRecord EquipmentID Foreign Key'
GO
ALTER TABLE [dbo].[MaintenanceRecord] WITH NOCHECK
	ADD CONSTRAINT [fk_MaintenanceRecord_EquipmentID] FOREIGN KEY([EquipmentID])
	REFERENCES [dbo].[Equipment] ([EquipmentID])
	ON UPDATE CASCADE
GO

print '' print '*** Creating MaintenanceRecord EmployeeID Foreign Key'
GO
ALTER TABLE [dbo].[MaintenanceRecord] WITH NOCHECK
	ADD CONSTRAINT [fk_MaintenanceRecord_EmployeeID] FOREIGN KEY([EmployeeID])
	REFERENCES [dbo].[Employee] ([EmployeeID])
	ON UPDATE CASCADE
GO


print '' print '*** Creating InspectionRecord EquipmentID Foreign Key'
GO
ALTER TABLE [dbo].[InspectionRecord] WITH NOCHECK
	ADD CONSTRAINT [fk_InspectionRecord_EquipmentID] FOREIGN KEY([EquipmentID])
	REFERENCES [dbo].[Equipment] ([EquipmentID])
	ON UPDATE CASCADE
GO

print '' print '*** Creating InspectionRecord EmployeeID Foreign Key'
GO
ALTER TABLE [dbo].[InspectionRecord] WITH NOCHECK
	ADD CONSTRAINT [fk_InspectionRecord_EmployeeID] FOREIGN KEY([EmployeeID])
	REFERENCES [dbo].[Employee] ([EmployeeID])
	ON UPDATE CASCADE
GO


print '' print '*** Creating PrepRecord EquipmentID Foreign Key'
GO
ALTER TABLE [dbo].[PrepRecord] WITH NOCHECK
	ADD CONSTRAINT [fk_PrepRecord_EquipmentID] FOREIGN KEY([EquipmentID])
	REFERENCES [dbo].[Equipment] ([EquipmentID])
	ON UPDATE CASCADE
GO

print '' print '*** Creating PrepRecord EmployeeID Foreign Key'
GO
ALTER TABLE [dbo].[PrepRecord] WITH NOCHECK
	ADD CONSTRAINT [fk_PrepRecord_EmployeeID] FOREIGN KEY([EmployeeID])
	REFERENCES [dbo].[Employee] ([EmployeeID])
	ON UPDATE CASCADE
GO


print '' print '*** Creating MaintenanceLine MaintenanceRecordID Foreign Key'
GO
ALTER TABLE [dbo].[MaintenanceLine] WITH NOCHECK
	ADD CONSTRAINT [fk_MaintenanceLine_MaintenanceRecordID] FOREIGN KEY([MaintenanceRecordID])
	REFERENCES [dbo].[MaintenanceRecord] ([MaintenanceRecordID])
	ON UPDATE CASCADE
GO

print '' print '*** Creating MaintenanceLine SupplyItemID Foreign Key'
GO
ALTER TABLE [dbo].[MaintenanceLine] WITH NOCHECK
	ADD CONSTRAINT [fk_MaintenanceLine_SupplyItemID] FOREIGN KEY([SupplyItemID])
	REFERENCES [dbo].[SupplyItem] ([SupplyItemID])
	ON UPDATE CASCADE
GO


print '' print '*** Creating Source SupplyItemID Foreign Key'
GO
ALTER TABLE [dbo].[Source] WITH NOCHECK
	ADD CONSTRAINT [fk_Source_SupplyItemID] FOREIGN KEY([SupplyItemID])
	REFERENCES [dbo].[SupplyItem] ([SupplyItemID])
	ON UPDATE CASCADE
GO

print '' print '*** Creating Source SpecialOrderItemID Foreign Key'
GO
ALTER TABLE [dbo].[Source] WITH NOCHECK
	ADD CONSTRAINT [fk_Source_SpecialOrderItemID] FOREIGN KEY([SpecialOrderItemID])
	REFERENCES [dbo].[SpecialOrderItem] ([SpecialOrderItemID])
	ON UPDATE CASCADE
GO

print '' print '*** Creating Source VendorID Foreign Key'
GO
ALTER TABLE [dbo].[Source] WITH NOCHECK
	ADD CONSTRAINT [fk_Source_VendorID] FOREIGN KEY([VendorID])
	REFERENCES [dbo].[Vendor] ([VendorID])
	ON UPDATE CASCADE
GO





print '' print '*** Creating SupplyOrder EmployeeID Foreign Key'
GO
ALTER TABLE [dbo].[SupplyOrder] WITH NOCHECK
	ADD CONSTRAINT [fk_SupplyOrder_EmployeeID] FOREIGN KEY([EmployeeID])
	REFERENCES [dbo].[Employee] ([EmployeeID])
	ON UPDATE CASCADE
GO

print '' print '*** Creating SupplyOrder JobID Foreign Key'
GO
ALTER TABLE [dbo].[SupplyOrder] WITH NOCHECK
	ADD CONSTRAINT [fk_SupplyOrder_JobID] FOREIGN KEY([JobID])
	REFERENCES [dbo].[Job] ([JobID])
	ON UPDATE CASCADE
GO

print '' print '*** Creating SupplyOrder SupplyStatusID Foreign Key'
GO
ALTER TABLE [dbo].[SupplyOrder] WITH NOCHECK
	ADD CONSTRAINT [fk_SupplyOrder_SupplyStatusID] FOREIGN KEY([SupplyStatusID])
	REFERENCES [dbo].[SupplyStatus] ([SupplyStatusID])
	ON UPDATE CASCADE
GO



print '' print '*** Creating SpecialOrderLine SpecialOrderItemID Foreign Key'
GO
ALTER TABLE [dbo].[SpecialOrderLine] WITH NOCHECK
	ADD CONSTRAINT [fk_SpecialOrderLine_SpecialOrderItemID] FOREIGN KEY([SpecialOrderItemID])
	REFERENCES [dbo].[SpecialOrderItem] ([SpecialOrderItemID])
	ON UPDATE CASCADE
GO


print '' print '*** Creating SpecialOrder EmployeeID Foreign Key'
GO
ALTER TABLE [dbo].[SpecialOrder] WITH NOCHECK
	ADD CONSTRAINT [fk_SpecialOrder_EmployeeID] FOREIGN KEY([EmployeeID])
	REFERENCES [dbo].[Employee] ([EmployeeID])
	ON UPDATE NO ACTION
GO

print '' print '*** Creating SpecialOrder JobID Foreign Key'
GO
ALTER TABLE [dbo].[SpecialOrder] WITH NOCHECK
	ADD CONSTRAINT [fk_SpecialOrder_JobID] FOREIGN KEY([JobID])
	REFERENCES [dbo].[Job] ([JobID])
	ON UPDATE NO ACTION
GO

print '' print '*** Creating SpecialOrder SupplyStatusID Foreign Key'
GO
ALTER TABLE [dbo].[SpecialOrder] WITH NOCHECK
	ADD CONSTRAINT [fk_SpecialOrder_SupplyStatusID] FOREIGN KEY([SupplyStatusID])
	REFERENCES [dbo].[SupplyStatus] ([SupplyStatusID])
	ON UPDATE NO ACTION
GO


print '' print '*** Creating ResupplyOrder EmployeeID Foreign Key'
GO
ALTER TABLE [dbo].[ResupplyOrder] WITH NOCHECK
	ADD CONSTRAINT [fk_ResupplyOrder_EmployeeID] FOREIGN KEY([EmployeeID])
	REFERENCES [dbo].[Employee] ([EmployeeID])
	ON UPDATE NO ACTION
GO

print '' print '*** Creating ResupplyOrder SupplyStatusID Foreign Key'
GO
ALTER TABLE [dbo].[ResupplyOrder] WITH NOCHECK
	ADD CONSTRAINT [fk_ResupplyOrder_SupplyStatusID] FOREIGN KEY([SupplyStatusID])
	REFERENCES [dbo].[SupplyStatus] ([SupplyStatusID])
	ON UPDATE NO ACTION
GO


print '' print '*** Creating ResupplyOrderLine ResupplyOrderID Foreign Key'
GO
ALTER TABLE [dbo].[ResupplyOrderLine] WITH NOCHECK
	ADD CONSTRAINT [fk_ResupplyOrderLine_ResupplyOrderID] FOREIGN KEY([ResupplyOrderID])
	REFERENCES [dbo].[ResupplyOrder] ([ResupplyOrderID])
	ON UPDATE CASCADE
GO

print '' print '*** Creating ResupplyOrderLine SupplyItemID Foreign Key'
GO
ALTER TABLE [dbo].[ResupplyOrderLine] WITH NOCHECK
	ADD CONSTRAINT [fk_ResupplyOrderLine_SupplyItemID] FOREIGN KEY([SupplyItemID])
	REFERENCES [dbo].[SupplyItem] ([SupplyItemID])
	ON UPDATE NO ACTION
GO


print '' print '*** Creating EmployeeRole EmployeeID Foreign Key'
GO
ALTER TABLE [dbo].[EmployeeRole] WITH NOCHECK
	ADD CONSTRAINT [fk_EmployeeRole_EmployeeID] FOREIGN KEY([EmployeeID])
	REFERENCES [dbo].[Employee] ([EmployeeID])
	ON UPDATE CASCADE
GO

/*  *** Updated by Badis SAIDANI *** */
print '' print '*** Creating EmployeeRole RoleID Foreign Key'
GO
ALTER TABLE [dbo].[EmployeeRole] WITH NOCHECK
	ADD CONSTRAINT [fk_EmployeeRole_RoleID] FOREIGN KEY([RoleID])
	REFERENCES [dbo].[Role] ([RoleID])
	ON DELETE CASCADE
	ON UPDATE NO ACTION
GO



print '' print '*** Creating EmployeeCertification CertificationID Foreign Key'
GO
ALTER TABLE [dbo].[EmployeeCertification] WITH NOCHECK
	ADD CONSTRAINT [fk_EmployeeCertification_CertificationID] FOREIGN KEY([CertificationID])
	REFERENCES [dbo].[Certification] ([CertificationID])
	ON UPDATE CASCADE
GO

print '' print '*** Creating EmployeeCertification EmployeeID Foreign Key'
GO
ALTER TABLE [dbo].[EmployeeCertification] WITH NOCHECK
	ADD CONSTRAINT [fk_EmployeeCertification_EmployeeID] FOREIGN KEY([EmployeeID])
	REFERENCES [dbo].[Employee] ([EmployeeID])
	ON UPDATE CASCADE
GO

print '' print '*** Creating Task ServiceItemID Foreign Key'
GO
ALTER TABLE [dbo].[Task] WITH NOCHECK
	ADD CONSTRAINT [fk_Task_ServiceItemID] FOREIGN KEY([ServiceItemID]) 
	REFERENCES [dbo].[ServiceItem]([ServiceItemID])
	ON UPDATE CASCADE
GO





print '' print '*** Creating Equipment EquipmentTypeID Foreign Key'
GO
ALTER TABLE [dbo].[Equipment] WITH NOCHECK
	ADD CONSTRAINT [fk_Equipment_EquipmentTypeID] FOREIGN KEY([EquipmentTypeID]) 
	REFERENCES [dbo].[EquipmentType]([EquipmentTypeID])
	ON UPDATE CASCADE
GO

print '' print '*** Creating Equipment MakeModelID Foreign Key'
GO
ALTER TABLE [dbo].[Equipment] WITH NOCHECK
	ADD CONSTRAINT [fk_Equipment_MakeModelID] FOREIGN KEY([MakeModelID]) 
	REFERENCES [dbo].[MakeModel]([MakeModelID])
	ON UPDATE CASCADE
GO

print '' print '*** Creating Equipment EquipmentStatusID Foreign Key'
GO
ALTER TABLE [dbo].[Equipment] WITH NOCHECK
	ADD CONSTRAINT [fk_Equipment_EquipmentStatusID] FOREIGN KEY([EquipmentStatusID]) 
	REFERENCES [dbo].[EquipmentStatus]([EquipmentStatusID])
	ON UPDATE CASCADE
GO


print '' print '*** Creating TimeOff EmployeeID Foreign Key'
GO
ALTER TABLE [dbo].[TimeOff] WITH NOCHECK
	ADD CONSTRAINT [fk_TimeOff_EmployeeID] FOREIGN KEY([EmployeeID]) 
	REFERENCES [dbo].[Employee]([EmployeeID])
	ON UPDATE NO ACTION
GO


print '' print '*** Creating Availability EmployeeID Foreign Key'
GO
ALTER TABLE [dbo].[Availability] WITH NOCHECK
	ADD CONSTRAINT [fk_Availability_EmployeeID] FOREIGN KEY([EmployeeID]) 
	REFERENCES [dbo].[Employee]([EmployeeID])
	ON UPDATE CASCADE
GO


print '' print '*** Creating EquipmentType InspectionChecklistID Foreign Key'
GO
ALTER TABLE [dbo].[EquipmentType] WITH NOCHECK
	ADD CONSTRAINT [fk_EquipmentType_InspectionChecklistID] FOREIGN KEY([InspectionChecklistID]) 
	REFERENCES [dbo].[InspectionChecklist]([InspectionChecklistID])
	ON UPDATE CASCADE
GO

print '' print '*** Creating EquipmentType PrepChecklistID Foreign Key'
GO
ALTER TABLE [dbo].[EquipmentType] WITH NOCHECK
	ADD CONSTRAINT [fk_EquipmentType_PrepChecklistID] FOREIGN KEY([PrepChecklistID]) 
	REFERENCES [dbo].[PrepChecklist]([PrepChecklistID])
	ON UPDATE CASCADE
GO


print '' print '*** Creating Job CustomerID foreign key'
GO
ALTER TABLE [dbo].[Job]  WITH NOCHECK 
	ADD CONSTRAINT [FK_Job_CustomerID] FOREIGN KEY([CustomerID])
	REFERENCES [dbo].[Customer] ([CustomerID])
	ON UPDATE CASCADE
GO
/* 
print '' print '*** Creating Job ServicePackageID foreign key'
GO
ALTER TABLE [dbo].[Job]  WITH NOCHECK 
	ADD CONSTRAINT [FK_Job_ServicePackageID] FOREIGN KEY([ServicePackageID])
	REFERENCES [dbo].[ServicePackage] ([ServicePackageID])
	ON UPDATE NO ACTION
GO
 */
print '' print '*** Creating Job EmployeeID foreign key'
GO
ALTER TABLE [dbo].[Job]  WITH NOCHECK 
	ADD CONSTRAINT [FK_Job_EmployeeID] FOREIGN KEY([EmployeeID])
	REFERENCES [dbo].[Employee] ([EmployeeID])
	ON UPDATE NO ACTION
GO

print '' print '*** Creating Job JobLocationID foreign key'
GO
ALTER TABLE [dbo].[Job]  WITH NOCHECK 
	ADD CONSTRAINT [FK_Job_JobLocationID] FOREIGN KEY([JobLocationID])
	REFERENCES [dbo].[JobLocation] ([JobLocationID])
	ON UPDATE NO ACTION
GO

print '' print '*** Creating JobServicePackage ServicePackageID Foreign Key'
GO
ALTER TABLE [dbo].[JobServicePackage] WITH NOCHECK
	ADD CONSTRAINT [fk_ServicePackage_ServicePackageID] FOREIGN KEY([ServicePackageID])
	REFERENCES [dbo].[ServicePackage] ([ServicePackageID])
	ON UPDATE CASCADE
GO

print '' print '*** Creating JobServicePackage JobID Foreign Key'
GO
ALTER TABLE [dbo].[JobServicePackage] WITH NOCHECK
	ADD CONSTRAINT [fk_Job_JobID] FOREIGN KEY([JobID])
	REFERENCES [dbo].[Job] ([JobID])
	ON UPDATE CASCADE
GO


print '' print '*** Creating TaskSupply SupplyItemID Foreign Key'
GO
ALTER TABLE [dbo].[TaskSupply] WITH NOCHECK
	ADD CONSTRAINT [fk_TaskSupply_SupplyItemID] FOREIGN KEY([SupplyItemID])
	REFERENCES [dbo].[SupplyItem] ([SupplyItemID])
	ON UPDATE CASCADE
GO



print '' print '*** Creating TaskEquipment EquipmentID Foreign Key'
GO
ALTER TABLE [dbo].[TaskEquipment] WITH NOCHECK
	ADD CONSTRAINT [fk_TaskEquipment_EquipmentID] FOREIGN KEY([EquipmentID])
	REFERENCES [dbo].[Equipment] ([EquipmentID])
	ON UPDATE CASCADE
GO



print '' print '*** Creating TaskEmployee EmployeeID Foreign Key'
GO
ALTER TABLE [dbo].[TaskEmployee] WITH NOCHECK
	ADD CONSTRAINT [fk_TaskEmployee_EmployeeID] FOREIGN KEY([EmployeeID])
	REFERENCES [dbo].[Employee] ([EmployeeID])
	ON UPDATE CASCADE
GO

print '' print '*** Creating ServicePackageOffering ServicePackageID Foreign Key'
GO
ALTER TABLE [dbo].[ServicePackageOffering] WITH NOCHECK
	ADD CONSTRAINT [fk_ServicePackageOffering_ServicePackageID] FOREIGN KEY([ServicePackageID])
	REFERENCES [dbo].[ServicePackage] ([ServicePackageID])
	ON UPDATE NO ACTION ON DELETE NO ACTION
GO

print '' print '*** Creating ServicePackageOffering ServiceOfferingID Foreign Key'
GO
ALTER TABLE [dbo].[ServicePackageOffering] WITH NOCHECK
	ADD CONSTRAINT [fk_ServicePackageOffering_ServiceOfferingID] FOREIGN KEY([ServiceOfferingID])
	REFERENCES [dbo].[ServiceOffering] ([ServiceOfferingID])
	ON UPDATE NO ACTION ON DELETE NO ACTION
GO

print '' print '*** Creating ServiceOfferingItem ServiceOfferingID Foreign Key'
GO
ALTER TABLE [dbo].[ServiceOfferingItem] WITH NOCHECK
	ADD CONSTRAINT [fk_ServiceOfferingItem_ServiceOfferingID] FOREIGN KEY([ServiceOfferingID])
	REFERENCES [dbo].[ServiceOffering] ([ServiceOfferingID])
	ON UPDATE NO ACTION ON DELETE NO ACTION
GO

print '' print '*** Creating ServiceOfferingItem ServiceItemID Foreign Key'
GO
ALTER TABLE [dbo].[ServiceOfferingItem] WITH NOCHECK
	ADD CONSTRAINT [fk_ServiceOfferingItem_ServiceItemID] FOREIGN KEY([ServiceItemID])
	REFERENCES [dbo].[ServiceItem] ([ServiceItemID])
	ON UPDATE NO ACTION ON DELETE NO ACTION
GO

print '' print '*** Creating JobLocationAttribute JobLocationAttributeTypeID Foreign Key'
GO
ALTER TABLE [dbo].[JobLocationAttribute] WITH NOCHECK
	ADD CONSTRAINT [fk_JobLocationAttribute_JobLocationAttributeTypeID] FOREIGN KEY([JobLocationAttributeTypeID])
	REFERENCES [dbo].[JobLocationAttributeType] ([JobLocationAttributeTypeID])
	ON UPDATE CASCADE
GO

print '' print '*** Creating JobLocationAttribute JobLocationID Foreign Key'
GO
ALTER TABLE [dbo].[JobLocationAttribute] WITH NOCHECK
	ADD CONSTRAINT [fk_JobLocationAttribute_JobLocationID] FOREIGN KEY([JobLocationID])
	REFERENCES [dbo].[JobLocation] ([JobLocationID])
	ON UPDATE CASCADE
GO

print '' print '*** Creating TaskTypeSupplyNeed TaskTypeID Foreign Key'
GO
ALTER TABLE [dbo].[TaskTypeSupplyNeed] WITH NOCHECK
	ADD CONSTRAINT [fk_TaskTypeSupplyNeed_TaskTypeID] FOREIGN KEY([TaskTypeID])
	REFERENCES [dbo].[TaskType] ([TaskTypeID])
	ON UPDATE CASCADE
GO

print '' print '*** Creating TaskTypeSupplyNeed SupplyItemID Foreign Key'
GO
ALTER TABLE [dbo].[TaskTypeSupplyNeed] WITH NOCHECK
	ADD CONSTRAINT [fk_TaskTypeSupplyNeed_SupplyItemID] FOREIGN KEY([SupplyItemID])
	REFERENCES [dbo].[SupplyItem] ([SupplyItemID])
	ON UPDATE CASCADE
GO

print '' print '*** Creating TaskTypeEquipmentNeed TaskTypeID Foreign Key'
GO
ALTER TABLE [dbo].[TaskTypeEquipmentNeed] WITH NOCHECK
	ADD CONSTRAINT [fk_TaskTypeEquipmentNeed_TaskTypeID] FOREIGN KEY([TaskTypeID])
	REFERENCES [dbo].[TaskType] ([TaskTypeID])
	ON UPDATE CASCADE
GO

print '' print '*** Creating TaskTypeEquipmentNeed TaskTypeID Foreign Key'
GO
ALTER TABLE [dbo].[TaskTypeEquipmentNeed] WITH NOCHECK
	ADD CONSTRAINT [fk_TaskTypeEquipmentNeed_EquipmentTypeID] FOREIGN KEY([EquipmentTypeID])
	REFERENCES [dbo].[EquipmentType] ([EquipmentTypeID])
	ON UPDATE CASCADE
GO

print '' print '*** Creating TaskTypeEmployeeNeed TaskTypeID Foreign Key'
GO
ALTER TABLE [dbo].[TaskTypeEmployeeNeed] WITH NOCHECK
	ADD CONSTRAINT [fk_TaskTypeEmployeeNeed_TaskTypeID] FOREIGN KEY([TaskTypeID])
	REFERENCES [dbo].[TaskType] ([TaskTypeID])
	ON UPDATE CASCADE
GO

print '' print '*** Creating TaskSupply JobID Foreign Key'
GO
ALTER TABLE [dbo].[TaskSupply] WITH NOCHECK
	ADD CONSTRAINT [fk_TaskSupply_JobID] FOREIGN KEY([JobID]) 
	REFERENCES [dbo].[Job]([JobID])
	ON UPDATE CASCADE
GO

print '' print '*** Creating TaskEquipment JobID Foreign Key'
GO
ALTER TABLE [dbo].[TaskEquipment] WITH NOCHECK
	ADD CONSTRAINT [fk_TaskEquipment_JobID] FOREIGN KEY([JobID]) 
	REFERENCES [dbo].[Job]([JobID])
	ON UPDATE CASCADE
GO

print '' print '*** Creating TaskEmployee JobID Foreign Key'
GO
ALTER TABLE [dbo].[TaskEmployee] WITH NOCHECK
	ADD CONSTRAINT [fk_TaskEmployee_JobID] FOREIGN KEY([JobID]) 
	REFERENCES [dbo].[Job]([JobID])
	ON UPDATE CASCADE
GO

print '' print '*** Creating EquipmentType CertificationID Foreign Key'
GO
ALTER TABLE [dbo].[EquipmentType] WITH NOCHECK
	ADD CONSTRAINT [fk_EquipmentType_CertificationID] FOREIGN KEY([CertificationID]) 
	REFERENCES [dbo].[Certification]([CertificationID])
	ON UPDATE CASCADE
GO	

print '' print '*** Creating TaskSupply TaskTypeSupplyNeedID Foreign Key'
GO
ALTER TABLE [TaskSupply]
ADD CONSTRAINT [fk_TaskSupply_TaskTypeSupplyNeedID] FOREIGN KEY([TaskTypeSupplyNeedID]) 
	REFERENCES [dbo].[TaskTypeSupplyNeed]([TaskTypeSupplyNeedID])
	ON UPDATE NO ACTION
GO

print '' print '*** Creating TaskEquipment TaskTypeEquipmentNeedID Foreign Key'
GO
ALTER TABLE [TaskEquipment]
ADD CONSTRAINT [fk_TaskEquipment_TaskTypeEquipmentNeedID] FOREIGN KEY([TaskTypeEquipmentNeedID]) 
	REFERENCES [dbo].[TaskTypeEquipmentNeed]([TaskTypeEquipmentNeedID])
	ON UPDATE NO ACTION
GO

print '' print '*** Creating TaskEmployee TaskTypeEmployeeNeedID Foreign Key'
GO
ALTER TABLE [TaskEmployee]
ADD CONSTRAINT [fk_TaskEmployee_TaskTypeEquipmentNeedID] FOREIGN KEY([TaskTypeEmployeeNeedID]) 
	REFERENCES [dbo].[TaskTypeEmployeeNeed]([TaskTypeEmployeeNeedID])
	ON UPDATE NO ACTION
GO

print '' print '*** Creating JobTask JobID Foreign Key'
GO
ALTER TABLE [dbo].[JobTask] WITH NOCHECK
	ADD CONSTRAINT [fk_JobTask_JobID] FOREIGN KEY([JobID])
	REFERENCES [dbo].[Job] ([JobID])
	ON UPDATE CASCADE
GO

print '' print '*** Creating JobTask TaskID Foreign Key'
GO
ALTER TABLE [dbo].[JobTask] WITH NOCHECK
	ADD CONSTRAINT [fk_JobTask_TaskID] FOREIGN KEY([TaskID])
	REFERENCES [dbo].[Task] ([TaskID])
	ON UPDATE CASCADE
GO


print '' print '*** Creating SpecialOrder VendorID Foreign Key'
GO
ALTER TABLE [dbo].[SpecialOrder] WITH NOCHECK
	ADD CONSTRAINT [fk_SpecialOrder_VendorID] FOREIGN KEY ([VendorID])
	REFERENCES [dbo].[Vendor]([VendorID])
	ON UPDATE CASCADE
GO

print '' print '*** Creating ResupplyOrder VendorID Foreign Key'
GO
ALTER TABLE [dbo].[ResupplyOrder] WITH NOCHECK
	ADD CONSTRAINT [fk_ResupplyOrder_VendorID] FOREIGN KEY([VendorID])
	REFERENCES [dbo].[Vendor] ([VendorID])
	ON UPDATE NO ACTION
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

/* ****************************************************************************************************************
   **************************************************** Indexes ***************************************************
   **************************************************************************************************************** */
   
print '' print '*** Creating EmployeeRole RoleID Index'
GO
CREATE NONCLUSTERED INDEX ix_EmployeeRole_RoleID
	ON [EmployeeRole] ([RoleID])
GO


print '' print '*** Creating Employee FirstName Index'
GO
CREATE NONCLUSTERED INDEX ix_Employee_FirstName
	ON [Employee] ([FirstName])
GO

print '' print '*** Creating Employee LastName Index'
GO
CREATE NONCLUSTERED INDEX ix_Employee_LastName
	ON [Employee] ([LastName])
GO


print '' print '*** Creating ResupplyOrder SupplyStatusID Index'
GO
CREATE NONCLUSTERED INDEX ix_ResupplyOrder_SupplyStatusID
	ON [ResupplyOrder] ([SupplyStatusID])
GO


print '' print '*** Creating SpecialOrder SupplyStatusID Index'
GO
CREATE NONCLUSTERED INDEX ix_SpecialOrder_SupplyStatusID
	ON [SpecialOrder] ([SupplyStatusID])
GO




print '' print '*** Creating Vendor Name Index'
GO
CREATE NONCLUSTERED INDEX ix_Vendor_Name
	ON [Vendor] ([Name])
GO


print '' print '*** Creating SupplyItem Name Index'
GO
CREATE NONCLUSTERED INDEX ix_SupplyItem_Name
	ON [SupplyItem] ([Name])
GO


print '' print '*** Creating Customer FirstName Index'
GO
CREATE NONCLUSTERED INDEX ix_Customer_FirstName
	ON [Customer] ([FirstName])
GO

print '' print '*** Creating Customer LastName Index'
GO
CREATE NONCLUSTERED INDEX ix_Customer_LastName
	ON [Customer] ([LastName])
GO



print '' print '*** Creating Certification Name Index'
GO
CREATE NONCLUSTERED INDEX ix_Certification_Name
	ON [Certification] ([Name])
GO


print '' print '*** Creating Equipment EquipmentTypeID Index'
GO
CREATE NONCLUSTERED INDEX ix_Equipment_EquipmentTypeID
	ON [Equipment] ([EquipmentTypeID])
GO

print '' print '*** Creating Equipment EquipmentStatusID Index'
GO
CREATE NONCLUSTERED INDEX ix_Equipment_EquipmentStatusID
	ON [Equipment] ([EquipmentStatusID])
GO

print '' print '*** Creating VendorID SupplyItemID Unique Key'
GO
SET QUOTED_IDENTIFIER ON
CREATE UNIQUE NONCLUSTERED INDEX [ix_VendorID_SupplyItemID]
ON [Source] ([VendorID], [SupplyItemID])
WHERE [SupplyItemID] IS NOT NULL
GO

print '' print '*** Creating VendorID SpecialOrderItemID Unique Key'
GO
SET QUOTED_IDENTIFIER ON
CREATE UNIQUE NONCLUSTERED INDEX [ix_VendorID_SpecialOrderItemID]
ON [Source] ([VendorID], [SpecialOrderItemID])
WHERE [SpecialOrderItemID] IS NOT NULL
GO


