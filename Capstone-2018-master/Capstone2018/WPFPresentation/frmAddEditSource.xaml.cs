using DataObjects;
using Logic;
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
    /// Interaction logic for frmAddEditSource.xaml
    /// </summary>
    public partial class frmAddEditSource : Window
    {
        private ISourceManager _sourceManager;
        private Source _source;
        private SourceDetail _sourceDetail;
        public List<Source> _sourceList;
        private IVendorManager _vendorManager;
        private ISpecialOrderItemManager _specialOrderItemManager;
        private ISupplyItemManager _supplyManager;

        private List<Vendor> _vendorList = new List<Vendor>();
        private List<SupplyItem> _supplyItemList = new List<SupplyItem>();
        private List<SpecialItem> _specialOrderList = new List<SpecialItem>();


        /// <summary>
        /// Badis Saidani
        /// Created 2018/04/19
        /// 
        /// Constructor for editing
        /// </summary>
        public frmAddEditSource(ISourceManager _sourceManager, SourceDetail _sourceDetail, IVendorManager vendorManager, ISupplyItemManager supplyManager, ISpecialOrderItemManager specialOrderManager)
        {
            this._sourceManager = _sourceManager;
            this._vendorManager = vendorManager;
            this._supplyManager = supplyManager;
            this._specialOrderItemManager = specialOrderManager;
            this._sourceDetail = _sourceDetail;
            this._source = (Source)this._sourceDetail;

            this._vendorList = _vendorManager.RetrieveVendorList().OrderBy(l => l.VendorID).Distinct().ToList();
            this._supplyItemList = _supplyManager.RetrieveSupplyItemList();
            this._specialOrderList = _specialOrderItemManager.RetrieveSpecialOrderItems();


            InitializeComponent();
            setUpEditForm();
        }


        public frmAddEditSource(ISourceManager _sourceManager, IVendorManager vendorManager, ISupplyItemManager supplyManager, ISpecialOrderItemManager specialOrderManager)
        {
            this._sourceManager = _sourceManager;
            this._vendorManager = vendorManager;
            this._supplyManager = supplyManager;
            this._specialOrderItemManager = specialOrderManager;
            this._source = new Source();
            InitializeComponent();
            setupAddForm();
        }



        private void setupAddForm()
        {
            lblHeader.Content = "Adding a new Source Item";
            btnAddEdit.Content = "Add";

            _vendorList = _vendorManager.RetrieveVendorList();
            List<Vendor> distinctVendors = _vendorList.GroupBy(p => p.Name).Select(g => g.First()).ToList();
            cboVendor.ItemsSource = distinctVendors;

            _supplyItemList = _supplyManager.RetrieveSupplyItemList();
            List<SupplyItem> distinctSupplyItems = _supplyItemList.GroupBy(p => p.Name).Select(g => g.First()).ToList();
            cboSupplyItem.ItemsSource = distinctSupplyItems;

            _specialOrderList = _specialOrderItemManager.RetrieveSpecialOrderItems();
            List<SpecialItem> distinctSpecialOrders = _specialOrderList.GroupBy(p => p.Name).Select(g => g.First()).ToList();
            cboSpecialOrderItem.ItemsSource = distinctSpecialOrders;
        }

        /// <summary>
        /// Dan Cable
        /// 2018/04/20
        /// 
        /// Method performAdd() a source
        /// </summary>
        private void performAdd()
        {
            if (ValidateFields())
            {
                Vendor selectedVendor = (Vendor)cboVendor.SelectedItem;
                SupplyItem selectedSupplyItem = (SupplyItem)cboSupplyItem.SelectedItem;
                SpecialItem selectedSpecialItem = (SpecialItem)cboSpecialOrderItem.SelectedItem;

                var source = new Source()
                {
                    VendorID = selectedVendor.VendorID,
                    SupplyItemID = selectedSupplyItem.SupplyItemID,
                    SpecialOrderItemID = selectedSpecialItem.SpecialOrderItemID,
                    MinimumOrderQTY = (int)numMinOrder.Value,
                    PriceEach = Convert.ToDecimal(numUnitPrice.Value),
                    LeadTime = (int)numLeadTime.Value,
                    Active = (bool)chkActive.IsChecked
                };
                try
                {
                    var result = _sourceManager.AddSource(source);
                    if (result == 0)
                    {
                        MessageBox.Show("Source was not added.\nVendors can't be assigned duplicate Supply items or Special Order items.", "Error!", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                        return;
                    }

                    this.DialogResult = true;
                    this.Close();
                }
                catch (Exception ex)
                {
                    var message = ex.Message;
                    if (ex.InnerException != null)
                    {
                        message += "\n\n" + ex.InnerException.Message;
                    }
                    MessageBox.Show(message, "Add failed", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                }
            }
        }

        /// <summary>
        /// Dan Cable
        /// 2018/04/20
        /// 
        /// Method to validate fields
        /// </summary>
        private bool ValidateFields()
        {
            if (cboVendor.SelectedItem == null)
            {
                MessageBox.Show("You must select a Vendor!");
                return false;
            }
            if (cboSupplyItem.SelectedItem == null)
            {
                MessageBox.Show("You must select a Supply Item!");
                return false;
            }
            if (cboSpecialOrderItem.SelectedItem == null)
            {
                MessageBox.Show("You must select a Special Order!");
                return false;
            }
            if (numMinOrder.Value == null)
            {
                MessageBox.Show("Minimum Order cannot be empty!");
                return false;
            }
            if (!IntegerValidations.IsValidQuantity((int)numMinOrder.Value))
            {
                MessageBox.Show("Minimum Order cannot be a negative value!");
                return false;
            }
            if (numUnitPrice.Value == null)
            {
                MessageBox.Show("Unit Price cannot not be empty!");
                return false;
            }
            if (!IntegerValidations.IsValidQuantity((int)numUnitPrice.Value))
            {
                MessageBox.Show("Unit Price cannot be a negative value!");
                return false;
            }
            if (numLeadTime.Value == null)
            {
                MessageBox.Show("Lead Time cannot not be empty!");
                return false;
            }
            if (!IntegerValidations.IsValidQuantity((int)numLeadTime.Value))
            {
                MessageBox.Show("Lead Time cannot be a negative value!");
                return false;
            }
            return true;
        }

        /// <summary>
        /// Badis Saidani
        /// Created 2018/04/19
        /// 
        /// 
        /// Button functionality
        /// </summary>
        private void btnAddEdit_Click(object sender, RoutedEventArgs e)
        {
            if (_sourceDetail == null)
            {
                performAdd();
            }
            else
            {
                performEdit();
            }
            this.Close();
        }

        /// <summary>
        /// Badis Saidani
        /// Created 2018/04/19
        /// 
        /// Edit the source
        /// </summary>
        private void performEdit()
        {
            var newSource = new Source()
            {
                SourceID = _sourceDetail.SourceID,
                VendorID = ((Vendor)this.cboVendor.SelectedItem).VendorID,
                SupplyItemID = ((SupplyItem)this.cboSupplyItem.SelectedItem).SupplyItemID,
                SpecialOrderItemID = ((SpecialItem)this.cboSpecialOrderItem.SelectedItem).SpecialOrderItemID,
                MinimumOrderQTY = (int)numMinOrder.Value,
                PriceEach = (decimal)numUnitPrice.Value,
                LeadTime = (int)numLeadTime.Value,
                Active = (bool)chkActive.IsChecked

            };
            try
            {
                int res = _sourceManager.EditSource(_source, newSource);
            }
            catch (Exception e)
            {
                var message = e.Message + "\n\n" + e.InnerException;
                MessageBox.Show(message,
             "Editing Error",
             MessageBoxButton.OK);
                throw new ApplicationException(message);
            }
        }
        /// <summary>
        /// Badis Saidani
        /// Created 2018/04/17
        /// 
        /// Fill in the form,  with source information
        /// </summary>
        private void setUpEditForm()
        {

            lblHeader.Content = "Editing Source";
            btnAddEdit.Content = "Submit";

            setComboBoxes();
            this.cboVendor.SelectedItem = _vendorList.Find(v => v.VendorID == _sourceDetail.VendorID);
            this.cboSupplyItem.SelectedItem = _supplyItemList.Find(s => s.SupplyItemID == _sourceDetail.SupplyItemID);
            this.cboSpecialOrderItem.SelectedItem = _specialOrderList.Find(s => s.SpecialOrderItemID == _sourceDetail.SpecialOrderItemID);


            this.numMinOrder.Text = "" + _sourceDetail.MinimumOrderQTY;
            this.numUnitPrice.Text = "" + _sourceDetail.PriceEach;
            this.numLeadTime.Text = "" + _sourceDetail.LeadTime;
            if (_sourceDetail.Active)
            {
                this.chkActive.IsChecked = true;
            }

        }
        /// <summary>
        /// Badis Saidani
        /// Created 2018/04/17
        /// 
        /// set ComboBoxes with data
        /// </summary>
        private void setComboBoxes()
        {

            try
            {

                this.cboVendor.ItemsSource = _vendorList;
                this.cboVendor.DisplayMemberPath = "Name";
                this.cboSupplyItem.ItemsSource = _supplyItemList;
                this.cboSupplyItem.DisplayMemberPath = "Name";
                this.cboSpecialOrderItem.ItemsSource = _specialOrderList;
                this.cboSpecialOrderItem.DisplayMemberPath = "Name";
            }
            catch (Exception ex)
            {
                MessageBox.Show("There was an issue retrieving a lists." + ex);
            }
        }

        /// <summary>
        /// Badis Saidani
        /// Created 2018/04/17
        /// 
        /// Cancel the change
        /// </summary>
        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Are you sure you want to Cancel?\nCanceling will discard any unsaved changes.",
             "Cancel Warning",
             MessageBoxButton.YesNo);

            if (result == MessageBoxResult.Yes)
            {
                this.DialogResult = false;
                this.Close();
            }
        }
    }
}
