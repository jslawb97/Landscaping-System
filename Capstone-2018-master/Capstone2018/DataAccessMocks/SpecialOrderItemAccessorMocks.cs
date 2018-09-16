using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess;
using DataObjects;

namespace DataAccessMocks
{
    public class SpecialOrderItemAccessorMocks : ISpecialOrderItemAccessor
    {
        private List<SpecialItem> _items;

        public SpecialOrderItemAccessorMocks()
        {
            _items = new List<SpecialItem>();
            _items.Add(new SpecialItem() {
                SpecialOrderItemID = 1000000,
                Name = "TestName1",
                Active = true
            });
            _items.Add(new SpecialItem()
            {
                SpecialOrderItemID = 1000001,
                Name = "TestName2",
                Active = true
            });
            _items.Add(new SpecialItem()
            {
                SpecialOrderItemID = 1000003,
                Name = "TestName3",
                Active = false
            });
        }

        public int CreateSpecialOrderItem(SpecialItem newItem)
        {
            int newID = _items.Count + 1 + Constants.IDSTARTVALUE;
            newItem.SpecialOrderItemID = newID;
            _items.Add(newItem);
            return newID;
        }

        public int DeactivateSpecialOrderByID(int id)
        {
            int rowCount = 0;
            foreach (var item in _items)
            {
                if(item.SpecialOrderItemID == id)
                {
                    item.Active = false;
                    rowCount++;
                }
            }

            return rowCount;
        }

        public int EditSpecialOrderItem(SpecialItem oldSpecialItem, SpecialItem newSpecialItem)
        {
            int rowCount = 0;
            var newItem = new SpecialItem()
            {

                Name = "TestName3",
                Active = true
            };
            var oldItem = new SpecialItem()
            {
                SpecialOrderItemID = 1000001,
                Name = "TestName2",
                Active = true
            };

            foreach (var item in _items)
            {
                if(item.SpecialOrderItemID == oldSpecialItem.SpecialOrderItemID)
                {
                    item.Name = newItem.Name;
                    item.Active = newItem.Active;
                    rowCount++;
                }
            }
            return rowCount;
        }

        public List<SpecialOrderItemDetail> RetrieveSpecialOrderItemDetails()
        {
            throw new NotImplementedException();
        }

        public List<SpecialItem> RetrieveSpecialOrderItemList()
        {
            return _items;
        }
    }
}
