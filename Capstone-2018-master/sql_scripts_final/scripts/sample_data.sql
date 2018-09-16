/* ****************************************************************************************************************
   ************************************************** Sample Data *************************************************
   **************************************************************************************************************** */
print '' print '*** Using database crlandscaping'
GO
USE [crlandscaping]
GO

print '' print '*** Inserting MaintenanceCheckList Sample Data'
GO
	INSERT INTO [dbo].[MaintenanceCheckList]
		([Name], [Description])
	VALUES
		('Tractor 1','Tires - POWER KING D402, Fuel - TVO; 10 gallons , Oil - Hydraulic Machine Oil;12 gallons *MAX Amount*' ),
		('lawnmower 1','Fuel -  87 octane/87 AKI; 1 Gallon, - TracGard N766 TURF Bias Tire 15X6.00-6 B/4 Ply, Air Pressor - 2-3 PSI, Blades - Stens 340 178 Hi-Lift , Oil - 5W-30 Conventional Motor Oil 24oz *MAX AMOUNT*' ),
		('Wheel Barrow 1','Tires - TracGard N766 TURF Bias Tire 15X6.00-6 B/4 Ply, Air Pressor - 2-3 PSI' ),
		('Tractor 2','Tires - POWER KING D402, Fuel - TVO; 10 gallons , Oil - Hydraulic Machine Oil 12 gallons *MAX Amount*')

GO

print '' print '*** Inserting PrepCheckList Sample Data'
GO
	INSERT INTO [dbo].[PrepCheckList]
		([Name], [Description])
	VALUES
		('Tractor 1','Tires, Fuel, Oil' ),
		('lawnmower 1', 'Fuel, Tires, Blades, Belts, Oil' ),
		('Wheel Barrow 1','Hand Grips, Washers and Bolts, Stands, Wheels' ),
		('Tractor 2','Oil Fuel')

GO

print '' print '*** Inserting InspectionCheckList Sample Data'
GO
	INSERT INTO [dbo].[InspectionCheckList]
		([Name], [Description])
	VALUES
		('Tractor 1', 'Tires, Fuel, Oil, Pedals, Grease Fittings, Seals and Belts, Guages, Leaks' ),
		('lawnmower 1', 'Fuel, Tires, Blades, Belts, Deck, Oil' ),
		('Wheel Barrow 1', 'Hand Grips, Washers and Bolts, Frame, Bracking, Bucket, Stands, Wheels' ),
		('Tractor 2', 'Guages, Lights, Horn, Mirrors, Oil, Fuel, Radiator, Battery, Main Frame, Leaks, Hoses, Fan, Belts')

GO

/* Created 05/07/2018 - Mike Mason*/
print '' print '*** Inserting Employee Test Records'
GO
INSERT INTO [dbo].[Employee]
		([FirstName], [LastName], [Address], [PhoneNumber]
			,[Email])
	VALUES
		('Ben', 'Perez', '2743 Mt Vernon Rd SE, Cedar Rapids, IA 52403', '3193640764', 'Example8@email.com'),
		('Wayne', 'Williams', '180 Collins Rd NE, Cedar Rapids, IA 52402', '3193730038', 'waynewilliams@crlandscaping.com'),
		('Ryan', 'Fields', '1055 Linden Dr, Marion, IA 52302', '3194471655', 'ryanfields@crlandscaping.com'),
		('Ivan', 'Sherman', '2360 Edgewood Rd SW, Cedar Rapids, IA 52404', '3196548446', 'ivansherman@crlandscaping.com'),
		('Owen', 'Moran', '1040 E Post Rd, Marion, IA 52302', '3193776325', 'owenmoran@crlandscaping.com'),
		('Derek', 'Moran', '4045 Deer Valley Dr, Marion, IA 52302', '3195339032', 'derekmoran@crlandscaping.com'),
		('Cary', 'Peterson', '224 16th Ave SW, Cedar Rapids, IA 52404', '3193644328', 'carypeterson@crlandscaping.com'),
		('Gerardo', 'Wallace', '4540 J St SW, Cedar Rapids, IA 52404', '3193626435', 'gerardowallace@crlandscaping.com'),
		('Joel', 'Caldwell', '1025 Hawkeye Dr, Hiawatha, IA 52233', '3193904900', 'joelcaldwell@crlandscaping.com'),
		('Rex', 'Graves', '4806 Mary Green Ct, Cedar Rapids, IA 52411', '3194472123', 'rexgraves@crlandscaping.com'),
		('Marty', 'Howard', '1155 3rd Ave, Marion, IA 52302', '3193772809', 'martyhoward@crlandscaping.com'),
		('Mindy', 'Martin', '1315 Parkview Dr, Marion, IA 52302', '3193772647', 'mindymartin@crlandscaping.com'),
		('Andres', 'Park', '616 16th St NE, Cedar Rapids, IA 52402', '3193694863', 'andrespark@crlandscaping.com'),
		('Willie', 'Jacobs', '5960 4th St SW A, Cedar Rapids, IA 52404', '3193652421', 'williejacobs@crlandscaping.com'),
		('Sophie', 'Riley', '2465 Valleyview Ct, Marion, IA 52302', '3192416551', 'sophieriley@crlandscaping.com'),
		('Ella', 'Wilkins', '919 14th Ave SW, Cedar Rapids, IA 52404', '3193630283', 'ellawilkins@crlandscaping.com'),
		('Tracy', 'Morris', '3925 Heather Ct SW, Cedar Rapids, IA 52404', '3199817333', 'tracymorris@crlandscaping.com'),
		('Franklin', 'Sanchez', '852 44th St SE, Cedar Rapids, IA 52403', '3193652243', 'franklinsanchez@crlandscaping.com'),
		('Albert', 'Fitzgerald', '3111 1st Ave SE, Cedar Rapids, IA 52402', '3192139504', 'albertfitzgerald@crlandscaping.com'),
		('Marshall', 'Stewart', '1100 H Ave NE, Cedar Rapids, IA 52402', '3193656977', 'marshallstewart@crlandscaping.com'),
		('Nathan', 'Christensen', '185 16th Ave SW, Cedar Rapids, IA 52404', '3193664304', 'nathanchristensen@crlandscaping.com'),
		('Merle', 'Ward', '1666 7th Ave, Marion, IA 52302', '3193772582', 'merleward@crlandscaping.com'),
		('Malcolm', 'Elliot', '4035 Mt Vernon Rd SE, Cedar Rapids, IA 52403', '3193654623', 'malcolmelliot@crlandscaping.com'),
		('Dave', 'Duncan', '1556 1st Ave NE, Cedar Rapids, IA 52402', '3193644196', 'daveduncan@crlandscaping.com'),
		('Taylor', 'Garrett', '20 Wilson Ave SW, Cedar Rapids, IA 52404', '3193662700', 'taylorgarrett@crlandscaping.com'),
		('James', 'Stewart', '3235 Oakland Rd NE, Cedar Rapids, IA 52402', '3193667756', 'jamesstewart@crlandscaping.com'),
		('Denise', 'Figueroa', ' 1843 Johnson Ave NW, Cedar Rapids, IA 52405', '3193650477', 'boobietaylor@crlandscaping.com'),
		('Bobbie', 'Terry', '5050 Edgewood Rd NE, Cedar Rapids, IA 52411', '3193780762', 'bobbieterry@crlandscaping.com'),
		('Ricardo', 'Taylor', '2790 7th Ave, Marion, IA 52302', '3193774821', 'ricardotaylor@crlandscaping.com'),
		('Kristy', 'Todd', '1520 6th St SW, Cedar Rapids, IA 52404', '3193639606', 'kristytodd@crlandscaping.com'),
		('Jennie', 'Carson', '505 Boyson Rd NE, Cedar Rapids, IA 52402', '3192949008', 'jenniecarson@crlandscaping.com'),
		('Meghan', 'Kim', '2001 Blairs Ferry Rd NE, Cedar Rapids, IA 52402', '3193930539', 'meghankim@crlandscaping.com')
