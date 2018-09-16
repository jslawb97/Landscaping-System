using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess;
using DataObjects;

namespace DataAccessMocks
{
    /// <summary>
    /// Zachary Hall
    /// Created 2018/03/10
    /// 
    /// Accessor Mock for Job Location
    /// </summary>
    public class JobLocationAccessorMock : IJobLocationAccessor
    {

        private List<JobLocation> _jobLocations;
        private List<JobLocationDetail> _jobLocationDetails;

        private List<JobLocationAttribute> _jobServicePackageAttributesList;

        public JobLocationAccessorMock()
        {
            _jobLocations = new List<JobLocation>();
            _jobLocationDetails = new List<JobLocationDetail>();

            _jobServicePackageAttributesList = new List<JobLocationAttribute>();
            _jobServicePackageAttributesList.Add(new JobLocationAttribute {
                JobLocationAttributeTypeID = "Acres to Sod",
                Value = 0

            });

            _jobServicePackageAttributesList.Add(new JobLocationAttribute
            {
                JobLocationAttributeTypeID = "Trees to Trim",
                Value = 0
            });

            _jobLocationDetails.Add(new JobLocationDetail {

                JobLocation = new JobLocation
                {
                    CustomerID = 1000000,
                    Street = "123 Main St",
                    City = "Cedar Rapids",
                    State = "IA",
                    ZipCode = "52404",
                    Comments = "Test comment",
                    Active = true
                },
                JobLocationAttributes = new List<JobLocationAttribute>
                {
                    new JobLocationAttribute
                    {
                        JobLocationID = 1000000,
                        JobLocationAttributeTypeID = "Acres to Mow",
                        Value = 5
                    },
                    new JobLocationAttribute
                    {
                        JobLocationID = 1000000,
                        JobLocationAttributeTypeID = "Acres to Sod",
                        Value = 3
                    }
                }

            });

            _jobLocations.Add(_jobLocationDetails.ElementAt(0).JobLocation);
            _jobLocations.Add(new JobLocation {

                JobLocationID = 1000001,
                CustomerID = 1000001,
                Street = "123 Main St",
                City = "Cedar Rapids",
                State = "IA",
                ZipCode = "52404",
                Comments = "Test comment 2",
                Active = true
            });
        }

        /// <summary>
        /// Zachary Hall
        /// Created 2018/03/10
        /// 
        /// Adds a JobLocation record to the data store
        /// </summary>
        /// <param name="jobLocation"></param>
        /// <returns></returns>
        public int CreateJobLocation(JobLocation jobLocation)
        {
            int jlID = 0;

            try
            {
                if(_jobLocations == null)
                {
                    throw new ApplicationException("Data store unaccessable");
                }
                jlID = Constants.IDSTARTVALUE + _jobLocations.Count + 1;
                jobLocation.JobLocationID = jlID;
                _jobLocations.Add(jobLocation);
            }
            catch (Exception)
            {

                throw;
            }

            return jlID;
        }

