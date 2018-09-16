using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Logic;
using DataObjects;

namespace WPFPresentation
{
    /// <summary>
    /// James McPherson
    /// Created 2018/04/20
    /// 
    /// Window for receiving SupplyOrders
    /// </summary>
    public partial class frmReceivingSupplyOrder : Window
    {
        private ISupplyOrderItemManager _supplyOrderItemManager;
        private SupplyOrder _supplyOrder;
        private List<SupplyOrderItemDetail> _supplyOrderItems;
        private List<SupplyOrderItem> _oldSupplyOrderItems;

        public frmReceivingSupplyOrder(ISupplyOrderItemManager supplyOrderItemManager
            , SupplyOrder supplyOrder, List<SupplyOrderItemDetail> supplyOrderItems)
        {
            _supplyOrderItemManager = supplyOrderItemManager;
            _supplyOrder = supplyOrder;
            _supplyOrderItems = supplyOrderItems;
            _oldSupplyOrderItems = new List<SupplyOrderItem>();
            foreach (var item in _supplyOrderItems)
            {
                var orderItem = new SupplyOrderItem
                {
                    SupplyOrderLineID = item.OrderItem.SupplyOrderLineID,
                    QuantityReceived = item.OrderItem.QuantityReceived
                };
                _oldSupplyOrderItems.Add(orderItem);
            }
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            chkReceived.IsEnabled = false;
            chkReceived.IsChecked = _supplyOrder.SupplyStatusID == "Received";
            populateControls();
        }

        private void populateControls()
        {
            dgSupplyOrderItems.ItemsSource = _supplyOrderItems;
            numQtyReceived.Value = 0;
        }

        /// <summary>
        /// James McPherson
        /// Created 2018/04/20
        /// 
        /// Update an individual quantity received with the
        /// numericupdown's current value
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSetQty_Click(object sender, RoutedEventArgs e)
        {
            if (dgSupplyOrderItems.SelectedItems.Count > 0)
            {
                var selectedOrderLine = (SupplyOrderItemDetail)dgSupplyOrderItems.SelectedItems[0];
                if (numQtyReceived.Value >= 0)
                {
                    selectedOrderLine.OrderItem.QuantityReceived = (int) numQtyReceived.Value;

                    dgSupplyOrderItems.ItemsSource = null;
                    dgSupplyOrderItems.ItemsSource = _supplyOrderItems;
                }
                else
                {
                    numQtyReceived.Value = selectedOrderLine.OrderItem.QuantityReceived;
                    MessageBox.Show("You cannot enter a negative value.");
                }
            }
            else
            {
                MessageBox.Show("You must select an order item.");
            }
        }

        /// <summary>
        /// James McPherson
        /// Created 2018/04/20
        /// 
        /// Set all QuantityReceived fields to their respective
        /// quantity ordered fields and update the numericupdown
        /// to reflect this change in the currently selected item
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnReceiveAll_Click(object sender, RoutedEventArgs e)
        {
            foreach(var item in _supplyOrderItems) {
                item.OrderItem.QuantityReceived = item.OrderItem.Quantity;
            }

            if (dgSupplyOrderItems.SelectedItems.Count == 1)
            {
                var selectedOrderLine = (SupplyOrderItemDetail)dgSupplyOrderItems.SelectedItems[0];
                numQtyReceived.Value = selectedOrderLine.OrderItem.QuantityReceived;
            }
            dgSupplyOrderItems.ItemsSource = null;
            dgSupplyOrderItems.ItemsSource = _supplyOrderItems;
        }

        /// <summary>
        /// James McPherson
        /// Created 2018/04/20
        /// 
        /// Update the numericupdown when selecting a new item
        /// Make sure clicking buttons doesn't deselect the
        /// current item
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgSupplyOrderItems_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count == 1)
            {
                var selectedOrderLine = (SupplyOrderItemDetail)e.AddedItems[0];
                numQtyReceived.Value = selectedOrderLine.OrderItem.QuantityReceived;
            }
            else
            {
                if (e.RemovedItems.Count == 1 && e.AddedItems.Count == 0)
                {
                    dgSupplyOrderItems.SelectedItem = e.RemovedItems[0];
                }
            }
        }

        /// <summary>
        /// James McPherson
        /// Created 2018/04/20
        /// 
        /// Save all QuantityReceived changes
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            var changesMade = false;
            var newSupplyOrderItems
                = _supplyOrderItems.Select(item => item.OrderItem).ToList();
            foreach (var newItem in newSupplyOrderItems)
            {
                foreach (var oldItem in _oldSupplyOrderItems)
                {
                    if (newItem.SupplyOrderLineID
                        == oldItem.SupplyOrderLineID
                        && newItem.QuantityReceived
                        != oldItem.QuantityReceived)
                    {
                        changesMade = true;
                        break;
                    }
                }
            }
            if (changesMade)
            {
                try
                {
                    var result = _supplyOrderItemManager.EditSupplyOrderLineQuantityReceived(_supplyOrder
                        , newSupplyOrderItems, _oldSupplyOrderItems);
                    
                    DialogResult = true;
                }
                catch
                {
                    MessageBox.Show("There was a problem updating the supply order lines.");
                }
            }
            else
            {
                MessageBox.Show("No changes were made.");
            }
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }
    }
}
