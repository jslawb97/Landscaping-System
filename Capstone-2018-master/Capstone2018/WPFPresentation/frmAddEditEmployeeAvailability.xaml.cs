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
    /// Interaction logic for frmAddEditEmployeeAvailability.xaml
    /// </summary>
    public partial class frmAddEditEmployeeAvailability : Window
    {
        private Employee _employee;
        private IAvailabilityManager _availabilityManager;

        public frmAddEditEmployeeAvailability()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Zachary Hall
        /// Created 2018/02/22
        /// 
        /// Full constructor
        /// </summary>
        /// <param name="employee"></param>
        /// <param name="availabilityManager"></param>
        public frmAddEditEmployeeAvailability(Employee employee, IAvailabilityManager availabilityManager)
        {
            InitializeComponent();
            _employee = employee;
            _availabilityManager = availabilityManager;
            populateControls();
        }
        
        /// <summary>
        /// Zachary Hall
        /// Created 2018/03/10
        /// 
        /// Rounds the seconds down, as DateTimePicker unwarrantly sets the seconds, for a DateTime object
        /// </summary>
        /// <param name="time"></param>
        /// <returns></returns>
        private DateTime roundSecondsDown(DateTime time)
        {
            if (time != null)
            {
                DateTime rounded = time.AddSeconds((time.Second) * -1);
                return rounded;
            }
            return time;
        }

        /// <summary>
        /// Zachary Hall
        /// Created 2018/02/22
        /// 
        /// Gets a list of Availability records and populates the controls
        /// </summary>
        private void populateControls()
        {
            var availabilities = getAvailabilities();
            foreach (var item in availabilities)
            {
                string dayOfWeek = item.StartTime.DayOfWeek.ToString();
                switch (dayOfWeek)
                {
                    case "Sunday":
                        timeFromSunday.Value = item.StartTime;
                        timeToSunday.Value = item.EndTime;
                        break;
                    case "Monday":
                        timeFromMonday.Value = item.StartTime;
                        timeToMonday.Value = item.EndTime;
                        break;
                    case "Tuesday":
                        timeFromTuesday.Value = item.StartTime;
                        timeToTuesday.Value = item.EndTime;
                        break;
                    case "Wednesday":
                        timeFromWednesday.Value = item.StartTime;
                        timeToWednesday.Value = item.EndTime;
                        break;
                    case "Thursday":
                        timeFromThursday.Value = item.StartTime;
                        timeToThursday.Value = item.EndTime;
                        break;
                    case "Friday":
                        timeFromFriday.Value = item.StartTime;
                        timeToFriday.Value = item.EndTime;
                        break;
                    case "Saturday":
                        timeFromSaturday.Value = item.StartTime;
                        timeToSaturday.Value = item.EndTime;
                        break;
                    default:
                        break;
                }
            }
        }

        /// <summary>
        /// Zachary Hall
        /// Created 2018/02/22
        /// 
        /// Retrieves a list of availability records from the data store.
        /// </summary>
        /// <returns></returns>
        private List<Availability> getAvailabilities()
        {
            var availabilities = new List<Availability>();
            try
            {
                availabilities = _availabilityManager.RetrieveAvailabilityByEmployeeID(_employee.EmployeeID);

            }
            catch (Exception ex)
            {
                var message = ex.Message + "\n\n" + ex.InnerException.Message;
                MessageBox.Show(message, "Data Retrieval Error.",
                    MessageBoxButton.OK, MessageBoxImage.Exclamation);
                this.DialogResult = false;
                this.Close();
            }
            return availabilities;

        }

        /// <summary>
        /// Zachary Hall
        /// Created 2018/02/22
        /// 
        /// Helper for getting an array of DateTime objects that represent the days of the current week, Sunday to Saturday
        /// </summary>
        /// <returns></returns>
        private DateTime[] getDatesThisWeek()
        {
            // Get the days of the current week
            DateTime today = DateTime.Today;
            int currentDayOfWeek = (int)today.DayOfWeek;
            DateTime sunday = today.AddDays(-currentDayOfWeek);
            var dates = Enumerable.Range(0, 7).Select(days => sunday.AddDays(days)).ToArray();
            return dates;
        }

        /// <summary>
        /// Zachary Hall
        /// Created 2018/02/22
        /// 
        /// Click event that triggers the save functionality
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            var dates = getDatesThisWeek();

            var availabilities = getAvailabilitiesFromFields(dates);
            if (availabilities == null)
            {
                return;
            }

            try
            {
                var result = _availabilityManager.EditAvailability(_employee.EmployeeID, availabilities);
                if (result >= 0)
                {
                    MessageBox.Show("Availability Updated!");
                }
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
                MessageBox.Show(message, "Edit Failed!", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
        }

        /// <summary>
        /// Zachary Hall
        /// Created 2018/02/22
        /// 
        /// Collects all the fields into an IEnumeral List, given that they are valid
        /// </summary>
        /// <param name="dates"></param>
        /// <returns></returns>
        private IEnumerable<Availability> getAvailabilitiesFromFields(DateTime[] dates)
        {
            var availabilities = new List<Availability>();

            if (validateFields())
            {
                if (timeFromSunday.Value.HasValue)
                {
                    var avail = new Availability()
                    {
                        EmployeeID = _employee.EmployeeID,
                        StartTime = roundSecondsDown(dates[0].Date.Add(((DateTime)timeFromSunday.Value).TimeOfDay)),
                        EndTime = roundSecondsDown(dates[0].Date.Add(((DateTime)timeToSunday.Value).TimeOfDay))
                    };
                    availabilities.Add(avail);
                }

                if (timeFromMonday.Value.HasValue)
                {
                    var avail = new Availability()
                    {
                        EmployeeID = _employee.EmployeeID,
                        StartTime = roundSecondsDown(dates[1].Date.Add(((DateTime)timeFromMonday.Value).TimeOfDay)),
                        EndTime = roundSecondsDown(dates[1].Date.Add(((DateTime)timeToMonday.Value).TimeOfDay))
                    };
                    availabilities.Add(avail);
                }

                if (timeFromTuesday.Value.HasValue)
                {
                    var avail = new Availability()
                    {
                        EmployeeID = _employee.EmployeeID,
                        StartTime = roundSecondsDown(dates[2].Date.Add(((DateTime)timeFromTuesday.Value).TimeOfDay)),
                        EndTime = roundSecondsDown(dates[2].Date.Add(((DateTime)timeToTuesday.Value).TimeOfDay))
                    };
                    availabilities.Add(avail);
                }

                if (timeFromWednesday.Value.HasValue)
                {
                    var avail = new Availability()
                    {
                        EmployeeID = _employee.EmployeeID,
                        StartTime = roundSecondsDown(dates[3].Date.Add(((DateTime)timeFromWednesday.Value).TimeOfDay)),
                        EndTime = roundSecondsDown(dates[3].Date.Add(((DateTime)timeToWednesday.Value).TimeOfDay))
                    };
                    availabilities.Add(avail);
                }

                if (timeFromThursday.Value.HasValue)
                {
                    var avail = new Availability()
                    {
                        EmployeeID = _employee.EmployeeID,
                        StartTime = roundSecondsDown(dates[4].Date.Add(((DateTime)timeFromThursday.Value).TimeOfDay)),
                        EndTime = roundSecondsDown(dates[4].Date.Add(((DateTime)timeToThursday.Value).TimeOfDay))
                    };
                    availabilities.Add(avail);
                }

                if (timeFromFriday.Value.HasValue)
                {
                    var avail = new Availability()
                    {
                        EmployeeID = _employee.EmployeeID,
                        StartTime = roundSecondsDown(dates[5].Date.Add(((DateTime)timeFromFriday.Value).TimeOfDay)),
                        EndTime = roundSecondsDown(dates[5].Date.Add(((DateTime)timeToFriday.Value).TimeOfDay))
                    };
                    availabilities.Add(avail);
                }

                if (timeFromSaturday.Value.HasValue)
                {
                    var avail = new Availability()
                    {
                        EmployeeID = _employee.EmployeeID,
                        StartTime = roundSecondsDown(dates[6].Date.Add(((DateTime)timeFromSaturday.Value).TimeOfDay)),
                        EndTime = roundSecondsDown(dates[6].Date.Add(((DateTime)timeToSaturday.Value).TimeOfDay))
                    };
                    availabilities.Add(avail);
                }

            }
            else
            {
                return null;
            }

            return availabilities;
        }

        /// <summary>
        /// Zachary Hall
        /// Created 2018/02/22
        /// 
        /// Validity check for fields.
        /// </summary>
        /// <returns></returns>
        private bool validateFields()
        {

            //check for nulls
            //check for greater thans
            if ((timeFromSunday.Value.HasValue && !timeToSunday.Value.HasValue)
                || (!timeFromSunday.Value.HasValue && timeToSunday.Value.HasValue))
            {
                MessageBox.Show("Either Sunday From or To field cannot be null", "Invalid Input", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                return false;
            }
            else if ((timeFromMonday.Value.HasValue && !timeToMonday.Value.HasValue)
               || (!timeFromMonday.Value.HasValue && timeToMonday.Value.HasValue))
            {
                MessageBox.Show("Either Monday From or To field cannot be null", "Invalid Input", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                return false;
            }
            else if ((timeFromTuesday.Value.HasValue && !timeToTuesday.Value.HasValue)
              || (!timeFromTuesday.Value.HasValue && timeToTuesday.Value.HasValue))
            {
                MessageBox.Show("Either Tuesday From or To field cannot be null", "Invalid Input", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                return false;
            }
            else if ((timeFromWednesday.Value.HasValue && !timeToWednesday.Value.HasValue)
             || (!timeFromWednesday.Value.HasValue && timeToWednesday.Value.HasValue))
            {
                MessageBox.Show("Either Wednesday From or To field cannot be null", "Invalid Input", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                return false;
            }
            else if ((timeFromThursday.Value.HasValue && !timeToThursday.Value.HasValue)
             || (!timeFromThursday.Value.HasValue && timeToThursday.Value.HasValue))
            {
                MessageBox.Show("Either Thursday From or To field cannot be null", "Invalid Input", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                return false;
            }
            else if ((timeFromFriday.Value.HasValue && !timeToFriday.Value.HasValue)
             || (!timeFromFriday.Value.HasValue && timeToFriday.Value.HasValue))
            {
                MessageBox.Show("Either Friday From or To field cannot be null", "Invalid Input", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                return false;
            }
            else if ((timeFromSaturday.Value.HasValue && !timeToSaturday.Value.HasValue)
             || (!timeFromSaturday.Value.HasValue && timeToSaturday.Value.HasValue))
            {
                MessageBox.Show("Either Saturday From or To field cannot be null", "Invalid Input", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                return false;
            }


            //check for greater thans
            if (timeFromSunday.Value >= timeToSunday.Value)
            {
                MessageBox.Show("Sunday From cannot be greater\nor equal to Sunday To", "Invalid Input", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                return false;
            }
            else if (timeFromMonday.Value >= timeToMonday.Value)
            {
                MessageBox.Show("Monday From cannot be greater\nor equal to Monday To", "Invalid Input", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                return false;
            }
            else if (timeFromTuesday.Value >= timeToTuesday.Value)
            {
                MessageBox.Show("Tuesday From cannot be greater\nor equal to Tuesday To", "Invalid Input", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                return false;
            }
            else if (timeFromWednesday.Value >= timeToWednesday.Value)
            {
                MessageBox.Show("Wednesday From cannot be greater\nor equal to Wednesday To", "Invalid Input", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                return false;
            }
            else if (timeFromThursday.Value >= timeToThursday.Value)
            {
                MessageBox.Show("Thursday From cannot be greater\nor equal to Thursday To", "Invalid Input", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                return false;
            }
            else if (timeFromFriday.Value >= timeToFriday.Value)
            {
                MessageBox.Show("Friday From cannot be greater\nor equal to Friday To", "Invalid Input", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                return false;
            }
            else if (timeFromSaturday.Value >= timeToSaturday.Value)
            {
                MessageBox.Show("Saturday From cannot be greater\nor equal to Saturday To", "Invalid Input", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                return false;
            }

            return true;
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
            this.Close();
        }

        private void btnUnavailableSunday_Click(object sender, RoutedEventArgs e)
        {
            timeFromSunday.Value = null;
            timeToSunday.Value = null;
        }

        private void btnUnavailableMonday_Click(object sender, RoutedEventArgs e)
        {
            timeFromMonday.Value = null;
            timeToMonday.Value = null;
        }

        private void btnUnavailableTuesday_Click(object sender, RoutedEventArgs e)
        {
            timeFromTuesday.Value = null;
            timeToTuesday.Value = null;
        }

        private void btnUnavailableWednesday_Click(object sender, RoutedEventArgs e)
        {
            timeFromWednesday.Value = null;
            timeToWednesday.Value = null;
        }

        private void btnUnavailableThursday_Click(object sender, RoutedEventArgs e)
        {
            timeFromThursday.Value = null;
            timeToThursday.Value = null;
        }

        private void btnUnavailableFriday_Click(object sender, RoutedEventArgs e)
        {
            timeFromFriday.Value = null;
            timeToFriday.Value = null;
        }

        private void btnUnavailableSaturday_Click(object sender, RoutedEventArgs e)
        {
            timeFromSaturday.Value = null;
            timeToSaturday.Value = null;
        }
    }
}
