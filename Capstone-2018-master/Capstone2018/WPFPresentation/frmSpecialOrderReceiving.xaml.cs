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
    /// Interaction logic for frmSpecialOrderReceiving.xaml
    /// </summary>
    public partial class frmSpecialOrderReceiving : Window
    {
        private ISpecialOrderManager _specialOrderManager;
        private ISpecialOrderLineManager _specialOrderLineManager;
        private SpecialOrder _specialOrder;
        private List<SpecialOrderLineDetail> _specialOrderLineDetailList;

        public frmSpecialOrderReceiving()
        {
            InitializeComponent();
        }

        public frmSpecialOrderReceiving(ISpecialOrderManager specialOrderManager, ISpecialOrderLineManager specialOrderLineManager, SpecialOrder specialOrder)
        {
            InitializeComponent();
            _specialOrderManager = specialOrderManager;
            _specialOrderLineManager = specialOrderLineManager;
            this._specialOrder = specialOrder;
            setupForm();
        }

        /// <summary>
        /// Zachary Hall
        /// 2018/04/20
        /// </summary>
        private void populateSpecialOrderLineDetailList()
        {
            try
            {
                _specialOrderLineDetailList = _specialOrderLineManager.RetrieveSpecialOrderLineDetailListByOrderID(_specialOrder.SpecialOrderID);
            }
            catch (Exception ex)
            {
                var message = ex.Message + "\n\n" + ex.InnerException;
                MessageBox.Show(message, "Error Retrieving Order Lines.", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
        }

        /// <summary>
        /// Zachary Hall
        /// 2018/04/20
        /// </summary>
        private void refreshDatagrid()
        {
            populateSpecialOrderLineDetailList();
            if(_specialOrderLineDetailList != null)
            {
                dgSpecialOrderLines.ItemsSource = null;
                dgSpecialOrderLines.ItemsSource = _specialOrderLineDetailList;
            }
        }

        private void setupForm()
        {
            chkReceived.IsChecked = _specialOrder.SupplyStatusID.Equals(SupplyStatus.Received.ToString()) ? true : false;
            refreshDatagrid();
        }

        /// <summary>
        /// Zachary Hall
        /// 2018/04/20
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnReceiveAll_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Are you sure you want to Receive All?\nDoing so will affect each order line in the table!", "Update Warning", MessageBoxButton.YesNo);
            if (result == MessageBoxResult.Yes)
            {
                try
                {
                    var managerResult = _specialOrderLineManager.EditSpecialOrderLineQtyReceivedToQtyOrderedByOrderID(_specialOrder.SpecialOrderID);
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

        /// <summary>
        /// Zachary Hall
        /// 2018/04/20
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSetQty_Click(object sender, RoutedEventArgs e)
        {
            if (this.numQtyReceived.Value == null)
            {
                MessageBox.Show("You must enter a quantity!");
                return;
            }
            else if (!(this.dgSpecialOrderLines.SelectedItems.Count > 0))
            {
                MessageBox.Show("You must select an order line!");
                return;
            }
            else
            {
                try
                {
                    var selectedDetail = ((SpecialOrderLineDetail)this.dgSpecialOrderLines.SelectedItem);
                    var result = _specialOrderLineManager.EditSpecialOrderLineQtyReceivedByID(selectedDetail.Line.SpecialOrderLineID, selectedDetail.Line.QtyReceived, (int)(this.numQtyReceived.Value));
                    if (result == true)
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
        /// 2018/04/20
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            var isOldOrderReceived = _specialOrder.SupplyStatusID.Equals(SupplyStatus.Received.ToString());
            if (isOldOrderReceived && chkReceived.IsChecked == false
                || !isOldOrderReceived && chkReceived.IsChecked == true)
            {
                try
                {
                    var result = _specialOrderManager.EditSpecialOrderSupplyStatusByID(_specialOrder.SpecialOrderID
                        , _specialOrder.SupplyStatusID
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
        /// 2018/04/20
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Are you sure you want to Cancel?\nCanceling will discard any unsaved changes!", "Cancel Warning", MessageBoxButton.YesNo);
            if (result == MessageBoxResult.Yes)
            {
                this.DialogResult = false;
                this.Close();
            }
        }
    }
}