GO

print '' print '*** Inserting Role Test Records'
GO
INSERT INTO [dbo].[Role]
		([RoleID], [Description])
	VALUES
		('Admin', 'Administrator'),
		('Mechanic', 'Equipment Mechanic'),
		('Maintenance', 'Equipment Maintenance'),
		('Prep', 'Equipment Prep'),
		('Inspector', 'Equipment Inspector'),
		('Supply Clerk', 'Clerk in charge of supplies'),
		('Applicant', 'Employment applicant'),
		('Customer', 'Current or past customer'),
		('Labor Scheduler', 'Person in charge of scheduling labor'),
		('Equipment Scheduler', 'Person in charge of scheduling equipment'),
		('Job Scheduler', 'Person in charge of scheduling jobs'),
		('Delivery', 'Person who delivers equipment and supplies to jobsite'),
		('Manager', 'Business manager'),
		('Worker', 'Full-time laborer'),
		('Foreman', 'Jobsite manager'),
		('Temp', 'Temporary employee'),
		('HR Manager', "Manager of human resources")
GO

print '' print '*** Inserting Certification Test Records'
GO
INSERT INTO [dbo].[Certification]
		([Name], [Description])
	VALUES
		('Certified Lawn Care Manager','Accredited by National Association of Lanscaping Professionals'),
		('Certified Lawn Care Technician','Accredited by National Association of Lanscaping Professionals'),
		('Certified Grounds Manager','Accredited by Professional Grounds Management Society'),
		('Certified Grounds Technician','Accredited by Professional Grounds Management Society'),
		('Certified Landscape Irrigation Manager','Accredited by Irrigation Association'),
		('Certified Irrigation Technician','Accredited by Irrigation Association'),
		('Certified Arborist','Accredited by International Society of Aborists'),
		('Certified Tree Climber Specialist','Accredited by International Society of Aborists'),
		('Certified Landscape Designer','Accredited by Association of Professional Landscape Designers'),
		('Ag and Turf Equipment Repair Technician','Certified to repair landscaping equipment'),
		('Heavy Equipment and Tractor Repair Technician','Certified to repair heavy equipment and tractors'),
		('Professional Human Resources Certification','Certified in human resources')
GO

print '' print '*** Inserting EmployeeCertification Records'
GO
INSERT INTO [dbo].[EmployeeCertification]
		([CertificationID], [EmployeeID], [EndDate])
	VALUES
		(1000011, '1000012', '2020-12-18'),
		(1000001, '1000023', '2019-02-08'),
		(1000001, '1000024', '2019-05-26'),
		(1000005, '1000031', '2020-01-13'),
		(1000009, '1000030', '2020-01-02'),
		(1000009, '1000026', '2019-08-29'),
		(1000003, '1000025', '2018-12-06'),
		(1000010, '1000026', '2019-08-14'),
		(1000011, '1000026', '2019-07-29'),
		(1000000, '1000011', '2019-01-21'),
		(1000002, '1000011', '2021-11-14'),
		(1000009, '1000004', '2018-08-20'),
		(1000004, '1000020', '2019-03-31'),
		(1000006, '1000020', '2022-12-31'),
		(1000008, '1000021', '2019-11-02'),
		(1000001, '1000021', '2019-06-30'),
		(1000009, '1000008', '2019-07-19'),
		(1000010, '1000014', '2018-11-13'),
		(1000011, '1000007', '2019-05-10')
	
