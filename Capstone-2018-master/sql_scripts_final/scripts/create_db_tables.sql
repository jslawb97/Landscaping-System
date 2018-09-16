IF EXISTS(SELECT 1 FROM master.dbo.sysdatabases WHERE name = 'crlandscaping')
BEGIN
	DROP DATABASE crlandscaping
	print '' print '*** Dropping database crlandscaping'
END
GO

print '' print '*** Creating database crlandscaping'
GO
CREATE DATABASE crlandscaping
GO

print '' print '*** Using database crlandscaping'
GO
USE [crlandscaping]
GO

SET QUOTED_IDENTIFIER ON
GO

/* ****************************************************************************************************************
   **************************************************** Tables ****************************************************
   **************************************************************************************************************** */

print '' print '*** Creating Customer Table'
GO
CREATE TABLE [Customer](
	[CustomerID]			[int] IDENTITY(1000000, 1)		NOT NULL,
	[CustomerTypeID]		[nvarchar](100)					NOT NULL,
	[Email]					[nvarchar](100)					NOT NULL,
	[FirstName]				[nvarchar](100)					NOT NULL,
	[LastName]				[nvarchar](100)					NOT NULL,
	[PhoneNumber]			[nvarchar](15)					NOT NULL,
	[PasswordHash]			[nvarchar](100)					NOT NULL DEFAULT
	'9c9064c59f1ffa2e174ee754d2979be80dd30db552ec03e7e327e9b1a4bd594e',
	[Active]				[bit]							NOT NULL DEFAULT 1,
	CONSTRAINT [pk_CustomerID] PRIMARY KEY([CustomerID] ASC),
	CONSTRAINT [ak_Customer_Email] UNIQUE ([Email] ASC)
)
GO

print '' print '*** Creating ServiceItem Table'
GO
CREATE TABLE [ServiceItem](
	[ServiceItemID]			[int] IDENTITY(1000000, 1)		NOT NULL,
	[Name]					[nvarchar](100)					NOT NULL,
	[Description]			[nvarchar](1000)				NOT NULL,
	[Active]				[bit]							NOT NULL DEFAULT 1,
	CONSTRAINT [pk_ServiceItemID] PRIMARY KEY([ServiceItemID] ASC),
	CONSTRAINT [ak_ServiceItem_Name] UNIQUE ([Name])
)
GO

print '' print '*** Creating PaymentType Table'
GO
CREATE TABLE [PaymentType](
	[PaymentTypeID]			[nvarchar](100)					NOT NULL,
	[Description]			[nvarchar](1000)				NOT NULL,
	[Active]				[bit]							NOT NULL DEFAULT 1,
	CONSTRAINT [pk_PaymentTypeID] PRIMARY KEY([PaymentTypeID] ASC)
)
GO

print '' print '*** Creating CustomerType Table'
GO
CREATE TABLE [CustomerType](
	[CustomerTypeID]		[nvarchar](100)					NOT NULL,
	CONSTRAINT [pk_CustomerTypeID] PRIMARY KEY([CustomerTypeID] ASC)
)
GO

print '' print '*** Creating JobLocation Table'
GO
CREATE TABLE [JobLocation](
	[JobLocationID]			[int] IDENTITY(1000000, 1)		NOT NULL,
	[CustomerID]			[int]							NOT NULL,
	[Street]				[nvarchar](100)					NOT NULL,
	[City]					[nvarchar](100)					NOT NULL,
	[State]					[nvarchar](2)					NOT NULL,
	[ZipCode]				[nvarchar](15)					NOT NULL,
	[Comments]				[nvarchar](1000)				NULL,
	[Active]				[bit]							NOT NULL DEFAULT 1,
	CONSTRAINT [pk_JobLocationID] PRIMARY KEY([JobLocationID] ASC)
)
GO

print '' print '*** Creating MakeModel Table'
GO
CREATE TABLE [MakeModel](
	[MakeModelID]			[int] IDENTITY(1000000, 1)		NOT NULL,
	[Make]					[nvarchar](100)					NOT NULL,
	[Model]					[nvarchar](100)					NOT NULL,
	[MaintenanceChecklistID] [int]							NULL,
	[Active]				[bit]							NOT NULL DEFAULT 1,
	CONSTRAINT [pk_MakeModelID] PRIMARY KEY([MakeModelID] ASC)
)
GO

