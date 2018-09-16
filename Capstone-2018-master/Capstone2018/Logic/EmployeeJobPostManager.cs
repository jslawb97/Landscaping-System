using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess;
using DataObjects;

namespace Logic
{

	/// <summary>
	/// Zachary Hall
	/// Created 2018/05/04
	/// </summary>
	public class EmployeeJobPostManager : IEmployeeJobPostManager
	{

		private IEmployeeJobPostAccessor _employeeJobPostAccessor;

		public EmployeeJobPostManager()
		{
			_employeeJobPostAccessor = new EmployeeJobPostAccessor();
		}

		public EmployeeJobPostManager(IEmployeeJobPostAccessor employeeJobPostAccessor)
		{
			_employeeJobPostAccessor = employeeJobPostAccessor;
		}

		/// <summary>
		/// Zachary Hall
		/// Created 2018/05/04
		/// 
		/// Accepts a Employee Job Post record
		/// </summary>
		/// <param name="employeeJobPostId"></param>
		/// <param name="employeeId"></param>
		/// <returns></returns>
		public bool AcceptJobPosting(int employeeJobPostId, int employeeId)
		{
			var result = true;

			try
			{
				int acceptResult = _employeeJobPostAccessor.AcceptJobPosting(employeeJobPostId, employeeId);
				if(acceptResult != 1)
				{
					result = false;
				}
			}
			catch (Exception)
			{

				throw;
			}
			
			return result;
		}

		/// <summary>
		/// Zachary Hall
		/// Created 2018/05/04
		/// 
		/// Submits a Employee Job Post record
		/// </summary>
		/// <param name="employeeId"></param>
		/// <param name="jobId"></param>
		/// <returns></returns>
		public bool CreateEmployeeJobPost(int employeeId, int jobId)
		{
			var result = true;

			try
			{
				int acceptResult = _employeeJobPostAccessor.CreateEmployeeJobPost(employeeId, jobId);
				if (acceptResult != 1)
				{
					result = false;
				}
			}
			catch (Exception)
			{

				throw;
			}

			return result;
		}

		/// <summary>
		/// Zachary Hall
		/// Created 2018/05/04
		/// 
		/// Gets a list of Job postings
		/// </summary>
		/// <param name="employeeId">The employee asking for the list. Filters based on their certifications.</param>
		/// <returns></returns>
		public List<EmployeeJobPost> RetreiveJobPostingByEmployeeCertification(int employeeId)
		{
			List<EmployeeJobPost> jobPosts = new List<EmployeeJobPost>();

			try
			{
				jobPosts = _employeeJobPostAccessor.RetreiveJobPostingByEmployeeCertification(employeeId);
			}
			catch (Exception)
			{

				throw;
			}


			return jobPosts;
		}
	}
}