GO

print '' print '*** Inserting into SupplyStatus Test Records'
GO
INSERT INTO [dbo].[SupplyStatus]
		([SupplyStatusID])
	VALUES
		('Available'),
		('Delivered'),
		('Out'),
		('Needs Inspection'),
		('Ordered'),
		('Received'),
		('Cancelled')
GO

print '' print '*** Inserting TimeOff Records'
GO
INSERT INTO [dbo].[TimeOff]
		([EmployeeID], [StartTime], [EndTime])
	VALUES
		(1000000, '2018-01-10 08:00', '2018-01-10 05:00'),
		(1000001, '2018-01-11 08:00', '2018-01-11 05:00'),
		(1000001, '2018-01-12 08:00', '2018-01-12 05:00'),
		(1000002, '2018-01-13 08:00', '2018-01-13 05:00'),
		(1000003, '2018-01-14 08:00', '2018-01-14 05:00'),
		(1000004, '2018-01-15 08:00', '2018-01-15 05:00')
GO

print '' print '*** Inserting CustomerType Records'
GO
INSERT INTO [dbo].[CustomerType]
		([CustomerTypeID])
	VALUES
		('Residential'),
		('Commercial')
GO


/* Created 05/07/2018 - Mike Mason*/
print '' print '*** Inserting Customer Records'
GO
INSERT INTO [dbo].[Customer]
		([CustomerTypeID], [Email], [FirstName], [LastName], [PhoneNumber])
	VALUES
		('Residential', 'jenniferf@gmail.com', 'Jennifer', 'Forcina', '3193901536'),
		('Commercial', 'ekanter@icloud.com', 'Eric', 'Kanter', '3196545281'),
		('Residential', 'charjong@gmail.com', 'Charline', 'Jong', '3196548832'),
		('Commercial', 'jarridBranco@fios.com', 'Jarrid', 'Branco', '3193907010'),
		('Commercial', 'ericka183@gmail.com', 'Ericka', 'Taube', '3193965262')
GO

print '' print '*** Inserting Availablity Records'
GO
INSERT INTO [dbo].[Availability]
		([EmployeeID], [StartTime], [EndTime])
	VALUES
		('1000001', '2018-01-18 8:00:00 AM', '2018-01-18 3:30:00 PM'),
		('1000002', '2018-02-08 8:00:00 AM', '2018-02-10 2:00:00 PM'),
		('1000003', '2018-02-12 8:00:00 AM', '2018-02-12 1:30:00 PM'),
		('1000004', '2018-02-13 8:00:00 AM', '2018-02-13 4:00:00 PM'),
		('1000005', '2018-02-14 8:00:00 AM', '2018-02-14 3:30:00 PM'),
		('1000006', '2018-02-15 8:00:00 AM', '2018-02-15 4:00:00 PM'),
		('1000007', '2018-02-16 8:00:00 AM', '2018-02-17 4:30:00 PM'),
		('1000008', '2018-02-18 8:00:00 AM', '2018-02-21 3:00:00 PM'),
		('1000009', '2018-02-22 8:00:00 AM', '2018-02-25 2:30:00 PM')
	
GO


print '' print '*** Inserting EquipmentStatus Sample Data'
GO
INSERT INTO [dbo].[EquipmentStatus]
		([EquipmentStatusID])
	VALUES
		('Available'),
		('At Inspection'),
		('At Maintenance'),
		('At Prep'),
		('Not in Service')
GO

print '' print '*** Inserting into EquipmentType Test Records'
GO
INSERT INTO [dbo].[EquipmentType]
		([EquipmentTypeID], [PrepChecklistID])
	VALUES
		('lawnmower', '1000002'),
		('aerator', null),
		('sod layer', null),
		('fertilizer spreader', null),
		('pesticide sprayer', null),
		('seeder', null),
		('rake/dethatcher', null),
		('digging', null),
		('blower', null),
		('bagger', null),
		('tractor', '1000002'),
		('wheel barrow', '1000001')
GO

print '' print '*** Inserting into MakeModel Test Records'
GO
INSERT INTO [dbo].[MakeModel]
		([Make], [Model], [MaintenanceCheckListID])
	VALUES
		('Craftsman', 'TurnTight Riding Mower', 1000000),
		('John Deer', '90', 1000000),
		('Bobcat', 'Hydraulic Mini Excavator', 1000000),
		('Cat', '31', 1000000),
		('Craftsman', '21 Inch Push Mower', 1000000),
		('Weed Eater', '12 Inch', 1000000)
GO


/* Created 05/07/2018 - Mike Mason*/
print '' print '*** Inserting JobLocation Test Records'
GO
INSERT INTO [dbo].[JobLocation]
		([CustomerID], [Street], [City], [State], [ZipCode], [Comments])
	VALUES
		(1000000, '1621 Pinehurst Dr NE', 'Cedar Rapids', 'IA', '52402', 'Make sure gate is closed'),
		(1000000, '733 Arrowhead Ln NE', 'Cedar Rapids', 'IA', '52402', 'NONE'),
		(1000001, '1200 Continental Pl NE', 'Cedar Rapids', 'IA', '52402', 'NONE'),
		(1000002, '4230 Fox Meadow Dr SE', 'Cedar Rapids', 'IA', '52403', 'NONE'),
		(1000003, '153 Collins Rd NE', 'Cedar Rapids', 'IA', '52402', 'Blow grass off sidewalks'),
		(1000004, '1843 Johnson Ave NW', 'Cedar Rapids', 'IA', '52405', 'NONE')
