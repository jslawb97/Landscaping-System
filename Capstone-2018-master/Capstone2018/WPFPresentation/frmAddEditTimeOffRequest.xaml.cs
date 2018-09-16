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
    /// Interaction logic for frmAddEditTimeOffRequest.xaml
    /// </summary>
    public partial class frmAddEditTimeOffRequest : Window
    {

        private ITimeOffRequestManager _timeOffRequestManager = new TimeOffRequestManager();
        private IEmployeeManager _employeeManager = new EmployeeManager();
        private DetailFormMode _mode;

        List<int> _employeeIDList = new List<int>();
        List<string> _employeeNameList = new List<string>();
        TimeOffRequest _timeOffRequest = null;

        public frmAddEditTimeOffRequest()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Weston Olund
        /// Created on 2018/02/22
        /// 
        /// Constructor for add mode
        /// </summary>
        /// <param name="timeOffRequestManager"></param>
        public frmAddEditTimeOffRequest(ITimeOffRequestManager timeOffRequestManager)
        {

            _timeOffRequestManager = timeOffRequestManager;
            _mode = DetailFormMode.Add;
            InitializeComponent();
        }


        /// <summary>
        /// Jacob Conley
        /// Created on 2018/03/01
        /// 
        /// Constructor for edit mode
        /// </summary>
        /// <param name="timeOffRequestManager"></param>
        /// <param name="TimeOffRequest"></param>
        public frmAddEditTimeOffRequest(ITimeOffRequestManager timeOffRequestManager, TimeOffRequestDetail timeOffRequest)
        {
            _timeOffRequestManager = timeOffRequestManager;
            _timeOffRequest = timeOffRequest.TimeOffRequest;
            _mode = DetailFormMode.Edit;
            InitializeComponent();
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }

        /// <summary>
        /// Weston Olund
        /// Created on 2018/02/22
        /// 
        /// Method that adds a new time off request when save is called
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <remarks>
        /// Jacob Conley
        /// Updated on 2018/03/10
        /// 
        /// If called in edit mode, an existing time off will be updated when save is called
        /// </remarks>
        private void btnSave_Click(object sender, RoutedEventArgs e)
        {

            if (_mode == DetailFormMode.Add)
            {
                addTimeOffRequest();
            }
            if (_mode == DetailFormMode.Edit)
            {
                editTimeOffRequest();
            }

        }

        /// <summary>
        /// Jacob Conley
        /// Created on 2018/03/10
        /// 
        /// Method to handle the editing of a time off request
        /// </summary>
        private void editTimeOffRequest()
        {
            if (this.cboEmployeeID.SelectedItem == null)
            {
                MessageBox.Show("Choose your email address");
                return;
            }
            Employee emp = (Employee)this.cboEmployeeID.SelectedItem;
            TimeOffRequest timeOffRequest = new TimeOffRequest();

            timeOffRequest.TimeOffID = _timeOffRequest.TimeOffID;
            timeOffRequest.EmployeeID = emp.EmployeeID;
            var startDate = this.dpStartDate.SelectedDate;
            var endDate = this.dpEndDate.SelectedDate;
            timeOffRequest.StartTime = startDate;
            timeOffRequest.EndTime = endDate;
            timeOffRequest.Approved = false;
            timeOffRequest.Active = true;

            try
            {
                if (_timeOffRequestManager.EditTimeOff(_timeOffRequest, timeOffRequest) == 1)
                {
                    this.DialogResult = true;
                }
                else
                {
                    this.DialogResult = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("There was an error editing the time off request.", ex.Message);
                this.DialogResult = false;
            }

        }

        /// <summary>
        /// Weston Olund
        /// Created on 2018/03/08
        /// 
        /// Method to handle adding a time off request
        /// </summary>
        private void addTimeOffRequest()
        {
            if (this.cboEmployeeID.SelectedItem == null)
            {
                MessageBox.Show("Choose your email address");
                return;
            }
            Employee emp = (Employee)this.cboEmployeeID.SelectedItem;
            TimeOffRequest timeOffRequest = new TimeOffRequest();

            timeOffRequest.EmployeeID = emp.EmployeeID;
            var startDate = this.dpStartDate.SelectedDate;
            var endDate = this.dpEndDate.SelectedDate;
            timeOffRequest.StartTime = startDate;
            timeOffRequest.EndTime = endDate;
            if (chkActive.IsChecked == true)
            {
                timeOffRequest.Active = true;
            }
            try
            {
                if (_timeOffRequestManager.CreateTimeOffRequest(timeOffRequest))
                {
                    this.DialogResult = true;
                }
                else
                {
                    this.DialogResult = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("There was an error adding the time off request.", ex.Message);
                this.DialogResult = false;
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            this.Title = "Time Off Request";
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
            constrainDatePickers();
            populateEmployeeIDComboBox();
            cboEmployeeID.SelectedItem = _timeOffRequest.EmployeeID;
            if (_timeOffRequest.Active)
            {
                this.chkActive.IsChecked = true;
            }
        }

        /// <summary>
        /// Weston Olund
        /// Created on 2018/22/02
        /// 
        /// Method to populate the employee id combo box with employee ids
        /// </summary>
        private void populateEmployeeIDComboBox()
        {
            List<Employee> employeeList = new List<Employee>();
            try
            {
                employeeList = _employeeManager.RetrieveEmployeeListByActive().OrderBy(e => e.Email).ToList();
                this.cboEmployeeID.ItemsSource = employeeList;
                this.cboEmployeeID.DisplayMemberPath = "Email";
                if (_mode == DetailFormMode.Edit)
                {
                    this.cboEmployeeID.SelectedItem = employeeList.Find(e => e.EmployeeID == _timeOffRequest.EmployeeID);
                    this.cboEmployeeID.IsEnabled = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("There was an issue retrieving a list of active employees." + ex);
            }

        }

        /// <summary>
        /// Weston Olund
        /// Created on 2018/22/02
        /// 
        /// Constrains datepickers
        /// </summary>
        private void constrainDatePickers()
        {
            if (_mode == DetailFormMode.Add)
            {
                dpStartDate.SelectedDate = DateTime.Today;
                dpEndDate.SelectedDate = DateTime.Today;
                dpStartDate.DisplayDateStart = DateTime.Today;
                dpEndDate.DisplayDateStart = DateTime.Today;
            }
            if (_mode == DetailFormMode.Edit)
            {
                dpStartDate.SelectedDate = _timeOffRequest.StartTime;
                dpEndDate.SelectedDate = _timeOffRequest.EndTime;
                dpStartDate.DisplayDateStart = _timeOffRequest.StartTime;
                dpEndDate.DisplayDateStart = _timeOffRequest.EndTime;
            }
        }

        /// <summary>
        /// Weston Olund
        /// Created on 2018/02/22
        /// 
        /// Method to make sure user cannot enter past dates or a start time after end time
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dpStartDate_CalendarClosed(object sender, RoutedEventArgs e)
        {
            if (dpEndDate.SelectedDate < dpStartDate.SelectedDate)
            {
                dpEndDate.SelectedDate = dpStartDate.SelectedDate;
            }
            dpEndDate.DisplayDateStart = dpStartDate.SelectedDate;
        }


        /// <summary>
        /// Weston Olund
        /// Created on 2018/03/08
        /// 
        /// Method to handle setting up add mode
        /// </summary>
        private void setUpAddMode()
        {
            constrainDatePickers();
            populateEmployeeIDComboBox();
        }

    }
}
