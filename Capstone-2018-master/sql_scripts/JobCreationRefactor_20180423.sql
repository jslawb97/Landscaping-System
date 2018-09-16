print '' print '*** in file JobCreationRefactor_20180423.sql ***'
USE [crlandscaping]
GO

print '' print '*** Altering JobID and EquipmentID to Unique in TaskEquipment DROP'
ALTER TABLE TaskEquipment
DROP CONSTRAINT UC_JobID_EquipmentID
GO


print '' print '*** Creating Supply qunatity function ***'
GO
CREATE FUNCTION [dbo].[fn_calculate_supply_quantity_for_job_supply_need]
(
	@TaskTypeSupplyNeedID [int],
	@JobID [int]
)RETURNS int
AS
BEGIN
	DECLARE @Result int, @TaskTypeSupplyNeed_Quantity int, @JobLocationAttribute_Value int,  @TaskType_Value int
	
	SELECT @TaskTypeSupplyNeed_Quantity = [TaskTypeSupplyNeed].[Quantity]
	FROM [TaskTypeSupplyNeed]
	WHERE [TaskTypeSupplyNeedID] = @TaskTypeSupplyNeedID
	
	SELECT @JobLocationAttribute_Value = [JobLocationAttribute].[Value]
	FROM [JobLocationAttribute], [TaskType], [TaskTypeSupplyNeed], [JobLocation], [Job]
	WHERE [JobLocationAttribute].[JobLocationAttributeTypeID] = [TaskType].[JobLocationAttributeTypeID]
	AND [TaskType].[TaskTypeID] = [TaskTypeSupplyNeed].[TaskTypeID]
	AND [TaskTypeSupplyNeed].[TaskTypeSupplyNeedID] = @TaskTypeSupplyNeedID
	AND [JobLocationAttribute].[JobLocationID] = [JobLocation].[JobLocationID]
	AND [JobLocation].[JobLocationID] = [Job].[JobLocationID]
	AND [Job].[JobID] = @JobID
	
	SELECT @TaskType_Value = [TaskType].[Quantity]
	FROM [TaskType], [TaskTypeSupplyNeed]
	WHERE [TaskTypeSupplyNeed].[TaskTypeID] = [TaskType].[TaskTypeID]
	AND [TaskTypeSupplyNeed].[TaskTypeSupplyNeedID] = @TaskTypeSupplyNeedID
	
	SELECT @Result = CEILING(@TaskTypeSupplyNeed_Quantity * ((@JobLocationAttribute_Value * 1.0) / @TaskType_Value))
	
	RETURN @Result
END
GO

print '' print '*** Creating calc employee hours function ***'
GO
CREATE FUNCTION [dbo].[fn_calculate_hours_of_work_for_job_employee_need]
(
	@TaskTypeEmployeeNeedID [int],
	@JobID [int]
)RETURNS int
AS
BEGIN
	DECLARE @Result int, @TaskTypeEmployeeNeed_HoursOfWork int, @JobLocationAttribute_Value int,  @TaskType_Value int
	
	SELECT @TaskTypeEmployeeNeed_HoursOfWork = [TaskTypeEmployeeNeed].[HoursOfWork]
	FROM [TaskTypeEmployeeNeed]
	WHERE [TaskTypeEmployeeNeedID] = @TaskTypeEmployeeNeedID
	
	SELECT @JobLocationAttribute_Value = [JobLocationAttribute].[Value]
	FROM [JobLocationAttribute], [TaskType], [TaskTypeEmployeeNeed], [JobLocation], [Job]
	WHERE [JobLocationAttribute].[JobLocationAttributeTypeID] = [TaskType].[JobLocationAttributeTypeID]
	AND [TaskType].[TaskTypeID] = [TaskTypeEmployeeNeed].[TaskTypeID]
	AND [TaskTypeEmployeeNeed].[TaskTypeEmployeeNeedID] = @TaskTypeEmployeeNeedID
	AND [JobLocationAttribute].[JobLocationID] = [JobLocation].[JobLocationID]
	AND [JobLocation].[JobLocationID] = [Job].[JobLocationID]
	AND [Job].[JobID] = @JobID
	
	SELECT @TaskType_Value = [TaskType].[Quantity]
	FROM [TaskType], [TaskTypeEmployeeNeed]
	WHERE [TaskTypeEmployeeNeed].[TaskTypeID] = [TaskType].[TaskTypeID]
	AND [TaskTypeEmployeeNeed].[TaskTypeEmployeeNeedID] = @TaskTypeEmployeeNeedID
	
	SELECT @Result = CEILING(@TaskTypeEmployeeNeed_HoursOfWork * ((@JobLocationAttribute_Value * 1.0) / @TaskType_Value))
	
	RETURN @Result
END
GO