print '' print '*** Creating SupplyOrderLine Table'
GO
CREATE TABLE [SupplyOrderLine](
	[SupplyOrderLineID]		[int] IDENTITY(1000000, 1)		NOT NULL,
	[SupplyItemID]			[int]							NULL,
	[Quantity]				[int]							NOT NULL,
	[SupplyOrderID]			[int]							NOT NULL,
	[QuantityReceived] 		[int] 							NOT NULL DEFAULT 0,
	CONSTRAINT [pk_SupplyOrderLineID] PRIMARY KEY([SupplyOrderLineID] ASC)
)
GO


print '' print '*** Creating EquipmentStatus Table'
GO
CREATE TABLE [EquipmentStatus](
	[EquipmentStatusID]		[nvarchar](100)					NOT NULL,
	CONSTRAINT [pk_EquipmentStatusID] PRIMARY KEY([EquipmentStatusID] ASC)
)
GO

print '' print '*** Creating MaintenanceRecord Table'
GO
CREATE TABLE [MaintenanceRecord](
	[MaintenanceRecordID]	[int] IDENTITY(1000000, 1)		NOT NULL,
	[EquipmentID]			[int]							NOT NULL,
	[EmployeeID]			[int]							NOT NULL,
	[Description]			[nvarchar](1000)				NOT NULL,
	[Date]					[date]							NOT NULL,
	CONSTRAINT [pk_MaintenanceRecordID] PRIMARY KEY([MaintenanceRecordID] ASC)
)
GO

print '' print '*** Creating InspectionRecord Table'
GO
CREATE TABLE [InspectionRecord](
	[InspectionRecordID]	[int] IDENTITY(1000000, 1)		NOT NULL,
	[EquipmentID]			[int]							NOT NULL,
	[EmployeeID]			[int]							NOT NULL,
	[Description]			[nvarchar](1000)				NOT NULL,
	[Date]					[date]							NOT NULL,
	CONSTRAINT [pk_InspectionRecordID] PRIMARY KEY([InspectionRecordID] ASC)
)
GO

print '' print '*** Creating PrepRecord Table'
GO
CREATE TABLE [PrepRecord](
	[PrepRecordID]			[int] IDENTITY(1000000, 1)		NOT NULL,
	[EquipmentID]			[int]							NOT NULL,
	[EmployeeID]			[int]							NOT NULL,
	[Description]			[nvarchar](1000)				NOT NULL,
	[Date]					[date]							NOT NULL,
	CONSTRAINT [pk_PrepRecordID] PRIMARY KEY([PrepRecordID] ASC)
)
GO

print '' print '*** Creating MaintenanceLine Table'
GO
CREATE TABLE [MaintenanceLine](
	[MaintenanceLineID]		[int] IDENTITY(1000000, 1)		NOT NULL,
	[MaintenanceRecordID]	[int]							NOT NULL,
	[SupplyItemID]			[int]							NOT NULL,
	[QuantityOfSupplies]	[int]							NOT NULL,
	CONSTRAINT [pk_MaintenanceLineID] PRIMARY KEY([MaintenanceLineID] ASC)
)
GO

print '' print '*** Creating SupplyItem Table'
GO
CREATE TABLE [SupplyItem](
	[SupplyItemID]			[int] IDENTITY(1000000, 1)		NOT NULL,
	[Name]					[nvarchar](100)					NOT NULL,
	[Description]			[nvarchar](1000)				NOT NULL,
	[Location]				[nvarchar](100)					NULL,
	[QuantityInStock]		[int]							NOT NULL DEFAULT 0,
	[ReorderLevel]			[int]							NOT NULL DEFAULT 0,
	[ReorderQuantity]		[int]							NOT NULL DEFAULT 0,
	[Active]				[bit]							NOT NULL DEFAULT 1,
	CONSTRAINT [pk_SupplyItemID] PRIMARY KEY([SupplyItemID] ASC)
)
GO

