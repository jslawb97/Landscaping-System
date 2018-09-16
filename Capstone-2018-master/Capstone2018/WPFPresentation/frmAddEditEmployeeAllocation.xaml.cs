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
    /// Interaction logic for frmAddEditEmployeeAllocation.xaml
    /// </summary>
    public partial class frmAddEditEmployeeAllocation : Window
    {
        public frmAddEditEmployeeAllocation()
        {
            InitializeComponent();
        }

        private TaskEmployeeDetail _taskEmployeeDetail;
        private ITaskEmployeeManager _taskEmployeeManager;
        private EmployeeManager _employeeManager = new EmployeeManager();
        private JobDetail _jobDetail;
        private Certification _certification;
        private List<Employee> _availableEmployees = new List<Employee>();
        private List<Employee> _assignedEmployees = new List<Employee>();


        public frmAddEditEmployeeAllocation(TaskEmployeeDetail taskEmployeeDetail, JobDetail jobDetail, ITaskEmployeeManager taskEmployeeManager, Certification certification)
        {
            _certification = certification;
            InitializeComponent();
            _taskEmployeeDetail = taskEmployeeDetail;
            _jobDetail = jobDetail;
            _taskEmployeeManager = taskEmployeeManager;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //populateControls();
            //refreshAvailableEmployees();
            //refreshAssignedEmployees();
        }

        ///// <summary>
        ///// Sam Dramstad
        ///// Created on 2018/04/05
        ///// 
        ///// Methods to manage adding employees to tasks.
        ///// </summary>

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnAddToAssigned_Click(object sender, RoutedEventArgs e)
        {
            //if (dgAvailableEmployees.SelectedItems.Count == 0)
            //{
            //    MessageBox.Show("You need to select an employee from the available employees list.");
            //    return;
            //}

            //var selectedItem = (Employee)this.dgAvailableEmployees.SelectedItem;

            //_taskEmployeeManager.CreateEmployeeTaskAssignment(selectedItem.EmployeeID, _jobDetail.Job.JobID, _taskEmployeeDetail.TaskTypeEmployeeNeedID);

            //_assignedEmployees.Add(selectedItem);
            //_availableEmployees.Remove(selectedItem);

            ////_iEmployeeTaskManager.CreateEmployeeTaskAssignment();
            ////update tables
            //refreshAvailableEmployees();
            //refreshAssignedEmployees();
        }

        private void btnRemoveFromAssigned_Click(object sender, RoutedEventArgs e)
        {
            //if (dgAssignedEmployees.SelectedItems.Count == 0)
            //{
            //    MessageBox.Show("You need to select an employee from the assigned employees list.");
            //    return;
            //}

            //var selectedItem = (Employee)this.dgAssignedEmployees.SelectedItem;


            //_taskEmployeeManager.DeleteEmployeeTaskAssignment(selectedItem.EmployeeID, _jobDetail.Job.JobID);

            //_availableEmployees.Add(selectedItem);
            //_assignedEmployees.Remove(selectedItem);

            ////update tables
            //refreshAvailableEmployees();
            //refreshAssignedEmployees();
        }

        //private void refreshAvailableEmployees()
        //{
        //    dgAvailableEmployees.ItemsSource = null;
        //    dgAvailableEmployees.ItemsSource = _availableEmployees;
        //}

        //private void refreshAssignedEmployees()
        //{
        //    try
        //    {
        //        dgAssignedEmployees.ItemsSource = null;
        //        _assignedEmployees = _taskEmployeeManager.RetrieveEmployeeListByTaskTypeEmployeeNeedID(_taskEmployeeDetail.TaskTypeEmployeeNeedID, _jobDetail.Job.JobID);
        //        dgAssignedEmployees.ItemsSource = _assignedEmployees;
        //    }
        //    catch (Exception ex)
        //    {
        //        var message = ex.Message + "\n\n" + ex.InnerException;
        //        MessageBox.Show(message, "Error Retrieving Assigned Employees!", MessageBoxButton.OK, MessageBoxImage.Exclamation);
        //    }
        //}

        //public void populateControls()
        //{
        //    // get availble Employee list
        //    try
        //    {
        //        //Change second _job.DateScheduled to the calculat
        //        _availableEmployees = _employeeManager.RetrieveEmployeeListByCertificationAndAvailability(_certification, _jobDetail.Job.DateScheduled, _jobDetail.Job.DateScheduled);
        //    }
        //    catch (Exception ex)
        //    {
        //        var message = ex.Message;
        //        if (ex.InnerException != null)
        //        {
        //            message += "\n\n" + ex.InnerException.Message;
        //        }
        //        MessageBox.Show(message, "Data Retrieval Fail", MessageBoxButton.OK, MessageBoxImage.Exclamation);
        //    }
        //}
    }
}
