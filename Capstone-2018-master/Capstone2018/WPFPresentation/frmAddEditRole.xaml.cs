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
    /// Interaction logic for frmAddEditRole.xaml
    /// </summary>
    public partial class frmAddEditRole : Window
    {
        /// <summary>
        /// Marshall Sejkora
        /// Created: 2018/01/31
        /// 
        /// variables to hold RoleManager, Role, and AddEditMode
        /// </summary>
        private RoleManager _roleManager;
        private Role _role;
        private AddEditMode _mode;

        /// <summary>
        /// Marshall Sejkora
        /// Created: 2018/01/31
        /// 
        /// frm loader for edit and view
        /// </summary>
        public frmAddEditRole(RoleManager roleManager, Role role, AddEditMode mode)
        {
            _roleManager = roleManager;
            _role = role;
            _mode = mode;

            InitializeComponent();
        }

        /// <summary>
        /// Marshall Sejkora
        /// Created: 2018/01/31
        /// 
        /// frm loader for add
        /// </summary>
        public frmAddEditRole(RoleManager roleManager)
        {
            _roleManager = roleManager;
            _mode = AddEditMode.add;

            InitializeComponent();
        }

        /// <summary>
        /// Marshall Sejkora
        /// Created: 2018/01/31
        /// </summary>
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            switch (_mode)
            {
                case AddEditMode.view:
                    setupVeiwMode();
                    break;
                case AddEditMode.edit:
                    setupEditMode();
                    break;
                case AddEditMode.add:
                    setupAddMode();
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// Marshall Sejkora
        /// Created: 2018/01/31
        /// </summary>
        private void setupVeiwMode()
        {
            populateControls();
            setInputs();
            this.btnAddEdit.Visibility = Visibility.Hidden;
        }

        /// <summary>
        /// Marshall Sejkora
        /// Created: 2018/01/31
        /// 
        /// Setup for an edit form
        /// </summary>
        private void setupEditMode()
        {
            populateControls();
            setInputs();
        }

        /// <summary>
        /// Marshall Sejkora
        /// Created: 2018/01/31
        /// 
        /// Setup for an add form
        /// </summary>
        private void setupAddMode()
        {
            setInputs();
        }

        /// <summary>
        /// Marshall Sejkora
        /// Created: 2018/01/31
        /// 
        /// Sets the inputs for add/edit mode
        /// </summary>
        private void setInputs()
        {
            switch (_mode)
            {
                case AddEditMode.view:
                    this.txtName.IsReadOnly = true;
                    this.txtDescription.IsReadOnly = true;
                    break;
                case AddEditMode.edit:
                    this.txtName.IsReadOnly = true;
                    this.txtDescription.IsReadOnly = false;
                    this.btnAddEdit.Content = "Save";
                    this.lblHeader.Content = "Editing Role";
                    this.Title = "Edit Role";
                    break;
                case AddEditMode.add:
                    this.txtName.IsReadOnly = false;
                    this.txtDescription.IsReadOnly = false;
                    this.Title = "Add Role";
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// Marshall Sejkora
        /// Created: 2018/01/31
        /// 
        /// Populates the controls with data
        /// </summary>
        private void populateControls()
        {
            this.txtName.Text = _role.RoleID;
            this.txtDescription.Text = _role.Description;
        }

        /// <summary>
        /// Marshall Sejkora
        /// Created: 2018/01/31
        /// 
        /// Saves the added/edited data
        /// </summary>
        private void btnAddEdit_Click(object sender, RoutedEventArgs e)
        {
            if(this.txtName.Text == "")
            {
                MessageBox.Show("Invalid Name", "Role must have a Name!",
                    MessageBoxButton.OK, MessageBoxImage.Exclamation);
                return;
            }
            if(this.txtDescription.Text == "")
            {
                MessageBox.Show("Invalid Description", "Role must have a Description!",
                    MessageBoxButton.OK, MessageBoxImage.Exclamation);
                return;
            }

            var newRole = new Role()
            {
                RoleID = this.txtName.Text,
                Description = this.txtDescription.Text
            };

            var result = 0;

            try
            {
                if(_mode == AddEditMode.add)
                {
                    result = _roleManager.CreateRole(newRole);
                }
                else if(_mode == AddEditMode.edit)
                {
                    result = _roleManager.EditRole(_role, newRole);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Something went wrong!", ex.Message + ex.InnerException.Message,
                    MessageBoxButton.OK, MessageBoxImage.Exclamation);
                return;
            }
            this.DialogResult = true;
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }
    }
}