print '' print '*** Creating Vendor Table'
GO
CREATE TABLE [Vendor](
	[VendorID]				[int] IDENTITY(1000000, 1)		NOT NULL,
	[Name]					[nvarchar](100)					NOT NULL,
	[Rep]					[nvarchar](100)					NOT NULL,
	[Address]				[nvarchar](250)					NOT NULL,
	[Website]				[nvarchar](250)					NULL,
	[Phone]					[nvarchar](15)					NOT NULL,
	[Active]				[bit]							NOT NULL DEFAULT 1,
	CONSTRAINT [pk_VendorID] PRIMARY KEY([VendorID] ASC)
)
GO

print '' print '*** Creating Source Table'
GO
CREATE TABLE [Source](
	[SourceID]				[int] IDENTITY(1000000, 1)		NOT NULL,
	[SupplyItemID]			[int]							NULL,
	[SpecialOrderItemID]	[int]							NULL,
	[VendorID]				[int]							NOT NULL,
	[MinimumOrderQTY]		[int]							NOT NULL DEFAULT 1,
	[PriceEach]				[money]							NOT NULL DEFAULT 0,
	[LeadTime]				[int]							NOT NULL,
	[Active]				[bit]							NOT NULL DEFAULT 1,
	CONSTRAINT [pk_SourceID] PRIMARY KEY([SourceID] ASC)
)
GO

print '' print '*** Creating SupplyStatus Table'
GO
CREATE TABLE [SupplyStatus](
	[SupplyStatusID]		[nvarchar](100)					NOT NULL,
	CONSTRAINT [pk_SupplyStatusID] PRIMARY KEY([SupplyStatusID] ASC)
)
GO


print '' print '*** Creating SupplyOrder Table'
GO
CREATE TABLE [SupplyOrder](
	[SupplyOrderID]			[int] IDENTITY(1000000, 1)		NOT NULL,
	[EmployeeID]			[int]							NOT NULL,
	[JobID]					[int]							NULL,
	[SupplyStatusID]		[nvarchar](100)					NOT NULL,
	[Date]					[date]							NOT NULL,
	CONSTRAINT [pk_SupplyOrderID] PRIMARY KEY([SupplyOrderID] ASC)
)
GO

print '' print '*** Creating SpecialOrderItem Table'
GO
CREATE TABLE [SpecialOrderItem](
	[SpecialOrderItemID]	[int] IDENTITY(1000000, 1)		NOT NULL,
	[Name]					[nvarchar](100)					NOT NULL,
	[Active]				[bit]							NOT NULL DEFAULT 1,
	[QuantityInStock] 		[int] 							NOT NULL DEFAULT 0,
	CONSTRAINT [pk_SpecialOrderItemID] PRIMARY KEY([SpecialOrderItemID] ASC)
)
GO

print '' print '*** Creating SpecialOrderLine Table'
GO
CREATE TABLE [SpecialOrderLine](
	[SpecialOrderLineID]	[int] IDENTITY(1000000, 1)		NOT NULL,
	[SpecialOrderID]		[int]							NOT NULL,
	[SpecialOrderItemID]	[int]							NOT NULL,
	[Quantity]				[int]							NOT NULL,
	[QtyReceived] 			[int] 							NOT NULL DEFAULT 0,
	CONSTRAINT [pk_SpecialOrderLineID] PRIMARY KEY([SpecialOrderLineID] ASC)
)
GO

print '' print '*** Creating SpecialOrder Table'
GO
CREATE TABLE [SpecialOrder](
	[SpecialOrderID]		[int] IDENTITY(1000000, 1)		NOT NULL,
	[EmployeeID]			[int]							NOT NULL,
	[JobID]					[int]							NULL,
	[Date]					[date]							NOT NULL,
	[SupplyStatusID]		[nvarchar](100)					NOT NULL,
	[VendorID] 				[int] 							NULL,
	CONSTRAINT [pk_SpecialOrderID] PRIMARY KEY([SpecialOrderID] ASC)
)
GO

