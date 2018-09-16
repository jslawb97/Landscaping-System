using DataObjects;
using Logic;
using System;
using System.Collections.Generic;
using System.Globalization;
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
    /// Interaction logic for frmAddEditMaintenanceRecord.xaml
    /// </summary>
    public partial class frmAddEditMaintenanceRecord : Window
    {
        private IMaintenanceRecordManager _maintenanceRecordManager;
        private IEmployeeManager _employeeManager;
        private IEquipmentManager _equipmentManager;
        private MaintenanceRecordDetail _maintenanceRecordDetail;
        private List<Employee> _employeeList;
        private List<Equipment> _equipmentList;
        private DetailFormMode _mode;

        public frmAddEditMaintenanceRecord()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Brady Feller
        /// Created 2018/03/08
        /// 
        /// Constructor that allows the record to be edited or viewed
        /// </summary>
        /// <param name="mrMgr"></param>
        /// <param name="emMgr"></param>
        /// <param name="eqMgr"></param>
        /// <param name="mrDetail"></param>
        /// <param name="mode"></param>
        public frmAddEditMaintenanceRecord(IMaintenanceRecordManager mrMgr, IEmployeeManager emMgr, IEquipmentManager eqMgr, MaintenanceRecordDetail mrDetail, DetailFormMode mode)
        {
            _maintenanceRecordManager = mrMgr;
            _employeeManager = emMgr;
            _equipmentManager = eqMgr;
            _maintenanceRecordDetail = mrDetail;
            _mode = mode;

            InitializeComponent();
        }

        /// <summary>
        /// Brady Feller
        /// Created 2018/03/08
        /// 
        /// Constructor that allows a record to be added
        /// </summary>
        /// <param name="mrMgr"></param>
        /// <param name="emMgr"></param>
        /// <param name="eqMgr"></param>
        public frmAddEditMaintenanceRecord(IMaintenanceRecordManager mrMgr, IEmployeeManager emMgr, IEquipmentManager eqMgr)
        {
            _maintenanceRecordManager = mrMgr;
            _employeeManager = emMgr;
            _equipmentManager = eqMgr;
            _mode = DetailFormMode.Add;

            InitializeComponent();
        }

        /// <summary>
        /// Brady Feller
        /// Created 2018/03/08
        /// 
        /// Populates the user's controls
        /// </summary>
        private void populateControls()
        {
            switch (_mode)
            {
                case DetailFormMode.Add:
                    setComboBoxes(true);
                    break;
                case DetailFormMode.Edit:
                    setComboBoxes(true);
                    this.txtDescription.Text = _maintenanceRecordDetail.MaintenanceRecord.Description;
                    this.dpDate.Text = _maintenanceRecordDetail.MaintenanceRecord.Date.ToString();
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// Brady Feller
        /// Created 2018/03/08
        /// 
        /// Sets up 'Edit' mode
        /// </summary>
        private void setupEditMode()
        {
            this.btnAddEdit.Content = "Save";
            this.Title = "Edit a Maintenance Record";
            populateControls();
        }

        /// <summary>
        /// Brady Feller
        /// Created 2018/03/08
        /// 
        /// Sets up 'Add' mode
        /// </summary>
        private void setupAddMode()
        {
            this.Title = "Add a New Maintenance Record";
        }

        /// <summary>
        /// Brady Feller
        /// Created 2018/03/08
        /// 
        /// Validates that fields have been entered
        /// </summary>
        /// <param name="maintenanceRecord"></param>
        /// <returns></returns>
        private bool captureMaintenanceRecord(MaintenanceRecord maintenanceRecord)
        {
            if (this.cboEquipmentID.SelectedItem == null)
            {
                MessageBox.Show("You must choose an Equipment.");
                return false;
            }
            else
            {
                maintenanceRecord.EquipmentID = ((Equipment)this.cboEquipmentID.SelectedItem).EquipmentID;
            }
            if (this.cboEmployeeID.SelectedItem == null)
            {
                MessageBox.Show("You must choose an Employee.");
                return false;
            }
            else
            {
                maintenanceRecord.EmployeeID = ((Employee)this.cboEmployeeID.SelectedItem).EmployeeID;
            }
            if (this.txtDescription.Text == "" || this.txtDescription.Text == null)
            {
                MessageBox.Show("You must enter a description.");
                return false;
            }
            else
            {
                maintenanceRecord.Description = txtDescription.Text;
            }
            if (this.dpDate == null)
            {
                MessageBox.Show("You must enter a date.");
                return false;
            }
            else
            {
                maintenanceRecord.Date = dpDate.DisplayDate;
            }

            return true;
        }

        /// <summary>
        /// Brady Feller
        /// Created 2018/03/08
        /// 
        /// Sets the combo boxes with values
        /// </summary>
        /// <param name="enabled"></param>
        private void setComboBoxes(bool enabled = false)
        {
            this.cboEquipmentID.Text = _maintenanceRecordDetail.Equipment.Name.ToString();
            this.cboEquipmentID.IsEnabled = enabled;

            this.cboEmployeeID.Text = _maintenanceRecordDetail.Employee.FirstName.ToString();
            this.cboEmployeeID.IsEnabled = enabled;
        }

        /// <summary>
        /// Brady Feller
        /// Created 2018/03/08
        /// 
        /// Adds or Edits the record depending on the user's choice
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAddEdit_Click(object sender, RoutedEventArgs e)
        {
            var maintenanceRecord = new MaintenanceRecord();

            switch (_mode)
            {
                case DetailFormMode.Add:
                    if (captureMaintenanceRecord(maintenanceRecord) == false)
                    {
                        return;
                    }
                    try
                    {
                        if (_maintenanceRecordManager.CreateMaintenanceRecord(maintenanceRecord) >= 0)
                        {
                            this.DialogResult = true;
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                    break;
                case DetailFormMode.Edit:
                    if (captureMaintenanceRecord(maintenanceRecord) == false)
                    {
                        return;
                    }

                    maintenanceRecord.MaintenanceRecordID = _maintenanceRecordDetail.MaintenanceRecord.MaintenanceRecordID;
                    var oldMaintenanceRecord = _maintenanceRecordDetail.MaintenanceRecord;

                    try
                    {
                        if (_maintenanceRecordManager.EditMaintenanceRecord(oldMaintenanceRecord, maintenanceRecord))
                        {
                            this.DialogResult = true;
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// Brady Feller
        /// Created 2018/03/08
        /// 
        /// Cancels out of the form
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }

        /// <summary>
        /// Brady Feller
        /// Created 2018/03/08
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                _equipmentList = _equipmentManager.RetrieveEquipmentList();
                _employeeList = _employeeManager.RetrieveEmployeeListByActive();// may need true

                this.cboEquipmentID.ItemsSource = _equipmentList;
                this.cboEmployeeID.ItemsSource = _employeeList;
            }
            catch
            {
                MessageBox.Show("One or more option lists were not found!");
            }

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
    }
}