GO

print '' print '*** Inserting EmployeeRole Records'
GO
INSERT INTO [dbo].[EmployeeRole]
		([EmployeeID], [RoleID])
	VALUES
		(1000000, "Admin"),
		(1000000, "Manager"),
		(1000001, "Labor Scheduler"),
		(1000002, "Admin"),
		(1000003, "Equipment Scheduler"),
		(1000004, "Mechanic"),
		(1000004, "Maintenance"),
		(1000005, "Supply Clerk"),
		(1000006, "Prep"),
		(1000007, "Job Scheduler"),
		(1000008, "Inspector"),
		(1000009, "Manager"),
		(1000010, "Delivery"),
		(1000011, "Foreman"),
		(1000012, "HR Manager"),
		(1000013, "Inspector"),
		(1000014, "Mechanic"),
		(1000015, "Job Scheduler"),
		(1000016, "Prep"),
		(1000017, "Delivery"),
		(1000018, "Labor Scheduler"),
		(1000019, "Manager"),
		(1000019, "HR Manager"),
		(1000020, "Foreman"),
		(1000021, "Foreman"),
		(1000022, "Equipment Scheduler"),
		(1000023, "Worker"),
		(1000024, "Worker"),
		(1000025, "Worker"),
		(1000026, "Worker"),
		(1000027, "Worker"),
		(1000028, "Worker"),
		(1000029, "Temp"),
		(1000030, "Temp"),
		(1000031, "Temp")
GO

print '' print '*** Inserting PaymentType Records'
GO
INSERT INTO [dbo].[PaymentType]
		([PaymentTypeID], [Description])
	VALUES
		("Credit Card", "Pay by Visa/Mastercard/American Express"),
		("Checking Account", "Pay by ACH payment")
GO



print '' print '*** Inserting Equipment Records'
INSERT INTO [dbo].[Equipment]
	([EquipmentTypeID], [Name], [MakeModelID], [DatePurchased], 
	[DateLastRepaired], [PriceAtPurchase],[CurrentValue],[WarrantyUntil],
	[EquipmentStatusID],[EquipmentDetails])
	VALUES
			('lawnmower', 'Beast 62ZBBM18 62 in. Zero-Turn Commercial Mower', '1000002', '2016-10-12', '2018-01-13'
			, '12000', '9000.00', '2019-01-01', 'Available', 'This is our favorite mower.'),
			('lawnmower', 'Beast 62ZBBM19 62 in. Zero-Turn Commercial Mower', '1000002', '2016-10-12', '2018-01-13'
			, '12000', '9000.00', '2019-01-01', 'Available', 'This is our favorite mower.'),
			('lawnmower', 'Beast 62ZBBM20 62 in. Zero-Turn Commercial Mower', '1000002', '2016-10-12', '2018-01-13'
			, '12000', '9000.00', '2019-01-01', 'Available', 'This is our favorite mower.'),
			('sod layer', 'Skid Steer Sod Unroller 1', '1000003', '2017-08-15', '2018-01-24'
			, '75.00', '50.54', '2018-02-15', 'Available', 'A sod layer'),
			('aerator', 'Yard Tuff 14" x 36" Steel Drum Spike Aerator', '1000002', '2017-06-05', '2017-10-31'
			, '25000.00', '22000.00', '2019-12-31', 'Available', 'Aerator'),
			('pesticide sprayer', 'Solutions Electric Skid Sprayer- 50 Gallon, Pumptec, 300ft 3/8inch Hose', '1000002', '2017-06-05', '2017-10-31'
			, '25000.00', '22000.00', '2019-12-31', 'Available', 'Aerator'),
			('seeder', 'Agri-Fab 45-0301 175 Lb. Spiker / Seeder / Spreader', '1000002', '2017-06-05', '2017-10-31'
			, '25000.00', '22000.00', '2019-12-31', 'Available', 'Aerator'),
			('rake/dethatcher', 'Agri-Fab 45-0301 175 Lb. Spiker / Seeder / Spreader', '1000002', '2017-06-05', '2017-10-31'
			, '22000.00', '22000.00', '2019-12-31', 'Available', 'Aerator'),
			('bagger', 'Murray / MTDBrands 38 inch Twin Bagger OEM-190-180A, Multicolor', '1000002', '2017-06-05', '2017-10-31'
			, '22000.00', '22000.00', '2019-12-31', 'Available', 'Aerator'),
			('wheel barrow', 'Wheel Barrow 1', 1000003, '2017-03-21', '2018-02-24'
			, '75.00', '50.54', '2018-09-15', 'Available', 'A wheel barrow. Used to move very heavy things.')
GO

print '' print '*** Inserting InspectionRecord Test Records'
INSERT INTO [dbo].[InspectionRecord]
           ([EquipmentID], [EmployeeID], [Description], [Date])
     VALUES
           (1000000, 1000003, 'Inspection Passed', CONVERT(date, '2017-08-25')),
           (1000001, 1000003, 'Inspection Passed', CONVERT(date, '2017-08-25')),
           (1000002, 1000003, 'Inspection Failed', CONVERT(date, '2017-08-25')),
           (1000003, 1000003, 'Inspection Passed', CONVERT(date, '2017-08-26')),
		   (1000004, 1000003, 'Inspection Passed', CONVERT(date, '2017-08-26'))
GO

