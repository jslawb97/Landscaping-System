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
		('Lawn Mower 1','Fuel -  87 octane/87 AKI; 1 Gallon, - TracGard N766 TURF Bias Tire 15X6.00-6 B/4 Ply, Air Pressor - 2-3 PSI, Blades - Stens 340 178 Hi-Lift , Oil - 5W-30 Conventional Motor Oil 24oz *MAX AMOUNT*' ),
		('Wheel Barrow 1','Tires - TracGard N766 TURF Bias Tire 15X6.00-6 B/4 Ply, Air Pressor - 2-3 PSI' ),
		('Tractor 2','Tires - POWER KING D402, Fuel - TVO; 10 gallons , Oil - Hydraulic Machine Oil 12 gallons *MAX Amount*')

GO

print '' print '*** Inserting PrepCheckList Sample Data'
GO
	INSERT INTO [dbo].[PrepCheckList]
		([Name], [Description])
	VALUES
		('Tractor 1','Tires, Fuel, Oil' ),
		('Lawn Mower 1', 'Fuel, Tires, Blades, Belts, Oil' ),
		('Wheel Barrow 1','Hand Grips, Washers and Bolts, Stands, Wheels' ),
		('Tractor 2','Oil Fuel')

GO

print '' print '*** Inserting InspectionCheckList Sample Data'
GO
	INSERT INTO [dbo].[InspectionCheckList]
		([Name], [Description])
	VALUES
		('Tractor 1', 'Tires, Fuel, Oil, Pedals, Grease Fittings, Seals and Belts, Guages, Leaks' ),
		('Lawn Mower 1', 'Fuel, Tires, Blades, Belts, Deck, Oil' ),
		('Wheel Barrow 1', 'Hand Grips, Washers and Bolts, Frame, Bracking, Bucket, Stands, Wheels' ),
		('Tractor 2', 'Guages, Lights, Horn, Mirrors, Oil, Fuel, Radiator, Battery, Main Frame, Leaks, Hoses, Fan, Belts')

GO

print '' print '*** Inserting Employee Test Records'
GO
INSERT INTO [dbo].[Employee]
		([FirstName], [LastName], [Address], [PhoneNumber]
			,[Email])
	VALUES
		('John1', 'Smith1', '123 4th st', '000-000-0001', 'Example1@email.com'),
		('John2', 'Smith2', '123 5th st', '000-000-0002', 'Example2@email.com'),
		('John3', 'Smith3', '123 6th st', '000-000-0003', 'Example3@email.com'),
		('John4', 'Smith4', '123 7th st', '000-000-0004', 'Example4@email.com'),
		('John5', 'Smith5', '123 8th st', '000-000-0005', 'Example5@email.com'),
		('John6', 'Smith6', '123 9th st', '000-000-0006', 'Example6@email.com'),
		('John7', 'Smith7', '123 10th st', '000-000-0007', 'Example7@email.com'),
		('John8', 'Smith8', '123 11th st', '000-000-0008', 'Example8@email.com'),
		('John9', 'Smith9', '123 12th st', '000-000-0009', 'Example9@email.com'),
		('John10', 'Smith10', '123 13th st', '000-000-0010', 'Example10@email.com')
GO

print '' print '*** Inserting Role Test Records'
GO
INSERT INTO [dbo].[Role]
		([RoleID], [Description])
	VALUES
		('Admin', 'Description1'),
		('Mechanic', 'Description2'),
		('Maintenance', 'Description7'),
		('Prep', 'Description3'),
		('Inspector', 'Description4'),
		('Supply Clerk', 'Description5'),
		('Applicant', 'Description6'),
		('Customer', 'Description7'),
		('Labor Scheduler', 'Description7'),
		('Equipment Scheduler', 'Description7'),
		('Job Scheduler', 'Description7'),
		('Delivery', 'Description8'),
		('Manager', 'Description9')
GO

print '' print '*** Inserting Certification Test Records'
GO
INSERT INTO [dbo].[Certification]
		([Name], [Description])
	VALUES
		('Name1','Description1'),
		('Name2','Description2'),
		('Name3','Description3'),
		('Name4','Description4'),
		('Name5','Description5'),
		('Name6','Description6'),
		('Name7','Description7'),
		('Name8','Description8'),
		('Name9','Description9'),
		('Name10','Description10')
GO

print '' print '*** Inserting EmployeeCertification Records'
GO
INSERT INTO [dbo].[EmployeeCertification]
		([CertificationID], [EmployeeID], [EndDate])
	VALUES
		(1000001, '1000001', '2018-12-18'),
		(1000001, '1000002', '2019-02-08'),
		(1000002, '1000003', '2018-11-12'),
		(1000002, '1000004', '2019-05-26'),
		(1000003, '1000005', '2020-01-13'),
		(1000003, '1000006', '2018-10-17'),
		(1000004, '1000007', '2020-01-02'),
		(1000004, '1000008', '2019-08-29'),
		(1000005, '1000009', '2018-12-06')
	
GO

