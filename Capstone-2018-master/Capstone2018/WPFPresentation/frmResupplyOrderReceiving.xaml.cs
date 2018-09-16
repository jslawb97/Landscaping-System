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
using Logic;

namespace WPFPresentation
{
    /// <summary>
    /// Interaction logic for frmResupplyOrderReceiving.xaml
    /// </summary>
    public partial class frmResupplyOrderReceiving : Window
    {
        private IResupplyOrderLineManager _resupplyOrderLineManager;
        private IResupplyOrderManager _resupplyOrderManager;
        private ResupplyOrderDetail _selectedItem;
        private List<ResupplyOrderLineDetail> _resupplyOrderLineDetailList;

        public frmResupplyOrderReceiving()
        {
            InitializeComponent();
        }

        public frmResupplyOrderReceiving(IResupplyOrderManager resupplyOrderManager, IResupplyOrderLineManager resupplyOrderLineManager, ResupplyOrderDetail selectedItem)
        {
            InitializeComponent();
            _resupplyOrderLineManager = resupplyOrderLineManager;
            _resupplyOrderManager = resupplyOrderManager;
            this._selectedItem = selectedItem;
            setupForm();
        }

        /// <summary>
        /// Zachary Hall
        /// 2018/04/19
        /// </summary>
        private void populateResupplyOrderLineDetailList()
        {
            try
            {
                _resupplyOrderLineDetailList = _resupplyOrderLineManager.RetrieveResupplyOrderLineDetailListByResupplyOrderIDWithReceived(_selectedItem.ResupplyOrder.ResupplyOrderID);
            }
            catch (Exception ex)
            {

                var message = ex.Message + "\n\n" + ex.InnerException;
                MessageBox.Show(message, "Error Retrieving Order Lines.", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
        }

        /// <summary>
        /// Zachary Hall
        /// 2018/04/19
        /// </summary>
        private void refreshDatagrid()
        {
            populateResupplyOrderLineDetailList();
            if(_resupplyOrderLineDetailList != null)
            {
                dgResupplyOrderLines.ItemsSource = null;
                dgResupplyOrderLines.ItemsSource = _resupplyOrderLineDetailList;
            }
        }

        /// <summary>
        /// Zachary Hall
        /// 2018/04/19
        /// </summary>
        private void setupForm()
        {
            
            chkReceived.IsChecked = _selectedItem.ResupplyOrder.SupplyStatusID.Equals(SupplyStatus.Received.ToString()) ? true : false;
            refreshDatagrid();
        }

        /// <summary>
        /// Zachary Hall
        /// 2018/04/19
        /// </summary>
        private void btnSetQty_Click(object sender, RoutedEventArgs e)
        {
            if (this.numQtyReceived.Value == null)
            {
                MessageBox.Show("You must enter a quantity!");
                return;
            }else if (!(this.dgResupplyOrderLines.SelectedItems.Count > 0))
            {
                MessageBox.Show("You must select an order line!");
                return;
            }
            else
            {
                try
                {
                    var selectedDetail = ((ResupplyOrderLineDetail)this.dgResupplyOrderLines.SelectedItem);
                    var result = _resupplyOrderLineManager.EditResupplyOrderLineQtyReceivedByID(selectedDetail.ResupplyOrderLineID, selectedDetail.QtyReceived, (int)(this.numQtyReceived.Value));
                    if(result == true)
                    {
                        refreshDatagrid();
                        this.numQtyReceived.Value = null;
                    }
                    else
                    {
                        MessageBox.Show("There was an error updating the received value!");
                        this.numQtyReceived.Value = null;
                    }
                }
                catch (Exception ex)
                {

                    var message = ex.Message + "\n\n" + ex.InnerException;
                    MessageBox.Show(message, "Error Submiting Quantity.", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                    this.numQtyReceived.Value = null;
                }
            }
        }

        /// <summary>
        /// Zachary Hall
        /// 2018/04/19
        /// </summary>
        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            var isOldOrderReceived = _selectedItem.ResupplyOrder.SupplyStatusID.Equals(SupplyStatus.Received.ToString());
            if (isOldOrderReceived && chkReceived.IsChecked == false
                || !isOldOrderReceived && chkReceived.IsChecked == true)
            {
                try
                {
                    var result = _resupplyOrderManager.EditResupplyOrderStatus(_selectedItem.ResupplyOrder.ResupplyOrderID
                        , _selectedItem.ResupplyOrder.SupplyStatusID
                        , chkReceived.IsChecked == true ? SupplyStatus.Received.ToString() : SupplyStatus.Ordered.ToString());
                    if (result)
                    {
                        MessageBox.Show("Changes were saved");
                        this.DialogResult = true;
                        this.Close();
                    }
                }
                catch (Exception ex)
                {

                    var message = ex.Message + "\n\n" + ex.InnerException;
                    MessageBox.Show(message, "Error Saving Order Status.", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                }
            }
            else
            {
                MessageBox.Show("Changes were saved");
                this.DialogResult = false;
                this.Close();
            }
        }

        /// <summary>
        /// Zachary Hall
        /// 2018/04/19
        /// </summary>
        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Are you sure you want to Cancel?\nCanceling will discard any unsaved changes!", "Cancel Warning", MessageBoxButton.YesNo);
            if (result == MessageBoxResult.Yes)
            {
                this.DialogResult = false;
                this.Close();
            }
        }

        /// <summary>
        /// Zachary Hall
        /// 2018/04/19
        /// </summary>
        private void btnReceiveAll_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Are you sure you want to Receive All?\nDoing so will affect each order line in the table!", "Update Warning", MessageBoxButton.YesNo);
            if (result == MessageBoxResult.Yes)
            {
                try
                {
                    var managerResult = _resupplyOrderLineManager.EditResupplyOrderLinesQtyReceivedToQtyOrderedByID(_selectedItem.ResupplyOrder.ResupplyOrderID);
                    if (managerResult)
                    {
                        MessageBox.Show("Changes were saved");
                        this.DialogResult = true;
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("Changes were not saved!");
                    }
                }
                catch (Exception ex)
                {

                    var message = ex.Message + "\n\n" + ex.InnerException;
                    MessageBox.Show(message, "Error Updating Quantity Received.", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                }
            }
        }
    }
}
