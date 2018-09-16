using System.Collections.Generic;
using DataObjects;

namespace Logic
{
	public interface IEmployeeJobPostManager
	{
		bool AcceptJobPosting(int employeeJobPostId, int employeeId);
		bool CreateEmployeeJobPost(int employeeId, int jobId);
		List<EmployeeJobPost> RetreiveJobPostingByEmployeeCertification(int employeeId);
	}
}