print '' print '*** Inserting into SupplyStatus Test Records'
GO
INSERT INTO [dbo].[SupplyStatus]
		([SupplyStatusID])
	VALUES
		('Available'),
		('Delivered'),
		('Out'),
		('Needs Inspection')
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
		('Commercial', 'ericka183@gmail.com', 'Ericka', 'Taube', '3193965262'),
		('Residential', 'ckraus@yahoo.com', 'Carrol', 'Kraus', '3192706440'),
		('Residential', 'hBrandwein@hotmail.com', 'Holly', 'Brandwein', '3193934817'),
		('Residential', 'oValenzuela@hotmail.com', 'Orlan', 'Valenzuela', '3193939065'),
		('Residential', 'shanemad@yahoo.com', 'Shane', 'Madison', '3198992200'),
		('Residential', 'clemens1@msn.com', 'Clemens', 'Whitley', '3198327094'),
		('Residential', 'bballard@fios.com', 'Bryce', 'Ballard', '3193890801'),
		('Residential', 'kowens3@hotmail.com', 'Kristopher', 'Owens', '3193896132'),
		('Commercial', 'tommyham@email.com', 'Tommy', 'Hamilton', '3193608687'),
		('Commercial', 'jamesmmoore@yahoo.com', 'James', 'Moore', '3197434915'),
		('Residential', 'fullermarion@icloud.com', 'Marion', 'Fuller', '3193939372'),
		('Residential', 'evelynbark34@gmail.com', 'Evelyn', 'Barker', '3198992205'),
		('Residential', 'cgutierrez@msn.com', 'Carolyn', 'Gutierrez', '3198995021'),
		('Commercial', 'karaturner@outlook.com', 'Kara', 'Turner', '3192703512'),
		('Residential', 'mknighy@fios.com', 'Melvin', 'Knight', '3193604050'),
		('Commercial', 'padillaruby@verizon.com', 'Ruby', 'Padilla', '3193290451'),
		('Residential', 'christinagordon@gmail.com', 'Christina', 'Gordon', '3192709891'),
		('Residential', 'sandra31223@hotmail.com', 'Sandra', 'Martinez', '3192707914'),
		('Residential', 'lowellchapman@email.com', 'Lowell', 'Chapman', '3193774301'),
		('Commercial', 'willlaw2@fios.com', 'Willard', 'Lawson', '3193774139'),
		('Residential', 'kurtfrank@yahoo.com', 'Kurt', 'Frank', '3194471048'),
		('Residential', 'lukepalmer3@gmail.com', 'Luke', 'Palmer', '3194471200'),
		('Commercial', 'robinLlong@icloud.com', 'Robin', 'Long', '3194471590'),
		('Commercial', 'dompayne@outlook.com', 'Dominick', 'Payne', '3193739995'),
		('Commercial', 'angelaperez@verizon.com', 'Angela', 'Perez', '3193775664'),
		('Residential', 'arlenewilkins@msn.com', 'Arlene', 'Wilkins', '3193737003'),
		('Commercial', 'jklein8832@gmail.com', 'Jonathan', 'Klein', '3193735558'),
		('Commercial', 'fayemathis@icloud.com', 'Faye', 'Mathis', '3193291936'),
		('Commercial', 'raquelryan@hotmaail.com', 'Raquel', 'Ryan', '3193608687'),
		('Commercial', 'maggiewagnet@outlook.com', 'Maggie', 'Wagner', '3192944534'),
		('Residential', 'harrymckenzie@email.com', 'Harry', 'McKenzie', '3192944532'),
		('Residential', 'jackfrench@gmail.com', 'Jack', 'French', '3193890808'),
		('Residential', 'jessiegomez@yahoo.com', 'Jessie', 'Gomez', '3193951245'),
		('Commercial', 'claudiamorris@hotmail.com', 'Claudia', 'Morris', '3197435873'),
		('Residential', 'lelawhite@yahoo.com', 'Lela', 'White', '3192948995'),
		('Commercial', 'jamiesullivan@yahoo.com', 'Jamie', 'Sullivan', '3192948547'),
		('Residential', 'kelleyduncan@gmail.com', 'Kelley', 'Duncan', '3198998971'),
		('Residential', 'sidneyquinn@sprint.com', 'Sidney', 'Quinn', '3192707835'),
		('Residential', 'leahw223@fios.com', 'Leah', 'Wallace', '3193609639'),
		('Residential', 'dhart5543@gmail.com', 'Desiree', 'Hart', '3193607477'),
		('Residential', 'justinhansen@mail.com', 'Justin', 'Hansen', '3192943918'),
		('Residential', 'Almorris@icloud.com', 'Alejandro', 'Morrison', '3192706447'),
		('Residential', 'lucychapman@msn.com', 'Lucy', 'Chapman', '3198327093'),
		('Residential', 'joycephelps@verizon.com', 'Joyce', 'Phelps', '3192704579'),
		('Residential', 'lauraeningram@sprint.com', 'Lauren', 'Ingram', '3198324570'),
		('Residential', 'geneoliver@outlook.com', 'Gene', 'Oliver', '3193607412'),
		('Residential', 'kirkchavez@outlook.com', 'Kirk', 'Chavez', '3193934888'),
		('Residential', 'lynnrhodes@yahoo.com', 'Lynn', 'Rhodes', '3193939000'),
		('Residential', 'lancerodriquez@gmail.com', 'Lance', 'Rodriquez', '3193608841'),
		('Commercial', 'bryanrise@fios.com', 'Bryan', 'Rise', '3197439941'),
		('Residential', 'hangram@verizon.com', 'Hannah', 'Graham', '3193934457'),
		('Residential', 'ronnierich@hotmail.com', 'Ronnie', 'Richards', '3193664571'),
		('Commercial', 'ericpittman@icloud.com', 'Eric', 'Pittman', '3193621448'),
		('Residential', 'jaredshelton1@yahoo.com', 'Jared', 'Shelton', '3195504413'),
		('Residential', 'eileenwilkins@gmail.com', 'Eileen', 'Wilkins', '3192044576'),
		('Residential', 'stellamiles@mail.com', 'Stella', 'Miles', '3193664487'),
		('Commercial', 'ricardoM@yahoo.com', 'Ricardo', 'Morrison', '3197412246'),
		('Residential', 'sergioturner@sprint.com', 'Sergio', 'Turner', '3198994412'),
		('Commercial', 'marshallwebb@outlook.com', 'Marshall', 'Webb', '3193614452'),
		('Residential', 'claypark@yahoo.com', 'Clay', 'Park', '3197742546'),
		('Commercial', 'dalebaldwin@gmail.com', 'Dale', 'Baldwin', '3193664412'),
		('Commercial', 'warrenreynolsa@yahoo.com', 'Warren', 'Reynolsa', '3193687745'),
		('Commercial', 'shawnamorton@email.com', 'Shawna', 'Morton', '3193387741'),
		('Residential', 'randygibson@icloud.com', 'Randy', 'Gibson', '3192020014'),
		('Residential', 'rosecaldwell@sprint.com', 'Rose', 'Caldwell', '3192021145'),
		('Residential', 'valmahguyen@hotmail.com', 'Velma', 'Nguyen', '3192214478'),
		('Residential', 'raypeterson@tmobile.com', 'Ray', 'Peterson', '31993654441'),
		('Residential', 'sheldonwarmer2@yahoo.com', 'Sheldon', 'Warmer', '3193665547'),
		('Commercial', 'jeanfigueroa@hotmail.com', 'Jean', 'Figueroa', '3195517884'),
		('Residential', 'cecilfields@sprint.com', 'Cecil', 'Fields', '3192415547'),
		('Commercial', 'clintonjordon@email.com', 'Clinton', 'Jordon', '3193687741'),
		('Commercial', 'curtisolson@icloud.com', 'Curtis', 'Olson', '3193935541'),
		('Residential', 'elmercarson@verizon.com', 'Elmer', 'Carson', '3198991147'),
		('Residential', 'lizgreen@tmoblie.com', 'Elizabth', 'Green', '3197412262'),
		('Residential', 'irvingmitchel3@yahoo.com', 'Irving', 'Mitchell', '3192941145'),
		('Residential', 'dennisc223@verizon.com', 'Constance', 'Dennis', '3193392146'),
		('Residential', 'andygardner12@gmail.com', 'Andy', 'Gardner', '3199912247'),
		('Commercial', 'anitaday@msn.com', 'Anita', 'Day', '3195501147'),
		('Residential', 'lynnfancis@hotmail.com', 'Lynn', 'Francis', '3195510042'),
		('Residential', 'jimmyadams@fios.com', 'Jimmy', 'Adams', '3196514478'),
		('Residential', 'carllynch@gmail.com', 'Carl', 'Lynch', '3196623347'),
		('Commercial', 'yolandapratt3@yahoo.com', 'Yolanda', 'Pratt', '3192981744'),
		('Residential', 'pattisims@outlook.com', 'Patti', 'Sims', '3192889941'),
		('Commercial', 'ellapark@msn.com', 'Ella', 'Park', '3193381125'),
		('Commercial', 'calvinstokes@hotmail.com', 'Calvin', 'Stokes', '3197452213'),
		('Commercial', 'johnnichols@icloud.com', 'Johnny', 'Nichols', '3196510047'),
		('Commercial', 'bryantmills@sprint.com', 'Bryant', 'Mills', '3198997741'),
		('Commercial', 'samgilber2@gmail.com', 'Sammy', 'Gilbert', '3192214453'),
		('Residential', 'yvettereed@outlook.com', 'Yvette', 'Reed', '3196517788'),
		('Residential', 'curtneygrant@fios.com', 'Courtney', 'Grant', '3196634410'),
		('Commercial', 'bobbyhampton@hotmail.com', 'Bobby', 'Hampton', '3195504478'),
		('Residential', 'lelandcastillo@outlook.com', 'Leland', 'Castillo', '3197413354'),
		('Residential', 'stacyscott3@msn.com', 'Stacy', 'Scott', '3193364412'),
		('Commercial', 'edithkelly@gmail.com', 'Edith', 'Kelly', '3193384874'),
		('Residential', 'antoniosantos@hotmail.com', 'Antonio', 'Santos', '3199897710'),
		('Residential', 'brenthenderson@yahoo.com', 'Brent', 'Henderson', '3192020974'),
		('Residential', 'dwightadams77@gmail.com', 'Dwight', 'Adams', '319930004')
	
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

