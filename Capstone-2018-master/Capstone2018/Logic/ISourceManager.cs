using DataObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic
{
    /// <summary>
    /// Brady Feller
    /// Created 2018/02/20
    /// </summary>
    public interface ISourceManager
    {
        bool DeactivateSource(int sourceID);

        /// <summary>
        /// Zachary Hall
        /// Created: 2018/01/31
        /// 
        /// Gets a list of Sources. 
        /// </summary>
        /// <returns>Returns a collection of Sources</returns>
        List<Source> RetrieveSource();

        List<SourceDetail> RetrieveSourceDetailList();

        /// <summary>
        /// Jayden Tollefson
        /// Created: 2018/02/09
        /// 
        /// Edits a supply item from an old entry to a new entry
        /// </summary>
        /// <param name="oldSpecialItem">The item being edited</param>
        /// <param name="newSpecialItem">The item with the new data</param>
        /// <returns>The number of records affected</returns>
        int EditSource(Source oldSource, Source source);

        /// <summary>
        /// Jayden Tollefson
        /// Created: 2018/02/09
        /// 
        /// Adds a source item 
        /// </summary>
        /// <param name="source">The item to add</param>
        /// <returns>The ID of the new item</returns>
        int AddSource(Source source);
    }
}
