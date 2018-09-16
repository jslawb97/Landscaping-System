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
    /// Noah Davison
    /// Created: 2018/02/07
    /// 
    /// Interaction logic for frmAddEditEquipment.xaml
    /// </summary>
    public partial class frmAddEditEquipment : Window
    {
        private IMakeModelManager _makeModelManager = new MakeModelManager();
        private List<MakeModel> _makeModelList = new List<MakeModel>();

        private IEquipmentManager _equipmentManager = new EquipmentManager();
        private Equipment _equipment = new Equipment();

        private IEquipmentDetailManager _equipmentDetailManager = new EquipmentDetailManager();
        private List<EquipmentDetail> _equipmentDetailList = new List<EquipmentDetail>();
        private EquipmentDetail _selectedEquipmentDetail = new EquipmentDetail();

        private IEquipmentTypeManager _equipmentTypeManager = new EquipmentTypeManager();
        private List<EquipmentType> _equipmentTypeList = new List<EquipmentType>();

        private IEquipmentStatusManager _equipmentStatusManager = new EquipmentStatusManager();
        private List<EquipmentStatus> _equipmentStatusList = new List<EquipmentStatus>();

        private DetailFormMode _mode;

        /// <summary>
        /// Noah Davison
        /// Created 2018/02/14
        /// 
        /// Constructor for add and view mode
        /// </summary>
        /// <param name="equipmentDetailManager"></param>
        /// /// <param name="mode></param>
        public frmAddEditEquipment(DetailFormMode mode)
        {
            _mode = mode;
            InitializeComponent();
        }

        /// <summary>
        /// Noah Davison
        /// Created 2018/02/14
        /// 
        /// Constructor for edit mode
        /// </summary>
        /// <param name="equipmentDetailManager"></param>
        /// /// <param name="mode></param>
        public frmAddEditEquipment(DetailFormMode mode, EquipmentDetail selectedEquipmentDetail)
        {
            _mode = mode;
            _selectedEquipmentDetail = selectedEquipmentDetail;
            InitializeComponent();
        }

        /// <summary>
        /// Noah Davison
        /// Created: 2018/02/14
        /// 
        /// Decides how to load based on the mode on windows loaded
        /// </summary>
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
                case DetailFormMode.View:
                    setupViewMode();
                    break;
            }

        }

        /// <summary>
        /// Noah Davison
        /// Created 2018/02/14
        /// 
        /// Sets up window to add equipment
        /// </summary>
        private void setupAddMode()
        {
            btnAddEdit.Content = "Add";
            Title = "Add new equipment";
            cboModel.IsEnabled = false;
            chkActive.IsChecked = true;
            chkActive.IsEnabled = false;

            try
            {
                _makeModelList = _makeModelManager.RetrieveMakeModelListByActive();

                //Grabs only unique makes from _makeModelList so there are no duplicate values in the combobox
                List<MakeModel> distinctMakes = _makeModelList
                                .GroupBy(p => p.Make)
                                .Select(g => g.First())
                                .ToList();
                cboMake.ItemsSource = distinctMakes;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: Failed to retrieve make and model list: " + ex);
                this.DialogResult = false;
            }
            try
            {
                cboType.ItemsSource = _equipmentTypeManager.RetrieveEquipmentTypeList();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: Failed to retrieve equipment type list: " + ex);
                this.DialogResult = false;
            }
            try
            {
                cboStatus.ItemsSource = _equipmentStatusManager.RetrieveEquipmentStatusList();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: Failed to retrieve equipment status list: " + ex);
                this.DialogResult = false;
            }
        }


        /// <summary>
        /// Noah Davison
        /// Created 2018/04/13
        /// 
        /// Setup for edit mode
        /// </summary>
        private void setupEditMode()
        {
            try
            {
                chkActive.IsChecked = _selectedEquipmentDetail.Equipment.Active;
                _makeModelList = _makeModelManager.RetrieveMakeModelListByActive();

                //Grabs only unique makes from _makeModelList so there are no duplicate values in the combobox
                List<MakeModel> distinctMakes = _makeModelList
                                .GroupBy(p => p.Make)
                                .Select(g => g.First())
                                .ToList();
                cboMake.ItemsSource = distinctMakes;
                this.cboMake.SelectedItem = _makeModelList.Find(m => m.Make == _selectedEquipmentDetail.Make);

                cboModel.ItemsSource = _makeModelManager.RetrieveMakeModelListByMake(_selectedEquipmentDetail.Make);

                //This is the line that isnt working
                cboModel.Text = _selectedEquipmentDetail.Model;

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: Failed to retrieve make and model list: " + ex);
                this.DialogResult = false;
            }
            try
            {
                _equipmentTypeList = _equipmentTypeManager.RetrieveEquipmentTypeList();
                cboType.ItemsSource = _equipmentTypeList;
                cboType.SelectedItem = _equipmentTypeList.Find(t => t.EquipmentTypeID == _selectedEquipmentDetail.Equipment.EquipmentTypeID);

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: Failed to retrieve equipment type list: " + ex);
                this.DialogResult = false;
            }
            try
            {
                _equipmentStatusList = _equipmentStatusManager.RetrieveEquipmentStatusList();
                cboStatus.ItemsSource = _equipmentStatusList;
                cboStatus.SelectedItem = _equipmentStatusList.Find(t => t.EquipmentStatusID == _selectedEquipmentDetail.Equipment.EquipmentStatusID);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: Failed to retrieve equipment status list: " + ex);
                this.DialogResult = false;
            }
            Title = "Edit equipment";
            btnAddEdit.Content = "Edit";
            txtName.Text = _selectedEquipmentDetail.Equipment.Name;
            txtDetails.Text = _selectedEquipmentDetail.Equipment.EquipmentDetails;
            numPurchasePrice.Value = _selectedEquipmentDetail.Equipment.PriceAtPurchase;
            numCurrentValue.Value = _selectedEquipmentDetail.Equipment.CurrentValue;
            dateWarrantyExpires.Value = _selectedEquipmentDetail.Equipment.WarrantyUntil;
            datePurchased.Value = _selectedEquipmentDetail.Equipment.DatePurchased;
            dateRepaired.Value = _selectedEquipmentDetail.Equipment.DateLastRepaired;
        }

        private void setupViewMode()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Noah Davison
        /// Created: 2018/02/07
        /// 
        /// Validates user input and adds or edits the database
        /// <remarks>
        /// Mike Mason
        /// Updated: 2018/04/26
        /// 
        /// Updated validations to include max length and implemented as a method
        /// </remarks>
        /// </summary>
        private void btnAddEdit_Click(object sender, RoutedEventArgs e)
        {
            //Check if any of the fields are empty
            //if (txtName.Text == "" || cboType.Text == "" || cboMake.Text == "" || cboModel.Text == "" || cboStatus.Text == "" ||
            //    datePurchased.Text == "" || dateRepaired.Text == "" ||
            //    numCurrentValue.Text == "" || numPurchasePrice.Text == "" || txtDetails.Text == "")
            //{
            //    MessageBox.Show("You must fill in all required fields");
            //    return;
            //}

            // Implements validation as a method
            if (validate())
            {

                //Figure out the selected MakeModel
                MakeModel selectedMakeModel = (MakeModel)cboModel.SelectedItem;

                //Create equipment object based on input data
                Equipment equipment = new Equipment();
                equipment.EquipmentTypeID = cboType.Text;
                equipment.Name = txtName.Text;
                equipment.MakeModelID = selectedMakeModel.MakeModelID;
                equipment.DatePurchased = DateTime.Parse(datePurchased.Text);
                equipment.DateLastRepaired = DateTime.Parse(dateRepaired.Text);
                equipment.PriceAtPurchase = Convert.ToDecimal(numPurchasePrice.Value);
                equipment.CurrentValue = Convert.ToDecimal(numCurrentValue.Value);
                equipment.WarrantyUntil = DateTime.Parse(dateWarrantyExpires.Text);
                equipment.EquipmentStatusID = cboStatus.Text;
                equipment.EquipmentDetails = txtDetails.Text;

                //Add equipment if add mode is selected
                if (_mode == DetailFormMode.Add)
                {
                    try
                    {
                        _equipmentManager.CreateEquipment(equipment);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error adding new equipment: " + ex);
                        this.DialogResult = false;
                    }
                }
                if (_mode == DetailFormMode.Edit)
                {
                    try
                    {
                        _equipmentManager.EditEquipment(_selectedEquipmentDetail.Equipment, equipment);
                        if (chkActive.IsChecked != _selectedEquipmentDetail.Equipment.Active && chkActive.IsChecked == false)
                        {
                            _equipmentManager.DeactivateEquipmentByID(_selectedEquipmentDetail.Equipment.EquipmentID);
                        }
                        if (chkActive.IsChecked != _selectedEquipmentDetail.Equipment.Active && chkActive.IsChecked == true)
                        {
                            _equipmentManager.ReactivateEquipmentByID(_selectedEquipmentDetail.Equipment.EquipmentID);
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error editing equipment: " + ex);
                        this.DialogResult = false;
                    }
                }
                this.DialogResult = false;
            }
        }

        /// <summary>
        /// Noah Davison
        /// Created: 2018/02/14
        /// 
        /// Fills in the Model combo box based on input from Make combo box
        /// </summary>
        private void cboMake_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            MakeModel selectedMake = (MakeModel)cboMake.SelectedItem;
            cboModel.IsEnabled = true;
            try
            {
                cboModel.ItemsSource = _makeModelManager.RetrieveMakeModelListByMake(selectedMake.Make);
            }
            catch
            {
                MessageBox.Show("Error: Failed to retrieve model list");
                this.DialogResult = false;
            }
        }


        /// <summary>
        /// Mike Mason
        /// Created 2018/04/26
        /// 
        /// Validates the user input
        /// </summary>
        /// <returns></returns>
        private bool validate()
        {
            if (!StringValidations.IsValidNamePropertyEmpty(txtName.Text))
            {
                MessageBox.Show("You must provide an equipment name.");
                return false;
            }

            if (!StringValidations.IsValidNamePropertyMaxSize(txtName.Text, 100))
            {
                MessageBox.Show("Your equipment name cannot be over 100 characters.");
                return false;
            }

            if (!StringValidations.IsValidNamePropertyEmpty(cboType.Text))
            {
                MessageBox.Show("You must select a type.");
                return false;
            }
            
            if (!StringValidations.IsValidNamePropertyEmpty(cboMake.Text))
            {
                MessageBox.Show("You must select a make.");
                return false;
            }

            if (!StringValidations.IsValidNamePropertyEmpty(cboModel.Text))
            {
                MessageBox.Show("You must select a model.");
                return false;
            }

            if (!StringValidations.IsValidNamePropertyEmpty(cboStatus.Text))
            {
                MessageBox.Show("You must select a status.");
                return false;
            }
            
            if (datePurchased.Value.ToString() == "")
            {
                MessageBox.Show("Please select purchase date.");
                datePurchased.Focus();
                return false;
            }

            if (dateRepaired.Value.ToString() == "")
            {
                MessageBox.Show("Please select repair date.");
                dateRepaired.Focus();
                return false;
            }        

            if (numPurchasePrice.Value.ToString() == "")
            {
                MessageBox.Show("Please enter purchase price.");
                numPurchasePrice.Focus();
                return false;
            }

            if (numCurrentValue.Value.ToString() == "")
            {
                MessageBox.Show("Please enter current value.");
                numCurrentValue.Focus();
                return false;
            }

            if (dateWarrantyExpires.Value.ToString() == "")
            {
                MessageBox.Show("Please select warranty expiration date.");
                dateWarrantyExpires.Focus();
                return false;
            }
            
            if (!StringValidations.IsValidNamePropertyEmpty(txtDetails.Text))
            {
                MessageBox.Show("You must provide details for this equipment.");
                return false;
            }

            if (!StringValidations.IsValidNamePropertyMaxSize(txtName.Text, 1000))
            {
                MessageBox.Show("Your equipment details cannot be over 1000 characters.");
                return false;
            }


            return true;
        }
    }
}