print '' print '*** Creating ResupplyOrder Table'
GO
CREATE TABLE [ResupplyOrder](
	[ResupplyOrderID]		[int] IDENTITY(1000000, 1)		NOT NULL,
	[EmployeeID]			[int]							NOT NULL,
	[Date]					[date]							NOT NULL,
	[SupplyStatusID]		[nvarchar](100)					NOT NULL,
	[VendorID]				[int]							NULL,
	CONSTRAINT [pk_ResupplyOrderID] PRIMARY KEY([ResupplyOrderID] ASC)
)
GO

print '' print '*** Creating ResupplyOrderLine Table'
GO
CREATE TABLE [ResupplyOrderLine](
	[ResupplyOrderLineID]	[int] IDENTITY(1000000, 1)		NOT NULL,
	[ResupplyOrderID]		[int]							NOT NULL,
	[SupplyItemID]			[int]							NOT NULL,
	[Quantity]				[int]							NOT NULL,
	[Price]					[money]							NOT NULL,
	[QtyReceived] 			[int] 							NOT NULL DEFAULT 0,
	CONSTRAINT [pk_ResupplyOrderLineID] PRIMARY KEY([ResupplyOrderLineID] ASC)
)
GO

print '' print '*** Creating Employee Table'
GO
CREATE TABLE [Employee](
	[EmployeeID]			[int] IDENTITY(1000000, 1)		NOT NULL,
	[FirstName]				[nvarchar](100)					NOT NULL,
	[LastName]				[nvarchar](100)					NOT NULL,
	[Address]				[nvarchar](250)					NOT NULL,
	[PhoneNumber]			[nvarchar](15)					NOT NULL,
	[Email]					[nvarchar](100)					NOT NULL,
	[PasswordHash]			[nvarchar](100)					NOT NULL DEFAULT
	'9c9064c59f1ffa2e174ee754d2979be80dd30db552ec03e7e327e9b1a4bd594e',
	[Active]				[bit]							NOT NULL DEFAULT 1,
	CONSTRAINT [pk_EmployeeID] PRIMARY KEY([EmployeeID] ASC),
	CONSTRAINT [ak_Employee_Email] UNIQUE([Email] ASC)
)
GO

print '' print '*** Creating EmployeeRole Table'
GO
CREATE TABLE [EmployeeRole](
	[EmployeeID]			[int]							NOT NULL,
	[RoleID]				[nvarchar](100)					NOT NULL,
	[Active]				[bit]							NOT NULL DEFAULT 1
	CONSTRAINT [pk_EmployeeID_RoleID] PRIMARY KEY([EmployeeID] ASC, [RoleID] ASC)
)
GO

print '' print '*** Creating Role Table'
GO
CREATE TABLE [Role](
	[RoleID]				[nvarchar](100)					NOT NULL,
	[Description]			[nvarchar](1000)				NOT NULL,
	CONSTRAINT [pk_RoleID] PRIMARY KEY([RoleID] ASC)
)
GO


print '' print '*** Creating EmployeeCertification Table'
GO
CREATE TABLE [EmployeeCertification](
	[CertificationID]		[int]							NOT NULL,
	[EmployeeID]			[int]							NOT NULL,
	[EndDate]				[datetime]						NOT NULL,
	[Active]				[bit]							NOT NULL DEFAULT 1,
	CONSTRAINT [pk_CertificationID_EmployeeID] PRIMARY KEY([CertificationID] ASC, [EmployeeID] ASC)
)
GO

print '' print '*** Creating Task Table'
GO
CREATE TABLE [Task](
	[TaskID]				[int] IDENTITY(1000000, 1)		NOT NULL,
	[TaskTypeID]			[int]							NOT NULL,
	[ServiceItemID]			[int]							NOT NULL,
	[Name]					[nvarchar](100)					NOT NULL,
	[Description]			[nvarchar](1000)				NULL,
	[Active]				[bit]							NOT NULL DEFAULT 1,
	CONSTRAINT [pk_TaskID] PRIMARY KEY([TaskID] ASC)

)
GO

