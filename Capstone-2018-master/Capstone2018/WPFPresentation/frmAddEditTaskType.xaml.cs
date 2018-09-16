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
    /// Created 2018/03/25
    /// Interaction logic for frmAddEditTaskType.xaml
    /// </summary>
    public partial class frmAddEditTaskType : Window
    {
        private ITaskTypeManager _taskTypeManager;
        private TaskType _taskType;

        public frmAddEditTaskType(ITaskTypeManager taskTypeManager)
        {
            _taskTypeManager = taskTypeManager;
            InitializeComponent();
            setUpAddForm();
        }

        public frmAddEditTaskType(ITaskTypeManager taskTypeManager, TaskType taskType)
        {
            _taskTypeManager = taskTypeManager;
            _taskType = taskType;
            InitializeComponent();
            setupEditForm();
        }

        /// <summary>
        /// John Miller
        /// Created 2018/03/25
        /// 
        /// Populates fields to reflect the Add functionality
        private void setUpAddForm()
        {
            lblHeader.Content = "Adding New TaskType";
            btnAddEdit.Content = "Add";
            chkActive.IsChecked = true;
            chkActive.IsEnabled = false;

            setAddComboBox();
            txtName.Focus();
        }

        private void setAddComboBox()
        {
            var jobLocationAttributeTypeList = _taskTypeManager.RetrieveJobLocationAttributeTypeList();
            this.cboJobLocationAttributeType.ItemsSource = jobLocationAttributeTypeList;
            this.cboJobLocationAttributeType.SelectedIndex = 0;

            List<int> quantityNumbers = new List<int>();
            for (int i = 0; i < 500; i++)
            {
                quantityNumbers.Add(i);
            }

            //this.intUpDwnQuantity.Increment.Equals(_taskType.Quantity);
            this.intUpDwnQuantity.Value = 0;
        }

        /// <summary>
        /// John Miller
        /// Created 2018/03/25
        /// 
        /// Populates fields to reflect the Edit functionality
        /// </summary>
        private void setupEditForm()
        {
            lblHeader.Content = "Editing TaskType " + _taskType.Name;
            btnAddEdit.Content = "Save";
            txtName.Text = _taskType.Name;

            setEditComboBox();
            chkActive.IsChecked = _taskType.Active;
            txtName.Focus();
        }

        private void setEditComboBox()
        {
            List<string> jobLocationAttributeTypeList = new List<string>();
            jobLocationAttributeTypeList = _taskTypeManager.RetrieveJobLocationAttributeTypeList();

            this.cboJobLocationAttributeType.ItemsSource = jobLocationAttributeTypeList;
            this.cboJobLocationAttributeType.SelectedItem = this._taskType.JobLocationAttributeTypeID;

            List<int> quantityNumbers = new List<int>();
            for (int i = 0; i < 100; i++)
            {
                quantityNumbers.Add(i);
            }

            this.intUpDwnQuantity.Value = this._taskType.Quantity;
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
            return true;
        }

        private void performEdit()
        {
            if (validateFields())
            {
                TaskType newTaskType = new TaskType();
                _taskType.JobLocationAttributeTypeID = _taskType.JobLocationAttributeTypeID;

                newTaskType.Name = this.txtName.Text;
                newTaskType.Quantity = Int32.Parse(this.intUpDwnQuantity.Text);
                newTaskType.Active = (bool)this.chkActive.IsChecked;
                newTaskType.JobLocationAttributeTypeID = this.cboJobLocationAttributeType.Text;

                try
                {
                    _taskTypeManager.EditTaskTypeByID(_taskType, newTaskType);
                    MessageBox.Show(newTaskType.Name + " Was successfully edited!");
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
                var newJobLocationAttribute = new JobLocationAttribute()
                {
                    JobLocationAttributeTypeID = cboJobLocationAttributeType.Text
                };

                TaskType taskType = new TaskType()
                {
                    Name = txtName.Text,
                    Quantity = (Int32)intUpDwnQuantity.Value,
                    JobLocationAttributeTypeID = cboJobLocationAttributeType.Text
                };

                try
                {
                    var result = _taskTypeManager.CreateTaskType(taskType);
                    MessageBox.Show(taskType.Name + " was successfully added!");
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


        /// <summary>
        /// John Miller
        /// Created 2018/03/25
        /// 
        /// Form Add or Edit Event called by user clicking Add/Edit button.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAddEdit_Click(object sender, RoutedEventArgs e)
        {
            if (_taskType == null)
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
        /// Created 2018/03/25
        /// 
        /// Form Cancel Event called by user clicking Cancel button.
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