print '' print '*** Creating calc employee hours function ***'
GO
CREATE FUNCTION [dbo].[fn_calculate_hours_of_work_for_job_equipment_need]
(
	@TaskTypeEquipmentNeedID [int],
	@JobID [int]
)RETURNS int
AS
BEGIN
	DECLARE @Result int, @TaskTypeEquipmentNeed_HoursOfWork int, @JobLocationAttribute_Value int,  @TaskType_Value int
	
	SELECT @TaskTypeEquipmentNeed_HoursOfWork = [TaskTypeEquipmentNeed].[HoursOfWork]
	FROM [TaskTypeEquipmentNeed]
	WHERE [TaskTypeEquipmentNeedID] = @TaskTypeEquipmentNeedID
	
	SELECT @JobLocationAttribute_Value = [JobLocationAttribute].[Value]
	FROM [JobLocationAttribute], [TaskType], [TaskTypeEquipmentNeed], [JobLocation], [Job]
	WHERE [JobLocationAttribute].[JobLocationAttributeTypeID] = [TaskType].[JobLocationAttributeTypeID]
	AND [TaskType].[TaskTypeID] = [TaskTypeEquipmentNeed].[TaskTypeID]
	AND [TaskTypeEquipmentNeed].[TaskTypeEquipmentNeedID] = @TaskTypeEquipmentNeedID
	AND [JobLocationAttribute].[JobLocationID] = [JobLocation].[JobLocationID]
	AND [JobLocation].[JobLocationID] = [Job].[JobLocationID]
	AND [Job].[JobID] = @JobID
	
	SELECT @TaskType_Value = [TaskType].[Quantity]
	FROM [TaskType], [TaskTypeEquipmentNeed]
	WHERE [TaskTypeEquipmentNeed].[TaskTypeID] = [TaskType].[TaskTypeID]
	AND [TaskTypeEquipmentNeed].[TaskTypeEquipmentNeedID] = @TaskTypeEquipmentNeedID
	
	SELECT @Result = CEILING(@TaskTypeEquipmentNeed_HoursOfWork * ((@JobLocationAttribute_Value * 1.0) / @TaskType_Value))
	
	RETURN @Result
END
GO


print '' print '*** Creating JobServicePackge after insert trigger ***'
GO
DROP TRIGGER [dbo].[TRIG_JobServicePackage_AFTERINSERT]
GO
CREATE TRIGGER [dbo].[TRIG_JobServicePackage_AFTERINSERT]
	ON [dbo].[JobServicePackage]
	AFTER INSERT
