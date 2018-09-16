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
using DataObjects;
using System.Collections;

namespace WPFPresentation
{
    /// <summary>
    /// Interaction logic for frmAddEditJobLocation.xaml
    /// </summary>
    public partial class frmAddEditJobLocation : Window
    {
        private JobLocationDetail _jobLocationDetail;
        private IJobLocationManager _jobLocationManager;
        private List<Customer> _customerList;

        private ICustomerManager _customerManager = new CustomerManager();

        private List<JobLocationAttribute> _attributes = new List<JobLocationAttribute>();

        /// <summary>
        /// Zachary Hall
        /// Created 2018/03/20
        /// 
        /// Add constructor
        /// </summary>
        /// <param name="jobLocationManager"></param>
        public frmAddEditJobLocation(IJobLocationManager jobLocationManager)
        {
            _jobLocationManager = jobLocationManager;
            InitializeComponent();
            populateAddForm();
        }

        /// <summary>
        /// Zachary Hall
        /// Created 2018/03/20
        /// 
        /// Edit Constructor
        /// </summary>
        /// <param name="jobLocationManager"></param>
        /// <param name="selectedItem"></param>
        public frmAddEditJobLocation(IJobLocationManager jobLocationManager, JobLocationDetail selectedItem)
        {
            this._jobLocationManager = jobLocationManager;
            this._jobLocationDetail = selectedItem;

            InitializeComponent();
            populateEditForm();
        }


        /// <summary>
        /// Zachary Hall
        /// Created 2018/03/10
        /// 
        /// Populates teh data grid with appropriate data
        /// </summary>
        private void populateAttributeDataGrid()
        {
            var allAttributes = _jobLocationManager.RetrieveJobLocationAttributeList();
            if (_jobLocationDetail == null)//add
            {
                _attributes = allAttributes;

            }
            else//edit
            {
                var jobLocationAttributes = _jobLocationManager.RetrieveJobLocationAttributeListByJobLocationID(_jobLocationDetail.JobLocation.JobLocationID);

                foreach (var i in jobLocationAttributes)
                {
                    foreach (var j in allAttributes)
                    {
                        if (i.JobLocationAttributeTypeID == j.JobLocationAttributeTypeID)
                        {
                            allAttributes.Remove(j);
                            break;
                        }
                    }
                }
                _attributes.AddRange(jobLocationAttributes);
                _attributes.AddRange(allAttributes);
                foreach (var attr in _attributes)
                {
                    attr.JobLocationID = _jobLocationDetail.JobLocation.JobLocationID;
                }
            }
            dgAttributes.ItemsSource = _attributes;

        }

        /// <summary>
        /// Zachary Hall
        /// Created 2018/03/10
        /// 
        /// Sets up the edit form
        /// </summary>
        private void populateEditForm()
        {
            lblHeader.Content = "Editing Job Location";
            populateCustomerList();
            foreach (Customer c in cboCustomers.Items)
            {
                if (c.CustomerID == _jobLocationDetail.Customer.CustomerID)
                {
                    this.cboCustomers.SelectedIndex = cboCustomers.Items.IndexOf(c);
                    break;
                }
            }

            this.txtStreet.Text = _jobLocationDetail.JobLocation.Street;
            this.txtCity.Text = _jobLocationDetail.JobLocation.City;
            cboStates.ItemsSource = Constants.STATES;
            foreach (string state in cboStates.Items)
            {
                if (state.Equals(_jobLocationDetail.JobLocation.State))
                {
                    this.cboStates.SelectedIndex = cboStates.Items.IndexOf(state);
                }
            }

            txtZipCode.Text = _jobLocationDetail.JobLocation.ZipCode;
            txtComments.Text = _jobLocationDetail.JobLocation.Comments;
            chkJobLocationActive.IsChecked = _jobLocationDetail.JobLocation.Active;

            populateAttributeDataGrid();
            btnAddEdit.Content = "Save";
        }

        /// <summary>
        /// Zachary Hall
        /// Created 2018/03/10
        /// 
        /// Sets up the add form
        /// </summary>
        private void populateAddForm()
        {
            populateCustomerList();
            cboStates.ItemsSource = Constants.STATES;
            populateAttributeDataGrid();
        }

        /// <summary>
        /// Zachary Hall
        /// Created 2018/03/10
        /// 
        /// Populates the customer list
        /// </summary>
        private void populateCustomerList()
        {
            _customerList = _customerManager.RetrieveCustomerList();
            this.cboCustomers.ItemsSource = _customerList;
            this.cboCustomers.DisplayMemberPath = "DropdownDisplay";
        }

