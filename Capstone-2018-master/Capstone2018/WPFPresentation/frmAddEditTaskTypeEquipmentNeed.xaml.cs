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
    /// Interaction logic for frmAddEditTaskTypeEquipmentNeed.xaml
    /// </summary>
    public partial class frmAddEditTaskTypeEquipmentNeed : Window
    {
        private DetailFormMode _mode;
        private List<TaskType> _taskTypeList;
        private List<EquipmentType> _equipmentTypeList;
        private TaskTypeEquipmentNeed _taskTypeEquipmentNeed;
        private ITaskTypeEquipmentNeedManager _taskTypeEquipmentNeedManager;

        public frmAddEditTaskTypeEquipmentNeed()
        {
            InitializeComponent();
        }

        // Constructor for add mode
        public frmAddEditTaskTypeEquipmentNeed(ITaskTypeEquipmentNeedManager taskTypeEquipmentNeedManager, List<TaskType> taskTypeList, List<EquipmentType> equipmentTypeList)
        {
            _taskTypeEquipmentNeedManager = taskTypeEquipmentNeedManager;
            _taskTypeList = taskTypeList;
            _equipmentTypeList = equipmentTypeList;
            _mode = DetailFormMode.Add;
            InitializeComponent();
        }

        // Constructor for edit mode
        public frmAddEditTaskTypeEquipmentNeed(ITaskTypeEquipmentNeedManager taskTypeEquipmentNeedManager, TaskTypeEquipmentNeed taskTypeEquipmentNeed, List<TaskType> taskTypeList, List<EquipmentType> equipmentTypeList, DetailFormMode mode)
        {
            _taskTypeEquipmentNeedManager = taskTypeEquipmentNeedManager;
            _taskTypeList = taskTypeList;
            _equipmentTypeList = equipmentTypeList;
            _mode = mode;
            _taskTypeEquipmentNeed = taskTypeEquipmentNeed;
            InitializeComponent();
        }

        /// <summary>
        /// Brady Feller
        /// Created 2018/03/28
        /// 
        /// Sets up the 'Add' mode for the window
        /// </summary>
        private void setupAddMode()
        {
            cboTaskType.ItemsSource = _taskTypeList;
            cboEquipmentType.ItemsSource = _equipmentTypeList;
            this.cboTaskType.DisplayMemberPath = "Name";
            this.cboEquipmentType.DisplayMemberPath = "EquipmentTypeID";
        }

        /// <summary>
        /// Brady Feller
        /// Created 2018/03/28
        /// 
        /// Sets up the 'Edit' mode for the window
        /// </summary>
        private void setupEditMode()
        {
            lblHeader.Content = "Editing an existing Task Type Equipment Need";
            btnAddEdit.Content = "Edit";
            cboTaskType.ItemsSource = _taskTypeList;
            cboEquipmentType.ItemsSource = _equipmentTypeList;
            cboTaskType.DisplayMemberPath = "Name";
            //cboEquipmentType.DisplayMemberPath = "EquipmentTypeID";
            cboTaskType.SelectedItem = _taskTypeList.Find(t => t.TaskTypeID == _taskTypeEquipmentNeed.TaskTypeID);
            cboEquipmentType.SelectedItem = _equipmentTypeList.Find(e => e.EquipmentTypeID == _taskTypeEquipmentNeed.EquipmentTypeID);
            cboTaskType.IsEnabled = false;
            cboEquipmentType.IsEnabled = false;
            hoursWorked.Value = _taskTypeEquipmentNeed.HoursOfWork;
        }

        /// <summary>
        /// Brady Feller
        /// Created 2018/03/28
        /// 
        /// Adds or Edits a record depending on the users choice
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            switch (_mode)
            {
                case DetailFormMode.Add:
                    setupAddMode();
                    break;
                case DetailFormMode.Edit:
                    setupEditMode();
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// Brady Feller
        /// Created 2018/03/28
        /// 
        /// Method to add a task type supply need item
        /// </summary>
        private void addTaskTypeEquipmentNeed()
        {
            if (cboTaskType.SelectedItem == null)
            {
                MessageBox.Show("Please select a task.");
            }
            if (cboEquipmentType.SelectedItem == null)
            {
                MessageBox.Show("Please select an equipment type for the task.");
            }
            else
            {
                try
                {
                    TaskType task = (TaskType)cboTaskType.SelectedItem;
                    EquipmentType eType = (EquipmentType)cboEquipmentType.SelectedItem;
                    TaskTypeEquipmentNeed taskTypeEquipmentNeed = new TaskTypeEquipmentNeed()
                    {
                        TaskTypeID = task.TaskTypeID,
                        EquipmentTypeID = eType.EquipmentTypeID,
                        HoursOfWork = (int)hoursWorked.Value
                    };
                    _taskTypeEquipmentNeedManager.CreateTaskTypeEquipmentNeed(taskTypeEquipmentNeed);
                    this.DialogResult = true;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("There was an error adding the equipment order.", ex.Message);
                    this.DialogResult = false;
                }
            }
        }

        /// <summary>
        /// Brady Feller
        /// Created on 2018/03/28
        /// 
        /// Method to edit a task type supply need item
        /// </summary>
        private void editTaskTypeEquipmentNeed()
        {
            if (_taskTypeEquipmentNeed.HoursOfWork == hoursWorked.Value)
            {
                this.DialogResult = false;
            }
            else
            {
                try
                {
                    TaskTypeEquipmentNeed newTaskTypeEquipmentNeed = new TaskTypeEquipmentNeed()
                    {
                        TaskTypeID = _taskTypeEquipmentNeed.TaskTypeID,
                        EquipmentTypeID = _taskTypeEquipmentNeed.EquipmentTypeID,
                        HoursOfWork = (int)hoursWorked.Value
                    };
                    _taskTypeEquipmentNeedManager.EditTaskTypeEquipmentNeed(_taskTypeEquipmentNeed, newTaskTypeEquipmentNeed);
                    this.DialogResult = true;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("There was an error editing the equipment order.", ex.Message);
                    this.DialogResult = false;
                }
            }
        }

        /// <summary>
        /// Brady Feller
        /// Created 2018/03/28
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAddEdit_Click(object sender, RoutedEventArgs e)
        {
            if (_mode == DetailFormMode.Add)
            {
                addTaskTypeEquipmentNeed();
            }
            if (_mode == DetailFormMode.Edit)
            {
                editTaskTypeEquipmentNeed();
            }
        }

        /// <summary>
        /// Brady Feller
        /// Created 2018/03/28
        /// 
        /// Cancels out of the form when cliicked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }
    }
}