print '' print '*** Inserting Applicant Sample Data'
GO
INSERT INTO [dbo].[Applicant]
		([FirstName], [LastName], [Address], [PhoneNumber],
		[Email])
	VALUES
		('John', 'Doe', '123 A St', '(123) 456-7890', 'abc@gmail.com'),
		('Sherlock', 'Holmes', '212 Baker St', '(123) 456-7891', 'def@gmail.com'),
		('Albert', 'Einstein', '456 B Blvd', '(123) 456-7892', 'abc@gmail.com'),
		('HIAlary', 'Clinton', '789 C Rd', '(123) 456-7893', 'ghi@gmail.com'),
		('Harry', 'Potter', '4 Privet Dr', '(123) 456-7894', 'jkl@gmail.com')
GO

print '' print '*** Inserting EquipmentStatus Sample Data'
GO
INSERT INTO [dbo].[EquipmentStatus]
		([EquipmentStatusID])
	VALUES
		('Available'),
		('At Inspection'),
		('At Maintenance'),
		('At Prep')
GO

print '' print '*** Inserting into EquipmentType Test Records'
GO
INSERT INTO [dbo].[EquipmentType]
		([EquipmentTypeID], [PrepChecklistID])
	VALUES
		('lawn mower', '1000002'),
		('wheel barrow', '1000001'),
		('mini excavator', '1000003'),
		('tractor', '1000002')
