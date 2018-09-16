using DataAccess;
using DataObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessMocks
{
    public class SourceAccessorMock : ISourceAccessor
    {
        private List<Source> _sourceList = new List<Source>();

        public SourceAccessorMock()
        {
            _sourceList.Add(new Source()
            {
                SourceID = 1000000,
                SupplyItemID = 1,
                SpecialOrderItemID = 1,
                VendorID = 1,
                MinimumOrderQTY = 1,
                PriceEach = 1.99M,
                LeadTime = 1,
                Active = true
            });
            _sourceList.Add(new Source()
            {
                SourceID = 1000001,
                SupplyItemID = 1,
                SpecialOrderItemID = 1,
                VendorID = 1,
                MinimumOrderQTY = 1,
                PriceEach = 1.99M,
                LeadTime = 1,
                Active = true
            });
            _sourceList.Add(new Source()
            {
                SourceID = 1000002,
                SupplyItemID = 1,
                SpecialOrderItemID = 1,
                VendorID = 1,
                MinimumOrderQTY = 1,
                PriceEach = 1.99M,
                LeadTime = 1,
                Active = false
            });
        }

        public int CreateSource(Source source)
        {
            throw new NotImplementedException();
        }

        public int DeactivateSourceByID(int sourceID)
        {
            int result = 0;

            foreach (Source source in _sourceList)
            {
                if (source.SourceID == sourceID)
                {
                    source.Active = false;
                    result++;
                    break;
                }
            }

            return result;
        }

        /// <summary>
        /// Badis Saidani
        /// Created: 2018/04/17
        /// 
        /// Edits an existing source for changes in the source
        /// </summary>
        /// <param name="newSource">The item containing the new data</param>
        /// <param name="oldSource">The item being edited</param>
        /// <returns>The number of records affected</returns>
        public int EditSource(Source newSource, Source oldSource)
        {
            int rowsAffected = 0;
            try
            {

                foreach (var c in _sourceList)
                {
                    if (c.SourceID == oldSource.SourceID)
                    {
                        c.SourceID = newSource.SourceID;

                        rowsAffected++;
                    }
                }


                if (rowsAffected == 0)
                {
                    throw new ApplicationException("The Source was not updated");

                }
            }
            catch (Exception)
            {

                throw;
            }

            return rowsAffected;
        }

        /// <summary>
        /// Mike Mason
        /// Created on 2018/04/19
        /// 
        /// Method to return mock data
        /// </summary>
        /// <param name="sourceID"></param>
        /// <returns></returns>
        public Source RetrieveSourceByID(int sourceID)
        {
            Source source = null;
            foreach (var src in _sourceList)
            {
                if (src.SourceID == sourceID)
                {
                    source = src;
                }
            }
            if (source == null)
            {
                throw new ApplicationException("Source not found.");
            }
            return source;
        }



        /// <summary>
        /// Mike Mason
        /// Created on 2018/04/19
        /// 
        /// Method to return mock data
        /// </summary>
        /// <param name="active"></param>
        /// <returns></returns>
        public List<Source> RetrieveSourceByActive(bool active = true)
        {
            List<Source> sourceList = new List<Source>();

            foreach (Source source in _sourceList)
            {
                if (source.Active == active)
                {
                    sourceList.Add(source);
                }
            }

            return sourceList;
        }


        public List<SourceDetail> RetrieveSourceDetailList()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Mike Mason
        /// Created 2018/04/19
        /// 
        /// Mock method to retrieve all Sources
        /// </summary>
        /// <returns></returns>
        public List<Source> RetrieveSource()
        {
            return _sourceList;
        }
    }
}
