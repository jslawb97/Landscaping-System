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
using Logic;
using DataObjects;

namespace WPFPresentation
{
    /// <summary>
    /// John Miller
    /// Created 2018/03/05
    /// Interaction logic for frmAddEditTask.xaml
    /// </summary>
    public partial class frmAddEditTask : Window
    {
        private ITaskManager _taskManager;
        private IServiceItemManager _serviceItemManager = new ServiceItemManager();
        private ITaskTypeManager _taskTypeManager = new TaskTypeManager();
        private DataObjects.Task _task;

        private TaskDetail _taskDetail;

        public frmAddEditTask(ITaskManager taskManager)
        {
            _taskManager = taskManager;
            InitializeComponent();
            setUpAddForm();
        }

        public frmAddEditTask(ITaskManager taskManager, DataObjects.Task task)
        {
            _taskManager = taskManager;
            _task = task;
            InitializeComponent();
            setupEditForm();

        }

        /// <summary>
        /// John Miller
        /// Created 2018/03/05
        /// 
        /// Populates fields to reflect the Edit functionality
        /// </summary>
        private void setupEditForm()
        {
            lblHeader.Content = "Editing Task " + _task.Name;
            btnAddEdit.Content = "Save";
            txtName.Text = _task.Name;
            txtDescription.Text = _task.Description;
            setEditComboBoxes();
            chkActive.IsChecked = _task.Active;

        }

        /// <summary>
        /// John Miller
        /// Created 2018/03/05
        /// 
        /// Populates fields to reflect the Add functionality
        /// </summary>
        private void setUpAddForm()
        {
            lblHeader.Content = "Adding New Task";
            btnAddEdit.Content = "Add";
            chkActive.IsChecked = true;
            chkActive.IsEnabled = false;
            setAddComboBoxes();

        }

        private void performEdit()
        {
            List<ServiceItem> serviceItemList = _serviceItemManager.RetrieveServiceItemList();
            ServiceItem selectedServiceItem = new ServiceItem();

            List<TaskType> taskTypeList = _taskTypeManager.RetrieveTaskTypeList();
            TaskType selectedTaskType = new TaskType();

            foreach (var serviceItem in serviceItemList)
            {
                if (serviceItem.Name.Equals(cboServiceTypeID.Text))
                {
                    selectedServiceItem = serviceItem;
                }
            }

            foreach (var taskType in taskTypeList)
            {
                if (taskType.Name.Equals(cboTaskTypeID.Text))
                {
                    selectedTaskType = taskType;
                }
            }

            if (validateFields())
            {
                var newTask = new DataObjects.Task()
                {
                    Name = txtName.Text,
                    Description = txtDescription.Text,
                    ServiceItemID = selectedServiceItem.ServiceItemID,
                    TaskTypeID = selectedTaskType.TaskTypeID,
                    Active = (bool)chkActive.IsChecked
                };

                try
                {
                    var result = _taskManager.EditTask(_task, newTask);
                    MessageBox.Show(_task.Name + " Was successfully edited!");
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
                    // display error
                    MessageBox.Show(message, "Edit failed", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                }
            }
        }

        private void performAdd()
        {
            if (validateFields())
            {
                List<ServiceItem> serviceItemList = _serviceItemManager.RetrieveServiceItemList();
                ServiceItem selectedServiceItem = new ServiceItem();

                List<TaskType> taskTypeList = _taskTypeManager.RetrieveTaskTypeList();
                TaskType selectedTaskType = new TaskType();

                foreach (var serviceItem in serviceItemList)
                {
                    if (serviceItem.Name.Equals(cboServiceTypeID.Text))
                    {
                        selectedServiceItem = serviceItem;
                    }
                }

                foreach (var taskType in taskTypeList)
                {
                    if (taskType.Name.Equals(cboTaskTypeID.Text))
                    {
                        selectedTaskType = taskType;
                    }
                }
                var newTask = new DataObjects.Task()
                {
                    Name = txtName.Text,
                    Description = txtDescription.Text,
                    ServiceItemID = selectedServiceItem.ServiceItemID,
                    TaskTypeID = selectedTaskType.TaskTypeID
                };


                try
                {
                    var result = _taskManager.CreateTask(newTask);
                    MessageBox.Show(newTask.Name + " was successfully added!");
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
                    // display the error
                    MessageBox.Show(message, "Add Failed", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                }
            }
        }

        private void setAddComboBoxes()
        {
            List<TaskType> taskTypeList = new List<TaskType>();
            List<ServiceItem> serviceItemList = new List<ServiceItem>();

            try
            {
                taskTypeList = _taskTypeManager.RetrieveTaskTypeList().OrderBy(n => n.Name).ToList();
                this.cboTaskTypeID.ItemsSource = taskTypeList;
                this.cboTaskTypeID.DisplayMemberPath = "Name";
                this.cboTaskTypeID.SelectedIndex = 0;

                serviceItemList = _serviceItemManager.RetrieveServiceItemList().OrderBy(n => n.Name).ToList();
                this.cboServiceTypeID.ItemsSource = serviceItemList;
                this.cboServiceTypeID.DisplayMemberPath = "Name";
                this.cboServiceTypeID.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show("There was an issue retrieving lists of ServiceItem and TaskType names." + ex.Message);
            }
        }

        private void setEditComboBoxes()
        {
            List<TaskType> taskTypeList = new List<TaskType>();
            List<ServiceItem> serviceItemList = new List<ServiceItem>();

            try
            {
                taskTypeList = _taskTypeManager.RetrieveTaskTypeList().OrderBy(n => n.Name).ToList();
                this.cboTaskTypeID.ItemsSource = taskTypeList;
                this.cboTaskTypeID.DisplayMemberPath = "Name";
                this.cboTaskTypeID.SelectedItem = taskTypeList.Find(n => n.TaskTypeID == _task.TaskTypeID);

                serviceItemList = _serviceItemManager.RetrieveServiceItemList().OrderBy(n => n.Name).ToList();
                this.cboServiceTypeID.ItemsSource = serviceItemList;
                this.cboServiceTypeID.DisplayMemberPath = "Name";
                this.cboServiceTypeID.SelectedItem = serviceItemList.Find(n => n.ServiceItemID == _task.ServiceItemID);

            }
            catch (Exception ex)
            {
                MessageBox.Show("There was an issue retrieving lists of ServiceItem and TaskType names." + ex.Message);
            }


        }

        private bool validateFields()
        {
            if (!StringValidations.IsValidNamePropertyEmpty(txtName.Text))
            {
                MessageBox.Show("You must provide a name.");
                return false;
            }

            if (!StringValidations.IsValidNamePropertyMaxSize(txtName.Text, 100))
            {
                MessageBox.Show("Name cannot be over 100 characters in length.");
                return false;
            }

            if (!StringValidations.IsValidNamePropertyEmpty(txtDescription.Text))
            {
                MessageBox.Show("Description field cannot be empty.");
                return false;
            }

            if (!StringValidations.IsValidNamePropertyMaxSize(txtDescription.Text, 1000))
            {
                MessageBox.Show("Description cannot be over 1000 characters in length.");
                return false;
            }
            return true;
        }

        private void btnAddEdit_Click(object sender, RoutedEventArgs e)
        {
            if (_task == null)
            {
                performAdd();
            }
            else
            {
                performEdit();
            }
        }

        /// <summary>
        /// John Miller
        /// Created 2018/03/05
        /// 
        /// Form Cancel Event called by user clicking Add button.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
