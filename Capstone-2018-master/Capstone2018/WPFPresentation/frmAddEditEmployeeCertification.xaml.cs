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
    /// Interaction logic for frmAddEditEmployeeCertification.xaml
    /// </summary>
    public partial class frmAddEditEmployeeCertification : Window
    {
        private IEmployeeCertificationManager _employeeCertificationManager;
        private ICertificationManager _certificationManager;
        private IEmployeeManager _employeeManager;
        private List<Certification> _certificationList;
        private List<Employee> _employeeList;
        private EmployeeCertificationDetail _employeeCertificationDetail;
        private DetailFormMode _mode;

        public frmAddEditEmployeeCertification()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Brady Feller
        /// Created 2018/03/22
        /// 
        /// Constructor for Retrieve/Edit mode
        /// </summary>
        /// <param name="ecMgr"></param>
        /// <param name="ceMgr"></param>
        /// <param name="emMgr"></param>
        /// <param name="ecDetail"></param>
        /// <param name="mode"></param>
        public frmAddEditEmployeeCertification(IEmployeeCertificationManager ecMgr, ICertificationManager ceMgr, IEmployeeManager emMgr, EmployeeCertificationDetail ecDetail, DetailFormMode mode)
        {
            _employeeCertificationManager = ecMgr;
            _certificationManager = ceMgr;
            _employeeManager = emMgr;
            _employeeCertificationDetail = ecDetail;
            _mode = mode;

            InitializeComponent();
        }

        /// <summary>
        /// Brady Feller
        /// Created 2018/03/22
        /// 
        /// Constructor for 'Add' mode
        /// </summary>
        /// <param name="ecMgr"></param>
        /// <param name="ceMgr"></param>
        /// <param name="emMgr"></param>
        public frmAddEditEmployeeCertification(IEmployeeCertificationManager ecMgr, ICertificationManager ceMgr, IEmployeeManager emMgr)
        {
            _employeeCertificationManager = ecMgr;
            _certificationManager = ceMgr;
            _employeeManager = emMgr;
            _mode = DetailFormMode.Add;

            InitializeComponent();
        }

        /// <summary>
        /// Brady Feller
        /// Created 2018/03/22
        /// 
        /// Populates the users controls with data.
        /// </summary>
        private void populateControls()
        {
            this.dateExpires.Value = _employeeCertificationDetail.EndDate;
            foreach (var cert in cboCertification.Items)
            {
                if(((Certification)cert).CertificationID
                    == _employeeCertificationDetail.Certification.CertificationID)
                {
                    cboCertification.SelectedItem = cert;
                }
            }
            foreach (var emp in cboEmployee.Items)
            {
                if (((Employee)emp).EmployeeID
                    == _employeeCertificationDetail.Employee.EmployeeID)
                {
                    cboEmployee.SelectedItem = emp;
                }
            }
            chkActive.IsChecked = _employeeCertificationDetail.Active;
        }

        /// <summary>
        /// Brady Feller
        /// Created 2018/03/22
        /// 
        /// Edit mode for the window
        /// </summary>
        private void setupEditMode()
        {
            this.btnAddEdit.Content = "Save";
            this.Title = "Edit an Employee Certification Record";
            lblHeader.Content = "Editing an Employee Certification Record";
            populateControls();
        }

        /// <summary>
        /// Brady Feller
        /// Created 2018/03/22
        /// 
        /// Add mode for the window.
        /// </summary>
        private void setupAddMode()
        {
            this.Title = "Add a New Employee Certification Record";
            lblHeader.Content = "Adding an Employee Certification Record";
            chkActive.IsChecked = true;
            chkActive.IsEnabled = false;
        }

        /// <summary>
        /// Brady Feller
        /// Created 2018/03/22
        /// 
        /// Validates user input
        /// </summary>
        /// <param name="equipmentType"></param>
        /// <returns></returns>
        private bool captureEmployeeCertification(EmployeeCertification employeeCertification)
        {
            if (this.cboEmployee.SelectedItem == null)
            {
                MessageBox.Show("You must choose an Employee.");
                return false;
            }
            else
            {
                employeeCertification.EmployeeID = ((Employee)this.cboEmployee.SelectedItem).EmployeeID;
            }
            if (this.cboCertification.SelectedItem == null)
            {
                MessageBox.Show("You must enter a certification.");
                return false;
            }
            else
            {
                employeeCertification.CertificationID = ((Certification)this.cboCertification.SelectedItem).CertificationID;
            }
            if (this.dateExpires.Value == null)
            {
                MessageBox.Show("You must choose an End Date for the Certification.");
                return false;
            }
            else
            {
                employeeCertification.EndDate = Convert.ToDateTime(dateExpires.Value);
            }
            employeeCertification.Active = (bool) chkActive.IsChecked;

            return true;
        }

        /// <summary>
        /// Brady Feller
        /// Created 2018/03/22
        /// 
        /// Cancels out of the Add/Edit window
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }

        /// <summary>
        /// Brady Feller
        /// Created 2018/03/22
        /// 
        /// Adds or Edits a record depending on the users choice
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAddEdit_Click(object sender, RoutedEventArgs e)
        {
            var employeeCertification = new EmployeeCertification();

            switch (_mode)
            {
                case DetailFormMode.Add:
                    if (captureEmployeeCertification(employeeCertification) == false)
                    {
                        return;
                    }
                    try
                    {
                        if (_employeeCertificationManager.CreateEmployeeCertification(employeeCertification) >= 0)
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
                    if (captureEmployeeCertification(employeeCertification) == false)
                    {
                        return;
                    }
                    
                    var oldEmployeeCertification = _employeeCertificationDetail.EmployeeCertification;
                    oldEmployeeCertification.Active = _employeeCertificationDetail.Active;

                    try
                    {
                        if (_employeeCertificationManager.EditEmployeeCertification(oldEmployeeCertification, employeeCertification))
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
        /// Created 2018/03/22
        /// 
        /// Loads when the window is opened
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                _certificationList = _certificationManager.RetrieveCertificationList();
                _employeeList = _employeeManager.RetrieveEmployeeListByActive();

                this.cboCertification.ItemsSource = _certificationList;
                this.cboEmployee.ItemsSource = _employeeList;
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
