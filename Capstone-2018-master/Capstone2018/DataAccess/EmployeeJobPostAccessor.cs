using DataObjects;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
	/// <summary>
	/// Zachary Hall
	/// Created 2018/05/04
	/// </summary>
    /// QA ShilinXiong 5/4/18 Add,Updated,Delete EmployeeCertification</remark>
	public class EmployeeJobPostAccessor : IEmployeeJobPostAccessor
	{
		/// <summary>
		/// Zachary Hall
		/// Created 2018/05/04
		/// 
		/// Submits a Employee Job Post record 
		/// </summary>
		/// <param name="employeeId">The employee posting the job post</param>
		/// <param name="jobId">The job being posted</param>
		/// <returns></returns>
		public int CreateEmployeeJobPost(int employeeId, int jobId)
		{
			int result = 0;

			var conn = DBConnection.GetDBConnection();
			var cmdText = @"sp_create_employee_job_post";
			var cmd = new SqlCommand(cmdText, conn);
			cmd.Parameters.AddWithValue("@EmployeeID", employeeId);
			cmd.Parameters.AddWithValue("@JobID", jobId);

			cmd.CommandType = CommandType.StoredProcedure;

			try
			{
				conn.Open();
				result = cmd.ExecuteNonQuery();
			}
			catch (Exception ex)
			{

				throw new ApplicationException("There was a problem creating the job posting.", ex);

			}
			finally
			{
				conn.Close();
			}
			
			return result;
		}

		/// <summary>
		/// Zachary Hall
		/// Created 2018/05/04
		/// 
		/// Accepts a Employee Job Post record
		/// </summary>
		/// <param name="employeeJobPostId">The job post being accepted</param>
		/// <param name="employeeId">The employee accepting the job post</param>
		/// <returns></returns>
		public int AcceptJobPosting(int employeeJobPostId, int employeeId)
		{
			int result = 0;

			var conn = DBConnection.GetDBConnection();
			var cmdText = @"sp_employee_accept_employee_job_post";
			var cmd = new SqlCommand(cmdText, conn);
			cmd.Parameters.AddWithValue("@EmployeeJobPostID", employeeJobPostId);
			cmd.Parameters.AddWithValue("@EmployeeID", employeeId);

			cmd.CommandType = CommandType.StoredProcedure;

			try
			{
				conn.Open();
				result = cmd.ExecuteNonQuery();
			}
			catch (Exception ex)
			{

				throw new ApplicationException("There was a problem accepting the job posting.", ex);

			}
			finally
			{
				conn.Close();
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
			var jobPostings = new List<EmployeeJobPost>();

			var conn = DBConnection.GetDBConnection();
			var cmdText = @"sp_retreive_employee_job_post_by_employee_certification";
			var cmd = new SqlCommand(cmdText, conn);
			cmd.Parameters.AddWithValue("@EmployeeID", employeeId);
			cmd.CommandType = CommandType.StoredProcedure;

			try
			{
				conn.Open();
				var reader = cmd.ExecuteReader();

				if (reader.HasRows)
				{
					while (reader.Read())
					{
						var employeeJobPost = new EmployeeJobPost()
						{
							EmployeeJobPostID = reader.GetInt32(0),
							PostingEmployeeID = reader.GetInt32(1),
							PostingEmployeeFirstName = reader.GetString(2),
							PostingEmployeeLastName = reader.GetString(3),
							JobLocationStreet = reader.GetString(4),
							JobLocationCity = reader.GetString(5),
							JobLocationState = reader.GetString(6),
							JobLocationZipCode = reader.GetString(7),
							JobScheduled = reader.GetDateTime(8)
						};
						jobPostings.Add(employeeJobPost);
					}
				}
			}
			catch (Exception ex)
			{

				throw new ApplicationException("There was a problem retrieving your data.", ex);

			}
			finally
			{
				conn.Close();
			}

			return jobPostings;
		}
	}

}
