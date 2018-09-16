using DataObjects;
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

namespace WPFPresentation
{
    /// <summary>
    /// Interaction logic for frmUpdateOrderItemQuantity.xaml
    /// </summary>
    public partial class frmUpdateOrderItemQuantity : Window
    {

        public List<SupplyOrderItemDetail> Orders { get; set; }

        private string _supplyName;
        private int _supplyItemID;
        private SupplyOrderItemDetail _supplyOrder;
        private List<SupplyOrderItemDetail> _orders;
        private DetailFormMode _mode;

        public frmUpdateOrderItemQuantity()
        {
            InitializeComponent();
        }

        public frmUpdateOrderItemQuantity(string supplyName, int supplyItemID, List<SupplyOrderItemDetail> orderItems)
        {
            _supplyName = supplyName;
            _supplyItemID = supplyItemID;
            _orders = orderItems;
            _mode = DetailFormMode.Add;
            InitializeComponent();
        }

        public frmUpdateOrderItemQuantity(SupplyOrderItemDetail item, List<SupplyOrderItemDetail> orderItems, DetailFormMode mode)
        {
            _supplyOrder = item;
            _orders = orderItems;
            _mode = mode;
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            switch (_mode)
            {
                case DetailFormMode.Add:
                    setUpAddMode();
                    break;
                case DetailFormMode.Edit:
                    setUpEditMode();
                    break;
                default:
                    break;
            }
        }

        private void setUpEditMode()
        {
            txtOrderItemDetail.Text = "Removing " + _supplyOrder.Name + " from order";
            intQuantity.Value = _supplyOrder.OrderItem.Quantity;
            intQuantity.Maximum = _supplyOrder.OrderItem.Quantity;
            btnAdd.Content = "Remove";
        }

        private void setUpAddMode()
        {
            txtOrderItemDetail.Text = _supplyName;
            if (_orders.Contains(_orders.Find(o => o.Name == _supplyName)))
            {
                intQuantity.Value = _orders.Find(o => o.Name == _supplyName).OrderItem.Quantity;
            }
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            if (_mode == DetailFormMode.Add)
            {
                if (_orders.Contains(_orders.Find(o => o.Name == _supplyName)))
                {
                    _orders[_orders.IndexOf(_orders.Find(o => o.Name == _supplyName))]
                        .OrderItem.Quantity = (int)intQuantity.Value;
                }
                else
                {

                    _orders.Add(new SupplyOrderItemDetail
                    {
                        Name = _supplyName,
                        OrderItem = new SupplyOrderItem
                        {
                            SupplyOrderID = 1000000,
                            SupplyOrderLineID = 1000000,
                            Quantity = (int)intQuantity.Value,
                            SupplyItemID = _supplyItemID
                        }
                    });
                }
            }
            else
            {
                if (intQuantity.Value == intQuantity.Maximum)
                {
                    _orders.Remove(_supplyOrder);
                }
                else
                {
                    _orders[_orders.IndexOf(_supplyOrder)].OrderItem.Quantity = (int)intQuantity.Value;
                }
            }
            this.Orders = _orders;
            this.DialogResult = true;
            this.Close();
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }
    }
}
