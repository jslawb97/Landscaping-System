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
    /// Interaction logic for frmAddEditJob.xaml
    /// </summary>
    public partial class frmAddEditJob : Window
    {
        Employee _currentEmployee = new Employee() { EmployeeID = 1000000 };

        private JobDetail _jobDetail;
        private IJobManager _jobManager;

        private ICustomerManager _customerManager = new CustomerManager();
        private List<Customer> _customerList;

        private ICustomerTypeManager _customerTypeManager = new CustomerTypeManager();
        private List<CustomerType> _customerTypeList;

        private IServicePackageManager _servicePackageManager = new ServicePackageManager();
        private List<ServicePackage> _servicePackageList;
        private List<ServicePackage> _assignedServicePackageList = new List<ServicePackage>();

        private IJobLocationManager _jobLocationManager = new JobLocationManager();
        private List<JobLocationAttribute> _attributeList;

        private bool _isCustomerValid = false;
        private bool _isPackageValid = false;
        private bool _isLocationValid = false;

        public frmAddEditJob()
        {
            InitializeComponent();
            setupDefaults();
        }

        /// <summary>
        /// Zachary Hall
        /// Created 2018/03/10
        /// 
        /// Add constructor
        /// </summary>
        /// <param name="jobManager"></param>
        public frmAddEditJob(IJobManager jobManager, Employee employee)
        {
            this._jobManager = jobManager;
            InitializeComponent();
            setupDefaults();
            var list = _jobLocationManager.RetrieveJobLocationDetailList();
            _currentEmployee = employee;
        }

        /// <summary>
        /// Zachary Hall
        /// Created 2018/03/10
        /// 
        /// Edit constructor
        /// </summary>
        /// <param name="_jobManager"></param>
        /// <param name="jobDetail"></param>
        public frmAddEditJob(IJobManager _jobManager, JobDetail jobDetail, Employee employee)
        {
            this._jobManager = _jobManager;
            this._jobDetail = jobDetail;
            InitializeComponent();
            setupDefaults();
            setupEditForm();
            _currentEmployee = employee;
        }

        /// <summary>
        /// Zachary Hall
        /// Created 2018/03/10
        /// 
        /// Inserts the given Job detail information into the form
        /// </summary>
        private void setupEditForm()
        {
            //populate customer tab
            foreach (Customer c in cboCustomers.Items)
            {
                if (c.CustomerID == _jobDetail.Customer.CustomerID)
                {
                    this.cboCustomers.SelectedIndex = cboCustomers.Items.IndexOf(c);
                    break;
                }
            }

            // populate service offerings tab

            _assignedServicePackageList = _jobManager.RetrieveServicePackageListByJobID(_jobDetail.Job.JobID);
            foreach (var i in _assignedServicePackageList)
            {
                foreach (var j in _servicePackageList)
                {
                    if (i.ServicePackageID == j.ServicePackageID)
                    {
                        _servicePackageList.Remove(j);
                        break;
                    }
                }
            }
            refreshServicePackageLists();

            // populate job location tab

            foreach (JobLocation jl in cboJobLocations.Items)
            {
                if (jl.JobLocationID == _jobDetail.JobLocationDetail.JobLocation.JobLocationID)
                {
                    this.cboJobLocations.SelectedIndex = cboJobLocations.Items.IndexOf(jl);
                    break;
                }

            }
            populateJobLocationAttributeDataGrid();

            // populate general tab
            timeScheduled.Value = _jobDetail.Job.DateScheduled;
            timeScheduled.Minimum = null;
            timeCompleted.IsEnabled = true;
            timeCompleted.Value = _jobDetail.Job.DateCompleted;
            timeTarget.Value = _jobDetail.Job.DateTarget;

            timeCompleted.Minimum = null;
            txtComments.Text = _jobDetail.Job.Comments;
            chkActive.IsChecked = _jobDetail.Job.Active;

            btnAddEdit.Content = "Save";

        }

        /// <summary>
        /// Zachary Hall
        /// Created 2018/03/10
        /// 
        /// Sets up form controls for add and edit. 
        /// </summary>
        private void setupDefaults()
        {
            radReturningCustomer.IsChecked = true;
            radReturningLocation.IsChecked = true;
            _customerList = _customerManager.RetrieveCustomerList();
            _customerTypeList = _customerTypeManager.RetrieveCustomerTypeList();
            _servicePackageList = _servicePackageManager.RetrieveServicePackageList();

            dgAvailableServicePackages.ItemsSource = null;
            dgAvailableServicePackages.Items.Clear();
            dgAvailableServicePackages.ItemsSource = _servicePackageList;

            dgAssignedServicePackages.ItemsSource = null;
            dgAssignedServicePackages.Items.Clear();
            dgAssignedServicePackages.ItemsSource = _assignedServicePackageList;

            populateCustomerList();
            populateCustomerTypeList();
            cboStates.ItemsSource = Constants.STATES;
            
            timeCompleted.IsEnabled = false;
        }

        /// <summary>
        /// Zachary Hall
        /// Created 2018/03/10
        /// 
        /// Populates the attribute datagrid based on selected service packages and merges that with the attributes of the joblocation if one is selected.
        /// 
        /// </summary>
        private void populateJobLocationAttributeDataGrid()
        {
            //create a list of attributes
            _attributeList = new List<JobLocationAttribute>();

            //foreach package, get the attributes and add them to the list
            foreach (var item in _assignedServicePackageList)
            {
                var packageAttributesList = _jobLocationManager.RetrieveJobLocationAttributeListByServicePackageID(item.ServicePackageID);
                _attributeList.AddRange(packageAttributesList);

            }

            //Now, filter that list down to distinct attributes
            _attributeList = _attributeList.GroupBy(a => a.JobLocationAttributeTypeID).Select(a => a.First()).ToList();

            List<JobLocationAttribute> selectedLocationAttributes;
            //if returning location, get the attributes and merge with attribute list
            if (cboJobLocations.SelectedItem != null && radReturningLocation.IsChecked == true)
            {
                selectedLocationAttributes = _jobLocationManager.RetrieveJobLocationAttributeListByJobLocationID(((JobLocation)(cboJobLocations.SelectedItem)).JobLocationID);
                foreach (var i in selectedLocationAttributes)
                {
                    foreach (var j in _attributeList)
                    {
                        if (i.JobLocationAttributeTypeID == j.JobLocationAttributeTypeID)
                        {
                            _attributeList.Remove(j);
                            break;
                        }
                    }
                }
                _attributeList.AddRange(selectedLocationAttributes);
            }


            dgAttributes.ItemsSource = null;
            dgAttributes.Items.Clear();
            dgAttributes.ItemsSource = _attributeList;

        }

        /// <summary>
        /// Zachary Hall
        /// Created 2018/03/10
        /// 
        /// Populates the customer type dropdown
        /// </summary>
        private void populateCustomerTypeList()
        {
            this.cboCustomerTypes.ItemsSource = _customerTypeList;
            this.cboCustomerTypes.DisplayMemberPath = "CustomerTypeID";
        }

        /// <summary>
        /// Sets controls for when a radio button on the customer tab is checked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CustomerRadioButton_Checked(object sender, RoutedEventArgs e)
        {

            if (radNewCustomer.IsChecked == true)
            {
                stkReturningCustomer.Visibility = Visibility.Hidden;
                grdNewCustomer.Visibility = Visibility.Visible;
            }
            if (radReturningCustomer.IsChecked == true)
            {
                stkReturningCustomer.Visibility = Visibility.Visible;
                grdNewCustomer.Visibility = Visibility.Hidden;
            }
        }

        /// <summary>
        /// Zachary Hall
        /// Created 2018/03/10
        /// 
        /// Populates the customer list
        /// </summary>
        private void populateCustomerList()
        {
            this.cboCustomers.ItemsSource = _customerList;
            this.cboCustomers.DisplayMemberPath = "DropdownDisplay";
        }

        /// <summary>
        /// Zachary Hall
        /// Created 2018/03/10
        /// 
        /// Validates the Customer tab
        /// </summary>
        /// <returns></returns>
        private bool validateCustomerTab()
        {
            if (radNewCustomer.IsChecked == true)
            {
                return validateNewCustomer();
            }
            if (radReturningCustomer.IsChecked == true)
            {
                if (this.cboCustomers.SelectedItem == null)
                {
                    MessageBox.Show("You must select a Customer", "Invalid Input", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                    return false;
                }

            }

            return true;
        }

        /// <summary>
        /// Zachary Hall
        /// Created 2018/03/10
        /// 
        /// Validates a new customer
        /// </summary>
        /// <returns></returns>
        private bool validateNewCustomer()
        {

            if (cboCustomerTypes.SelectedItem == null)
            {
                MessageBox.Show("You must select a Customer Type", "Invalid Input", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                return false;
            }

            if (!StringValidations.IsValidNamePropertyMaxSize(txtEmail.Text, Constants.MAX_CUSTOMER_EMAIL_LENGTH))
            {
                MessageBox.Show("Email exceeds max characters of " + Constants.MAX_CUSTOMER_EMAIL_LENGTH, "Invalid Input", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                return false;
            }

            if (!StringValidations.IsValidEmail(txtEmail.Text))
            {
                MessageBox.Show("That is not an email", "Invalid Input", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                return false;
            }

            if (!StringValidations.IsValidNamePropertyEmpty(txtEmail.Text))
            {
                MessageBox.Show("Email cannot be empty", "Invalid Input", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                return false;
            }

            if (!StringValidations.IsValidNamePropertyEmpty(txtFirstName.Text))
            {
                MessageBox.Show("First Name cannot be empty", "Invalid Input", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                return false;
            }

            if (!StringValidations.IsValidNamePropertyMaxSize(txtFirstName.Text, Constants.MAX_CUSTOMER_FIRST_NAME_LENGTH))
            {
                MessageBox.Show("First Name exceeds max characters of " + Constants.MAX_CUSTOMER_FIRST_NAME_LENGTH, "Invalid Input", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                return false;
            }

            if (!StringValidations.IsValidNamePropertyEmpty(txtLastName.Text))
            {
                MessageBox.Show("Last Name cannot be empty", "Invalid Input", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                return false;
            }

            if (!StringValidations.IsValidNamePropertyMaxSize(txtLastName.Text, Constants.MAX_CUSTOMER_FIRST_NAME_LENGTH))
            {
                MessageBox.Show("Last Name exceeds max characters of " + Constants.MAX_CUSTOMER_FIRST_NAME_LENGTH, "Invalid Input", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                return false;
            }

            if (!StringValidations.IsValidPhoneNumber(txtPhone.Text))
            {
                MessageBox.Show("Phone Number invalid", "Invalid Input", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                return false;
            }


            return true;

        }

        /// <summary>
        /// Zachary Hall
        /// Created 2018/03/10
        /// 
        /// Removes a ServicePackage item from the assigned datagrid into the available ServicePackages datagrid
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnRemoveServicePackage_Click(object sender, RoutedEventArgs e)
        {
            if (this.dgAssignedServicePackages.SelectedItems.Count > 0)
            {
                var item = (ServicePackage)dgAssignedServicePackages.SelectedItem;
                _servicePackageList.Add(item);
                _assignedServicePackageList.Remove(item);
                refreshServicePackageLists();
            }
            else
            {
                MessageBox.Show("You must select something!");
            }
        }

        /// <summary>
        /// Zachary Hall
        /// Created 2018/03/10
        /// 
        /// Resets the two ServicePackage datagrids
        /// </summary>
        private void refreshServicePackageLists()
        {
            dgAvailableServicePackages.ItemsSource = null;
            dgAvailableServicePackages.Items.Clear();
            dgAvailableServicePackages.ItemsSource = _servicePackageList;

            dgAssignedServicePackages.ItemsSource = null;
            dgAssignedServicePackages.Items.Clear();
            dgAssignedServicePackages.ItemsSource = _assignedServicePackageList;
        }

        /// <summary>
        /// Zachary Hall
        /// Created 2018/03/10
        /// 
        /// Removes the ServicePackage from Available datagrid to the Assigned datagrid
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAddServicePackage_Click(object sender, RoutedEventArgs e)
        {
            if (this.dgAvailableServicePackages.SelectedItems.Count > 0)
            {
                var item = (ServicePackage)dgAvailableServicePackages.SelectedItem;
                _servicePackageList.Remove(item);
                _assignedServicePackageList.Add(item);
                refreshServicePackageLists();
            }
            else
            {
                MessageBox.Show("You must select something!");
            }
        }

        /// <summary>
        /// Zachary Hall
        /// Created 2018/03/10
        /// 
        /// Validates all tabs up to the Job Location tab; if any are not valid, it will return to that tab with a message
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TabItem_Selected(object sender, RoutedEventArgs e)
        {
            _isCustomerValid = validateCustomerTab();
            if (!_isCustomerValid)
            {
                e.Handled = true;
                tabCtrlMain.SelectedItem = tabCustomer;
                tabLocation.IsSelected = false;
                return;
            }

            _isPackageValid = validatePackageTab();
            if (!_isPackageValid)
            {
                e.Handled = true;
                tabCtrlMain.SelectedItem = tabPackage;
                tabLocation.IsSelected = false;
                return;
            }


            if (_jobDetail == null)
            {
                if (cboJobLocations.SelectedItem == null)
                {
                    populateJobLocationList();
                    populateJobLocationAttributeDataGrid();
                }
            }
            else
            {
                if (dgAttributes.Items.IsEmpty)
                {
                    populateJobLocationAttributeDataGrid();
                }

            }
        }

        /// <summary>
        /// Zachary Hall
        /// Created 2018/03/10
        /// 
        /// Populates the Job Location list and dropdown
        /// </summary>
        private void populateJobLocationList()
        {
            if (radReturningCustomer.IsChecked == true)
            {
                radReturningLocation.IsEnabled = true;
                this.cboJobLocations.ItemsSource = _jobLocationManager.RetrieveJobLocationListByCustomerID(((Customer)cboCustomers.SelectedItem).CustomerID);
                this.cboJobLocations.DisplayMemberPath = "AddressDisplay";
            }
            else
            {
                radReturningLocation.IsEnabled = false;
                radNewLocation.IsChecked = true;
            }
        }

        /// <summary>
        /// Zachary Hall
        /// Created 2018/03/10
        /// 
        /// Event for when the Returning Location radio button is checked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void radReturningLocation_Checked(object sender, RoutedEventArgs e)
        {
            lblHeader.Visibility = Visibility.Hidden;
            txtStreet.Visibility = Visibility.Hidden;
            txtCity.Visibility = Visibility.Hidden;
            cboStates.Visibility = Visibility.Hidden;
            chkJobLocationActive.Visibility = Visibility.Hidden;
            txtZipCode.Visibility = Visibility.Hidden;

            lblState.Visibility = Visibility.Hidden;
            lblStreet.Visibility = Visibility.Hidden;
            lblCity.Visibility = Visibility.Hidden;
            lblActive.Visibility = Visibility.Hidden;
            lblZipCode.Visibility = Visibility.Hidden;

            stkReturningJobLocation.Visibility = Visibility.Visible;
        }

        /// <summary>
        /// Zachary Hall
        /// Created 2018/03/10
        /// 
        /// Event for when the New Location radio button is checked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void radNewLocation_Checked(object sender, RoutedEventArgs e)
        {
            lblHeader.Visibility = Visibility.Visible;
            txtStreet.Visibility = Visibility.Visible;
            txtCity.Visibility = Visibility.Visible;
            cboStates.Visibility = Visibility.Visible;
            chkJobLocationActive.Visibility = Visibility.Visible;
            txtZipCode.Visibility = Visibility.Visible;

            lblState.Visibility = Visibility.Visible;
            lblStreet.Visibility = Visibility.Visible;
            lblCity.Visibility = Visibility.Visible;
            lblActive.Visibility = Visibility.Visible;
            lblZipCode.Visibility = Visibility.Visible;

            stkReturningJobLocation.Visibility = Visibility.Hidden;
        }

        /// <summary>
        /// Zachary Hall
        /// Created 2018/03/10
        /// 
        /// Validates up to the Service Packages tab
        /// If these tabs are not valid, will return to those tabs 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TabItem_Selected_1(object sender, RoutedEventArgs e)
        {
            _isCustomerValid = validateCustomerTab();
            if (!_isCustomerValid)
            {
                e.Handled = true;
                tabCtrlMain.SelectedItem = tabCustomer;
                tabPackage.IsSelected = false;
                return;
            }
        }

        /// <summary>
        /// Zachary Hall
        /// Created 2018/03/10
        /// 
        /// Validates the Service Package tab.
        /// </summary>
        /// <returns></returns>
        private bool validatePackageTab()
        {
            if (!dgAssignedServicePackages.HasItems)
            {
                MessageBox.Show("You must select at least 1 package", "Invalid!", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                return false;
            }

            return true;
        }

        /// <summary>
        /// Zachary Hall
        /// Created 2018/03/10
        /// 
        /// Valdates up to the General tab. 
        /// If any tab is invalid, will move to that tab
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tabGeneral_Selected(object sender, RoutedEventArgs e)
        {

            _isCustomerValid = validateCustomerTab();
            if (!_isCustomerValid)
            {
                e.Handled = true;
                tabCtrlMain.SelectedItem = tabCustomer;
                tabGeneral.IsSelected = false;
                return;
            }

            _isPackageValid = validatePackageTab();
            if (!_isPackageValid)
            {
                e.Handled = true;
                tabCtrlMain.SelectedItem = tabPackage;
                tabGeneral.IsSelected = false;
                return;
            }

            _isLocationValid = validateLocationTab();
            if (!_isLocationValid)
            {
                e.Handled = true;
                tabCtrlMain.SelectedItem = tabLocation;
                tabGeneral.IsSelected = false;
                return;
            }
        }

        /// <summary>
        /// Zachary Hall
        /// Created 2018/03/10
        /// 
        /// Validates the location tab
        /// </summary>
        /// <returns></returns>
        private bool validateLocationTab()
        {
            if (radNewLocation.IsChecked == true)
            {
                return validateNewLocation();
            }
            if (radReturningLocation.IsChecked == true)
            {
                if (this.cboJobLocations.SelectedItem == null)
                {
                    MessageBox.Show("You must select a Location", "Invalid Input", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                    return false;
                }

            }

            if (dgAttributes.Items.Count == 0)
            {
                MessageBox.Show("You must edit attributes for the job location", "Invalid Input", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                return false;
            }


            return true;
        }

        /// <summary>
        /// Zachary Hall
        /// Created 2018/03/10
        /// 
        /// Validates a new location
        /// </summary>
        /// <returns></returns>
        private bool validateNewLocation()
        {
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

            return true;
        }

        /// <summary>
        /// Zachary Hall
        /// Created 2018/03/10
        /// 
        /// Validates the General tab
        /// </summary>
        /// <returns></returns>
        public bool validateGeneralTab()
        {
            if (timeTarget.Value == null)
            {
                MessageBox.Show("The target date cannot be empty", "Invalid Input", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                return false;
            }
            if (!StringValidations.IsValidNamePropertyEmpty(txtComments.Text))
            {
                MessageBox.Show("The comments cannot be empty", "Invalid Input", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                return false;
            }
            return true;
        }

        /// <summary>
        /// Zachary Hall
        /// Created 2018/03/10
        /// 
        /// Gets the address from street, city, state, and zipcode to pass to the JobLocation Street field
        /// </summary>
        /// <returns></returns>
        private string getAddressFromFields()
        {
            return txtStreet.Text + ", " + txtCity.Text + ", " + (string)cboStates.SelectedItem + ", " + txtZipCode.Text;
        }

        /// <summary>
        /// Zachary Hall
        /// Created 2018/03/10
        /// 
        /// Adds or Edits the data from the fields
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAddEdit_Click(object sender, RoutedEventArgs e)
        {
            if (_jobDetail == null)
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
        /// Created 2018/03/10
        /// 
        /// Performs the Edit fucntionality for a Job
        /// </summary>
        private void performEdit()
        {
            if (validateGeneralTab())
            {
                // declare variables
                int customerID;
                int jobLocationID;

                customerID = manageCustomerAdd();
                if (customerID == 0)
                {
                    return;
                }

                jobLocationID = manageJobLocationAdd(customerID);
                if (jobLocationID == 0)
                {
                    return;
                }

                //edit job location attributes
                int jobAttrResult = manageJobLocationAttributes(jobLocationID);
                if (jobAttrResult == -1)
                {
                    return;
                }

                // manage job service packages.
                int jobServicePackageResult = _jobManager.CreateUpdateJobServicePackage(_jobDetail.Job.JobID, (IEnumerable<ServicePackage>)(dgAssignedServicePackages.ItemsSource));

                // update job.
                manageJobEdit(customerID, jobLocationID);

                this.DialogResult = true;
                this.Close();
            }
        }

        /// <summary>
        /// Zachary Hall
        /// Created 2018/03/10
        /// 
        /// Rounds the seconds down, as DateTimePicker unwarrantly sets the seconds, for a nullable DateTime
        /// </summary>
        /// <param name="time"></param>
        /// <returns></returns>
        private DateTime? roundSecondsDown(DateTime? time)
        {
            if (time != null)
            {
                DateTime? rounded = ((DateTime)time).AddSeconds((((DateTime)time).Second) * -1);
                return rounded;
            }
            return time;
        }

        /// <summary>
        /// Zachary Hall
        /// Created 2018/03/10
        /// 
        /// Rounds the seconds down, as DateTimePicker unwarrantly sets the seconds, for a DateTime object
        /// </summary>
        /// <param name="time"></param>
        /// <returns></returns>
        private DateTime roundSecondsDown(DateTime time)
        {
            if (time != null)
            {
                DateTime rounded = time.AddSeconds((time.Second) * -1);
                return rounded;
            }
            return time;
        }

        /// <summary>
        /// Zachary Hall
        /// Created 2018/03/10
        /// 
        /// Mangages the editing of a job
        /// </summary>
        /// <param name="customerID"></param>
        /// <param name="jobLocationID"></param>
        private void manageJobEdit(int customerID, int jobLocationID)
        {
            int jobID = 0;
            try
            {
                var newJob = new Job()
                {

                    DateScheduled = roundSecondsDown(timeScheduled.Value),
                    EmployeeID = _currentEmployee.EmployeeID,
                    JobLocationID = jobLocationID,
                    Comments = txtComments.Text,
                    CustomerID = customerID,
                    Active = (chkActive.IsChecked == true),
                    DateCompleted = roundSecondsDown(timeCompleted.Value),
                    DateTarget = roundSecondsDown((DateTime)timeTarget.Value)

                };
                bool result = _jobManager.UpdateJob(_jobDetail.Job, newJob);
            }
            catch (Exception ex)
            {

                var message = ex.Message;
                if (ex.InnerException != null)
                {
                    message += "\n\n" + ex.InnerException.Message;
                }
                //have to display the error
                MessageBox.Show(message, "Create Job Failed", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }

        }

        /// <summary>
        /// Zachary Hall
        /// Created 2018/03/10
        /// 
        /// Performs the Add functionality for a Job wit fields from the form
        /// </summary>
        private void performAdd()
        {
            if (validateGeneralTab())
            {
                // declare variables
                int customerID;
                int jobLocationID;
                int jobID;


                customerID = manageCustomerAdd();
                if (customerID == 0)
                {
                    return;
                }

                jobLocationID = manageJobLocationAdd(customerID);
                if (jobLocationID == 0)
                {
                    return;
                }

                //edit job location attributes
                int jobAttrResult = manageJobLocationAttributes(jobLocationID);
                if (jobAttrResult == -1)
                {
                    return;
                }

                // manage job
                jobID = manageJobAdd(customerID, jobLocationID);
                if (jobID == 0)
                {
                    return;
                }

                // manage job service packages.
                int jobServicePackageResult = _jobManager.CreateUpdateJobServicePackage(jobID, (IEnumerable<ServicePackage>)(dgAssignedServicePackages.ItemsSource));


                this.DialogResult = true;
                this.Close();
            }
        }

        /// <summary>
        /// Zachary Hall
        /// Created 2018/03/10
        /// 
        /// Manages the adding of a job record
        /// </summary>
        /// <param name="customerID"></param>
        /// <param name="jobLocationID"></param>
        /// <returns></returns>
        private int manageJobAdd(int customerID, int jobLocationID)
        {
            int jobID = 0;
            try
            {

                var job = new Job()
                {
                    DateScheduled = (timeScheduled.Value),
                    EmployeeID = _currentEmployee.EmployeeID,
                    JobLocationID = jobLocationID,
                    Comments = txtComments.Text,
                    CustomerID = customerID,
                    Active = (chkActive.IsChecked == true),
                    DateCompleted = (timeCompleted.Value),
                    DateTarget = (DateTime)(timeTarget.Value)


                };
                jobID = _jobManager.CreateJob(job);
            }
            catch (Exception ex)
            {

                var message = ex.Message;
                if (ex.InnerException != null)
                {
                    message += "\n\n" + ex.InnerException.Message;
                }
                //have to display the error
                MessageBox.Show(message, "Create Job Failed", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }

            return jobID;
        }

        /// <summary>
        /// Zachary Hall
        /// Created 2018/03/10
        /// 
        /// Manages the adding/editing of JobLocationAttributes
        /// </summary>
        /// <param name="jobLocationID"></param>
        /// <returns></returns>
        private int manageJobLocationAttributes(int jobLocationID)
        {
            int result = 0;
            var attributes = (IEnumerable<JobLocationAttribute>)(dgAttributes.ItemsSource);
            foreach (var item in attributes)
            {
                item.JobLocationID = jobLocationID;
            }
            try
            {
                result = _jobLocationManager.CreateUpdateJobLocationAttributes(attributes);
            }
            catch (Exception ex)
            {
                var message = ex.Message;
                if (ex.InnerException != null)
                {
                    message += "\n\n" + ex.InnerException.Message;
                }
                //have to display the error
                MessageBox.Show(message, "Create Job Location Attributes Failed", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                result = -1;
            }
            return result;
        }

        /// <summary>
        /// Zachary Hall
        /// Created 2018/03/10
        /// 
        /// Manages the adding of a JobLocation
        /// </summary>
        /// <param name="customerID"></param>
        /// <returns></returns>
        private int manageJobLocationAdd(int customerID)
        {
            int id = 0;

            if (radReturningLocation.IsChecked == true)
            {
                id = ((JobLocation)cboJobLocations.SelectedItem).JobLocationID;
            }
            else
            {

                try
                {
                    var jobLocation = new JobLocation()
                    {
                        CustomerID = customerID,
                        Street = txtStreet.Text,
                        City = txtCity.Text,
                        State = (string)(cboStates.SelectedItem),
                        ZipCode = txtZipCode.Text,
                        Comments = "none"
                    };
                    id = _jobLocationManager.CreateJobLocation(jobLocation);
                }
                catch (Exception ex)
                {

                    var message = ex.Message;
                    if (ex.InnerException != null)
                    {
                        message += "\n\n" + ex.InnerException.Message;
                    }
                    //have to display the error
                    MessageBox.Show(message, "Create New Job Location Failed", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                }

            }
            return id;
        }

        /// <summary>
        /// Zachary Hall
        /// Created 2018/03/10
        /// 
        /// Manages the adding of a customer
        /// </summary>
        /// <returns></returns>
        private int manageCustomerAdd()
        {
            int customerID = 0;
            // manage customer
            if (radReturningCustomer.IsChecked == true)
            {
                customerID = ((Customer)(cboCustomers.SelectedItem)).CustomerID;
            }
            else // create customer
            {
                try
                {
                    var customer = new Customer()
                    {
                        CustomerTypeID = ((CustomerType)(cboCustomerTypes.SelectedItem)).CustomerTypeID,
                        Email = txtEmail.Text,
                        FirstName = txtFirstName.Text,
                        LastName = txtLastName.Text,
                        PhoneNumber = txtPhone.Text,
                    };
                    customerID = _customerManager.CreateCustomer(customer);
                }
                catch (Exception ex)
                {

                    var message = ex.Message;
                    if (ex.InnerException != null)
                    {
                        message += "\n\n" + ex.InnerException.Message;
                    }
                    //have to display the error
                    MessageBox.Show(message, "Create New Customer Failed", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                }

            }
            return customerID;
        }

        /// <summary>
        /// Zachary Hall
        /// Created 2018/03/10
        /// 
        /// Cancels the form
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

        /// <summary>
        /// Zachary Hall
        /// Created 2018/03/10
        /// 
        /// Event for when the selection is changed from the customers dropdown. Populates the JobLocationList based on the customer and the JobLocationAttributeDataGrid
        /// if the JobLocation is set to Returning
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cboCustomers_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            populateJobLocationList();

            if (radReturningLocation.IsChecked == true)
            {
                populateJobLocationAttributeDataGrid();
            }

        }

        /// <summary>
        /// Zachary Hall
        /// Created 2018/03/10
        /// 
        /// Repopulates the joblocation datagrid when the JobLocations dropdown is changed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cboJobLocations_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cboJobLocations.SelectedItem != null)
            {
                populateJobLocationAttributeDataGrid();
            }
        }

        /// <summary>
        /// Zachary Hall
        /// Created 2018/03/10
        /// 
        /// Event for when Scheduled time changes. Checks if timeCompleted needs to change based on its value
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timeScheduled_ValueChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            timeCompleted.IsEnabled = true;
            if (timeCompleted.Value != null)
            {
                if (timeCompleted.Value < timeScheduled.Value)
                {
                    timeCompleted.Value = null;
                }
            }
            timeCompleted.Minimum = timeScheduled.Value;
        }
    }
}
