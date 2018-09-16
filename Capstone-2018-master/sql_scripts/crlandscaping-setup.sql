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
	CONSTRAINT [pk_SupplyOrderLineID] PRIMARY KEY([SupplyOrderLineID] ASC)
)
GO

print '' print '*** Creating EquipmentSchedule Table'
GO
CREATE TABLE [EquipmentSchedule](
	[EquipmentScheduleID]	[int] IDENTITY(1000000, 1)		NOT NULL,
	[JobID]					[int]							NOT NULL,
	[EquipmentID]			[int]							NOT NULL,
	[StartDate]				[datetime]						NOT NULL,
	[EndDate]				[datetime]						NOT NULL,
	CONSTRAINT [pk_EquipmentScheduleID] PRIMARY KEY([EquipmentScheduleID] ASC)
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

print '' print '*** Creating ExceptionLine Table'
GO
CREATE TABLE [ExceptionLine](
	[ExceptionLineID]		[int] IDENTITY(1000000, 1)		NOT NULL,
	[SupplyOrderID]			[int]							NULL,
	[ResupplyOrderID]		[int]							NULL,
	[SpecialOrderID]		[int]							NULL,
	[ExceptionTypeID]		[nvarchar](100)					NOT NULL,
	[Quantity]				[int]							NOT NULL,
	CONSTRAINT [pk_ExceptionLineID] PRIMARY KEY([ExceptionLineID] ASC)
)
GO

print '' print '*** Creating ExceptionType Table'
GO
CREATE TABLE [ExceptionType](
	[ExceptionTypeID]		[nvarchar](100)					NOT NULL,
	CONSTRAINT [pk_ExceptionTypeID] PRIMARY KEY([ExceptionTypeID] ASC)
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

print '' print '*** Creating Delivery Table'
GO
CREATE TABLE [Delivery](
	[DeliveryID]			[int] IDENTITY(1000000, 1)		NOT NULL,
	[Address]				[nvarchar](250)					NOT NULL,
	[Time]					[datetime]						NOT NULL,
	[SupplyOrderID]			[int]							NOT NULL,
	[SupplyStatusID]		[nvarchar](100)					NOT NULL,
	CONSTRAINT [pk_DeliveryID] PRIMARY KEY([DeliveryID] ASC)
)
GO

print '' print '*** Creating SpecialOrderItem Table'
GO
CREATE TABLE [SpecialOrderItem](
	[SpecialOrderItemID]	[int] IDENTITY(1000000, 1)		NOT NULL,
	[Name]					[nvarchar](100)					NOT NULL,
	[Active]				[bit]							NOT NULL DEFAULT 1,
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
	[VendorID]				[int]							NOT NULL,
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

print '' print '*** Creating Group Table'
GO
CREATE TABLE [Group](
	[GroupID]				[int] IDENTITY(1000000, 1)		NOT NULL,
	[JobID]					[int]							NOT NULL,
	CONSTRAINT [pk_GroupID] PRIMARY KEY([GroupID] ASC)
)
GO

print '' print '*** Creating EmployeeGroup Table'
GO
CREATE TABLE [EmployeeGroup](
	[GroupID]				[int]							NOT NULL,
	[EmployeeID]			[int]							NOT NULL,
	CONSTRAINT [pk_GroupID_EmployeeID] PRIMARY KEY([GroupID] ASC, [EmployeeID] ASC)
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
	[TaskID]				[int] 							NOT NULL,
	[SupplyItemID]			[int]							NOT NULL,
	[Quantity]				[int]							NOT NULL,
	CONSTRAINT [pk_Task_TaskID_SupplyItem_SupplyItemID] PRIMARY KEY([TaskID], [SupplyItemID] ASC)
)
GO

/* 		Created 03/07/2018 - Zachary Hall		*/
print '' print '*** Creating TaskEquipment Table'
GO
CREATE TABLE [TaskEquipment](
	[TaskID]				[int] 							NOT NULL,
	[EquipmentID]			[int]							NOT NULL,
	[Quantity]				[int]							NOT NULL,
	CONSTRAINT [pk_Task_TaskID_Equipment_EqupmentID] PRIMARY KEY([TaskID], [EquipmentID] ASC)
)
GO

/* 		Created 03/07/2018 - Zachary Hall		*/
print '' print '*** Creating TaskEmployee Table'
GO
CREATE TABLE [TaskEmployee](
	[TaskID]				[int] 							NOT NULL,
	[EmployeeID]			[int]							NOT NULL,
	CONSTRAINT [pk_Task_TaskID_Employee_EmployeeID] PRIMARY KEY([TaskID], [EmployeeID] ASC)
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
	[TaskTypeID]					[int]								NOT NULL,
	[SupplyItemID]					[int]								NOT NULL,
	[Quantity]						[int]								NOT NULL,
	CONSTRAINT [pk_TaskTypeSupplyNeed_TaskTypeID] PRIMARY KEY([TaskTypeID] ASC)
)
GO

/* 		Created 03/07/2018 - Zachary Hall		*/
print '' print '*** Creating TaskTypeEquipmentNeed Table'
GO
CREATE TABLE [TaskTypeEquipmentNeed](
	[TaskTypeID]					[int]								NOT NULL,
	[EquipmentTypeID]				[nvarchar](100)						NOT NULL,
	[HoursOfWork]					[int]								NOT NULL,
	CONSTRAINT [pk_TaskType_TaskTypeID_EquipmentType_EquipmentTypeID] PRIMARY KEY([TaskTypeID],[EquipmentTypeID] ASC)
)
GO

/* 		Created 03/07/2018 - Zachary Hall		*/
print '' print '*** Creating TaskTypeEmployeeNeed Table'
GO
CREATE TABLE [TaskTypeEmployeeNeed](
	[TaskTypeID]					[int]								NOT NULL,
	[HoursOfWork]					[int]								NOT NULL,
	CONSTRAINT [pk_TaskType_TaskTypeID] PRIMARY KEY([TaskTypeID] ASC)
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

print '' print '*** Creating Message Table'
GO
CREATE TABLE [Message](
	[MessageID] 			[int] IDENTITY(1000000, 1)		NOT NULL,
	[ToEmployeeID]			[int] 							NOT NULL,
	[FromEmployeeID]		[int]		 					NOT NULL,
	[SendDate]				[date]							NOT NULL,
	[Subject]				[nvarchar](100)					NOT NULL,
	[Message]				[nvarchar](1000)				NOT NULL,
	[Read]					[bit]							NOT NULL DEFAULT 0,
	[Active]				[bit]							NOT NULL DEFAULT 1,
	CONSTRAINT [pk_MessageID] PRIMARY KEY([MessageID] ASC)
	
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

print '' print '*** Creating Applicant Table'
GO
CREATE TABLE [Applicant](
	[ApplicantID]			[int] IDENTITY(1000000, 1)		NOT NULL,
	[FirstName]				[nvarchar](100)					NOT NULL,
	[LastName]				[nvarchar](100)					NOT NULL,
	[Address]				[nvarchar](250)					NOT NULL,
	[PhoneNumber]			[nvarchar](15)					NOT NULL,
	[Email]					[nvarchar](100)					NOT NULL,
	[Active]				[bit]							NOT NULL DEFAULT 1,
	CONSTRAINT [pk_ApplicantID] PRIMARY KEY([ApplicantID] ASC)
)
GO

print '' print '*** Creating EquipmentType Table'
GO
CREATE TABLE [EquipmentType](
	[EquipmentTypeID]		[nvarchar](100)					NOT NULL,
	[InspectionChecklistID]	[int]							NULL,
	[PrepChecklistID]		[int]							NULL,
	[Active]				[bit]							NOT NULL DEFAULT 1,
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
	[DateTimeScheduled]		[DateTime]						NOT NULL,
	[EmployeeID]			[int]							NOT NULL, 
	[JobLocationID]			[int]							NOT NULL,
	[Active]				[bit]							NOT NULL DEFAULT '1',
	[DateCompleted]			[DateTime]						NULL,
	[Comments]				[nvarchar](1000)				NOT NULL,
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

/*
print '' print '*** Creating SupplyOrderLine SpecialOrderItemID Foreign Key'
GO
ALTER TABLE [dbo].[SupplyOrderLine] WITH NOCHECK
	ADD CONSTRAINT [fk_SupplyOrderLine_SpecialOrderItemID] FOREIGN KEY([SupplyItemID])
	REFERENCES [dbo].[SpecialOrderItem] ([SpecialOrderItemID])
	ON UPDATE CASCADE
GO
*/

print '' print '*** Creating SupplyOrderLine SupplyOrderID Foreign Key'
GO
ALTER TABLE [dbo].[SupplyOrderLine] WITH NOCHECK
	ADD CONSTRAINT [fk_SupplyOrderLine_SupplyOrderID] FOREIGN KEY([SupplyOrderID])
	REFERENCES [dbo].[SupplyOrder] ([SupplyOrderID])
	ON UPDATE CASCADE
GO


print '' print '*** Creating EquipmentSchedule JobID Foreign Key'
GO
ALTER TABLE [dbo].[EquipmentSchedule] WITH NOCHECK
	ADD CONSTRAINT [fk_EquipmentSchedule_JobID] FOREIGN KEY([JobID])
	REFERENCES [dbo].[Job] ([JobID])
	ON UPDATE CASCADE
GO

print '' print '*** Creating EquipmentSchedule EquipmentID Foreign Key'
GO
ALTER TABLE [dbo].[EquipmentSchedule] WITH NOCHECK
	ADD CONSTRAINT [fk_EquipmentSchedule_EquipmentID] FOREIGN KEY([EquipmentID])
	REFERENCES [dbo].[Equipment] ([EquipmentID])
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


print '' print '*** Creating ExceptionLine SupplyOrderID Foreign Key'
GO
ALTER TABLE [dbo].[ExceptionLine] WITH NOCHECK
	ADD CONSTRAINT [fk_ExceptionLine_SupplyOrderID] FOREIGN KEY([SupplyOrderID])
	REFERENCES [dbo].[SupplyOrder] ([SupplyOrderID])
	ON UPDATE CASCADE
GO

print '' print '*** Creating ExceptionLine ResupplyOrderID Foreign Key'
GO
ALTER TABLE [dbo].[ExceptionLine] WITH NOCHECK
	ADD CONSTRAINT [fk_ExceptionLine_ResupplyOrderID] FOREIGN KEY([ResupplyOrderID])
	REFERENCES [dbo].[ResupplyOrder] ([ResupplyOrderID])
	ON UPDATE CASCADE
GO

print '' print '*** Creating ExceptionLine SpecialOrderID Foreign Key'
GO
ALTER TABLE [dbo].[ExceptionLine] WITH NOCHECK
	ADD CONSTRAINT [fk_ExceptionLine_SpecialOrderID] FOREIGN KEY([SpecialOrderID])
	REFERENCES [dbo].[SpecialOrder] ([SpecialOrderID])
	ON UPDATE CASCADE
GO

print '' print '*** Creating ExceptionLine ExceptionTypeID Foreign Key'
GO
ALTER TABLE [dbo].[ExceptionLine] WITH NOCHECK
	ADD CONSTRAINT [fk_ExceptionLine_ExceptionTypeID] FOREIGN KEY([ExceptionTypeID])
	REFERENCES [dbo].[ExceptionType] ([ExceptionTypeID])
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

/* 
print '' print '*** Creating Delivery ResupplyOrderID Foreign Key'
GO
ALTER TABLE [dbo].[Delivery] WITH NOCHECK
	ADD CONSTRAINT [fk_Delivery_ResupplyOrderID] FOREIGN KEY([ResupplyOrderID])
	REFERENCES [dbo].[ResupplyOrder] ([ResupplyOrderID])
	ON UPDATE CASCADE
GO
 */

 print '' print '*** Creating Delivery SupplyOrderID Foreign Key'
GO
ALTER TABLE [dbo].[Delivery] WITH NOCHECK
	ADD CONSTRAINT [fk_Delivery_SupplyOrderID] FOREIGN KEY([SupplyOrderID])
	REFERENCES [dbo].[SupplyOrder] ([SupplyOrderID])
	ON UPDATE CASCADE
GO
 

print '' print '*** Creating Delivery SupplyStatusID Foreign Key'
GO
ALTER TABLE [dbo].[Delivery] WITH NOCHECK
	ADD CONSTRAINT [fk_Delivery_SupplyStatusID] FOREIGN KEY([SupplyStatusID])
	REFERENCES [dbo].[SupplyStatus] ([SupplyStatusID])
	ON UPDATE NO ACTION
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


print '' print '*** Creating Group JobID Foreign Key'
GO
ALTER TABLE [dbo].[Group] WITH NOCHECK
	ADD CONSTRAINT [fk_Group_JobID] FOREIGN KEY([JobID])
	REFERENCES [dbo].[Job] ([JobID])
	ON UPDATE CASCADE
GO


print '' print '*** Creating EmployeeGroup GroupID Foreign Key'
GO
ALTER TABLE [dbo].[EmployeeGroup] WITH NOCHECK
	ADD CONSTRAINT [fk_EmployeeGroup_GroupID] FOREIGN KEY([GroupID])
	REFERENCES [dbo].[Group] ([GroupID])
	ON UPDATE CASCADE
GO

print '' print '*** Creating EmployeeGroup EmployeeID Foreign Key'
GO
ALTER TABLE [dbo].[EmployeeGroup] WITH NOCHECK
	ADD CONSTRAINT [fk_EmployeeGroup_EmployeeID] FOREIGN KEY([EmployeeID])
	REFERENCES [dbo].[Employee] ([EmployeeID])
	ON UPDATE CASCADE
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


print '' print '*** Creating Message ToEmployeeID Foreign Key'
GO
ALTER TABLE [dbo].[Message] WITH NOCHECK
	ADD CONSTRAINT [fk_Message_ToEmployeeID] FOREIGN KEY([ToEmployeeID]) 
	REFERENCES [dbo].[Employee]([EmployeeID])
	ON UPDATE CASCADE
GO

print '' print '*** Creating Message FromEmployeeID Foreign Key'
GO
ALTER TABLE [dbo].[Message] WITH NOCHECK
	ADD CONSTRAINT [fk_Message_FromEmployeeID] FOREIGN KEY([FromEmployeeID]) 
	REFERENCES [dbo].[Employee]([EmployeeID])
	ON UPDATE NO ACTION
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

print '' print '*** Creating TaskSupply TaskID Foreign Key'
GO
ALTER TABLE [dbo].[TaskSupply] WITH NOCHECK
	ADD CONSTRAINT [fk_Task_TaskID] FOREIGN KEY([TaskID])
	REFERENCES [dbo].[Task] ([TaskID])
	ON UPDATE CASCADE
GO

print '' print '*** Creating TaskSupply SupplyItemID Foreign Key'
GO
ALTER TABLE [dbo].[TaskSupply] WITH NOCHECK
	ADD CONSTRAINT [fk_TaskSupply_SupplyItemID] FOREIGN KEY([SupplyItemID])
	REFERENCES [dbo].[SupplyItem] ([SupplyItemID])
	ON UPDATE CASCADE
GO

print '' print '*** Creating TaskEquipment TaskID Foreign Key'
GO
ALTER TABLE [dbo].[TaskEquipment] WITH NOCHECK
	ADD CONSTRAINT [fk_TaskEquipment_TaskID] FOREIGN KEY([TaskID])
	REFERENCES [dbo].[Task] ([TaskID])
	ON UPDATE CASCADE
GO

print '' print '*** Creating TaskEquipment EquipmentID Foreign Key'
GO
ALTER TABLE [dbo].[TaskEquipment] WITH NOCHECK
	ADD CONSTRAINT [fk_TaskEquipment_EquipmentID] FOREIGN KEY([EquipmentID])
	REFERENCES [dbo].[Equipment] ([EquipmentID])
	ON UPDATE CASCADE
GO

print '' print '*** Creating TaskEmployee TaskID Foreign Key'
GO
ALTER TABLE [dbo].[TaskEmployee] WITH NOCHECK
	ADD CONSTRAINT [fk_TaskEmployee_TaskID] FOREIGN KEY([TaskID])
	REFERENCES [dbo].[Task] ([TaskID])
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


print '' print '*** Creating Source SupplyItemID Index'
GO
CREATE NONCLUSTERED INDEX ix_Source_SupplyItemID
	ON [Source] ([SupplyItemID])
GO

print '' print '*** Creating Source VendorID Index'
GO
CREATE NONCLUSTERED INDEX ix_Source_VendorID
	ON [Source] ([VendorID])
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

/* 
print '' print '*** Creating ServiceItem ServiceOfferingID Index'
GO
CREATE NONCLUSTERED INDEX ix_ServiceItem_ServiceOfferingID
	ON [ServiceItem] ([ServiceOfferingID])
GO
 */

print '' print '*** Creating Certification Name Index'
GO
CREATE NONCLUSTERED INDEX ix_Certification_Name
	ON [Certification] ([Name])
GO


print '' print '*** Creating Message ToEmployeeID Index'
GO
CREATE NONCLUSTERED INDEX ix_Message_ToEmployeeID
	ON [Message] ([ToEmployeeID])
GO

print '' print '*** Creating Message FromEmployeeID Index'
GO
CREATE NONCLUSTERED INDEX ix_Message_FromEmployeeID
	ON [Message] ([FromEmployeeID])
GO

print '' print '*** Creating Message Subject Index'
GO
CREATE NONCLUSTERED INDEX ix_Subject_Name
	ON [Message] ([Subject])
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


print '' print '*** Creating Applicant FirstName Index'
GO
CREATE NONCLUSTERED INDEX ix_Applicant_FirstName
	ON [Applicant] ([FirstName])
GO

print '' print '*** Creating Applicant LastName Index'
GO
CREATE NONCLUSTERED INDEX ix_Applicant_LastName
	ON [Applicant] ([LastName])
GO

/* ****************************************************************************************************************
   *********************************************** Stored Procedures **********************************************
   **************************************************************************************************************** */   
/* James M */
/* Tested by James McPherson 3/23/2018 */
print '' print '*** Creating sp_create_inspectionrecord'
GO
CREATE PROCEDURE [dbo].[sp_create_inspectionrecord]
	(
	@EquipmentID			[int],
	@EmployeeID				[int],
	@Description			[nvarchar](1000),
	@Date					[date]
	)
AS
	BEGIN
		INSERT INTO [InspectionRecord]
			([EquipmentID], [EmployeeID], [Description], [Date])
		VALUES
			(@EquipmentID, @EmployeeID, @Description, @Date)
		SELECT SCOPE_IDENTITY()
	END
GO

/* Tested by James McPherson 3/23/2018 */
print '' print '*** Creating sp_retrieve_inspectionrecord_list_by_equipmentid'
GO
CREATE PROCEDURE [dbo].[sp_retrieve_inspectionrecord_list_by_equipmentid]
	(
	@EquipmentID			[int]
	)
AS
	BEGIN
		SELECT	[InspectionRecordID], [EquipmentID], [EmployeeID]
		, [Description], [Date]
		FROM	[InspectionRecord]
		WHERE	[EquipmentID] = @EquipmentID
	END
GO

/* Tested by James McPherson 3/23/2018 */
print '' print '*** Creating sp_retrieve_inspectionrecord_list'
GO
CREATE PROCEDURE [dbo].[sp_retrieve_inspectionrecord_list]
AS
	BEGIN
		SELECT	[InspectionRecordID], [EquipmentID], [EmployeeID]
		, [Description], [Date]
		FROM	[InspectionRecord]
	END
GO

/* Tested by James McPherson 3/23/2018 */
print '' print '*** Creating sp_retrieve_inspectionrecord_by_id'
GO
CREATE PROCEDURE [dbo].[sp_retrieve_inspectionrecord_by_id]
	(
	@InspectionRecordID			[int]
	)
AS
	BEGIN
		SELECT	[InspectionRecordID], [EquipmentID], [EmployeeID]
		, [Description], [Date]
		FROM	[InspectionRecord]
		WHERE	[InspectionRecordID] = @InspectionRecordID
	END
GO

/* Tested by James McPherson 3/23/2018 */
print '' print '*** Creating sp_edit_inspectionrecord_by_id'
GO
CREATE PROCEDURE [dbo].[sp_edit_inspectionrecord_by_id]
	(
	@InspectionRecordID			[int],
	@NewEquipmentID				[int],
	@NewEmployeeID				[int],
	@NewDescription				[nvarchar](1000),
	@NewDate					[date],
	@OldEquipmentID				[int],
	@OldEmployeeID				[int],
	@OldDescription				[nvarchar](1000),
	@OldDate					[date]
	)
AS
	BEGIN
		UPDATE	[InspectionRecord]
			SET [EquipmentID] = @NewEquipmentID
			, [EmployeeID] = @NewEmployeeID
			, [Description] = @NewDescription
			, [Date] = @NewDate
		WHERE	[InspectionRecordID] = @InspectionRecordID
		AND		[EquipmentID] = @OldEquipmentID
		AND		[EmployeeID] = @OldEmployeeID
		AND		[Description] = @OldDescription
		AND		[Date] = @OldDate
	RETURN @@ROWCOUNT
	END
GO

/* Tested by James McPherson 3/23/2018 */
print '' print '*** Creating sp_delete_inspectionrecord_by_id'
GO
CREATE PROCEDURE [dbo].[sp_delete_inspectionrecord_by_id]
	(
	@InspectionRecordID			[int]
	)
AS
	BEGIN
		DELETE FROM	[InspectionRecord]
		WHERE	[InspectionRecordID] = @InspectionRecordID
	RETURN @@ROWCOUNT
	END
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
	
/* Tested by James McPherson 2/16/2018 */
print '' print '*** Creating sp_retrieve_makemodel_list_by_active'
GO
CREATE PROCEDURE [dbo].[sp_retrieve_makemodel_list_by_active]
	(
	@Active					[bit]
	)
AS
	BEGIN
		SELECT 	[MakeModelID], [Make], [Model], [MaintenanceChecklistID]
		FROM	[MakeModel]
		WHERE	[Active] = @Active
	END
GO

/* Tested by James McPherson 2/13/2018 */
print '' print '*** Creating sp_deactivate_equipmenttype_by_id'
GO
CREATE PROCEDURE [dbo].[sp_deactivate_equipmenttype_by_id]
	(
	@EquipmentTypeID		[nvarchar](100)
	)
AS
	BEGIN
		UPDATE [EquipmentType]
			SET [Active] = 0
			WHERE [EquipmentTypeID] = @EquipmentTypeID
		RETURN @@ROWCOUNT
	END
GO

/* Tested by James McPherson 2/13/2018 */
print '' print '*** Creating sp_deactivate_certification_by_id'
GO
CREATE PROCEDURE [dbo].[sp_deactivate_certification_by_id]
	(
	@CertificationID		[int]
	)
AS
	BEGIN
		UPDATE [Certification]
			SET [Active] = 0
			WHERE [CertificationID] = @CertificationID
		RETURN @@ROWCOUNT
	END
GO

/* Tested by James McPherson 2/13/2018 */
print '' print '*** Creating sp_deactivate_employeecertification'
GO
CREATE PROCEDURE [dbo].[sp_deactivate_employeecertification]
	(
	@EmployeeID			[int],
	@CertificationID	[int]
	)
AS
	BEGIN
		UPDATE [EmployeeCertification]
			SET [Active] = 0
			WHERE	[EmployeeID] = @EmployeeID
			AND		[CertificationID] = @CertificationID
		RETURN @@ROWCOUNT
	END
GO

/* Tested by James McPherson 2/13/2018 */
print '' print '*** Creating sp_deactivate_makemodel_by_id'
GO
CREATE PROCEDURE [dbo].[sp_deactivate_makemodel_by_id]
	(
	@MakeModelID		[int]
	)
AS
	BEGIN
		UPDATE [MakeModel]
			SET [Active] = 0
			WHERE [MakeModelID] = @MakeModelID
		RETURN @@ROWCOUNT
	END
GO

/* Tested by James McPherson 2/4/2018 */
print '' print '*** Creating sp_retrieve_maintenancechecklist_by_id'
GO
CREATE PROCEDURE [dbo].[sp_retrieve_maintenancechecklist_by_id]
	(
	@MaintenanceChecklistID		[int]
	)
AS
	BEGIN
		SELECT	[MaintenanceChecklistID], [Name], [Description], [Active]
		FROM	[MaintenanceChecklist]
		WHERE	[MaintenanceChecklistID] = @MaintenanceChecklistID
	END
GO

/* Tested by James McPherson 2/4/2018 */
print '' print '*** Creating sp_retrieve_maintenancechecklist_list'
GO
CREATE PROCEDURE [dbo].[sp_retrieve_maintenancechecklist_list]
AS
	BEGIN
		SELECT 	[MaintenanceChecklistID], [Name], [Description], [Active]
		FROM	[MaintenanceChecklist]	
	END
GO

/* Tested by James McPherson 2/4/2018 */
print '' print '*** Creating sp_retrieve_makemodel_list'
GO
CREATE PROCEDURE [dbo].[sp_retrieve_makemodel_list]
AS
	BEGIN
		SELECT 	[MakeModelID], [Make], [Model]
		FROM	[MakeModel]
	END
GO

/* Tested by James McPherson 2/4/2018 */
print '' print '*** Creating sp_retrieve_makemodel_by_id'
GO
CREATE PROCEDURE [dbo].[sp_retrieve_makemodel_by_id]
	(
	@MakeModelID		[int]
	)
AS
	BEGIN
		SELECT	[MakeModelID], [Make], [Model], [MaintenanceChecklistID]
		FROM	[MakeModel]
		WHERE	[MakeModelID] = @MakeModelID
	END
GO

/* Tested by James McPherson 2/4/2018 */
print '' print '*** Creating sp_retrieve_makemodel_list_by_make'
GO
CREATE PROCEDURE [dbo].[sp_retrieve_makemodel_list_by_make]
	(
	@Make		[nvarchar](100)
	)
AS
	BEGIN
		SELECT	[MakeModelID], [Make], [Model], [MaintenanceChecklistID]
		FROM	[MakeModel]
		WHERE	[Make] = @Make
	END
GO

/* Tested by James McPherson 2/4/2018 */
print '' print '*** Creating sp_create_makemodel'
GO
CREATE PROCEDURE [dbo].[sp_create_makemodel]
	(
	@Make						[nvarchar](100),
	@Model						[nvarchar](100),
	@MaintenanceChecklistID		[int]
	)
AS
	BEGIN
		INSERT INTO [MakeModel]
			([Make], [Model], [MaintenanceChecklistID])
		VALUES
			(@Make, @Model, @MaintenanceChecklistID)
		SELECT SCOPE_IDENTITY()
	END
GO

/* Tested by James McPherson 2/4/2018 */
print '' print '*** Creating sp_edit_makemodel_by_id'
GO
CREATE PROCEDURE [dbo].[sp_edit_makemodel_by_id]
	(
	@MakeModelID				[int],
	@NewMake					[nvarchar](100),
	@NewModel					[nvarchar](100),
	@NewMaintenanceChecklistID	[int],
	@OldMake					[nvarchar](100),
	@OldModel					[nvarchar](100),
	@OldMaintenanceChecklistID	[int]
	)
AS
	BEGIN
		UPDATE 	[MakeModel]
		SET		[Make] = @NewMake,
				[Model] = @NewModel,
				[MaintenanceChecklistID] = @NewMaintenanceChecklistID
		WHERE	[Make] = @OldMake
		AND		[Model] = @OldModel
		AND		[MaintenanceChecklistID] = @OldMaintenanceChecklistID
		RETURN @@ROWCOUNT
	END
GO

/* Tested by James McPherson 2/4/2018 */
print '' print '*** Creating sp_delete_makemodel_by_id'
GO
CREATE PROCEDURE [dbo].[sp_delete_makemodel_by_id]
	(
	@MakeModelID				[int]
	)
AS
	BEGIN
	DELETE FROM [MakeModel]
	WHERE		[MakeModelID] = @MakeModelID
	RETURN @@ROWCOUNT
	END
GO

/* Tested by James McPherson 2/3/2018 */
print '' print '*** Creating sp_create_equipmentstatus'
GO
CREATE PROCEDURE [dbo].[sp_create_equipmentstatus]
	(
	@EquipmentStatusID		[nvarchar](100)
	)
AS
	BEGIN
		INSERT INTO [dbo].[EquipmentStatus]
			([EquipmentStatusID])
		VALUES
			(@EquipmentStatusID)
	END
GO

/* Tested by James McPherson 2/3/2018 */
print '' print '*** Creating sp_create_applicant'
GO
CREATE PROCEDURE [dbo].[sp_create_applicant]
	(
	@FirstName				[nvarchar](100),
	@LastName				[nvarchar](100),
	@Address				[nvarchar](250),
	@PhoneNumber			[nvarchar](15),
	@Email					[nvarchar](100)
	)
AS
	BEGIN
		INSERT INTO [dbo].[Applicant]
			([FirstName], [LastName], [Address], [PhoneNumber], [Email])
		VALUES
			(@FirstName, @LastName, @Address, @PhoneNumber, @Email)
		SELECT SCOPE_IDENTITY()
	END
GO

/* Tested by James McPherson 2/3/2018 */
print '' print '*** Creating sp_retrieve_applicant_by_applicantid'
GO
CREATE PROCEDURE [dbo].[sp_retrieve_applicant_by_applicantid]
	(
	@ApplicantID			[int]
	)
AS
	BEGIN
		SELECT	[ApplicantID], [FirstName], [LastName], [Address],
				[PhoneNumber], [Email], [Active]
		FROM	[Applicant]
		WHERE	[ApplicantID] = @ApplicantID
	END
GO

/* Tested by James McPherson 2/3/2018 */
print '' print '*** Creating sp_retrieve_applicant_by_email'
GO
CREATE PROCEDURE [dbo].[sp_retrieve_applicant_by_email]
	(
	@Email					[nvarchar](100)
	)
AS
	BEGIN
		SELECT	[ApplicantID], [FirstName], [LastName], [Address],
				[PhoneNumber], [Email], [Active]
		FROM	[Applicant]
		WHERE	[Email] = @Email
	END
GO

/* Tested by James McPherson 2/3/2018 */
print '' print '*** Creating sp_retrieve_applicant_by_phonenumber'
GO
CREATE PROCEDURE [dbo].[sp_retrieve_applicant_by_phonenumber]
	(
	@PhoneNumber			[nvarchar](100)
	)
AS
	BEGIN
		SELECT	[ApplicantID], [FirstName], [LastName], [Address],
				[PhoneNumber], [Email], [Active]
		FROM	[Applicant]
		WHERE	[PhoneNumber] = @PhoneNumber
	END
GO

/* Tested by James McPherson 2/3/2018 */
print '' print '*** Creating sp_retrieve_applicant_list_by_active'
GO
CREATE PROCEDURE [dbo].[sp_retrieve_applicant_list_by_active]
	(
	@Active					[bit]
	)
AS
	BEGIN
		SELECT	[ApplicantID], [FirstName], [LastName], [Address],
				[PhoneNumber], [Email], [Active]
		FROM	[Applicant]
		WHERE	[Active] = @Active
	END
GO

/* Tested by James McPherson 2/3/2018 */
print '' print '*** Creating sp_edit_applicant_by_id'
GO
CREATE PROCEDURE [dbo].[sp_edit_applicant_by_id]
	(
	@ApplicantID			[int],
	@NewFirstName			[nvarchar](100),
	@NewLastName			[nvarchar](100),
	@NewAddress				[nvarchar](250),
	@NewPhoneNumber			[nvarchar](15),
	@NewEmail				[nvarchar](100),
	@OldFirstName			[nvarchar](100),
	@OldLastName			[nvarchar](100),
	@OldAddress				[nvarchar](250),
	@OldPhoneNumber			[nvarchar](15),
	@OldEmail				[nvarchar](100)
	)
AS
	BEGIN
		UPDATE [Applicant]
			SET [FirstName] = @NewFirstName
			, [LastName] = @NewLastName
			, [Address] = @NewAddress
			, [PhoneNumber] = @NewPhoneNumber
			, [Email] = @NewEmail
			WHERE [ApplicantID] = @ApplicantID
			AND [FirstName] = @OldFirstName
			AND [LastName] = @OldLastName
			AND [Address] = @OldAddress
			AND [PhoneNumber] = @OldPhoneNumber
			AND [Email] = @OldEmail
		RETURN @@ROWCOUNT
	END
GO

/* Tested by James McPherson 2/3/2018 */
print '' print '*** Creating sp_deactivate_applicant_by_id'
GO
CREATE PROCEDURE [dbo].[sp_deactivate_applicant_by_id]
	(
	@ApplicantID		[int]
	)
AS
	BEGIN
		UPDATE [Applicant]
			SET [Active] = 0
			WHERE [ApplicantID] = @ApplicantID
		RETURN @@ROWCOUNT
	END
GO

/* Tested by James McPherson 2/3/2018 */
print '' print '*** Creating sp_delete_applicant_by_id'
GO
CREATE PROCEDURE [dbo].[sp_delete_applicant_by_id]
	(
	@ApplicantID		[int]
	)
AS
	BEGIN
		DELETE FROM [Applicant]
			WHERE [ApplicantID] = @ApplicantID
		RETURN @@ROWCOUNT
	END
GO
/* End James M */
   
/* Badis S */
/* Tested by James McPherson 2/3/2018 */
print '' print '*** Creating sp_create_supplyorder'
GO
CREATE PROCEDURE [dbo].[sp_create_supplyorder]
	(
	 @EmployeeID           		[int],
     @JobID                		[int],
     @SupplyStatusID       		[nvarchar](100),
     @Date      				[date]
	)
AS
	BEGIN
		INSERT INTO [dbo].[SupplyOrder]
			([EmployeeID], [JobID], [SupplyStatusID], [Date])
	VALUES
		(@EmployeeID, @JobID, @SupplyStatusID, @Date)
		SELECT SCOPE_IDENTITY()
	END
GO

/* Tested by James McPherson 2/3/2018 */
print '' print '*** Creating sp_retrieve_supplyorder_list'
GO
CREATE PROCEDURE [dbo].[sp_retrieve_supplyorder_list]
AS
	BEGIN
		SELECT 	[SupplyOrderID], [EmployeeID], [JobID], [SupplyStatusID], [Date]
		FROM 	[SupplyOrder]
	END
GO	

/* Tested by James McPherson 2/3/2018 */
print '' print '*** Creating sp_retrieve_supplyorder_by_id'
GO
CREATE PROCEDURE [dbo].[sp_retrieve_supplyorder_by_id]
	(
	@SupplyOrderID 				[int]
	)
AS
	BEGIN
		SELECT 	[SupplyOrderID], [EmployeeID], [JobID], [SupplyStatusID], [Date]
		FROM 	[SupplyOrder]
		WHERE 	[SupplyOrderID] = @SupplyOrderID
	END
GO	

/* Tested by James McPherson 2/3/2018 */
print '' print '*** Creating sp_retrieve_supplyorder_by_employeeid'
GO
CREATE PROCEDURE [dbo].[sp_retrieve_supplyorder_by_employeeid]
	(
	@EmployeeID 				[int]
	)
AS
	BEGIN
		SELECT 	[SupplyOrderID], [EmployeeID], [JobID], [SupplyStatusID], [Date]
		FROM 	[SupplyOrder]
		WHERE 	[EmployeeID] = @EmployeeID
	END
GO	

/* Tested by James McPherson 2/3/2018 */
print '' print '*** Creating sp_retrieve_supplyorder_by_jobid'
GO
CREATE PROCEDURE [dbo].[sp_retrieve_supplyorder_by_jobid]
	(
	@JobID 						[int]
	)
AS
	BEGIN
		SELECT 	[SupplyOrderID], [EmployeeID], [JobID], [SupplyStatusID], [Date]
		FROM 	[SupplyOrder]
		WHERE 	[JobID] = @JobID
	END
GO	

/* Untested */
print '' print '*** Creating sp_retrieve_supplyorder_by_job_supplystatusid'
GO
CREATE PROCEDURE [dbo].[sp_retrieve_supplyorder_by_job_supplystatusid]
	(
	@JobID 						[int],
    @SupplyStatusID    			[nvarchar](100)
	)
AS
	BEGIN
		SELECT 	[SupplyOrderID], [EmployeeID], [JobID], [SupplyStatusID], [Date]
		FROM 	[SupplyOrder]
		WHERE 	[JobID] = @JobID
        AND     [SupplyStatusID] = @SupplyStatusID
	END
GO	

/* Tested by James McPherson 2/3/2018 */
print '' print '*** Creating sp_edit_supplyorder_by_id'
GO
CREATE PROCEDURE [dbo].[sp_edit_supplyorder_by_id]
	(
	@SupplyOrderID       		[int],
    @OldEmployeeID           	[int],
    @OldJobID                	[int],
    @OldSupplyStatusID       	[nvarchar](100),
    @OldDate     	[date],
    @NewEmployeeID           	[int],
    @NewJobID               	[int],
    @NewSupplyStatusID       	[nvarchar](100),   
    @NewDate      	[date] 
	)
AS
	BEGIN
		UPDATE SupplyOrder 
        SET [EmployeeID] = @NewEmployeeID, [JobID] = @NewJobID,
        [Date] = @NewDate, [SupplyStatusID] = @NewSupplyStatusID
		where SupplyOrderID = @SupplyOrderID
        AND [EmployeeID] = @OldEmployeeID
        AND [JobID] = @OldJobID
        AND [Date] = @OldDate
        AND [SupplyStatusID] = @OldSupplyStatusID
		RETURN @@ROWCOUNT
	END
GO

/* Tested by James McPherson 2/3/2018 */
print '' print '*** Creating sp_delete_supplyorder_by_id'
GO
CREATE PROCEDURE [dbo].[sp_delete_supplyorder_by_id]
	(
	@SupplyOrderID				[int]
	)
AS
	BEGIN
		DELETE 
		FROM [SupplyOrder]
		WHERE [SupplyOrderID] = @SupplyOrderID
		RETURN @@ROWCOUNT
	END
GO

/* Tested by James McPherson 2/20/2018 */
print '' print '*** Creating sp_create_specialorder'
GO
CREATE PROCEDURE [dbo].[sp_create_specialorder]
	(
	 @EmployeeID           		[int],
     @JobID                		[int],
     @Date      				[date],
     @SupplyStatusID       		[nvarchar](100)
	)
AS
	BEGIN
		INSERT INTO [dbo].[SpecialOrder]
			([EmployeeID], [JobID], [Date], [SupplyStatusID])
	VALUES
		
		(@EmployeeID, @JobID, @Date, @SupplyStatusID)
		SELECT SCOPE_IDENTITY()
	END
GO

/* Tested by James McPherson 2/20/2018 */
print '' print '*** Creating sp_retrieve_specialorder_list'
GO
CREATE PROCEDURE [dbo].[sp_retrieve_specialorder_list]
	
AS
	BEGIN
		SELECT 	[SpecialOrderID], [EmployeeID], [JobID], [Date], [SupplyStatusID]
		FROM 	[SpecialOrder]
	END
GO	

/* Tested by James McPherson 2/20/2018 */
print '' print '*** Creating sp_retrieve_specialorder_by_id'
GO
CREATE PROCEDURE [dbo].[sp_retrieve_specialorder_by_id]
	(
	@SpecialOrderID 			[int]
	)
AS
	BEGIN
		SELECT 	[SpecialOrderID], [EmployeeID], [JobID], [SupplyStatusID], [Date]
		FROM 	[SpecialOrder]
		WHERE 	[SpecialOrderID] = @SpecialOrderID
	END
GO	

/* Tested by James McPherson 2/20/2018 */
print '' print '*** Creating sp_retrieve_specialorder_by_employeeid'
GO
CREATE PROCEDURE [dbo].[sp_retrieve_specialorder_by_employeeid]
	(
	@EmployeeID 				[int]
	)
AS
	BEGIN
		SELECT 	[SpecialOrderID], [EmployeeID], [JobID], [Date], [SupplyStatusID]
		FROM 	[SpecialOrder]
		WHERE 	[EmployeeID] = @EmployeeID
	END
GO	

/* Tested by James McPherson 2/20/2018 */
print '' print '*** Creating sp_retrieve_specialorder_by_jobid'
GO
CREATE PROCEDURE [dbo].[sp_retrieve_specialorder_by_jobid]
	(
	@JobID 						[int]
	)
AS
	BEGIN
		SELECT 	[SpecialOrderID], [EmployeeID], [JobID], [SupplyStatusID], [Date]
		FROM 	[SpecialOrder]
		WHERE 	[JobID] = @JobID
	END
GO	

/* Untested */
print '' print '*** Creating sp_retrieve_specialorder_by_job_supplystatusid'
GO
CREATE PROCEDURE [dbo].[sp_retrieve_specialorder_by_job_supplystatusid]
	(
	@JobID 	int,
    @SupplyStatusID    			[nvarchar](100)
	)
AS
	BEGIN
		SELECT 	[SpecialOrderID], [EmployeeID], [JobID], [SupplyStatusID], [Date]
		FROM 	[SpecialOrder]
		WHERE 	[JobID] = @JobID
        AND     [SupplyStatusID] = @SupplyStatusID
	END
GO	

/* Tested by James McPherson 2/20/2018 */
print '' print '*** Creating sp_edit_specialorder'
GO
CREATE PROCEDURE [dbo].[sp_edit_specialorder]
	(
	@SpecialOrderID        		[int],
    @OldEmployeeID           	[int],
    @OldJobID                	[int],
    @OldDate      				[date],
    @OldSupplyStatusID       	[nvarchar](100),
    @NewEmployeeID           	[int],
    @NewJobID                	[int],
    @NewDate      				[date],
    @NewSupplyStatusID       	[nvarchar](100)
	)
AS
	BEGIN
		UPDATE SpecialOrder 
        SET [EmployeeID] = @NewEmployeeID, [JobID] = @NewJobID,
        [Date] = @NewDate, [SupplyStatusID] = @NewSupplyStatusID
		where SpecialOrderID = @SpecialOrderID
        AND [EmployeeID] = @OldEmployeeID
        AND [Date] = @OldDate
        AND [SupplyStatusID] = @OldSupplyStatusID
		return @@rowcount
	END
GO

/* Tested by James McPherson 2/20/2018 */
print '' print '*** Creating sp_delete_specialorder_by_id'
GO
CREATE PROCEDURE [dbo].[sp_delete_specialorder_by_id]
	(
	@SpecialOrderID				[int]
	)
AS
	BEGIN
		DELETE 
		FROM [SpecialOrder]
		WHERE [SpecialOrderID] = @SpecialOrderID
		RETURN @@ROWCOUNT
	END
GO

/* Untested */
print '' print '*** Creating sp_create_delivery'
GO
CREATE PROCEDURE [dbo].sp_create_delivery
	(
	 @Address           		[nvarchar](250),
     @Time                		[datetime],
     @SupplyStatusID      		[nvarchar](100)
	)
AS
	BEGIN
		INSERT INTO [dbo].[Delivery]
			([Address], [Time], [SupplyStatus].[SupplyStatusID])
	VALUES
		(@Address, @Time, @SupplyStatusID)
		SELECT SCOPE_IDENTITY()
	END
GO

/* Untested */
print '' print '*** Creating sp_retrieve_delivery_list'
GO
CREATE PROCEDURE [dbo].sp_retrieve_delivery_list
AS
	BEGIN
		SELECT 	[DeliveryID], [Address], [Time], [SupplyStatus].[SupplyStatusID]
		FROM 	Delivery, SupplyStatus
	END
GO	

/* Untested */
print '' print '*** Creating sp_retrieve_delivery_by_id'
GO
CREATE PROCEDURE [dbo].[sp_retrieve_delivery_by_id]
	(
	@DeliveryID 				[int]
	)
AS
	BEGIN
		SELECT 	[DeliveryID], [Address], [Time], [Resupply].[ResupplyOrderID], [SupplyStatus].[SupplyStatusID]
		FROM 	Delivery, Resupply, SupplyStatus
		WHERE 	[DeliveryID] = @DeliveryID
	END
GO	

/* Untested */
print '' print '*** Creating sp_retrieve_delivery_by_supplyorderid'
GO
CREATE PROCEDURE [dbo].[sp_retrieve_delivery_by_supplyorderid]
	(
	@SupplyOrderID 				[int]
	)
AS
	BEGIN
		SELECT 	[DeliveryID], [Address], [Time], [Resupply].[ResupplyOrderID], [SupplyStatus].[SupplyStatusID]
		FROM 	Delivery, Resupply, SupplyStatus
		WHERE 	[SupplyOrderID] = @SupplyOrderID
	END
GO	

/* Untested */
print '' print '*** Creating sp_retrieve_delivery_by_supplystatusid'
GO
CREATE PROCEDURE [dbo].[sp_retrieve_delivery_by_supplystatusid]
	(
    @SupplyStatusID    			[nvarchar](100)
	)
AS
	BEGIN
		SELECT 	[DeliveryID], [Address], [Time], [Resupply].[ResupplyOrderID], [SupplyStatus].[SupplyStatusID]
		FROM 	Delivery, Resupply, SupplyStatus
		WHERE [SupplyStatusID] = @SupplyStatusID
	END
GO	

/* Untested */
print '' print '*** Creating sp_edit_delivery_by_id'
GO
CREATE PROCEDURE [dbo].[sp_edit_delivery_by_id]
	(
	@DeliveryID          	 	[int],
    	@OldAddress            	[nvarchar](250),
    	@OldTime              	[datetime],
    	@OldSupplyStatusID      [nvarchar](100),
    	@NewAddress            	[nvarchar](250),
    	@NewTime              	[datetime],
    	@NewSupplyStatusID      [nvarchar](100) 
	)
AS
	BEGIN
		UPDATE Delivery
        SET [Address] = @NewAddress
		, [Time] = @NewTime
		, [SupplyStatusID] = @NewSupplyStatusID
		where [DeliveryID] = @DeliveryID
        AND [Address] = @OldAddress
        AND [Time] = @OldTime
        AND [SupplyStatusID] = @OldSupplyStatusID
	END
GO

/* Untested */
print '' print '*** Creating sp_delete_delivery_by_id'
GO
CREATE PROCEDURE [dbo].[sp_delete_delivery_by_id]
	(
	@DeliveryID					[int]
	)
AS
	BEGIN
		DELETE 
		FROM [Delivery]
		WHERE [DeliveryID] = @DeliveryID
		RETURN @@ROWCOUNT
	END
GO

/* Tested by James McPherson 2/20/2018 */
print '' print '*** Creating sp_create_supplystatus'
GO
CREATE PROCEDURE [dbo].[sp_create_supplystatus]
	(
	 @SupplyStatusID          	[nvarchar](100)
	)
AS
	BEGIN
		INSERT INTO [dbo].[SupplyStatus]
			([SupplyStatusID])
		VALUES
			(@SupplyStatusID)
	END
GO

/* Tested by James McPherson 2/20/2018 */
print '' print '*** Creating sp_retrieve_supplystatus_list'
GO
CREATE PROCEDURE [dbo].[sp_retrieve_supplystatus_list]
	
AS
	BEGIN
		SELECT 	[SupplyStatusID]
		FROM 	[SupplyStatus]
	END
GO	

/* Tested by James McPherson 2/20/2018 */
print '' print '*** Creating sp_edit_supplystatus'
GO
CREATE PROCEDURE [dbo].[sp_edit_supplystatus]
	(
	@NewSupplyStatusID        		[nvarchar](100),
	@OldSupplyStatusID        		[nvarchar](100)
	)
AS
	BEGIN
		UPDATE SupplyStatus 
        SET [SupplyStatusID] = @NewSupplyStatusID
		WHERE [SupplyStatusID] = @OldSupplyStatusID
		RETURN @@ROWCOUNT
	END
GO

/* Tested by James McPherson 2/20/2018 */
print '' print '*** Creating sp_delete_supplystatus'
GO
CREATE PROCEDURE [dbo].[sp_delete_supplystatus]
	(
	@SupplyStatusID				[nvarchar](100) 
	)
AS
	BEGIN
		DELETE 
		FROM [SupplyStatus]
		WHERE [SupplyStatusID] = @SupplyStatusID
		RETURN @@ROWCOUNT
	END
GO
/* End Badis S */

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

/* Tested by James McPherson 2/20/2018 */
print '' print '*** Creating sp_create_employeecertification'
GO
CREATE PROCEDURE [dbo].[sp_create_employeecertification]
	(
	@CertificationID			[int],
	@EmployeeID					[int],
	@EndDate					[datetime]
	)
AS
	BEGIN
		INSERT INTO [dbo].[EmployeeCertification]
			([CertificationID], [EmployeeID], [EndDate])
		VALUES
			(@CertificationID, @EmployeeID, @EndDate)
	END
GO

print '' print '*** Creating sp_retrieve_employeecertification_by_id'
GO
CREATE PROCEDURE [dbo].[sp_retrieve_employeecertification_by_id]
	(
	@CertificationID			[int],
	@EmployeeID					[int]
	)
AS
	BEGIN
		SELECT 		[CertificationID], [EmployeeID], [EndDate], [Active]
		FROM		[EmployeeCertification]
		WHERE		[CertificationID] = @CertificationID
		AND			[EmployeeID] = @EmployeeID
		ORDER BY 	[CertificationID]
	END
GO

print '' print '*** Creating sp_edit_employeecertification_by_id'
GO
CREATE PROCEDURE [dbo].[sp_edit_employeecertification_by_id]
	(
	@CertificationID			[int],
	@EmployeeID					[int],
	@EndDate					[datetime]
	)
AS
	BEGIN
		UPDATE [EmployeeCertification]
			SET		[CertificationID] = @CertificationID,
					[EmployeeID] = @EmployeeID,
					[EndDate] = @EndDate
			WHERE	[CertificationID] = @CertificationID
		RETURN @@ROWCOUNT
	END
GO

print '' print '*** Creating sp_delete_employeecertification_by_id'
GO
CREATE PROCEDURE [dbo].[sp_delete_employeecertification_by_id]
	(
	@CertificationID			[int]
	)
AS
	BEGIN
		DELETE FROM EmployeeCertification
		WHERE [CertificationID] = @CertificationID
	END
GO

print '' print '*** Creating sp_create_availability'
GO
CREATE PROCEDURE [dbo].[sp_create_availability]
	(
	@EmployeeID					[int],
	@StartTime					[datetime],
	@EndTime					[datetime]
	)
AS
	BEGIN
		INSERT INTO [dbo].[Availability]
			([EmployeeID], [StartTime], [EndTime])
		VALUES
			(@EmployeeID, @StartTime, @EndTime)
	END
GO

print '' print '*** Creating sp_retrieve_availability_by_id'
GO
CREATE PROCEDURE [dbo].[sp_retrieve_availability_by_id]
	(
	@AvailabilityID				[int]
	)
AS
	BEGIN
		SELECT 		[AvailabilityID], [EmployeeID], [StartTime], [EndTime]
		FROM		[Availability]
		WHERE		[AvailabilityID] = @AvailabilityID
		ORDER BY 	[AvailabilityID]
	END
GO

print '' print '*** Creating sp_delete_availability_by_id'
GO
CREATE PROCEDURE [dbo].[sp_delete_availability_by_id]
	(
	@AvailabilityID				[int]
	)
AS
	BEGIN
		DELETE FROM Availability
		WHERE [AvailabilityID] = @AvailabilityID
	END
GO
/* End Jacob S */

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

print '' print '*** Creating sp_retrieve_maintenancerecord_list'
GO
CREATE PROCEDURE [dbo].[sp_retrieve_maintenancerecord_list]
AS
	BEGIN
		SELECT	[MaintenanceRecordID],[EquipmentID],[EmployeeID],[Description]
				, [Date]
		FROM	[MaintenanceRecord]
	END
GO

print '' print '*** Creating sp_deactivate_equipment_by_id'
GO
CREATE PROCEDURE [dbo].[sp_deactivate_equipment_by_id]
	(
	@EquipmentID	[int]
	)
AS
	BEGIN
		UPDATE 		[Equipment]
			SET 	[Active] = 0					
			WHERE 	[EquipmentID] = @EquipmentID
			AND		[Active] = 1		
		RETURN @@ROWCOUNT		
	END
GO

print '' print '*** Creating sp_reactivate_equipment_by_id'
GO
CREATE PROCEDURE [dbo].[sp_reactivate_equipment_by_id]
	(
	@EquipmentID	[int]
	)
AS
	BEGIN
		UPDATE 		[Equipment]
			SET 	[Active] = 1				
			WHERE 	[EquipmentID] = @EquipmentID
			AND		[Active] = 0	
		RETURN @@ROWCOUNT		
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

print '' print '*** Creating sp_create_employee'
GO
CREATE PROCEDURE [dbo].[sp_create_employee]
	(
	@FirstName			[nvarchar](100),
	@LastName			[nvarchar](100),
	@Address			[nvarchar](250),
	@PhoneNumber		[nvarchar](15),
	@Email				[nvarchar](100),
	@PasswordHash		[nvarchar](100),
	@Active				[bit]
	)
AS
	BEGIN
		INSERT INTO [dbo].[Employee]
			([FirstName], [LastName], [Address], [PhoneNumber]
			,[Email], [PasswordHash], [Active])
		VALUES
			(@FirstName, @LastName, @Address, @PhoneNumber
			,@Email, @PasswordHash, @Active)
	END
GO

/* Tested by James McPherson 2/3/2018 */
print '' print '*** Creating sp_retrieve_employee_list_by_active'
GO
CREATE PROCEDURE [dbo].[sp_retrieve_employee_list_by_active]
	(
	@Active				[bit]
	)
AS
	BEGIN
		SELECT	[EmployeeID], [FirstName], [LastName], [Address], [PhoneNumber]
			,[Email], [Active]
		FROM	[Employee]
		WHERE	[Active] = @Active
	END
GO

print '' print '*** Creating sp_retrieve_employee_by_id'
GO
CREATE PROCEDURE [dbo].[sp_retrieve_employee_by_id]
	(
		@EmployeeID		[int]
	)
AS
	BEGIN
		SELECT	[FirstName], [LastName], [Address], [PhoneNumber]
			,[Email], [Active]
		FROM	[Employee]
		WHERE	[EmployeeID] = @EmployeeID
	END
GO

print '' print '*** Creating sp_retrieve_employee_by_name'
GO
CREATE PROCEDURE [dbo].[sp_retrieve_employee_by_name]
	(
		@FirstName		[nvarchar](100),
		@LastName		[nvarchar](100)	
	)
AS
	BEGIN
		SELECT	[EmployeeID], [Address], [PhoneNumber]
			,[Email], [Active]
		FROM	[Employee]
		WHERE	[FirstName] = @FirstName
		AND		[LastName] = @LastName
	END
GO

print '' print '*** Creating sp_edit_employee_by_id'
GO
CREATE PROCEDURE [dbo].[sp_edit_employee_by_id]
	(
		@NewFirstName 	 	[nvarchar](100),
		@OldFirstName 	 	[nvarchar](100),
		@NewLastName 		[nvarchar](100),
		@OldLastName  		[nvarchar](100),
		@NewAddress  		[nvarchar](250),
		@OldAddress  		[nvarchar](250),
		@NewPhoneNumber		[nvarchar](15),
		@OldPhoneNumber		[nvarchar](15),
		@NewEmail			[nvarchar](100),
		@OldEmail			[nvarchar](100),
		@EmployeeID			[int]
	)
AS
	BEGIN
		UPDATE [Employee]
			SET 	[FirstName] = @NewFirstName,
					[LastName] = @NewLastName,
					[Address] = @NewAddress,
					[PhoneNumber] = @NewPhoneNumber,
					[Email] = @NewEmail
			WHERE 	[EmployeeID] = @EmployeeID
			AND		[FirstName] = @OldFirstName
			AND		[LastName] = @OldLastName
			AND		[Address] = @OldAddress
			AND		[PhoneNumber] = @OldPhoneNumber
			AND		[Email] = @OldEmail
		RETURN @@ROWCOUNT
	END
GO

print '' print '*** Creating sp_reactivate_employee_by_id'
GO
CREATE PROCEDURE [dbo].[sp_reactivate_employee_by_id]
	(
	@EmployeeID 	[int]
	)
AS
	BEGIN
		UPDATE [Employee]
			SET [Active] = 1
			WHERE [EmployeeID] = @EmployeeID
		RETURN @@ROWCOUNT
	END
GO

/* Tested by James McPherson 2/3/2018 */
print '' print '*** Creating sp_deactivate_employee_by_id'
GO
CREATE PROCEDURE [dbo].[sp_deactivate_employee_by_id]
	(
		@EmployeeID 	[int]
	)
AS
	BEGIN
		UPDATE [Employee]
			SET [Active] = 0
			WHERE [EmployeeID] = @EmployeeID
		RETURN @@ROWCOUNT
	END
GO

print '' print '*** Creating sp_delete_employee_by_id'
GO
CREATE PROCEDURE [dbo].[sp_delete_employee_by_id]
	(
		@EmployeeID		[int]
	)
AS
	BEGIN
			DELETE
			FROM 		[Employee]
			WHERE 		[EmployeeID] = @EmployeeID
		RETURN @@ROWCOUNT
	END
GO

print '' print '*** Creating sp_create_employeegroup'
GO
CREATE PROCEDURE [dbo].[sp_create_employeegroup]
	(
	@GroupID			[int],
	@EmployeeID			[int]
	)
AS
	BEGIN
		INSERT INTO [dbo].[EmployeeGroup]
			([GroupID], [EmployeeID])
		VALUES
			(@GroupID, @EmployeeID)
		RETURN @@ROWCOUNT
	END
GO

print '' print '*** Creating sp_retrieve_employeegroup_by_groupid'
GO
CREATE PROCEDURE [dbo].[sp_retrieve_employeegroup_by_groupid]
	(
		@GroupID	[int]
	)
AS
	BEGIN
		SELECT	[GroupID], [EmployeeID]
		FROM	[EmployeeGroup]
		WHERE	[GroupID] = @GroupID
	END
GO

print '' print '*** Creating sp_delete_employeegroup_by_groupid'
GO
CREATE PROCEDURE [dbo].[sp_delete_employeegroup_by_groupid]
	(
		@GroupID		[int]
	)
AS
	BEGIN
			DELETE
			FROM 		[EmployeeGroup]
			WHERE 		[GroupID] = @GroupID
		RETURN @@ROWCOUNT
	END
GO

print '' print '*** Creating sp_delete_employeegroup_by_employeeid'
GO
CREATE PROCEDURE [dbo].[sp_delete_employeegroup_by_employeeid]
	(
		@EmployeeID		[int]
	)
AS
	BEGIN
			DELETE
			FROM 		[EmployeeGroup]
			WHERE 		[EmployeeID] = @EmployeeID
		RETURN @@ROWCOUNT
	END
GO

print '' print '*** Creating sp_create_role'
GO
CREATE PROCEDURE [dbo].[sp_create_role]
	(
		@RoleID				[nvarchar](100),
		@Description		[nvarchar](1000)
	)
AS
	BEGIN
		INSERT INTO [dbo].[Role]
			([RoleID], [Description])
		VALUES
			(@RoleID, @Description)
		RETURN @@ROWCOUNT
	END
GO

print '' print '*** Creating sp_retrieve_role_list'
GO
CREATE PROCEDURE [dbo].[sp_retrieve_role_list]
AS
	BEGIN
		SELECT	[RoleID], [Description]
		FROM	[Role]
	END
GO

print '' print '*** Creating sp_retrieve_role_by_id'
GO
CREATE PROCEDURE [dbo].[sp_retrieve_role_by_id]
	(
	@RoleID			[nvarchar](100)
	)
AS
	BEGIN
		SELECT	[RoleID], [Description]
		FROM	[Role]
		WHERE	[RoleID] = @RoleID
	END
GO

print '' print '*** Creating sp_edit_role'
GO
CREATE PROCEDURE [dbo].[sp_edit_role]
	(
		@NewDescription		[nvarchar](1000),
		@OldDescription		[nvarchar](1000),
		@RoleID				[nvarchar](100)
	)
AS
	BEGIN
		UPDATE [Role]
			SET 	[Description] = @NewDescription
			WHERE 	[Description] = @OldDescription
			AND		[RoleID] = @RoleID
		RETURN @@ROWCOUNT
	END
GO

print '' print '*** Creating sp_delete_role_by_id'
GO
CREATE PROCEDURE [dbo].[sp_delete_role_by_id]
	(
		@RoleID		[nvarchar](100)
	)
AS
	BEGIN
			DELETE
			FROM 		[Role]
			WHERE 		[RoleID] = @RoleID
		RETURN @@ROWCOUNT
	END
GO

print '' print '*** Creating sp_create_certification'
GO
CREATE PROCEDURE [dbo].[sp_create_certification]
	(
		@Name			[nvarchar](100),
		@Description	[nvarchar](1000),
		@Active	[bit]
	)
AS
	BEGIN
		INSERT INTO [dbo].[certification]
			([Name], [Description],[Active])
		VALUES
			(@Name, @Description, @Active)
		SELECT SCOPE_IDENTITY()
	END
GO

print '' print '*** Creating sp_retrieve_certification_by_id'
GO
CREATE PROCEDURE [dbo].[sp_retrieve_certification_by_id]
	(
		@CertificationID		[int]
	)
AS
	BEGIN
		SELECT	[Name], [Description], [Active]
		FROM	[certification]
		WHERE	[CertificationID] = @CertificationID
	END
GO

print '' print '*** Creating sp_edit_certification'
GO
CREATE PROCEDURE [dbo].[sp_edit_certification]
	(
		@NewName			[nvarchar](100),
		@OldName			[nvarchar](100),
		@NewDescription		[nvarchar](1000),
		@OldDescription		[nvarchar](1000),
		@CertificationID	[int],
		@OldActive				[bit],
		@NewActive				[bit]
	)
AS
	BEGIN
		UPDATE [certification]
			SET 	[Name] = @NewName,
					[Description] = @NewDescription,
					[Active] = @NewActive
			WHERE 	[Name] = @OldName
			AND		[Description] = @OldDescription
			AND		[CertificationID] = @CertificationID
			AND   [Active] = @OldActive
		RETURN @@ROWCOUNT
	END
GO

print '' print '*** Creating sp_delete_certification_by_id'
GO
CREATE PROCEDURE [dbo].[sp_delete_certification_by_id]
	(
		@CertificationID		[int]
	)
AS
	BEGIN
			DELETE
			FROM 		[certification]
			WHERE 		[CertificationID] = @CertificationID
		RETURN @@ROWCOUNT
	END
GO
/* End Noah D */

/* Zachary H */
print '' print '*** Creating custom table type AvailabilityTableType'
GO
CREATE TYPE [dbo].[AvailabilityTableType] AS TABLE
	([EmployeeID]	[int], [StartTime]	[datetime], [EndTime]	[datetime])
GO

print '' print '*** Creating sp_edit_availability'
GO
CREATE PROCEDURE [dbo].[sp_edit_availability]
	(
	@tvpNewAvailabilities	[dbo].[AvailabilityTableType] READONLY,
	@EmployeeID			[int]
	)
AS
	BEGIN
		DECLARE @ServicePackageDeleteErr [int]
		DECLARE @ServicePackageInsertErr [int]
		DECLARE @maxerr [int]
		DECLARE @err_message [nvarchar](255)
		
		SET @maxerr = 0
		
		BEGIN TRANSACTION
		
		/*Delete all current availability records*/
		DELETE FROM [Availability]
		WHERE [EmployeeID] = @EmployeeID
		
		/*Check for delete error*/
		SET @ServicePackageDeleteErr = @@error
		IF @ServicePackageDeleteErr > @maxerr
			SET @maxerr = @ServicePackageDeleteErr
		
		/*Insert data from the custom availability table type*/
		INSERT INTO [Availability]
			([EmployeeID], [StartTime], [EndTime])
			SELECT [CustomAvailability].[EmployeeID]
				, [CustomAvailability].[StartTime]
				, [CustomAvailability].[EndTime]
			FROM @tvpNewAvailabilities AS [CustomAvailability]
		
		/*Check for insert errors*/
		SET @ServicePackageInsertErr = @@error
		IF @ServicePackageInsertErr > @maxerr
			SET @maxerr = @ServicePackageInsertErr
		
		/*Check for errors and rollback if there are any. Else, commit*/
		IF @ServicePackageDeleteErr <> 0
			BEGIN
				ROLLBACK
				SET @err_message = 'Could not properly edit the availability records'
				RAISERROR (@err_message, 20, 1)
			END
		ELSE IF @ServicePackageInsertErr <> 0
			BEGIN
				ROLLBACK
				SET @err_message = 'Could not properly insert availability records'
				RAISERROR (@err_message, 20, 1)
			END
		ELSE
			BEGIN
				COMMIT
				RETURN @@ROWCOUNT
			END
	END
GO

/* Tested by James McPherson 2/23/2018 */
print '' print '*** Creating sp_retrieve_availability_list_by_employee_id'
GO
CREATE PROCEDURE [dbo].[sp_retrieve_availability_list_by_employee_id]
	(
	@EmployeeID	[int]
	)
AS
	BEGIN
		SELECT [AvailabilityID], [EmployeeID], [StartTime], [EndTime]
		FROM [Availability]
		WHERE [EmployeeID] = @EmployeeID		
	END
GO

print '' print '*** Creating sp_edit_specialorderitem'
GO
CREATE PROCEDURE [dbo].[sp_edit_specialorderitem]
	(
	@SpecialOrderItemID	[int],
	@OldName			[nvarchar](100),
	@NewName			[nvarchar](100),
	@OldActive			[bit],
	@NewActive			[bit]
	)
AS
	BEGIN
		UPDATE	[SpecialOrderItem]
			SET		[Name] = @NewName
			, [Active] = @NewActive
			WHERE	[SpecialOrderItemID] = @SpecialOrderItemID
			AND		[Name] = @OldName
			AND [Active] = @OldActive
		RETURN @@ROWCOUNT
	END
GO

print '' print '*** Creating sp_create_specialorderitem'
GO
CREATE PROCEDURE [dbo].[sp_create_specialorderitem]
	(
	@Name	[nvarchar](100),
	@Active	[bit]
	)
AS
	BEGIN
		INSERT INTO [dbo].[SpecialOrderItem]
			([Name], [Active])
		VALUES
			(@Name, @Active)
		SELECT SCOPE_IDENTITY()
	END
GO

print '' print '*** Creating sp_retrieve_specialorderitem_list'
GO
CREATE PROCEDURE [dbo].[sp_retrieve_specialorderitem_list]
AS
	BEGIN
		SELECT [SpecialOrderItemID], [Name], [Active]
		FROM [SpecialOrderItem]
	END
GO

print '' print '*** Creating sp_edit_supplyitem_by_id'
GO
CREATE PROCEDURE [dbo].[sp_edit_supplyitem_by_id]
	(
	@SupplyItemID				[int],
	@OldName			        nvarchar(100),
	@NewName			        nvarchar(100),
	@OldDescription		        nvarchar(1000),
	@NewDescription		        nvarchar(1000),
    @OldLocation                nvarchar(100),
    @NewLocation                nvarchar(100),
    @OldQuantityInStock         int,
    @NewQuantityInStock         int,
    @OldReorderLevel            int,
    @NewReorderLevel            int,
    @OldReorderQuantity         int,
    @NewReorderQuantity         int,
	@OldActive					bit,
	@NewActive					bit
	)
AS
	BEGIN
		UPDATE [SupplyItem]
			SET [Name] = @NewName 
			, [Description] = @NewDescription
            , [Location] = @NewLocation
            , [QuantityInStock] = @NewQuantityInStock
            , [ReorderLevel] = @NewReorderLevel
            , [ReorderQuantity] = @NewReorderQuantity
			, [Active] = @NewActive
			WHERE [Name] = @OldName 
			AND [Description] = @OldDescription
            AND [Location] = @OldLocation
            AND [QuantityInStock] = @OldQuantityInStock
            AND [ReorderLevel] = @OldReorderLevel
            AND [ReorderQuantity] = @OldReorderQuantity
            AND [SupplyItemID] = @SupplyItemID
			AND [Active] = @OldActive
		RETURN @@ROWCOUNT
	END
GO

print '' print '*** Creating sp_create_supplyitem'
GO
CREATE PROCEDURE [dbo].[sp_create_supplyitem]
	(
	@Name			    [nvarchar](100),
	@Description	    [nvarchar](1000),
    @Location           [nvarchar](100),
    @QuantityInStock    [int],
    @ReorderLevel       [int],
    @ReorderQuantity    [int],
	@Active				[bit]
	)
AS
	BEGIN
		INSERT INTO [dbo].[SupplyItem]
		(Name, Description, Location, QuantityInStock, ReorderLevel, ReorderQuantity, Active)
		VALUES	(
			@Name, @Description, @Location, @QuantityInStock, @ReorderLevel, @ReorderQuantity, @Active
		)
		SELECT SCOPE_IDENTITY()
	END
GO

print '' print '*** Creating sp_retrieve_supplyitem_list'
GO
CREATE PROCEDURE [dbo].[sp_retrieve_supplyitem_list]
AS
	BEGIN
		SELECT [SupplyItemID], [Name], [Description], [Location], [QuantityInStock], [ReorderLevel], [ReorderQuantity], [Active]
		FROM [SupplyItem]
	END
GO
/* End Zachary H */

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

print '' print '*** Creating sp_retrieve_prepchecklist_by_active'
GO
CREATE PROCEDURE [dbo].[sp_retrieve_prepchecklist_by_active]
	(
	@Active 	[bit]
	)
AS
	BEGIN
		SELECT 		[PrepChecklistID], [Name], [Description], [Active]
		FROM 		[PrepChecklist]
		WHERE 		[Active] = @Active
		ORDER BY 	[PrepChecklistID]
	END
GO

print '' print '*** Creating sp_retrieve_prepchecklist_list'
GO
CREATE PROCEDURE [dbo].[sp_retrieve_prepchecklist_list]
AS
	BEGIN
		SELECT 	[PrepChecklistID], [Name], [Description], [Active]
		FROM 	[PrepChecklist]
	END
GO

print '' print '*** Creating sp_delete_prepchecklist_list'
GO
CREATE PROCEDURE [dbo].[sp_delete_prepchecklist_by_id]
	(
	@PrepChecklistID 		[int]
	)
AS
	BEGIN
		DELETE FROM 	[PrepChecklist]
		WHERE 			[PrepChecklistID] = @PrepChecklistID
		RETURN @@ROWCOUNT
	END
GO

print '' print '*** Creating sp_deactivate_prepchecklist_by_id'
GO
CREATE PROCEDURE [dbo].[sp_deactivate_prepchecklist_by_id]
	(
	@PrepChecklistID				[int]
	)
AS
	BEGIN
		UPDATE [PrepChecklist]
			SET [Active] = 0
			WHERE [PrepChecklistID] = @PrepChecklistID 
		RETURN @@ROWCOUNT
	END
GO

print '' print '*** Creating sp_retrieve_prepchecklist_by_id'
GO
CREATE PROCEDURE [dbo].[sp_retrieve_prepchecklist_by_id]
	(
	@PrepChecklistID 		[int]
	)
AS
	BEGIN
		SELECT 		[PrepChecklistID], [Description], [Active]
		FROM 		[PrepChecklist]
		WHERE 		[PrepChecklistID] = @PrepChecklistID
		ORDER BY 	[PrepChecklistID]
	END
GO

print '' print '*** Creating sp_create_equipment'
GO
CREATE PROCEDURE [dbo].[sp_create_equipment]
	(
	@EquipmentTypeID	[nvarchar](100),
	@Name				[nvarchar](100),
	@MakeModelID		[int],
	@DatePurchased		[date],
	@DateLastRepaired	[date],
	@PriceAtPurchase	[money],
	@CurrentValue		[money],
	@EquipmentStatusID	[nvarchar](100),
	@EquipmentDetails	[nvarchar](1000)
	)
AS
	BEGIN
		INSERT INTO [dbo].[Equipment]
			([EquipmentTypeID], [Name], [MakeModelID], [DatePurchased], [DateLastRepaired], [PriceAtPurchase], [CurrentValue], [EquipmentStatusID], [EquipmentDetails])
		VALUES
			(@EquipmentTypeID, @Name, @MakeModelID, @DatePurchased, @DateLastRepaired, @PriceAtPurchase, @CurrentValue, @EquipmentStatusID, @EquipmentDetails  )
		SELECT SCOPE_IDENTITY()
	END
GO

print '' print '*** Creating sp_delete_equipment_by_id'
GO
CREATE PROCEDURE [dbo].[sp_delete_equipment_by_id]
	(
	@EquipmentID 		[int]
	)
AS
	BEGIN
		DELETE FROM [Equipment]
		WHERE 		[EquipmentID] = @EquipmentID
		RETURN @@ROWCOUNT
	END
GO

print '' print '*** Creating sp_delete_preprecord_by_id'
GO
CREATE PROCEDURE [dbo].[sp_delete_preprecord_by_id]
	(
	@PrepRecordID 		[int]
	)
AS
	BEGIN
		DELETE FROM [PrepRecord]
		WHERE 		[PrepRecordID] = @PrepRecordID
		RETURN @@ROWCOUNT
	END
GO

print '' print '*** Creating sp_create_prepchecklist'
GO
CREATE PROCEDURE [dbo].[sp_create_prepchecklist]
	(
	@Name				[nvarchar](100),
	@Description		[nvarchar](1000)
	)
AS
	BEGIN
		INSERT INTO [dbo].[PrepChecklist]
			([Name], [Description])
		VALUES
			(@Name, @Description)
		SELECT SCOPE_IDENTITY()
	END
GO

print '' print '*** Creating sp_edit_prepchecklist'
GO
CREATE PROCEDURE [dbo].[sp_edit_prepchecklist]
	(
	
	@PrepChecklistID		[int],
	@NewName				[nvarchar](100),
	@NewDescription			[nvarchar](1000),
	@NewActive				[bit],
	@OldName				[nvarchar](100),
	@OldDescription			[nvarchar](1000),
	@OldActive				[bit]
	)
AS
	BEGIN
		UPDATE 		[PrepChecklist]
			SET 	[Name] = @NewName
					, [Description] = @NewDescription
					, [Active] = @NewActive
			WHERE 	[PrepChecklistID] = @PrepChecklistID
			AND		[Name] = @OldName
			AND		[Description] = @OldDescription
			AND		[Active] = @OldActive
		RETURN @@ROWCOUNT		
	END
GO

print '' print '*** Creating sp_create_inspectionchecklist'
GO
CREATE PROCEDURE [dbo].[sp_create_inspectionchecklist]
	(
	@Name				[nvarchar](100),
	@Description		[nvarchar](1000)
	)
AS
	BEGIN
		INSERT INTO [dbo].[InspectionChecklist]
			([Name], [Description])
		VALUES
			(@Name, @Description)
		SELECT SCOPE_IDENTITY()
	END
GO
/* End Amanda T */

/* Brady F */
print '' print '*** Creating sp_retrieve_maintenance_record_by_id'
GO
CREATE PROCEDURE [dbo].[sp_retrieve_maintenance_record_by_id]
	(
	@MaintenanceRecordID	[int]
	)
AS
	BEGIN
		SELECT	[MaintenanceRecordID], [EquipmentID], [EmployeeID], [Description], [Date]
		FROM	[MaintenanceRecord]
		WHERE	[MaintenanceRecordID] = @MaintenanceRecordID
	END
GO

print '' print '*** Creating sp_edit_maintenance_record'
GO
CREATE PROCEDURE [dbo].[sp_edit_maintenance_record]
	(
	@MaintenanceRecordID [int],
	@NewEquipmentID		 [int],
	@OldEquipmentID		 [int],
	@NewEmployeeID		 [int],
	@OldEmployeeID		 [int],
	@NewDescription		 [nvarchar](1000),
	@OldDescription		 [nvarchar](1000),
	@NewDate			 [Date],
	@OldDate			 [Date]
	)
AS
	BEGIN
		UPDATE [MaintenanceRecord]
			SET [EquipmentID] = @NewEquipmentID,
				[EmployeeID] = @NewEmployeeID,
				[Description] = @NewDescription,
				[Date] = @NewDate
			WHERE [MaintenanceRecordID] = @MaintenanceRecordID
            AND [EquipmentID] = @OldEquipmentID
            AND [EmployeeID] = @OldEmployeeID
			AND [Description] = @OldDescription
			AND [Date] = @OldDate
		RETURN @@ROWCOUNT	
	END
GO

print '' print '*** Creating sp_create_maintenance_record'
GO
CREATE PROCEDURE [dbo].[sp_create_maintenance_record]
	(
	@EquipmentID		[int],
	@EmployeeID			[int],
	@Description		[nvarchar](1000),
	@Date 				[Date]
	)
AS
	BEGIN
		INSERT INTO	[dbo].[MaintenanceRecord]
			([EquipmentID], [EmployeeID], [Description], [Date])
		VALUES
			(@EquipmentID, @EmployeeID, @Description, @Date)
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

print '' print '*** Creating sp_edit_equipment_type'
GO
CREATE PROCEDURE [dbo].[sp_edit_equipment_type]
	(
	@EquipmentTypeID			[nvarchar](100),
    @OldInspectionChecklistID  	[int],
    @OldPrepChecklistID   		[int],
    @NewInspectionChecklistID   [int],
    @NewPrepChecklistID      	[int]
	)
AS
	BEGIN
		UPDATE [EquipmentType]
			SET [InspectionChecklistID] = @NewInspectionChecklistID,
				[PrepChecklistID] = @NewPrepChecklistID
			WHERE [EquipmentTypeID] = @EquipmentTypeID 
            AND [InspectionChecklistID] = @OldInspectionChecklistID
            AND [PrepChecklistID] = @OldPrepChecklistID
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

/* 
print '' print '*** Creating sp_retrieve_serviceoffering_by_packageid'
GO
CREATE PROCEDURE [dbo].[sp_retrieve_serviceoffering_by_packageid]
	(
	@ServicePackageID	[int]
	)
AS
	BEGIN
		SELECT	[ServiceOfferingID], [ServicePackageID], [Name], [Description]
		FROM	[ServiceOffering]
		WHERE	[ServiceOffering].[ServicePackageID] = @ServicePackageID
	END
GO
*/

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
/* End Brady F */

/* Dan C */
/* Weston O modified*/
print '' print '*** Creating sp_create_timeoff'
GO
CREATE PROCEDURE [dbo].[sp_create_timeoff]
	(
	@EmployeeID			[int],
	@StartTime			[DATETIME],
	@EndTime			[DATETIME],
	@Active				[bit]
	)
AS
	BEGIN
		INSERT INTO [dbo].[TimeOff]
			([EmployeeID], [StartTime], [EndTime], [Active])
		VALUES
			(@EmployeeID, @StartTime, @EndTime, @Active)
		SELECT SCOPE_IDENTITY()
	END
GO

/* Updated to include Active fields 2018/03/02 - John Miller */
print '' print '*** Creating sp_retrieve_timeoff_by_id'
GO
CREATE PROCEDURE [dbo].[sp_retrieve_timeoff_by_id]
	(
	@TimeOffID		[int]
	)
AS
	BEGIN
		SELECT	[TimeOffID], [EmployeeID], [StartTime], [EndTime], [Approved], [Active]
		FROM	[TimeOff]
		WHERE	[TimeOffID] = @TimeOffID
	END
GO

/* Updated to include Active fields 2018/03/02 - John Miller */
print '' print '*** Creating sp_retrieve_timeoff_by_employeeid'
GO
CREATE PROCEDURE [dbo].[sp_retrieve_timeoff_by_employeeid]
	(
	@EmployeeID			[int]
	)
AS
	BEGIN
		SELECT	[TimeOffID], [EmployeeID], [StartTime], [EndTime], [Approved], [Active]
		FROM	[TimeOff]
		WHERE	[EmployeeID] = @EmployeeID
	END
GO

/* John Miller - 2018/03/02 */
/* Weston Olund retrieve all rather than only active*/
print '' print '*** Creating sp_retrieve_timeoff_list'
GO
CREATE PROCEDURE [dbo].[sp_retrieve_timeoff_list]
AS
	BEGIN
		SELECT	[TimeOffID],[EmployeeID], [StartTime], [EndTime], [Approved],[Active]
		FROM	[TimeOff]
	END
GO


/* Updated to include Active fields 2018/03/02 - John Miller */
print '' print '*** Creating sp_edit_timeoff_by_timeoffid'
GO
CREATE PROCEDURE [dbo].[sp_edit_timeoff_by_timeoffid]
	(
	@TimeOffID				[int],
	@NewStartTime			[datetime],
	@NewEndTime				[datetime],
	@NewApproved			[bit],
	@NewActive				[bit],
	@OldStartTime			[datetime],
	@OldEndTime				[datetime],
	@OldApproved			[bit],
	@OldActive				[bit]
	)
AS
	BEGIN
		UPDATE [TimeOff]
			SET [StartTime] = @NewStartTime
			,	[EndTime] 	= @NewEndTime
			, 	[Approved] 	= @NewApproved
			,	[Active] = @NewActive
			WHERE [TimeOffID] = @TimeOffID
			AND [StartTime] = @OldStartTime
			AND [EndTime] = @OldEndTime
			AND [Approved] = @OldApproved
			AND [Active] = @OldActive
		RETURN @@ROWCOUNT
	END
GO

/* Created 2018/03/02 - John Miller */
print '' print '*** Creating sp_deactivate_timeoffrequest_by_id'
GO
CREATE PROCEDURE [dbo].[sp_deactivate_timeoffrequest_by_id]
	(
	@TimeOffID 		[int]
	)
AS
	BEGIN
		UPDATE 	[TimeOff]
			SET 	[Active] = 0
			WHERE 	[TimeOffID] = @TimeOffID
		RETURN @@ROWCOUNT
	END
GO

print '' print '*** Creating sp_delete_timeoff_by_id'
GO
CREATE PROCEDURE [dbo].[sp_delete_timeoff_by_id]
	(
	@TimeOffID		[int]
	)
AS
	BEGIN
		DELETE FROM [TimeOff]
			WHERE [TimeOffID] = @TimeOffID
		RETURN @@ROWCOUNT
	END
GO

print '' print '*** Creating sp_create_group'
GO
CREATE PROCEDURE [dbo].[sp_create_group]
	(
	@JobID			[int]
	)
AS
	BEGIN
		INSERT INTO [dbo].[Group]
			([JobID])
		VALUES
			(@JobID)
		SELECT SCOPE_IDENTITY()
	END
GO

print '' print '*** Creating sp_retrieve_group_by_id'
GO
CREATE PROCEDURE [dbo].[sp_retrieve_group_by_id]
	(
	@GroupID		[int]
	)
AS
	BEGIN
		SELECT	[GroupID], [JobID]
		FROM	[Group]
		WHERE	[GroupID] = @GroupID
	END
GO

print '' print '*** Creating sp_retrieve_group_by_jobid'
GO
CREATE PROCEDURE [dbo].[sp_retrieve_group_by_jobid]
	(
	@JobID			[int]
	)
AS
	BEGIN
		SELECT	[GroupID], [JobID]
		FROM	[Group]
		WHERE	[JobID] = @JobID
	END
GO

print '' print '*** Creating sp_edit_group_by_id'
GO
CREATE PROCEDURE [dbo].[sp_edit_group_by_id]
	(
	@GroupID		[int],
	@NewJobID		[int],
	@OldJobID		[int]

	)
AS
	BEGIN
		UPDATE [Group]
			SET [JobID] = @NewJobID
			WHERE [GroupID] = @GroupID
			AND [JobID] = @OldJobID
		RETURN @@ROWCOUNT
	END
GO

print '' print '*** Creating sp_delete_group_by_id'
GO
CREATE PROCEDURE [dbo].[sp_delete_group_by_id]
	(
	@GroupID	[int]
	)
AS
	BEGIN
		DELETE FROM [Group]
			WHERE [GroupID] = @GroupID
		RETURN @@ROWCOUNT
	END
GO

print '' print '*** Creating sp_create_employeerole'
GO
CREATE PROCEDURE [dbo].[sp_create_employeerole]
	(
	@EmployeeID		[int],
	@RoleID			[nvarchar](100)
	)
AS
	BEGIN
		INSERT INTO [dbo].[EmployeeRole]
			([EmployeeID], [RoleID])
		VALUES
			(@EmployeeID, @RoleID)
	END
GO

print '' print '*** Creating sp_retrieve_employeerole_by_employeeid'
GO
CREATE PROCEDURE [dbo].[sp_retrieve_employeerole_by_employeeid]
	(
	@EmployeeID		[int]
	)
AS
	BEGIN
		SELECT	[EmployeeID], [RoleID]
		FROM	[EmployeeRole]
		WHERE	[EmployeeID] = @EmployeeID
	END
GO

print '' print '*** Creating sp_edit_employeerole_by_employeeid'
GO
CREATE PROCEDURE [dbo].[sp_edit_employeerole_by_employeeid]
	(
	@EmployeeID				[int],
	@NewRoleID				[nvarchar](100),
	@OldRoleID				[nvarchar](100)
	)
AS
	BEGIN
		UPDATE [EmployeeRole]
			SET [RoleID] = @NewRoleID
			WHERE [EmployeeID] = @EmployeeID
			AND [RoleID] = @OldRoleID
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

print '' print '*** Creating sp_retrieve_equipment_list'
GO
CREATE PROCEDURE [dbo].[sp_retrieve_equipment_list]

AS
	BEGIN
		SELECT 		[EquipmentID], [EquipmentTypeID], [Name], [MakeModelID], 
					[DatePurchased], [DateLastRepaired],[PriceAtPurchase], 
					[CurrentValue], [WarrantyUntil], [EquipmentStatusID], 
					[EquipmentDetails], [Active]
		FROM 		[Equipment]
		ORDER BY 	[EquipmentID]
	END
GO

print '' print '*** Creating sp_retrieve_equipment_by_id'
GO
CREATE PROCEDURE [dbo].[sp_retrieve_equipment_by_id]
	(
	@EquipmentID 	[nvarchar](100)
	)
AS
	BEGIN
		SELECT 		[EquipmentID], [EquipmentTypeID], [Name], [MakeModelID], 
					[DatePurchased], [DateLastRepaired],[PriceAtPurchase], 
					[CurrentValue], [WarrantyUntil], [EquipmentStatusID], 
					[EquipmentDetails], [Active]
		FROM 		[Equipment]
		WHERE 		[EquipmentID] = @EquipmentID
		ORDER BY 	[EquipmentID]
	END
GO

print '' print '*** Creating sp_retrieve_equipment_by_name'
GO
CREATE PROCEDURE [dbo].[sp_retrieve_equipment_by_name]
	(
	@Name 	[nvarchar](100)
	)
AS
	BEGIN
		SELECT 		[EquipmentID], [EquipmentTypeID], [Name], [MakeModelID], 
					[DatePurchased], [DateLastRepaired],[PriceAtPurchase], 
					[CurrentValue], [WarrantyUntil], [EquipmentStatusID], 
					[EquipmentDetails], [Active]
		FROM 		[Equipment]
		WHERE 		[Name] = @Name
		ORDER BY 	[EquipmentID]
	END
GO

print '' print '*** Creating sp_retrieve_equipment_by_status'
GO
CREATE PROCEDURE [dbo].[sp_retrieve_equipment_by_status]
	(
	@EquipmentStatusID 	[nvarchar](100)
	)
AS
	BEGIN
		SELECT 		[EquipmentID], [EquipmentTypeID], [Name], [MakeModelID], 
					[DatePurchased], [DateLastRepaired],[PriceAtPurchase], 
					[CurrentValue], [WarrantyUntil], [EquipmentStatusID], 
					[EquipmentDetails], [Active]
		FROM 		[Equipment]
		WHERE 		[EquipmentStatusID] = @EquipmentStatusID
		ORDER BY 	[EquipmentID]
	END
GO

print '' print '*** Creating sp_retrieve_equipment_by_type'
GO
CREATE PROCEDURE [dbo].[sp_retrieve_equipment_by_type]
	(
	@EquipmentTypeID 	[nvarchar](100)
	)
AS
	BEGIN
		SELECT 		[EquipmentID], [EquipmentTypeID], [Name], [MakeModelID], 
					[DatePurchased], [DateLastRepaired],[PriceAtPurchase], 
					[CurrentValue], [WarrantyUntil], [EquipmentStatusID], 
					[EquipmentDetails], [Active]
		FROM 		[Equipment]
		WHERE 		[EquipmentTypeID] = @EquipmentTypeID
		ORDER BY 	[EquipmentID]
	END
GO



print '' print '*** Creating sp_retrieve_equipment_by_active'
GO
CREATE PROCEDURE [dbo].[sp_retrieve_equipment_by_active]
	(
	@Active 	[bit]
	)
AS
	BEGIN
		SELECT 		[EquipmentID], [EquipmentTypeID], [Name], [MakeModelID], 
					[DatePurchased], [DateLastRepaired],[PriceAtPurchase], 
					[CurrentValue], [WarrantyUntil], [EquipmentStatusID], 
					[EquipmentDetails], [Active]
		FROM 		[Equipment]
		WHERE 		[Active] = @Active
		ORDER BY 	[EquipmentID]
	END
GO

print '' print '*** Creating sp_edit_equipment_by_id'
GO
CREATE PROCEDURE [dbo].[sp_edit_equipment_by_id]
	(
	@NewEquipmentTypeID		[nvarchar](100),
	@NewName				[nvarchar](100),
	@NewMakeModelID			[int],
	@NewDatePurchased		[date],
	@NewDateLastRepaired	[date],
	@NewPriceAtPurchase		[decimal],
	@NewCurrentValue		[decimal],
	@NewWarrantyUntil		[date],
	@NewEquipmentStatusID	[nvarchar](100),
	@NewEquipmentDetails	[nvarchar](1000),
	
	@OldEquipmentTypeID		[nvarchar](100),
	@OldName				[nvarchar](100),
	@OldMakeModelID			[int],
	@OldDatePurchased		[date],
	@OldDateLastRepaired	[date],
	@OldPriceAtPurchase		[decimal],
	@OldCurrentValue		[decimal],
	@OldWarrantyUntil		[date],
	@OldEquipmentStatusID	[nvarchar](100),
	@OldEquipmentDetails	[nvarchar](1000),
	@EquipmentID			[int]
	)
AS
	BEGIN
		UPDATE 		[Equipment]
			SET 	[EquipmentTypeID] = @NewEquipmentTypeID,
					[Name] = @NewName,
					[MakeModelID] = @NewMakeModelID,
					[DatePurchased] = @NewDatePurchased,
					[DateLastRepaired] = @NewDateLastRepaired,
					[PriceAtPurchase] = @NewPriceAtPurchase,
					[CurrentValue] = @NewCurrentValue,
					[WarrantyUntil] = @NewWarrantyUntil,
					[EquipmentStatusID] = @NewEquipmentStatusID,
					[EquipmentDetails] = @NewEquipmentDetails
					
			WHERE 	[EquipmentID] = @EquipmentID
			AND		[EquipmentTypeID] = @OldEquipmentTypeID
			AND		[Name] = @OldName
			AND		[MakeModelID] = @OldMakeModelID
			AND		[DatePurchased] = @OldDatePurchased
			AND		[DateLastRepaired] = @OldDateLastRepaired
			AND		[PriceAtPurchase] = @OldPriceAtPurchase
			AND		[CurrentValue] = @OldCurrentValue
			AND		[WarrantyUntil] = @OldWarrantyUntil
			AND		[EquipmentStatusID] = @OldEquipmentStatusID
			AND		[EquipmentDetails] = @OldEquipmentDetails
		RETURN @@ROWCOUNT		
	END
GO

print '' print '*** Creating sp_retrieve_inspectionchecklist_by_id'
GO
CREATE PROCEDURE [dbo].[sp_retrieve_inspectionchecklist_by_id]
	(
	@InspectionChecklistID 		[int]
	)
AS
	BEGIN
		SELECT 		[InspectionChecklistID], [Name], [Description], [Active]
		FROM 		[InspectionChecklist]
		WHERE 		[InspectionChecklistID] = @InspectionChecklistID
		ORDER BY 	[InspectionChecklistID]
	END
GO

print '' print '*** Creating sp_delete_inspectionchecklist_by_id'
GO
CREATE PROCEDURE [dbo].[sp_delete_inspectionchecklist_by_id]
	(
	@InspectionChecklistID 		[int]
	)
AS
	BEGIN
		DELETE FROM 	[InspectionChecklist]
		WHERE 			[InspectionChecklistID] = @InspectionChecklistID
		RETURN @@ROWCOUNT
	END
GO

print '' print '*** Creating sp_deactivate_inspectionchecklist_by_id'
GO
CREATE PROCEDURE [dbo].[sp_deactivate_inspectionchecklist_by_id]
	(
	@InspectionChecklistID 		[int]
	)
AS
	BEGIN
		UPDATE 	[InspectionChecklist]
			SET 	[Active] = 0
			WHERE 	[InspectionChecklistID] = @InspectionChecklistID
		RETURN @@ROWCOUNT
	END
GO
/* End John M */

/* Jayden T */
/* fixed by Badis Saidani */
print '' print '*** Creating sp_retrieve_source_detail_list'
GO
CREATE PROCEDURE [dbo].[sp_retrieve_source_detail_list]
AS
	BEGIN
		SELECT [SourceID], [SupplyItem].[SupplyItemID], [SpecialOrderItem].[SpecialOrderItemID], [Vendor].[VendorId], [MinimumOrderQTY], [PriceEach], [LeadTime], [Source].[Active], [SpecialOrderItem].[Name],
		[Vendor].[Name], [SupplyItem].[Name]
		FROM [SpecialOrderItem], [Vendor], [SupplyItem], [Source]
		WHERE [Source].[VendorID] = [Vendor].[VendorID]
		AND [Source].[SpecialOrderItemID] = [SpecialOrderItem].[SpecialOrderItemID]
		AND [Source].[SupplyItemID] = [SupplyItem].[SupplyItemID]
	END

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

print '' print '*** Creating sp_create_preprecord'
GO
CREATE PROCEDURE [dbo].[sp_create_preprecord]
	(
	@EquipmentID		[int],
	@EmployeeID			[int],
	@Description		[nvarchar](1000),
	@Date				[date]
	)
AS
	BEGIN
		INSERT INTO [dbo].[PrepRecord]
			([EquipmentID], [EmployeeID], [Description], [Date])
		VALUES
			(@EquipmentID, @EmployeeID, @Description, @Date)
			
		SELECT SCOPE_IDENTITY()
	END
GO
/* End Jayden T */

/* Jacob C */
print '' print '*** Creating sp_create_exceptiontype'
GO
CREATE PROCEDURE [dbo].[sp_create_exceptiontype]
	(
		@ExceptionTypeID		[nvarchar](100)
	)
AS
	BEGIN
		INSERT INTO [dbo].[ExceptionType]
		([ExceptionTypeID])
		VALUES
		(@ExceptionTypeID)
		RETURN @@ROWCOUNT
	END
GO


print '' print '*** Creating sp_retrieve_exceptiontype_list'
GO
CREATE PROCEDURE [dbo].[sp_retrieve_exceptiontype_list]
AS
	BEGIN
		SELECT 	[ExceptionTypeID]
		FROM 	[ExceptionType]
	END
GO


print '' print '*** Creating sp_retrieve_exception_by_id'
GO
CREATE PROCEDURE [dbo].[sp_retrieve_exception_by_id]
	(
		@ExceptionTypeID		[nvarchar](100)
	)
AS
	BEGIN
		SELECT 	[ExceptionTypeID]
		FROM 	[ExceptionType]
		WHERE 	[ExceptionTypeID] = @ExceptionTypeID
	END
GO


print '' print '*** Creating sp_edit_exceptiontype'
GO
CREATE PROCEDURE [dbo].[sp_edit_exceptiontype]
	(
		@NewExceptionTypeID			[nvarchar](100),
		@OldExceptionTypeID			[nvarchar](100)
	)
AS
	BEGIN
		UPDATE [dbo].[ExceptionType]
			SET 	[ExceptionTypeID] = @NewExceptionTypeID
			WHERE 	[ExceptionTypeID] = @OldExceptionTypeID
		RETURN @@ROWCOUNT
	END
GO

print '' print '*** Creating sp_delete_exceptiontype_by_id'
GO
CREATE PROCEDURE [dbo].[sp_delete_exceptiontype_by_id]
	(
		@ExceptionTypeID				[nvarchar](100)
	)
AS
	BEGIN
		DELETE FROM [ExceptionType]
			WHERE [ExceptionTypeID] = @ExceptionTypeID
		RETURN @@ROWCOUNT
	END
GO


print '' print '*** Creating sp_create_vendor'
GO
CREATE PROCEDURE [dbo].[sp_create_vendor]
	(
		@Name					[nvarchar](100),
		@Rep					[nvarchar](100),
		@Address				[nvarchar](250),
		@Website				[nvarchar](250),
		@Phone					[nvarchar](15)
	)
AS
	BEGIN
		INSERT INTO [dbo].[Vendor]
		([Name], [Rep], [Address], [Website], [Phone])
		VALUES
		(@Name, @Rep, @Address, @Website, @Phone)
		RETURN @@ROWCOUNT
	END
GO


print '' print '*** Creating sp_retrieve_vendor_list'
GO
CREATE PROCEDURE [dbo].[sp_retrieve_vendor_list]
AS
	BEGIN
		SELECT 	[VendorID], [Name], [Rep],
				[Address], [Website], [Phone], [Active]
		FROM 	[Vendor]
	END
GO


print '' print '*** Creating sp_retrieve_vendor_by_id'
GO
CREATE PROCEDURE [dbo].[sp_retrieve_vendor_by_id]
	(
		@VendorID		[int]
	)
AS
	BEGIN
		SELECT 	[VendorID], [Name], [Rep],
				[Address], [Website], [Phone], [Active]
		FROM 	[Vendor]
		WHERE 	[VendorID] = @VendorID
	END
GO


print '' print '*** Creating sp_retrieve_vendor_by_active'
GO
CREATE PROCEDURE [dbo].[sp_retrieve_vendor_by_active]
AS
	BEGIN
		SELECT 	[VendorID], [Name], [Rep],
				[Address], [Website], [Phone], [Active]
		FROM 	[Vendor]
		WHERE 	[Active] = 1
	END
GO


print '' print '*** Creating sp_retrieve_vendor_by_name'
GO
CREATE PROCEDURE [dbo].[sp_retrieve_vendor_by_name]
	(
		@Name		[nvarchar](100)
	)
AS
	BEGIN
		SELECT 	[VendorID], [Name], [Rep],
				[Address], [Website], [Phone], [Active]
		FROM 	[Vendor]
		WHERE 	[Name] = @Name
	END
GO


print '' print '*** Creating sp_retrieve_vendor_by_rep'
GO
CREATE PROCEDURE [dbo].[sp_retrieve_vendor_by_rep]
	(
		@Rep		[nvarchar](100)
	)
AS
	BEGIN
		SELECT 	[VendorID], [Name], [Rep],
				[Address], [Website], [Phone], [Active]
		FROM 	[Vendor]
		WHERE 	[Rep] = @Rep
	END
GO



print '' print '*** Creating sp_edit_vendor'
GO
CREATE PROCEDURE [dbo].[sp_edit_vendor]
	(
		@VendorID			[int],
		@OldName			[nvarchar](100),
		@NewName			[nvarchar](100),
		@OldRep				[nvarchar](100),
		@NewRep				[nvarchar](100),
		@OldAddress			[nvarchar](250),
		@NewAddress			[nvarchar](250),
		@OldWebsite			[nvarchar](250),
		@NewWebsite			[nvarchar](250),
		@OldPhone			[nvarchar](15),
		@NewPhone			[nvarchar](15),
		@OldActive			[bit],
		@NewActive			[bit]
	)
AS
	BEGIN
		UPDATE [dbo].[Vendor]
			SET 	[Name] = @NewName,
					[Rep] = @NewRep,
					[Address] = @NewAddress,
					[Website] = @NewWebsite,
					[Phone] = @NewPhone,
					[Active] = @NewActive
			WHERE 	[VendorID] = @VendorID
			AND		[Name] = @OldName
			AND 	[Rep] = @OldRep
			AND		[Address] = @OldAddress
			AND		[Website] = @OldWebsite
			AND		[Phone] = @OldPhone
			AND		[Active] = @OldActive
		RETURN @@ROWCOUNT
	END
GO

print '' print '*** Creating sp_edit_vendor_active'
GO
CREATE PROCEDURE [dbo].[sp_edit_vendor_active]
	(
		@VendorID			[int]
	)
AS
	BEGIN
		UPDATE [dbo].[Vendor]
			SET 	[Active] = 1
			WHERE 	[VendorID] = @VendorID
			AND		[Active] = 0
		RETURN @@ROWCOUNT
	END
GO


print '' print '*** Creating sp_deactivate_vendor_by_id'
GO
CREATE PROCEDURE [dbo].[sp_deactivate_vendor_by_id]
	(
		@VendorID			[int]
	)
AS
	BEGIN
		UPDATE [dbo].[Vendor]
			SET 	[Active] = 0
			WHERE 	[VendorID] = @VendorID
			AND		[Active] = 1
		RETURN @@ROWCOUNT
	END
GO


print '' print '*** Creating sp_delete_vendor_by_id'
GO
CREATE PROCEDURE [dbo].[sp_delete_vendor_by_id]
	(
		@VendorID				[int]
	)
AS
	BEGIN
		DELETE FROM [Vendor]
			WHERE [VendorID] = @VendorID
		RETURN @@ROWCOUNT
	END
GO
/* End Jacob C */

/* Reuben C */
print '' print '*** Creating sp_create_payment_type'
GO
CREATE PROCEDURE [dbo].[sp_create_payment_type]
	(
	@PaymentTypeID	[nvarchar](100),
	@Description	[nvarchar](1000)
	)
AS
	BEGIN
		INSERT INTO [dbo].[PaymentType]
			([PaymentTypeID],[Description])
		VALUES
			(@PaymentTypeID, @Description)
		RETURN @@ROWCOUNT
	END
GO

print '' print '*** Creating sp_deactivate_specialorderitem_by_id'
GO
CREATE PROCEDURE [dbo].[sp_deactivate_specialorderitem_by_id]
	(
	@SpecialOrderItemID	[int]
	)
AS
	BEGIN
		UPDATE	[SpecialOrderItem]
			SET		[Active] = 0
			WHERE	[SpecialOrderItemID] = @SpecialOrderItemID
		RETURN @@ROWCOUNT
	END
GO

GO

GO
SET QUOTED_IDENTIFIER ON
GO

print '' print '*** Creating sp_create_source'
GO
CREATE PROCEDURE [dbo].[sp_create_source]
	(
	@SupplyItemID		[int],
	@SpecialOrderItemID	[int],
	@VendorID			[int],
	@MinimumOrderQTY	[int],
	@PriceEach			[money],
	@LeadTime			[int],
	@Active				[bit]
	)
AS
	BEGIN
		INSERT INTO	[dbo].[Source]
			([SupplyItemID],[SpecialOrderItemID],[VendorID],[MinimumOrderQTY],[PriceEach],
			[LeadTime],[Active])
		VALUES
			(@SupplyItemID,@SpecialOrderItemID,@VendorID,@MinimumOrderQTY,@PriceEach,
			@LeadTime,@Active)
		SELECT SCOPE_IDENTITY()
	END
GO

print '' print '*** Creating sp_retrieve_source_list'
GO
CREATE PROCEDURE [dbo].[sp_retrieve_source_list]
AS
	BEGIN
		SELECT 	[SourceID],[SupplyItemID],[SpecialOrderItemID],[VendorID],
				[MinimumOrderQTY],[PriceEach],[LeadTime],[Active]
		FROM	[Source]
	END
GO

print '' print '*** Creating sp_retrieve_source_by_sourceid'
GO
CREATE PROCEDURE [dbo].[sp_retrieve_source_list_by_sourceid]
	(
	@SourceID	[int]
	)
AS
	BEGIN
		SELECT 	[SourceID],[SupplyItemID],[SpecialOrderItemID],[VendorID],
				[MinimumOrderQTY],[PriceEach],[LeadTime],[Active]
		FROM	[Source]
		WHERE	[SourceID] = @SourceID
	END
GO

print '' print '*** Creating sp_retrieve_source_by_supplyitemid'
GO
CREATE PROCEDURE [dbo].[sp_retrieve_source_list_by_supplyitemid]
	(
	@SupplyItemID	[int]
	)
AS
	BEGIN
		SELECT 	[SourceID],[SupplyItemID],[SpecialOrderItemID],[VendorID],
				[MinimumOrderQTY],[PriceEach],[LeadTime],[Active]
		FROM	[Source]
		WHERE	[SupplyItemID] = @SupplyItemID
	END
GO

print '' print '*** Creating sp_retrieve_source_by_specialorderitemid'
GO
CREATE PROCEDURE [dbo].[sp_retrieve_source_list_by_specialorderitemid]
	(
	@SpecialOrderItemID	[int]
	)
AS
	BEGIN
		SELECT 	[SourceID],[SupplyItemID],[SpecialOrderItemID],[VendorID],
				[MinimumOrderQTY],[PriceEach],[LeadTime],[Active]
		FROM	[Source]
		WHERE	[SpecialOrderItemID] = @SpecialOrderItemID
	END
GO

print '' print '*** Creating sp_retrieve_source_by_active'
GO
CREATE PROCEDURE [dbo].[sp_retrieve_source_by_active]
	(
	@Active	[bit]
	)
AS
	BEGIN
		SELECT 	[SourceID],[SupplyItemID],[SpecialOrderItemID],[VendorID],
				[MinimumOrderQTY],[PriceEach],[LeadTime],[Active]
		FROM	[Source]
		WHERE	[Active] = @Active
	END
GO

print '' print '*** Creating sp_update_source'
GO
CREATE PROCEDURE [dbo].[sp_update_source]
	(
	@SourceID				[int],
	@NewSupplyItemID		[int],
	@NewSpecialOrderItemID	[int],
	@NewVendorID			[int],
	@NewMinimumOrderQTY		[int],
	@NewPriceEach			[money],
	@NewLeadTime			[int],
	@NewActive				[bit],
	@OldSupplyItemID		[int],
	@OldSpecialOrderItemID	[int],
	@OldVendorID			[int],
	@OldMinimumOrderQTY		[int],
	@OldPriceEach			[money],
	@OldLeadTime			[int],
	@OldActive				[bit]
	)
AS
	BEGIN
		UPDATE [Source]
			SET 	[SupplyItemID] = @NewSupplyItemID,
					[SpecialOrderItemID] = @NewSpecialOrderItemID,
					[VendorID] = @NewVendorID,
					[MinimumOrderQTY] = @NewMinimumOrderQTY,
					[PriceEach] = @NewPriceEach,
					[LeadTime] = @NewLeadTime,
					[Active] = @NewActive
			WHERE	[SourceID] = @SourceID
			AND		[SupplyItemID] = @OldSupplyItemID
			AND		[SpecialOrderItemID] = @OldSpecialOrderItemID
			AND		[VendorID] = @OldVendorID
			AND		[MinimumOrderQTY] = @OldMinimumOrderQTY
			AND		[PriceEach] = @OldPriceEach
			AND		[LeadTime] = @OldLeadTime
			AND		[Active] = @OldActive
		RETURN @@ROWCOUNT
	END
GO

print '' print '*** Creating sp_update_source_active'
GO
CREATE PROCEDURE [dbo].[sp_update_source_active]
	(
	@SourceID	[int],
	@Active 	[bit]
	)
AS
	BEGIN
		UPDATE [Source]
			SET	[Active] = @Active
			WHERE	[SourceID] = @SourceID
		RETURN @@ROWCOUNT
	END
GO

print '' print '*** Creating sp_deactivate_source_by_sourceid'
GO
CREATE PROCEDURE [dbo].[sp_deactivate_source_by_sourceid]
	(
	@SourceID	[int]	
	)
AS
	BEGIN
		UPDATE [Source]
			SET		[Active] = 0
			WHERE	[SourceID] = @SourceID
		RETURN @@ROWCOUNT
	END
GO

print '' print '*** Creating sp_delete_source_by_sourceid'
GO
CREATE PROCEDURE [dbo].[sp_delete_source_by_sourceid]
	(
	@SourceID	[int]
	)
AS
	BEGIN
		DELETE FROM [Source]
			WHERE	[SourceID] = @SourceID
		RETURN @@ROWCOUNT
	END
GO

print '' print '*** Creating sp_retrieve_specialorderitem_by_id'
GO
CREATE PROCEDURE [dbo].[sp_retrieve_specialorderitem_by_id]
	(
	@SpecialOrderItemID [int]
	)
AS
	BEGIN
		SELECT 	[SpecialOrderItemID], [Name]
		FROM	[SpecialOrderItem]
		WHERE	[SpecialOrderItemID] = @SpecialOrderItemID
	END
GO

print '' print '*** Creating sp_retrieve_specialorderitem_by_name'
GO
CREATE PROCEDURE [dbo].[sp_retrieve_specialorderitem_by_name]
	(
	@Name [nvarchar](100)
	)
AS
	BEGIN
		SELECT 	[SpecialOrderItemID],[Name]
		FROM	[SpecialOrderItem]
		WHERE	[Name] = @Name
	END
GO

print '' print '*** Creating sp_delete_specialorderitem_by_id'
GO
CREATE PROCEDURE [dbo].[sp_delete_specialorderitem_by_id]
	(
	@SpecialOrderItemID	[int]
	)
AS
	BEGIN
		DELETE FROM	[SpecialOrderItem]
			WHERE [SpecialOrderItemID] = @SpecialOrderItemID
		RETURN @@ROWCOUNT
	END
GO

print '' print '*** Creating sp_create_specialorderline'
GO
CREATE PROCEDURE [dbo].[sp_create_specialorderline]
	(
	
	@SpecialOrderItemID	[int],
	@Quantity			[int]
	)
AS
	BEGIN
		INSERT INTO [dbo].[SpecialOrderLine]
			([SpecialOrderItemID],[Quantity])
		VALUES
			(@SpecialOrderItemID,@Quantity)
		RETURN @@ROWCOUNT
	END
GO	

print '' print '*** Creating sp_retrieve_specialorderline_by_id'
GO
CREATE PROCEDURE [dbo].[sp_retrieve_specialorderline_by_id]
	(
	@SpecialOrderLineID	[int]
	)
AS
	BEGIN
		SELECT	[SpecialOrderLineID],[SpecialOrderItemID],[Quantity]
		FROM	[SpecialOrderLine]
		WHERE	[SpecialOrderLineID] = @SpecialOrderLineID
	END
GO

/* 
print '' print '*** Creating sp_retrieve_specialorderline_by_specialorderid'
GO
CREATE PROCEDURE [dbo].[sp_retrieve_specialorderline_by_specialorderid]
	(
	@SpecialOrderID	[int]
	)
AS
	BEGIN
		SELECT	[SpecialOrderLineID],[SpecialOrderID],[SpecialOrderItemID],[Quantity]
		FROM	[SpecialOrderLine]
		WHERE	[SpecialOrderID] = @SpecialOrderID
	END
GO
 */
 
print '' print '*** Creating sp_edit_specialorderline'
GO
CREATE PROCEDURE [dbo].[sp_edit_specialorderline]
	(
	@SpecialOrderLineID	[int],
	@OldQuantity		[int],
	@NewQuantity		[int]
	)
AS
	BEGIN
		UPDATE	[SpecialOrderLine]
			SET		[Quantity] = @NewQuantity
			WHERE	[SpecialOrderLineID] = @SpecialOrderLineID
			AND		[Quantity] = @OldQuantity
		RETURN @@ROWCOUNT
	END
GO

print '' print '*** Creating sp_delete_specialorderline'
GO
CREATE PROCEDURE [dbo].[sp_delete_specialorderline]
	(
	@SpecialOrderLineID	[int]
	)
AS
	BEGIN
		DELETE FROM	[SpecialOrderLine]
			WHERE	[SpecialOrderLineID] = @SpecialOrderLineID
		RETURN @@ROWCOUNT
	END
GO

/* Tested by James McPherson 2/3/2018 */
print '' print '*** Creating sp_authenticate_user'
GO
CREATE PROCEDURE [dbo].[sp_authenticate_user]
	(
	@Email			[nvarchar](100),
	@PasswordHash	[nvarchar](100)
	)
AS
	BEGIN
		SELECT COUNT([EmployeeID])
		FROM 	[Employee]
		WHERE 	[Email] = @Email
		AND 	[PasswordHash] = @PasswordHash
		AND		[Active] = 1
	END
GO

/* Tested by James McPherson 2/3/2018 */
print '' print '*** Creating sp_update_passwordHash'
GO
CREATE PROCEDURE [dbo].[sp_update_passwordHash]
	(
	@EmployeeID			[int],
	@OldPasswordHash	[nvarchar](100),
	@NewPasswordHash	[nvarchar](100)
	)
AS
	BEGIN
		UPDATE [Employee]
			SET [PasswordHash] = @NewPasswordHash
			WHERE [EmployeeID] = @EmployeeID
			AND [PasswordHash] = @OldPasswordHash
		RETURN @@ROWCOUNT
	END
GO

/* Tested by James McPherson 2/3/2018 */
print '' print '*** Creating sp_retrieve_employee_by_email'
GO
CREATE PROCEDURE [dbo].[sp_retrieve_employee_by_email]
	(
	@Email		[nvarchar](100)
	)
AS
	BEGIN
		SELECT 	[EmployeeID], [FirstName], [LastName], [Address]
				, [PhoneNumber], [Email], [Active]
		FROM 	[Employee]
		WHERE 	[Email] = @Email
	END
GO

/* Tested by James McPherson 2/17/2018 */
/* Added a join to allow the creation of role objects */
print '' print '*** Creating sp_retrieve_employee_roles'
GO
CREATE PROCEDURE [dbo].[sp_retrieve_employee_roles]
	(
	@EmployeeID		[int]
	)
AS
	BEGIN
		SELECT 	[EmployeeRole].[RoleID], [Description] 
		FROM 	[EmployeeRole], [Role]
		WHERE 	[EmployeeRole].[EmployeeID] = @EmployeeID
		AND		[EmployeeRole].[RoleID] = [Role].[RoleID]
	END
GO
/* End Reuben C */

/* Sam D */
print '' print '*** Creating sp_retrieve_supplyitem_by_id'
GO
CREATE PROCEDURE [dbo].[sp_retrieve_supplyitem_by_id]
	(
	@SupplyItemID		[int]
	)
AS
	BEGIN
		SELECT 	[SupplyItemID], [Name], [Description], [Location], [QuantityInStock], [ReorderLevel], [ReorderQuantity]
		FROM 	[SupplyItem]
		WHERE 	[SupplyItemID] = @SupplyItemID
	END
GO

print '' print '*** Creating sp_retrieve_supplyitem_by_active'
GO
CREATE PROCEDURE [dbo].[sp_retrieve_supplyitem_by_active]
	(
	@Active		[bit]
	)
AS
	BEGIN
		SELECT 	[SupplyItemID], [Name], [Description], [Location], [QuantityInStock], [ReorderLevel], [ReorderQuantity]
		FROM 	[SupplyItem]
		WHERE 	[Active] = @Active
        ORDER BY [SupplyItemID]
	END
GO

print '' print '*** Creating sp_retrieve_supplyitem_by_reorderlevel'
GO
CREATE PROCEDURE [dbo].[sp_retrieve_supplyitem_by_reorderlevel]
	(
	@ReorderLevel		[bit]
	)
AS
	BEGIN
		SELECT 	[SupplyItemID], [Name], [Description], [Location], [QuantityInStock], [ReorderLevel], [ReorderQuantity]
		FROM 	[SupplyItem]
		WHERE 	[QuantityInStock] < [ReorderLevel]
        ORDER BY [SupplyItemID]
	END
GO

print '' print '*** Creating sp_retrieve_supplyitem_by_vendorid'

GO
CREATE PROCEDURE [dbo].[sp_retrieve_supplyitem_by_vendorid]
	(
	@VendorID		[int]
	)
AS
	BEGIN
		SELECT 	[SupplyItem].[SupplyItemID], [SupplyItem].[Name], [SupplyItem].[Description], [SupplyItem].[Location], [SupplyItem].[QuantityInStock], [SupplyItem].[ReorderLevel], [SupplyItem].[ReorderQuantity]
		FROM 	[SupplyItem], [Source]
		WHERE 	[SupplyItem].[SupplyItemID] = [Source].[SupplyItemID]
        AND     [Source].[VendorID] = @VendorID
		AND		[Source].[Active] = 1
	END
GO

print '' print '*** Creating sp_deactivate_supplyitem_by_id'
GO
CREATE PROCEDURE [dbo].[sp_deactivate_supplyitem_by_id]
	(
	@SupplyItemID				[int]
	)
AS
	BEGIN
		UPDATE [SupplyItem]
			SET [Active] = 0
			WHERE [SupplyItemID] = @SupplyItemID 
		RETURN @@ROWCOUNT
	END
GO

print '' print '*** Creating sp_delete_supplyitem_by_id'
GO
CREATE PROCEDURE [dbo].[sp_delete_supplyitem_by_id]
	(
	@SupplyItemID				[int]
	)
AS
	BEGIN
		DELETE FROM [SupplyItem]
			WHERE [SupplyItemID] = @SupplyItemID 
		RETURN @@ROWCOUNT
	END
GO

print '' print '*** Creating sp_create_resupplyorder'
GO
CREATE PROCEDURE [dbo].[sp_create_resupplyorder]
	(
	@EmployeeID			    [int],
	@Date	      			[date],
  @SupplyStatusID         [nvarchar](100),
  @VendorID					[int]
	)
AS
	BEGIN
		INSERT INTO [dbo].[ResupplyOrder]
		([EmployeeID], [Date], [SupplyStatusID], [VendorID])
		VALUES	(
			@EmployeeID, @Date, @SupplyStatusID, @VendorID
		)
		SELECT SCOPE_IDENTITY()
	END
GO

print '' print '*** Creating sp_retrieve_resupplyorder_by_id'

GO
CREATE PROCEDURE [dbo].[sp_retrieve_resupplyorder_by_id]
	(
	@ResupplyOrderID		[int]
	)
AS
	BEGIN
		SELECT 	[ResupplyOrderID], [EmployeeID], [Date], [SupplyStatusID], [VendorID]
		FROM 	[ResupplyOrder]
		WHERE 	[ResupplyOrderID] = @ResupplyOrderID
	END
GO

print '' print '*** Creating sp_edit_resupplyorder_by_id'

GO
CREATE PROCEDURE [dbo].[sp_edit_resupplyorder_by_id]
	(
	@ResupplyOrderID		[int],
    @OldEmployeeID          [int],
    @OldDate   				[date],
    @OldSupplyStatusID      [nvarchar](100),
    @NewEmployeeID          [int],
    @NewDate   				[date],
    @NewSupplyStatusID      [nvarchar](100)
	)
AS
	BEGIN
		UPDATE [ResupplyOrder]
			SET [EmployeeID] = @NewEmployeeID 
			, [Date] = @NewDate
            , [SupplyStatusID] = @NewSupplyStatusID
			WHERE [EmployeeID] = @OldEmployeeID 
            AND [Date] = @OldDate
            AND [SupplyStatusID] = @OldSupplyStatusID
            AND [ResupplyOrderID] = @ResupplyOrderID
		RETURN @@ROWCOUNT
	END
GO
/* End Sam D */

/* David X */
print '' print  '*** Creating procedure sp_delete_employeerole'
GO
CREATE PROCEDURE [dbo].[sp_delete_employeerole]
	(
	@EmployeeID			[int],
	@RoleID				[nvarchar](100)
	)
AS
	BEGIN
		DELETE FROM [EmployeeRole]
		WHERE [EmployeeID] = @EmployeeID
		AND [RoleID] = @RoleID
	END
GO

print '' print  '*** Creating procedure sp_retrieve_employeerole_list'
GO
CREATE PROCEDURE [dbo].[sp_retrieve_employeerole_list]
AS
	BEGIN
		SELECT [EmployeeID], [RoleID]
		FROM [EmployeeRole]
	END
GO

print '' print  '*** Creating procedure sp_retrieve_employee_list'
GO
CREATE PROCEDURE [dbo].[sp_retrieve_employee_list]
AS
	BEGIN
		SELECT [EmployeeID], [FirstName], [LastName], [Address]
		, [PhoneNumber], [Email], [Active]
		FROM [Employee]
	END
GO
/* End David X */

/* Weston O */
print '' print '*** Creating sp_retrieve_certification_list'
GO
CREATE PROCEDURE [dbo].[sp_retrieve_certification_list]
AS
	BEGIN
		SELECT	[CertificationID], [Name], [Description], [Active]
		FROM	[Certification]
	END
GO

print '' print '*** Creating sp_retrieve_resupplyorder_list'
GO
CREATE PROCEDURE [dbo].[sp_retrieve_resupplyorder_list]
AS
	BEGIN
		SELECT	[ResupplyOrderID], [EmployeeID], [Date], [SupplyStatusID], [VendorID]
		FROM	[ResupplyOrder]
	END
GO
/* End Weston O */

/* Zach M */
print '' print '*** Creating sp_create_maintenancechecklist'
GO
CREATE PROCEDURE [dbo].[sp_create_maintenancechecklist]
	(
	@Name				[nvarchar](100),
	@Description		[nvarchar](1000)
	)
AS
	BEGIN
		INSERT INTO [dbo].[MaintenanceChecklist]
			([Name], [Description])
		VALUES
			(@Name, @Description)
		SELECT SCOPE_IDENTITY()
	END
GO

/* Tested by James McPherson 2/16/2018 */
print '' print '*** Creating sp_retrieve_inspectionchecklist_list'
GO
CREATE PROCEDURE [dbo].[sp_retrieve_inspectionchecklist_list]
AS
    BEGIN
        SELECT  [InspectionChecklistID], [Name], [Description], [Active]
        FROM    [InspectionChecklist]
    END
GO

/* Tested by James McPherson 2/16/2018 */
print '' print '*** Creating sp_edit_inspectionchecklist'
GO
CREATE PROCEDURE [dbo].[sp_edit_inspectionchecklist]
    (
    @InspectionChecklistID  [int],
	@NewName			[nvarchar](100),
	@OldName			[nvarchar](100),
    @NewDescription     [nvarchar](1000),
    @OldDescription     [nvarchar](1000),
    @NewActive 			[bit],
	@OldActive			[bit]
    )
AS
    BEGIN
        UPDATE      [InspectionChecklist]
            SET     [Description] = @NewDescription
					, [Active] = @NewActive
					, [Name] = @NewName
            WHERE   [InspectionChecklistID] = @InspectionChecklistID
            AND     [Description] = @OldDescription
			AND		[Active] = @OldActive
        RETURN @@ROWCOUNT   
    END
GO

/* Tested by James McPherson 2/16/2018 */
print '' print '*** Creating sp_edit_maintenancechecklist'
GO
CREATE PROCEDURE [dbo].[sp_edit_maintenancechecklist]
	(
	@MaintenanceChecklistID	[int],
	@NewName			[nvarchar](100),
	@OldName			[nvarchar](100),
	@NewDescription		[nvarchar](1000),
	@OldDescription		[nvarchar](1000),
    @NewActive 			[bit],
    @OldActive 			[bit]
	)
AS
	BEGIN
		UPDATE 		[MaintenanceChecklist]
			SET 	[Description] = @NewDescription
			, [Active] = @NewActive
			, [Name] = @NewName
			WHERE 	[MaintenanceChecklistID] = @MaintenanceChecklistID
			AND		[Name] = @OldName
			AND     [Description] = @OldDescription
			AND		[Active] = @OldActive
		RETURN @@ROWCOUNT		
	END
GO

/* Tested by James McPherson 2/16/2018 */
print '' print '*** Creating sp_delete_maintenancechecklist_by_id'
GO
CREATE PROCEDURE [dbo].[sp_delete_maintenancechecklist_by_id]
	(
	@MaintenanceChecklistID 		[int]
	)
AS
	BEGIN
		DELETE FROM 	[MaintenanceChecklist]
		WHERE 			[MaintenanceChecklistID] = @MaintenanceChecklistID
		RETURN @@ROWCOUNT
	END
GO

/* Tested by James McPherson 2/16/2018 */
print '' print '*** Creating sp_deactivate_maintenancechecklist_by_id'
GO
CREATE PROCEDURE [dbo].[sp_deactivate_maintenancechecklist_by_id]
	(
	@MaintenanceChecklistID 		[int]
	)
AS
	BEGIN
		UPDATE 	[MaintenanceChecklist]
			SET 	[Active] = 0
			WHERE 	[MaintenanceChecklistID] = @MaintenanceChecklistID
		RETURN @@ROWCOUNT
	END
GO
/* End Zach M */

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

print '' print '*** Creating sp_retrieve_employeecertification_list'
GO
CREATE PROCEDURE [dbo].[sp_retrieve_employeecertification_list]
AS
	BEGIN
		SELECT	[CertificationID], [EmployeeID], [EndDate], [Active]
		FROM	[EmployeeCertification]
	END
GO
/* End Mike M */

/* Brady Feller */
print '' print '*** Creating sp_retrieve_equipment_type_by_type'
GO
CREATE PROCEDURE [dbo].[sp_retrieve_equipment_type_by_type]
	(
	@EquipmentTypeID	[nvarchar](100)
	)
AS
	BEGIN
		SELECT	[EquipmentTypeID], [InspectionChecklistID], [PrepChecklistID], [Active]
		FROM	[EquipmentType]
		WHERE	[EquipmentTypeID] = @EquipmentTypeID
	END
GO

print '' print '*** Creating sp_retrieve_equipment_type'
GO
CREATE PROCEDURE [dbo].[sp_retrieve_equipment_type]
AS
	BEGIN
		SELECT	[EquipmentTypeID], [InspectionChecklistID], [PrepChecklistID], [Active]
		FROM	[EquipmentType]
	END
GO

print '' print  '*** Creating procedure sp_create_equipment_type'
GO
CREATE PROCEDURE [dbo].[sp_create_equipment_type]
	(
	@EquipmentTypeID		[nvarchar](100),
	@InspectionChecklistID	[int],
	@PrepChecklistID		[int],
	@Active					[bit]
	)
AS
	BEGIN
		INSERT INTO [EquipmentType]
			([EquipmentTypeID], [InspectionChecklistID], [PrepChecklistID], [Active])
		VALUES
			(@EquipmentTypeID, @InspectionChecklistID, @PrepChecklistID, @Active)
		SELECT SCOPE_IDENTITY()
	END
GO

/* End Brady Feller */

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
		INNER JOIN [TaskType] 
		ON [JobLocationAttributeType].[JobLocationAttributeTypeID] = [TaskType].[JobLocationAttributeTypeID]
		INNER JOIN [Task]
		ON [TaskType].[TaskTypeID] = [Task].[TaskTypeID]
		INNER JOIN [ServiceItem]
		ON [Task].[ServiceItemID] = [ServiceItem].[ServiceItemID]
		INNER JOIN [ServiceOfferingItem]
		ON [ServiceItem].[ServiceItemID] = [ServiceOfferingItem].[ServiceItemID]
		INNER JOIN [ServicePackageOffering]
		ON [ServiceOfferingItem].[ServiceOfferingID] = [ServicePackageOffering].[ServiceOfferingID]
		WHERE [ServicePackageOffering].[ServicePackageID] = @ServicePackageID
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

print '' print '*** Creating JobServicePackageType'
GO
CREATE TYPE [JobServicePackageType] AS TABLE (
    [JobID]             [int],
    [ServicePackageID]       [int]
)
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

print '' print '*** Creating JobLocationAttributeTableType'
GO
CREATE TYPE [JobLocationAttributeTableType] AS TABLE (
    [JobLocationID]             		[int],
    [JobLocationAttributeTypeID]       	[nvarchar](100),
	[Value]								[int]
)
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

/* Jacob Slaubaugh */

print '' print '*** Creating sp_retrieve_equipmentstatus_list'
GO
CREATE PROCEDURE [dbo].[sp_retrieve_equipmentstatus_list]
AS
	BEGIN
		SELECT [EquipmentStatusID]
		FROM [EquipmentStatus]
	END
GO

print '' print '*** Creating sp_retrieve_equipmentstatus_by_id'
GO
CREATE PROCEDURE [dbo].[sp_retrieve_equipmentstatus_by_id]
	(
		@EquipmentStatusID		[nvarchar](100)
	)
AS
	BEGIN
		SELECT 		[EquipmentStatusID]
		FROM		[EquipmentStatus]
		WHERE		[EquipmentStatusID] = @EquipmentStatusID
	END
GO

print '' print '*** Creating sp_delete_equipmentstatus_by_id'
GO
CREATE PROCEDURE [dbo].[sp_delete_equipmentstatus_by_id]
	(
	@EquipmentStatusID		[nvarchar](100)
	)
AS
	BEGIN
		DELETE FROM [EquipmentStatus]
			WHERE [EquipmentStatusID] = @EquipmentStatusID
		RETURN @@ROWCOUNT
	END
GO

print '' print '*** Creating sp_edit_equipmentstatus_by_id'
GO
CREATE PROCEDURE [dbo].[sp_edit_equipmentstatus_by_id]
	(
    @OldEquipmentStatusID      	[nvarchar](100),
    @NewEquipmentStatusID      	[nvarchar](100)
	)
AS
	BEGIN
		UPDATE [EquipmentStatus]
			SET [EquipmentStatusID] = @NewEquipmentStatusID 
			WHERE [EquipmentStatusID] = @OldEquipmentStatusID 
		RETURN @@ROWCOUNT
	END
GO

print '' print '*** Creating sp_delete_maintenancerecord_by_id'
GO
CREATE PROCEDURE [dbo].[sp_delete_maintenancerecord_by_id]
	(
	@MaintenanceRecordID	[int]
	)
AS
	BEGIN
		DELETE FROM [MaintenanceRecord]
			WHERE [MaintenanceRecordID] = @MaintenanceRecordID
		RETURN @@ROWCOUNT
	END
GO
/* End Jacob Slaubaugh */

print '' print '*** Creating sp_retrieve_EmployeeRole_detail_list'
GO
CREATE PROCEDURE [dbo].[sp_retrieve_EmployeeRole_detail_list]
AS
	BEGIN
		SELECT 		[Employee].[EmployeeID],[Employee].[FirstName],[Employee].[LastName],[EmployeeRole].[RoleID]
		FROM		[Employee],[EmployeeRole]
		WHERE [Employee].[EmployeeID] = [EmployeeRole].[EmployeeID]
	END
GO

/* Noah Davison */
print '' print '*** Creating sp_retrieve_equipmenttype_list_by_active'
GO
CREATE PROCEDURE [dbo].[sp_retrieve_equipmenttype_list_by_active]
	(
		@Active					[bit]
	)
AS
	BEGIN
		SELECT		[EquipmentTypeID],[InspectionChecklistID],[PrepChecklistID]
		FROM     	[EquipmentType]
		WHERE		[Active] = @Active
	END
GO

/* sp_create_equipment was missing WarrantyUntil - Noah Davison*/
print '' print '*** Creating sp_create_equipment_fixed'
GO
CREATE PROCEDURE [dbo].[sp_create_equipment_fixed]
	(
	@EquipmentTypeID	[nvarchar](100),
	@Name				[nvarchar](100),
	@MakeModelID		[int],
	@DatePurchased		[date],
	@DateLastRepaired	[date],
	@PriceAtPurchase	[money],
	@CurrentValue		[money],
	@WarrantyUntil		[date],
	@EquipmentStatusID	[nvarchar](100),
	@EquipmentDetails	[nvarchar](1000)
	)
AS
	BEGIN
		INSERT INTO [dbo].[Equipment]
			([EquipmentTypeID], [Name], [MakeModelID], [DatePurchased], [DateLastRepaired], [PriceAtPurchase], [CurrentValue], [WarrantyUntil], [EquipmentStatusID], [EquipmentDetails])
		VALUES
			(@EquipmentTypeID, @Name, @MakeModelID, @DatePurchased, @DateLastRepaired, @PriceAtPurchase, @CurrentValue, @WarrantyUntil, @EquipmentStatusID, @EquipmentDetails  )
		SELECT SCOPE_IDENTITY()
	END
GO