print '' print '*** Inserting PrepRecord Records'
INSERT INTO [dbo].[PrepRecord]
	([EquipmentID], [EmployeeID], [Description], [Date])
	VALUES
			(1000000, '1000001', 'Went well', '2016-05-10'),
			(1000001, '1000002', 'Could have gone better', '2017-08-15'),
			(1000002, '1000000', '7.5/10', '2018-01-05'),
			(1000003, '1000003', 'Exceptional preparation.', '2017-03-12'),
			(1000004, '1000003', 'Exceptional preparation.', '2017-03-12')
GO


print '' print '*** Inserting SpecialOrderItem Data'
GO
INSERT INTO [dbo].[SpecialOrderItem]
		([Name])
	VALUES
		('Star-Head Screwdriver'),
		('20in Bolt'),
		('Roomba Blade'),
		('Large Toolbox')
GO

print '' print '*** Inserting into SupplyItem table'
GO
INSERT INTO [dbo].[SupplyItem]
		([Name], [Description], [Location], [QuantityInStock], [ReorderLevel], [ReorderQuantity])
	VALUES
		('Premium Gasoline', 'A plastic 5 gallon oil container.', 'Row 5, Column 2', 250, 260, 100),
		('Sod', 'Farmed grass', 'Supply Yard', 200, 500, 1000),
		('Fertilizer', 'Premium all purpose fertilizer', 'Supply Yard', 200, 150, 100),
		('Pesticide', 'Premium all purpose pesticide', 'Supply Yard', 50, 40, 30),
		('Grass Seed', 'Premium all grass seed', 'Supply Yard', 50, 40, 30),
		('Sprinkler Repair Kit', 'All you need for sprinkler repair', 'Supply Yard', 30, 20, 30)
GO

print '' print '*** Inserting into MaintenanceRecord Test Records'
GO
INSERT INTO [dbo].[MaintenanceRecord]
		([EquipmentID], [EmployeeID], [description], [Date])
	VALUES
		(1000000, '1000001', 'Maintenance succeded', Convert (date,'2018-03-27')),
		(1000001, '1000003', 'Need More Parts', Convert (date,'2018-04-07')),
		(1000002, '1000002', 'Need More Parts', Convert (date,'2018-03-20')),
		(1000003, '1000001', 'Need More Parts', Convert (date,'2018-02-25'))
GO

print '' print '*** Inserting into MaintenanceLine Test Records'
GO
INSERT INTO [dbo].[MaintenanceLine]
		([MaintenanceRecordID], [SupplyItemID], [QuantityOfSupplies])
	VALUES
		(1000000, '1000001', 5),
		(1000001, '1000003', 10),
		(1000002, '1000002', 15),
		(1000003, '1000001', 20)
GO

print '' print '*** Inserting ServicePackage Test Records'
GO
INSERT INTO [dbo].[ServicePackage]
		([Name], [Description])
	VALUES
		('Gold', 'The Gold Package'),
		('Silver', 'The Silver Package'),
		('Platinum', 'The Platinum Package')
GO

print '' print '*** Inserting ServiceOffering Test Records'
GO
INSERT INTO [dbo].[ServiceOffering]
		([Name], [Description])
	VALUES
		('Premium Pest Prevention', 'NONE'),
		('Basic Pest Prevention', 'NONE'),
		('Premium Lawn Maintenance', 'NONE'),
		('Moderate Lawn Maintenance', 'NONE'),
		('Basic Lawn Maintenance', 'NONE')
GO

print '' print '*** Inserting ServicePackageOffering Test Records'
GO
INSERT INTO [dbo].[ServicePackageOffering]
		([ServicePackageID], [ServiceOfferingID])
	VALUES
		(1000000, 1000000),
		(1000000, 1000002),
		(1000001, 1000001),
		(1000001, 1000003),
		(1000002, 1000004)
GO

print '' print '*** Inserting ServiceItem Records'
GO
INSERT INTO [dbo].[ServiceItem]
		([Name], [Description])
	VALUES
		("Lawn Fertilization", "Fertilizating lawn"),
		("Sod Installation", "Install sod"),
		("Deep Core Aeration", "Aration"),
		("Irrigation/Sprinkler Diagnostic & Repair", "Sprinkler diagnostic"),
		("Over-Seeding", "Over Seed"),
		("Lawn Mowing", "Mow the lawn"),
		("Edging and Blowing", "Edge and Blow"),
		("Dethatching & Power Raking", "Dethatching & Power Raking"),
		("Bagging of Clippings", "Bagging of Clippings"),
		("Weed Control", "Weed Control"),
		("Disease and Fungus Control", "Disease and Fungus Control"),
		("Insect Control", "Insect Control")
GO

print '' print '*** Inserting ServiceOfferingItem Test Records'
GO
INSERT INTO [dbo].[ServiceOfferingItem]
		([ServiceOfferingID], [ServiceItemID])
	VALUES
		(1000000, 1000009),
		(1000000, 1000010),
		(1000000, 1000011),
		(1000001, 1000009),
		(1000001, 1000010),
		(1000001, 1000011),
		(1000002, 1000000),
		(1000002, 1000001),
		(1000002, 1000002),
		(1000002, 1000003),
		(1000002, 1000004),
		(1000002, 1000005),
		(1000002, 1000006),
		(1000002, 1000007),
		(1000002, 1000008),
		(1000003, 1000005),
		(1000003, 1000006),
		(1000003, 1000007),
		(1000003, 1000008),
		(1000004, 1000005),
		(1000004, 1000006),
		(1000004, 1000007)
		
GO


