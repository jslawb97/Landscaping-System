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
using System.Windows.Shapes;

namespace WPFPresentation
{
    /// <summary>
    /// Interaction logic for frmAddEditEquipmentAllocation.xaml
    /// </summary>
    public partial class frmAddEditEquipmentAllocation : Window
    {

        private ITaskEquipmentManager _taskEquipmentManager = new TaskEquipmentManager();
        EquipmentManager _equipmentManager = new EquipmentManager();
        Job _job;
        EquipmentType _equipmentType;
        TaskEquipmentDetail _taskEquipmentDetail;
        List<Equipment> _equipmentAvailable = null;
        private int _taskID;
        private JobDetail _jobDetail;

        public frmAddEditEquipmentAllocation(Job job, EquipmentType equipmentType, int taskID, TaskEquipmentDetail taskEquipmentDetail)
        {
            _equipmentType = equipmentType;
            _job = job;
            _taskEquipmentDetail = taskEquipmentDetail;
            InitializeComponent();
        }

        /// <summary>
        /// Noah Davison
        /// Created 4/13/2018
        /// 
        /// Constructor to populate allocated equipment. Temporary until RetrieveAvailableEquipment is complete.
        /// </summary>
        /// <param name="taskID"></param>
        public frmAddEditEquipmentAllocation(int taskID, JobDetail jobDetail)
        {
            _taskID = taskID;
            _jobDetail = jobDetail;
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            populateControls();

        }

        public void populateControls()
        {
            // get availble Equipment list
            try
            {
                //Change second _job.DateScheduled to the calculat
                _equipmentAvailable = _equipmentManager.RetrieveEquipmentListByTypeAndAvailability(_equipmentType, _job.DateScheduled, _job.DateScheduled);
            }
            catch (Exception ex)
            {
                var message = ex.Message;
                if (ex.InnerException != null)
                {
                    message += "\n\n" + ex.InnerException.Message;
                }
                MessageBox.Show(message, "Data Retrieval Fail", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
            

            // get assigned equipment list
            try
            {
                dgAssignedEquipment.ItemsSource = _taskEquipmentManager.RetrieveAssignedEquipmentByTaskIDAndJobID(_taskID, _jobDetail.Job.JobID);
            }
            catch(Exception ex)
            { 
                var message = ex.Message;
                if (ex.InnerException != null)
                {
                    message += "\n\n" + ex.InnerException.Message;
                }
                MessageBox.Show(message, "Data Retrieval Fail", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }

            // populate dgAvailableEquipment
            dgAvailableEquipment.ItemsSource = _equipmentAvailable;
            
        }

        /// <summary>
        /// Sam Dramstad 
        /// Created on 2018/04/06
        /// 
        /// Adds a selected equipment item to the assigned task list.
        /// </summary>
        private void btnAddToAssigned_Click(object sender, RoutedEventArgs e)
        {
            if (dgAvailableEquipment.SelectedItems.Count == 0 )
            {
                MessageBox.Show("You need to select an equipment item from the Available list.");
                return;
            }

            var selectedItem = (Equipment)this.dgAvailableEquipment.SelectedItem;

            _taskEquipmentManager.AddEquipmentToTaskEquipment(selectedItem.EquipmentID, _job.JobID, _taskEquipmentDetail.TaskTypeEquipmentNeedID);
            _equipmentAvailable.Remove(selectedItem);

            populateControls();
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }
    }
}
