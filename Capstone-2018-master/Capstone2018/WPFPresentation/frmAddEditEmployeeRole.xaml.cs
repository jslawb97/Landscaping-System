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
    /// Interaction logic for frmAddEditEmployeeRole.xaml
    /// </summary>
    /// <remarks>QA Jayden T 4/20/18</remarks>
    public partial class frmAddEditEmployeeRole : Window
    {
        private IRoleManager _roleManager = new RoleManager();
        private IEmployeeManager _employeeManager = new EmployeeManager();
        private EmployeeRoleDetail _employeeRoleDetail;
        private IEmployeeRoleManager _employeeRoleManager;

        public frmAddEditEmployeeRole(IEmployeeRoleManager employeeRoleManager)
        {
            _employeeRoleManager = employeeRoleManager;
            InitializeComponent();
            setUpAddForm();
        }

        public frmAddEditEmployeeRole(IEmployeeRoleManager employeeRoleManager, EmployeeRoleDetail employeeRoleDetail)
        {
            _employeeRoleManager = employeeRoleManager;
            _employeeRoleDetail = employeeRoleDetail;
            InitializeComponent();
            setupEditForm();
        }

        /// <summary>
        /// John Miller
        /// Created 2018/04/01
        /// 
        /// Populates fields to reflect the edit functionality
        /// </summary>
        private void setupEditForm()
        {
            lblHeader.Content = "Editing Employee Role " + _employeeRoleDetail.EmployeeRole.RoleID;
            btnAddEdit.Content = "Save";

            setEditComboBox();
            chkActive.IsChecked = _employeeRoleDetail.EmployeeRole.Active;
            cboEmployee.Focus();
        }

        private void setEditComboBox()
        {
            List<Employee> employeeList = new List<Employee>();
            List<Role> roleList = new List<Role>();
            List<EmployeeRoleDetail> employeeRoleList = new List<EmployeeRoleDetail>();
            try
            {
                employeeList = _employeeManager.RetrieveEmployeeListByActive().OrderBy(l => l.LastName).Distinct().ToList();
                this.cboEmployee.ItemsSource = employeeList;
                this.cboEmployee.DisplayMemberPath = "LastName";
                this.cboEmployee.SelectedItem = employeeList.Find(e => e.EmployeeID == _employeeRoleDetail.Employee.EmployeeID);
                this.cboEmployee.IsEnabled = false;

                roleList = _roleManager.RetrieveRolesList().OrderBy(e => e.RoleID).Distinct().ToList();

                employeeRoleList = _employeeRoleManager.RetrieveEmployeeRoleDetailList().OrderBy(e => e.EmployeeRole.RoleID).Distinct().ToList();

                // Loop through lists of roles to remove any roles an employee has already been assigned. 
                // Prevents FK constraint exception.

                for (int j = 0; j < roleList.Count; j++)
                {
                    for (int i = 0; i < employeeRoleList.Count; i++)
                    {
                        if (roleList[j].RoleID == employeeRoleList[i].EmployeeRole.RoleID && _employeeRoleDetail.Employee.EmployeeID == employeeRoleList[i].Employee.EmployeeID
                            && roleList[j].RoleID != _employeeRoleDetail.EmployeeRole.RoleID)
                        {
                            roleList.Remove(roleList.Find(r => r.RoleID == employeeRoleList[i].EmployeeRole.RoleID));
                            j--;
                        }
                    }
                }

                this.cboRole.ItemsSource = roleList;
                this.cboRole.SelectedItem = roleList.Find(r => r.RoleID == _employeeRoleDetail.EmployeeRole.RoleID);
                this.cboRole.DisplayMemberPath = "RoleID";
            }
            catch (Exception)
            {
                MessageBox.Show("There was an error retrieving a list of active Employees.");
            }
        }

        /// <summary>
        /// John Miller
        /// Created 2018/04/01
        /// 
        /// Populates fields to reflect the Add functionality
        private void setUpAddForm()
        {
            lblHeader.Content = "Adding a New Employee Role";
            btnAddEdit.Content = "Add";
            chkActive.IsChecked = true;
            chkActive.IsEnabled = false;

            setAddComboBox();
            cboEmployee.Focus();
        }


        private void setAddComboBox()
        {
            List<Employee> employeeList = new List<Employee>();
            List<Role> roleList = new List<Role>();
            try
            {
                employeeList = _employeeManager.RetrieveEmployeeListByActive().OrderBy(l => l.LastName).Distinct().ToList();
                this.cboEmployee.ItemsSource = employeeList;
                this.cboEmployee.DisplayMemberPath = "LastName";
                this.cboEmployee.SelectedIndex = 0;

                roleList = _roleManager.RetrieveRolesList().OrderBy(l => l.RoleID).ToList();
                this.cboRole.ItemsSource = roleList;
                this.cboRole.DisplayMemberPath = "RoleID";
                this.cboRole.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show("There was an issue retrieving a list of active Employees." + ex);
            }
        }

        /// <summary>
        /// John Miller
        /// Created 2018/04/01
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

        /// <summary>
        /// John Miller
        /// Created 2018/04/01
        /// 
        /// Form Add or Edit Event called by user clicking Add/Edit button.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAddEdit_Click(object sender, RoutedEventArgs e)
        {
            if (_employeeRoleDetail == null)
            {
                performAdd();
            }
            else
            {
                performEdit();
            }

        }

        private void performEdit()
        {
            var oldEmployeeRoleDetail = _employeeRoleDetail;
            var newEmployeeRole = new EmployeeRole()
            {
                RoleID = ((Role) this.cboRole.SelectedItem).RoleID,
                Active = (bool)this.chkActive.IsChecked
            };
            var newEmployeeRoleDetail = new EmployeeRoleDetail()
            {
                Employee = (Employee)this.cboEmployee.SelectedItem,
                EmployeeRole = newEmployeeRole
            };
            try
            {
                var result = _employeeRoleManager.EditEmployeeRoleDetail(oldEmployeeRoleDetail, newEmployeeRoleDetail);
                MessageBox.Show(_employeeRoleDetail.Employee.FullName + "'s role was successfully edited!");
                this.DialogResult = true;
                this.Close();
            }
            catch (Exception)
            {
                MessageBox.Show("There was an error Editing the Employee Role.", "Edit Failed");
            }
        }

        private void performAdd()
        {
            if (null == this.cboEmployee.SelectedItem)
            {
                MessageBox.Show("Please make a selection.");
                return;
            }
            if (null == this.cboRole.SelectedItem)
            {
                MessageBox.Show("Please make a selection.");
                return;
            }

            Employee employee = (Employee)this.cboEmployee.SelectedItem;
            Role role = (Role)this.cboRole.SelectedItem;

            try
            {
                if (_employeeRoleManager.AddEmployeeRoleDetail(employee, role) == 1)
                {
                    this.DialogResult = true;
                }
                else
                {
                    this.DialogResult = false;
                }
            }
            catch (Exception)
            {
                MessageBox.Show("There was an error adding the EmployeeRole.\nEmployees cannot be assigned duplicate roles.", "Add Employee Role Failed");
                this.DialogResult = false;
            }
        }
    }
}