using DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataObjects;

namespace Logic
{
    /// <summary>
    /// Brady Feller
    /// Created 2018/02/19
    /// </summary>
    public class SourceManager : ISourceManager
    {
        private ISourceAccessor _sourceAccessor;


        /// <summary>
        /// Brady Feller
        /// Created 2018/02/20
        /// default constructor
        /// </summary>
        public SourceManager()
        {
            _sourceAccessor = new SourceAccessor();
        }

        /// <summary>
        /// Brady Feller
        /// Created 2018/02/20
        /// 
        /// Unit testing constructor
        /// </summary>
        /// <param name="sourceAccessor"></param>
        public SourceManager(ISourceAccessor sourceAccessor)
        {
            _sourceAccessor = sourceAccessor;
        }

        /// <summary>
        /// Jayden Tollefson
        /// Created: 2018/02/09
        /// 
        /// Calls the accessor to add a Special Order Item to the database
        /// </summary>
        /// <param name="newItem">The item to be added</param>
        /// <returns>The newly created items id</returns>
        public int AddSource(Source source)
        {
            int result = 0;

            try
            {
                result = _sourceAccessor.CreateSource(source);
            }
            catch (Exception)
            {

                throw;
            }

            return result;
        }

        /// <summary>
        /// Brady Feller
        /// Created 2018/02/19
        /// 
        /// Deacativates a source by the ID
        /// </summary>
        /// <param name="sourceID"></param>
        /// <returns></returns>
        public bool DeactivateSource(int sourceID)
        {
            int result = 0;

            try
            {
                result = _sourceAccessor.DeactivateSourceByID(sourceID);
            }
            catch (Exception)
            {
                throw;
            }
            if (result == 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Jayden Tollefson
        /// Created: 2018/02/09
        /// 
        /// Edits the old source item with data from the new source item.
        /// </summary>
        /// <param name="oldSource">The item being edited</param>
        /// <param name="source">The item with the new data</param>
        /// <returns>The number of records affected</returns>
        public int EditSource(Source oldSource, Source source)
        {
            int result = 0;

            try
            {
                result = _sourceAccessor.EditSource(oldSource, source);
            }
            catch (Exception)
            {

                throw;
            }

            return result;
        }

        /// <summary>
        /// Jayden Tollefson
        /// Created: 2018/02/02
        /// 
        /// Retrieve a list of Sources from an Accessor
        /// </summary>
        /// <returns>A list of Source objects</returns>
        public List<Source> RetrieveSource()
        {
            var items = new List<Source>();

            try
            {
                items = _sourceAccessor.RetrieveSourceByActive();
            }
            catch (Exception)
            {
                throw;
            }

            return items;
        }

        public List<SourceDetail> RetrieveSourceDetailList()
        {
            var sourceDetailList = new List<SourceDetail>();

            try
            {
                sourceDetailList = _sourceAccessor.RetrieveSourceDetailList();
            }
            catch (Exception)
            {
                throw;
            }

            return sourceDetailList;
        }


        /// <summary>
        /// Mike Mason
        /// Created: 2018/02/02
        /// 
        /// Retrieve a source by id
        /// </summary>
        /// <returns>A source object</returns>
        public Source RetrieveSourceById(int sourceId)
        {
            Source sourceList = null;

            try
            {
                sourceList = _sourceAccessor.RetrieveSourceByID(sourceId);
            }
            catch (Exception)
            {
                throw;
            }
            return sourceList;
        }
    }
}
