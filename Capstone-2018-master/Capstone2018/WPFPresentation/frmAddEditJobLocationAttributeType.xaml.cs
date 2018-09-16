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
    /// Interaction logic for frmAddEditJobLocationAttributeType.xaml
    /// </summary>
    public partial class frmAddEditJobLocationAttributeType : Window
    {
        private IJobLocationAttributeTypeManager _jobLocationAttributeTypeManager;
        private JobLocationAttributeType _jobLocationAttributeType;
        private DetailFormMode _mode;

        /// <summary>
        /// Brady Feller
        /// Created 2018/03/19
        /// 
        /// Default constructor
        /// </summary>
        public frmAddEditJobLocationAttributeType()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Brady Feller
        /// Created 2018/03/19
        /// 
        /// 'Edit/Retrieve' Constructor
        /// </summary>
        /// <param name="jlatMgr"></param>
        /// <param name="mode"></param>
        public frmAddEditJobLocationAttributeType(IJobLocationAttributeTypeManager jlatMgr, JobLocationAttributeType jlat, DetailFormMode mode)
        {
            _jobLocationAttributeTypeManager = jlatMgr;
            _jobLocationAttributeType = jlat;
            _mode = mode;

            InitializeComponent();
        }

        /// <summary>
        /// Brady Feller
        /// Created 2018/03/19
        /// 
        /// 'Add mode' constructor
        /// </summary>
        /// <param name="jlatMgr"></param>
        public frmAddEditJobLocationAttributeType(IJobLocationAttributeTypeManager jlatMgr)
        {
            _jobLocationAttributeTypeManager = jlatMgr;
            _mode = DetailFormMode.Add;

            InitializeComponent();
        }

        /// <summary>
        /// Brady Feller
        /// Created 2018/03/19
        /// 
        /// Populates the users controls with data.
        /// </summary>
        private void populateControls()
        {
            this.txtID.Text = _jobLocationAttributeType.JobLocationAttributeTypeID.ToString();
        }

        /// <summary>
        /// Brady Feller
        /// Created 2018/03/19
        /// 
        /// Edit mode for the window
        /// </summary>
        private void setupEditMode()
        {
            this.btnAddEdit.Content = "Save";
            this.Title = "Edit a Job Location Attribute Type";
            populateControls();
        }

        /// <summary>
        /// Brady Feller
        /// Created 2018/03/19
        /// 
        /// Add mode for the window
        /// </summary>
        private void setupAddMode()
        {
            this.Title = "Add a a Job Location Attribute Type";
        }

        /// <summary>
        /// Brady Feller
        /// Created 2018/03/19
        /// 
        /// Displays message to user if information is filled in wrong on the window
        /// </summary>
        /// <param name="jobLocationAttributeType"></param>
        /// <returns></returns>
        private bool captureJobLocationAttributeType(JobLocationAttributeType jobLocationAttributeType)
        {

            if (this.txtID.Text == "" || this.txtID.Text == null)
            {
                MessageBox.Show("You must enter an ID.");
                return false;
            }
            else
            {
                jobLocationAttributeType.JobLocationAttributeTypeID = txtID.Text;
            }

            return true;
        }

        /// <summary>
        /// Brady Feller
        /// Created 2018/03/19
        /// 
        /// Adds/Edits a record depending on what the user wants
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAddEdit_Click(object sender, RoutedEventArgs e)
        {
            var jobLocationAttributeType = new JobLocationAttributeType();


            switch (_mode)
            {
                case DetailFormMode.Add:
                    if (captureJobLocationAttributeType(jobLocationAttributeType) == false)
                    {
                        return;
                    }
                    try
                    {
                        if (_jobLocationAttributeTypeManager.CreateJobLocationAttributeType(jobLocationAttributeType) >= 0)
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
                    if (captureJobLocationAttributeType(jobLocationAttributeType) == false)
                    {
                        return;
                    }

                    //jobLocationAttributeType.JobLocationAttributeTypeID = _jobLocationAttributeType.JobLocationAttributeTypeID;
                    var oldJobLocationAttributeType = _jobLocationAttributeType;

                    try
                    {
                        if (_jobLocationAttributeTypeManager.EditJobLocationAttributeType(oldJobLocationAttributeType, jobLocationAttributeType) > 0)
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
        /// Created 2018/03/19
        /// 
        /// Closes out of the form
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }

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
                default:
                    break;
            }
        }
    }
}
