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
using DataObjects;

namespace WPFPresentation
{
    /// <summary>
    /// Interaction logic for frmAddEditSpecialOrderLine.xaml
    /// </summary>
    public partial class frmAddEditSpecialOrderLine : Window
    {
        private DetailFormMode _mode;

        private string _specialOrderItemName;
        private int _specialOrderItemID;

        private SpecialOrderLineDetail _specialOrderLineDetail;
        public List<SpecialOrderLineDetail> OrderLineDetails { get; set; }

        public frmAddEditSpecialOrderLine()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Reuben Cassell
        /// Created 4/6/2018
        /// 
        /// Constructor for Add Mode
        /// </summary>
        /// <param name="name"></param>
        /// <param name="specialOrderItemID"></param>
        /// <param name="orderLineDetails"></param>
        public frmAddEditSpecialOrderLine(string name, int specialOrderItemID, List<SpecialOrderLineDetail> orderLineDetails)
        {
            this._mode = DetailFormMode.Add;
            this._specialOrderItemName = name;
            this._specialOrderItemID = specialOrderItemID;
            this.OrderLineDetails = orderLineDetails;
            InitializeComponent();
        }

        /// <summary>
        /// Reuben CAssell
        /// Created 4/6/2018
        /// 
        /// Consctructor for edit mode.
        /// </summary>
        /// <param name="specialOrderLine"></param>
        /// <param name="specialOrderLines"></param>
        /// <param name="mode"></param>
        public frmAddEditSpecialOrderLine(SpecialOrderLineDetail specialOrderLine, List<SpecialOrderLineDetail> specialOrderLines, DetailFormMode mode)
        {
            this._specialOrderLineDetail = specialOrderLine;
            this.OrderLineDetails = specialOrderLines;
            this._mode = mode;
            InitializeComponent();

        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            if (_mode == DetailFormMode.Add)
            {
                if (OrderLineDetails.Contains(OrderLineDetails.Find(n => n.ItemName == _specialOrderItemName)))
                {
                    OrderLineDetails[OrderLineDetails.IndexOf(
                        OrderLineDetails.Find(n => n.ItemName == _specialOrderItemName))]
                        .Line.Quantity = (int)intQuantity.Value;
                }
                else
                {
                    OrderLineDetails.Add(new SpecialOrderLineDetail
                    {
                        ItemName = _specialOrderItemName,
                        Line = new SpecialOrderLine
                        {
                            SpecialOrderItemID = _specialOrderItemID,
                            Quantity = (int)intQuantity.Value
                        }
                    });
                }
            }
            else  // edit mode
            {
                //if (OrderLineDetails == null)
                //{
                //    OrderLineDetails = new List<SpecialOrderLineDetail>();
                    
                    
                //}
                if (intQuantity.Value == intQuantity.Maximum)
                {
                    OrderLineDetails.Remove(OrderLineDetails.Find(l => l.ItemName == _specialOrderLineDetail.ItemName));
                }
                else
                {
                    int index = OrderLineDetails.IndexOf(OrderLineDetails.Find(l => l.ItemName == _specialOrderLineDetail.ItemName));
                    //MessageBox.Show("Index of item:" + index);
                    OrderLineDetails[index].Line.Quantity = (int)intQuantity.Value;
                }
            }
            this.DialogResult = true;
            // this.Close();
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            switch (_mode)
            {
                case DetailFormMode.Add:
                    setupAddMode();
                    break;
                case DetailFormMode.Edit:
                    setupEditMode();
                    break;
                default:
                    break;
            }
        }

        private void setupEditMode()
        {
            lblOrderItemDetail.Content = _specialOrderItemName;
            intQuantity.Value = _specialOrderLineDetail.Line.Quantity;
            intQuantity.Maximum = _specialOrderLineDetail.Line.Quantity;
            btnAdd.Content = "Remove";
        }

        private void setupAddMode()
        {
            lblOrderItemDetail.Content = _specialOrderItemName;
            if (OrderLineDetails.Contains(OrderLineDetails.Find(n => n.ItemName == _specialOrderItemName)))
            {
                intQuantity.Value = OrderLineDetails.Find(n => n.ItemName == _specialOrderItemName).Line.Quantity;
            }
        }
    }
}
