print '' print '*** Using database crlandscaping'
GO
USE [crlandscaping]
GO

print '' print '*** In file JobTaskRelationshipChanges-20190104.sql'
GO

print '' print '*** Adding JobID field to TaskSupply'
GO
ALTER TABLE [TaskSupply] ADD [JobID] INT NOT NULL
GO

print '' print '*** Adding JobID field to TaskEquipment'
GO
ALTER TABLE [TaskEquipment] ADD [JobID] INT NOT NULL
GO

print '' print '*** Adding JobID field to TaskEmployee'
GO
ALTER TABLE [TaskEmployee] ADD [JobID] INT NOT NULL
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

print '' print '*** Altering DateTimeScheduled to be nullable in Job table'
GO
ALTER TABLE [Job] ALTER COLUMN [DateTimeScheduled] [DateTime] NULL
GO

print '' print '*** Adding DateTimeTargetWindow field to Job'
GO
ALTER TABLE [Job] ADD [DateTimeTargetWindow] [DateTime] NULL
GO

print '' print '*** Adding CertifcationID field to EquipmentType'
GO
ALTER TABLE [EquipmentType] ADD [CertificationID] INT NULL
GO	

print '' print '*** Creating EquipmentType CertificationID Foreign Key'
GO
ALTER TABLE [dbo].[EquipmentType] WITH NOCHECK
	ADD CONSTRAINT [fk_EquipmentType_CertificationID] FOREIGN KEY([CertificationID]) 
	REFERENCES [dbo].[Certification]([CertificationID])
	ON UPDATE CASCADE
GO	

print '' print '*** Adding TaskSupplyID identity field to TaskSupply'
GO
ALTER TABLE [TaskSupply] ADD [TaskSupplyID] INT IDENTITY(1000000, 1) NOT NULL
GO

print '' print '*** Altering Primary Key Constraint for TaskSupply table'
GO
ALTER TABLE [TaskSupply]
DROP CONSTRAINT [pk_Task_TaskID_SupplyItem_SupplyItemID]

ALTER TABLE [TaskSupply]
ADD CONSTRAINT [pk_TaskSupply_TaskSupplyID] PRIMARY KEY ([TaskSupplyID]) 
GO

print '' print '*** Adding TaskEmployeeID identity field to TaskEmployee'
GO
ALTER TABLE [TaskEmployee] ADD [TaskEmployeeID] INT IDENTITY(1000000, 1) NOT NULL
GO

print '' print '*** Altering Primary Key Constraint for TaskEmployee table'
GO
ALTER TABLE [TaskEmployee]
DROP CONSTRAINT [pk_Task_TaskID_Employee_EmployeeID]

ALTER TABLE [TaskEmployee]
ADD CONSTRAINT [pk_TaskEmployee_TaskEmployeeID] PRIMARY KEY ([TaskEmployeeID]) 
GO

print '' print '*** Adding TaskEquipmentID identity field to TaskEquipment'
GO
ALTER TABLE [TaskEquipment] ADD [TaskEquipmentID] INT IDENTITY(1000000, 1) NOT NULL
GO

print '' print '*** Altering Primary Key Constraint for TaskEmployee table'
GO
ALTER TABLE [TaskEquipment]
DROP CONSTRAINT [pk_Task_TaskID_Equipment_EqupmentID]

ALTER TABLE [TaskEquipment]
ADD CONSTRAINT [pk_TaskEquipment_TaskEquipmentID] PRIMARY KEY ([TaskEquipmentID]) 
GO

print '' print '*** Altering SupplyID to be nullable in TaskSupply table'
GO
ALTER TABLE [TaskSupply] ALTER COLUMN [SupplyItemID] INT NULL
GO

print '' print '*** Altering EquipmentID to be nullable in TaskEquipment table'
GO
ALTER TABLE [TaskEquipment] ALTER COLUMN [EquipmentID] INT NULL
GO

print '' print '*** Altering EmployeeID to be nullable in TaskEmployee table'
GO
ALTER TABLE [TaskEmployee] ALTER COLUMN [EmployeeID] INT NULL
GO

print '' print '*** Adding TaskTypeSupplyNeedID identity field to TaskTypeSupplyNeed'
GO
ALTER TABLE [TaskTypeSupplyNeed] ADD [TaskTypeSupplyNeedID] INT IDENTITY(1000000, 1) NOT NULL
GO

print '' print '*** Altering Primary Key Constraint for TaskTypeSupplyNeed table'
GO
ALTER TABLE [TaskTypeSupplyNeed]
DROP CONSTRAINT [pk_TaskTypeSupplyNeed_TaskTypeID]

ALTER TABLE [TaskTypeSupplyNeed]
ADD CONSTRAINT [pk_TaskTypeSupplyNeed_TaskTypeSupplyNeedID] PRIMARY KEY ([TaskTypeSupplyNeedID]) 
GO

print '' print '*** Adding TaskTypeEquipmentNeedID identity field to TaskTypeEquipmentNeed'
GO
ALTER TABLE [TaskTypeEquipmentNeed] ADD [TaskTypeEquipmentNeedID] INT IDENTITY(1000000, 1) NOT NULL
GO

print '' print '*** Altering Primary Key Constraint for TaskTypeEquipmentNeed table'
GO
ALTER TABLE [TaskTypeEquipmentNeed]
DROP CONSTRAINT [pk_TaskType_TaskTypeID_EquipmentType_EquipmentTypeID]

