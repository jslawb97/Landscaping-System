using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess;
using DataObjects;

namespace DataAccessMocks
{
    public class ResupplyOrderLineAccessorMock : IResupplyOrderLineAccessor
    {
        private List<ResupplyOrderLine> _resupplyOrderLineList = new List<ResupplyOrderLine>();

        /// <summary>
        /// Weston Olund
        /// 2018/04/05
        /// Constructor to add data to resupply order line list
        /// </summary>
        public ResupplyOrderLineAccessorMock()
        {
            _resupplyOrderLineList.Add(new ResupplyOrderLine()
            {
                ResupplyOrderLineID = Constants.IDSTARTVALUE,
                ResupplyOrderID = Constants.IDSTARTVALUE,
                SupplyItemID = Constants.IDSTARTVALUE,
                Quantity = 5,
                Price = 100M
            });
            _resupplyOrderLineList.Add(new ResupplyOrderLine()
            {
                ResupplyOrderLineID = Constants.IDSTARTVALUE + 1,
                ResupplyOrderID = Constants.IDSTARTVALUE + 1,
                SupplyItemID = Constants.IDSTARTVALUE + 1,
                Quantity = 10,
                Price = 65M
            });
        }

        /// <summary>
        /// Weston Olund
        /// 2018/04/05
        /// Method to return mock data
        /// </summary>
        /// <param name="resupplyOrderLine"></param>
        /// <returns></returns>
        public int CreateResupplyOrderLine(ResupplyOrderLine resupplyOrderLine)
        {
            if (resupplyOrderLine.ResupplyOrderID == Constants.IDSTARTVALUE * 500)
            {
                throw new ApplicationException("Database error");
            }
            else return 1;
        }

        /// <summary>
        /// Weston Olund
        /// 2018/04/05
        /// Method to return mock data
        /// </summary>
        /// <param name="resupplyOrderLineID"></param>
        /// <returns></returns>
        public int DeleteResupplyOrderLineByID(int resupplyOrderLineID)
        {
            int result = 0;

            foreach (var item in _resupplyOrderLineList)
            {
                if (item.ResupplyOrderLineID == resupplyOrderLineID)
                {
                    result++;
                }
            }
            if (result == 0)
            {
                throw new ApplicationException("Database Error");
            }
            return result;
        }

        /// <summary>
        /// Weston Olund
        /// 2018/05/04
        /// Method to return mock data
        /// </summary>
        /// <param name="resupplyOrderID"></param>
        /// <returns></returns>
        public int DeleteResupplyOrderLineByResupplyOrderID(int resupplyOrderID)
        {
            int result = 0;

            foreach (var item in _resupplyOrderLineList)
            {
                if(item.ResupplyOrderID == resupplyOrderID)
                {
                    result++;
                }
            }
            if(result == 0)
            {
                throw new ApplicationException("Database Error");
            }
            return result;
        }

        /// <summary>
        /// Weston Olund
        /// 2018/4/05
        /// Method to return mock data
        /// </summary>
        /// <param name="oldResupplyOrderLineDetail"></param>
        /// <param name="newResupplyOrderLineDetail"></param>
        /// <returns></returns>
        public int EditResupplyOrderLine(ResupplyOrderLineDetail oldResupplyOrderLineDetail, ResupplyOrderLineDetail newResupplyOrderLineDetail)
        {
            var rowsAffected = 0;
            foreach (var item in _resupplyOrderLineList)
            {
                if (oldResupplyOrderLineDetail.ResupplyOrderLineID == item.ResupplyOrderLineID
                    && newResupplyOrderLineDetail.ResupplyOrderLineID == item.ResupplyOrderLineID)
                {
                    rowsAffected++;
                }
            }
            if (rowsAffected == 0)
            {
                throw new ApplicationException("No rows affected by edit");
            }
            return rowsAffected;
        }

        public int EditResupplyOrderLineQtyReceivedByID(int id, int oldQtyReceived, int newQtyReceived)
        {
            throw new NotImplementedException();
        }

        public int EditResupplyOrderLinesQtyReceivedToQtyOrderedByID(int id)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Weston Olund
        /// 2018/04/05
        /// Method to return mock data
        /// </summary>
        /// <param name="resupplyID"></param>
        /// <returns></returns>
        public List<ResupplyOrderLineDetail> RetrieveResupplyOrderLineDetailListByResupplyOrderID(int resupplyID)
        {
            List<ResupplyOrderLineDetail> resupplyOrderLineDetailList = new List<ResupplyOrderLineDetail>();
            foreach (var item in _resupplyOrderLineList)
            {
                if (item.ResupplyOrderID == resupplyID)
                {
                    resupplyOrderLineDetailList.Add(new ResupplyOrderLineDetail()
                    {
                        ResupplyOrderID = resupplyID,
                        ResupplyOrderLineID = Constants.IDSTARTVALUE,
                        SupplyItemID = Constants.IDSTARTVALUE,
                        NameOfItem = "new item",
                        Price = 50M,
                        Quantity = 5

                    });
                }
                if (resupplyOrderLineDetailList.Count <= 0)
                {
                    throw new ApplicationException("No data found");
                }
            }
            return resupplyOrderLineDetailList;
        }

        public List<ResupplyOrderLineDetail> RetrieveResupplyOrderLineDetailListByResupplyOrderIDWithReceived(int resupplyOrderID)
        {
            List<ResupplyOrderLineDetail> resupplyOrderLineDetailList = new List<ResupplyOrderLineDetail>();
            foreach (var item in _resupplyOrderLineList)
            {
                if (item.ResupplyOrderID == resupplyOrderID)
                {
                    resupplyOrderLineDetailList.Add(new ResupplyOrderLineDetail()
                    {
                        ResupplyOrderID = resupplyOrderID,
                        ResupplyOrderLineID = Constants.IDSTARTVALUE,
                        SupplyItemID = Constants.IDSTARTVALUE,
                        NameOfItem = "new item",
                        Price = 50M,
                        Quantity = 5

                    });
                }
                if (resupplyOrderLineDetailList.Count <= 0)
                {
                    throw new ApplicationException("No data found");
                }
            }
            return resupplyOrderLineDetailList;
        }
    }
}