/* 		Created 03/07/2018 - Zachary Hall		*/
print '' print '*** Creating TaskSupply Table'
GO
CREATE TABLE [TaskSupply](
	[TaskSupplyID] 			[int] 	IDENTITY(1000000, 1) 	NOT NULL,
	[TaskTypeSupplyNeedID] 	[int] 							NOT NULL,
	[SupplyItemID]			[int]							NULL,
	[Quantity]				[int]							NOT NULL,
	[JobID] 				[int] 							NOT NULL,
	[QuantityPulled] 		[int] 							NOT NULL DEFAULT 0,
	[JobSiteQuantityReceived] [int] 						NOT NULL DEFAULT 0,
	[JobSiteQuantityReturned] [int] 						NOT NULL DEFAULT 0,
	CONSTRAINT [pk_TaskSupply_TaskSupplyID] PRIMARY KEY([TaskSupplyID] ASC)
)
GO

/* 		Created 03/07/2018 - Zachary Hall		*/
print '' print '*** Creating TaskEquipment Table'
GO
CREATE TABLE [TaskEquipment](
	[TaskEquipmentID] 		[int] 	IDENTITY(1000000, 1) 	NOT NULL,
	[TaskTypeEquipmentNeedID] [int] 						NOT NULL,
	[EquipmentID]			[int]							NULL,
	[JobID] 				[int] 							NOT NULL,
	CONSTRAINT [pk_TaskEquipment_TaskEquipmentID] PRIMARY KEY([TaskEquipmentID] ASC)
)
GO

/* 		Created 03/07/2018 - Zachary Hall		*/
print '' print '*** Creating TaskEmployee Table'
GO
CREATE TABLE [TaskEmployee](
	[TaskEmployeeID] 		[int] 	IDENTITY(1000000, 1) 	NOT NULL,
	[TaskTypeEmployeeNeedID] [int] 							NOT NULL,
	[EmployeeID]			[int]							NULL,
	[JobID] 				[int] 							NOT NULL,
	CONSTRAINT [pk_TaskEmployee_TaskEmployeeID] PRIMARY KEY([TaskEmployeeID] ASC)
)
GO

/* 		Created 03/04/2018 - John Miller		*/
print '' print '*** Creating TaskType Table'
GO
CREATE TABLE [TaskType](
	[TaskTypeID]					[int] IDENTITY(1000000, 1)			NOT NULL,
	[Name]							[nvarchar](100)						NOT NULL,
	[Quantity]						[int]								NOT NULL,
	[JobLocationAttributeTypeID]	[nvarchar](100)						NOT NULL,
	[Active]						[bit]								NOT NULL DEFAULT 1,
	CONSTRAINT [pk_TaskTypeID] PRIMARY KEY([TaskTypeID] ASC)
)
GO

/* 		Created 03/07/2018 - Zachary Hall		*/
print '' print '*** Creating TaskTypeSupplyNeed Table'
GO
CREATE TABLE [TaskTypeSupplyNeed](
	[TaskTypeSupplyNeedID] 			[int] 	IDENTITY(1000000, 1) 		NOT NULL,
	[TaskTypeID]					[int]								NOT NULL,
	[SupplyItemID]					[int]								NOT NULL,
	[Quantity]						[int]								NOT NULL,
	[Active] 						[bit] 								NOT NULL DEFAULT 1,
	CONSTRAINT [pk_TaskTypeSupplyNeed_TaskTypeSupplyNeedID] PRIMARY KEY([TaskTypeSupplyNeedID] ASC)
)
GO

/* 		Created 03/07/2018 - Zachary Hall		*/
print '' print '*** Creating TaskTypeEquipmentNeed Table'
GO
CREATE TABLE [TaskTypeEquipmentNeed](
	[TaskTypeEquipmentNeedID] 		[int] 	IDENTITY(1000000, 1) 		NOT NULL,
	[TaskTypeID]					[int]								NOT NULL,
	[EquipmentTypeID]				[nvarchar](100)						NOT NULL,
	[HoursOfWork]					[int]								NOT NULL,
	CONSTRAINT [pk_TaskTypeEquipmentNeed_TaskTypeEquipmentNeedID] PRIMARY KEY([TaskTypeEquipmentNeedID] ASC)
)
GO