        /// <summary>
        /// Zachary Hall
        /// Created 2018/03/10
        /// 
        /// Merges the given list of JobLocationAttributes records into the data store
        /// </summary>
        /// <param name="jobLocationAttributes"></param>
        /// <returns></returns>
        public int CreateUpdateJobLocationAttributes(IEnumerable<JobLocationAttribute> jobLocationAttributes)
        {
            int rowsAffected = 0;
            try
            {
                if(jobLocationAttributes == null)
                {
                    throw new ApplicationException("List is null");
                }

                foreach (var jla in jobLocationAttributes)
                {
                    foreach (var jld in _jobLocationDetails)
                    {
                        if(jla.JobLocationID == jld.JobLocation.JobLocationID)
                        {
                            bool insert = true;
                            foreach (var jld_jla in jld.JobLocationAttributes)
                            {
                                if(jld_jla.JobLocationAttributeTypeID == jla.JobLocationAttributeTypeID)
                                {
                                    jld_jla.Value = jla.Value;
                                    insert = false;
                                    rowsAffected++;
                                }
                            }
                            if (insert == false)
                            {
                                jld.JobLocationAttributes.Add(jla);

                                rowsAffected++;
                            }
                        }
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }

            return rowsAffected;
        }

        /// <summary>
        /// Zachary Hall
        /// Created 2018/03/10
        /// 
        /// Gets a list of JobLocationAttribute records associated with a given JobLocation's id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public List<JobLocationAttribute> RetrieveJobLocationAttributeListByJobLocationID(int id)
        {
            try
            {
                foreach (var jld in _jobLocationDetails)
                {
                    if(jld.JobLocation.JobLocationID == id)
                    {
                        return jld.JobLocationAttributes;
                    }
                }
                throw new ApplicationException("Job location does not exist");
            }
            catch (Exception)
            {

                throw;
            }
            
        }

        /// <summary>
        /// Zachary Hall
        /// Created 2018/03/10
        /// 
        /// Gets a list of JobLocationAttributes based on the need of a given ServicePackage
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public List<JobLocationAttribute> RetrieveJobLocationAttributeListByServicePackageID(int id)
        {
            return _jobServicePackageAttributesList;
        }

        /// <summary>
        /// Zachary Hall
        /// Created 2018/03/10
        /// 
        /// Gets the JobLocation record with the given id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public JobLocation RetrieveJobLocationByID(int id)
        {
            try
            {
                foreach (var jl in _jobLocations)
                {
                    if(jl.JobLocationID == id)
                    {
                        return jl;
                    }


                }
                throw new ApplicationException("Job location does not exist");
            }
            catch (Exception)
            {

                throw;
            }
        }

        /// <summary>
        /// Zachary Hall
        /// Created 2018/03/10
        /// 
        /// gets the job detail based on the given job location id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public JobLocationDetail RetrieveJobLocationDetailByID(int id)
        {
            try
            {
                foreach (var jld in _jobLocationDetails)
                {
                    if(jld.JobLocation.JobLocationID == id)
                    {
                        return jld;
                    }
                }
                throw new ApplicationException("Job Location Detail does not exist");
            }
            catch (Exception)
            {

                throw;
            }
        }

        /// <summary>
        /// Zachary Hall
        /// Created 2018/03/10
        /// 
        /// Gets the Job Locations associated with the given customer id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public List<JobLocation> RetrieveJobLocationListByCustomerID(int id)
        {
            List<JobLocation> jobLocations = new List<JobLocation>();
            try
            {
                foreach (var jl in _jobLocations)
                {
                    if(jl.CustomerID == id)
                    {
                        jobLocations.Add(jl);
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }

            return jobLocations;
        }

        /// <summary>
        /// Zachary Hall
        /// Created 2018/03/10
        /// 
        /// Updates the given job location with data from the newJobLocation
        /// </summary>
        /// <param name="oldJobLocation"></param>
        /// <param name="newJobLocation"></param>
        /// <returns></returns>
        public int UpdateJobLocation(JobLocation oldJobLocation, JobLocation newJobLocation)
        {
            int rowsAffected = 0;

            try
            {
                foreach (var jl in _jobLocations)
                {
                    if(jl.JobLocationID == oldJobLocation.JobLocationID)
                    {
                        jl.CustomerID = newJobLocation.CustomerID;
                        jl.Comments = newJobLocation.Comments;
                        jl.Street = newJobLocation.Street;
                        jl.City = newJobLocation.City;
                        jl.State = newJobLocation.State;
                        jl.ZipCode = newJobLocation.ZipCode;
                        jl.Active = jl.Active;
                        rowsAffected++;
                    }
                }
                if(rowsAffected == 0)
                {
                    throw new ApplicationException("The Job Location was not updated");
                }
            }
            catch (Exception)
            {

                throw;
            }


            return rowsAffected;
        }

        /// <summary>
        /// Zachary Hall
        /// Created 2018/03/10
        /// 
        /// Gets a list of JobLocationDetails
        /// </summary>
        /// <returns></returns>
        public List<JobLocationDetail> RetrieveJobLocationDetailList()
        {
            return _jobLocationDetails;
        }

        public int DeactivateJobLocationByID(int id)
        {
            throw new NotImplementedException();
        }

        public List<JobLocationAttribute> RetrieveJobLocationAttributeList()
        {
            throw new NotImplementedException();
        }
    }
}
