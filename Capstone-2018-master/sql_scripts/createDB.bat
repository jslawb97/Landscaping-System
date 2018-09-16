echo off

rem batch file to run a script to create a db
rem 1/19/2018

sqlcmd -S localhost -E -i crlandscaping-setup.sql

sqlcmd -S localhost -E -i crlandscaping-sampledata.sql
rem ******************************
sqlcmd -S localhost -E -i EmployeeCertificationDetail.sql
sqlcmd -S localhost -E -i SupplyOrderAndTaskTypeSupplyNeedSPs.sql
sqlcmd -S localhost -E -i JobLocationAttributeType.sql
sqlcmd -S localhost -E -i InspectionRecordDetail.sql
sqlcmd -S localhost -E -i JobTaskRelationshipChanges-20190104.sql
sqlcmd -S localhost -E -i WorkingEmployeeRoleSPs.sql
sqlcmd -S localhost -E -i ResourceAllocation.sql
sqlcmd -S localhost -E -i EditTaskSupplyQuantity.sql
sqlcmd -S localhost -E -i TaskTypeEquipmentNeed.sql
sqlcmd -S localhost -E -i CreateEmployeeWithoutPasswordHash.sql
sqlcmd -S localhost -E -i EmployeeCertificationEdit.sql
sqlcmd -S localhost -E -i EquipmentByTypeAndAvailability.sql
sqlcmd -S localhost -E -i TaskEmployee.sql
sqlcmd -S localhost -E -i ServiceOfferingItemSPs.sql
sqlcmd -S localhost -E -i SpecialOrderLineCorrections.sql
sqlcmd -S localhost -E -i PopulateAllocatedEquipment.sql
sqlcmd -S localhost -E -i UpdateJobWithTargetWindow.sql
sqlcmd -S localhost -E -i AllocateEmployeeAssignedEmployeeList.sql
sqlcmd -S localhost -E -i TaskTypeEmployeeNeedCRUD.sql
sqlcmd -S localhost -E -i RetrieveTaskEquipmentAllocationDetailListByJobIDUpdated.sql
sqlcmd -S localhost -E -i SupplyOrderReceiving.sql
sqlcmd -S localhost -E -i TaskEquipmentAllocation.sql
sqlcmd -S localhost -E -i SpecialOrderReceiving.sql
sqlcmd -S localhost -E -i RetrieveAllocatedEquipmentByTaskAndJobID.sql
sqlcmd -S localhost -E -i RemoveAllAssignedEquipment.sql
sqlcmd -S localhost -E -i SpecialOrderRefactor.sql
sqlcmd -S localhost -E -i EditCustomerByIDWithCustomerTypeID.sql
sqlcmd -S localhost -E -i ResupplyOrder.sql
sqlcmd -S localhost -E -i SupplyOrderRefactor.sql
sqlcmd -S localhost -E -i ResupplyOrderReceiving.sql
sqlcmd -S localhost -E -i RetrieveCustomerByEmail.sql
sqlcmd -S localhost -E -i JobCreationRefactor_20180423.sql
sqlcmd -S localhost -E -i ResourceAllocationRefactor_20180424.sql
sqlcmd -S localhost -E -i RetrieveMakeModelDetail.sql
sqlcmd -S localhost -E -i RetrieveMaintenanceRecordDetailList.sql
sqlcmd -S localhost -E -i SupplyItemReorderFunctionality.sql
sqlcmd -S localhost -E -i UpdatedTaskTypeSupplyNeed.sql
sqlcmd -S localhost -E -i EmployeeEquipmentAssignmentScript.sql
sqlcmd -S localhost -E -i OrderLineReceivedTriggers.sql
sqlcmd -S localhost -E -i RetrievePrepRecordDetail.sql
sqlcmd -S localhost -E -i EditEquipmentFixed.sql
sqlcmd -S localhost -E -i EmployeeJobBoard.sql
sqlcmd -S localhost -E -i RetrieveEmployeeRolesByActive.sql
sqlcmd -S localhost -E -i EditPrepRecord.sql
sqlcmd -S localhost -E -i RetrieveEquipmentTypeDetail.sql

ECHO .
ECHO if no error messages appear DB was created
PAUSE