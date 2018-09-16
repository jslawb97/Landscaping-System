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
    /// Created 2018/03/29
    /// 
    /// The accessor mock for TaskTypeEmployeeNeed
    /// </summary>
    public class TaskTypeEmployeeNeedAccessorMock : ITaskTypeEmployeeNeedAccessor
    {
        private List<TaskTypeEmployeeNeedDetail> _detailList;
        private List<TaskType> _taskTypeList;
        private List<TaskTypeEmployeeNeed> _taskTypeEmployeeNeedList;

        public TaskTypeEmployeeNeedAccessorMock()
        {
            _taskTypeEmployeeNeedList = new List<TaskTypeEmployeeNeed>()
            {
                new TaskTypeEmployeeNeed()
                {
                    TaskTypeID = Constants.IDSTARTVALUE,
                    HoursOfWork = 5,
                    Active = true
                },
                new TaskTypeEmployeeNeed()
                {
                    TaskTypeID = Constants.IDSTARTVALUE + 1,
                    HoursOfWork = 6,
                    Active = true
                },
                new TaskTypeEmployeeNeed()
                {
                    TaskTypeID = Constants.IDSTARTVALUE + 2,
                    HoursOfWork = 1,
                    Active = false
                }
            };

            _taskTypeList = new List<TaskType>()
            {
                new TaskType()
                {
                    TaskTypeID = Constants.IDSTARTVALUE,
                    Name = "Test task 1",
                    Quantity = 1,
                    JobLocationAttributeTypeID = "TEST1",
                    Active = true
                },
                new TaskType()
                {
                    TaskTypeID = Constants.IDSTARTVALUE + 1,
                    Name = "Test task 2",
                    Quantity =2,
                    JobLocationAttributeTypeID = "TEST2",
                    Active = true
                },
                new TaskType()
                {
                    TaskTypeID = Constants.IDSTARTVALUE + 2,
                    Name = "Test task 3",
                    Quantity = 3,
                    JobLocationAttributeTypeID = "TEST3",
                    Active = false
                },
                new TaskType()
                {
                    TaskTypeID = Constants.IDSTARTVALUE + 3,
                    Name = "Test task 4",
                    Quantity = 3,
                    JobLocationAttributeTypeID = "TEST4",
                    Active = false
                }
            };
        }

        /// <summary>
        /// Zachary Hall
        /// Created 2018/03/29
        /// 
        /// Validates the objects
        /// </summary>
        /// <param name="need"></param>
        private void validateTaskTypeEmployeeNeed(TaskTypeEmployeeNeed need)
        {
            if (need == null)
            {
                throw new ApplicationException("The TaskTypeEmployeeNeed object was null");
            }

            if (need.TaskTypeID < Constants.IDSTARTVALUE)
            {
                throw new ArgumentOutOfRangeException("The TaskTypeEmployeeNeed object's TaskTypeID field is invalid");
            }

            if (need.HoursOfWork <= 0)
            {
                throw new ApplicationException("The hours must be greater than zero");
            }
        }


        /// <summary>
        /// Zachary Hall
        /// Created 2018/03/29
        /// 
        /// Mocks adding an employee need to a data store
        /// </summary>
        /// <param name="need"></param>
        /// <returns></returns>
        public int CreateTaskTypeEmployeeNeed(TaskTypeEmployeeNeed need)
        {
            int rowsAffected = 0;

            try
            {
                validateTaskTypeEmployeeNeed(need);
                _taskTypeEmployeeNeedList.Add(need);
                rowsAffected++;
            }
            catch (Exception ex)
            {

                throw ex;
            }
            return rowsAffected;
        }

        /// <summary>
        /// Zachary Hall
        /// Created 2018/03/29
        /// 
        /// Mocks deactivating a employee need record in the data store
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public int DeactivateTaskTypeEmployeeNeedByID(int id)
        {
            int rowsAffected = 0;
            if(id < Constants.IDSTARTVALUE)
            {
                throw new ArgumentOutOfRangeException("The id is invalid");
            }
            foreach (var item in _taskTypeEmployeeNeedList)
            {
                if(item.TaskTypeID == id)
                {
                    item.Active = false;
                    rowsAffected++;
                    break;
                }
                
            }

            return rowsAffected;
        }

        /// <summary>
        /// Zachary Hall
        /// Created 2018/03/29
        /// 
        /// Mocks getting a list of all the employee need detail objects from a database
        /// </summary>
        /// <returns></returns>
        public List<TaskTypeEmployeeNeedDetail> RetrieveTaskTypeEmployeeDetailList()
        {
            _detailList = new List<TaskTypeEmployeeNeedDetail>();

            foreach (var item in _taskTypeEmployeeNeedList)
            {
                foreach (var taskType in _taskTypeList)
                {
                    if(taskType.TaskTypeID == item.TaskTypeID)
                    {
                        var detail = new TaskTypeEmployeeNeedDetail
                        {
                            TaskType = taskType,
                            TaskTypeEmployeeNeed = item
                        };
                        _detailList.Add(detail);
                    }
                }

                
            }
            
            return _detailList;
        }

        /// <summary>
        /// Zachary Hall
        /// Created 2018/03/29
        /// 
        /// Mocks editing a TaskTypeEMployeeNeed record in a data store
        /// </summary>
        /// <param name="oldNeed"></param>
        /// <param name="newNeed"></param>
        /// <returns></returns>
        public int UpdateTaskTypeEmployeeNeed(TaskTypeEmployeeNeed oldNeed, TaskTypeEmployeeNeed newNeed)
        {
            int rowsAffected = 0;

            validateTaskTypeEmployeeNeed(oldNeed);
            validateTaskTypeEmployeeNeed(newNeed);
            foreach (var item in _taskTypeEmployeeNeedList)
            {
                if(oldNeed.TaskTypeID == item.TaskTypeID)
                {
                    item.HoursOfWork = newNeed.HoursOfWork;
                    item.Active = newNeed.Active;
                    rowsAffected++;
                }
            }

            return rowsAffected;
        }
    }
}
