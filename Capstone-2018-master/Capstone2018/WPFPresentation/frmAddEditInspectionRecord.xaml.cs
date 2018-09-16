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
    /// Interaction logic for frmAddEditInspectionRecord.xaml
    /// </summary>
    public partial class frmAddEditInspectionRecord : Window
    {
        private IInspectionRecordManager _inspectionRecordManager;
        private IEquipmentManager _equipmentManager;
        private IEmployeeManager _employeeManager;
        private DetailFormMode _mode;
        private InspectionRecordDetail _inspectionRecordDetail;
        private User _user;

        private List<Equipment> _equipmentList;
        private List<Employee> _employeeList;

        /// <summary>
        /// James McPherson
        /// Created 2018/04/03
        /// 
        /// Constructor for edit and view modes
        /// </summary>
        /// <param name="inspectionRecordManager"></param>
        /// <param name="equipmentManager"></param>
        /// <param name="employeeManager"></param>
        /// <param name="mode"></param>
        /// <param name="inspectionRecordDetail"></param>
        public frmAddEditInspectionRecord(IInspectionRecordManager inspectionRecordManager
            , IEquipmentManager equipmentManager, IEmployeeManager employeeManager
            , DetailFormMode mode, InspectionRecordDetail inspectionRecordDetail)
        {
            _inspectionRecordManager = inspectionRecordManager;
            _equipmentManager = equipmentManager;
            _employeeManager = employeeManager;
            _mode = mode;
            _inspectionRecordDetail = inspectionRecordDetail;
            InitializeComponent();
        }

        /// <summary>
        /// James McPherson
        /// Created 2018/04/03
        /// 
        /// Constructor for add mode
        /// </summary>
        /// <param name="inspectionRecordManager"></param>
        /// <param name="equipmentManager"></param>
        /// <param name="employeeManager"></param>
        /// <param name="user"></param>
        public frmAddEditInspectionRecord(IInspectionRecordManager inspectionRecordManager
            , IEquipmentManager equipmentManager, IEmployeeManager employeeManager,
            User user)
        {
            _inspectionRecordManager = inspectionRecordManager;
            _equipmentManager = equipmentManager;
            _employeeManager = employeeManager;
            _mode = DetailFormMode.Add;
            _user = user;
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                _equipmentList = _equipmentManager.RetrieveEquipmentList();
                _employeeList = _employeeManager.RetrieveEmployeeListByActive();

                cboEquipment.ItemsSource = _equipmentList;
                cboEmployee.ItemsSource = _employeeList;
            }
            catch
            {
                MessageBox.Show("One or more option lists were not found!");
            }

            switch (_mode)
            {
                case DetailFormMode.Add:
                    setupAddMode();
                    break;
                case DetailFormMode.Edit:
                    setupEditMode();
                    break;
                case DetailFormMode.View:
                    setupViewMode();
                    break;
            }
        }

        private void setupAddMode()
        {
            btnAddEdit.Content = "Add";
            Title = "Add a new InspectionRecord";
            lblHeader.Content = "Adding a new InspectionRecord";
            foreach(var employee in cboEmployee.Items)
            {
                if(((Employee) employee).EmployeeID == _user.Employee.EmployeeID)
                {
                    cboEmployee.SelectedItem = employee;
                    break;
                }
            }
            dpDate.SelectedDate = DateTime.Now;
            setReadOnlyInputs(false);
        }

        private void setupEditMode()
        {
            btnAddEdit.Content = "Save";
            Title = "Edit an existing InspectionRecord";
            lblHeader.Content = "Editing an existing InspectionRecord";
            setReadOnlyInputs(false);
            populateControls();
        }

        private void setupViewMode()
        {
            btnAddEdit.Content = "Edit";
            Title = "View an existing InspectionRecord";
            lblHeader.Content = "Viewing an existing InspectionRecord";
            setReadOnlyInputs(true);
            populateControls();
        }

        /// <summary>
        /// James McPherson
        /// Created 2018/04/03
        /// 
        /// Sets input values to the values of the item being edited/viewed
        /// </summary>
        private void populateControls()
        {
            foreach(var item in cboEquipment.Items)
            {
                if(((Equipment)item).EquipmentID
                    == _inspectionRecordDetail.EquipmentID)
                {
                    cboEquipment.SelectedItem = item;
                    break;
                }
            }

            foreach (var item in cboEmployee.Items)
            {
                if (((Employee)item).EmployeeID
                    == _inspectionRecordDetail.EmployeeID)
                {
                    cboEmployee.SelectedItem = item;
                    break;
                }
            }

            txtDescription.Text = _inspectionRecordDetail.Description;
            dpDate.SelectedDate = _inspectionRecordDetail.Date;
        }

        private void setReadOnlyInputs(bool readOnly)
        {
            txtDescription.IsReadOnly = readOnly;
            cboEquipment.IsEnabled = !readOnly;
            cboEmployee.IsEnabled = !readOnly;
            dpDate.IsEnabled = !readOnly;
        }

        private void btnAddEdit_Click(object sender, RoutedEventArgs e)
        {
            if (_mode == DetailFormMode.View)
            {
                _mode = DetailFormMode.Edit;
                setupEditMode();
                return;
            }

            var inspectionRecord = new InspectionRecord();

            switch(_mode)
            {
                case DetailFormMode.Add:
                    if(captureInspectionRecord(inspectionRecord) == false)
                    {
                        return;
                    }

                    try
                    {
                        if(_inspectionRecordManager.CreateInspectionRecord(inspectionRecord))
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
                    if(captureInspectionRecord(inspectionRecord) == false)
                    {
                        return;
                    }

                    inspectionRecord.InspectionRecordID = _inspectionRecordDetail.InspectionRecordID;
                    try
                    {
                        if (_inspectionRecordManager.EditInspectionRecord(_inspectionRecordDetail
                            , inspectionRecord))
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

        private bool captureInspectionRecord(InspectionRecord inspectionRecord)
        {
            if (cboEquipment.SelectedItem == null)
            {
                MessageBox.Show("You must select an equipment.");
                return false;
            } else
            {
                inspectionRecord.EquipmentID = ((Equipment)cboEquipment.SelectedItem).EquipmentID;
            }
            if (cboEmployee.SelectedItem == null)
            {
                MessageBox.Show("You must select an employee.");
                return false;
            } else
            {
                inspectionRecord.EmployeeID = ((Employee)cboEmployee.SelectedItem).EmployeeID;
            }
            if(txtDescription.Text == "")
            {
                MessageBox.Show("You must enter a description.");
                return false;
            } else
            {
                inspectionRecord.Description = txtDescription.Text;
            }
            if (dpDate.SelectedDate == null)
            {
                MessageBox.Show("You must select a date.");
                return false;
            }
            else
            {
                inspectionRecord.Date = (DateTime) dpDate.SelectedDate;
            }

            return true;
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }
    }
}