/* 		Created 03/07/2018 - Zachary Hall		*/
print '' print '*** Creating TaskTypeEmployeeNeed Table'
GO
CREATE TABLE [TaskTypeEmployeeNeed](
	[TaskTypeEmployeeNeedID] 		[int] 	IDENTITY(1000000, 1) 		NOT NULL,
	[TaskTypeID]					[int]								NOT NULL,
	[HoursOfWork]					[int]								NOT NULL,
	[Active] 						[bit] 								NOT NULL DEFAULT 1,
	CONSTRAINT [pk_TaskTypeEmployeeNeed_TaskTypeEmployeeNeedID] PRIMARY KEY([TaskTypeEmployeeNeedID] ASC)
)
GO

/* 		Created 03/04/2018 - John Miller		*/
print '' print '*** Creating JobLocationAttributeType Table'
GO
CREATE TABLE [JobLocationAttributeType](
	[JobLocationAttributeTypeID]	[nvarchar](100)						NOT NULL,
	CONSTRAINT [pk_JobLocationAttributeTypeID] PRIMARY KEY ([JobLocationAttributeTypeID] ASC)
)
GO

/* 		Created 03/07/2018 - Zachary Hall		*/
print '' print '*** Creating JobLocationAttribute Table'
GO
CREATE TABLE [JobLocationAttribute](
	[JobLocationID]					[int]								NOT NULL,
	[JobLocationAttributeTypeID]	[nvarchar](100)						NOT NULL,
	[Value]							[int]								NOT NULL,
	CONSTRAINT [pk_JobLocation_JobLocationID_JobLocationAttributeType_JobLocationAttributeTypeID] PRIMARY KEY ([JobLocationAttributeTypeID], [JobLocationID] ASC)
)
GO

print '' print '*** Creating Certification Table'
GO
CREATE TABLE [Certification](
	[CertificationID] 		[int] IDENTITY(1000000, 1)		NOT NULL,
	[Name]					[nvarchar](100)					NOT NULL,
	[Description]			[nvarchar](1000)				NULL,
	[Active]				[bit]							NOT NULL DEFAULT 1,
	CONSTRAINT [pk_CertificationID] PRIMARY KEY([CertificationID] ASC)
	
)
GO




print '' print '*** Creating Equipment Table'
GO
CREATE TABLE [Equipment](
	[EquipmentID] 			[int] IDENTITY(1000000, 1) 		NOT NULL,
	[EquipmentTypeID]		[nvarchar](100)					NOT NULL,
	[Name]					[nvarchar](100)					NOT NULL,
	[MakeModelID]			[int]							NOT NULL,
	[DatePurchased]			[date]							NOT NULL,
	[DateLastRepaired]		[date]							NULL,
	[PriceAtPurchase]		[money]							NOT NULL,
	[CurrentValue]			[money]			   				NOT NULL,
	[WarrantyUntil]			[date]							NULL,	
	[EquipmentStatusID]		[nvarchar](100)					NOT NULL,
	[EquipmentDetails]		[nvarchar](1000)				NOT NULL, 
	[Active]				[bit]							NOT NULL DEFAULT 1,
	CONSTRAINT [pk_EquipmentID] PRIMARY KEY([EquipmentID] ASC)
)
GO

print '' print '*** Creating TimeOff Table'
GO
CREATE TABLE [TimeOff](
	[TimeOffID] 			[int] IDENTITY(1000000, 1)		NOT NULL,
	[EmployeeID] 			[int] 							NOT NULL,
	[StartTime]				[DateTime]						NOT NULL,
	[EndTime]				[DateTime]						NOT NULL,
	[Approved]				[bit]							NOT NULL DEFAULT 0,
	[Active]				[bit]							NOT NULL DEFAULT 1,
	CONSTRAINT [pk_TimeOffID_EmployeeID] PRIMARY KEY([TimeOffID] ASC,[EmployeeID] ASC)
)
GO

print '' print '*** Creating MaintenanceChecklist Table'
GO
CREATE TABLE [MaintenanceChecklist](
	[MaintenanceChecklistID]	[int] IDENTITY(1000000, 1)	NOT NULL,
	[Name]					[nvarchar](100)					NOT NULL,
	[Description]			[nvarchar](1000)				NOT NULL,
	[Active]				[bit]							NOT NULL DEFAULT 1,
	CONSTRAINT [pk_MaintenanceChecklistID] PRIMARY KEY([MaintenanceChecklistID] ASC)
	
)
GO

