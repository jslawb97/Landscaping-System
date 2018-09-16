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
    /// Interaction logic for frmAddEditMakeModel.xaml
    /// </summary>
    public partial class frmAddEditMakeModel : Window
    {
        private IMakeModelManager _makeModelManager;
        private IMaintenanceChecklistManager _maintenanceChecklistManager;
        private DetailFormMode _mode;
        private MakeModel _makeModel;
        private List<MaintenanceChecklist> _maintenanceChecklists;

        /// <summary>
        /// James McPherson
        /// Created 2018/02/04
        /// 
        /// Constructor for edit and view modes
        /// </summary>
        /// <param name="makeModelManager"></param>
        /// <param name="maintenanceChecklistManager"></param>
        /// <param name="makeModel"></param>
        /// <param name="mode"></param>
        public frmAddEditMakeModel(IMakeModelManager makeModelManager, IMaintenanceChecklistManager maintenanceChecklistManager
            , MakeModel makeModel, DetailFormMode mode)
        {
            _makeModelManager = makeModelManager;
            _maintenanceChecklistManager = maintenanceChecklistManager;
            _makeModel = makeModel;
            _mode = mode;
            InitializeComponent();
        }

        /// <summary>
        /// James McPherson
        /// Created 2018/02/04
        /// 
        /// Constructor for add mode
        /// </summary>
        /// <param name="makeModelManager"></param>
        /// <param name="maintenanceChecklistManager"></param>
        public frmAddEditMakeModel(IMakeModelManager makeModelManager, IMaintenanceChecklistManager maintenanceChecklistManager)
        {
            _makeModelManager = makeModelManager;
            _maintenanceChecklistManager = maintenanceChecklistManager;
            _mode = DetailFormMode.Add;
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                _maintenanceChecklists = _maintenanceChecklistManager.RetrieveMaintenanceChecklistList();

                cboMaintenanceChecklist.ItemsSource = _maintenanceChecklists;
            }
            catch
            {
                MessageBox.Show("One or more option lists were not found!");
            }

            switch(_mode)
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
        /// James McPherson
        /// Created 2018/02/04
        /// 
        /// Sets input values to the values of the item being edited/viewed
        /// </summary>
        private void populateControls()
        {
            this.txtMake.Text = _makeModel.Make;
            this.txtModel.Text = _makeModel.Model;
            foreach(var item in cboMaintenanceChecklist.Items)
            {
                if(((MaintenanceChecklist)item).MaintenanceChecklistID
                        == _makeModel.MaintenanceChecklistID)
                {
                    cboMaintenanceChecklist.SelectedItem = item;
                    break;
                }
            }
        }

        /// <summary>
        /// James McPherson
        /// Created 2018/02/04
        /// 
        /// Sets the readonly attributes of inputs to the specified value
        /// </summary>
        /// <param name="readOnly"></param>
        private void setReadOnlyInputs(bool readOnly)
        {
            txtMake.IsReadOnly = readOnly;
            txtModel.IsReadOnly = readOnly;
            cboMaintenanceChecklist.IsEnabled = !readOnly;
        }

        private void setupAddMode()
        {
            this.btnAddEdit.Content = "Add";
            this.Title = "Add a new MakeModel";
            setReadOnlyInputs(false);
        }

        private void setupEditMode()
        {
            this.btnAddEdit.Content = "Save";
            this.Title = "Edit an existing MakeModel";
            setReadOnlyInputs(false);
            populateControls();
        }

        private void setupViewMode()
        {
            this.btnAddEdit.Content = "Edit";
            this.Title = "View an existing MakeModel";
            setReadOnlyInputs(true);
            populateControls();
        }

        /// <summary>
        /// James McPherson
        /// Created 2018/02/04
        /// 
        /// If currently in add mode, check inputs for valid values and
        /// attempt to add new MakeModel
        /// If currently in edit mode, check inputs for valid values and
        /// attempt to save an existing MakeModel's changes
        /// If currently in view mode, switch to edit mode
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAddEdit_Click(object sender, RoutedEventArgs e)
        {
            if(_mode == DetailFormMode.View)
            {
                _mode = DetailFormMode.Edit;
                setupEditMode();
                return;
            }

            var makeModel = new MakeModel();

            switch(_mode)
            {
                case DetailFormMode.Add:
                    if(captureMakeModel(makeModel) == false)
                    {
                        return;
                    }

                    try
                    {
                        if(_makeModelManager.CreateMakeModel(makeModel))
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
                    if(captureMakeModel(makeModel) == false)
                    {
                        return;
                    }

                    makeModel.MakeModelID = _makeModel.MakeModelID;

                    try
                    {
                        if(0 != _makeModelManager.EditMakeModel(_makeModel, makeModel))
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
        /// James McPherson
        /// Created 2018/02/04
        /// 
        /// Check for valid input values and make changes to the makeModel object
        /// </summary>
        /// <param name="makeModel"></param>
        /// <returns>The validity of the input values</returns>
        private bool captureMakeModel(MakeModel makeModel)
        {
            // Null maintenance checklist ID is fine
            if(cboMaintenanceChecklist.SelectedItem == null)
            {
                makeModel.MaintenanceChecklistID = null;
            } else
            {
                makeModel.MaintenanceChecklistID
                    = ((MaintenanceChecklist)cboMaintenanceChecklist.SelectedItem).MaintenanceChecklistID;
            }
            if(txtMake.Text == "")
            {
                MessageBox.Show("You must enter a make.");
                return false;
            }
            else
            {
                makeModel.Make = txtMake.Text;
            }
            if(txtModel.Text == "")
            {
                MessageBox.Show("You must enter a model.");
                return false;
            }
            else
            {
                makeModel.Model = txtModel.Text;
            }

            return true;
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }
    }
}