/* Created 03/04/2018 - John Miller*/
print '' print '*** Inserting JobLocationAttributeTypeID Test Records'
GO
INSERT INTO [dbo].[JobLocationAttributeType]
		([JobLocationAttributeTypeID])
	VALUES
		('Acres of Sprinkler'),
		('Sq. Ft. to Sod'),
		('Acres of Lawn')
GO


/* Created 03/04/2018 - John Miller*/
print '' print '*** Inserting TaskType Test Records'
GO
INSERT INTO [dbo].[TaskType]
		([Name], [Quantity], [JobLocationAttributeTypeID])
	VALUES
		('Mow', 1, 'Acres of Lawn'),
		('Install Sod', 5, 'Sq. Ft. to Sod'),
		('Weed Control', 1, 'Acres of Lawn'),
		('Seeding', 1, 'Acres of Lawn'),
		('Sprinkler Repair', 1, 'Acres of Sprinkler'),
		('Edging and Blowing', 1, 'Acres of Lawn'),
		('Aeration', 1, 'Acres of Lawn'),
		('Fungus Control', 1, 'Acres of Lawn'),
		('Insect Control', 1, 'Acres of Lawn'),
		('Bag Clippings', 1, 'Acres of Lawn'),
		('Fertilization', 1, 'Acres of Lawn'),
		('Dethatch & Power Rake', 1, 'Acres of Lawn')
GO

print '' print '*** Inserting Task Test Records'
GO
INSERT INTO [dbo].[Task]
		([TaskTypeID], [ServiceItemID], [Name], [Description])
	VALUES
		(1000000, 1000005, 'Mow Lawn', 'NONE'),
		(1000001, 1000001, 'Install Sod', 'NONE'),
		(1000002, 1000009, 'Weed Control', 'NONE'),
		(1000003, 1000004, 'Seeding', 'NONE'),
		(1000004, 1000003, 'Sprinkler Repair', 'NONE'),
		(1000005, 1000006, 'Edging and Blowing', 'NONE'),
		(1000006, 1000002, 'Aeration', 'NONE'),
		(1000007, 1000010, 'Fungus Control', 'NONE'),
		(1000008, 1000011, 'Insect Control', 'NONE'),
		(1000009, 1000008, 'Bag Clippings', 'NONE'),
		(1000010, 1000000, 'Fertilization', 'NONE'),
		(1000011, 1000007, 'Dethatch & Power Rake', 'NONE')
GO


/* Created 03/07/2018 - Zachary Hall*/
print '' print '*** Inserting JobLocationAttribute Test Records'
GO
INSERT INTO [dbo].[JobLocationAttribute]
		([JobLocationID], [JobLocationAttributeTypeID], [Value])
	VALUES
		(1000000, 'Acres of Lawn', 1),
		(1000000, 'Sq. Ft. to Sod', 50)
GO




print '' print '*** Inserting SpecialOrder Test Records'
GO
INSERT INTO [dbo].[SpecialOrder]
		([EmployeeID], [SupplyStatusID], [Date])
	VALUES
		
		(1000000, 'Available', '12/12/2017'),
		(1000001, 'Delivered', '11/24/2017'),
		(1000002, 'Out', '11/01/2017')
GO

print '' print '*** Inserting Vendor Test Records'
INSERT INTO [dbo].[Vendor]
           ([Name], [Rep], [Address], [Website], [Phone], [Active])
     VALUES
           ('Supply Store', 'John', '123 Fake Street', 'www.supplies.com', 3193555455, 1),
           ('VendorZilla', 'Jane', '543 E Avenue', 'www.VendorZilla.com', 5631234567, 1),
		   ('Supplies R Us', 'Zach', '514 Supply Blvd', 'www.suppliesrus.com', 5157654987, 1),
		   ('My Supply Hut', 'Amanda', '777 Hut Street', 'www.SuplyHut.com', 5551236543, 1),
		   ('Big Time Supply', 'Jayden', '9021 Big Hts', 'www.BigTimeSupply.com', 3193916279, 1)
GO

print '' print '*** Inserting into ResupplyOrder table'
GO
INSERT INTO [dbo].[ResupplyOrder]
		([EmployeeID], [Date], [SupplyStatusID],[VendorID])
	VALUES
		(1000000, '6-16-2018',	'Available', 1000000),
        (1000000, '7-22-2018',	'Delivered', 1000001),
        (1000001, '3-19-2018',	'Out', 1000002),
        (1000002, '6-11-2018',	'Needs Inspection', 1000002),
        (1000000, '9-09-2019',	'Out', 1000001)
GO


print '' print '*** Inserting Source Data'
GO
SET QUOTED_IDENTIFIER ON
INSERT INTO [dbo].[Source]
		([SupplyItemID],[SpecialOrderItemID],[VendorID],[LeadTime],[PriceEach])
	VALUES
		(1000001,1000001,1000000,5,19.95),
		(1000000,1000000,1000001,4,24.99),
		(1000002,1000000,1000004,2,5.95),
		(1000000,1000002,1000003,1,24.99),
		(1000002,1000002,1000000,1,7.99),
		(1000001,1000003,1000002,1,22.99),
		(1000003,1000003,1000000,1,49.99),
		(1000000,1000000,1000000,3,24.99),
		(1000002,1000002,1000002,2,5.95),
		(1000000,1000000,1000002,1,24.99)
GO

print '' print '*** Inserting SpecialOrderLine Data'
GO
INSERT INTO [dbo].[SpecialOrderLine]
	([SpecialOrderID], [SpecialOrderItemID],[Quantity])
	VALUES
		(1000000, 1000000,1),
		(1000000, 1000001,1),
		(1000001, 1000002,2)
GO

