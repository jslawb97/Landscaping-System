using DataObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    /// <summary>
    /// Brady Feller
    /// Created 2018/02/20
    /// </summary>
    public interface ISourceAccessor
    {
        int DeactivateSourceByID(int sourceID);

        /// <summary>
        /// Jayden Tollefson
        /// Created: 2018/02/02
        /// 
        /// Retrieves a list of Sources
        /// </summary>
        /// <returns>A list of Sources from the database</returns>
        List<Source> RetrieveSourceByActive(bool active = true);

        /// <summary>
        /// Jayden Tollefson
        /// Created: 2018/02/09
        /// 
        /// Creates a new source item
        /// </summary>
        /// <param name="source">The new source item</param>
        /// <returns>The ID of the new source item</returns>
        int CreateSource(Source source);

        /// <summary>
        /// Jayden Tollefson
        /// Created: 2018/02/09
        /// 
        /// Edits an existing source for changes in the source
        /// </summary>
        /// <param name="oldSource">The item being edited</param>
        /// <param name="source">The item containing the new data</param>
        /// <returns>The number of records affected</returns>
        int EditSource(Source source, Source oldSource);

        /// <summary>
        /// Jayden Tollefson
        /// Created 3/9/2018
        /// 
        /// Retrieve a list of source details
        /// </summary>
        /// <returns></returns>
        List<SourceDetail> RetrieveSourceDetailList();


        /// <summary>
        /// Mike Mason
        /// Created 2018/04/19
        /// 
        /// Retrieve source by sourceID
        /// </summary>
        /// <returns></returns>
        Source RetrieveSourceByID(int sourceID);
    }
}
