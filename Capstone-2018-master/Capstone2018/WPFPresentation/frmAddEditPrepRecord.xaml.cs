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
    /// Interaction logic for frmAddEditPrepRecord.xaml
    /// </summary>
    public partial class frmAddEditPrepRecord : Window
    {
        private IPrepRecordManager _prepRecordManager;

        private IEmployeeManager _employeeManager = new EmployeeManager();
        private List<Employee> _employeeList = new List<Employee>();

        private IEquipmentManager _equipmentManager = new EquipmentManager();
        private List<Equipment> _equipmentList = new List<Equipment>();

        private PrepRecordDetail _prepRecordDetail;
        private PrepRecord _prepRecord;

        public frmAddEditPrepRecord()
        {
            InitializeComponent();
        }

        public frmAddEditPrepRecord(IPrepRecordManager prepRecordManager)
        {
            InitializeComponent();
            _prepRecordManager = prepRecordManager;
            populateAdd();
        }


        public frmAddEditPrepRecord(IPrepRecordManager prepRecordManager, PrepRecordDetail selectedItem)
        {
            InitializeComponent();
            _prepRecordManager = prepRecordManager;

            this._prepRecordDetail = selectedItem;
            _prepRecord = new PrepRecord
            {
                PrepRecordID = _prepRecordDetail.PrepRecordID,
                EquipmentID = _prepRecordDetail.EquipmentID,
                EmployeeID = _prepRecordDetail.EmployeeID,
                Description = _prepRecordDetail.Description,
                Date = _prepRecordDetail.Date
            };
            populateEdit();
        }

        /// <summary>
        /// Badis Saidani
        /// Created 2018/05/04
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

        /// <summary>
        // Badis Saidani
        /// Created 2018/05/04
        /// 
        /// Add/edit click event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAddEdit_Click(object sender, RoutedEventArgs e)
        {
            if (_prepRecordDetail == null)//add
            {
                performAdd();
            }
            else
            {
                performEdit();
            }
        }

        /// <summary>
        /// Badis Saidani
        /// Created 2018/05/04
        /// 
        /// Populates the fields for adding
        /// </summary>
        private void populateAdd()
        {
            populateDropdowns();
            dpDate.SelectedDate = DateTime.Now;
        }


        /// <summary>
        /// Badis Saidani
        /// Created 2018/05/04
        /// 
        /// Populates the dropdowns
        /// </summary>
        private void populateDropdowns()
        {
            try
            {
                _equipmentList = _equipmentManager.RetrieveEquipmentList();
                cboEquipment.ItemsSource = _equipmentList;
                cboEquipment.DisplayMemberPath = "Name";

                _employeeList = _employeeManager.RetrieveEmployeeList();
                cboEmployee.ItemsSource = _employeeList;
                cboEmployee.DisplayMemberPath = "FullName";

            }
            catch (Exception ex)
            {

                var message = ex.Message + "\n\n" + ex.InnerException;
                MessageBox.Show(message, "List Retrieval Error!", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
        }

        /// <summary>
        /// Badis Saidani
        /// Created 2018/05/04
        /// 
        /// populates the fields for the edit
        /// </summary>
        private void populateEdit()
        {
            populateDropdowns();
            foreach (Equipment t in cboEquipment.Items)
            {
                if (t.EquipmentID == _prepRecordDetail.EquipmentID)
                {
                    this.cboEquipment.SelectedIndex = cboEquipment.Items.IndexOf(t);
                    break;
                }
            }

            foreach (Employee t in cboEmployee.Items)
            {
                if (t.EmployeeID == _prepRecordDetail.EmployeeID)
                {
                    this.cboEmployee.SelectedIndex = cboEmployee.Items.IndexOf(t);
                    break;
                }
            }


            txtDescription.Text = _prepRecordDetail.Description;

            dpDate.SelectedDate = _prepRecordDetail.Date;

            lblHeader.Content = "Editing Prep Record";
            btnAddEdit.Content = "Save";
        }

        /// <summary>
        /// Badis Saidani
        /// Created 2018/05/04
        /// 
        /// Performs the edit functionality
        /// </summary>
        private void performEdit()
        {
            if (validateFields())
            {
                try
                {
                    var newPrepRecord = new PrepRecord
                    {
                        PrepRecordID = _prepRecordDetail.PrepRecordID,
                        EquipmentID = ((Equipment)(cboEquipment.SelectedItem)).EquipmentID,
                        EmployeeID = ((Employee)(cboEmployee.SelectedItem)).EmployeeID,
                        Description = txtDescription.Text,
                        Date = (DateTime)dpDate.SelectedDate

                    };
                    var updatePrepRecordResult = _prepRecordManager.EditPrepRecordItem(_prepRecord, newPrepRecord);

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
        /// Badis Saidani
        /// Created 2018/05/04
        /// 
        /// Performs the add functionality
        /// </summary>
        private void performAdd()
        {
            if (validateFields())
            {
                try
                {
                    var newPrepRecord = new PrepRecord
                    {

                        EquipmentID = ((Equipment)(cboEquipment.SelectedItem)).EquipmentID,
                        EmployeeID = ((Employee)(cboEmployee.SelectedItem)).EmployeeID,
                        Description = txtDescription.Text,
                        Date = (DateTime)dpDate.SelectedDate

                    };
                    var createEmployeeNeed = _prepRecordManager.CreatePrepRecord(newPrepRecord);

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


        private bool validateFields()
        {
            bool isValid = true;

            if (cboEquipment.SelectedItem == null)
            {
                MessageBox.Show("You must select an equipment!", "Invalid Input", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                return false;
            }

            if (cboEmployee.SelectedItem == null)
            {
                MessageBox.Show("You must select an employee!", "Invalid Input", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                return false;
            }

            if (txtDescription.Text == null || txtDescription.Text == "")
            {
                MessageBox.Show("You must set a descrption!", "Invalid Input", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                return false;
            }


            return isValid;
        }



    }
}
