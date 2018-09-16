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
    /// Interaction logic for frmAddEditCertification.xaml
    /// </summary>
    public partial class frmAddEditCertification : Window
    {
        private ICertificationManager _certificationManager = new CertificationManager();
        private Certification _cert;
        private DetailFormMode _mode;

        public frmAddEditCertification()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Weston Olund
        /// Created on 2018/03/01
        /// 
        /// Constructor for add mode
        /// </summary>
        public frmAddEditCertification(ICertificationManager certificationManager)
        {
            _certificationManager = certificationManager;
            _mode = DetailFormMode.Add;
            InitializeComponent();
        }

        /// <summary>
        /// Weston Olund
        /// Created on 2018/03/07
        /// 
        /// Constructor for edit mode
        /// </summary>
        /// <param name="certificationManager"></param>
        /// <param name="cert"></param>
        public frmAddEditCertification(ICertificationManager certificationManager, Certification cert, DetailFormMode mode)
        {
            _certificationManager = certificationManager;
            _cert = cert;
            _mode = mode;
            InitializeComponent();
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }

        /// <summary>
        /// Weston Olund
        /// created on 2018/03/01
        /// 
        /// Method to handle add/edit button being clicked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAddEdit_Click(object sender, RoutedEventArgs e)
        {
            if (_mode == DetailFormMode.Add)
            {
                addCertification();
            }
            if (_mode == DetailFormMode.Edit)
            {
                editCertification();
            }

        }

        /// <summary>
        /// Weston Olund
        /// Created on 2018/03/07
        /// 
        /// Method to handle edit when in edit mode
        /// </summary>
        private void editCertification()
        {
            var cert = new Certification();
            var oldCert = _cert;
            cert.CertificationID = _cert.CertificationID;
            cert.CertificationName = this.txtName.Text;
            cert.CertificationDescription = this.txtDescription.Text;
            if (chkActive.IsChecked == true)
            {
                cert.Active = true;
            }

            try
            {
                if (_certificationManager.EditCertification(oldCert, cert))
                {
                    MessageBox.Show("Certification updated");
                    this.DialogResult = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Update failed. " + ex.Message);
            }
        }

        /// <summary>
        /// Weston Olund
        /// Created on 2018/03/07
        /// 
        /// Method to handle adding when in add mode
        /// </summary>
        private void addCertification()
        {
            if (this.txtName.Text == null || this.txtName.Text.Length <= 0)
            {
                MessageBox.Show("Please enter a name");
                return;
            }
            if (this.txtDescription.Text == null || this.txtDescription.Text.Length <= 0)
            {
                MessageBox.Show("Please enter a description");
                return;
            }
            Certification cert = new Certification();
            cert.CertificationName = this.txtName.Text;
            cert.CertificationDescription = this.txtDescription.Text;
            if (chkActive.IsChecked == true)
            {
                cert.Active = true;
            }
            try
            {
                if (_certificationManager.CreateCertification(cert))
                {
                    this.DialogResult = true;
                }
                else
                {
                    this.DialogResult = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("There was an error adding the certification request.", ex.Message);
                this.DialogResult = false;
            }
        }

        /// <summary>
        /// Weston Olund
        /// Created on 2018/03/07
        /// 
        /// Method to setup form for edit mode
        /// </summary>
        /// <remarks>
        /// Noah Davison
        /// Modified 2018/04/27
        /// 
        /// Fixed title
        /// </remarks>
        private void setupEditMode()
        {
            this.Title = "Edit Certification";
            this.lblHeader.Content = "Edit a Certification";
            this.btnAddEdit.Content = "Save";
            populateControls();
        }

        /// <summary>
        /// Noah Davison
        /// Created 2018/04/27
        /// 
        /// Setup for add mode
        /// </summary>
        private void setupAddMode()
        {
            chkActive.IsChecked = true;
            chkActive.IsEnabled = false;
            this.Title = "Add Certification";
        }

        /// <summary>
        /// Weston Olund
        /// Created on 2018/03/07
        /// 
        /// Method to populate controls when in edit mode
        /// </summary>
        private void populateControls()
        {
            this.txtName.Text = _cert.CertificationName;
            this.txtDescription.Text = _cert.CertificationDescription;
            if (_cert.Active)
            {
                this.chkActive.IsChecked = true;
            }

        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
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
