/* Mike Mason */
print '' print '*** in file EmployeeCertificationDetail.sql ***'
USE [crlandscaping]
GO
print '' print '*** Creating sp_retrieve_employee_certificationdetails_list'
GO
CREATE PROCEDURE [dbo].[sp_retrieve_employeecertificationdetails_list]
AS
		BEGIN
			SELECT Employee.EmployeeID,Employee.Email, Certification.CertificationID,
			Certification.Name, EmployeeCertification.EndDate, EmployeeCertification.Active
			FROM   Employee, EmployeeCertification, Certification
			WHERE Employee.EmployeeID = EmployeeCertification.EmployeeID
			AND EmployeeCertification.CertificationID = Certification.CertificationID
		END
GO