AS 
	DECLARE @TableID int, @LinesNeeded int
	

	-- Supply Lines
	INSERT INTO [TaskSupply]
	([TaskTypeSupplyNeedID], [SupplyItemID], [Quantity], [JobID])
	(
		SELECT DISTINCT [TaskTypeSupplyNeed].[TaskTypeSupplyNeedID], [TaskTypeSupplyNeed].[SupplyItemID]
		, dbo.fn_calculate_supply_quantity_for_job_supply_need([TaskTypeSupplyNeed].[TaskTypeSupplyNeedID], INSERTED.[JobID]), INSERTED.[JobID]
		FROM [ServicePackage], [ServicePackageOffering], [ServiceOffering], [ServiceOfferingItem],
			[ServiceItem], [Task], [TaskType], [TaskTypeSupplyNeed], INSERTED
		WHERE INSERTED.[ServicePackageID] = [ServicePackage].[ServicePackageID]
		AND [ServicePackage].[ServicePackageID] = [ServicePackageOffering].[ServicePackageID]
		AND [ServicePackageOffering].[ServiceOfferingID] = [ServiceOffering].[ServiceOfferingID]
		AND [ServiceOffering].[ServiceOfferingID] = [ServiceOfferingItem].[ServiceOfferingID]
		AND [ServiceOfferingItem].[ServiceItemID] = [ServiceItem].[ServiceItemID]
		AND [ServiceItem].[ServiceItemID] = [Task].[ServiceItemID]
		AND [Task].[TaskTypeID] = [TaskType].[TaskTypeID]
		AND [TaskType].[TaskTypeID] = [TaskTypeSupplyNeed].[TaskTypeID]
	)
	
	
	-- Employee Lines
	
	-- get initial data
	SELECT DISTINCT [TaskTypeEmployeeNeed].[TaskTypeEmployeeNeedID], [TaskTypeEmployeeNeed].[TaskTypeID], [TaskTypeEmployeeNeed].[HoursOfWork]
	, dbo.fn_calculate_hours_of_work_for_job_employee_need([TaskTypeEmployeeNeed].[TaskTypeEmployeeNeedID], INSERTED.[JobID]) as HoursTotal, INSERTED.[JobID] as JobID
	INTO #TmpTaskEmployeeData
	FROM [ServicePackage], [ServicePackageOffering], [ServiceOffering], [ServiceOfferingItem],
		[ServiceItem], [Task], [TaskType], [TaskTypeEmployeeNeed], INSERTED
	WHERE INSERTED.[ServicePackageID] = [ServicePackage].[ServicePackageID]
	AND [ServicePackage].[ServicePackageID] = [ServicePackageOffering].[ServicePackageID]
	AND [ServicePackageOffering].[ServiceOfferingID] = [ServiceOffering].[ServiceOfferingID]
	AND [ServiceOffering].[ServiceOfferingID] = [ServiceOfferingItem].[ServiceOfferingID]
	AND [ServiceOfferingItem].[ServiceItemID] = [ServiceItem].[ServiceItemID]
	AND [ServiceItem].[ServiceItemID] = [Task].[ServiceItemID]
	AND [Task].[TaskTypeID] = [TaskType].[TaskTypeID]
	AND [TaskType].[TaskTypeID] = [TaskTypeEmployeeNeed].[TaskTypeID]
	
	-- loop through creating a TaskEmployee for each line
	WHILE EXISTS (SELECT * FROM #TmpTaskEmployeeData)
	BEGIN
		
		SELECT TOP 1 @TableID = TaskTypeEmployeeNeedID
		FROM #TmpTaskEmployeeData
		
		SELECT @LinesNeeded = CEILING(HoursTotal / 12.0)
		FROM #TmpTaskEmployeeData
		WHERE TaskTypeEmployeeNeedID = @TableID
		
		WHILE @LinesNeeded > 0
		BEGIN
			INSERT INTO [TaskEmployee]
				([JobID], [TaskTypeEmployeeNeedID], [EmployeeID])	
			(
				SELECT [JobID], [TaskTypeEmployeeNeedID], NULL
				FROM #TmpTaskEmployeeData
				WHERE TaskTypeEmployeeNeedID = @TableID
			)
			SET @LinesNeeded = @LinesNeeded - 1;
		END
		
		DELETE #TmpTaskEmployeeData
		WHERE TaskTypeEmployeeNeedID = @TableID
	END
	
	
	-- Equipment Lines
	
	SELECT DISTINCT [TaskTypeEquipmentNeed].[TaskTypeEquipmentNeedID], [TaskTypeEquipmentNeed].[TaskTypeID], [TaskTypeEquipmentNeed].[HoursOfWork]
	, [TaskTypeEquipmentNeed].[EquipmentTypeID]
	, [dbo].[fn_calculate_hours_of_work_for_job_equipment_need]([TaskTypeEquipmentNeed].[TaskTypeEquipmentNeedID], INSERTED.[JobID]) as HoursTotal, INSERTED.[JobID] as JobID
	INTO #TmpTaskEquipmentData
	FROM [ServicePackage], [ServicePackageOffering], [ServiceOffering], [ServiceOfferingItem],
		[ServiceItem], [Task], [TaskType], [TaskTypeEquipmentNeed], INSERTED
	WHERE INSERTED.[ServicePackageID] = [ServicePackage].[ServicePackageID]
	AND [ServicePackage].[ServicePackageID] = [ServicePackageOffering].[ServicePackageID]
	AND [ServicePackageOffering].[ServiceOfferingID] = [ServiceOffering].[ServiceOfferingID]
	AND [ServiceOffering].[ServiceOfferingID] = [ServiceOfferingItem].[ServiceOfferingID]
	AND [ServiceOfferingItem].[ServiceItemID] = [ServiceItem].[ServiceItemID]
	AND [ServiceItem].[ServiceItemID] = [Task].[ServiceItemID]
	AND [Task].[TaskTypeID] = [TaskType].[TaskTypeID]
	AND [TaskType].[TaskTypeID] = [TaskTypeEquipmentNeed].[TaskTypeID]
	
	WHILE EXISTS (SELECT * FROM #TmpTaskEquipmentData)
	BEGIN
		
		SELECT TOP 1 @TableID = TaskTypeEquipmentNeedID
		FROM #TmpTaskEquipmentData
		
		SELECT @LinesNeeded = CEILING(HoursTotal / 12.0)
		FROM #TmpTaskEquipmentData
		WHERE TaskTypeEquipmentNeedID = @TableID
		
		WHILE @LinesNeeded > 0
		BEGIN
			INSERT INTO [TaskEquipment]
				([JobID], [TaskTypeEquipmentNeedID], [EquipmentID])	
			(
				SELECT [JobID], [TaskTypeEquipmentNeedID], NULL
				FROM #TmpTaskEquipmentData
				WHERE TaskTypeEquipmentNeedID = @TableID
			)
			SET @LinesNeeded = @LinesNeeded - 1;
		END
		
		DELETE #TmpTaskEquipmentData
		WHERE TaskTypeEquipmentNeedID = @TableID
	END
	
	
	-- Create JobTasks
	SELECT DISTINCT [Task].[TaskID], Inserted.[JobID]
	INTO #Tasks
	FROM INSERTED, [ServicePackageOffering], [ServiceOfferingItem], [Task]
	WHERE INSERTED.[ServicePackageID] = [ServicePackageOffering].[ServicePackageID]
	AND [ServicePackageOffering].[ServiceOfferingID] = [ServiceOfferingItem].[ServiceOfferingID]
	AND [ServiceOfferingItem].[ServiceItemID] = [Task].[ServiceItemID]
	
	INSERT INTO [JobTask]
		([JobID], [TaskID])
	(
		SELECT #Tasks.[JobID], #Tasks.[TaskID]
		FROM #Tasks
	)
GO


