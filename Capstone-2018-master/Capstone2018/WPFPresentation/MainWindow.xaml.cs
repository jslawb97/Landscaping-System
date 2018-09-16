using DataObjects;
using Logic;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WPFPresentation
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	/// <remarks>
	/// Zachary Hall
	/// Updated 2018/01/30
	/// </remarks>
	public partial class MainWindow : Window
	{
		private List<TaskTypeEmployeeNeedDetail> _taskTypeEmployeeNeedDetailList = new List<TaskTypeEmployeeNeedDetail>();
		private ITaskTypeEmployeeNeedManager _taskTypeEmployeeNeedManager = new TaskTypeEmployeeNeedManager();

		private IAvailabilityManager _availabilityManager = new AvailabilityManager();

		private IEquipmentDetailManager _equipmentDetailManager = new EquipmentDetailManager();
		private List<EquipmentDetail> _equipmentDetailList = new List<EquipmentDetail>();

		private IJobLocationAttributeTypeManager _jobLocationAttributeTypeManager = new JobLocationAttributeTypeManager();
		private List<JobLocationAttributeType> _jobLocationAttributeTypeList = new List<JobLocationAttributeType>();

		private ITaskTypeEquipmentNeedManager _taskTypeEquipmentNeedManager = new TaskTypeEquipmentNeedManager();
		private List<TaskTypeEquipmentNeed> _taskTypeEquipmentNeedList = new List<TaskTypeEquipmentNeed>();
		private List<TaskTypeEquipmentNeedDetail> _taskTypeEquipmentNeedDetailList = new List<TaskTypeEquipmentNeedDetail>();

		private ISpecialOrderItemManager _specialOrderItemManager = new SpecialOrderItemManager();
		private List<SpecialItem> _specialOrderItemList = new List<SpecialItem>();

		private IJobLocationManager _jobLocationManager = new JobLocationManager();
		private List<JobLocationDetail> _jobLocationDetailList = new List<JobLocationDetail>();

		private IJobManager _jobManager = new JobManager();
		private List<JobDetail> _jobDetailList = new List<JobDetail>();

		private TaskManager _taskManager = new TaskManager();
		private List<DataObjects.Task> _taskList;

		private TaskTypeManager _taskTypeManager = new TaskTypeManager();
		private List<TaskType> _taskTypeList;

		private ISupplyOrderManager _supplyOrderManager = new SupplyOrderManager();
		private ISupplyOrderItemManager _supplyOrderItemManager = new SupplyOrderItemManager();
		private List<SupplyOrder> _supplyOrderList;

		private ITaskTypeSupplyNeedManager _taskTypeSupplyNeedManager = new TaskTypeSupplyNeedManager();
		private List<TaskTypeSupplyNeed> _taskTypeSupplyNeedList = new List<TaskTypeSupplyNeed>();

		private UserManager _userManager = new UserManager();
		private User _user = null;

		private IServicePackageManager _servicePackageManager = new ServicePackageManager();
		private List<ServicePackage> _servicePackageList = new List<ServicePackage>();

		private IPrepChecklistManager _prepChecklistManager;
		private List<PrepChecklist> _prepChecklist;

		private List<ServiceItem> _serviceItem;

		private List<Certification> _certificationList;
		private ICertificationManager _certificationManager = new CertificationManager();

		private List<Customer> _customerList;
		private ICustomerManager _customerManager = new CustomerManager();

		private List<Employee> _employeeList = new List<Employee>();
		private IEmployeeManager _employeeManager = new EmployeeManager();

		private List<MakeModelDetail> _makeModelList = new List<MakeModelDetail>();
		private IMakeModelManager _makeModelManager = new MakeModelManager();

		private List<EquipmentType> _equipmentTypeList = new List<EquipmentType>();
		private IEquipmentTypeManager _equipmentTypeManager = new EquipmentTypeManager();

		private List<EmployeeCertification> _employeeCertificationList = new List<EmployeeCertification>();
		private List<EmployeeCertificationDetail> _employeeCertificationDetailList;
		private IEmployeeCertificationManager _employeeCertificationManager = new EmployeeCertificationManager();

		private List<EquipmentStatus> _equipmentStatusList;
		private IEquipmentStatusManager _equipmentStatusManager = new EquipmentStatusManager();

		private ISupplyItemManager _supplyItemManager = new SupplyItemManager();
		private List<SupplyItem> _supplyItemList;

		private List<MaintenanceChecklist> _maintenanceChecklistList = new List<MaintenanceChecklist>();
		private IMaintenanceChecklistManager _maintenanceChecklistManager = new MaintenanceChecklistManager();

		private ISourceManager _sourceManager = new SourceManager();
		private List<Source> _source;
		private List<SourceDetail> _sourceDetail;

		private List<PaymentType> _paymentTypeList = new List<PaymentType>();
		private IPaymentTypeManager _paymentTypeManager = new PaymentTypeManager();

		private readonly InspectionChecklistManager _inspectionChecklistManager;
		private readonly ServiceItemManager _serviceItemManager;
		private IVendorManager _vendorManager = new VendorManager();
		private List<Vendor> _vendorList;

		private ICustomerTypeManager _customerTypeManager = new CustomerTypeManager();
		private List<CustomerType> _customerTypeList;
		private List<TimeOffRequest> _timeOffRequestList;
		private ITimeOffRequestManager _timeOffRequestManager = new TimeOffRequestManager();
		private List<ResupplyOrder> _resupplyOrderList;
		private IResupplyOrderManager _resupplyOrderManager = new ResupplyOrderManager();
		private IServiceOfferingManager _serviceOfferingManager = new ServiceOfferingManager();
		private List<ServiceOffering> _serviceOfferingList = new List<ServiceOffering>();
		private IServiceOfferingItemManager _serviceOfferingItemManager = new ServiceOfferingItemManager();
		private IResupplyOrderLineManager _resupplyOrderLineManager = new ResupplyOrderLineManager();

		private List<SpecialOrder> _specialOrderList = new List<SpecialOrder>();
		private ISpecialOrderManager _specialOrderManager = new SpecialOrderManager();

		private ISpecialOrderLineManager _specialOrderLineManager = new SpecialOrderLineManager();

		private List<EmployeeRole> _employeeRoleList;
		private List<EmployeeRoleDetail> _employeeRoleDetailList;
		private IEmployeeRoleManager _employeeRoleManager = new EmployeeRoleManager();

		private IEquipmentManager _equipmentManager = new EquipmentManager();
		private List<Equipment> _equipmentList = new List<Equipment>();

		private IInspectionRecordManager _inspectionRecordManager = new InspectionRecordManager();
		private List<InspectionRecordDetail> _inspectionRecordDetailList = new List<InspectionRecordDetail>();

		private RoleManager _roleManager = new RoleManager();
		private List<Role> _roleList = new List<Role>();

        private IMaintenanceRecordManager _maintenanceRecordManager = new MaintenanceRecordManager();
        private List<MaintenanceRecordDetail> _maintenanceRecordDetailList = new List<MaintenanceRecordDetail>();

        private IPrepRecordManager _prepRecordManager = new PrepRecordManager();
        private List<PrepRecordDetail> _prepRecordDetailList = new List<PrepRecordDetail>();


        /// <summary>
        /// Constructor for MainWindow.
        /// Initializes the ISpecialOrderItemManager
        /// </summary>
        /// <remarks>
        /// Zachary Hall
        /// Updated 2018/01/30
        /// </remarks>
        public MainWindow()
		{
			_inspectionChecklistManager = new InspectionChecklistManager();
			_maintenanceChecklistManager = new MaintenanceChecklistManager();
			_serviceItemManager = new ServiceItemManager();
			_prepChecklistManager = new PrepChecklistManager();
			InitializeComponent();
		}


		/// <summary>
		/// Badis Saidani
		/// Created: 2018/01/31
		/// 
		/// Constructs and submits a new event using a form filled out by a user.
		/// </summary>
		/// 
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnLogin_Click(object sender, RoutedEventArgs e)
		{
			if (_user != null) // this means someone is logged in, so log out!
			{
				logout();
				return;
			}

			// accept the input
			var username = txtUsername.Text;
			var password = txtPassword.Password;

			// check for missing or invalid data
			if (username.Length < Constants.MINUSERNAMELENGTH ||
				username.Length > Constants.MAXUSERNAMELENGTH)
			{
				MessageBox.Show("Invalid Username", "Login Failed!",
					MessageBoxButton.OK, MessageBoxImage.Exclamation);

				clearLogin();

				return;
			}
			if (password.Length <= Constants.MINPASSWORDLENGTH)
			{
				MessageBox.Show("Invalid Password", "Login Failed!",
					MessageBoxButton.OK, MessageBoxImage.Exclamation);

				clearLogin();
				return;
			}




			try
			{
				_user = _userManager.AuthenticateUser(username, password);

				if (_user.Roles.Count == 0)
				{
					// check for unauthorized user
					_user = null;

					MessageBox.Show("You have not been assigned any roles. \nYou will be logged out. \nPlease see your supervisor.",
						"Unauthorized Employee", MessageBoxButton.OK,
						MessageBoxImage.Stop);

					clearLogin();

					return;
				}
				// user is now logged in
				var message = "Welcome back, " + _user.Employee.FirstName +
					". You are logged in as: ";
				foreach (var r in _user.Roles)
				{
					message += r.RoleID + "   ";
				}

				showUserTabs();
				sbStatus.Items[0] = message;

				clearLogin();
				txtPassword.Visibility = Visibility.Hidden;
				txtUsername.Visibility = Visibility.Hidden;
				lblPassword.Visibility = Visibility.Hidden;
				lblUsername.Visibility = Visibility.Hidden;


				this.btnLogin.IsDefault = false;
				btnLogin.Content = "Log Out";


			}
			catch (Exception ex) // nowhere to throw an exception at the presentation layer
			{
				string message = ex.Message;

				if (ex.InnerException != null)
				{
					message += "\n\n" + ex.InnerException.Message;
				}

				MessageBox.Show(message, "Login Failed!",
					MessageBoxButton.OK, MessageBoxImage.Exclamation);

				clearLogin();
				return;
			}
		}


		/// <summary>
		/// Badis Saidani
		/// Created: 2018/01/31
		/// 
		/// Return all attributes to their initialization status
		/// and hide all tabs.
		/// </summary>
		private void logout()
		{
			_user = null;
			// do anything else we need to do to clear the screen, to be done later

			// reenable the login controls
			txtPassword.Visibility = Visibility.Visible;
			txtUsername.Visibility = Visibility.Visible;
			lblPassword.Visibility = Visibility.Visible;
			lblUsername.Visibility = Visibility.Visible;
			btnLogin.Content = "Log In";
			clearLogin();
			sbStatus.Items[0] = "You are not logged in.";

			hideAllTabs();
		}



		/// <summary>
		/// Badis Saidani
		/// Created: 2018/01/31
		/// 
		/// Clears the text boxes of username and password.
		/// And focus the cursor on the username textbox.
		/// </summary>
		private void clearLogin()
		{
			this.btnLogin.IsDefault = true;
			txtUsername.Text = "";
			txtPassword.Password = "";
			txtUsername.Focus();
		}


		/// <summary>
		/// Badis Saidani
		/// Created: 2018/01/31
		/// 
		/// Constructs and submits a new event using a form filled out by a user.
		/// it shows and hides tabs depending on the role of the user.
		/// </summary>
		/// 
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void Window_Loaded(object sender, RoutedEventArgs e)
		{
			txtUsername.Focus();
			this.btnLogin.IsDefault = true;
			hideAllTabs();

		}

		/// <summary>
		/// Badis Saidani
		/// Created: 2018/01/31
		/// 
		/// Hides all tabs.
		/// </summary>
		private void hideAllTabs()
		{
			foreach (var tab in tabsetMain.Items)
			{
				// collapse all the tabs
				((TabItem)tab).Visibility = Visibility.Collapsed;
				// hide the tabset completely
				tabsetMain.Visibility = Visibility.Hidden;
			}
		}

		/// <summary>
		/// Badis Saidani
		/// Created: 2018/01/31
		/// 
		/// Shows User tabs depending on his roler.
		/// </summary>
		private void showUserTabs()
		{

			foreach (var r in _user.Roles)
			{
				if (r.RoleID == "Manager")
				{
					foreach (var tab in tabsetMain.Items)
					{
						// collapse all the tabs
						((TabItem)tab).Visibility = Visibility.Visible;
						// hide the tabset completely
						tabsetMain.Visibility = Visibility.Visible;
					}
                    foreach (var tab in tabsetEmployee.Items)
                    {
                        ((TabItem)tab).Visibility = Visibility.Visible;
                    }
                    foreach (var tab in tabsetEquipment.Items)
                    {
                        ((TabItem)tab).Visibility = Visibility.Visible;
                    }
                    foreach (var tab in tabsetSupply.Items)
                    {
                        ((TabItem)tab).Visibility = Visibility.Visible;
                    }
                    foreach (var tab in tabsetCustomer.Items)
                    {
                        ((TabItem)tab).Visibility = Visibility.Visible;
                    }
                    foreach (var tab in tabsetServices.Items)
                    {
                        ((TabItem)tab).Visibility = Visibility.Visible;
                    }
                    foreach (var tab in tabsetJob.Items)
                    {
                        ((TabItem)tab).Visibility = Visibility.Visible;
                    }
				}

				if (r.RoleID == "HR Manager" || r.RoleID == "Foreman" ||
					r.RoleID == "Labor Scheduler" || r.RoleID == "Worker" || r.RoleID == "Job Scheduler")
				{
					tabEmployees.Visibility = Visibility.Visible;
					tabEmployees.IsSelected = true;

					switch (r.RoleID)
					{
						case "Labor Scheduler":
							tabEmployees.Visibility = Visibility.Visible;
							Roles.Visibility = Visibility.Hidden;
							Certifications.Visibility = Visibility.Visible;
							EmployeeRoles.Visibility = Visibility.Hidden;
							break;
						case "Worker":
							tabEmployees.Visibility = Visibility.Hidden;
							Roles.Visibility = Visibility.Hidden;
							Certifications.Visibility = Visibility.Visible;
							EmployeeRoles.Visibility = Visibility.Hidden;
							break;
						case "Job Scheduler":
							tabJobs.Visibility = Visibility.Visible;
							tabJob.Visibility = Visibility.Visible;
							tabJobLocations.Visibility = Visibility.Visible;
							break;
					}

				}
				if (r.RoleID == "Inspector" || r.RoleID == "Mechanic" || r.RoleID == "Equipment Scheduler"
					|| r.RoleID == "Maintenance" || r.RoleID == "Supply Clerk")
				{
					tabEquipment.Visibility = Visibility.Visible;
					tabEquipment.IsSelected = true;

					switch (r.RoleID)
					{
						case "Inspector":
							InspectionChecklistTab.Visibility = Visibility.Visible;
							tabPrepChecklist.Visibility = Visibility.Visible;
							tiMaintChecklist.Visibility = Visibility.Visible;
							tabMakeModel.Visibility = Visibility.Collapsed;
							tabEquipmentType.Visibility = Visibility.Collapsed;
							tabEquipmentStatus.Visibility = Visibility.Collapsed;
							break;
						case "Mechanic":
							InspectionChecklistTab.Visibility = Visibility.Collapsed;
							tabPrepChecklist.Visibility = Visibility.Visible;
							tiMaintChecklist.Visibility = Visibility.Visible;
							tabMakeModel.Visibility = Visibility.Collapsed;
							tabEquipmentType.Visibility = Visibility.Collapsed;
							tabEquipmentStatus.Visibility = Visibility.Visible;
							break;
						case "Equipment Scheduler":
							InspectionChecklistTab.Visibility = Visibility.Collapsed;
							tabPrepChecklist.Visibility = Visibility.Collapsed;
							tiMaintChecklist.Visibility = Visibility.Collapsed;
							tabMakeModel.Visibility = Visibility.Visible;
							tabEquipmentType.Visibility = Visibility.Visible;
							tabEquipmentStatus.Visibility = Visibility.Visible;
							break;
						case "Maintenance":
							InspectionChecklistTab.Visibility = Visibility.Collapsed;
							tabPrepChecklist.Visibility = Visibility.Visible;
							tiMaintChecklist.Visibility = Visibility.Visible;
							tabMakeModel.Visibility = Visibility.Collapsed;
							tabEquipmentType.Visibility = Visibility.Collapsed;
							tabEquipmentStatus.Visibility = Visibility.Collapsed;
							break;
						case "Supply Clerk":
							InspectionChecklistTab.Visibility = Visibility.Collapsed;
							tabPrepChecklist.Visibility = Visibility.Collapsed;
							tiMaintChecklist.Visibility = Visibility.Collapsed;
							tabMakeModel.Visibility = Visibility.Collapsed;
							tabEquipmentType.Visibility = Visibility.Visible;
							tabEquipmentStatus.Visibility = Visibility.Visible;
							break;
					}
				}
				if (r.RoleID == "Supply Clerk" || r.RoleID == "Delivery Schedule" || r.RoleID == "Foreman"
					|| r.RoleID == "Unloader/Loader" || r.RoleID == "Delivery" || r.RoleID == "Employee")
				{
					tabSupply.Visibility = Visibility.Visible;
					tabSupply.IsSelected = true;

					switch (r.RoleID)
					{
						case "Supply Clerk":
							tabSupplyItem.Visibility = Visibility.Visible;
							tabSpecialOrderItem.Visibility = Visibility.Visible;
							tabVendor.Visibility = Visibility.Visible;
							tabSource.Visibility = Visibility.Visible;
							break;
						case "Delivery Schedule":
							tabSupplyItem.Visibility = Visibility.Visible;
							tabSpecialOrderItem.Visibility = Visibility.Visible;
							tabVendor.Visibility = Visibility.Visible;
							tabSource.Visibility = Visibility.Visible;
							break;

						case "Unloader/Loader":
							tabSupplyItem.Visibility = Visibility.Visible;
							btnAddSupply.Visibility = Visibility.Hidden;
							btnEditSupply.Visibility = Visibility.Hidden;
							btnDeactivateSupply.Visibility = Visibility.Hidden;

							tabSpecialOrderItem.Visibility = Visibility.Visible;
							btnAddSpecialOrder.Visibility = Visibility.Hidden;
							btnEditSpecialOrder.Visibility = Visibility.Hidden;
							btnDeleteSpecialOrder.Visibility = Visibility.Hidden;

							tabVendor.Visibility = Visibility.Hidden;
							tabSource.Visibility = Visibility.Hidden;
							break;
						case "Delivery":
							tabSupplyItem.Visibility = Visibility.Hidden;
							tabSpecialOrderItem.Visibility = Visibility.Visible;
							tabVendor.Visibility = Visibility.Hidden;
							tabSource.Visibility = Visibility.Hidden;
							break;
						case "Employee":
							tabSupplyItem.Visibility = Visibility.Visible;
							btnAddSupply.Visibility = Visibility.Hidden;
							btnEditSupply.Visibility = Visibility.Hidden;
							btnDeactivateSupply.Visibility = Visibility.Hidden;

							tabSpecialOrderItem.Visibility = Visibility.Visible;
							btnAddSpecialOrder.Visibility = Visibility.Hidden;
							btnEditSpecialOrder.Visibility = Visibility.Hidden;
							btnDeleteSpecialOrder.Visibility = Visibility.Hidden;

							tabVendor.Visibility = Visibility.Hidden;
							tabSource.Visibility = Visibility.Hidden;

							break;
					}
				}


			}
			tabsetMain.Visibility = Visibility.Visible;
		}

		private void refreshPrepChecklist()
		{
			try
			{

				_prepChecklist = _prepChecklistManager.RetrievePrepChecklist();
				dgPrepChecklist.ItemsSource = _prepChecklist;
			}
			catch (Exception ex)
			{
				var message = ex.Message + "\n\n" + ex.InnerException;
				MessageBox.Show(message, "Error Retrieving Prep Checklist!", MessageBoxButton.OK, MessageBoxImage.Exclamation);
			}
		}

		/// <summary>
		///		Sam Dramstad
		///		Created 2018/02/20
		///		
		///		Refreshes the customers data grid to populate it with data
		/// </summary>
		/// <param name="active"></param>
		private void refreshCustomerList(bool active = true)
		{
			dgCustomers.ItemsSource = null;
			dgCustomers.Items.Clear();
			try
			{
				_customerList = _customerManager.RetrieveCustomerListByActive(true);
				dgCustomers.ItemsSource = _customerList;
			}
			catch (Exception ex)
			{
				var message = ex.Message + "\n\n" + ex.InnerException.Message;

				Dispatcher.BeginInvoke(new Action(() => MessageBox.Show(message, "Data Retrieval Error.",
					MessageBoxButton.OK, MessageBoxImage.Exclamation)),
					System.Windows.Threading.DispatcherPriority.Normal);
			}
		}

		/// <summary>
		///     Sam Dramstad
		///     Created 2018/02/20
		///     
		///     Calls the refresh customers method whenever the customer tab is selected
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void TabItem_Customer_Selected(object sender, RoutedEventArgs e)
		{
			refreshCustomerList();
		}

		private void TabItem_Equipment_Selected(object sender, RoutedEventArgs e)
		{
			refreshEquipmentList();
		}

		private void refreshEquipmentList()
		{
			dgEquipment.ItemsSource = null;
			dgEquipment.Items.Clear();
			try
			{
				_equipmentDetailList = _equipmentDetailManager.RetrieveEquipmentDetailList();
				dgEquipment.ItemsSource = _equipmentDetailList;
			}
			catch (Exception ex)
			{
				var message = ex.Message + "\n\n" + ex.InnerException.Message;
				MessageBox.Show(message, "Data Retrieval Error.",
					MessageBoxButton.OK, MessageBoxImage.Exclamation);
			}
		}

		/// <summary>
		/// Sam Dramstad
		/// Created on 4/24/2018
		/// 
		/// Presents a prompt to the user before deactivating a customer.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnDeactivateCustomer_Click(object sender, RoutedEventArgs e)
		{
			try
			{
				if (this.dgCustomers.SelectedItems.Count > 0)
				{

					if (((Customer)this.dgCustomers.SelectedItem).Active == false)
					{
						MessageBox.Show("Customer already deactivated!");
					}
					else
					{
						MessageBoxResult result = MessageBox.Show("Are you sure you want to deactivate Customer "
							+ ((Customer)this.dgCustomers.SelectedItem).FirstName + ((Customer)this.dgCustomers.SelectedItem).LastName
							+ "?", "Deactivation Warning", MessageBoxButton.YesNo);
						if (result == MessageBoxResult.No)
						{
							return;
						}
						_customerManager.DeactivateCustomer(((Customer)this.dgCustomers.SelectedItem).CustomerID);
						refreshCustomerList();
					}

				}
				else
				{
					MessageBox.Show("You must select something!");
				}
			}
			catch (Exception ex)
			{

				var message = ex.Message + "\n\n" + ex.InnerException;
				MessageBox.Show(message, "Deactivation Error!", MessageBoxButton.OK, MessageBoxImage.Exclamation);
			}

		}

		private void TabPrepChecklist_Selected(object sender, RoutedEventArgs e)
		{
			refreshPrepChecklist();
		}

		private void btnAddPrepChecklist_Click(object sender, RoutedEventArgs e)
		{
			var prepChecklistForm = new frmAddEditPrepChecklist(_prepChecklistManager);
			var result = prepChecklistForm.ShowDialog();
			if (result == true)
			{
				refreshPrepChecklist();
			}
		}

		private void btnEditPrepChecklist_Click(object sender, RoutedEventArgs e)
		{
			if (this.dgPrepChecklist.SelectedItems.Count > 0)
			{
				var editForm = new frmAddEditPrepChecklist(_prepChecklistManager, (PrepChecklist)this.dgPrepChecklist.SelectedItem);
				var result = editForm.ShowDialog();
				if (result == true)
				{
					refreshPrepChecklist();
				}
			}
		}

		private void btnDeactivatePrepChecklist_Click(object sender, RoutedEventArgs e)
		{
			try
			{
				if (this.dgPrepChecklist.SelectedItems.Count > 0)
				{

					if (((PrepChecklist)this.dgPrepChecklist.SelectedItem).Active == false)
					{
						MessageBox.Show("Prep Checklist already deactivated!");
					}
					else
					{
						MessageBoxResult result = MessageBox.Show("Are you sure you want to Deactivate Item "
							+ ((PrepChecklist)this.dgPrepChecklist.SelectedItem).PrepChecklistID
							+ "?", "Deactivation Warning", MessageBoxButton.YesNo);
						if (result == MessageBoxResult.No)
						{
							return;
						}
						_prepChecklistManager.DeactivatePrepChecklistByID(((PrepChecklist)this.dgPrepChecklist.SelectedItem).PrepChecklistID);
						refreshPrepChecklist();
					}

				}
				else
				{
					MessageBox.Show("You must select something!");
				}
			}
			catch (Exception ex)
			{

				var message = ex.Message + "\n\n" + ex.InnerException;
				MessageBox.Show(message, "Deactivation Error!", MessageBoxButton.OK, MessageBoxImage.Exclamation);
			}
		}




		///  SERVICE ITEM CODE

		/// <summary>
		/// Jacob Slaubaugh
		/// Created 2018/02/18
		/// 
		/// Refreshes the ServiceItem data grid to populate with data
		/// </summary>
		private void refreshServiceItem()
		{
			dgServiceItem.ItemsSource = null;
			dgServiceItem.Items.Clear();
			try
			{
				_serviceItem = _serviceItemManager.RetrieveServiceItemList();
				dgServiceItem.ItemsSource = _serviceItem;
			}
			catch (Exception ex)
			{
				var message = ex.Message + "\n\n" + ex.InnerException.Message;
				MessageBox.Show(message, "Data Retrieval Error.", MessageBoxButton.OK, MessageBoxImage.Exclamation);
			}
		}

		private void btnDeactivateServiceItem_Click(object sender, RoutedEventArgs e)
		{

			try
			{
				if (this.dgServiceItem.SelectedItems.Count > 0)
				{

					if (((ServiceItem)this.dgServiceItem.SelectedItem).Active == false)
					{
						MessageBox.Show("Service Item already deactivated!");
					}
					else
					{
						MessageBoxResult result = MessageBox.Show("Are you sure you want to Deactivate this Service Item "
							+ ((ServiceItem)this.dgServiceItem.SelectedItem).ServiceItemID
							+ "?", "Deactivation Warning", MessageBoxButton.YesNo);
						if (result == MessageBoxResult.No)
						{
							return;
						}
						_serviceItemManager.DeactivateServiceItemByID(((ServiceItem)this.dgServiceItem.SelectedItem).ServiceItemID);
						refreshServiceItem();
					}

				}
				else
				{
					MessageBox.Show("You must select something!");
				}
			}
			catch (Exception ex)
			{

				var message = ex.Message + "\n\n" + ex.InnerException;
				MessageBox.Show(message, "Deactivation Error!", MessageBoxButton.OK, MessageBoxImage.Exclamation);
			}

		}



		private void btnEditServiceItem_Click(object sender, RoutedEventArgs e)
		{


			if (this.dgServiceItem.SelectedItems.Count > 0)
			{
				// Refresh service offering list and pass in to window.
				refreshServiceOfferingsList();
				var editForm = new frmAddEditServiceItem(_serviceItemManager, (ServiceItem)this.dgServiceItem.SelectedItem, _serviceOfferingList);
				var result = editForm.ShowDialog();
				if (result == true)
				{
					refreshServiceItem();
				}
			}


		}

		private void TabServiceItems_Selected(object sender, RoutedEventArgs e)
		{
			refreshServiceItem();
		}


		private void btnAddServiceItem_Click(object sender, RoutedEventArgs e)
		{
			// Refresh list of service offerings and pass to window.
			refreshServiceOfferingsList();
			var serviceItemForm = new frmAddEditServiceItem(_serviceItemManager, _serviceOfferingList);
			var result = serviceItemForm.ShowDialog();
			if (result == true)
			{
				refreshServiceItem();
			}
		}

		/// <summary>
		/// James McPherson
		/// Created 2018/02/13
		/// 
		/// Deactivates the selected EmployeeCertification
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnDeactivateEmployeeCertification_Click(object sender, RoutedEventArgs e)
		{
			EmployeeCertificationDetail selectedEmployeeCertificationDetail = (EmployeeCertificationDetail)dgEmployeeCertification.SelectedItem;
			if (selectedEmployeeCertificationDetail != null)
			{
				try
				{
					_employeeCertificationManager.DeactivateEmployeeCertificationByID(selectedEmployeeCertificationDetail.Employee.EmployeeID
						, selectedEmployeeCertificationDetail.Certification.CertificationID);
					refreshEmployeeCertificationList();
					MessageBox.Show("Employee certification deactivated.");
				}
				catch
				{
					MessageBox.Show("Failed to deactivate employee certification.");
				}
			}
			else
			{
				MessageBox.Show("You must select an employee certification to deactivate.");
			}
		}

		/// <summary>
		/// James McPherson
		/// Created 2018/02/13
		/// 
		/// Deactivates the selected Certification
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnDeactivateCertification_Click(object sender, RoutedEventArgs e)
		{
			Certification selectedCertification = (Certification)dgCertification.SelectedItem;
			if (selectedCertification != null)
			{
				try
				{
					_certificationManager.DeactivateCertificationByID(selectedCertification.CertificationID);
					refreshCertificationsList();
					MessageBox.Show("Certification deactivated.");
				}
				catch
				{
					MessageBox.Show("Failed to deactivate certification.");
				}
			}
			else
			{
				MessageBox.Show("You must select a certification to deactivate.");
			}
		}

		/// <summary>
		///     James McPherson
		///     Created 2018/02/13
		///     
		///     Deactivates the selected EquipmentType
		/// </summary>
        /// <remarks>
        /// Noah Davison
        /// Modified 2018/05/09
        /// 
        /// modified so it doesn't crash from the changes I made to RefreshEquipmentTypeList()
        /// </remarks>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnDeactivateEquipmentType_Click(object sender, RoutedEventArgs e)
		{
			EquipmentTypeDetail selectedEquipmentTypeDetail = (EquipmentTypeDetail)dgEquipmentType.SelectedItem;
			if (selectedEquipmentTypeDetail != null)
			{
				try
				{
					_equipmentTypeManager.DeactivateEquipmentTypeByID(selectedEquipmentTypeDetail.EquipmentType.EquipmentTypeID);
					refreshEquipmentTypeList();
					MessageBox.Show("Equipment type deactivated.");
				}
				catch
				{
					MessageBox.Show("Failed to deactivate equipment type.");
				}
			}
			else
			{
				MessageBox.Show("You must select an equipment type to deactivate.");
			}
		}

		private void InspectionChecklistTab_Selected(object sender, RoutedEventArgs e)
		{
			refreshInspectionChecklist();
		}

		/// <summary>
		/// Utility for updating the inspection checklist data grid with current data from the database.
		/// </summary>
		/// <remarks>
		/// Zach Murphy
		/// Updated 2018/2/1
		/// </remarks>
		private void refreshInspectionChecklist()
		{
			try
			{
				dgInspectionChecklist.ItemsSource = _inspectionChecklistManager.RetrieveInspectionChecklistItems();
			}
			catch (Exception ex)
			{
				MessageBox.Show("Error Retrieving Inspection Checklists!");
			}
		}

		/// John Miller
		/// Created: 2018/02/18
		/// 
		/// Reinstantiates the CustomerType list with up-to-date data from the database
		/// </summary>
		private void refreshCustomerTypeList()
		{
			dgCustomerTypes.ItemsSource = null;
			dgCustomerTypes.Items.Clear();
			try
			{
				dgCustomerTypes.ItemsSource = _customerTypeManager.RetrieveCustomerTypeList();

			}
			catch (Exception ex)
			{
				var message = ex.Message + "\n\n" + ex.InnerException;
				MessageBox.Show(message, "Error Retrieving Customer Types!", MessageBoxButton.OK, MessageBoxImage.Exclamation);
			}
		}

		/// <summary>
		/// Utility for adding an inspection checklist to the data grid with current data from the database.
		/// </summary>
		/// <remarks>
		/// Zach Murphy
		/// Updated 2018/2/1
		/// </remarks>
		private void btnAddInspectionChecklist_Click(object sender, RoutedEventArgs e)
		{
			var frmAddEditChecklist = new frmAddEditChecklist();
			try
			{
				frmAddEditChecklist.ShowAddDialog(this._inspectionChecklistManager);
			}
			catch (Exception ex)
			{
				MessageBox.Show("Failed to add inspection checklist.");
			}

			refreshInspectionChecklist();
		}

		/// <summary>
		/// Utility for editing an existing inspection checklist and updating the data grid with current data from the database.
		/// </summary>
		/// <remarks>
		/// Zach Murphy
		/// Updated 2018/2/1
		/// </remarks>
        /// <remarks>
        /// Noah Davison
        /// Updated 2018/05/10
        /// 
        /// Added message box for successful edit and no row selected
        /// </remarks>
		private void btnEditInspectionChecklist_Click(object sender, RoutedEventArgs e)
		{
			if (this.dgInspectionChecklist.SelectedItems.Count > 0)
			{
				try
				{
					var editForm = new frmAddEditChecklist();
					editForm.ShowEditDialog(this._inspectionChecklistManager,
						(InspectionChecklist)this.dgInspectionChecklist.SelectedItem);
					refreshInspectionChecklist();
                    MessageBox.Show("Successfully edited inspection checklist.");
				}
				catch (Exception ex)
				{
					MessageBox.Show("Failed to edit inspection checklist.");
				}
			}
            else
            {
                MessageBox.Show("You must select something!");
            }
		}

		/// <summary>
		///     James McPherson
		///     Created 2018/02/02
		///     
		///     Deactivates the selected employee
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnDeactivateEmployee_Click(object sender, RoutedEventArgs e)
		{
			Employee selectedEmployee = (Employee)dgEmployees.SelectedItem;
			if (selectedEmployee != null)
			{
				try
				{
					_employeeManager.DeactivateEmployeeByID(selectedEmployee.EmployeeID);
					refreshEmployeeList();
					MessageBox.Show("Employee deactivated.");
				}
				catch
				{
					MessageBox.Show("Failed to deactivate employee.");

				}
			}
			else
			{
				MessageBox.Show("You must select a employee to deactivate.");
			}
		}

		/// <summary>
		/// Utility for updating the maintenance checklist data grid with current data from the database.
		/// </summary>
		/// <remarks>
		/// Zach Murphy
		/// Updated 2018/2/2
		/// </remarks>
		private void refreshMaintenanceChecklist()
		{
			try
			{
                dgMaintenanceChecklist.ItemsSource = null;
				dgMaintenanceChecklist.ItemsSource = _maintenanceChecklistManager.RetrieveMaintenanceChecklistList();
			}
			catch (Exception ex)
			{
				MessageBox.Show("Error Retrieving Maintenence Checklists!");
			}
		}

		private void tiMaintChecklist_Selected(object sender, RoutedEventArgs e)
		{
			refreshMaintenanceChecklist();
		}

		private void btnAddMaintenanceChecklist_Click(object sender, RoutedEventArgs e)
		{
			try
			{
				new frmAddEditChecklist().ShowAddDialog(this._maintenanceChecklistManager);
				refreshMaintenanceChecklist();
			}
			catch (Exception ex)
			{
				MessageBox.Show("Failed to add maintenence checklist.");
			}
		}

		private void btnEditMaintenanceChecklist_Click(object sender, RoutedEventArgs e)
		{
			if (this.dgMaintenanceChecklist.SelectedItems.Count > 0)
			{
				try
				{
					var editForm = new frmAddEditChecklist();
					var result = editForm.ShowEditDialog(_maintenanceChecklistManager,
						(MaintenanceChecklist)this.dgMaintenanceChecklist.SelectedItem);
					if (result == true)
					{
						refreshMaintenanceChecklist();
					}
				}
				catch (Exception ex)
				{
					MessageBox.Show("Failed to edit maintenence checklist.");
				}
			}
			else
			{
				MessageBox.Show("You must select something!");
			}
		}

		/// <summary>
		/// James McPherson
		/// Created 2018/02/04
		/// 
		/// Handles the creation of a new MakeModel
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnAddMakeModel_Click(object sender, RoutedEventArgs e)
		{
			var makeModelForm = new frmAddEditMakeModel(_makeModelManager, _maintenanceChecklistManager);
			var result = makeModelForm.ShowDialog();
			if (result == true)
			{
				refreshMakeModelList();
			}
		}

		/// <summary>
		/// James McPherson
		/// Created 2018/02/04
		/// 
		/// Handles the editing of a MakeModel selected in the MakeModel datagrid
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnEditMakeModel_Click(object sender, RoutedEventArgs e)
		{
			MakeModel makeModel = null;
			if (this.dgMakeModel.SelectedItems.Count > 0)
			{
				try
				{
					makeModel = (MakeModel)dgMakeModel.SelectedItem;
					var makeModelForm = new frmAddEditMakeModel(_makeModelManager, _maintenanceChecklistManager
						, makeModel, DetailFormMode.Edit);
					var result = makeModelForm.ShowDialog();
					if (result == true)
					{
						refreshMakeModelList();
					}
				}
				catch (Exception ex)
				{
					MessageBox.Show(ex.Message + "\n\n" + ex.InnerException.Message);
				}
			}
			else
			{
				MessageBox.Show("You must select a makemodel to edit.");
			}
		}

		/// <summary>
		/// James McPherson
		/// Created 2018/02/13
		/// 
		/// Deactivates the selected MakeModel
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnDeactivateMakeModel_Click(object sender, RoutedEventArgs e)
		{
			MakeModel selectedMakeModel = (MakeModel)dgMakeModel.SelectedItem;
			if (selectedMakeModel != null)
			{
				try
				{
					_makeModelManager.DeactivateMakeModelByID(selectedMakeModel.MakeModelID);
					refreshMakeModelList();
					MessageBox.Show("Makemodel deactivated.");
				}
				catch
				{
					MessageBox.Show("Failed to deactivate makemodel.");
				}
			}
			else
			{
				MessageBox.Show("You must select a makemodel to deactivate.");
			}
		}

		/// <summary>
		/// James McPherson
		/// Created 2018/02/20
		/// 
		/// Handles the editing of a PaymentType selected in the PaymentType datagrid
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnEditPaymentType_Click(object sender, RoutedEventArgs e)
		{
			PaymentType paymentType = null;
			if (this.dgPaymentTypes.SelectedItems.Count > 0)
			{
				try
				{
					paymentType = (PaymentType)dgPaymentTypes.SelectedItem;
					var paymentTypeForm = new frmAddEditPaymentType(_paymentTypeManager, DetailFormMode.Edit
						, paymentType);
					var result = paymentTypeForm.ShowDialog();
					if (result == true)
					{
						refreshPaymentTypeList();
					}
				}
				catch (Exception ex)
				{
					MessageBox.Show(ex.Message + "\n\n" + ex.InnerException.Message);
				}
			}
			else
			{
				MessageBox.Show("You must select a payment type to edit.");
			}
		}

		/// <summary>
		/// James McPherson
		/// Created 2018/02/19
		/// 
		/// Deactivates the selected PaymentType
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnDeactivatePaymentType_Click(object sender, RoutedEventArgs e)
		{
			PaymentType selectedPaymentType = (PaymentType)dgPaymentTypes.SelectedItem;
			if (selectedPaymentType != null)
			{
				try
				{
					_paymentTypeManager.DeactivatePaymentTypeByID(selectedPaymentType.PaymentTypeID);
					refreshPaymentTypeList();
					MessageBox.Show("Payment type deactivated");
				}
				catch
				{
					MessageBox.Show("Failed to deactivate paymenttype.");
				}
			}
			else
			{
				MessageBox.Show("You must select a paymenttype to deactivate.");
			}
		}

		/// <summary>
		/// Brady Feller
		/// Created 2018/04/09
		/// 
		/// Displays the EquipmentTypeList
        /// 
        /// <remarks>
        /// Noah Davison
        /// Modified 2018/04/09
        /// 
        /// Fixed so right data is grabbed for EquipmentTypeList
        /// </remarks>
		/// </summary>
		private void refreshEquipmentTypeList()
		{
			try
			{
				dgEquipmentType.ItemsSource = _equipmentTypeManager.RetrieveEquipmentTypeDetailList();
			}
			catch (Exception ex)
			{
				MessageBox.Show("Error Retrieving Equipment Type List!" + ex);
			}
		}

		private void btnDeleteMaintenanceChecklist_Click(object sender, RoutedEventArgs e)
		{
			if (this.dgMaintenanceChecklist.SelectedItems.Count > 0)
			{
				try
				{
					this._maintenanceChecklistManager.DeactivateMaintenanceChecklist(((MaintenanceChecklist)this.dgMaintenanceChecklist.SelectedItem).MaintenanceChecklistID);
				}
				catch (Exception ex)
				{
					MessageBox.Show("Failed to deactivate maintenence checklist.");
				}

				refreshMaintenanceChecklist();
			}
			else
			{
				MessageBox.Show("You must select something!");
			}
		}

		/// John Miller
		/// Created: 2018/02/15
		/// 
		/// Reinstantiates the Vendor list with up-to-date data from the database
		/// </summary>
		private void refreshVendorList()
		{
			try
			{

				//_employeeList = _employeeManager.RetrieveEmployeeListByActive();
				//dgEmployees.ItemsSource = _employeeList;

				_vendorList = _vendorManager.RetrieveVendorList();
				dgVendor.ItemsSource = _vendorList;
			}
			catch (Exception ex)
			{
				var message = ex.Message + "\n\n" + ex.InnerException.Message;
				Dispatcher.BeginInvoke(new Action(() => MessageBox.Show(message, "Data Retrieval Error.",
					MessageBoxButton.OK, MessageBoxImage.Exclamation)),
					System.Windows.Threading.DispatcherPriority.Normal);
			}
		}

		/// <summary>
		///     James McPherson
		///     Created 2018/02/19
		///     
		///     Refreshes the PaymentType data grid to populate it with data
		/// </summary>
		private void refreshPaymentTypeList()
		{
			dgPaymentTypes.ItemsSource = null;
			dgPaymentTypes.Items.Clear();
			try
			{
				_paymentTypeList = _paymentTypeManager.RetrievePaymentTypeListByActive();
				dgPaymentTypes.ItemsSource = _paymentTypeList;

			}
			catch (Exception ex)
			{
				var message = ex.Message + "\n\n" + ex.InnerException.Message;
				MessageBox.Show(message, "Data Retrieval Error.",
					MessageBoxButton.OK, MessageBoxImage.Exclamation);
			}
		}

		/// <summary>
		///     James McPherson
		///     Created 2018/02/04
		///     
		/// Refreshes the MakeModel data grid to populate it with data
		/// </summary>
		public void refreshMakeModelList()
		{
			dgMakeModel.ItemsSource = null;
			dgMakeModel.Items.Clear();
			try
			{
				_makeModelList = _makeModelManager.RetrieveMakeModelDetailList();
				dgMakeModel.ItemsSource = _makeModelList;
			}
			catch (Exception ex)
			{
				var message = ex.Message + "\n\n" + ex.InnerException.Message;
				MessageBox.Show(message, "Data Retrieval Error.",
					MessageBoxButton.OK, MessageBoxImage.Exclamation);
			}
		}

		/// <summary>
		///		Weston Olund
		///		Created 2018/01/26
		///		
		///		Refreshes the certifications data grid to populate it with data
		/// </summary>
		/// <param name="active"></param>
		private void refreshCertificationsList()
		{
			dgCertification.ItemsSource = null;
			dgCertification.Items.Clear();
			try
			{
				_certificationList = _certificationManager.RetrieveCertificationList();
				dgCertification.ItemsSource = _certificationList;
			}
			catch (Exception ex)
			{
				var message = ex.Message + "\n\n" + ex.InnerException.Message;
				Dispatcher.BeginInvoke(new Action(() => MessageBox.Show(message, "Data Retrieval Error.",
					MessageBoxButton.OK, MessageBoxImage.Exclamation)),
					System.Windows.Threading.DispatcherPriority.Normal);
			}
		}


		/// <summary>
		/// John Miller
		/// Created 2018/02/02
		/// 
		/// CustomerTypes selected event
		/// </summary>
		/// <param name="active"></param>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void TabItem_CustomerTypes_Selected(object sender, RoutedEventArgs e)
		{
			refreshCustomerTypeList();
		}

		/// <summary>
		/// John Miller
		/// Created 2018/02/15
		/// 
		/// Button click event to call a form to add a Vendor
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnAddVendor_Click(object sender, RoutedEventArgs e)
		{
			var vendorForm = new frmAddEditVendor(_vendorManager);
			var result = vendorForm.ShowDialog();
			if (result == true)
			{
				refreshVendorList();
			}
		}

		/// <summary>
		/// John Miller
		/// Created 2018/02/02
		/// 
		/// Vendor selected event
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void TabVendor_Selected(object sender, RoutedEventArgs e)
		{
			refreshVendorList();
		}

		/// <summary>
		/// John Miller
		/// Created 2018/02/15
		/// 
		/// Button Click event to call a form to edit a Vendor
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnEditVendor_Click(object sender, RoutedEventArgs e)
		{
			if (this.dgVendor.SelectedItems.Count > 0)
			{
				var editForm = new frmAddEditVendor(_vendorManager, (Vendor)this.dgVendor.SelectedItem);
				var result = editForm.ShowDialog();
				if (result == true)
				{
					refreshVendorList();
				}

			}

			else
			{
				MessageBox.Show("You must select a Vendor.");
			}
		}

		/// <summary>
		/// Utility for deleting an inspection checklist record from the database
		/// </summary>
		/// <remarks>
		/// Zach Murphy
		/// Updated 2018/2/16
		/// </remarks>
        /// <remarks>
        /// Noah Davison
        /// Updated 2018/05/10
        /// 
        /// Fixed crash when no inspection checklist selected
        /// </remarks>
		private void btnDeleteInspectionChecklist_Click(object sender, RoutedEventArgs e)
		{
            InspectionChecklist selectedInspectionChecklist = (InspectionChecklist)dgInspectionChecklist.SelectedItem;
            if(selectedInspectionChecklist != null)
            {
                try
                {
                    _inspectionChecklistManager.DeactivateInspectionChecklist(((InspectionChecklist)dgInspectionChecklist.SelectedItem).InspectionChecklistID);
                    refreshInspectionChecklist();
                    MessageBox.Show("Inspection checklist successfully deactivated");
                }
                catch (Exception ex)
                {
                    var message = ex.Message + "\n\n" + ex.InnerException.Message;
                    MessageBox.Show(message, "Data Retrieval Error.", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                }
            }
            else
            {
                MessageBox.Show("You must select something!");
            }
			
		}

		/// <summary>
		///     Weston Olund
		///     Created 2018/01/26
		///     
		///     Calls the refresh certifications whenever the certification tab is selected
		/// </summary>
		private void TabItem_Certification_Selected(object sender, RoutedEventArgs e)
		{
			refreshCertificationsList();
		}

		private void TabItem_Employee_Selected(object sender, RoutedEventArgs e)
		{
			refreshEmployeeList();
		}


		/// <summary>
		///     James McPherson
		///     Created 2018/02/02
		///     
		///     Refreshes the Employees data grid to populate it with data
		/// </summary>
		private void refreshEmployeeList()
		{
			dgEmployees.ItemsSource = null;
			dgEmployees.Items.Clear();
			try
			{
				_employeeList = _employeeManager.RetrieveEmployeeList();
				dgEmployees.ItemsSource = _employeeList;
			}
			catch (Exception ex)
			{
				var message = ex.Message + "\n\n" + ex.InnerException.Message;
				Dispatcher.BeginInvoke(new Action(() => MessageBox.Show(message, "Data Retrieval Error.",
					MessageBoxButton.OK, MessageBoxImage.Exclamation)),
					System.Windows.Threading.DispatcherPriority.Normal);
			}
		}


		/// <summary>
		///     Jacob Conley
		///     Created 2018/02/21
		/// 
		///     Refreshes the list of service offerings data grid to populate with data
		/// </summary>
		private void refreshServiceOfferingsList()
		{
			dgServiceOffering.ItemsSource = null;
			dgServiceOffering.Items.Clear();
			try
			{
				_serviceOfferingList = _serviceOfferingManager.RetrieveServiceOfferingList();

				dgServiceOffering.ItemsSource = _serviceOfferingList;
			}
			catch (Exception ex)
			{
				var message = ex.Message + "\n\n" + ex.InnerException.Message;
				MessageBox.Show(message, "Data Retrieval Error.", MessageBoxButton.OK, MessageBoxImage.Exclamation);
			}
		}


		/// <summary>
		/// Utility for updating the ServiceItem data grid with current data from the database.
		/// </summary>
		/// <remarks>
		/// Zach Murphy
		/// Updated 2018/2/22
		/// </remarks>
		private void refreshServiceItems()
		{
			try
			{
				this.dgServiceItem.ItemsSource = this._serviceItemManager.RetrieveServiceItemList();
			}
			catch (Exception ex)
			{
				var message = ex.Message + "\n\n" + ex.InnerException;
				MessageBox.Show(message, "Error Retrieving Service Items!", MessageBoxButton.OK, MessageBoxImage.Exclamation);
			}
		}

		private void tabServiceItems_Selected(object sender, RoutedEventArgs e)
		{
			refreshServiceItems();
		}

		/// <summary>
		/// John Miller
		/// Created 2018/02/15
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnDeactivateVendor_Click(object sender, RoutedEventArgs e)
		{
			try
			{
				if (this.dgVendor.SelectedItems.Count > 0)
				{
					if (((Vendor)this.dgVendor.SelectedItem).Active == false)
					{
						MessageBox.Show("This Vendor is currently inactive.");
					}
					else
					{
						MessageBoxResult result = MessageBox.Show("Are you sure you want to Deactivate Vendor " +
							((Vendor)this.dgVendor.SelectedItem).Name + "?", "Deactivation Warning", MessageBoxButton.YesNo);
						if (result == MessageBoxResult.No)
						{
							return;
						}
						_vendorManager.DeactivateVendorByID(((Vendor)this.dgVendor.SelectedItem).VendorID);
						refreshVendorList();
					}
				}
				else
				{
					MessageBox.Show("You must select a vendor.");
				}
			}
			catch (Exception ex)
			{
				var message = ex.Message + "\n\n" + ex.InnerException;
				MessageBox.Show(message, "Deactivation Error", MessageBoxButton.OK, MessageBoxImage.Exclamation);
			}

		}

		/// <summary>
		///     James McPherson
		///     Created 2018/02/04
		///     
		///     Calls the refreshMakeModelList method whenever the MakeModel tab is selected
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void TabItem_MakeModel_Selected(object sender, RoutedEventArgs e)
		{
			refreshMakeModelList();
		}

		/// <summary>
		///     James McPherson
		///     Created 2018/02/19
		///     
		///     Calls the refreshPaymentTypeList method whenever the PaymentType tab
		///     is selected
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void TabItem_PaymentType_Selected(object sender, RoutedEventArgs e)
		{
			refreshPaymentTypeList();
		}

		private void btnAddPaymentType_Click(object sender, RoutedEventArgs e)
		{
			var frmAddEditPaymentType = new frmAddEditPaymentType(_paymentTypeManager);
			var result = frmAddEditPaymentType.ShowDialog();
		}

		private void btnDeactivateCustomerType_Click(object sender, RoutedEventArgs e)
		{
			CustomerType selectedCustomerType = (CustomerType)dgCustomerTypes.SelectedItem;
			if (selectedCustomerType != null)
			{
				var messageBoxResult = MessageBox.Show("Are you sure you want to delete the customer type "
														+ selectedCustomerType.CustomerTypeID + "?",
														"Confirm Deactivate", MessageBoxButton.YesNo);
				if (messageBoxResult == MessageBoxResult.Yes)
				{
					try
					{
						if (_customerTypeManager.DeleteCustomerType(selectedCustomerType.CustomerTypeID))
						{
							refreshCustomerTypeList();
							MessageBox.Show("Customer type sucessfully deleted.");
						}
						else
						{
							MessageBox.Show("Something went wrong trying to delete customer type.");
						}
					}
					catch (SqlException)
					{
						MessageBox.Show("Failed to delete customer type. Make sure there are no customers with type \""
										+ selectedCustomerType.CustomerTypeID + "\" in the customers table");
					}
					catch (Exception)
					{
						MessageBox.Show("Failed to delete customer type.");
					}
				}
			}
			else
			{
				MessageBox.Show("You must select a row to deactivate");
			}
		}

		/// <summary>
		/// Zachary Hall
		/// Created: 2018/02/01
		/// 
		/// Supply Item Tab selected event
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void tabSupply_Selected(object sender, RoutedEventArgs e)
		{
			refreshSupplyItemList();
		}

		/// <summary>
		/// Zachary Hall
		/// Created: 2018/02/01
		/// 
		/// Reinstantiates the supply item list with up-to-date data from the database
		/// </summary>
		private void refreshSupplyItemList()
		{
			try
			{
				_supplyItemList = _supplyItemManager.RetrieveSupplyItemList();
				dgSupplyItem.ItemsSource = _supplyItemList;
			}
			catch (Exception ex)
			{
				var message = ex.Message + "\n\n" + ex.InnerException;
				MessageBox.Show(message, "Error Retrieving Supply Items!", MessageBoxButton.OK, MessageBoxImage.Exclamation);
			}
		}

		/// <summary>
		/// Zachary Hall
		/// Created: 2018/02/01
		/// 
		/// Button click event to call a form to adda SupplyItem record
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnAddSupply_Click(object sender, RoutedEventArgs e)
		{
			var supplyItemForm = new frmAddEditSupply(_supplyItemManager);
			var result = supplyItemForm.ShowDialog();
			if (result == true)
			{
				refreshSupplyItemList();
			}
		}

		/// <summary>
		/// Zachary Hall
		/// Created: 2018/02/01
		/// 
		/// Button Click event to call a form to edit a supply item record.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnEditSupply_Click(object sender, RoutedEventArgs e)
		{
			if (this.dgSupplyItem.SelectedItems.Count > 0)
			{
				var editForm = new frmAddEditSupply(_supplyItemManager, (SupplyItem)this.dgSupplyItem.SelectedItem);
				var result = editForm.ShowDialog();
				if (result == true)
				{
					refreshSupplyItemList();
				}
			}
			else
			{
				MessageBox.Show("You must select something!");
			}
		}

		/// <summary>
		/// Zachary Hall
		/// Created: 2018/02/01
		/// 
		/// Button click event for deactivating SupplyItem records
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnDeactivateSupply_Click(object sender, RoutedEventArgs e)
		{

			try
			{
				if (this.dgSupplyItem.SelectedItems.Count > 0)
				{

					if (((SupplyItem)this.dgSupplyItem.SelectedItem).Active == false)
					{
						MessageBox.Show("Supply Item already deactivated!");
					}
					else
					{
						MessageBoxResult result = MessageBox.Show("Are you sure you want to Deactivate Item "
							+ ((SupplyItem)this.dgSupplyItem.SelectedItem).SupplyItemID
							+ "?", "Deactivation Warning", MessageBoxButton.YesNo);
						if (result == MessageBoxResult.No)
						{
							return;
						}
						var deactivateResult = _supplyItemManager.DeactivateSupplyItemByID(((SupplyItem)this.dgSupplyItem.SelectedItem).SupplyItemID);
						if (!deactivateResult)
						{
							throw new ApplicationException("Supply Item was not deactivated properly!");
						}
						else
						{
							MessageBox.Show("Supply Item was deactivated!");
						}
						refreshSupplyItemList();
					}

				}
				else
				{
					MessageBox.Show("You must select something!");
				}
			}
			catch (Exception ex)
			{

				var message = ex.Message + "\n\n" + ex.InnerException;
				MessageBox.Show(message, "Deactivation Error!", MessageBoxButton.OK, MessageBoxImage.Exclamation);
			}

		}

		/// <summary>
		/// Zachary Hall
		/// Created 2018/02/22
		/// 
		/// Click event to trigger add service package functionality
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnAddServicePackage_Click(object sender, RoutedEventArgs e)
		{
			var servicePackageForm = new frmAddEditServicePackage(_servicePackageManager);
			var result = servicePackageForm.ShowDialog();
			if (result == true)
			{
				refreshServicePackage();
			}
		}

		/// <summary>
		/// Zachary Hall
		/// Created 2018/02/22
		/// 
		/// Click event to trigger edit service package functionality
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnEditServicePackage_Click(object sender, RoutedEventArgs e)
		{
			if (this.dgServicePackage.SelectedItems.Count > 0)
			{
				var editForm = new frmAddEditServicePackage(_servicePackageManager, (ServicePackage)this.dgServicePackage.SelectedItem);
				var result = editForm.ShowDialog();
				if (result == true)
				{
					refreshServicePackage();
				}
			}
			else
			{
				MessageBox.Show("You must select something!");
			}
		}

		/// <summary>
		/// Zachary Hall
		/// Created 2018/02/22
		/// 
		/// Placeholder for Jayden's method
		/// </summary>
		/// <remarks>
		/// Jayden Tollefson
		/// Created 2018/02/22
		/// </remarks>
		private void refreshServicePackage()
		{
			dgServicePackage.ItemsSource = null;
			dgServicePackage.Items.Clear();
			try
			{
				_servicePackageList = _servicePackageManager.RetrieveServicePackageList();
				dgServicePackage.ItemsSource = _servicePackageList;
			}
			catch (Exception ex)
			{
				var message = ex.Message + "\n\n" + ex.InnerException.Message;
				MessageBox.Show(message, "Data Retrieval Error.", MessageBoxButton.OK, MessageBoxImage.Exclamation);
			}
		}

		/// <summary>
		///		Weston Olund
		///		Created 2018/02/21
		///		
		///		Refreshes the time off request data grid to populate it with data
		/// </summary>
		private void refreshTimeOffRequestList()
		{
			List<TimeOffRequestDetail> _timeOffRequestDetailList = new List<TimeOffRequestDetail>();
			dgTimeOffRequests.ItemsSource = null;
			dgTimeOffRequests.Items.Clear();
			try
			{
				_timeOffRequestList = _timeOffRequestManager.RetrieveTimeOffRequestList();
				foreach (var timeOffRequest in _timeOffRequestList)
				{
					Employee employee = _employeeManager.RetrieveEmployeeByID(timeOffRequest.EmployeeID);
					TimeOffRequestDetail timeOffRequestDetail = new TimeOffRequestDetail()
					{
						TimeOffRequest = timeOffRequest,
						Employee = employee
					};
					_timeOffRequestDetailList.Add(timeOffRequestDetail);
				}
				dgTimeOffRequests.ItemsSource = _timeOffRequestDetailList;
			}
			catch (Exception ex)
			{
				var message = ex.Message + "\n\n" + ex.InnerException.Message;
				Dispatcher.BeginInvoke(new Action(() => MessageBox.Show(message, "Data Retrieval Error.",
					MessageBoxButton.OK, MessageBoxImage.Exclamation)),
					System.Windows.Threading.DispatcherPriority.Normal);
			}
		}

		/// <summary>
		/// Weston Olund
		/// Created on 2018/02/22
		/// 
		/// Calls the refresh timeOffRequestList method when the time off request tab is 
		/// selected.
		/// </summary>
		private void TabItem_TimeOffRequests_Selected(object sender, RoutedEventArgs e)
		{
			refreshTimeOffRequestList();
		}

		/// <summary>
		/// Weston Olund
		/// Created on 2018/02/22
		/// 
		/// Method to create add form when add is clicked
		/// </summary>
		private void btnAddTimeOffRequest_Click(object sender, RoutedEventArgs e)
		{
			var frmAddTimeOffRequest = new frmAddEditTimeOffRequest(_timeOffRequestManager);
			frmAddTimeOffRequest.ShowDialog();
			try
			{
				var result = frmAddTimeOffRequest.ShowDialog();
			}
			catch (Exception)
			{
			}
			refreshTimeOffRequestList();
		}

		/// <summary>
		/// Weston Olund
		/// Created on 2018/02/28
		/// 
		/// Method to add a certificate to database
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnAddCertification_Click(object sender, RoutedEventArgs e)
		{
			var frmAddCertification = new frmAddEditCertification(_certificationManager);
			frmAddCertification.ShowDialog();
			try
			{
				var result = frmAddCertification.ShowDialog();
			}
			catch (Exception)
			{
			}
			refreshCertificationsList();
		}

		/// <summary>
		/// Weston Olund
		/// Created on 2018/03/07
		/// 
		/// Method to edit certification
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnEditCertification_Click(object sender, RoutedEventArgs e)
		{
			if (this.dgCertification.SelectedItems.Count == 0)
			{
				MessageBox.Show("You must select a certification.");
				return;
			}

			Certification selectedCert = null;
			selectedCert = (Certification)this.dgCertification.SelectedItem;
			var certID = selectedCert.CertificationID;
			try
			{
				var cert = _certificationManager.RetrieveCertificationByID(certID);
				var editForm = new frmAddEditCertification(_certificationManager, cert, DetailFormMode.Edit);
				var result = editForm.ShowDialog();
				if (result == true)
				{
					refreshCertificationsList();
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show("There was an error editing the certificate." + ex);
			}
		}

		/// <summary>
		/// Weston Olund
		/// Created on 2018/03/08
		/// 
		/// Method to refresh resupply order data grid
		/// </summary>
		private void refreshResupplyOrderList()
		{
			List<ResupplyOrderDetail> _resupplyOrderDetailList = new List<ResupplyOrderDetail>();
			dgResupplyOrder.ItemsSource = null;
			dgResupplyOrder.Items.Clear();
			try
			{
				_resupplyOrderList = _resupplyOrderManager.RetrieveResupplyOrderList();
				foreach (var resupplyOrder in _resupplyOrderList)
				{
					Employee employee = _employeeManager.RetrieveEmployeeByID(resupplyOrder.EmployeeID);
					ResupplyOrderDetail resupplyOrderDetail = new ResupplyOrderDetail()
					{
						ResupplyOrder = resupplyOrder,
						Employee = employee
					};
					_resupplyOrderDetailList.Add(resupplyOrderDetail);
				}
				dgResupplyOrder.ItemsSource = _resupplyOrderDetailList;
			}
			catch (Exception ex)
			{
				var message = ex.Message + "\n\n" + ex.InnerException.Message;
				Dispatcher.BeginInvoke(new Action(() => MessageBox.Show(message, "Data Retrieval Error.",
					MessageBoxButton.OK, MessageBoxImage.Exclamation)),
					System.Windows.Threading.DispatcherPriority.Normal);
			}
		}

		/// <summary>
		/// Weston Olund
		/// Created on 2018/03/08
		/// 
		/// Method to refresh list when tab is selected
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void TabItem_ResupplyOrder_Selected(object sender, RoutedEventArgs e)
		{
			refreshResupplyOrderList();
		}

		/// <summary>
		/// Weston Olund
		/// Created on 2018/03/08
		/// 
		/// Method when add button is clicked
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnAddResupplyOrder_Click(object sender, RoutedEventArgs e)
		{
			var frmAddResupplyOrder = new frmAddEditResupplyOrder(_resupplyOrderManager, _user);
			frmAddResupplyOrder.ShowDialog();
			try
			{
				var result = frmAddResupplyOrder.ShowDialog();
			}
			catch (Exception)
			{
			}
			refreshResupplyOrderList();
		}

		/// <summary>
		/// Weston Olund
		/// Created on 2018/03/08
		/// 
		/// Method to handle edit button selected
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnEditResupplyOrder_Click(object sender, RoutedEventArgs e)
		{
			if (this.dgResupplyOrder.SelectedItems.Count == 0)
			{
				MessageBox.Show("You must select a resupply order.");
				return;
			}

			ResupplyOrderDetail selectedResupplyOrder = null;
			selectedResupplyOrder = (ResupplyOrderDetail)this.dgResupplyOrder.SelectedItem;
			var resupplyID = selectedResupplyOrder.ResupplyOrder.ResupplyOrderID;
			List<ResupplyOrderLineDetail> resupplyOrderLineDetailList = new List<ResupplyOrderLineDetail>();
			try
			{
				var resupplyOrder = _resupplyOrderManager.RetrieveResupplyOrderByID(resupplyID);
				try
				{
					resupplyOrderLineDetailList = _resupplyOrderLineManager.RetrieveResupplyOrderLineDetailListByResupplyOrderID(resupplyID);
				}
				catch (Exception)
				{
					MessageBox.Show("This Resupply Order currently has no order lines.");
				}

				var editForm = new frmAddEditResupplyOrder(_resupplyOrderManager, resupplyOrder, _user, resupplyOrderLineDetailList);
				var result = editForm.ShowDialog();
				if (result == true)
				{
					refreshResupplyOrderList();
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show("There was an error editing the resupply order." + ex);
			}
		}

		/// <summary>
		/// Brady Feller
		/// Created 2018/02/22
		/// 
		/// Adds an equipment type
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnAddEquipmentType_Click(object sender, RoutedEventArgs e)
		{
			var frmAddEditEquipmentType = new frmAddEditEquipmentType(_equipmentTypeManager, _inspectionChecklistManager, _prepChecklistManager);
			var result = frmAddEditEquipmentType.ShowDialog();
			if (result == true)
			{
				dgEquipmentType.ItemsSource = _equipmentTypeList;
				refreshEquipmentTypeList();
			}
		}

		/// <summary>
		/// Brady Feller
		/// Created 2018/02/22
		/// 
		/// Edits an equipment type
		/// </summary>
        /// <remarks>
        /// Noah Davison
        /// Modified 2018/05/09
        /// 
        /// Modified so it doesn't crash from the changes I made to refreshEquipmentTypeList()
        /// </remarks>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnEditEquipmentType_Click(object sender, RoutedEventArgs e)
		{
			EquipmentType item = null;
			EquipmentTypeDetail etDetail = null;
			if (this.dgEquipmentType.SelectedItems.Count > 0)
			{
				etDetail = (EquipmentTypeDetail)this.dgEquipmentType.SelectedItem;
				try
				{
					
					var detailForm = new frmAddEditEquipmentType(_equipmentTypeManager, _inspectionChecklistManager, _prepChecklistManager, etDetail, DetailFormMode.Edit);
					var result = detailForm.ShowDialog();
					if (result == true)
					{
						refreshEquipmentTypeList();
						dgEquipmentType.ItemsSource = _equipmentTypeList;
					}
				}
				catch (Exception ex)
				{
					MessageBox.Show(ex.Message + "\n\n" + ex.InnerException.Message);
				}
			}
			else
			{
				MessageBox.Show("You need to select something!");
			}
		}

        /// <summary>
        /// Brady Feller
        /// Created 2018/02/19
        /// 
        /// Deactivates a source record when selected
        /// 
        /// Marshall Sejkora
        /// Updated 2018/04/03
        /// 
        /// Added refreshSourceList
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDeactivateSource_Click(object sender, RoutedEventArgs e)
		{
			Source selectedSource = (Source)dgSource.SelectedItem;
			if (selectedSource != null)
			{
				try
				{
					_sourceManager.DeactivateSource(selectedSource.SourceID);
					MessageBox.Show("Source deactivated.");
                    refreshSourceList();
				}
				catch
				{
					MessageBox.Show("Failed to deactivate source.");
				}
			}
			else
			{
				MessageBox.Show("You must select a source to deactivate.");
			}
		}

		/// <summary>
		/// Brady Feller
		/// Created 2018/02/22
		/// 
		/// Deactivates a service package record when selected
		/// 
		/// Jacob Slaubaugh
		/// Updated 2018/04/12
		/// 
		/// Added refreshServicePackage()
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnDeactivateServicePackage_Click(object sender, RoutedEventArgs e)
		{
			ServicePackage selectedSource = (ServicePackage)dgServicePackage.SelectedItem;
			if (selectedSource != null)
			{
				try
				{
					_servicePackageManager.DeactivateServicePackage(selectedSource.ServicePackageID);
					MessageBox.Show("Source deactivated.");
					refreshServicePackage();
				}
				catch
				{
					MessageBox.Show("Failed to deactivate source.");
				}
			}
			else
			{
				MessageBox.Show("You must select a source to deactivate.");
			}
		}

		/// <summary>
		///     Jacob Conley
		///     Created 2018/02/21
		/// 
		///     Calls the refreshServiceOfferingsList method when the Service Offering tab is selected
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void TabItem_Service_Offerings_Selected(object sender, RoutedEventArgs e)
		{
			refreshServiceOfferingsList();
		}

		/// <summary>
		/// Jacob Conley
		/// Created 2018/03/01
		/// 
		/// Allows a user to edit a currently existing time off request
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnEditTimeOffRequest_Click(object sender, RoutedEventArgs e)
		{
			if (this.dgTimeOffRequests.SelectedItems.Count > 0)
			{
				var editForm = new frmAddEditTimeOffRequest(_timeOffRequestManager, (TimeOffRequestDetail)this.dgTimeOffRequests.SelectedItem);
				var result = editForm.ShowDialog();
				if (result == true)
				{
					refreshTimeOffRequestList();
				}

			}
			else
			{
				MessageBox.Show("You must select a request.");
			}

		}



		private void btnAddTask_Click(object sender, RoutedEventArgs e)
		{
			var taskForm = new frmAddEditTask(_taskManager);
			var result = taskForm.ShowDialog();
			if (result == true)
			{
				refreshTaskList();
			}
		}

		private void refreshTaskTypeList()
		{
			try
			{
				_taskTypeList = _taskTypeManager.RetrieveTaskTypeList();
				dgTaskType.ItemsSource = _taskTypeList;
			}
			catch (Exception ex)
			{
				var message = ex.Message + "\n\n" + ex.InnerException;
				MessageBox.Show(message, "Error Retrieving TaskTypes", MessageBoxButton.OK, MessageBoxImage.Exclamation);
			}
		}

		private void tabTasks_Selected(object sender, RoutedEventArgs e)
		{
			refreshTaskList();
		}

		private void refreshTaskList()
		{
			try
			{
				_taskList = _taskManager.RetrieveTaskList();
				dgTask.ItemsSource = _taskList;
			}
			catch (Exception ex)
			{
				var message = ex.Message + "\n\n" + ex.InnerException;
				MessageBox.Show(message, "Error Retrieving Tasks", MessageBoxButton.OK, MessageBoxImage.Exclamation);
			}
		}

		private void btnEditTask_Click(object sender, RoutedEventArgs e)
		{
			if (this.dgTask.SelectedItems.Count > 0)
			{
				var editForm = new frmAddEditTask(_taskManager, (DataObjects.Task)this.dgTask.SelectedItem);
				var result = editForm.ShowDialog();
				if (result == true)
				{
					refreshTaskList();
				}
			}
			else
			{
				MessageBox.Show("You must select a Task.");
			}
		}

		private void btnDeactivateTask_Click(object sender, RoutedEventArgs e)
		{
			try
			{
				if (this.dgTask.SelectedItems.Count > 0)
				{
					if (((DataObjects.Task)this.dgTask.SelectedItem).Active == false)
					{
						MessageBox.Show("This Task is currently inactive.");
					}
					else
					{
						MessageBoxResult result = MessageBox.Show("Are you sure you want to Deactivate Task " +
							((DataObjects.Task)this.dgTask.SelectedItem).Name + "?", "Deactivation Warning", MessageBoxButton.YesNo);
						if (result == MessageBoxResult.No)
						{
							return;
						}
						_taskManager.DeactivateTaskByID(((DataObjects.Task)this.dgTask.SelectedItem).TaskID);
						refreshTaskList();
					}
				}
				else
				{
					MessageBox.Show("You must select a task.");
				}
			}
			catch (Exception ex)
			{
				var message = ex.Message + "\n\n" + ex.InnerException;
				MessageBox.Show(message, "Deactivation Error", MessageBoxButton.OK, MessageBoxImage.Exclamation);
			}
		}

		/// <summary>
		/// John Miller 
		/// 2018/03/16
		/// 
		/// Deactivate Time Off Request click functionality
		/// 
		/// ** May want to change populate datagrid from only active requests to 
		///    the complete list? **
		///    
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnDeactivateTimeOffRequest_Click(object sender, RoutedEventArgs e)
		{
			try
			{
				if (this.dgTimeOffRequests.SelectedItems.Count > 0)
				{
					if (((TimeOffRequestDetail)this.dgTimeOffRequests.SelectedItem).TimeOffRequest.Active == false)
					{
						MessageBox.Show("This Time Off Request is currently inactive.");
					}
					else
					{
						MessageBoxResult result = MessageBox.Show("Are you sure you want to Deactivate Time Off Request for " +
							((TimeOffRequestDetail)this.dgTimeOffRequests.SelectedItem).Employee.EmployeeID + "?", "Deactivation Warning", MessageBoxButton.YesNo);
						if (result == MessageBoxResult.No)
						{
							return;
						}
						_timeOffRequestManager.DeactivateTimeOffRequestByID(((TimeOffRequestDetail)this.dgTimeOffRequests.SelectedItem).TimeOffRequest.TimeOffID);
						refreshTimeOffRequestList();
					}
				}
				else
				{
					MessageBox.Show("You must select a time off request.");
				}
			}
			catch (Exception ex)
			{
				var message = ex.Message + "\n\n" + ex.InnerException;
				MessageBox.Show(message, "Deactivation Error", MessageBoxButton.OK, MessageBoxImage.Exclamation);
			}
		}


		/// <summary>
		/// Noah Davison
		/// 2018/03/08
		/// 
		/// Delete Service Offering click functionality
		///    
		/// Jacob Conley
		/// Updated: 2018/04/07
		/// 
		/// Added delete to remove the ServiceOfferingItem connection before deleting.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnDeleteServiceOffering_Click(object sender, RoutedEventArgs e)
		{
			ServiceOffering selectedServiceOffering = (ServiceOffering)dgServiceOffering.SelectedItem;
			if (selectedServiceOffering != null)
			{
				var messageBoxResult = MessageBox.Show("Are you sure you want to delete the service offering "
														+ selectedServiceOffering.Name + "?",
														"Confirm Delete", MessageBoxButton.YesNo);
				if (messageBoxResult == MessageBoxResult.Yes)
				{
					try
					{
						var serviceOfferingItems = _serviceOfferingItemManager.RetrieveServiceOfferingItemByID(selectedServiceOffering.ServiceOfferingID);
						foreach (var serviceOfferingItem in serviceOfferingItems)
						{
							_serviceOfferingItemManager.DeleteServiceOfferingItem(serviceOfferingItem);
						}
						if (_serviceOfferingManager.DeleteServiceOfferingByID(selectedServiceOffering.ServiceOfferingID))
						{
							refreshServiceOfferingsList();
							MessageBox.Show("Service Offering sucessfully deleted.");
						}
						else
						{
							MessageBox.Show("Something went wrong trying to delete service offering.");
						}
					}
					catch (SqlException)
					{
						MessageBox.Show("Failed to delete service offering. Make sure there are no service items with the service offering \""
										+ selectedServiceOffering.Name + "\" in the service items table");
					}
					catch (Exception)
					{
						MessageBox.Show("Failed to delete customer type.");
					}
				}
			}
			else
			{
				MessageBox.Show("You must select a row to delete");
			}
		}

		/// <summary>
		/// Mike Mason
		/// Created: 2018/01/31
		/// 
		/// Populates the employee certification datagrid
		/// </summary>
		private void refreshEmployeeCertificationList()
		{
			dgEmployeeCertification.ItemsSource = null;
			dgEmployeeCertification.Items.Clear();
			try
			{
				_employeeCertificationDetailList = _employeeCertificationManager.RetrieveEmployeeCertificationDetailList();
				dgEmployeeCertification.ItemsSource = _employeeCertificationDetailList; ;
			}
			catch (Exception ex)
			{
				var message = ex.Message + "\n\n" + ex.InnerException.Message;
				Dispatcher.BeginInvoke(new Action(() => MessageBox.Show(message, "Data Retrieval Error.", MessageBoxButton.OK, MessageBoxImage.Exclamation)), System.Windows.Threading.DispatcherPriority.Normal);
			}
		}


		/// <summary>
		/// Mike Mason
		/// Created 2018/02/21
		/// 
		/// Button Event for launching the Create an Employee form
		/// </summary>
		private void btnAddEmployee_Click(object sender, RoutedEventArgs e)
		{
			var employeeForm = new frmAddEditEmployee(_employeeManager);
			var result = employeeForm.ShowDialog();
			if (result == true)
			{
				refreshEmployeeList();
			}
		}


		/// <summary>
		///     Mike Mason
		///     Created 2018/02/15
		///     
		///     Calls the refreshEmployeeCertificationList method whenever the 
		///     EmployeeCertification tab is selected
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void TabItem_EmployeeCertifications_Selected(object sender, RoutedEventArgs e)
		{
			refreshEmployeeCertificationList();
		}


		/// <summary>
		/// Mike Mason
		/// Created 2018/02/21
		/// 
		/// Button Event for launching the Create a Customer form
		/// </summary>
		private void btnAddCustomer_Click(object sender, RoutedEventArgs e)
		{
			var customerForm = new frmAddEditCustomer(_customerManager);
			var result = customerForm.ShowDialog();
			if (result == true)
			{
				refreshCustomerList();
			}
		}




		/// <summary>
		/// Jayden Tollefson
		/// Created 2018/02/22
		/// 
		/// Method to call the refresh service package function everytime the tab is selected
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void tabServicePackages_Selected(object sender, RoutedEventArgs e)
		{
			refreshServicePackage();
		}

		private void btnEditCustomer_Click(object sender, RoutedEventArgs e)
		{
			if (this.dgCustomers.SelectedItems.Count > 0)
			{
				//try
				//{
				var editForm = new frmAddEditCustomer(_customerManager, (Customer)this.dgCustomers.SelectedItem);
				var result = editForm.ShowDialog();
				if (result == true)
				{
					refreshCustomerList();
				}
				//    }
				//    catch (Exception ex)
				//    {
				//        MessageBox.Show(ex.Message + "\n\n" + ex.InnerException.Message);
				//    }
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
		/// Event for when Job tab is selected
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void tabJobs_Selected(object sender, RoutedEventArgs e)
		{
			refreshJobList();
		}

		/// <summary>
		/// Zachary Hall
		/// Created 2018/03/10
		/// 
		/// Refreshes the list of jobs
		/// </summary>
		private void refreshJobList()
		{
			dgJobs.ItemsSource = null;
			dgJobs.Items.Clear();
			try
			{
				_jobDetailList = _jobManager.RetrieveJobDetailList();
				dgJobs.ItemsSource = _jobDetailList;
			}
			catch (Exception ex)
			{
				var message = ex.Message + "\n\n" + ex.InnerException.Message;
				Dispatcher.BeginInvoke(new Action(() => MessageBox.Show(message, "Data Retrieval Error.",
					MessageBoxButton.OK, MessageBoxImage.Exclamation)),
					System.Windows.Threading.DispatcherPriority.Normal);
			}
		}

		/// <summary>
		/// Zachary Hall
		/// Created 2018/03/10
		/// 
		/// Add event for jobs. User clicks add and is sent to add job form
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnAddJob_Click(object sender, RoutedEventArgs e)
		{
			var addform = new frmAddEditJob(_jobManager, _user.Employee);
			// Missing Throws Arg out of Range Exception? 
			var result = addform.ShowDialog();
			if (result == true)
			{
				refreshJobList();
			}
		}

		/// <summary>
		/// Zachary Hall
		/// Created 2018/03/10
		/// 
		/// opens edit job form if user selects a job from datagrid and clicks the edit button
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnEditJob_Click(object sender, RoutedEventArgs e)
		{
			if (this.dgJobs.SelectedItems.Count > 0)
			{
				var editForm = new frmAddEditJob(_jobManager, (JobDetail)this.dgJobs.SelectedItem, _user.Employee);
				var result = editForm.ShowDialog();
				if (result == true)
				{
					refreshJobList();
				}
			}
			else
			{
				MessageBox.Show("You must select something!");
			}
		}

		/// <summary>
		/// Zachary Hall
		/// Created 2018/03/23
		/// 
		/// Deactivate click event. Deactivates the selected Job object
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnDeactivateJob_Click(object sender, RoutedEventArgs e)
		{
			try
			{
				if (this.dgJobs.SelectedItems.Count > 0)
				{

					if (((JobDetail)this.dgJobs.SelectedItem).Job.Active == false)
					{
						MessageBox.Show("Job already deactivated!");
					}
					else
					{
						MessageBoxResult result = MessageBox.Show("Are you sure you want to Deactivate Job "
							+ ((JobDetail)this.dgJobs.SelectedItem).Job.JobID
							+ "?", "Deactivation Warning", MessageBoxButton.YesNo);
						if (result == MessageBoxResult.No)
						{
							return;
						}
						var deactivateResult = _jobManager.DeactivateJob(((JobDetail)this.dgJobs.SelectedItem).Job.JobID);
						if (!deactivateResult)
						{
							throw new ApplicationException("Job was not deactivated properly!");
						}
						else
						{
							MessageBox.Show("Job was deactivated!");
						}
						refreshJobList();
					}

				}
				else
				{
					MessageBox.Show("You must select something!");
				}
			}
			catch (Exception ex)
			{

				var message = ex.Message + "\n\n" + ex.InnerException;
				MessageBox.Show(message, "Deactivation Error!", MessageBoxButton.OK, MessageBoxImage.Exclamation);
			}
		}

		/// <summary>
		/// Zachary Hall
		/// Created 2018/03/23
		/// 
		/// Refreshes the JobLocation datagrid
		/// </summary>
		private void refreshJobLocationList()
		{
			dgJobLocations.ItemsSource = null;
			dgJobLocations.Items.Clear();
			try
			{
				_jobLocationDetailList = _jobLocationManager.RetrieveJobLocationDetailList();
				dgJobLocations.ItemsSource = _jobLocationDetailList;
			}
			catch (Exception ex)
			{
				var message = ex.Message + "\n\n" + ex.InnerException.Message;
				Dispatcher.BeginInvoke(new Action(() => MessageBox.Show(message, "Data Retrieval Error.",
					MessageBoxButton.OK, MessageBoxImage.Exclamation)),
					System.Windows.Threading.DispatcherPriority.Normal);
			}
		}

		/// <summary>
		/// Zachary Hall
		/// Created 2018/03/23
		/// 
		/// Job Location tab select
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void tabJobLocations_Selected(object sender, RoutedEventArgs e)
		{
			refreshJobLocationList();
		}

		/// <summary>
		/// Zachary Hall
		/// Created 2018/03/23
		/// 
		/// Opens add form for job location
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnAddJobLocation_Click(object sender, RoutedEventArgs e)
		{
			var addform = new frmAddEditJobLocation(_jobLocationManager);
			var result = addform.ShowDialog();
			if (result == true)
			{
				refreshJobLocationList();
			}
		}

		/// <summary>
		/// Zachary Hall
		/// Created 2018/03/23
		/// 
		/// Edit button even for job location
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnEditJobLocation_Click(object sender, RoutedEventArgs e)
		{
			if (this.dgJobLocations.SelectedItems.Count > 0)
			{
				var editForm = new frmAddEditJobLocation(_jobLocationManager, (JobLocationDetail)this.dgJobLocations.SelectedItem);
				var result = editForm.ShowDialog();
				if (result == true)
				{
					refreshJobLocationList();
				}
			}
			else
			{
				MessageBox.Show("You must select something!");
			}
		}

		/// <summary>
		/// Zachary Hall
		/// Created 2018/03/23
		/// 
		/// Button click event for deactivating a selected job location
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnDeactivateJobLocation_Click(object sender, RoutedEventArgs e)
		{
			try
			{
				if (this.dgJobLocations.SelectedItems.Count > 0)
				{

					if (((JobLocationDetail)this.dgJobLocations.SelectedItem).JobLocation.Active == false)
					{
						MessageBox.Show("Job Location already deactivated!");
					}
					else
					{
						MessageBoxResult result = MessageBox.Show("Are you sure you want to Deactivate Job Location "
							+ ((JobLocationDetail)this.dgJobLocations.SelectedItem).JobLocation.JobLocationID
							+ "?", "Deactivation Warning", MessageBoxButton.YesNo);
						if (result == MessageBoxResult.No)
						{
							return;
						}
						var deactivateResult = _jobLocationManager.DeactivateJobLocationByID(((JobLocationDetail)this.dgJobLocations.SelectedItem).JobLocation.JobLocationID);
						if (!deactivateResult)
						{
							throw new ApplicationException("Job was not deactivated properly!");
						}
						else
						{
							MessageBox.Show("Job was deactivated!");
						}
						refreshJobLocationList();
					}

				}
				else
				{
					MessageBox.Show("You must select something!");
				}
			}
			catch (Exception ex)
			{

				var message = ex.Message + "\n\n" + ex.InnerException;
				MessageBox.Show(message, "Deactivation Error!", MessageBoxButton.OK, MessageBoxImage.Exclamation);
			}
		}

		private void TabItem_EmployeeRole_Selected(object sender, RoutedEventArgs e)
		{
			RefreshEmployeeRoleList();
		}

		private void RefreshEmployeeRoleList()
		{
			dgEmployeeRole.ItemsSource = null;
			dgEmployeeRole.Items.Clear();

			try
			{
				_employeeRoleDetailList = _employeeRoleManager.RetrieveEmployeeRoleDetailList();
				dgEmployeeRole.ItemsSource = _employeeRoleDetailList;
			}
			catch (Exception ex)
			{
				var message = ex.Message + "\n\n" + ex.InnerException.Message;
				MessageBox.Show(message, "Data Retrieval Error.", MessageBoxButton.OK, MessageBoxImage.Exclamation);
			}
		}


		//private void btnDeleteEmployeeRole_Click(object sender, RoutedEventArgs e)
		//{



		//    EmployeeRole selectedEmployeeRole = (EmployeeRole)dgEmployeeRole.SelectedItems;


		//    if (selectedEmployeeRole != null)
		//    {
		//        try
		//        {
		//            _employeeRoleManager.(selectedEmployeeRole.EmployeeId,
		//                selectedEmployeeRole.RoleID);

		//            RefreshEmployeeRoleList();
		//            MessageBox.Show("Employee reole deactivated.");

		//        }
		//        catch (Exception)
		//        {

		//            MessageBox.Show("Failed to deactivate employee role.");
		//        }

		//    }
		//    else
		//    {

		//        MessageBox.Show("select an deactivate employee role.");

		//    }

		//}

		/// <summary>
		/// Jacob Slaubaugh
		/// Created 2018/02/15
		/// 
		/// Calls refreshEquipmentStatusList everytime the tab is selected
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void TabItem_EquipmentStatus_Selected(object sender, RoutedEventArgs e)
		{
			refreshEquipmentStatusList();
		}

		/// <summary>
		/// Jacob Slaubaugh
		/// Created 2018/02/15
		/// 
		/// Refreshes the EquipmentStatus data grid to populate with data
		/// </summary>
		/// <param></param>
		private void refreshEquipmentStatusList()
		{
			dgEquipmentStatus.ItemsSource = null;
			dgEquipmentStatus.Items.Clear();
			try
			{
				_equipmentStatusList = _equipmentStatusManager.RetrieveEquipmentStatusList();
				dgEquipmentStatus.ItemsSource = _equipmentStatusList;
			}
			catch (Exception ex)
			{
				var message = ex.Message + "\n\n" + ex.InnerException.Message;
				MessageBox.Show(message, "Data Retrieval Error.", MessageBoxButton.OK, MessageBoxImage.Exclamation);
			}
		}

		/// <summary>
		/// Jacob Slaubaugh
		/// Created 2018/02/18
		/// 
		/// Deletes an equipment status
		/// </summary>
        /// <remarks>
        /// Noah Davison
        /// Modified 2018/05/04
        /// 
        /// Added error message in case of foreign key constraint conflict.
        /// </remarks>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnDeleteEquipmentStatus_Click(object sender, RoutedEventArgs e)
		{
			EquipmentStatus selectedStatus = (EquipmentStatus)dgEquipmentStatus.SelectedItem;
			if (selectedStatus != null)
			{
				try
				{
					var result = MessageBox.Show("Are you sure you want to delete " + ((EquipmentStatus)(dgEquipmentStatus.SelectedItem)).EquipmentStatusID + "?", "Delete EquipmentStatus", MessageBoxButton.OKCancel, MessageBoxImage.Warning);
					if (result == MessageBoxResult.OK)
					{
						_equipmentStatusManager.DeleteEquipmentStatus(selectedStatus.EquipmentStatusID);
						MessageBox.Show("The status has been deleted.");
						refreshEquipmentStatusList();
					}

				}
                catch(SqlException)
                {
                    MessageBox.Show("Failed to delete equipment status. " 
                    + "Make sure there is no equipment currently in the database "
                    + "with the equipment status you are trying to delete.");
                }
				catch
				{
					MessageBox.Show("Failed to delete the equipment status.");
				}
			}
			else
			{
				MessageBox.Show("You must select an equipment status to delete.");
			}
		}

		/// <summary>
		/// Jacob Slaubaugh
		/// Created 2018/02/18
		/// 
		/// Creates a new form to add an equipment status
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnAddEquipmentStatus_Click(object sender, RoutedEventArgs e)
		{
			var detailForm = new frmAddEditEquipmentStatus(_equipmentStatusManager);
			var result = detailForm.ShowDialog();
			if (result == true)
			{
				refreshEquipmentStatusList();
			}
		}

		/// <summary>
		/// Jacob Slaubaugh
		/// Created 2018/02/23
		/// 
		/// Opens the form to edit a status
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnEditEquipmentStatus_Click(object sender, RoutedEventArgs e)
		{
			EquipmentStatus equipmentStatus = null;
			EquipmentStatus eqSt = null;
			if (this.dgEquipmentStatus.SelectedItems.Count > 0)
			{
				equipmentStatus = (EquipmentStatus)this.dgEquipmentStatus.SelectedItem;
				try
				{
					eqSt = _equipmentStatusManager.RetrieveEquipmentStatusByID(equipmentStatus.EquipmentStatusID);
					var detailForm = new frmAddEditEquipmentStatus(_equipmentStatusManager, eqSt, DetailFormMode.Edit);
					var result = detailForm.ShowDialog();
					if (result == true)
					{
						refreshEquipmentStatusList();
						dgEquipmentStatus.ItemsSource = _equipmentStatusList;
					}
				}
				catch (Exception ex)
				{
					MessageBox.Show(ex.Message + "\n\n" + ex.InnerException.Message, "Error.");
					throw;
				}
			}
			else
			{
				MessageBox.Show("Please select a status.");
			}
		}

		/// <summary>
		/// Zachary Hall
		/// Created: 2018/01/31
		/// 
		/// Event to be fired when the Add button on the Special Order Item tab is clicked.
		/// Opens the frmAddEditSpecialOrderItem window and performs add functionailty with the User.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnAddSpecialOrder_Click(object sender, RoutedEventArgs e)
		{
			var specialOrderItemForm = new frmAddEditSpecialOrderItem(_specialOrderItemManager);
			var result = specialOrderItemForm.ShowDialog();
			if (result == true)
			{
				refreshSpecialOrderItemList();
			}
		}

		/// <summary>
		/// Zachary Hall
		/// Created: 2018/01/31
		/// 
		/// Open window prompt to edit special orders
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnEditSpecialOrder_Click(object sender, RoutedEventArgs e)
		{
			if (this.dgSpecialOrderItem.SelectedItems.Count > 0)
			{
				var editForm = new frmAddEditSpecialOrderItem(_specialOrderItemManager, (SpecialItem)this.dgSpecialOrderItem.SelectedItem);
				var result = editForm.ShowDialog();
				if (result == true)
				{
					refreshSpecialOrderItemList();
				}
			}
			else
			{
				MessageBox.Show("You must select something!");
			}
		}

		/// <summary>
		/// Zachary Hall
		/// Created: 2018/02/08
		/// 
		/// Deactivate Button click event, deactivates the selected SpecialItem
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnDeactivateSpecialOrder_Click(object sender, RoutedEventArgs e)
		{
			try
			{
				if (this.dgSpecialOrderItem.SelectedItems.Count > 0)
				{

					if (((SpecialItem)this.dgSpecialOrderItem.SelectedItem).Active == false)
					{
						MessageBox.Show("Special Item already deactivated!");
					}
					else
					{
						MessageBoxResult result = MessageBox.Show("Are you sure you want to Deactivate Item "
							+ ((SpecialItem)this.dgSpecialOrderItem.SelectedItem).SpecialOrderItemID
							+ "?", "Deactivation Warning", MessageBoxButton.YesNo);
						if (result == MessageBoxResult.No)
						{
							return;
						}
						_specialOrderItemManager.DeactivateSpecialOrderItem(((SpecialItem)this.dgSpecialOrderItem.SelectedItem).SpecialOrderItemID);
						refreshSpecialOrderItemList();
					}

				}
				else
				{
					MessageBox.Show("You must select something!");
				}
			}
			catch (Exception ex)
			{

				var message = ex.Message + "\n\n" + ex.InnerException;
				MessageBox.Show(message, "Deactivation Error!", MessageBoxButton.OK, MessageBoxImage.Exclamation);
			}
		}

		/// <summary>
		/// Zachary Hall
		/// Created: 2018/01/31
		/// 
		/// Utility for updating the special order item list with current data from the database.
		/// </summary>
		private void refreshSpecialOrderItemList()
		{
			try
			{
				_specialOrderItemList = _specialOrderItemManager.RetrieveSpecialOrderItems();
				dgSpecialOrderItem.ItemsSource = _specialOrderItemList;
			}
			catch (Exception ex)
			{
				var message = ex.Message + "\n\n" + ex.InnerException.Message;
				Dispatcher.BeginInvoke(new Action(() => MessageBox.Show(message, "Error Retrieving Special Items!", MessageBoxButton.OK, MessageBoxImage.Exclamation)),
					System.Windows.Threading.DispatcherPriority.Normal);
			}
		}

		/// <summary>
		/// Zachary Hall
		/// Created: 2018/01/31
		/// 
		/// Event to be fired when the Special Order Item tab has been selected. 
		/// Refreshes the special order item list and sets the item source by calling refreshSpecialOrderItemList()
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void SpecialOrderItemTab_Selected(object sender, RoutedEventArgs e)
		{
			refreshSpecialOrderItemList();
		}

		private void btnAvailability_Click(object sender, RoutedEventArgs e)
		{
			try
			{
				//check if there is a selected item
				if (this.dgEmployees.SelectedItems.Count > 0)
				{

					//open the market entry form in add mode
					var availabilityForm = new frmAddEditEmployeeAvailability((Employee)this.dgEmployees.SelectedItem, _availabilityManager);
					var result = availabilityForm.ShowDialog();
				}
				else
				{
					MessageBox.Show("You must select an Employee!");
				}
			}
			catch (Exception ex)
			{

				var message = ex.Message + "\n\n" + ex.InnerException;
				MessageBox.Show(message, "Error Saving Availability!", MessageBoxButton.OK, MessageBoxImage.Exclamation);
			}
		}

		private void tabTaskTypes_Selected(object sender, RoutedEventArgs e)
		{
			refreshTaskTypeList();
		}

		/// <summary>
		/// John Miller
		/// Created on 2018/03/26
		/// 
		/// Method to frmAddEditTask if a row is double clicked
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void dgTask_MouseDoubleClick(object sender, MouseButtonEventArgs e)
		{
			if (this.dgTask.SelectedItems.Count > 0)
			{
				try
				{
					var editTaskForm = new frmAddEditTask(_taskManager, (DataObjects.Task)this.dgTask.SelectedItem);
					var result = editTaskForm.ShowDialog();
					if (result == true)
					{
						refreshTaskList();
					}
				}
				catch (Exception ex)
				{
					MessageBox.Show("There was an error selecting the Task." + ex);
				}
			}

		}

		private void dgVendor_MouseDoubleClick(object sender, MouseButtonEventArgs e)
		{
			if (this.dgVendor.SelectedItems.Count > 0)
			{
				try
				{
					var editVendorForm = new frmAddEditVendor(_vendorManager, (Vendor)this.dgVendor.SelectedItem);
					var result = editVendorForm.ShowDialog();
					if (result == true)
					{
						refreshVendorList();
					}
				}
				catch (Exception ex)
				{
					MessageBox.Show("There was an error selecting the Vendor." + ex);
				}
			}
		}
		/// <summary>
		/// Reuben Cassell
		/// Created 3/5/2018
		/// 
		/// Opens the Add Special Order window
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnAddSpecialOrderForm_Click(object sender, RoutedEventArgs e)
		{
			var specialOrderForm = new frmAddEditSpecialOrderForm(_specialOrderManager, _specialOrderItemList, _specialOrderItemManager, _user.Employee.EmployeeID);
			var result = specialOrderForm.ShowDialog();
			if (result == true)
			{
				refreshSpecialOrderList();
			}
		}

		/// <summary>
		/// Reuben Cassell
		/// Created 3/5/2018
		/// 
		/// Refreshes the Special Order data grid to populate it with data
		/// </summary>
		private void refreshSpecialOrderList()
		{

			dgSpecialOrderForm.ItemsSource = null;
			dgSpecialOrderForm.Items.Clear();
			try
			{
				_specialOrderList = _specialOrderManager.RetrieveSpecialOrders();
				dgSpecialOrderForm.ItemsSource = _specialOrderList;
			}
			catch (Exception ex)
			{
				var message = ex.Message + "\n\n" + ex.InnerException.Message;
				Dispatcher.BeginInvoke(new Action(() => MessageBox.Show(message, "Data Retrieval Error.",
					MessageBoxButton.OK, MessageBoxImage.Exclamation)),
					System.Windows.Threading.DispatcherPriority.Normal);

			}
		}

		/// <summary>
		/// Reuben Cassell
		/// Created 3/8/2018
		/// 
		/// Refreshes the special order list when the tab is selected
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void TabItem_SpecialOrderTab_Selected(object sender, RoutedEventArgs e)
		{
			refreshSpecialOrderItemList();
			refreshSpecialOrderList();
		}

		/// <summary>
		/// Reuben Cassell
		/// Created 2/23/2018
		/// 
		/// Opens the AddEditSpecialOrderForm in Edit Mode
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnEditSpecialOrderForm_Click(object sender, RoutedEventArgs e)
		{

			SpecialOrder specialOrder = null;
			if (this.dgSpecialOrderForm.SelectedItems.Count > 0)
			{
				try
				{
					specialOrder = (SpecialOrder)dgSpecialOrderForm.SelectedItem;
					var specialOrderForm = new frmAddEditSpecialOrderForm(_specialOrderManager, DetailFormMode.Edit
						, specialOrder, _specialOrderItemList, _specialOrderItemManager, _user.Employee.EmployeeID);
					var result = specialOrderForm.ShowDialog();
					if (result == true)
					{
						refreshSpecialOrderList();
					}
				}
				catch (Exception ex)
				{
					MessageBox.Show(ex.Message + "\n\n" + ex.InnerException.Message);
				}
			}
			else
			{
				MessageBox.Show("You must select a Special Order to edit.");
			}
		}

		/// <summary>
		/// Reuben Cassell
		/// Created 2/23/2018
		/// 
		/// Deletes a selected Special Order
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnDeleteSpecialOrderForm_Click(object sender, RoutedEventArgs e)
		{
			SpecialOrder deleteOrder = (SpecialOrder)dgSpecialOrderForm.SelectedItem;
			if (deleteOrder != null)
			{
				var messageBoxResult = MessageBox.Show("Are you sure you want to delete Special Order "
													   + deleteOrder.SpecialOrderID + "?",
													   "Confirm Deletion", MessageBoxButton.YesNo);
				if (messageBoxResult == MessageBoxResult.Yes)
				{
					try
					{
						List<SpecialOrderLine> lines = _specialOrderLineManager.RetrieveSpecialOrderLineBySpecialOrderID(deleteOrder.SpecialOrderID);

						int deletedLines = 0;

						foreach (var line in lines)
						{
							deletedLines += _specialOrderLineManager.DeleteSpecialOrderLine(line.SpecialOrderLineID);
						}

						int rowCount = _specialOrderManager.DeleteSpecialOrderByID(
							deleteOrder.SpecialOrderID);
						if (rowCount == 1)
						{
							MessageBox.Show("Special Order deleted.\nRows Affected: " + rowCount + "\n Order Lines deleted: " + deletedLines);
							refreshSpecialOrderList();
						}
						else
						{
							MessageBox.Show("Delete failed. \nRows Affected: " + rowCount);
						}
					}
					catch (Exception ex)
					{
						MessageBox.Show(ex.Message + "\n\n" + ex.InnerException.Message);
					}
				}
			}
			else
			{
				MessageBox.Show("You must select a Special Order to delete.");
			}
		}

		/// <summary>
		/// Reuben Cassell
		/// Created 2/23/2018
		/// 
		/// Opens the AddEditSpecialOrderForm in View Mode
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btViewSpecialOrderFormDetails_Click(object sender, RoutedEventArgs e)
		{
			SpecialOrder specialOrder = (SpecialOrder)dgSpecialOrderForm.SelectedItem;

			if (specialOrder != null)
			{
				specialOrder = (SpecialOrder)dgSpecialOrderForm.SelectedItem;
				var specialOrderForm = new frmAddEditSpecialOrderForm(_specialOrderManager, DetailFormMode.View
					, specialOrder, _specialOrderItemList);
				var result = specialOrderForm.ShowDialog();
			}
			else
			{
				MessageBox.Show("You must select a Special Order to delete.");
			}
		}

		/// <summary>
		/// Noah Davison
		/// Created 2018/02/21
		/// 
		/// Deactivate click event. Deactivates the selected Equipment object
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnDeactivateEquipment_Click(object sender, RoutedEventArgs e)
		{
			EquipmentDetail selectedEquipmentView = (EquipmentDetail)dgEquipment.SelectedItem;
			if (selectedEquipmentView != null)
			{
				Equipment selectedEquipment = selectedEquipmentView.Equipment;
				var messageBoxResult = MessageBox.Show("Are you sure you want to deactivate this item?",
				"Confirm Deactivate", MessageBoxButton.YesNo);
				if (messageBoxResult == MessageBoxResult.Yes)
				{
					try
					{
						_equipmentManager.DeactivateEquipmentByID(selectedEquipment.EquipmentID);
						refreshEquipmentList();
						MessageBox.Show("Equipment deactivated.");
					}
					catch
					{
						MessageBox.Show("Failed to deactivate equipment.");
					}
				}
			}
			else
			{
				MessageBox.Show("You must select a row to deactivate");
			}
		}


		/// <summary>
		/// John Miller
		/// Created on 2018/03/26
		/// 
		/// Method to create new frmAddEditTaskType form to Add a taskType 
		/// when it is double-clicked in the datagrid.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void dgTaskType_MouseDoubleClick(object sender, MouseButtonEventArgs e)
		{
			if (this.dgTaskType.SelectedItems.Count > 0)
			{
				try
				{
					var editTaskTypeForm = new frmAddEditTaskType(_taskTypeManager, (TaskType)this.dgTaskType.SelectedItem);
					var result = editTaskTypeForm.ShowDialog();
					if (result == true)
					{
						refreshTaskTypeList();
					}
				}
				catch (Exception ex)
				{
					MessageBox.Show("There was an error selecting the TaskType." + ex);
				}
			}
		}

		/// <summary>
		/// John Miller
		/// Created on 2018/03/26
		/// 
		/// Method to create a new frmAddEditTaskType form to Add a taskType
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnAddTaskType_Click(object sender, RoutedEventArgs e)
		{
			var taskTypeForm = new frmAddEditTaskType(_taskTypeManager);
			var result = taskTypeForm.ShowDialog();
			if (result == true)
			{
				refreshTaskTypeList();
			}
		}

		/// <summary>
		/// John Miller
		/// Created on 2018/03/26
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnEditTaskType_Click(object sender, RoutedEventArgs e)
		{
			if (this.dgTaskType.SelectedItems.Count > 0)
			{
				var editTaskTypeForm = new frmAddEditTaskType(_taskTypeManager, (TaskType)this.dgTaskType.SelectedItem);
				var result = editTaskTypeForm.ShowDialog();
				if (result == true)
				{
					refreshTaskTypeList();
				}
			}
			else
			{
				MessageBox.Show("You must select a Task.");
			}
		}

		private void btnDeactivateTaskType_Click(object sender, RoutedEventArgs e)
		{
			TaskType taskType = (TaskType)this.dgTaskType.SelectedItem;
			if (taskType != null)
			{
				if (taskType.Active == false)
				{
					MessageBox.Show("This TaskType is currently inactive.");
				}
				else
				{
					try
					{
						_taskTypeManager.DeactivateTaskType(taskType);
						refreshTaskTypeList();
						MessageBox.Show("TaskType deactivated.");
					}
					catch
					{
						MessageBox.Show("Failed to deactivate TaskType.");
					}
				}
			}
			else
			{
				MessageBox.Show("You must select a TaskType to deactivate.");
			}
		}

		private void btnDeleteTaskType_Click(object sender, RoutedEventArgs e)
		{
			try
			{
				MessageBoxResult result = MessageBox.Show("Are you sure you want to Permanently delete TaskType " +
							((TaskType)this.dgTaskType.SelectedItem).Name + "?", "Deletion Warning", MessageBoxButton.YesNo);
				if (result == MessageBoxResult.No)
				{
					return;
				}
				_taskTypeManager.DeleteTaskTypeByID(((TaskType)this.dgTaskType.SelectedItem).TaskTypeID);
				refreshTaskTypeList();
			}
			catch (Exception ex)
			{
				var message = ex.Message + "\n\n" + ex.InnerException;
				MessageBox.Show(message, "Deletion Error", MessageBoxButton.OK, MessageBoxImage.Exclamation);
			}
		}

		/// <summary>
		/// James McPherson
		/// Created 2018/04/02
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void TabItem_InspectionRecord_Selected(object sender, RoutedEventArgs e)
		{
			refreshInspectionRecordDetailList();
		}

		/// <summary>
		/// James McPherson
		/// Created 2018/04/02
		/// </summary>
		private void refreshInspectionRecordDetailList()
		{
			dgInspectionRecord.ItemsSource = null;
			dgInspectionRecord.Items.Clear();
			try
			{
				_inspectionRecordDetailList = _inspectionRecordManager.RetrieveInspectionRecordDetailList();
				dgInspectionRecord.ItemsSource = _inspectionRecordDetailList;
			}
			catch (Exception ex)
			{
				var message = ex.Message + "\n\n" + ex.InnerException.Message;
				Dispatcher.BeginInvoke(new Action(() => MessageBox.Show(message, "Data Retrieval Error.",
					MessageBoxButton.OK, MessageBoxImage.Exclamation)),
					System.Windows.Threading.DispatcherPriority.Normal);
			}
		}

		/// <summary>
		/// Jacob Conley
		/// Created on 2018/03/14
		/// 
		/// Method to refresh supply order data grid
		/// </summary>
		private void refreshSupplyOrderList()
		{
			List<SupplyOrderDetail> _supplyOrderDetailList = new List<SupplyOrderDetail>();
			dgSupplyOrder.ItemsSource = null;
			dgSupplyOrder.Items.Clear();
			try
			{
				_supplyOrderList = _supplyOrderManager.RetrieveSupplyOrderList();
				foreach (var supplyOrder in _supplyOrderList)
				{
					Employee employee = _employeeManager.RetrieveEmployeeByID(supplyOrder.EmployeeID);
					SupplyOrderDetail supplyOrderDetail = new SupplyOrderDetail()
					{
						Order = supplyOrder,
						EmployeeFirstName = employee.FirstName,
						EmployeeLastName = employee.LastName
					};
					_supplyOrderDetailList.Add(supplyOrderDetail);
				}
				dgSupplyOrder.ItemsSource = _supplyOrderDetailList;
			}
			catch (Exception ex)
			{
				var message = ex.Message + "\n\n" + ex.InnerException.Message;
				Dispatcher.BeginInvoke(new Action(() => MessageBox.Show(message, "Data Retrieval Error.",
					MessageBoxButton.OK, MessageBoxImage.Exclamation)),
					System.Windows.Threading.DispatcherPriority.Normal);
			}
		}

		/// <summary>
		/// Jacob Conley
		/// Created on 2018/03/14
		/// 
		/// Method to refresh list when tab is selected
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void TabItem_SupplyOrder_Selected(object sender, RoutedEventArgs e)
		{
			refreshSupplyOrderList();
		}

		/// <summary>
		/// Jacob Conley
		/// Created on 2018/03/14
		/// 
		/// Method to allow a user to add a new supply order
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnAddSupplyOrder_Click(object sender, RoutedEventArgs e)
		{
			var frmAddSupplyOrder = new frmAddEditSupplyOrder(_supplyOrderManager, _user, _supplyItemList);
			try
			{
				var result = frmAddSupplyOrder.ShowDialog();
			}
			catch (Exception ex)
			{
				MessageBox.Show("There was an error adding the supply order." + ex);
			}
			refreshSupplyOrderList();
		}

		/// <summary>
		/// Jacob Conley
		/// Created on 2018/03/14
		/// 
		/// Method to allow a user to edit an existing supply order
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnEditSupplyOrder_Click(object sender, RoutedEventArgs e)
		{
			if (this.dgSupplyOrder.SelectedItems.Count == 0)
			{
				MessageBox.Show("You must select a supply order.");
				return;
			}

			SupplyOrderDetail supplyOrder = null;
			supplyOrder = (SupplyOrderDetail)this.dgSupplyOrder.SelectedItem;
			var supplyID = supplyOrder.Order.SupplyOrderID;
			try
			{
				var supplyOrderItems = _supplyOrderItemManager.RetrieveSupplyOrderItemsByID(supplyID);
				var supplyOrderItemDetails = createDetails(supplyOrderItems);
				var editForm = new frmAddEditSupplyOrder(_supplyOrderManager, _user, supplyOrder.Order, supplyOrderItemDetails, _supplyItemList, DetailFormMode.Edit);
				var result = editForm.ShowDialog();
				if (result == true)
				{
					refreshSupplyOrderList();
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show("There was an error editing the supply order." + ex);
			}
		}

		/// <summary>
		/// Jacob Conley
		/// Created on 2018/03/14
		/// 
		/// Method to create the supply item that go along with a supply order
		/// </summary>
		/// <param name="supplyOrderItems"></param>
		/// <returns></returns>
		private List<SupplyOrderItemDetail> createDetails(List<SupplyOrderItem> supplyOrderItems)
		{
			List<SupplyOrderItemDetail> details = new List<SupplyOrderItemDetail>();

			foreach (var order in supplyOrderItems)
			{
				details.Add(new SupplyOrderItemDetail()
				{
					Name = _supplyItemList.Find(i => i.SupplyItemID == order.SupplyItemID).Name,
					OrderItem = order
				});
			}

			return details;
		}

		/// <summary>
		/// Jacob Conley
		/// Created on 2018/03/14
		/// 
		/// Method that 'deactivates' an existing supply order
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnDeactivateSupplyOrder_Click(object sender, RoutedEventArgs e)
		{
			if (this.dgSupplyOrder.SelectedItems.Count == 0)
			{
				MessageBox.Show("You must select a supply order.");
				return;
			}
			var order = (SupplyOrderDetail)dgSupplyOrder.SelectedItem;
			MessageBox.Show("Cancelling Order: " + order.Order.SupplyOrderID + ".", "Cancellation in progress...", MessageBoxButton.OK);
			_supplyOrderManager.DeleteSupplyOrder(order.Order.SupplyOrderID, order.Order.SupplyStatusID);
			refreshSupplyOrderList();
		}


		/// <summary>
		/// Jacob Conley
		/// Created 2018/03/29
		/// 
		/// Task Type Supply Need selected event
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void TabItem_Task_Type_Supply_Need_Selected(object sender, RoutedEventArgs e)
		{
			refreshTaskTypeList();
			refreshSupplyItemList();
			refreshTaskTypeSupplyNeedList();
		}

		/// <summary>
		/// Jacob Conley
		/// Created on 2018/03/29
		/// 
		/// Method to refresh Task Type Supply Need data grid
		/// </summary>
		private void refreshTaskTypeSupplyNeedList()
		{
			List<TaskTypeSupplyNeedDetail> _taskTypeSupplyNeedDetailsList = new List<TaskTypeSupplyNeedDetail>();
			dgSupplyOrder.ItemsSource = null;
			dgSupplyOrder.Items.Clear();
			try
			{
				_taskTypeSupplyNeedList = _taskTypeSupplyNeedManager.RetrieveTaskTypeSupplyNeedList();
				foreach (var taskTypeSupplyNeed in _taskTypeSupplyNeedList)
				{
					var taskName = _taskTypeList.Find(t => t.TaskTypeID == taskTypeSupplyNeed.TaskTypeID).Name;
					var itemName = _supplyItemList.Find(i => i.SupplyItemID == taskTypeSupplyNeed.SupplyItemID).Name;
					TaskTypeSupplyNeedDetail taskTypeSupplyNeedDetails = new TaskTypeSupplyNeedDetail()
					{
						TaskName = taskName,
						ItemName = itemName,
						Supply = taskTypeSupplyNeed
					};
					_taskTypeSupplyNeedDetailsList.Add(taskTypeSupplyNeedDetails);
				}
				dgTaskTypeSupplyNeed.ItemsSource = _taskTypeSupplyNeedDetailsList;
			}
			catch (Exception ex)
			{
				var message = ex.Message + "\n\n" + ex.InnerException.Message;
				Dispatcher.BeginInvoke(new Action(() => MessageBox.Show(message, "Data Retrieval Error.",
					MessageBoxButton.OK, MessageBoxImage.Exclamation)),
					System.Windows.Threading.DispatcherPriority.Normal);
			}
		}

		/// <summary>
		/// James McPherson
		/// Created 2018/04/02
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnAddInspectionRecord_Click(object sender, RoutedEventArgs e)
		{
			var inspectionRecordForm = new frmAddEditInspectionRecord(_inspectionRecordManager
				, _equipmentManager, _employeeManager, _user);
			var result = inspectionRecordForm.ShowDialog();
			if (result == true)
			{
				refreshInspectionRecordDetailList();
			}
		}

		/// <summary>
		/// James McPherson
		/// Created 2018/04/02
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnEditInspectionRecord_Click(object sender, RoutedEventArgs e)
		{
			InspectionRecordDetail inspectionRecordDetail = null;
			if (this.dgInspectionRecord.SelectedItems.Count > 0)
			{
				try
				{
					inspectionRecordDetail = (InspectionRecordDetail)dgInspectionRecord.SelectedItem;
					var inspectionRecordForm = new frmAddEditInspectionRecord(_inspectionRecordManager
						, _equipmentManager, _employeeManager, DetailFormMode.Edit, inspectionRecordDetail);
					var result = inspectionRecordForm.ShowDialog();
					if (result == true)
					{
						refreshInspectionRecordDetailList();
					}
				}
				catch (Exception ex)
				{
					MessageBox.Show(ex.Message + "\n\n" + ex.InnerException.Message);
				}
			}
			else
			{
				MessageBox.Show("You must select an inspection record to edit.");
			}
		}

		/// <summary>
		/// James McPherson
		/// Created 2018/04/02
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnDeleteInspectionRecord_Click(object sender, RoutedEventArgs e)
		{
			InspectionRecordDetail selectedInspectionRecord
				= (InspectionRecordDetail)dgInspectionRecord.SelectedItem;
			if (selectedInspectionRecord != null)
			{
				try
				{
					_inspectionRecordManager.DeleteInspectionRecord(selectedInspectionRecord.InspectionRecordID);
					refreshInspectionRecordDetailList();
					MessageBox.Show("Inspection record deleted.");
				}
				catch
				{
					MessageBox.Show("Failed to delete inspection record.");
				}
			}
			else
			{
				MessageBox.Show("You must select an inspection record to delete.");
			}
		}

		/// <summary>
		/// Jacob Conley
		/// Created on 2018/03/29
		/// 
		/// Method to create the task type supply need item
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnAddTaskTypeSupplyNeed_Click(object sender, RoutedEventArgs e)
		{
			var frmAddSupplyOrder = new frmAddEditTaskTypeSupplyNeed(_taskTypeSupplyNeedManager, _taskTypeList, _supplyItemList);
			try
			{
				var result = frmAddSupplyOrder.ShowDialog();
			}
			catch (Exception ex)
			{
				MessageBox.Show("There was an error adding the task type supply need." + ex);
			}
			refreshTaskTypeSupplyNeedList();
		}

		/// <summary>
		/// Jacob Conley
		/// Created on 2018/03/29
		/// 
		/// Method to allow a user to edit an existing task type supply need item
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnEditTaskTypeSupplyNeed_Click(object sender, RoutedEventArgs e)
		{

			if (this.dgTaskTypeSupplyNeed.SelectedItems.Count == 0)
			{
				MessageBox.Show("You must select a task type supply need.");
				return;
			}

			TaskTypeSupplyNeedDetail detail = null;
			detail = (TaskTypeSupplyNeedDetail)this.dgTaskTypeSupplyNeed.SelectedItem;
			var taskTypeSupply = detail.Supply;
			try
			{
				var editForm = new frmAddEditTaskTypeSupplyNeed(_taskTypeSupplyNeedManager, taskTypeSupply, _taskTypeList, _supplyItemList, DetailFormMode.Edit);
				var result = editForm.ShowDialog();
				if (result == true)
				{
					refreshTaskTypeSupplyNeedList();
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show("There was an error editing the task type supply need." + ex);
			}
		}

		/// <summary>
		/// Jacob Conley
		/// Created on 2018/03/29
		/// 
		/// Method that deactivates an existing task type supply need
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
        private void btnDeactivateTaskTypeSupplyNeed_Click(object sender, RoutedEventArgs e)
		{

			if (this.dgTaskTypeSupplyNeed.SelectedItems.Count == 0)
			{
				MessageBox.Show("You must select a Task Type Supply Need item.");
				return;
			}
			var taskTypeSupplyNeed = (TaskTypeSupplyNeedDetail)dgTaskTypeSupplyNeed.SelectedItem;
			MessageBox.Show("Removing Supply: " + taskTypeSupplyNeed.ItemName + ".", "Removal in progress...", MessageBoxButton.OK);
			_taskTypeSupplyNeedManager.DeactivateTaskTypeSupplyNeedItem(taskTypeSupplyNeed.Supply.TaskTypeSupplyNeedID);
			refreshTaskTypeSupplyNeedList();
		}

		private void btnDeactivateEmployeeRole_Click(object sender, RoutedEventArgs e)
		{
			try
			{
				if (this.dgEmployeeRole.SelectedItems.Count > 0)
				{
					if (((EmployeeRoleDetail)this.dgEmployeeRole.SelectedItem).EmployeeRole.Active == false)
					{
						MessageBox.Show("EmployeeRole is already inactive.");
					}

					else
					{
						MessageBoxResult result = MessageBox.Show("Are you sure you want to Deactivate Item "
							+ ((EmployeeRoleDetail)this.dgEmployeeRole.SelectedItem).EmployeeRole.RoleID
							+ "?", "Deactivation Warning", MessageBoxButton.YesNo);
						if (result == MessageBoxResult.No)
						{
							return;
						}
						_employeeRoleManager.DeactivateEmployeeRole(((EmployeeRoleDetail)this.dgEmployeeRole.SelectedItem));
						RefreshEmployeeRoleList();
					}
				}
				else
				{
					MessageBox.Show("Please select an EmployeeRole.");
				}
			}
			catch (Exception ex)
			{
				var message = ex.Message + "\n\n" + ex.InnerException;
				MessageBox.Show(message, "Deactivation error.", MessageBoxButton.OK, MessageBoxImage.Exclamation);
			}
		}

		/// <summary>
		/// John Miller
		/// Created on 2018/04/01
		/// 
		/// Method to create a new frmAddEditEmployeeRole form to Add an EmployeeRole
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnAddEmployeeRole_Click(object sender, RoutedEventArgs e)
		{
			var employeeRoleForm = new frmAddEditEmployeeRole(_employeeRoleManager);
			var result = employeeRoleForm.ShowDialog();
			if (result == true)
			{
				RefreshEmployeeRoleList();
			}
		}
		/// Zachary Hall
		/// Created on 2018/04/01
		/// 
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnResources_Click(object sender, RoutedEventArgs e)
		{
			if (this.dgJobs.SelectedItems.Count > 0)
			{
				var editForm = new frmEditJobResourceAllocation((JobDetail)this.dgJobs.SelectedItem);
				var result = editForm.ShowDialog();
				refreshJobList();
			}
			else
			{
				MessageBox.Show("You must select something!");
			}
		}

		/// <summary>
		/// Brady Feller
		/// Created 2018/03/26
		/// 
		/// Opens the 'Add' window for JobLocationATtributeType
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnAddJobLocationAttributeType_Click(object sender, RoutedEventArgs e)
		{
			var frmAddEditJobLocationAttributeType = new frmAddEditJobLocationAttributeType(_jobLocationAttributeTypeManager);
			var result = frmAddEditJobLocationAttributeType.ShowDialog();
			if (result == true)
			{
				dgJobLocationAttributeType.ItemsSource = _jobLocationAttributeTypeList;
				refreshJobLocationAttributeTypeList();
			}
		}

		/// <summary>
		/// Brady Feller
		/// Created 2018/03/26
		/// 
		/// Opens the 'Edit' window for JobLocationATtributeType
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnEditJobLocationAttributeType_Click(object sender, RoutedEventArgs e)
		{
			JobLocationAttributeType jobLocationAttributeType = null;
			JobLocationAttributeType jlat = null;
			if (this.dgJobLocationAttributeType.SelectedItems.Count > 0)
			{
				jobLocationAttributeType = (JobLocationAttributeType)this.dgJobLocationAttributeType.SelectedItem;
				try
				{
					jlat = _jobLocationAttributeTypeManager.RetrieveJobLocationAttributeTypeByID(jobLocationAttributeType.JobLocationAttributeTypeID);
					var detailForm = new frmAddEditJobLocationAttributeType(_jobLocationAttributeTypeManager, jlat, DetailFormMode.Edit);
					var result = detailForm.ShowDialog();
					if (result == true)
					{
						refreshJobLocationAttributeTypeList();
						dgEquipmentStatus.ItemsSource = _equipmentStatusList;
					}
				}
				catch (Exception ex)
				{
					MessageBox.Show(ex.Message + "\n\n" + ex.InnerException.Message, "Error.");
					throw;
				}
			}
			else
			{
				MessageBox.Show("Please select a status.");
			}
		}

		private void tabJobLocationAttributeType_Selected(object sender, RoutedEventArgs e)
		{
			refreshJobLocationAttributeTypeList();
		}

		/// <summary>
		/// Brady Feller
		/// Created 2018/03/26
		/// 
		/// Refreshes the JobLocationAttribute List
		/// </summary>
		private void refreshJobLocationAttributeTypeList()
		{
			dgJobLocationAttributeType.ItemsSource = null;
			dgJobLocationAttributeType.Items.Clear();
			try
			{
				_jobLocationAttributeTypeList = _jobLocationAttributeTypeManager.RetrieveJobLocationAttributeTypeList();
				dgJobLocationAttributeType.ItemsSource = _jobLocationAttributeTypeList;
			}
			catch (Exception ex)
			{
				var message = ex.Message + "\n\n" + ex.InnerException;
				MessageBox.Show(message, "Error Retrieving Job Location Attribute Type List!", MessageBoxButton.OK, MessageBoxImage.Exclamation);
			}
		}

		/// <summary>
		///     Noah Davison
		///     Created 2018/02/08
		///     
		///     Creates window to add new equipment
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnAddEquipment_Click(object sender, RoutedEventArgs e)
		{
			new frmAddEditEquipment(DetailFormMode.Add).ShowDialog();
			refreshEquipmentList();
		}

		/// <summary>
		/// Brady Feller
		/// Created 2018/04/04
		/// 
		/// Refreshes TaskTypeEquipmentNeed list
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void tabTaskTypeEquipmentNeed_Selected(object sender, RoutedEventArgs e)
		{
			refreshTaskTypeList();
			refreshTaskTypeEquipmentNeedList();
		}

		/// <summary>
		/// Brady Feller
		/// Created 2018/04/04
		/// 
		/// Retrieves a TaskTypeEquipmentNeed form for adding a record
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnAddTaskTypeEquipmentNeed_Click(object sender, RoutedEventArgs e)
		{
			var frmAddTaskTypeEquipmentNeed = new frmAddEditTaskTypeEquipmentNeed(_taskTypeEquipmentNeedManager, _taskTypeList, _equipmentTypeList);
			try
			{
				var result = frmAddTaskTypeEquipmentNeed.ShowDialog();
			}
			catch (Exception ex)
			{
				MessageBox.Show("There was an error adding the task type equipment need." + ex);
			}
			refreshTaskTypeEquipmentNeedList();
		}

		/// <summary>
		/// Brady Feller
		/// Created 2018/04/04
		/// 
		/// Retrieves a TaskTypeEquipmentNeed form for editing a record
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnEditTaskTypeEquipmentNeed_Click(object sender, RoutedEventArgs e)
		{
			if (this.dgTaskTypeEquipmentNeed.SelectedItems.Count == 0)
			{
				MessageBox.Show("You must select a task type supply need.");
				return;
			}

			TaskTypeEquipmentNeedDetailList detail = null;
			detail = (TaskTypeEquipmentNeedDetailList)this.dgTaskTypeEquipmentNeed.SelectedItem;
			var taskTypeEquipmentNeed = detail.TaskTypeEquipmentNeed;
			try
			{
				var editForm = new frmAddEditTaskTypeEquipmentNeed(_taskTypeEquipmentNeedManager, taskTypeEquipmentNeed, _taskTypeList, _equipmentTypeList, DetailFormMode.Edit);
				var result = editForm.ShowDialog();
				if (result == true)
				{
					refreshTaskTypeEquipmentNeedList();
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show("There was an error editing the task type supply need." + ex);
			}
		}

		/// <summary>
		/// Brady Feller
		/// Created 2018/04/04
		/// 
		/// Refreshes the TaskTypeEquipmentNeed List
		/// </summary>
		private void refreshTaskTypeEquipmentNeedList()
		{
			List<TaskTypeEquipmentNeedDetailList> _taskTypeEquipmentNeedDetailsList = new List<TaskTypeEquipmentNeedDetailList>();
			dgSupplyOrder.ItemsSource = null;
			dgSupplyOrder.Items.Clear();
			try
			{
				_taskTypeEquipmentNeedList = _taskTypeEquipmentNeedManager.RetrieveTaskTypeEquipmentNeedList();
				foreach (var taskTypeEquipmentNeed in _taskTypeEquipmentNeedList)
				{
					var taskTypeName = _taskTypeList.Find(t => t.TaskTypeID == taskTypeEquipmentNeed.TaskTypeID).Name;
					TaskTypeEquipmentNeedDetailList taskTypeEquipmentNeedDetails = new TaskTypeEquipmentNeedDetailList()
					{
						TaskTypeName = taskTypeName,
						TaskTypeEquipmentNeed = taskTypeEquipmentNeed
					};
					_taskTypeEquipmentNeedDetailsList.Add(taskTypeEquipmentNeedDetails);
				}
				dgTaskTypeEquipmentNeed.ItemsSource = _taskTypeEquipmentNeedDetailsList;
			}
			catch (Exception ex)
			{
				var message = ex.Message + "\n\n" + ex.InnerException.Message;
				Dispatcher.BeginInvoke(new Action(() => MessageBox.Show(message, "Data Retrieval Error.",
					MessageBoxButton.OK, MessageBoxImage.Exclamation)),
					System.Windows.Threading.DispatcherPriority.Normal);
			}
		}

		/// <summary>
		/// Brady Feller
		/// Created 2018/03/22
		/// 
		/// Displays the 'Add' form for EmployeeCertification
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnAddEmployeeCertification_Click(object sender, RoutedEventArgs e)
		{
			var frmAddEditEmployeeCertification = new frmAddEditEmployeeCertification(_employeeCertificationManager, _certificationManager, _employeeManager);
			var result = frmAddEditEmployeeCertification.ShowDialog();
			if (result == true)
			{
				dgEmployeeCertification.ItemsSource = _employeeCertificationList;
				refreshEmployeeCertificationList();
			}
		}

		/// <summary>
		/// Brady Feller
		/// Created 2018/03/22
		/// 
		/// Displays the 'Edit' form for EmployeeCertification
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnEditEmployeeCertificatione_Click(object sender, RoutedEventArgs e)
		{
			EmployeeCertificationDetail ecDetail = null;
			if (this.dgEmployeeCertification.SelectedItems.Count > 0)
			{
				ecDetail = ((EmployeeCertificationDetail)dgEmployeeCertification.SelectedItem);
				ecDetail.EmployeeCertification = new EmployeeCertification()
				{
					CertificationID = ecDetail.Certification.CertificationID,
					Active = ecDetail.Certification.Active,
					EmployeeID = ecDetail.Employee.EmployeeID,
					EndDate = (DateTime)ecDetail.EndDate
				};

				try
				{
					var detailForm = new frmAddEditEmployeeCertification(_employeeCertificationManager, _certificationManager, _employeeManager, ecDetail, DetailFormMode.Edit);
					var result = detailForm.ShowDialog();
					if (result == true)
					{
						refreshEmployeeCertificationList();
					}
				}
				catch (Exception ex)
				{
					MessageBox.Show(ex.Message + "\n\n" + ex.InnerException.Message);
				}
			}
			else
			{
				MessageBox.Show("You need to select something!");
			}
		}

		private void btnEditEmployeeRole_Click(object sender, RoutedEventArgs e)
		{
			EmployeeRoleDetail item = null;
			if (this.dgEmployeeRole.SelectedItems.Count > 0)
			{
				item = (EmployeeRoleDetail)dgEmployeeRole.SelectedItem;
				try
				{
					var detailForm = new frmAddEditEmployeeRole(_employeeRoleManager, item);
					var result = detailForm.ShowDialog();
					if (result == true)
					{
						RefreshEmployeeRoleList();
					}
				}
				catch (Exception ex)
				{
					MessageBox.Show(ex.Message + "\n\n" + ex.InnerException.Message);
				}
			}
			else
			{
				MessageBox.Show("Please select an EmployeeRole.");
			}
		}
		/// <summary>
		/// Brady Feller
		/// Created 2018/04/09
		/// 
		/// Refreshes the EquipmentTypeList when the tab is clicked
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void tabEquipmentType_Selected(object sender, RoutedEventArgs e)
		{
			refreshEquipmentTypeList();
		}

		private void btnEditCustomerType_Click(object sender, RoutedEventArgs e)
		{
			if (this.dgCustomerTypes.SelectedItems.Count == 0)
			{
				MessageBox.Show("You must select a customer type.");
				return;
			}

			CustomerType detail = null;
			detail = (CustomerType)this.dgCustomerTypes.SelectedItem;
			var customerType = detail;
			try
			{
				var editForm = new frmAddEditCustomerType(_customerTypeManager, customerType);
				var result = editForm.ShowDialog();
				if (result == false)
				{
					refreshCustomerTypeList();
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show("There was an error editing the customer type." + ex);
			}

		}

		/// <summary>
		/// Dan Cable
		/// Created 2018/03/29
		/// 
		/// Displays the 'edit' form for Employee
		/// </summary>
        /// <remarks>
        /// Noah Davison
        /// Updated 2018/03/29
        /// 
        /// Fixed crash when nothing is selected.
        /// </remarks>
		private void btnEditEmployee_Click(object sender, RoutedEventArgs e)
		{
            Employee selectedEmployee = (Employee)this.dgEmployees.SelectedItem;
			
            if (selectedEmployee != null)
            {
                var editForm = new frmAddEditEmployee(_employeeManager, selectedEmployee);
                var result = editForm.ShowDialog();
            }
            else
            {
                MessageBox.Show("You must select something!");
            }
			refreshEmployeeList();

			
		}


		/// <summary>
		/// Weston Olund
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnDeactivateResupplyOrder_Click(object sender, RoutedEventArgs e)
		{
			if (dgResupplyOrder.SelectedItems.Count != 1)
			{
				MessageBox.Show("You must select an order to cancel");
			}
			else
			{
				var resupplyOrderDetail = (ResupplyOrderDetail)dgResupplyOrder.SelectedItem;
				try
				{
					_resupplyOrderLineManager.DeleteResupplyOrderLineByResupplyOrderID(resupplyOrderDetail.ResupplyOrder.ResupplyOrderID);
					_resupplyOrderManager.DeleteResupplyOrderByID(resupplyOrderDetail.ResupplyOrder.ResupplyOrderID);
					refreshResupplyOrderList();
					MessageBox.Show("Resupply Order was successfully cancelled.");
				}
				catch (Exception)
				{
					MessageBox.Show("There was an error deleting the Resupply Order");
				}
			}
		}

		/// <summary>
		/// Mike Mason
		/// Created 2018/04/12
		/// 
		/// Button Event for launching the Create a CustomerType form
		/// </summary>
		private void btnAddCustomerType_Click(object sender, RoutedEventArgs e)
		{
			var customerTypeForm = new frmAddEditCustomerType(_customerTypeManager);
			var result = customerTypeForm.ShowDialog();
			if (result == true)
			{
				refreshCustomerTypeList();
			}
		}

		/// <summary>
		/// Jacob Conley
		/// Created 2018/04/05
		/// 
		/// Opens the add version of the service offering window
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnAddServiceOffering_Click(object sender, RoutedEventArgs e)
		{
			refreshServiceItem();
			var frmAddEditServiceOffering = new frmAddEditServiceOffering(_serviceOfferingManager, _serviceOfferingItemManager, _serviceItem);
			try
			{
				var result = frmAddEditServiceOffering.ShowDialog();
			}
			catch (Exception ex)
			{
				MessageBox.Show("There was an error adding the task type supply need." + ex);
			}
			refreshServiceOfferingsList();
		}

		/// <summary>
		/// Jacob Conley
		/// 2018/04/05
		/// 
		/// Opens the edit version of the service offering window
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnEditServiceOffering_Click(object sender, RoutedEventArgs e)
		{
			if (this.dgServiceOffering.SelectedItems.Count == 0)
			{
				MessageBox.Show("You must select a service offering.");
				return;
			}

			try
			{
				refreshServiceItem();
				var serviceOffering = (ServiceOffering)dgServiceOffering.SelectedItem;
				var frmAddEditServiceOffering = new frmAddEditServiceOffering(_serviceOfferingManager, _serviceOfferingItemManager, _serviceItem, serviceOffering, DetailFormMode.Edit);
				var result = frmAddEditServiceOffering.ShowDialog();
				if (result == true)
				{
					refreshServiceOfferingsList();
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show("There was an error editing the service offering." + ex);
			}
		}

		/// <summary>
		/// Marshall Sejkora
		/// 
		/// Adds a role
		/// 
		/// Jacob Slaubaugh
		/// Updated 2018/04/13
		/// 
		/// Called the wrong method to refresh
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnAddRole_Click(object sender, RoutedEventArgs e)
		{
			var frmAddEditRole = new frmAddEditRole(_roleManager);
			var result = frmAddEditRole.ShowDialog();
			if (result == true)
			{
				refreshRoleList();
			}
		}

        /// <summary>
        /// Marshall Sejkora
        /// 
        /// Edits a role
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
		private void btnEditRole_Click(object sender, RoutedEventArgs e)
		{
			if (this.dgRole.SelectedItems.Count == 0)
			{
				MessageBox.Show("You must select a Role.");
				return;
			}
			try
			{
				var role = (Role)dgRole.SelectedItem;
				var frmAddEditRole = new frmAddEditRole(_roleManager, role, AddEditMode.edit);
				var result = frmAddEditRole.ShowDialog();
				if (result == true)
				{
					refreshRoleList();
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show("There was an error editing the Role." + ex);
			}
		}

		/// <summary>
		/// Badis Saidani
        ///
		/// Updated 2018/04/24
		/// 
		/// Refresh role list
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void refreshRoleList()
		{
			this.dgRole.ItemsSource = null;
			dgRole.Items.Clear();
			try
			{
				_roleList = _roleManager.RetrieveRolesList();
				this.dgRole.ItemsSource = _roleList;
			}
			catch (Exception ex)
			{
				var message = ex.Message + "\n\n" + ex.InnerException.Message;

				Dispatcher.BeginInvoke(new Action(() => MessageBox.Show(message, "Data Retrieval Error.",
					MessageBoxButton.OK, MessageBoxImage.Exclamation)),
					System.Windows.Threading.DispatcherPriority.Normal);
			}
		}

		/// <summary>
		/// Badis Saidani
		/// Updated 2018/04/24
		/// 
		/// Deletes the selected Role
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnDeleteRole_Click(object sender, RoutedEventArgs e)
		{
			int result = 0;
			if (this.dgRole.SelectedItems.Count == 0)
			{
				MessageBox.Show("You must select a Role.");
				return;
			}
			try
			{
				var role = (Role)dgRole.SelectedItem;
				result = _roleManager.DeleteRole(role);
			}
			catch (Exception ex)
			{
				MessageBox.Show("There was an error deleting the Role." + ex);
			}

			if (result > 0)
			{
				MessageBox.Show("Role has been deleted.");
				refreshRoleList();
			}
		}

		/// <summary>
		/// Jacob Slaubaugh
		/// Created 2018/04/13
		/// 
		/// Refreshes the data grid with the tab is selected
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void TabItem_Roles_Selected(object sender, RoutedEventArgs e)
		{
			refreshRoleList();
		}

		/// <summary>
		/// Created by Zachary Hall
		/// 3/27/2018
		/// 
		/// Refreshes the Task Type Employee Need  Detail list
		/// </summary>
		private void refreshTaskTypeEmployeeNeedDetailList()
		{
			dgTaskTypeEmployeeNeed.ItemsSource = null;
			dgTaskTypeEmployeeNeed.Items.Clear();

			try
			{
				_taskTypeEmployeeNeedDetailList = _taskTypeEmployeeNeedManager.RetrieveTaskTypeEmployeeDetailList();
				dgTaskTypeEmployeeNeed.ItemsSource = _taskTypeEmployeeNeedDetailList;


			}
			catch (Exception ex)
			{

				var message = ex.Message + "\n\n" + ex.InnerException.Message;
				MessageBox.Show(message, "Data Retrieval Error.", MessageBoxButton.OK, MessageBoxImage.Exclamation);
			}
		}

		/// <summary>
		/// Created by Zachary Hall
		/// 3/29/2018
		/// 
		/// TaskTypeEmployeeNeed tab selected event
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void tabTaskTypeEmployeeNeed_Selected(object sender, RoutedEventArgs e)
		{
			refreshTaskTypeEmployeeNeedDetailList();
		}

		private void btnAddTaskTypeEmployeeNeed_Click(object sender, RoutedEventArgs e)
		{
			var detailForm = new frmAddEditTaskTypeEmployeeNeed(_taskTypeEmployeeNeedManager);
			var result = detailForm.ShowDialog();
			if (result == true)
			{
				refreshTaskTypeEmployeeNeedDetailList();
			}
		}

		private void btnEditTaskTypeEmployeeNeed_Click(object sender, RoutedEventArgs e)
		{
			if (this.dgTaskTypeEmployeeNeed.SelectedItems.Count > 0)
			{
				var editForm = new frmAddEditTaskTypeEmployeeNeed(_taskTypeEmployeeNeedManager, (TaskTypeEmployeeNeedDetail)this.dgTaskTypeEmployeeNeed.SelectedItem);
				var result = editForm.ShowDialog();
				if (result == true)
				{
					refreshTaskTypeEmployeeNeedDetailList();
				}
			}
			else
			{
				MessageBox.Show("You must select something!");
			}
		}

		/// <summary>
		///  Created by Zachary Hall
		/// 3/29/2018
		/// 
		/// Deactivate TaskTypeEmployeeNeedEvent
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnDeactivateTaskTypeEmployeeNeed_Click(object sender, RoutedEventArgs e)
		{
			try
			{
				if (this.dgTaskTypeEmployeeNeed.SelectedItems.Count > 0)
				{

					if (((TaskTypeEmployeeNeedDetail)this.dgTaskTypeEmployeeNeed.SelectedItem).TaskTypeEmployeeNeed.Active == false)
					{
						MessageBox.Show("Need already deactivated!");
					}
					else
					{
						MessageBoxResult result = MessageBox.Show("Are you sure you want to Deactivate the selected Need "
							+ ((TaskTypeEmployeeNeedDetail)this.dgTaskTypeEmployeeNeed.SelectedItem).TaskTypeEmployeeNeed.TaskTypeID
							+ "?", "Deactivation Warning", MessageBoxButton.YesNo);
						if (result == MessageBoxResult.No)
						{
							return;
						}
						var deactivateResult = _taskTypeEmployeeNeedManager.DeactivateTaskTypeEmployeeNeedByID(((TaskTypeEmployeeNeedDetail)this.dgTaskTypeEmployeeNeed.SelectedItem).TaskTypeEmployeeNeed.TaskTypeID);
						MessageBox.Show("Need was deactivated!");
						refreshTaskTypeEmployeeNeedDetailList();
					}

				}
				else
				{
					MessageBox.Show("You must select something!");
				}
			}
			catch (Exception ex)
			{

				var message = ex.Message + "\n\n" + ex.InnerException;
				MessageBox.Show(message, "Deactivation Error!", MessageBoxButton.OK, MessageBoxImage.Exclamation);
			}
		}

		/// <summary>
		/// James McPherson
		/// Created 2018/04/19
		/// 
		/// Open the Receiving window for a SupplyOrder
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnReceivingSupplyOrder_Click(object sender, RoutedEventArgs e)
		{
			if (dgSupplyOrder.SelectedItems.Count == 0)
			{
				MessageBox.Show("You must select a Supply Order.");
			} else
			{
				try
				{
					var selectedSupplyOrder = (SupplyOrderDetail)dgSupplyOrder.SelectedItem;
					var supplyOrderItems = _supplyOrderItemManager
						.RetrieveSupplyOrderItemsByID(selectedSupplyOrder.Order.SupplyOrderID);
					var supplyOrderItemDetails = createDetails(supplyOrderItems);
					var frmReceiving = new frmReceivingSupplyOrder(_supplyOrderItemManager
						, selectedSupplyOrder.Order, supplyOrderItemDetails);
					var result = frmReceiving.ShowDialog();
					if (result == true)
					{
						MessageBox.Show("Supply order updated.");
						refreshSupplyOrderList();
					}
				}
				catch
				{
					MessageBox.Show("There was a problem updating the supply order.");
				}
			}
		}

		private void btnAddSource_Click(object sender, RoutedEventArgs e)
		{
			var addSourceForm = new frmAddEditSource(_sourceManager, _vendorManager, _supplyItemManager, _specialOrderItemManager);
			var result = addSourceForm.ShowDialog();

			if (result == true)
			{
				refreshSourceList();
			}
		}

		private void btnEditSource_Click(object sender, RoutedEventArgs e)
		{
			if (this.dgSource.SelectedItems.Count > 0)
			{
				var editForm = new frmAddEditSource(_sourceManager, (SourceDetail)this.dgSource.SelectedItem, _vendorManager, _supplyItemManager, _specialOrderItemManager);
				var result = editForm.ShowDialog();
				if (result == false)
				{
					refreshSourceList();
				}
			}
			else
			{
				MessageBox.Show("You must select something!");
			}
		}

		/// <summary>
		/// Jayden Tollefson
		/// Created: 2018/02/02
		/// 
		/// Utility for updating the source list with current data from the database.
		/// </summary>
		private void refreshSourceList()
		{
			try
			{
				_sourceDetail = _sourceManager.RetrieveSourceDetailList();
				dgSource.ItemsSource = _sourceDetail;
			}
			catch (Exception ex)
			{
				var message = ex.Message + "\n\n" + ex.InnerException;
				MessageBox.Show(message, "Error Retrieving Sources.", MessageBoxButton.OK, MessageBoxImage.Exclamation);
			}
		}

		/// <summary>
		/// Jayden Tollefson
		/// Created: 2018/02/02
		/// 
		/// Event to be fired when the Source tab has been selected. 
		/// Refreshes the source list and sets the item source by calling refreshSourceList()
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void Source_Selected(object sender, RoutedEventArgs e)
		{
			refreshSourceList();
		}

		/// <summary>
		/// Zachary Hall
		/// 2018/04/19
		/// </summary>
		private void btnResupplyOrderReceiving_Click(object sender, RoutedEventArgs e)
		{
			if (this.dgResupplyOrder.SelectedItems.Count > 0)
			{
				var receivingForm = new frmResupplyOrderReceiving(_resupplyOrderManager, _resupplyOrderLineManager, (ResupplyOrderDetail)this.dgResupplyOrder.SelectedItem);
				var result = receivingForm.ShowDialog();
				if (result == true)
				{
					refreshResupplyOrderList();
				}

			}
			else
			{
				MessageBox.Show("You must select something!");
			}
		}

		/// <summary>
		/// Zachary Hall
		/// 2018/04/20
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnReceiveSpecialOrder_Click(object sender, RoutedEventArgs e)
		{
			if (this.dgSpecialOrderForm.SelectedItems.Count > 0)
			{
				try
				{
					var specialOrder = (SpecialOrder)dgSpecialOrderForm.SelectedItem;
					var receiveForm = new frmSpecialOrderReceiving(_specialOrderManager, _specialOrderLineManager, specialOrder);
					var result = receiveForm.ShowDialog();
					if (result == true)
					{
						refreshSpecialOrderList();
					}
				}
				catch (Exception ex)
				{
					MessageBox.Show(ex.Message + "\n\n" + ex.InnerException.Message);
				}
			}
			else
			{
				MessageBox.Show("You must select a Special Order to receive.");
			}
		}

		/// <summary>
		/// Jacob Slaubaugh
		/// Created 2018/04/19
		/// 
		/// Refreshes the maintenance record dg when the tab is selected
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void TabItem_MaintenanceRecord_Selected(object sender, RoutedEventArgs e)
		{
			refreshMaintenanceRecordList();
		}

		private void refreshMaintenanceRecordList()
		{
            try
            {
                _maintenanceRecordDetailList = _maintenanceRecordManager.RetrieveMaintenanceRecordDetailList();
                dgMaintenanceRecord.ItemsSource = _maintenanceRecordDetailList;
            }
            catch (Exception ex)
            {
                var message = ex.Message + "\n\n" + ex.InnerException;
                MessageBox.Show(message, "Error Retrieving Sources.", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
        }
		/// <summary>
		/// Weston Olund
		/// 2018/04/27
		/// Method to reorder supplies where quantity at or below reorder level
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnReorderSupplies_Click(object sender, RoutedEventArgs e)
		{
			List<SupplyItemDetail> filteredList = new List<SupplyItemDetail>();
			try
			{
				List<SupplyItemDetail> itemstoBeReorderd = _supplyItemManager.RetrieveItemsNeedingReorderSupplyItemDetailList();
				List<SupplyItemDetail> otherItemsToBeReordered = _supplyItemManager.RetrieveItemsNeedingReorderSupplyItemDetailListNotOnReorder();
                if(itemstoBeReorderd.Count == 0 && otherItemsToBeReordered.Count == 0)
                {
                    MessageBox.Show("No items need to be reordered");
                    return;
                }

				foreach (var item in otherItemsToBeReordered)
				{
					itemstoBeReorderd.Add(item);
				}
				

				List<string> itemNameList = new List<string>();

				// the following loops filter the list to only unique supply item names within the retrieved list
				for (int i = 0; i < itemstoBeReorderd.Count; i++)
				{
					if (!itemNameList.Contains(itemstoBeReorderd[i].SupplyItem.Name))
					{
						itemNameList.Add(itemstoBeReorderd[i].SupplyItem.Name);
					}
				}
				for (int i = 0; i < itemNameList.Count; i++)
				{
					for (int j = 0; j < itemstoBeReorderd.Count; j++)
					{
						if (itemNameList[i] == itemstoBeReorderd[j].SupplyItem.Name)
						{
							filteredList.Add(itemstoBeReorderd[j]);
							break;
						}
					}
				}
				List<int> uniqueVendorIDList = new List<int>();
				for (int i = 0; i < filteredList.Count; i++)
				{
					if (!uniqueVendorIDList.Contains(filteredList[i].VendorID))
					{
						uniqueVendorIDList.Add(filteredList[i].VendorID);
					}
				}

                var reorderedList = filteredList.OrderByDescending(s => s.VendorID);
                string itemMessage = "";
                foreach (var item in reorderedList)
			{
				itemMessage += "\n" + item.VendorName + ": " + item.SupplyItem.Name + "\t(" + item.SupplyItem.ReorderQuantity + ")";
			}
            var result = MessageBox.Show("Would you like to reorder the following supplies?\n " + itemMessage,"Resupply Items", MessageBoxButton.OKCancel);

            if (result == MessageBoxResult.Cancel)
            {
                return;
            }
				// create Resupply Order
				// may need to change supplyStatusID based on business rules
				foreach (var vendorID in uniqueVendorIDList)
				{
					var resupplyOrder = new ResupplyOrder()
					{
						EmployeeID = _user.Employee.EmployeeID,
						Date = DateTime.Now,
						SupplyStatusID = "Ordered",
						VendorID = vendorID
					};
					int newResupplyOrderID = 0;
					try
					{
						newResupplyOrderID = _resupplyOrderManager.CreateResupplyOrder(resupplyOrder);
					}
					catch (Exception)
					{
						MessageBox.Show("There was an error creating the resupply order");
					}
					// resupply order lines
					foreach (var item in filteredList)
					{
						if (item.VendorID == vendorID)
						{
							ResupplyOrderLine resupplyOrderLine = new ResupplyOrderLine()
							{
								SupplyItemID = item.SupplyItem.SupplyItemID,
								Quantity = item.SupplyItem.ReorderQuantity,
								Price = item.PriceEach * item.SupplyItem.ReorderQuantity,
								ResupplyOrderID = newResupplyOrderID
							};
							try
							{
								_resupplyOrderLineManager.CreateResupplyOrderLine(resupplyOrderLine);
							}
							catch (Exception)
							{
								MessageBox.Show("There was an error creating the resupply order line");
							}
						}
					}
				}
			}
			catch (Exception)
			{
				MessageBox.Show("There was an error reordering the needed supplies");
			}
			// displays a message to the user of what is being reordered
			string itemList = "";

            var orderedList = filteredList.OrderByDescending(s => s.VendorID);
            foreach (var item in orderedList)
			{
                itemList += "\n" + item.VendorName + ": " + item.SupplyItem.Name + "\t(" + item.SupplyItem.ReorderQuantity + ")";
			}
             MessageBox.Show("The following items have been reordered: \n" + itemList);
		}
	

        private void btnEditEquipment_Click(object sender, RoutedEventArgs e)
        {
            if (dgEquipment.SelectedItem == null)
            {
                MessageBox.Show("You must select an equipment item to edit");
            }
            else
            {
                EquipmentDetail selectedEquipmentDetail = (EquipmentDetail)dgEquipment.SelectedItem;
                frmAddEditEquipment frmAddEditEquipment = new frmAddEditEquipment(DetailFormMode.Edit, selectedEquipmentDetail);
                frmAddEditEquipment.ShowDialog();
                refreshEquipmentList();
            }
        }


        /// <summary>
        /// Created by Badis Saidani
        /// 5/3/2018
        /// 
        /// Refreshes the Prep Record  Detail list
        /// </summary>
        private void refreshPrepRecordDetailList()
        {
            dgPrepRecord.ItemsSource = null;
            dgPrepRecord.Items.Clear();
            try
            {
                _prepRecordDetailList = _prepRecordManager.RetrievePrepRecordDetailList();
                dgPrepRecord.ItemsSource = _prepRecordDetailList;
            }
            catch (Exception ex)
            {
                var message = ex.Message + "\n\n" + ex.InnerException.Message;
                MessageBox.Show(message, "Data Retrieval Error.",
                    MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
        }

        /// <summary>
        /// Created by Badis Saidani
        /// 5/3/2018
        /// 
        /// Refreshes the Prep Record  Detail list
        /// </summary>
        private void TabItem_PrepRecord_Selected(object sender, RoutedEventArgs e)
        {
            refreshPrepRecordDetailList();
        }

        /// <summary>
        /// Created by Badis Saidani
        /// 5/4/2018
        /// 
        /// Adds Prep Record  Detail list
        /// </summary>
        private void btnAddPrepRecord_Click(object sender, RoutedEventArgs e)
        {
            var detailForm = new frmAddEditPrepRecord(_prepRecordManager);
            var result = detailForm.ShowDialog();
            if (result == true)
            {
                refreshPrepRecordDetailList();
            }
        }

        /// <summary>
        /// Created by Badis Saidani
        /// 5/4/2018
        /// 
        /// Edits Prep Record  Detail list
        /// </summary>
        private void btnEditPrepRecord_Click(object sender, RoutedEventArgs e)
        {
            if (this.dgPrepRecord.SelectedItems.Count > 0)
            {
                var editForm = new frmAddEditPrepRecord(_prepRecordManager, (PrepRecordDetail)this.dgPrepRecord.SelectedItem);
                var result = editForm.ShowDialog();
                if (result == true)
                {
                    refreshPrepRecordDetailList();
                }
            }
            else
            {
                MessageBox.Show("You must select something!");
            }
        }

        /// <summary>
        /// Created by Badis Saidani
        /// 5/4/2018
        /// 
        /// Deletes Prep Record  Detail list
        /// </summary>
        private void btnDeletePrepRecord_Click(object sender, RoutedEventArgs e)
        {
            if (this.dgPrepRecord.SelectedItems.Count > 0)
            {
                try
                {
                    this._prepRecordManager.DeletePrepRecordByID(((PrepRecordDetail)this.dgPrepRecord.SelectedItem).PrepRecordID);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Failed to delete prep record." + ex.InnerException);
                }

                refreshPrepRecordDetailList();
            }
            else
            {
                MessageBox.Show("You must select something!");
            }
        }

        /// <summary>
        /// Jacob Slaubaugh
        /// Created 2018/05/06
        /// 
        /// Deactivates a TaskTypeEquipmentNeed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDeactivateTaskTypeEquipmentNeed_Click(object sender, RoutedEventArgs e)
        {
            TaskTypeEquipmentNeedDetailList selected = (TaskTypeEquipmentNeedDetailList)dgTaskTypeEquipmentNeed.SelectedItem;
            if (selected != null)
            {
                try
                {
                    var result = MessageBox.Show("Are you sure you want to deactivate this need?", "Delete TaskTypeEquipmentNeed", MessageBoxButton.OKCancel, MessageBoxImage.Warning);
                    if (result == MessageBoxResult.OK)
                    {
                        _taskTypeEquipmentNeedManager.DeleteTaskTypeEquipmentNeedItem(selected.TaskTypeEquipmentNeed.TaskTypeEquipmentNeedID);
                        MessageBox.Show("The need has been deleted.");
                        refreshTaskTypeEquipmentNeedList();
                    }
                }
                catch (Exception)
                {

                    MessageBox.Show("Failed to delete.");
                }
            }
            else
            {
                MessageBox.Show("You must select something to delete.");
            }
        }

        /// <summary>
        /// Jacob Slaubaugh
        /// Created 2018/05/09
        /// 
        /// Deletes a MaintenanceRecord
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDeleteMaintenanceRecord_Click(object sender, RoutedEventArgs e)
        {
            MaintenanceRecordDetail selected = (MaintenanceRecordDetail)dgMaintenanceRecord.SelectedItem;
            if (selected != null)
            {
                try
                {
                    var result = MessageBox.Show("Are you sure you want to deactivate this need?", "Delete MaintanenceRecord", MessageBoxButton.OKCancel, MessageBoxImage.Warning);
                    if (result == MessageBoxResult.OK)
                    {
                        _maintenanceRecordManager.DeleteMaintenanceRecord(((MaintenanceRecordDetail)this.dgMaintenanceRecord.SelectedItem).MaintenanceRecord.MaintenanceRecordID);
                        MessageBox.Show("The need has been deleted.");
                        refreshMaintenanceRecordList();
                    }
                }
                catch (Exception)
                {
                    MessageBox.Show("Failed to delete.");
                }
            }
            else
            {
                MessageBox.Show("You must select something to delete.");
            }
        }

        /// <summary>
        /// Jacob Slaubaugh
        /// Created 2018/05/09
        /// 
        /// Creates a new MaintenanceRecord
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAddMaintenanceRecord_Click(object sender, RoutedEventArgs e)
        {
            var frmAddEditMaintenanceRecord = new frmAddEditMaintenanceRecord(_maintenanceRecordManager, _employeeManager, _equipmentManager);
            var result = frmAddEditMaintenanceRecord.ShowDialog();

            if (result == true)
            {
                refreshMaintenanceRecordList();
            }
        }

        /// <summary>
        /// Jacob Slaubaugh
        /// Created 2018/05/09
        /// 
        /// Edits a MaintenanceRecord
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnEditMaintenanceRecord_Click(object sender, RoutedEventArgs e)
        {
            MaintenanceRecordDetail maintenanceRecordDetail = null;
            if (dgMaintenanceRecord.SelectedItems.Count > 0)
            {
                try
                {
                    maintenanceRecordDetail = (MaintenanceRecordDetail)dgMaintenanceRecord.SelectedItem;
                    var maintenanceRecordForm = new frmAddEditMaintenanceRecord(_maintenanceRecordManager, _employeeManager, _equipmentManager, maintenanceRecordDetail, DetailFormMode.Edit);
                    var result = maintenanceRecordForm.ShowDialog();
                    if (result == true)
                    {
                        refreshMaintenanceRecordList();
                    }
                }
                catch (Exception ex)
                {

                    MessageBox.Show(ex.Message + "\n\n" + ex.InnerException.Message);
                }
            }
            else
            {
                MessageBox.Show("You must select something to edit.");
            }

        }
    }
}