print '' print '*** Inserting into ResupplyOrderLine table'
GO
INSERT INTO [dbo].[ResupplyOrderLine]
		([ResupplyOrderID], [SupplyItemID], [Quantity], [Price])
	VALUES
		(1000000, 1000000, 25, 2500.29),
		(1000000, 1000001, 75, 735.14),
		(1000001, 1000000, 40, 485.54),
		(1000001, 1000001, 32, 2500.29),
		(1000000, 1000002, 95, 150.49)
GO



print '' print '*** Inserting PersonalEquipment sample data'
GO
INSERT INTO [dbo].[PersonalEquipment]
		([PersonalEquipmentType], [Name], [Description], [PersonalEquipmentStatus])
	VALUES
		('Tools','Toolbox','Box for holding tools', 'Ready'),
		('Tools','Toolbox','Box for holding tools', 'Damaged'),
		('Tools','Toolbox','Box for holding tools', 'Being Serviced'),
		('Tools','Tape Measure','Tape used for measuring', 'Ready'),
		('Tools','Tape Measure','Tape used for measuring', 'Damaged'),
		('Tools','Hammer','Used for hammering', 'Ready')
GO


print '' print '*** Inserting TaskTypeSupplyNeed sample data'
GO
INSERT INTO [dbo].[TaskTypeSupplyNeed]
		([TaskTypeID], [SupplyItemID], [Quantity])
	VALUES
		(1000000, 1000000, 2),
		(1000001, 1000000, 2),
		(1000001, 1000001, 1),
		(1000002, 1000000, 2),
		(1000002, 1000003, 20),
		(1000003, 1000000, 2),
		(1000003, 1000004, 10),
		(1000004, 1000005, 1),
		(1000005, 1000000, 2),
		(1000006, 1000000, 2),
		(1000007, 1000003, 20),
		(1000008, 1000003, 20),
		(1000009, 1000000, 2),
		(1000010, 1000002, 10),
		(1000011, 1000000, 2)
GO

print '' print '*** Inserting TaskTypeEmployeeNeed sample data'
GO
INSERT INTO [dbo].[TaskTypeEmployeeNeed]
		([TaskTypeID], [HoursOfWork])
	VALUES
		(1000000, 1),
		(1000001, 1),
		(1000002, 1),
		(1000003, 1),
		(1000004, 1),
		(1000005, 1),
		(1000006, 1),
		(1000007, 1),
		(1000008, 1),
		(1000009, 1),
		(1000010, 1),
		(1000011, 1)
GO

print '' print '*** Inserting TaskTypeEquipmentNeed sample data'
GO
INSERT INTO [dbo].[TaskTypeEquipmentNeed]
		([TaskTypeID], [EquipmentTypeID], [HoursOfWork])
	VALUES
		(1000000, 'lawnmower', 1),
		(1000001, 'sod layer', 1),
		(1000002, 'pesticide sprayer', 1),
		(1000003, 'seeder', 1),
		(1000004, 'digging', 1),
		(1000005, 'blower', 1),
		(1000006, 'aerator', 1),
		(1000007, 'pesticide sprayer', 1),
		(1000008, 'pesticide sprayer', 1),
		(1000009, 'bagger', 1),
		(1000010, 'fertilizer spreader', 1),
		(1000011, 'rake/dethatcher', 1)
GO