print '' print '*** Creating Availability Table'
GO
CREATE TABLE [Availability](
	[AvailabilityID] 		[int] IDENTITY(1000000, 1)		NOT NULL,
	[EmployeeID] 			[int] 							NOT NULL,
	[StartTime]				[DateTime]						NOT NULL,
	[EndTime]				[DateTime]						NOT NULL,
	CONSTRAINT [pk_AvailabilityID_EmployeeID] PRIMARY KEY([AvailabilityID] ASC,[EmployeeID] ASC)
	
)
GO


print '' print '*** Creating EquipmentType Table'
GO
CREATE TABLE [EquipmentType](
	[EquipmentTypeID]		[nvarchar](100)					NOT NULL,
	[InspectionChecklistID]	[int]							NULL,
	[PrepChecklistID]		[int]							NULL,
	[Active]				[bit]							NOT NULL DEFAULT 1,
	[CertificationID] 		[int] 							NULL,
	CONSTRAINT [pk_EquipmentTypeID] PRIMARY KEY([EquipmentTypeID] ASC)
)
GO

print '' print '*** Creating PrepChecklist Table'
GO
CREATE TABLE [PrepChecklist](
	[PrepChecklistID]		[int] IDENTITY(1000000, 1)		NOT NULL,
	[Name]					[nvarchar](100)					NOT NULL,
	[Description]			[nvarchar](1000)				NOT NULL,
	[Active]				[bit]							NOT NULL DEFAULT 1,
	CONSTRAINT [pk_PrepChecklistID] PRIMARY KEY([PrepChecklistID] ASC)
)
GO

print '' print '*** Creating InspectionChecklist Table'
GO
CREATE TABLE [InspectionChecklist](
	[InspectionChecklistID]	[int] IDENTITY(1000000, 1)		NOT NULL,
	[Name]					[nvarchar](100)					NOT NULL,
	[Description]			[nvarchar](1000)				NOT NULL,
	[Active]				[bit]							NOT NULL DEFAULT 1,
	CONSTRAINT [pk_InspectionChecklistID] PRIMARY KEY([InspectionChecklistID] ASC)
)
GO

print '' print '*** Creating ServiceOffering Table'
GO
CREATE TABLE [ServiceOffering](
	[ServiceOfferingID] 	[int] IDENTITY (1000000, 1)		NOT NULL,
	[Name]					[nvarchar](100)					NOT NULL,
	[Description]			[nvarchar](1000)				NULL,
	CONSTRAINT [pk_ServiceOfferingID] PRIMARY KEY([ServiceOfferingID] ASC),
	CONSTRAINT [ak_ServiceOffering_Name] UNIQUE([Name] ASC)
)
GO

/* 		Created 03/07/2018 - Zachary Hall		*/
print '' print '*** Creating ServiceOfferingItem Table'
GO
CREATE TABLE [ServiceOfferingItem](
	[ServiceItemID] 		[int]							NOT NULL,
	[ServiceOfferingID]		[int]							NOT NULL,
	CONSTRAINT [pk_ServiceItem_ServiceItemID_ServiceOffering_ServiceOfferingID] PRIMARY KEY([ServiceItemID], [ServiceOfferingID] ASC)
)
GO

print '' print '*** Creating ServicePackage Table'
GO
CREATE TABLE [ServicePackage](
	[ServicePackageID] 		[int] IDENTITY (1000000, 1)		NOT NULL,
	[Name]					[nvarchar](100)					NOT NULL,
	[Description]			[nvarchar](1000)				NOT NULL,
	[Active]				[bit]							NOT NULL DEFAULT 1,	
	CONSTRAINT [pk_ServicePackageID] PRIMARY KEY([ServicePackageID] ASC),
	CONSTRAINT [ak_ServicePackage_Name] UNIQUE([Name] ASC)
)
GO

