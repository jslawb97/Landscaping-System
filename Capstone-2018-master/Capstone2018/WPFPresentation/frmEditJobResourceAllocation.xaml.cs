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
	/// Interaction logic for frmEditJobResourceAllocation.xaml
	/// </summary>
	public partial class frmEditJobResourceAllocation : Window
	{
		private JobDetail _jobDetail;

		private int _oldQuantity;
		private List<TaskEmployeeDetail> _employeeDetailList;
		private List<TaskSupplyDetail> _taskSupplyDetailList;
		private List<TaskEquipmentDetail> _equipmentDetailList;
		private ITaskSupplyManager _taskSupplyManager;
		private ITaskEquipmentManager _taskEquipmentManager = new TaskEquipmentManager();
		private ITaskEmployeeManager _taskEmployeeManager = new TaskEmployeeManager();

        private IEmployeeManager _employeeManager = new EmployeeManager();
        private List<Employee> _availableEmployeeList = new List<Employee>();

        private IEquipmentManager _equipmentManager = new EquipmentManager();
        private List<Equipment> _availableEquipmentList = new List<Equipment>();

        private IJobManager _jobManager = new JobManager();

		public frmEditJobResourceAllocation(JobDetail jobDetail)
		{
			InitializeComponent();
			_jobDetail = jobDetail;
			//timeScheduled.Minimum = DateTime.Now;
			_taskSupplyManager = new TaskSupplyManager();
			//timeScheduled.Maximum = timeTargetDate.Value;

		}

		/// <summary>
		/// Zachary Hall
		/// Created on 2018/04/05
		/// 
		/// Cancels the form
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		/// <summary>
		private void btnCancel_Click(object sender, RoutedEventArgs e)
		{
			this.Close();
		}

        /// James McPherson
        /// Created 2018/04/06
        /// 
        /// Method to update a TaskSupply that was changed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <summary>
        //private void dgSupplyAllocation_SelectionChanged(object sender, SelectionChangedEventArgs e)
        //{
        //    RemovedItems has the previously selected row

        //    if (e.RemovedItems.Count > 0)
        //    {
        //        var newTaskSupply = (TaskSupplyDetail)e.RemovedItems[0];
        //        if (_oldQuantity != newTaskSupply.TaskSupplyQuantity)
        //        {
        //            try
        //            {
        //                var oldTaskSupply = new TaskSupplyDetail
        //                {
        //                    TaskSupplyTaskSupplyID = newTaskSupply.TaskSupplyTaskSupplyID,
        //                    TaskName = newTaskSupply.TaskName,
        //                    TaskSupplyQuantity = _oldQuantity,
        //                    TaskTypeSupplyNeedQuantity = newTaskSupply.TaskTypeSupplyNeedQuantity,
        //                    SupplyItemQuantityInStock = newTaskSupply.SupplyItemQuantityInStock,
        //                    SupplyItemName = newTaskSupply.SupplyItemName
        //                };
        //                var result = _taskSupplyManager.EditTaskSupplyQuantity(oldTaskSupply, newTaskSupply);
        //            }
        //            catch (Exception ex)
        //            {
        //                MessageBox.Show(ex.Message + "\n\n" + ex.InnerException.Message);
        //                newTaskSupply.TaskSupplyQuantity = _oldQuantity;
        //                dgSupplyAllocation.ItemsSource = null;
        //                dgSupplyAllocation.ItemsSource = _taskSupplyDetailList;
        //            }
        //        }
        //    }

        //    Need to store the old TaskSupply quantity

        //    if (e.AddedItems.Count > 0)
        //    {
        //        var nextTaskSupply = (TaskSupplyDetail)e.AddedItems[0];
        //        _oldQuantity = nextTaskSupply.TaskSupplyQuantity;
        //    }
        //}

        /// Zachary Hall
        /// Created on 2018/04/05
        /// 
        /// Refreshes the Employee datagrid
        /// </summary>
        private void refreshTaskEmployeeList()
		{
			dgEmployeeAllocation.ItemsSource = null;
			dgEmployeeAllocation.Items.Clear();
			try
			{
				_employeeDetailList = _taskEmployeeManager.RetrieveTaskEmployeeDetailByJobID(_jobDetail.Job.JobID);
				dgEmployeeAllocation.ItemsSource = _employeeDetailList;

			}
			catch (Exception ex)
			{
				var message = ex.Message + "\n\n" + ex.InnerException.Message;
				MessageBox.Show(message, "Data Retrieval Error.",
					MessageBoxButton.OK, MessageBoxImage.Exclamation);
			}
		}

        private void refreshAvailableEmployeeList()
        {
            dgAvailableEmployees.ItemsSource = null;
            dgAvailableEmployees.Items.Clear();
            try
            {
                _availableEmployeeList = _employeeManager.RetreiveEmployeeListByJobAvailability(_jobDetail.Job.JobID);
                dgAvailableEmployees.ItemsSource = _availableEmployeeList;

            }
            catch (Exception ex)
            {
                var message = ex.Message + "\n\n" + ex.InnerException.Message;
                MessageBox.Show(message, "Data Retrieval Error.",
                    MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
        }

		/// <summary>
		/// Zachary Hall
		/// Created on 2018/04/05
		/// 
		/// Refreshes the Employee tab
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void TabItem_Selected(object sender, RoutedEventArgs e)
		{
			if (!validateJobDetailTab())
			{
				e.Handled = true;
				tabsetMain.SelectedItem = jobDetail;
				tabEmployees.IsSelected = false;
				return;
			}
			refreshTaskEmployeeList();
            refreshAvailableEmployeeList();

        }


		/// <summary>
		/// Mike Mason
		/// Created on 2018/04/05
		/// 
		/// Refreshes the Supplies tab
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void TabSupplies_Selected(object sender, RoutedEventArgs e)
		{
			if (!validateJobDetailTab())
			{
				e.Handled = true;
				tabsetMain.SelectedItem = jobDetail;
				tabSupplies.IsSelected = false;
				return;
			}
			refreshTaskSupplyList();
		}


		/// <summary>
		/// Mike Mason
		/// Created on 2018/04/05
		/// 
		/// Refreshes the Supply datagrid
		/// </summary>
		private void refreshTaskSupplyList()
		{
			dgSupplyAllocation.ItemsSource = null;
			try
			{
				_taskSupplyDetailList = _taskSupplyManager.RetrieveTaskSupplyDetailList(_jobDetail.Job.JobID);
				dgSupplyAllocation.ItemsSource = _taskSupplyDetailList;

			}
			catch (Exception ex)
			{
				var message = ex.Message + "\n\n" + ex.InnerException.Message;
				MessageBox.Show(message, "Data Retrieval Error.",
					MessageBoxButton.OK, MessageBoxImage.Exclamation);
			}
		}

		/// <summary>
		/// Badis Saidani
		/// Created on 2018/04/05
		/// 
		/// Removes the Employee tab
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		//private void btnRemoveEmployeeAllocation_Click(object sender, RoutedEventArgs e)
		//{
		//	TaskEmployeeDetail selectedTaskEmployee = (TaskEmployeeDetail)dgEmployeeAllocation.SelectedItem;
		//	if (selectedTaskEmployee != null)
		//	{
		//		try
		//		{
		//			_taskEmployeeManager.RemoveTaskEmployeeByTaskTypeEmployeeNeedId(selectedTaskEmployee.TaskTypeEmployeeNeedID);
		//			refreshTaskEmployeeList();
		//			MessageBox.Show("TaskEmployee records removed.");
		//		}
		//		catch
		//		{
		//			MessageBox.Show("Failed to remove TaskEmployee. :(");
		//		}
		//	}
		//	else
		//	{
		//		MessageBox.Show("You must select a TaskEmployeeDetail to deactivate.");
		//	}
		//}

		//private void btnEditEmployeeAllocation_Click(object sender, RoutedEventArgs e)
		//{
		//	if (this.dgEmployeeAllocation.SelectedItems.Count > 0)
		//	{
		//		//var editForm = new frmAddEditEmployeeAllocation((TaskEmployeeDetail)this.dgEmployeeAllocation.SelectedItem, _jobDetail, _taskEmployeeManager);
		//		//var result = editForm.ShowDialog();
		//		refreshTaskEmployeeList();
		//	}
		//	else
		//	{
		//		MessageBox.Show("You must select something!");
		//	}
		//}

		/// <summary>
		/// Brady Feller
		/// Created on 2018/04/05
		/// 
		/// Refreshes the Equipment datagrid
		/// </summary>
		private void refreshTaskEquipmentList()
		{
			dgEquipmentAllocation.ItemsSource = null;
			dgEquipmentAllocation.Items.Clear();
			try
			{
				_equipmentDetailList = _taskEquipmentManager.RetrieveTaskEquipmentDetailByJobID(_jobDetail.Job.JobID);
				dgEquipmentAllocation.ItemsSource = _equipmentDetailList;

			}
			catch (Exception ex)
			{
				var message = ex.Message + "\n\n" + ex.InnerException.Message;
				MessageBox.Show(message, "Data Retrieval Error.",
					MessageBoxButton.OK, MessageBoxImage.Exclamation);
			}
		}

        private void refreshAvailabledEquipment()
        {
            //dgAvailableEquipment
            dgAvailableEquipment.ItemsSource = null;
            dgAvailableEquipment.Items.Clear();
            try
            {
                _availableEquipmentList = _equipmentManager.RetreiveAvailableEquipmentByJobID(_jobDetail.Job.JobID);
                dgAvailableEquipment.ItemsSource = _availableEquipmentList;

            }
            catch (Exception ex)
            {
                var message = ex.Message + "\n\n" + ex.InnerException.Message;
                MessageBox.Show(message, "Data Retrieval Error.",
                    MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
        }

        /// <summary>
        /// Brady Feller
        /// 2018/04/05
        /// 
        /// Refreshes the Equipment datagrid
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TabItem_Selected_1(object sender, RoutedEventArgs e)
		{
			if (!validateJobDetailTab())
			{
				e.Handled = true;
				tabsetMain.SelectedItem = jobDetail;
				tabEquipment.IsSelected = false;
				return;
			}
			refreshTaskEquipmentList();
            refreshAvailabledEquipment();

        }

		/// <summary>
		/// Weston Olund
		/// 2018/04/13
		/// Method to handle job details tab being selected
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void TabJobDetails_Selected(object sender, RoutedEventArgs e)
		{
			refreshJobDetailTab();
		}

		/// <summary>
		/// Weston Olund
		/// Gets Job Detail Info and populates tab
		/// 2018/04/13
		/// </summary>
		private void refreshJobDetailTab()
		{
			var customerNameField = "";
			customerNameField += _jobDetail.Customer.LastName + ", " + _jobDetail.Customer.FirstName;
			this.txtCustomerName.Text = customerNameField;
			this.txtJobAddress.Text = _jobDetail.JobLocationDetail.JobLocation.AddressDisplay;
			this.txtComments.Text = _jobDetail.Job.Comments;
			this.timeTargetDate.Value = _jobDetail.Job.DateTarget;
            if (_jobDetail.Job.DateScheduled.HasValue)
            {
                this.timeScheduled.Value = _jobDetail.Job.DateScheduled;
            }
            else
            {
                this.timeScheduled.Minimum = DateTime.Now;
                this.timeScheduled.ClipValueToMinMax = true;
            }
			
          
		}

        /// <summary>
        /// Weston Olund
        /// 2018/05/13
        /// Ensures time scheduled is selected
        /// </summary>
        /// <returns></returns>
		private bool validateJobDetailTab()
		{
			if (this.timeScheduled.Value == null)
			{
				MessageBox.Show("You must select a Date to schedule the job", "Invalid Input", MessageBoxButton.OK, MessageBoxImage.Exclamation);
				return false;
			}
			return true;
		}

        private void btnAssign_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (this.dgAvailableEmployees.SelectedItems.Count > 0 && this.dgEmployeeAllocation.SelectedItems.Count > 0)
                {
                    var taskEmployeeDetail = ((TaskEmployeeDetail)this.dgEmployeeAllocation.SelectedItem);
                    var employee = ((Employee)this.dgAvailableEmployees.SelectedItem);

                    bool result = _taskEmployeeManager.UpdateEmployeeID(taskEmployeeDetail.TaskEmployeeID, employee.EmployeeID);
                    if (!result)
                    {
                        throw new ApplicationException("There was an error updating the TaskEmployee record");
                    }
                    else
                    {
                        MessageBox.Show("Update success!");
                        refreshTaskEmployeeList();
                    }
                }
                else
                {
                    MessageBox.Show("You must select a Job Task and an Available Employee.");
                }
            }
            catch (Exception ex)
            {

                var message = ex.Message + "\n\n" + ex.InnerException.Message;
                MessageBox.Show(message, "Assign Error",
                    MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
        }

        private void btnUnassign_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (this.dgEmployeeAllocation.SelectedItems.Count > 0)
                {
                    var taskEmployeeDetail = ((TaskEmployeeDetail)this.dgEmployeeAllocation.SelectedItem);
                    if(taskEmployeeDetail.EmployeeID == null)
                    {
                        MessageBox.Show("There is no employee for this record.\n\nCannot unassign.");
                        return;
                    }
                    bool result = _taskEmployeeManager.UpdateEmployeeIDToNull(taskEmployeeDetail.TaskEmployeeID);
                    if (!result)
                    {
                        throw new ApplicationException("There was an error updating the TaskEmployee record");
                    }
                    else
                    {
                        MessageBox.Show("Update success!");
                        refreshTaskEmployeeList();
                    }
                }
                else
                {
                    MessageBox.Show("You must select a Job Task.");
                }
            }
            catch (Exception ex)
            {

                var message = ex.Message + "\n\n" + ex.InnerException.Message;
                MessageBox.Show(message, "Assign Error",
                    MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
        }

        private void btnSaveScheduledDate_Click(object sender, RoutedEventArgs e)
        {
            if (this.timeScheduled.Value.HasValue)
            {
                if(this.timeScheduled.Value >= DateTime.Now && this.timeScheduled.Value <= timeTargetDate.Value)
                {
                    try
                    {
                        bool result = _jobManager.UpdateJobScheduledDate(_jobDetail.Job.JobID, (DateTime)(this.timeScheduled.Value));

                        if (result)
                        {
                            MessageBox.Show("Scheduled date updated.");
                            _jobDetail = _jobManager.RetreiveJobDetailByID(_jobDetail.Job.JobID);
                        }
                    }
                    catch (Exception ex)
                    {

                        var message = ex.Message + "\n\n" + ex.InnerException.Message;
                        MessageBox.Show(message, "Data Retrieval Error.",
                            MessageBoxButton.OK, MessageBoxImage.Exclamation);
                    }
                }
                else
                {
                    MessageBox.Show("You must select a value between now and the target date!");
                    this.timeScheduled.Value = null;
                }
            }
            else
            {
                MessageBox.Show("You must select a value!");
            }
        }

        private void btnAssignEquipment_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (this.dgAvailableEquipment.SelectedItems.Count > 0 && this.dgEquipmentAllocation.SelectedItems.Count > 0)
                {
                    var taskEquipmentDetail = ((TaskEquipmentDetail)this.dgEquipmentAllocation.SelectedItem);
                    var equipment = ((Equipment)this.dgAvailableEquipment.SelectedItem);

                    bool result = _taskEquipmentManager.UpdateEquipmentID(taskEquipmentDetail.TaskEquipmentID, equipment.EquipmentID);
                    if (!result)
                    {
                        throw new ApplicationException("There was an error updating the TaskEquipment record");
                    }
                    else
                    {
                        MessageBox.Show("Update success!");
                        refreshTaskEquipmentList();
                    }
                }
                else
                {
                    MessageBox.Show("You must select a Job Task and an Available Equipment.");
                }
            }
            catch (Exception ex)
            {

                var message = ex.Message + "\n\n" + ex.InnerException.Message;
                MessageBox.Show(message, "Assign Error",
                    MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
        }

        private void btnUnassignEquipment_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (this.dgEquipmentAllocation.SelectedItems.Count > 0)
                {
                    var taskEquipmentDetail = ((TaskEquipmentDetail)this.dgEquipmentAllocation.SelectedItem);
                    if (taskEquipmentDetail.EquipmentID == null)
                    {
                        MessageBox.Show("There is no equipment for this record.\n\nCannot unassign.");
                        return;
                    }
                    bool result = _taskEquipmentManager.UpdateEquipmentIDToNull(taskEquipmentDetail.TaskEquipmentID);
                    if (!result)
                    {
                        throw new ApplicationException("There was an error updating the TaskEquipment record");
                    }
                    else
                    {
                        MessageBox.Show("Update success!");
                        refreshTaskEquipmentList();
                    }
                }
                else
                {
                    MessageBox.Show("You must select a Job Task.");
                }
            }
            catch (Exception ex)
            {

                var message = ex.Message + "\n\n" + ex.InnerException.Message;
                MessageBox.Show(message, "Assign Error",
                    MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
        }

        /// <summary>
        /// Noah Davison
        /// 2018/04/13
        /// 
        /// button to open frmAddEditEquipmentAllocation
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        //private void btnEditEquipmentAllocation_Click(object sender, RoutedEventArgs e)
        //{
        //    if (dgEquipmentAllocation.SelectedItem == null)
        //    {
        //        MessageBox.Show("You must select a task to edit");
        //    }
        //    else
        //    {
        //        TaskEquipmentDetail selectedTaskEquipmentDetail = (TaskEquipmentDetail)dgEquipmentAllocation.SelectedItem;
        //        frmAddEditEquipmentAllocation frmAddEditEquipmentAllocation = new frmAddEditEquipmentAllocation(selectedTaskEquipmentDetail.TaskID, _jobDetail);
        //        frmAddEditEquipmentAllocation.ShowDialog();
        //    }


        //}

        //private void btnRemoveEquipmentAllocation_Click(object sender, RoutedEventArgs e)
        //{
        //    if (dgEquipmentAllocation.SelectedItem == null)
        //    {
        //        MessageBox.Show("You must select a task to delete equipment from.");
        //    }
        //    else
        //    {
        //        try
        //        {
        //            int rowsDeleted = 0;
        //            TaskEquipmentDetail selectedTaskEquipmentDetail = (TaskEquipmentDetail)dgEquipmentAllocation.SelectedItem;
        //            rowsDeleted = _taskEquipmentManager.DeleteAssignedEquipmentByTaskIDAndJobID(selectedTaskEquipmentDetail.TaskID, _jobDetail.Job.JobID);
        //            MessageBox.Show(rowsDeleted + " equipment items were unassigned from task " + selectedTaskEquipmentDetail.TaskName);
        //            refreshTaskEquipmentList();
        //        }
        //        catch (Exception ex)
        //        {
        //            MessageBox.Show("Error: " + ex.Message);
        //        }
        //    }
        //}
    }
}

