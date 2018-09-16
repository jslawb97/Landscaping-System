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
    /// Interaction logic for frmAddEditEquipmentType.xaml
    /// </summary>
    public partial class frmAddEditEquipmentType : Window
    {
        private IEquipmentTypeManager _equipmentTypeManager;
        private IInspectionChecklistManager _inspectionChecklistManager;
        private IPrepChecklistManager _prepChecklistManager;
        private EquipmentTypeDetail _equipmentTypeDetail;
        private List<PrepChecklist> _prepLists;
        private List<InspectionChecklist> _inspectionLists;
        private DetailFormMode _mode;

        public frmAddEditEquipmentType()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Brady Feller
        /// Created 2018/02/05
        /// 
        /// Constructor that allows equipment types to viewed or edited
        /// </summary>
        /// <param name="etMgr"></param>
        /// <param name="etDetail"></param>
        /// <param name="mode"></param>
        public frmAddEditEquipmentType(IEquipmentTypeManager etMgr, IInspectionChecklistManager icMgr, IPrepChecklistManager pcMgr, EquipmentTypeDetail etDetail, DetailFormMode mode)
        {
            _equipmentTypeManager = etMgr;
            _inspectionChecklistManager = icMgr;
            _prepChecklistManager = pcMgr;
            _equipmentTypeDetail = etDetail;
            _mode = mode;

            InitializeComponent();
        }

        /// <summary>
        /// Brady Feller
        /// Created 2018/02/05
        /// 
        /// A constructor that allows the program to add an equipment type.
        /// </summary>
        /// <param name="etMgr"></param>
        public frmAddEditEquipmentType(IEquipmentTypeManager etMgr, IInspectionChecklistManager icMgr, IPrepChecklistManager pcMgr) // no detail object = add mode
        {
            _equipmentTypeManager = etMgr;
            _inspectionChecklistManager = icMgr;
            _prepChecklistManager = pcMgr;
            _mode = DetailFormMode.Add;

            InitializeComponent();
        }

        /// <summary>
        /// Brady Feller
        /// Created 2018/02/05
        /// 
        /// Populates the users controls with data.
        /// </summary>
        private void populateControls()
        {
            this.txtType.Text = _equipmentTypeDetail.EquipmentType.EquipmentTypeID;
        }

        /// <summary>
        /// Brady Feller
        /// Created 2018/02/05
        /// 
        /// Edit mode for the window
        /// </summary>
        private void setupEditMode()
        {
            this.btnAddEdit.Content = "Save";
            this.Title = "Edit an Equipment Type Record";
            populateControls();
        }

        /// <summary>
        /// Brady Feller
        /// Created 2018/02/05
        /// 
        /// Add mode for the window.
        /// </summary>
        private void setupAddMode()
        {
            this.Title = "Add a New Equipment Type Record";
        }

        /// <summary>
        /// Brady Feller
        /// Created 2018/02/06
        /// 
        /// Validates user input
        /// </summary>
        /// <param name="equipmentType"></param>
        /// <returns></returns>
        private bool captureEquipmentType(EquipmentType equipmentType)
        {

            if (this.txtType.Text == "" || this.txtType.Text == null)
            {
                MessageBox.Show("You must enter a name.");
                return false;
            }
            else
            {
                equipmentType.EquipmentTypeID = txtType.Text;
            }

            if (this.cboInspectionChecklist.SelectedItem == null)
            {
                MessageBox.Show("You must choose an InspectionChecklist.");
                return false;
            }
            else
            {
                equipmentType.InspectionChecklistID = ((InspectionChecklist)this.cboInspectionChecklist.SelectedItem).InspectionChecklistID;
            }
            if (this.cboPrepChecklist.SelectedItem == null)
            {
                MessageBox.Show("You must choose an PrepChecklist.");
                return false;
            }
            else
            {
                equipmentType.PrepChecklistID = ((PrepChecklist)this.cboPrepChecklist.SelectedItem).PrepChecklistID;
            }

            return true;
        }

        /// <summary>
        /// Brady Feller
        /// Created 2018/02/06
        /// 
        /// Adds or edits an equipment type
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAddEdit_Click(object sender, RoutedEventArgs e)
        {
            var equipmentType = new EquipmentType();

            switch (_mode)
            {
                case DetailFormMode.Add:
                    if (captureEquipmentType(equipmentType) == false)
                    {
                        return;
                    }
                    try
                    {
                        if (_equipmentTypeManager.CreateEquipmentType(equipmentType) >= 0)
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
                    if (captureEquipmentType(equipmentType) == false)
                    {
                        return;
                    }

                    equipmentType.EquipmentTypeID = _equipmentTypeDetail.EquipmentType.EquipmentTypeID;
                    var oldEquipmentType = _equipmentTypeDetail.EquipmentType;

                    try
                    {
                        if (_equipmentTypeManager.EditEquipmentType(oldEquipmentType, equipmentType))
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
        /// Created 2018/02/06
        /// 
        /// Closes the 'AddEdit' window
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }

        /// <summary>
        /// Brady Feller
        /// Created 2018/09/08
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                _inspectionLists = _inspectionChecklistManager.RetrieveInspectionChecklistItems();
                _prepLists = _prepChecklistManager.RetrievePrepChecklist();

                this.cboInspectionChecklist.ItemsSource = _inspectionLists;
                this.cboPrepChecklist.ItemsSource = _prepLists;
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
                default:
                    break;
            }
        }
    }
}