/* 		Created 03/07/2018 - Zachary Hall		*/
print '' print '*** Creating ServicePackageOffering Table'
GO
CREATE TABLE [ServicePackageOffering](
	[ServicePackageID] 		[int]							NOT NULL,
	[ServiceOfferingID]		[int]							NOT NULL,
	CONSTRAINT [pk_ServicePackage_ServicePackageID_ServiceOffering_ServiceOfferingID] PRIMARY KEY([ServicePackageID], [ServiceOfferingID] ASC)
)
GO

print '' print '*** Creating Job Table'
GO
CREATE TABLE [dbo].[Job](
	[JobID] 				[int] IDENTITY (1000000, 1)		NOT NULL,
	[CustomerID]			[int]							NOT NULL,
	[DateTimeScheduled]		[DateTime]						NULL,
	[EmployeeID]			[int]							NOT NULL DEFAULT 1000006,
	[JobLocationID]			[int]							NOT NULL,
	[Active]				[bit]							NOT NULL DEFAULT 1,
	[DateCompleted]			[DateTime]						NULL,
	[Comments]				[nvarchar](1000)				NOT NULL,
	[DateTimeTargetWindow] 	[DateTime] 						NULL,
	CONSTRAINT [pk_JobID] PRIMARY KEY([JobID] ASC)
)
GO

print '' print '*** Creating JobServicePackage Table'
GO
CREATE TABLE [dbo].[JobServicePackage](
	[JobID] 				[int] 	NOT NULL,
	[ServicePackageID]		[int]	NOT NULL,
	CONSTRAINT [pk_ServicePackage_ServicePackageID_Job_JobID] PRIMARY KEY([ServicePackageID], [JobID] ASC)
)
GO

print '' print '*** Creating custom table type AvailabilityTableType'
GO
CREATE TYPE [dbo].[AvailabilityTableType] AS TABLE
	([EmployeeID]	[int], [StartTime]	[datetime], [EndTime]	[datetime])
GO

print '' print '*** Creating JobServicePackageType'
GO
CREATE TYPE [JobServicePackageType] AS TABLE (
    [JobID]             [int],
    [ServicePackageID]       [int]
)
GO

print '' print '*** Creating JobLocationAttributeTableType'
GO
CREATE TYPE [JobLocationAttributeTableType] AS TABLE (
    [JobLocationID]             		[int],
    [JobLocationAttributeTypeID]       	[nvarchar](100),
	[Value]								[int]
)
GO


print '' print '*** Creating JobTask Table'
GO
CREATE TABLE [JobTask](
	[JobTaskID]				[int] IDENTITY(1000000, 1)		NOT NULL,
	[JobID]					[int]							NOT NULL,
	[TaskID]				[int]							NOT NULL,
	[IsDone]				[bit]							NOT NULL DEFAULT 0,
	CONSTRAINT [pk_JobTaskID] PRIMARY KEY([JobTaskID] ASC)
)
GO

print '' print '*** Creating custom table type SupplyOrderLineTableType'
GO
CREATE TYPE [dbo].[SupplyOrderLineReceivedTableType] AS TABLE
	([SupplyOrderLineID]	[int], [QuantityReceived]	[int])
GO


print '' print '*** Creating PersonalEquipment Table'
GO
CREATE TABLE [PersonalEquipment](
	[PersonalEquipmentID] 				[int] IDENTITY(1000000, 1) 		NOT NULL,
	[PersonalEquipmentType]				[nvarchar](100)					NOT NULL,
	[Name]								[nvarchar](100)					NOT NULL,
	[Description]						[nvarchar](1000)				NOT NULL,
	[PersonalEquipmentStatus]			[nvarchar](50)					NOT NULL,
	[Assigned]							[bit]							NOT NULL DEFAULT 0,
	CONSTRAINT [pk_PersonalEquipmentID] PRIMARY KEY([PersonalEquipmentID] ASC)
)
GO

print '' print '*** Creating EmplpoyeePersonalEquipmentAssignment Table'
GO
CREATE TABLE [EmployeePersonalEquipmentAssignment] ( 
	[PersonalEquipmentID]	[int]	NOT NULL,
	[EmployeeID]			[int]	NOT NULL,
	CONSTRAINT	[pk_PersonalEquipmentID_EmployeeID] PRIMARY KEY([PersonalEquipmentID] ASC, [EmployeeID] ASC)
)
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