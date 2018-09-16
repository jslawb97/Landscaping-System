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
    /// Interaction logic for frmAddEditTaskTypeSupplyNeed.xaml
    /// </summary>
    public partial class frmAddEditTaskTypeSupplyNeed : Window
    {
        private DetailFormMode _mode;
        private List<SupplyItem> _supplyItemList;
        private List<TaskType> _taskTypeList;
        private TaskTypeSupplyNeed _taskTypeSupplyNeed;
        private ITaskTypeSupplyNeedManager _taskTypeSupplyNeedManager;

		// Constructor for add mode
        public frmAddEditTaskTypeSupplyNeed(ITaskTypeSupplyNeedManager taskTypeSupplyNeedManager, List<TaskType> taskTypeList, List<SupplyItem> supplyItemList)
        {
            _taskTypeSupplyNeedManager = taskTypeSupplyNeedManager;
            _taskTypeList = taskTypeList;
            _supplyItemList = supplyItemList;
            _mode = DetailFormMode.Add;
            InitializeComponent();
        }

		// Constructor for edit mode
        public frmAddEditTaskTypeSupplyNeed(ITaskTypeSupplyNeedManager taskTypeSupplyNeedManager, TaskTypeSupplyNeed taskTypeSupplyNeed, List<TaskType> taskTypeList, List<SupplyItem> supplyItemList, DetailFormMode mode)
        {
            _taskTypeSupplyNeedManager = taskTypeSupplyNeedManager;
            _taskTypeList = taskTypeList;
            _supplyItemList = supplyItemList;
            _mode = mode;
            _taskTypeSupplyNeed = taskTypeSupplyNeed;
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            switch (_mode)
            {
                case DetailFormMode.Edit:
                    setupEditMode();
                    break;
                case DetailFormMode.Add:
                    setupAddMode();
                    break;
                default:
                    break;
            }
        }

        private void setupAddMode()
        {
            cboSupply.ItemsSource = _supplyItemList;
            cboTask.ItemsSource = _taskTypeList;
			this.cboSupply.DisplayMemberPath = "Name";
			this.cboTask.DisplayMemberPath = "Name";
        }

        private void setupEditMode()
        {
            lblHeader.Content = "Editing an existing Task Type Supply Need";
            btnAddEdit.Content = "Edit";
            cboSupply.ItemsSource = _supplyItemList;
            cboTask.ItemsSource = _taskTypeList;
            cboSupply.DisplayMemberPath = "Name";
            cboTask.DisplayMemberPath = "Name";
            cboSupply.SelectedItem = _supplyItemList.Find(s => s.SupplyItemID == _taskTypeSupplyNeed.SupplyItemID);
            cboTask.SelectedItem = _taskTypeList.Find(t => t.TaskTypeID == _taskTypeSupplyNeed.TaskTypeID);
            cboSupply.IsEnabled = false;
			cboTask.IsEnabled = false;
            numQuantity.Value = _taskTypeSupplyNeed.Quantity;
        }

        private void btnAddEdit_Click(object sender, RoutedEventArgs e)
        {
            if (_mode == DetailFormMode.Add)
            {
                addTaskTypeSupplyNeed();
            }
            if (_mode == DetailFormMode.Edit)
            {
                editTaskTypeSupplyNeed();
            }
        }
		
        /// <summary>
        /// Jacob Conley
        /// Created on 2018/03/29
        /// 
        /// Method to add a task type supply need item
        /// </summary>
        private void addTaskTypeSupplyNeed()
        {
            if (cboTask.SelectedItem == null)
            {
                MessageBox.Show("Please select a task.");
            }
            if (cboSupply.SelectedItem == null)
            {
                MessageBox.Show("Please select a supply for the task.");
            }
            else
            {
                try
                {
					TaskType task = (TaskType)cboTask.SelectedItem;
					SupplyItem supply = (SupplyItem)cboSupply.SelectedItem;
					TaskTypeSupplyNeed taskTypeSupplyNeed = new TaskTypeSupplyNeed()
					{
						TaskTypeID = task.TaskTypeID,
						SupplyItemID = supply.SupplyItemID,
                        Quantity = (int)numQuantity.Value
					};
					_taskTypeSupplyNeedManager.AddTaskTypeSupplyNeedItem(taskTypeSupplyNeed);
                    this.DialogResult = true;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("There was an error adding the supply order.", ex.Message);
                    this.DialogResult = false;
                }
            }
        }
		
		/// <summary>
        /// Jacob Conley
        /// Created on 2018/03/29
        /// 
        /// Method to edit a task type supply need item
        /// </summary>
        private void editTaskTypeSupplyNeed()
        {
            if (_taskTypeSupplyNeed.Quantity == numQuantity.Value)
            {
                this.DialogResult = false;
            }
            else
            {
				try 
				{
					TaskTypeSupplyNeed newTaskTypeSupplyNeed = new TaskTypeSupplyNeed()
					{
						TaskTypeID = _taskTypeSupplyNeed.TaskTypeID,
						SupplyItemID = _taskTypeSupplyNeed.SupplyItemID,
                        Quantity = (int)numQuantity.Value
					};
					_taskTypeSupplyNeedManager.EditTaskTypeSupplyNeedItem(_taskTypeSupplyNeed, newTaskTypeSupplyNeed);
					this.DialogResult = true;
				 }
                catch (Exception ex)
                {
                    MessageBox.Show("There was an error editing the supply order.", ex.Message);
                    this.DialogResult = false;
                }
			}
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }
    }
}
