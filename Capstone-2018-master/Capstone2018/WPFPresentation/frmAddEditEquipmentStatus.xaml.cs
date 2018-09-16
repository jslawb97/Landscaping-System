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
    /// Jacob Slaubaugh
    /// Created 2018/02/18
    /// 
    /// Interaction logic for frmAddEditEquipmentStatus.xaml
    /// </summary>
    public partial class frmAddEditEquipmentStatus : Window
    {
        private IEquipmentStatusManager _equipmentStatusManager;
        private EquipmentStatus _equipmentStatus;
        private DetailFormMode _type;

        // Edit an existing status
        public frmAddEditEquipmentStatus(IEquipmentStatusManager eqstMgr, EquipmentStatus equipmentStatus, DetailFormMode type)
        {
            _equipmentStatusManager = eqstMgr;
            _equipmentStatus = equipmentStatus;
            _type = type;
            InitializeComponent();
        }

        // Adding a new EquipmentStatus
        public frmAddEditEquipmentStatus(IEquipmentStatusManager eqstMgr)
        {
            _equipmentStatusManager = eqstMgr;
            _type = DetailFormMode.Add;
            InitializeComponent();
        }

        private void Window_Loaded_1(object sender, RoutedEventArgs e)
        {
            switch (_type)
            {
                case DetailFormMode.Add:
                    setupAddForm();
                    break;
                case DetailFormMode.Edit:
                    setupEditForm();
                    break;
                default:
                    break;
            }
        }

        private void setupEditForm()
        {
            this.Title = "Edit Status";
            lblHeader.Content = "Edit an Equipment Status";
            btnAddEdit.Content = "Save";
            this.txtStatus.Text = _equipmentStatus.EquipmentStatusID.ToString();
        }

        private void setupAddForm()
        {
            this.Title = "Add Status";
        }

        private void btnAddEdit_Click(object sender, RoutedEventArgs e)
        {
            var equipmentStatus = new EquipmentStatus();
            switch (_type)
            {
                case DetailFormMode.Add:
                    if (captureStatus(equipmentStatus) == false)
                    {
                        return;
                    }
                    try
                    {
                        if (_equipmentStatusManager.AddEquipmentStatus(equipmentStatus))
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
                    if (captureStatus(equipmentStatus) == false)
                    {
                        return;
                    }
                    var oldEquipmentStatus = _equipmentStatus;

                    try
                    {
                        if (1 == _equipmentStatusManager.EditEquipmentStatus(equipmentStatus, oldEquipmentStatus))
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

        private bool captureStatus(EquipmentStatus equipmentStatus)
        {
            if (this.txtStatus.Text == "")
            {
                MessageBox.Show("You must enter a status to add.");
                return false;
            }
            else
            {
                equipmentStatus.EquipmentStatusID = txtStatus.Text;
            }
            return true;
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }
    }
}
