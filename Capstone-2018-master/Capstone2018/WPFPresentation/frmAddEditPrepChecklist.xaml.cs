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
    /// Interaction logic for frmAddEditPrepChecklist.xaml
    /// </summary>
    public partial class frmAddEditPrepChecklist : Window
    {

        private IPrepChecklistManager _prepChecklistManager;
        private PrepChecklist _prepChecklist;

        /// <summary>
        /// Amanda Tampir
        /// Created: 2018/02/15
        /// 
        /// Window Constructor for adding prep checklists
        /// </summary>
        /// <param name="_prepChecklistManager">Dependency for a IPrepChecklistManager</param>
        public frmAddEditPrepChecklist(IPrepChecklistManager prepChecklistManager)
        {
            this._prepChecklistManager = prepChecklistManager;

            InitializeComponent();
            setupAddForm();
        }



        /// <summary>
        /// Amanda Tampir
        /// Created: 2018/02/15
        /// 
        /// Window Constructor for adding prep checklists
        /// </summary>
        /// <param name="_prepChecklistManager">Dependency for a IPrepChecklistManager</param>
        public frmAddEditPrepChecklist(IPrepChecklistManager _prepChecklistManager, PrepChecklist prepChecklist)
        {
            this._prepChecklistManager = _prepChecklistManager;
            this._prepChecklist = prepChecklist;
            
            InitializeComponent();
            setupEditForm();
        }

        /// <summary>
        /// Amanda Tampir
        /// Created: 2018/02/15
        /// 
        /// Sets fields to Add functionality.
        /// </summary>
        private void setupAddForm()
        {
            lblHeader.Content = "Adding a new Prep Checklist";
            btnAddEdit.Content = "Add";
        }


        /// <summary>
        /// Amanda Tampir
        /// Created: 2018/02/15
        /// 
        /// Sets fields to Edit functionality.
        /// </summary>
        private void setupEditForm()
        {
            lblHeader.Content = "Editing Prep Checklist: " + _prepChecklist.PrepChecklistID;
            txtName.Text = _prepChecklist.Name;
            txtDescription.Text = _prepChecklist.Description;
            chkActive.IsChecked = _prepChecklist.Active;   
            btnAddEdit.Content = "Save";
        }


        private void btnAddEdit_Click(object sender, RoutedEventArgs e)
        {
            if (_prepChecklist == null)
            {
                performAdd();
            }
            else
            {
                performEdit();
            }
        }



        /// <summary>
        /// Amanda Tampir
        /// Created: 2018/02/15
        /// 
        /// Validates fields and submits Edit data
        /// </summary>
        private void performEdit()
        {
            if (validateFields())
            {
                var newPrepList = new PrepChecklist()
                {
                    Name =txtName.Text,
                    Description = txtDescription.Text,
                    Active = (bool)chkActive.IsChecked

                };

                try
                {
                    int result = _prepChecklistManager.EditPrepChecklist(_prepChecklist, newPrepList);  
                    if (result != 1)
                    {
                        throw new ApplicationException("Could not update Prep Checklist to the database");
                    }
                    MessageBox.Show(_prepChecklist.PrepChecklistID + " was successfully edited!");
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
                    //have to display the error
                    MessageBox.Show(message, "Edit Failed", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                }
            }
        }



        /// <summary>
        /// Amanda Tampir
        /// Created: 2018/02/15
        /// 
        /// Validates fields and submits Add data
        /// </summary>
        private void performAdd()
        {
            if (validateFields())
            {
                var prepChecklist = new PrepChecklist()
                {
                    Name = txtName.Text,
                    Description = txtDescription.Text,
                    Active = (bool)chkActive.IsChecked
                };

                try
                {

                    int result = _prepChecklistManager.AddPrepChecklist(prepChecklist);
                    MessageBox.Show(result + " was successfully added!");
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
                    //have to display the error
                    MessageBox.Show(message, "Add Failed", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                }
            }
        }



        /// <summary>  
        /// Amanda Tampir
        /// Created: 2018/02/15
        /// 
        /// Validates all the fields with proper error messages when invalid
        /// </summary>
        /// <returns>True if all fields are valid, false otherwise</returns>
        private bool validateFields()
        {
            if (!StringValidations.IsValidNamePropertyMaxSize(txtDescription.Text, 1000))
            {
                MessageBox.Show("Description cannot be over 1000 characters!");
                return false;
            }
            else if (!StringValidations.IsValidNamePropertyEmpty(txtDescription.Text))
            {
                MessageBox.Show("Description  cannot be empty!");
                return false;
            }

            if (!StringValidations.IsValidNamePropertyMaxSize(txtName.Text, 100))
            {
                MessageBox.Show("Name cannot be over 100 characters!");
                return false;
            }
            else if (!StringValidations.IsValidNamePropertyEmpty(txtName.Text))
            {
                MessageBox.Show("Name  cannot be empty!");
                return false;
            }

            return true;
        }


        /// <summary>
        /// Amanda Tampir
        /// Created: 2018/02/15
        /// 
        /// Button click event to prompt user to exit the window
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
    }
}