GO

print '' print '*** Inserting into MakeModel Test Records'
GO
INSERT INTO [dbo].[MakeModel]
		([Make], [Model], [MaintenanceCheckListID])
	VALUES
		('John Deer', '100F', 1000000),
		('John Deer', '102', 1000000),
		('Jackson', 'Poly', 1000000),
		('CAT', '300.9D', 1000000)
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
		(1000004, '1843 Johnson Ave NW', 'Cedar Rapids', 'IA', '52405', 'NONE'),
		(1000005, '5918 Sharon Ln NW ', 'Cedar Rapids', 'IA', '52405', 'NONE'),
		(1000006, '3963 Sally Dr NE', 'Cedar Rapids', 'IA', '52402', 'NONE'),
		(1000006, '1280 S 15th St', 'Marion', 'IA', '52302', 'NONE'),
		(1000007, '1075 Edwin Dr', 'Marion', 'IA', '52302', 'NONE'),
		(1000008, '3600 White Oak Rd SE', 'Cedar Rapids', 'IA', '52403', 'NONE'),
		(1000009, '2715 Goldfinch Ln', 'Hiawatha', 'IA', '52233', 'NONE'),
		(1000010, '2000 Rockford Rd SW', 'Cedar Rapids', 'IA', '52404', 'NONE'),
		(1000011, '504 Fairfax Rd', 'Fairfax', 'IA', '52228', 'NONE'),
		(1000012, '2030 6th St SW', 'Cedar Rapids', 'IA', '52404', 'NONE'),
		(1000013, '1621 13th St NW', 'Cedar Rapids', 'IA', '52405', 'NONE'),
		(1000014, '2795 Silver Oak Trl', 'Marion', 'IA', '52302', 'NONE'),
		(1000015, '2415 2nd St SW', 'Cedar Rapids', 'IA', '52404', 'NONE'),
		(1000016, '5571 Meadow Grass Cir SE', 'Cedar Rapids', 'IA', '52403', 'No mowing between 12PM and 2PM'),
		(1000017, '400 41st Ave SW', 'Cedar Rapids', 'IA', '52404', 'NONE'),
		(1000018, '1618 J Ave NE', 'Cedar Rapids', 'IA', '52402', 'NONE'),
		(1000019, '901 Ingleside Dr SW', 'Cedar Rapids', 'IA', '52404', 'NONE'),
		(1000019, '6305 11th St SW', 'Cedar Rapids', 'IA', '52404', 'NONE'),
		(1000020, '4730 Bever Ave SE', 'Cedar Rapids', 'IA', '52403', 'NONE'),
		(1000021, '205 Teakwood Ln NE', 'Cedar Rapids', 'IA', '52402', 'NONE'),
		(1000022, '3502 Royal Dr SW', 'Cedar Rapids', 'IA', '52404', 'NONE'),
		(1000023, '1323 O Ave NW', 'Cedar Rapids', 'IA', '52405', 'NONE'),
		(1000024, '6811 Surrey Dr NE', 'Cedar Rapids', 'IA', '52402', 'NONE'),
		(1000025, '2531 Ellis View Ct NW', 'Cedar Rapids', 'IA', '52405', 'NONE'),
		(1000026, '403 6th St SW', 'Cedar Rapids', 'IA', '52404', 'NONE'),
		(1000027, '919 6th St SW', 'Cedar Rapids', 'IA', '52404', 'NONE'),
		(1000028, '5515 Council St NE', 'Cedar Rapids', 'IA', '52402', 'NONE'),
		(1000029, '4300 Windemere Way', 'Marion', 'IA', '52302', 'NONE'),
		(1000030, '2160 Linden Dr SE', 'Cedar Rapids', 'IA', '52403', 'NONE'),
		(1000031, '4444 1st Ave NE', 'Cedar Rapids', 'IA', '52402', 'NONE'),
		(1000032, '3217 1st Ave SE', 'Cedar Rapids', 'IA', '52403', 'NONE'),
		(1000033, '4444 C Ave NE', 'Cedar Rapids', 'IA', '52402', 'NONE'),
		(1000034, '2267 Washington Ave SE', 'Cedar Rapids', 'IA', '52403', 'NONE'),
		(1000035, '4 Cottage Grove Woods SE', 'Cedar Rapids', 'IA', '52403', 'NONE'),
		(1000035, '2800 1st Ave NW', 'Cedar Rapids', 'IA', '52405', 'NONE'),
		(1000036, '285 Robins Rd', 'Hiawatha', 'IA', '52233', 'NONE'),
		(1000037, '317 7th Ave SE', 'Cedar Rapids', 'IA', '52403', 'NONE'),
		(1000038, '1220 Alpine Rd', 'Marion', 'IA', '52302', 'NONE'),
		(1000039, '1200 11th St NW', 'Cedar Rapids', 'IA', '52405', 'NONE'),
		(1000040, '2054 G Ave NE', 'Cedar Rapids', 'IA', '52402', 'NONE'),
		(1000041, '4014 Treeline Ct NE', 'Cedar Rapids', 'IA', '52411', 'Yard waste pickup is on Tuesdays'),
		(1000042, '303 Pebble Ln', 'Hiawatha', 'IA', '52233', 'NONE'),
		(1000043, '1925 Wilson Ave SW', 'Cedar Rapids', 'IA', '52404', 'NONE'),
		(1000044, '6420 Council St NE', 'Cedar Rapids', 'IA', '52402', 'NONE'),
		(1000045, '6108 Dean Rd SW', 'Cedar Rapids', 'IA', '52404', 'NONE'),
		(1000046, '439 20th St NW', 'Cedar Rapids', 'IA', '52405', 'NONE'),
		(1000047, '226 Greenfield St NE', 'Cedar Rapids', 'IA', '52402', 'NONE'),
		(1000048, '4061 Charter Oak Ln SE', 'Cedar Rapids', 'IA', '52403', 'NONE'),
		(1000049, '434 Stoney Creek Rd NW', 'Cedar Rapids', 'IA', '52405', 'NONE'),
		(1000050, '7423 Mount Vernon Rd SE', 'Cedar Rapids', 'IA', '52403', 'NONE'),
		(1000051, '3711 Stonewall Ct NW', 'Cedar Rapids', 'IA', '52405', 'NONE'),
		(1000052, '2603 Bryant Blvd SW', 'Cedar Rapids', 'IA', '52404', 'NONE'),
		(1000053, '1004 1st Ave NW', 'Cedar Rapids', 'IA', '52405', 'NONE'),
		(1000054, '2860 Seely Ave SE', 'Cedar Rapids', 'IA', '52403', 'NONE'),
		(1000055, '208 30th Street Dr SE', 'Cedar Rapids', 'IA', '52403', 'NONE'),
		(1000056, '4990 Johnson Ave NW', 'Cedar Rapids', 'IA', '52405', 'NONE'),
		(1000057, '6712 Idlebrook Ln NE', 'Cedar Rapids', 'IA', '52403', 'NONE'),
		(1000058, '717 Blake Blvd SE', 'Cedar Rapids', 'IA', '52403', 'NONE'),
		(1000059, '2175 Chandler St SW', 'Cedar Rapids', 'IA', '52404', 'NONE'),
		(1000060, '400 41st Avenue Dr SW', 'Cedar Rapids', 'IA', '52404', 'NONE'),
		(1000061, '3325 Mansfield Ave SE', 'Cedar Rapids', 'IA', '52403', 'NONE'),
		(1000062, '222 Edgewood Rd NW,', 'Cedar Rapids', 'IA', '52405', 'NONE'),
		(1000063, '1372 38th St SE', 'Cedar Rapids', 'IA', '52403', 'NONE'),
		(1000064, '320 Blairs Ferry Rd NE', 'Cedar Rapids', 'IA', '52233', 'NONE'),
		(1000065, '3358 Center Point Rd NE', 'Cedar Rapids', 'IA', '52402', 'NONE'),
		(1000066, '2840 7th Ave', 'Marion', 'IA', '52302', 'NONE'),
		(1000067, '1715 C Ave NW', 'Cedar Rapids', 'IA', '52405', 'NONE'),
		(1000068, '1244 Robinwood Ln NE', 'Cedar Rapids', 'IA', '52402', 'NONE'),
		(1000069, '5630 Woodbridge Crst', 'Marion', 'IA', '52302', 'NONE'),
		(1000070, '2412 Arlington St SW', 'Cedar Rapids', 'IA', '52404', 'NONE'),
		(1000071, '5340 Bowe Ct SW', 'Cedar Rapids', 'IA', '52404', 'NONE'),
		(1000072, '3200 16th Ave SW', 'Cedar Rapids', 'IA', '52404', 'NONE'),
		(1000073, '131 Leroy St NW', 'Cedar Rapids', 'IA', '52405', 'NONE'),
		(1000074, '3349 Mt Vernon Rd SE', 'Cedar Rapids', 'IA', '52403', 'NONE'),
		(1000075, '3434 1st Ave NE', 'Cedar Rapids', 'IA', '52402', 'NONE'),
		(1000076, '160 Redfox Rd SE', 'Cedar Rapids', 'IA', '52403', 'NONE'),
		(1000077, '2222 1st Ave NE', 'Cedar Rapids', 'IA', '52402', 'NONE'),
		(1000078, '53 33rd Ave SW', 'Cedar Rapids', 'IA', '52404', 'NONE'),
		(1000078, '3322 Terry Dr SE', 'Cedar Rapids', 'IA', '52403', 'NONE'),
		(1000079, '1426 7th Ave SE', 'Cedar Rapids', 'IA', '52403', 'NONE'),
		(1000080, '6523 Boxwood Ln NE', 'Cedar Rapids', 'IA', '52402', 'Bag the grass'),
		(1000081, '1201 1st Ave SE', 'Cedar Rapids', 'IA', '52402', 'NONE'),
		(1000082, '4410 Viola St NE', 'Cedar Rapids', 'IA', '52411', 'NONE'),
		(1000083, '6325 Rockwell Dr NE', 'Cedar Rapids', 'IA', '52402', 'NONE'),
		(1000084, '2718 Prairie Dr NE', 'Cedar Rapids', 'IA', '52402', 'NONE'),
		(1000085, '975 S 11th St', 'Marion', 'IA', '52302', 'NONE'),
		(1000086, '2601 Maple Dr NW', 'Cedar Rapids', 'IA', '52405', 'NONE'),
		(1000087, '201 8th Ave SE', 'Cedar Rapids', 'IA', '52401', 'NONE'),
		(1000088, '1661 32nd St NE', 'Cedar Rapids', 'IA', '52402', 'Service Monday-Wednesday only'),
		(1000089, '341 22nd St NE', 'Cedar Rapids', 'IA', '52402', 'NONE'),
		(1000090, '340 Marion Blvd', 'Marion', 'IA', '52302', 'NONE'),
		(1000091, '501 6th St SW', 'Cedar Rapids', 'IA', '52404', 'NONE'),
		(1000092, '257 23rd St NW', 'Cedar Rapids', 'IA', '52405', 'NONE'),
		(1000093, '2951 6th St SW', 'Cedar Rapids', 'IA', '52404', 'NONE'),
		(1000094, '2020 7th Ave', 'Marion', 'IA', '52302', 'NONE'),
		(1000095, '7130 Chelsea Dr NE', 'Cedar Rapids', 'IA', '52402', 'NONE'),
		(1000096, '415 Hanover Rd SW', 'Cedar Rapids', 'IA', '52404', 'NONE'),
		(1000097, '130 41st Ave Dr SW', 'Cedar Rapids', 'IA', '52404', 'NONE'),
		(1000098, '2224 Grande Ave SE', 'Cedar Rapids', 'IA', '52403', 'NONE'),
		(1000098, '3122 Waveland Dr NW', 'Cedar Rapids', 'IA', '52405', 'NONE'),
		(1000099, '2480 24th St', 'Marion', 'IA', '52302', 'NONE')
		