ALTER TABLE [TaskTypeEquipmentNeed]
ADD CONSTRAINT [pk_TaskTypeEquipmentNeed_TaskTypeEquipmentNeedID] PRIMARY KEY ([TaskTypeEquipmentNeedID]) 
GO

print '' print '*** Adding TaskTypeEmployeeNeedID identity field to TaskTypeEmployeeNeed'
GO
ALTER TABLE [TaskTypeEmployeeNeed] ADD [TaskTypeEmployeeNeedID] INT IDENTITY(1000000, 1) NOT NULL
GO

print '' print '*** Altering Primary Key Constraint for TaskTypeEquipmentNeed table'
GO
ALTER TABLE [TaskTypeEmployeeNeed]
DROP CONSTRAINT [pk_TaskType_TaskTypeID]

ALTER TABLE [TaskTypeEmployeeNeed]
ADD CONSTRAINT [pk_TaskTypeEmployeeNeed_TaskTypeEmployeeNeedID] PRIMARY KEY ([TaskTypeEmployeeNeedID]) 
GO


print '' print '*** Dropping TaskID foreign key from TaskSupply'
GO
ALTER TABLE [TaskSupply]
DROP CONSTRAINT [fk_Task_TaskID]
GO

print '' print '*** Dropping TaskID foreign key from TaskEquipment'
GO
ALTER TABLE [TaskEquipment]
DROP CONSTRAINT [fk_TaskEquipment_TaskID]
GO

print '' print '*** Dropping TaskID foreign key from TaskEmployee'
GO
ALTER TABLE [TaskEmployee]
DROP CONSTRAINT [fk_TaskEmployee_TaskID]
GO

print '' print '*** Dropping TaskID from TaskSupply table'
GO
ALTER TABLE [TaskSupply] DROP COLUMN [TaskID]
GO

print '' print '*** Dropping TaskID from TaskEquipment table'
GO
ALTER TABLE [TaskEquipment] DROP COLUMN [TaskID]
GO

print '' print '*** Dropping TaskID from TaskEmployee table'
GO
ALTER TABLE [TaskEmployee] DROP COLUMN [TaskID]
GO


print '' print '*** Adding TaskTypeSupplyNeedID identity field to TaskSupply'
GO
ALTER TABLE [TaskSupply] ADD [TaskTypeSupplyNeedID] INT NOT NULL

ALTER TABLE [TaskSupply]
ADD CONSTRAINT [fk_TaskSupply_TaskTypeSupplyNeedID] FOREIGN KEY([TaskTypeSupplyNeedID]) 
	REFERENCES [dbo].[TaskTypeSupplyNeed]([TaskTypeSupplyNeedID])
	ON UPDATE NO ACTION
GO

print '' print '*** Adding TaskTypeEquipmentNeedID identity field to TaskEquipment'
GO
ALTER TABLE [TaskEquipment] ADD [TaskTypeEquipmentNeedID] INT NOT NULL

ALTER TABLE [TaskEquipment]
ADD CONSTRAINT [fk_TaskEquipment_TaskTypeEquipmentNeedID] FOREIGN KEY([TaskTypeEquipmentNeedID]) 
	REFERENCES [dbo].[TaskTypeEquipmentNeed]([TaskTypeEquipmentNeedID])
	ON UPDATE NO ACTION
GO

print '' print '*** Adding TaskTypeEmployeeNeedID identity field to TaskEmployee'
GO
ALTER TABLE [TaskEmployee] ADD [TaskTypeEmployeeNeedID] INT NOT NULL

ALTER TABLE [TaskEmployee]
ADD CONSTRAINT [fk_TaskEmployee_TaskTypeEquipmentNeedID] FOREIGN KEY([TaskTypeEmployeeNeedID]) 
	REFERENCES [dbo].[TaskTypeEmployeeNeed]([TaskTypeEmployeeNeedID])
	ON UPDATE NO ACTION
GO

print '' print '*** Dropping Quantity from TaskEquipment table'
GO
ALTER TABLE [TaskEquipment] DROP COLUMN [Quantity]
GO

print '' print '*** Inserting TaskTypeSupplyNeed Test Records'
GO
INSERT INTO [dbo].[TaskTypeSupplyNeed]
		([TaskTypeID], [SupplyItemID], [Quantity])
	VALUES
		(1000000, 1000001, 15),
		(1000000, 1000002, 1),
		(1000001, 1000003, 1),
		(1000004, 1000004, 1)
GO


print '' print '*** Inserting TaskTypeEquipmentNeed Test Records'
GO
INSERT INTO [dbo].[TaskTypeEquipmentNeed]
		([TaskTypeID], [EquipmentTypeID], [HoursOfWork])
	VALUES
		(1000000, 'lawn mower', 1),
		(1000001, 'wheel barrow', 1),
		(1000004, 'mini excavator', 1)
GO


print '' print '*** Inserting TaskTypeEquipmentNeed Test Records'
GO
INSERT INTO [dbo].[TaskTypeEmployeeNeed]
		([TaskTypeID], [HoursOfWork])
	VALUES
		(1000000, 1),
		(1000001, 1),
		(1000002, 1),
		(1000003, 1),
		(1000004, 1)
GO