        /// <summary>
        /// Zachary Hall
        /// Created 2018/03/10
        /// 
        /// Validates a new location
        /// </summary>
        /// <returns></returns>
        private bool validateLocation()
        {
            if (this.cboCustomers.SelectedItem == null)
            {
                MessageBox.Show("You must select a customer!", "Invalid Input", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                return false;
            }
            if (!StringValidations.IsValidNamePropertyEmpty(txtStreet.Text))
            {
                MessageBox.Show("The street field cannot be empty!", "Invalid Input", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                return false;
            }

            if (!StringValidations.IsValidNamePropertyMaxSize(txtStreet.Text, Constants.MAX_JOB_LOCATION_STREET_LENGTH))
            {
                MessageBox.Show("The street field cannot be more than " + Constants.MAX_JOB_LOCATION_STREET_LENGTH + " characters!", "Invalid Input", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                return false;
            }

            if (!StringValidations.IsValidNamePropertyEmpty(txtCity.Text))
            {
                MessageBox.Show("The city field cannot be empty!", "Invalid Input", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                return false;
            }

            if (!StringValidations.IsValidNamePropertyMaxSize(txtCity.Text, Constants.MAX_JOB_LOCATION_CITY_LENGTH))
            {
                MessageBox.Show("The city field cannot be more than " + Constants.MAX_JOB_LOCATION_CITY_LENGTH + " characters!", "Invalid Input", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                return false;
            }

            if (this.cboStates.SelectedItem == null)
            {
                MessageBox.Show("You must select a state", "Invalid Input", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                return false;
            }
            if (!StringValidations.IsValidNamePropertyEmpty(txtZipCode.Text))
            {
                MessageBox.Show("The zip code field cannot be empty", "Invalid Input", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                return false;
            }

            if (!StringValidations.IsValidNamePropertyMaxSize(txtZipCode.Text, Constants.MAX_JOB_LOCATION_ZIP_CODE_LENGTH))
            {
                MessageBox.Show("The zip code field cannot be more than " + Constants.MAX_JOB_LOCATION_ZIP_CODE_LENGTH + " characters!", "Invalid Input", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                return false;
            }

            if (!StringValidations.IsValidNamePropertyEmpty(txtComments.Text))
            {
                MessageBox.Show("The comments field cannot be empty", "Invalid Input", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                return false;
            }

            if (!StringValidations.IsValidNamePropertyMaxSize(txtComments.Text, Constants.MAXDESCRIPTIONLENGTH))
            {
                MessageBox.Show("The comments field cannot be more than " + Constants.MAXDESCRIPTIONLENGTH + " characters!", "Invalid Input", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                return false;
            }

            return true;
        }

        /// <summary>
        /// Zachary Hall
        /// Created 2018/03/20
        /// 
        /// manages adding and editing after button click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAddEdit_Click(object sender, RoutedEventArgs e)
        {
            if (_jobLocationDetail == null)
            {
                performAdd();
            }
            else
            {
                performEdit();
            }
        }

        /// <summary>
        /// Zachary Hall
        /// Created 2018/03/20
        /// 
        /// Starts Edit functionality on form
        /// </summary>
        private void performEdit()
        {
            if (validateLocation())
            {
                try
                {
                    var newJobLocation = new JobLocation
                    {
                        CustomerID = ((Customer)(cboCustomers.SelectedItem)).CustomerID,
                        Street = txtStreet.Text,
                        City = txtCity.Text,
                        State = (string)(cboStates.SelectedItem),
                        ZipCode = txtZipCode.Text,
                        Comments = txtComments.Text,
                        Active = (chkJobLocationActive.IsChecked == true)

                    };
                    var updateLocationResult = _jobLocationManager.UpdateJobLocation(_jobLocationDetail.JobLocation, newJobLocation);

                    var attributeResult = _jobLocationManager.CreateUpdateJobLocationAttributes((IEnumerable<JobLocationAttribute>)(dgAttributes.ItemsSource));

                    this.DialogResult = true;
                    this.Close();
                }
                catch (Exception)
                {

                    MessageBox.Show("There was an error editing the Job Location", "Editing Error", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                }

            }
        }

        /// <summary>
        /// Zachary Hall
        /// Created 2018/03/20
        /// 
        /// Starts add functionality on form
        /// </summary>
        private void performAdd()
        {
            if (validateLocation())
            {
                try
                {
                    var newJobLocation = new JobLocation
                    {
                        CustomerID = ((Customer)(cboCustomers.SelectedItem)).CustomerID,
                        Street = txtStreet.Text,
                        City = txtCity.Text,
                        State = (string)(cboStates.SelectedItem),
                        ZipCode = txtZipCode.Text,
                        Comments = txtComments.Text,
                        Active = (chkJobLocationActive.IsChecked == true)

                    };
                    var newLocationID = _jobLocationManager.CreateJobLocation(newJobLocation);
                    foreach (var attr in (IEnumerable<JobLocationAttribute>)(dgAttributes.ItemsSource))
                    {
                        attr.JobLocationID = newLocationID;
                    }
                    var attributeResult = _jobLocationManager.CreateUpdateJobLocationAttributes((IEnumerable<JobLocationAttribute>)(dgAttributes.ItemsSource));

                    this.DialogResult = true;
                    this.Close();
                }
                catch (Exception)
                {

                    MessageBox.Show("There was an error adding the Job Location", "Add Error", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                }

            }
        }

        /// <summary>
        /// Zachary Hall
        /// Created 2018/03/20
        /// 
        /// Button cancel event
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