GO

print '' print '*** Inserting EmployeeRole Records'
GO
INSERT INTO [dbo].[EmployeeRole]
		([EmployeeID], [RoleID])
	VALUES
		(1000000, "Supply Clerk"),
		(1000001, "Labor Scheduler"),
		(1000002, "Equipment Scheduler"),
		(1000003, "Inspector"),
		(1000004, "Mechanic"),
		(1000005, "Maintenance"),
		(1000006, "Job Scheduler"),
		(1000007, "Manager")
GO

print '' print '*** Inserting PaymentType Records'
GO
INSERT INTO [dbo].[PaymentType]
		([PaymentTypeID], [Description])
	VALUES
		("Credit Card", "Pay by Visa/Mastercard/American Express"),
		("Checking Account", "Pay by ACH payment")
GO

print '' print '*** Inserting Message Records'
GO
INSERT INTO [dbo].[Message]
		([ToEmployeeID], [FromEmployeeID], [SendDate], [Subject], [Message])
	VALUES
		(1000000, 1000001, '2018-01-10', "Test1", "This is a test message"),
		(1000001, 1000002, '2018-01-10', "Test2", "This is a test message"),
		(1000002, 1000003, '2018-01-10', "Test3", "This is a test message")

GO

print '' print '*** Inserting Equipment Records'
INSERT INTO [dbo].[Equipment]
	([EquipmentTypeID], [Name], [MakeModelID], [DatePurchased], 
	[DateLastRepaired], [PriceAtPurchase],[CurrentValue],[WarrantyUntil],
	[EquipmentStatusID],[EquipmentDetails])
	VALUES
			('lawn mower', 'Tractor1', '1000002', '2016-10-12', '2018-01-13'
			, '12000', '9000', '2019-01-01', 'Available', 'This is our favorite tractor.'),
			
			('wheel barrow', 'LawnMower3.5', '1000001', '2017-03-26', '2017-11-15'
			, '1000.99', '850', '2020-12-31', 'At Maintenance', 'This is a lawn mower.'),
			
			('mini excavator', 'WheelBarrowBeta', '1000003', '2017-08-15', '2018-01-24'
			, '75.00', '50.54', '2018-02-15', 'At Inspection', 'A wheel barrow. Used to move very heavy things.'),
			
			('tractor', 'MiniExcavatorAlpha', '1000002', '2017-06-05', '2017-10-31'
			, '25000', '22000', '2019-12-31', 'At Prep', 'A miniature excavator. Kind of like a very small backhoe.')
			
