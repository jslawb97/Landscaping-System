using System.Collections.Generic;
using DataObjects;

namespace DataAccess
{
	public interface IEmployeeJobPostAccessor
	{
		int AcceptJobPosting(int employeeJobPostId, int employeeId);
		int CreateEmployeeJobPost(int employeeId, int jobId);
		List<EmployeeJobPost> RetreiveJobPostingByEmployeeCertification(int employeeId);
	}
}