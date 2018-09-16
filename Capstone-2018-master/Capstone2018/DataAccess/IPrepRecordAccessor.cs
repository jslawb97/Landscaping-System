using DataObjects;
using System.Collections.Generic;

namespace DataAccess
{
    public interface IPrepRecordAccessor
    {
        int CreatePrepRecord(PrepRecord newItem);

        List<PrepRecord> RetrievePrepRecordList();

        List<PrepRecordDetail> RetrievePrepRecordDetailList();

        PrepRecord RetrievePrepRecordByID(int id);

        int EditPrepRecordItem(PrepRecord oldItem, PrepRecord newItem);

        int DeletePrepRecordByID(int id);

    }
}