GO

print '' print '*** Inserting InspectionRecord Test Records'
INSERT INTO [dbo].[InspectionRecord]
           ([EquipmentID], [EmployeeID], [Description], [Date])
     VALUES
           (1000000, 1000003, 'Inspection Passed', CONVERT(date, '2017-08-25')),
           (1000001, 1000003, 'Inspection Passed', CONVERT(date, '2017-08-25')),
           (1000002, 1000003, 'Inspection Failed', CONVERT(date, '2017-08-25')),
           (1000003, 1000003, 'Inspection Passed', CONVERT(date, '2017-08-26'))
GO

print '' print '*** Inserting PrepRecord Records'
INSERT INTO [dbo].[PrepRecord]
	([EquipmentID], [EmployeeID], [Description], [Date])
	VALUES
			(1000000, '1000001', 'Went well', '2016-05-10'),
			(1000001, '1000002', 'Could have gone better', '2017-08-15'),
			(1000002, '1000000', '7.5/10', '2018-01-05'),
			(1000003, '1000003', 'Exceptional preparation.', '2017-03-12')
GO

print '' print '*** Inserting ExceptionType Test Records'
GO
INSERT INTO [dbo].[ExceptionType]
		([ExceptionTypeID])
	VALUES
		('Ordered Item Out Of Stock'),
		('Discontinued by Manufacturer'),
		('Insufficient Quantity Ordered'),
		('Order Mismatch'),
		('No longer sold by Supplier')
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
		('5 Gallon Oil Container', 'A plastic 5 gallon oil container.', 'Row 5, Column 2', 14, 1000, 1250),
		('Open-End Wrench', 'An open end wrench', 'Row 2, Column 6', 7, 750, 1150),
		('Torque Wrench', 'A torque wrench.', 'Row 2, Column 7', 15, 85, 135),
		('Pipe Wrench', 'A pipe wrench.', 'Row 2, Column 5', 10, 50, 100),
		('5.5 HP (173cc) OHV Vertical Shaft Gas Engine', 'A vertical shaft engine for a lawnmower.', 'Row 1, Column 1', 4, 2, 2),
		('Sod', 'Farmed grass', 'Supply Yard', 200, 500, 1000)
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
		('ServiceOffering1', 'NONE'),
		('ServiceOffering2', 'NONE'),
		('ServiceOffering3', 'NONE'),
		('ServiceOffering4', 'NONE'),
		('ServiceOffering5', 'NONE'),
		('ServiceOffering6', 'NONE'),
		('ServiceOffering7', 'NONE'),
		('ServiceOffering8', 'NONE'),
		('ServiceOffering9', 'NONE'),
		('ServiceOffering10', 'NONE'),
		('ServiceOffering11', 'NONE')