INSERT INTO [dbo].[Availability] 
([EmployeeID], [StartTime], [EndTime]) 
VALUES 
(1000020, N'2018-05-06 05:00:00', N'2018-05-06 17:00:00'),
(1000020, N'2018-05-07 05:00:00', N'2018-05-07 17:00:00'),
(1000020, N'2018-05-08 05:00:00', N'2018-05-08 17:00:00'),
(1000020, N'2018-05-09 05:00:00', N'2018-05-09 17:00:00'),
(1000020, N'2018-05-10 05:00:00', N'2018-05-10 17:00:00'),
(1000020, N'2018-05-11 05:00:00', N'2018-05-11 17:00:00'),
(1000020, N'2018-05-12 05:00:00', N'2018-05-12 17:00:00'),
(1000021, N'2018-05-06 05:00:00', N'2018-05-06 17:00:00'),
(1000021, N'2018-05-07 05:00:00', N'2018-05-07 17:00:00'),
(1000021, N'2018-05-08 05:00:00', N'2018-05-08 17:00:00'),
(1000021, N'2018-05-09 05:00:00', N'2018-05-09 17:00:00'),
(1000021, N'2018-05-10 05:00:00', N'2018-05-10 17:00:00'),
(1000021, N'2018-05-11 05:00:00', N'2018-05-11 17:00:00'),
(1000021, N'2018-05-12 05:00:00', N'2018-05-12 17:00:00'),
(1000022, N'2018-05-06 05:00:00', N'2018-05-06 17:00:00'),
(1000022, N'2018-05-07 05:00:00', N'2018-05-07 17:00:00'),
(1000022, N'2018-05-08 05:00:00', N'2018-05-08 17:00:00'),
(1000022, N'2018-05-09 05:00:00', N'2018-05-09 17:00:00'),
(1000022, N'2018-05-10 05:00:00', N'2018-05-10 17:00:00'),
(1000022, N'2018-05-11 05:00:00', N'2018-05-11 17:00:00'),
(1000022, N'2018-05-12 05:00:00', N'2018-05-12 17:00:00'),
(1000023, N'2018-05-06 05:00:00', N'2018-05-06 17:00:00'),
(1000023, N'2018-05-07 05:00:00', N'2018-05-07 17:00:00'),
(1000023, N'2018-05-08 05:00:00', N'2018-05-08 17:00:00'),
(1000023, N'2018-05-09 05:00:00', N'2018-05-09 17:00:00'),
(1000023, N'2018-05-10 05:00:00', N'2018-05-10 17:00:00'),
(1000023, N'2018-05-11 05:00:00', N'2018-05-11 17:00:00'),
(1000023, N'2018-05-12 05:00:00', N'2018-05-12 17:00:00'),
(1000024, N'2018-05-06 05:00:00', N'2018-05-06 17:00:00'),
(1000024, N'2018-05-07 05:00:00', N'2018-05-07 17:00:00'),
(1000024, N'2018-05-08 05:00:00', N'2018-05-08 17:00:00'),
(1000024, N'2018-05-09 05:00:00', N'2018-05-09 17:00:00'),
(1000024, N'2018-05-10 05:00:00', N'2018-05-10 17:00:00'),
(1000024, N'2018-05-11 05:00:00', N'2018-05-11 17:00:00'),
(1000024, N'2018-05-12 05:00:00', N'2018-05-12 17:00:00'),
(1000025, N'2018-05-06 05:00:00', N'2018-05-06 17:00:00'),
(1000025, N'2018-05-07 05:00:00', N'2018-05-07 17:00:00'),
(1000025, N'2018-05-08 05:00:00', N'2018-05-08 17:00:00'),
(1000025, N'2018-05-09 05:00:00', N'2018-05-09 17:00:00'),
(1000025, N'2018-05-10 05:00:00', N'2018-05-10 17:00:00'),
(1000025, N'2018-05-11 05:00:00', N'2018-05-11 17:00:00'),
(1000025, N'2018-05-12 05:00:00', N'2018-05-12 17:00:00'),
(1000026, N'2018-05-06 05:00:00', N'2018-05-06 17:00:00'),
(1000026, N'2018-05-07 05:00:00', N'2018-05-07 17:00:00'),
(1000026, N'2018-05-08 05:00:00', N'2018-05-08 17:00:00'),
(1000026, N'2018-05-09 05:00:00', N'2018-05-09 17:00:00'),
(1000026, N'2018-05-10 05:00:00', N'2018-05-10 17:00:00'),
(1000026, N'2018-05-11 05:00:00', N'2018-05-11 17:00:00'),
(1000026, N'2018-05-12 05:00:00', N'2018-05-12 17:00:00'),
(1000027, N'2018-05-06 05:00:00', N'2018-05-06 17:00:00'),
(1000027, N'2018-05-07 05:00:00', N'2018-05-07 17:00:00'),
(1000027, N'2018-05-08 05:00:00', N'2018-05-08 17:00:00'),
(1000027, N'2018-05-09 05:00:00', N'2018-05-09 17:00:00'),
(1000027, N'2018-05-10 05:00:00', N'2018-05-10 17:00:00'),
(1000027, N'2018-05-11 05:00:00', N'2018-05-11 17:00:00'),
(1000027, N'2018-05-12 05:00:00', N'2018-05-12 17:00:00'),
(1000028, N'2018-05-06 05:00:00', N'2018-05-06 17:00:00'),
(1000028, N'2018-05-07 05:00:00', N'2018-05-07 17:00:00'),
(1000028, N'2018-05-08 05:00:00', N'2018-05-08 17:00:00'),
(1000028, N'2018-05-09 05:00:00', N'2018-05-09 17:00:00'),
(1000028, N'2018-05-10 05:00:00', N'2018-05-10 17:00:00'),
(1000028, N'2018-05-11 05:00:00', N'2018-05-11 17:00:00'),
(1000028, N'2018-05-12 05:00:00', N'2018-05-12 17:00:00'),
(1000029, N'2018-05-06 05:00:00', N'2018-05-06 17:00:00'),
(1000029, N'2018-05-07 05:00:00', N'2018-05-07 17:00:00'),
(1000029, N'2018-05-08 05:00:00', N'2018-05-08 17:00:00'),
(1000029, N'2018-05-09 05:00:00', N'2018-05-09 17:00:00'),
(1000029, N'2018-05-10 05:00:00', N'2018-05-10 17:00:00'),
(1000029, N'2018-05-11 05:00:00', N'2018-05-11 17:00:00'),
(1000029, N'2018-05-12 05:00:00', N'2018-05-12 17:00:00'),
(1000030, N'2018-05-06 05:00:00', N'2018-05-06 17:00:00'),
(1000030, N'2018-05-07 05:00:00', N'2018-05-07 17:00:00'),
(1000030, N'2018-05-08 05:00:00', N'2018-05-08 17:00:00'),
(1000030, N'2018-05-09 05:00:00', N'2018-05-09 17:00:00'),
(1000030, N'2018-05-10 05:00:00', N'2018-05-10 17:00:00'),
(1000030, N'2018-05-11 05:00:00', N'2018-05-11 17:00:00'),
(1000030, N'2018-05-12 05:00:00', N'2018-05-12 17:00:00'),
(1000031, N'2018-05-06 05:00:00', N'2018-05-06 17:00:00'),
(1000031, N'2018-05-07 05:00:00', N'2018-05-07 17:00:00'),
(1000031, N'2018-05-08 05:00:00', N'2018-05-08 17:00:00'),
(1000031, N'2018-05-09 05:00:00', N'2018-05-09 17:00:00'),
(1000031, N'2018-05-10 05:00:00', N'2018-05-10 17:00:00'),
(1000031, N'2018-05-11 05:00:00', N'2018-05-11 17:00:00'),
(1000031, N'2018-05-12 05:00:00', N'2018-05-12 17:00:00')
GO

