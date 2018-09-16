using DataObjects;
using Logic;
using System.Windows;

namespace WPFPresentation
{

    /// <summary>
    /// Interaction logic for frmAddEditChecklist.xaml
    /// </summary>
    public partial class frmAddEditChecklist : Window
    {
        public frmAddEditChecklist()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Displays a dialog with the fields required to create a new Inspection Checklist
        /// </summary>
        /// <param name="inspectionChecklistManager">The Inspection Checklist Manager</param>
        /// <remarks>
        /// Zach Murphy
        /// Updated 2018/02/1
        /// </remarks>
        /// <remarks>QA Jayden T 4/6/18 Added field sets name to the description</remarks>
        public bool? ShowAddDialog(IInspectionChecklistManager inspectionChecklistManager)
        {
            this.lblHeader.Content = "Adding an Inspection Checklist";

            this.btnAddEdit.Click += (object sender, RoutedEventArgs e) =>
            {
                var newItem = new InspectionChecklist
                {
                    Name = this.txtName.Text,
                    Description = this.txtDescription.Text,
                    Active = this.chkActive.IsChecked.Value
                };

                inspectionChecklistManager.AddInspectionChecklist(newItem);
                
                Close();
            };
            
            return ShowDialog();
        }

        /// <summary>
        /// Displays a dialog with the fields required to edit an existing inspection Checklist
        /// </summary>
        /// <param name="inspectionChecklistManager">The Inspection Checklist Manager</param>
        /// <param name="inspectionChecklist">The Inspection Checklist Manager</param>
        /// ///
        /// <returns>A System.Nullable`1 value of type System.Boolean that specifies whether the activity</returns>
        /// <remarks>
        /// Zach Murphy
        /// Updated 2018/02/1
        /// </remarks>
        public bool? ShowEditDialog(IInspectionChecklistManager inspectionChecklistManager,
            InspectionChecklist inspectionChecklist)
        {
            this.lblHeader.Content = "Editing an Inspection Checklist";
            this.btnAddEdit.Content = "Edit";
            this.txtName.Text = inspectionChecklist.Name;
            this.txtDescription.Text = inspectionChecklist.Description;
            this.chkActive.IsChecked = inspectionChecklist.Active;

            this.btnAddEdit.Click += (object sender, RoutedEventArgs e) =>
            {
                var newItem = new InspectionChecklist
                {
                    Name = this.txtName.Text,
                    Description = this.txtDescription.Text,
                    Active = this.chkActive.IsChecked.Value
                };

                if (inspectionChecklist.Description.Equals(newItem.Description) && inspectionChecklist.Active && !newItem.Active)
                {
                    inspectionChecklistManager.DeactivateInspectionChecklist(inspectionChecklist.InspectionChecklistID);
                }
                else
                {
                    inspectionChecklistManager.EditInspectionChecklist(inspectionChecklist, newItem);
                }

                DialogResult = true;
                Close();
            };

            return ShowDialog();
        }

        /// <summary>
        /// Displays a dialog with the fields required to create a new MaintenanceChecklist
        /// </summary>
        /// <param name="maintenanceChecklistManager">The MaintenanceChecklist Manager</param>
        /// <remarks>
        /// Zach Murphy
        /// Updated 2018/02/2
        /// </remarks>
        public bool? ShowAddDialog(IMaintenanceChecklistManager maintenanceChecklistManager)
        {
            this.lblHeader.Content = "Adding a Maintenance Checklist";

            this.btnAddEdit.Click += (object sender, RoutedEventArgs e) =>
            {
                var newItem = new MaintenanceChecklist
                {
                    Name = this.txtName.Text,
                    Description = this.txtDescription.Text,
                    Active = this.chkActive.IsChecked.Value
                };

                maintenanceChecklistManager.AddMaintenanceChecklist(newItem);

                DialogResult = true;
                Close();
            };
            
            return ShowDialog();
        }

        /// <summary>
        /// Displays a dialog with the fields required to edit an existing MaintenanceChecklist
        /// </summary>
        /// <param name="maintenanceChecklistManager">The MaintenanceChecklist Manager</param>
        /// <param name="maintenanceChecklist">The MaintenanceChecklist Manager</param>
        /// <returns>A System.Nullable`1 value of type System.Boolean that specifies whether the activity
        //     was accepted (true) or canceled (false). The return value is the value of the
        //     System.Windows.Window.DialogResult property before a window closes.</returns>
        /// <remarks>
        /// Zach Murphy
        /// Updated 2018/02/2
        /// </remarks>
        public bool? ShowEditDialog(IMaintenanceChecklistManager maintenanceChecklistManager, MaintenanceChecklist maintenanceChecklist)
        {
            this.lblHeader.Content = "Editing a Maintenance Checklist";
            this.txtName.Text = maintenanceChecklist.Name;
            this.txtDescription.Text = maintenanceChecklist.Description;
            this.chkActive.IsChecked = maintenanceChecklist.Active;

            this.btnAddEdit.Click += (object sender, RoutedEventArgs e) =>
            {
                var newItem = new MaintenanceChecklist
                {
                    Name = this.txtName.Text,
                    Description = this.txtDescription.Text,
                    Active = this.chkActive.IsChecked.Value
                };

                maintenanceChecklistManager.EditMaintenanceChecklist(maintenanceChecklist, newItem);

                DialogResult = true;
                Close();
            };

            return ShowDialog();
        }

        private bool validateFields()
        {
            if (StringValidations.IsValidDescriptionProperty(this.txtDescription.Text)) return true;
            
            MessageBox.Show("The desciption value must be between 1 and 1000 characters long.");
            
            return false;
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