GO

print '' print '*** Inserting ServiceItem Records'
GO
INSERT INTO [dbo].[ServiceItem]
		([Name], [Description])
	VALUES
		("Lawn Mowing Package", "Mow 'x' amount of land."),
		("Tree Trimming Package", "Trim 'x' amount of trees"),
		("Sod Installation", "Install 'x' square feet of sod")

GO


/* Created 03/04/2018 - John Miller*/
print '' print '*** Inserting TaskType Test Records'
GO
INSERT INTO [dbo].[TaskType]
		([Name], [Quantity], [JobLocationAttributeTypeID])
	VALUES
		('Mow', 1, 'Acres to Mow'),
		('Trim Tree', 5, 'Trees to Trim'),
		('Prepare Sod', 1, 'Sq. Ft. of Sod'),
		('Prepare Ground for Sod', 1, 'Sq. Ft. of Sod'),
		('Install Sod', 1, 'Sq. Ft. of Sod')
GO

print '' print '*** Inserting Task Test Records'
GO
INSERT INTO [dbo].[Task]
		([TaskTypeID], [ServiceItemID], [Name], [Description])
	VALUES
		(1000000, 1000000, 'Mow Lawn', 'NONE'),
		(1000001, 1000001, 'Trim Trees', 'NONE'),
		(1000002, 1000002, 'Get Sod', 'NONE'),
		(1000003, 1000002, 'Prepare Ground', 'NONE'),
		(1000004, 1000002, 'Install Sod', 'NONE')
GO

/* Created 03/04/2018 - John Miller*/
print '' print '*** Inserting JobLocationAttributeTypeID Test Records'
GO
INSERT INTO [dbo].[JobLocationAttributeType]
		([JobLocationAttributeTypeID])
	VALUES
		('Acres to Mow'),
		('Sq. Ft. to Mow'),
		('Sq. Ft. of Sod'),
		('Trees to Trim'),
		('Trees to Fertilize'),
		('Flower Beds')
GO

/* Created 03/07/2018 - Zachary Hall*/
print '' print '*** Inserting JobLocationAttribute Test Records'
GO
INSERT INTO [dbo].[JobLocationAttribute]
		([JobLocationID], [JobLocationAttributeTypeID], [Value])
	VALUES
		(1000000, 'Acres to Mow', 1),
		(1000000, 'Trees to Trim', 20),
		(1000000, 'Sq. Ft. of Sod', 50)
GO




		

