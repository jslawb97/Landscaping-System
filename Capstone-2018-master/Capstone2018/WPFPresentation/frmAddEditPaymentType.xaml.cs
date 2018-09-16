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
    /// Interaction logic for frmAddEditPaymentType.xaml
    /// </summary>
    public partial class frmAddEditPaymentType : Window
    {
        private IPaymentTypeManager _paymentTypeManager;
        private DetailFormMode _mode;
        private PaymentType _paymentType;

        /// <summary>
        /// James McPherson
        /// Created 2018/02/20
        /// 
        /// Constructor for edit and view modes
        /// </summary>
        /// <param name="paymentTypeManager"></param>
        /// <param name="mode"></param>
        /// <param name="paymentType"></param>
        public frmAddEditPaymentType(IPaymentTypeManager paymentTypeManager, DetailFormMode mode,
            PaymentType paymentType)
        {
            _paymentTypeManager = paymentTypeManager;
            _mode = mode;
            _paymentType = paymentType;
            InitializeComponent();
        }

        /// <summary>
        /// James McPherson
        /// Created 2018/02/20
        /// 
        /// Constructor for add mode
        /// </summary>
        /// <param name="paymentTypeManager"></param>
        public frmAddEditPaymentType(IPaymentTypeManager paymentTypeManager)
        {
            _paymentTypeManager = paymentTypeManager;
            InitializeComponent();
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
                case DetailFormMode.View:
                    setupViewMode();
                    break;
            }
        }

        /// <summary>
        /// James McPherson
        /// Created 2018/02/20
        /// 
        /// Sets the readonly attributes of inputs to the specified value
        /// </summary>
        /// <param name="readOnly"></param>
        private void setReadOnlyInputs(bool readOnly)
        {
            if(_mode == DetailFormMode.Add)
            {
                txtPaymentTypeID.IsReadOnly = false;
            }
            else
            {
                txtPaymentTypeID.IsReadOnly = true;
            }
            txtDescription.IsReadOnly = readOnly;
        }

        private void setupAddMode()
        {
            btnAddEdit.Content = "Add";
            Title = "Add a new PaymentType";
            setReadOnlyInputs(false);

        }

        private void setupEditMode()
        {
            btnAddEdit.Content = "Save";
            Title = "Edit an existing PaymentType";
            setReadOnlyInputs(false);
            populateControls();
        }

        private void setupViewMode()
        {
            btnAddEdit.Content = "Edit";
            Title = "View an existing PaymentType";
            setReadOnlyInputs(true);
            populateControls();
        }

        /// <summary>
        /// James McPherson
        /// Created 2018/02/20
        /// 
        /// Sets input values to the values of the item being edited/viewed
        /// </summary>
        private void populateControls()
        {
            txtPaymentTypeID.Text = _paymentType.PaymentTypeID;
            txtDescription.Text = _paymentType.Description;
        }

        /// <summary>
        /// James McPherson
        /// Created 2018/02/20
        /// 
        /// If currently in add mode, check inputs for valid values and
        /// attempt to add new PaymentType
        /// If currently in edit mode, check inputs for valid values and
        /// attempt to save an existing PaymentType's changes
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

            var paymentType = new PaymentType();

            switch(_mode)
            {
                case DetailFormMode.Add:
                    PaymentType newPaymentType = new PaymentType()
                    {
                        PaymentTypeID = this.txtPaymentTypeID.Text,
                        Description = this.txtDescription.Text
                    };

                    try
                    {
                        int rowCount = _paymentTypeManager.CreatePaymentType(
                            newPaymentType.PaymentTypeID, newPaymentType.Description);
                        MessageBox.Show("New Payment Type Added.\nRows Affected: " + rowCount);
                        this.DialogResult = true;
                    }
                    catch (Exception ex)
                    {
                        var message = ex.Message;
                        MessageBox.Show(message, "Add Failed!",
                            MessageBoxButton.OK, MessageBoxImage.Exclamation);
                        return;
                    }
                    break;
                case DetailFormMode.Edit:
                    if(capturePaymentType(paymentType) == false)
                    {
                        return;
                    }
                    
                    try
                    {
                        if(0 != _paymentTypeManager.EditPaymentTypeByID(_paymentType, paymentType))
                        {
                            DialogResult = true;
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
        /// Created 2018/02/20
        /// </summary>
        /// <param name="paymentType"></param>
        /// <returns></returns>
        private bool capturePaymentType(PaymentType paymentType)
        {
            if(txtPaymentTypeID.Text == "")
            {
                MessageBox.Show("You must enter a name for the payment type.");
                return false;
            } else
            {
                paymentType.PaymentTypeID = txtPaymentTypeID.Text;
            }
            if (txtDescription.Text == "")
            {
                MessageBox.Show("You must enter a description.");
                return false;
            }
            else
            {
                paymentType.Description = txtDescription.Text;
            }

            return true;
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }
    }
}
