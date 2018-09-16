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
    /// Interaction logic for frmAddEditTaskTypeEmployeeNeed.xaml
    /// </summary>
    public partial class frmAddEditTaskTypeEmployeeNeed : Window
    {
        private ITaskTypeEmployeeNeedManager _taskTypeEmployeeNeedManager;

        private ITaskTypeManager _taskTypeManager = new TaskTypeManager();
        private List<TaskType> _taskTypeList = new List<TaskType>();

        private TaskTypeEmployeeNeedDetail _taskTypeEmployeeNeedDetail;

        public frmAddEditTaskTypeEmployeeNeed()
        {
            InitializeComponent();
        }

        public frmAddEditTaskTypeEmployeeNeed(ITaskTypeEmployeeNeedManager taskTypeEmployeeNeedManager)
        {
            InitializeComponent();
            _taskTypeEmployeeNeedManager = taskTypeEmployeeNeedManager;
            populateAdd();
        }

        public frmAddEditTaskTypeEmployeeNeed(ITaskTypeEmployeeNeedManager taskTypeEmployeeNeedManager, TaskTypeEmployeeNeedDetail selectedItem)
        {
            InitializeComponent();
            _taskTypeEmployeeNeedManager = taskTypeEmployeeNeedManager;
            this._taskTypeEmployeeNeedDetail = selectedItem;
            populateEdit();
        }

        private bool validateFields()
        {
            bool isValid = true;

            if (cboTaskTypes.SelectedItem == null)
            {
                MessageBox.Show("You must select a task type!", "Invalid Input", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                return false;
            }

            if (numHoursOfWork.Value == null)
            {
                MessageBox.Show("You must set the hours of work!", "Invalid Input", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                return false;
            }


            return isValid;
        }

        /// <summary>
        /// Zachary Hall
        /// Created 2018/03/29
        /// 
        /// Add/edit click event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAddEdit_Click(object sender, RoutedEventArgs e)
        {
            if (_taskTypeEmployeeNeedDetail == null)//add
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
        /// Created 2018/03/29
        /// 
        /// Populates the TaskType dropdown
        /// </summary>
        private void populateTaskTypeDropdown()
        {
            try
            {
                _taskTypeList = _taskTypeManager.RetrieveTaskTypeList();
                cboTaskTypes.ItemsSource = _taskTypeList;
                cboTaskTypes.DisplayMemberPath = "Name";

            }
            catch (Exception ex)
            {

                var message = ex.Message + "\n\n" + ex.InnerException;
                MessageBox.Show(message, "TaskType List Retrieval Error!", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
        }

        /// <summary>
        /// Zachary Hall
        /// Created 2018/03/29
        /// 
        /// populates the fields for the edit
        /// </summary>
        private void populateEdit()
        {
            populateTaskTypeDropdown();
            foreach (TaskType t in cboTaskTypes.Items)
            {
                if (t.TaskTypeID == _taskTypeEmployeeNeedDetail.TaskTypeEmployeeNeed.TaskTypeID)
                {
                    this.cboTaskTypes.SelectedIndex = cboTaskTypes.Items.IndexOf(t);
                    break;
                }
            }
            cboTaskTypes.IsEnabled = false;

            numHoursOfWork.Value = _taskTypeEmployeeNeedDetail.TaskTypeEmployeeNeed.HoursOfWork;

            chkActive.IsChecked = _taskTypeEmployeeNeedDetail.TaskTypeEmployeeNeed.Active;

            lblHeader.Content = "Editing Task Type Employee record";
            btnAddEdit.Content = "Save";
        }


        /// <summary>
        /// Zachary Hall
        /// Created 2018/03/29
        /// 
        /// Populates the fields for adding
        /// </summary>
        private void populateAdd()
        {
            populateTaskTypeDropdown();
        }

        /// <summary>
        /// Zachary Hall
        /// Created 2018/03/29
        /// 
        /// Performs the edit functionality
        /// </summary>
        private void performEdit()
        {
            if (validateFields())
            {
                try
                {
                    var newNeed = new TaskTypeEmployeeNeed
                    {
                        TaskTypeID = ((TaskType)(cboTaskTypes.SelectedItem)).TaskTypeID,
                        HoursOfWork = (int)numHoursOfWork.Value,
                        Active = (chkActive.IsChecked == true)

                    };
                    var updateNeedResults = _taskTypeEmployeeNeedManager.UpdateTaskTypeEmployeeNeed(_taskTypeEmployeeNeedDetail.TaskTypeEmployeeNeed, newNeed);

                    this.DialogResult = true;
                    this.Close();
                }
                catch (Exception ex)
                {

                    var message = ex.Message + "\n\n" + ex.InnerException;
                    MessageBox.Show(message, "Update Error!", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                }

            }
        }

        /// <summary>
        /// Zachary Hall
        /// Created 2018/03/29
        /// 
        /// Performs the add functionality
        /// </summary>
        private void performAdd()
        {
            if (validateFields())
            {
                try
                {
                    var newNeed = new TaskTypeEmployeeNeed
                    {
                        TaskTypeID = ((TaskType)(cboTaskTypes.SelectedItem)).TaskTypeID,
                        HoursOfWork = (int)numHoursOfWork.Value,
                        Active = (chkActive.IsChecked == true)

                    };
                    var createEmployeeNeed = _taskTypeEmployeeNeedManager.CreateTaskTypeEmployeeNeed(newNeed);

                    this.DialogResult = true;
                    this.Close();
                }
                catch (Exception ex)
                {
                    var message = ex.Message + "\n\n" + ex.InnerException;
                    MessageBox.Show(message, "Creation Error!", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                }

            }
        }

        /// <summary>
        /// Zachary Hall
        /// Created 2018/03/29
        /// 
        /// Cancel button click event
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