print '' print '*** Inserting Job Test Records'
GO
INSERT INTO [dbo].[Job]
		([CustomerID], [DateTimeScheduled], [EmployeeID]
		, [JobLocationID], [Comments])
	VALUES
		(1000000, '2017-01-11 10:34:09.900', 1000006, 1000000, "Sample Data"),
		(1000001, '2017-02-11 10:34:09.900', 1000006, 1000001, "Sample Data"),
		(1000002, '2017-03-11 10:34:09.900', 1000006, 1000002, "Sample Data"),
		(1000003, '2017-04-11 10:34:09.900', 1000006, 1000003, "Sample Data"),
		(1000004, '2017-05-11 10:34:09.900', 1000006, 1000004, "Sample Data"),
		(1000005, '2017-06-11 10:34:09.900', 1000006, 1000005, "Sample Data"),
		(1000006, '2017-07-11 10:34:09.900', 1000006, 1000006, "Sample Data"),
		(1000007, '2017-08-11 10:34:09.900', 1000006, 1000007, "Sample Data"),
		(1000008, '2017-09-11 10:34:09.900', 1000006, 1000008, "Sample Data"),
		(1000000, '2017-10-11 10:34:09.900', 1000006, 1000009, "Sample Data"),
		(1000001, '2017-11-11 10:34:09.900', 1000006, 1000010, "Sample Data")
GO

print '' print '*** Inserting JobServicePackage Test Records'
GO
INSERT INTO [dbo].[JobServicePackage]
		([JobID], [ServicePackageID])
	VALUES
		(1000000, 1000000),
		(1000000, 1000001),
		(1000000, 1000002),
		(1000001, 1000000),
		(1000001, 1000001),
		(1000002, 1000000),
		(1000003, 1000000),
		(1000004, 1000000),
		(1000005, 1000000),
		(1000006, 1000000),
		(1000007, 1000000),
		(1000008, 1000000),
		(1000009, 1000000),
		(1000010, 1000000)
GO


print '' print '*** Inserting ServicePackageOffering Test Records'
GO
INSERT INTO [dbo].[ServicePackageOffering]
		([ServicePackageID], [ServiceOfferingID])
	VALUES
		(1000000, 1000000),
		(1000000, 1000001),
		(1000000, 1000002),
		(1000000, 1000003),
		(1000001, 1000000),
		(1000001, 1000001),
		(1000001, 1000002),
		(1000002, 1000000)
GO

print '' print '*** Inserting ServiceOfferingItem Test Records'
GO
INSERT INTO [dbo].[ServiceOfferingItem]
		([ServiceOfferingID], [ServiceItemID])
	VALUES
		(1000000, 1000000),
		(1000000, 1000001),
		(1000000, 1000002),
		(1000001, 1000000),
		(1000001, 1000001),
		(1000001, 1000002),
		(1000002, 1000000),
		(1000002, 1000001)
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

print '' print '*** Inserting into SupplyOrder table'
GO
INSERT INTO [dbo].[SupplyOrder]
		([EmployeeID], [JobID], [Date], [SupplyStatusID])
	VALUES
		(1000000, 1000000, '6-16-2018',	'Available'),
        (1000000, 1000001, '7-22-2018',	'Delivered'),
        (1000000, 1000002, '3-19-2018',	'Out'),
        (1000000, 1000003, '6-11-2018',	'Needs Inspection'),
        (1000000, 1000004, '9-09-2019',	'Out')
GO

print '' print '*** Inserting Delivery Test Records'
GO
INSERT INTO [dbo].[Delivery] 
		([Address], [Time], [SupplyOrderID], [SupplyStatusID])
	VALUES
		
		('105 kirkwood CT SW',	'12/12/2017 12:00',1000000, 'Available'),
		('145 Bowling st', 		'11/24/2017 8:00', 1000001,	'Delivered'),
		('6995 kirkwood pl',	'11/01/2017 11:05', 1000002,	'Out')
GO

 
print '' print '*** Inserting Group Records'
GO
INSERT INTO [dbo].[Group]
		([JobID])
	VALUES
		(1000000),
		(1000001),
		(1000002),
		(1000003),
		(1000004),
		(1000005),
		(1000006)
GO
 
print '' print '*** Inserting Employee Group Test Records'
GO
INSERT INTO [dbo].[EmployeeGroup]
		([GroupID], [EmployeeID])
	VALUES
		(1000001, 1000000),
		(1000002, 1000001),
		(1000000, 1000002),
		(1000001, 1000003),
		(1000002, 1000004),
		(1000003, 1000005),
		(1000000, 1000006),
		(1000001, 1000007),
		(1000002, 1000008),
		(1000003, 1000009)
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

print '' print '*** Inserting Source Data'
GO
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

print '' print '*** Inserting into EquipmentSchedule Test Records'
GO
INSERT INTO [dbo].[EquipmentSchedule]
		([JobID], [EquipmentID], [StartDate], [EndDate])
	VALUES
		(1000001, 1000000, Convert (date,'2018-03-07'), Convert (date,'2018-03-27')),
		(1000002, 1000001, Convert (date,'2018-03-19'), Convert (date,'2018-04-07')),
		(1000000, 1000002, Convert (date,'2018-06-11'), Convert (date,'2018-03-20')),
		(1000003, 1000003, Convert (date,'2018-12-03'), Convert (date,'2018-12-25'))
GO
