using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Employee;
using System.ServiceModel.Channels;
using Windows.UI.Popups;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace PROG1224
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class EmployeeSelection : Page
    {
        private List<Employee.Employee> employees; // Collection of employees

        public EmployeeSelection()
        {
            this.InitializeComponent();

            // Call method to populate the collection of employees
            employees = new List<Employee.Employee>(Data.GenerateSampleEmployees());

            // Set the ListView's ItemSource to the collection of employees
            empList.ItemsSource = employees;


            // Subscribe to the Loaded event
            this.Loaded += EmployeeSelection_Loaded;

        }

        private bool pageLoaded = false;
        private void EmployeeSelection_Loaded(object sender, RoutedEventArgs e)
        {
            pageLoaded = true; // Now the page is fully loaded, and it's safe to filter
            FilterEmployees(); // Optionally call FilterEmployees here if you need to apply filters right away
        }




        private void cboEmpType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            if (cboEmpType.SelectedIndex < 0) return;
            FilterEmployees();
        }
        private void empTxtSelect_TextChanged(object sender, TextChangedEventArgs e)
        {
            FilterEmployees();
        }

        private void empStart_DateChanged(object sender, DatePickerValueChangedEventArgs e)
        {
            FilterEmployees();
        }

       
        private void FilterEmployees()
        {
            if (!pageLoaded) return; // Ensure page is loaded

            ComboBoxItem selectedType = (ComboBoxItem)cboEmpType.SelectedItem;
            string searchText = empTxtSelect.Text.Trim();
            DateTime? selectedStartDate = empStart.SelectedDate?.Date;

            List<Employee.Employee> filteredEmployees = new List<Employee.Employee>();

            if (selectedType != null && selectedType.Content.ToString() != "All")
            {
                string selectedTypeName = selectedType.Content.ToString();

                foreach (var emp in employees)
                {
                    // Check if the employee type matches the selected type
                    if (emp.GetType().Name == selectedTypeName)
                    {
                        // Check if the start date matches the selected start date (if any)
                        if (selectedStartDate == null || emp.HireDate.Date == selectedStartDate)
                        {
                            filteredEmployees.Add(emp);
                        }
                    }
                }
            }
            else
            {
                // If "All" is selected or no selection is made, show all employees
                filteredEmployees.AddRange(employees);
            }

            // Apply additional search filter if searchText is not empty
            if (!string.IsNullOrWhiteSpace(searchText))
            {
                List<Employee.Employee> tempEmployees = new List<Employee.Employee>();
                foreach (var emp in filteredEmployees)
                {
                    // Check if any employee property contains the search text
                    if (emp.FirstName.Contains(searchText, StringComparison.OrdinalIgnoreCase) ||
                        emp.LastName.Contains(searchText, StringComparison.OrdinalIgnoreCase) ||
                        emp.Sin.Contains(searchText, StringComparison.OrdinalIgnoreCase))
                    {
                        tempEmployees.Add(emp);
                    }
                }
                filteredEmployees = tempEmployees;
            }

            // Update the ListView's ItemsSource
            empList.ItemsSource = filteredEmployees;
        }




      


        private void empList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Get the selected employee from the ListView
            Employee.Employee selectedEmployee = (Employee.Employee)empList.SelectedItem;

            // Check if an employee is selected
            if (selectedEmployee != null)
            {
                // Display the data of the selected employee in appropriate components

                // Update Text Boxes with employee data
                txtFirstname.Text = selectedEmployee.FirstName;
                txtLastname.Text = selectedEmployee.LastName;
                txtSin.Text = selectedEmployee.Sin;




                // Adjust visibility or enable/disable state of components based on employee type
                if (selectedEmployee is Hourly h)
                {
                    // Hourly employee specific adjustments
                    txtHourlyRate.IsEnabled = true;
                    txtHourlyRate.Text = h.Rate.ToString();
                    txtHourlyRate.Visibility = Visibility.Visible;
                    txtSalary.IsEnabled = false;
                    txtSalary.Visibility = Visibility.Collapsed;
                    txtCommissionRate.IsEnabled = false;
                    txtCommissionRate.Visibility = Visibility.Collapsed;
                    txtHoursWorked.Visibility = Visibility.Visible;
                    txtHoursWorked.IsEnabled = true;
                    txtHoursWorked.Text = h.Hours.ToString();
                    txtSalesAmt.Visibility = Visibility.Collapsed;
                    txtSalesAmt.IsEnabled = false;
                }
                else if (selectedEmployee is Salary s)
                {
                    // Salary employee specific adjustments
                    txtHourlyRate.IsEnabled = false;
                    txtSalary.Text = s.Amount.ToString();
                    txtHourlyRate.Visibility = Visibility.Collapsed;
                    txtSalary.IsEnabled = true;
                    txtSalary.Visibility = Visibility.Visible;
                    txtCommissionRate.IsEnabled = false;
                    txtCommissionRate.Visibility = Visibility.Collapsed;
                    txtHoursWorked.Visibility = Visibility.Collapsed;
                    txtHoursWorked.IsEnabled = false;
                    txtSalesAmt.Visibility = Visibility.Collapsed;
                    txtSalesAmt.IsEnabled = false;
                }
                else if (selectedEmployee is SalesPerson sp)
                {
                    // Sales person specific adjustments
                    txtCommissionRate.Text = sp.CommissionRate.ToString();
                    txtSalesAmt.Text = sp.SalesAmt.ToString();
                    txtHourlyRate.IsEnabled = false;
                    txtHourlyRate.Visibility = Visibility.Collapsed;
                    txtSalesAmt.Visibility = Visibility.Visible;
                    txtSalesAmt.IsEnabled = true;
                    txtSalary.IsEnabled = false;
                    txtSalary.Visibility = Visibility.Collapsed;
                    txtCommissionRate.IsEnabled = true;
                    txtCommissionRate.Visibility = Visibility.Visible;
                    txtHoursWorked.Visibility = Visibility.Collapsed;
                    txtHoursWorked.IsEnabled = false;

                }
            }
            else
            {
                // Clear UI components if no employee is selected
                txtFirstname.Text = string.Empty;
                txtLastname.Text = string.Empty;
                txtSin.Text = string.Empty;
                txtHourlyRate.IsEnabled = false;
                txtHourlyRate.Visibility = Visibility.Collapsed;
                txtSalary.IsEnabled = false;
                txtSalary.Visibility = Visibility.Collapsed;
                txtCommissionRate.IsEnabled = false;
                txtCommissionRate.Visibility = Visibility.Collapsed;
                txtHoursWorked.Visibility = Visibility.Collapsed;
                txtHoursWorked.IsEnabled = false;
                txtSalesAmt.Visibility = Visibility.Collapsed;
                txtSalesAmt.IsEnabled = false;
            }
        }

        private void btnChange_Click(object sender, RoutedEventArgs e)
        {
            // Get the selected employee from the ListView
            Employee.Employee selectedEmployee = (Employee.Employee)empList.SelectedItem;

            // Check if an employee is selected
            if (selectedEmployee != null)
            {
                // Update the properties of the selected employee based on the values entered in the text boxes
                selectedEmployee.FirstName = txtFirstname.Text;
                selectedEmployee.LastName = txtLastname.Text;
                selectedEmployee.Sin = txtSin.Text;

                // Update properties based on the type of employee
                if (selectedEmployee is Hourly h)
                {
                    if (decimal.TryParse(txtHourlyRate.Text, out decimal rate))
                    {
                        h.Rate = rate;
                    }

                    if (decimal.TryParse(txtHoursWorked.Text, out decimal hours))
                    {
                        h.Hours = hours;
                    }
                }
                else if (selectedEmployee is Salary s)
                {
                    if (decimal.TryParse(txtSalary.Text, out decimal amount))
                    {
                        s.Amount = amount;
                    }
                }
                else if (selectedEmployee is SalesPerson sp)
                {
                    if (decimal.TryParse(txtCommissionRate.Text, out decimal commissionRate))
                    {
                        sp.CommissionRate = commissionRate;
                    }
                }

                // update changes
                empList.ItemsSource = null;
                FilterEmployees();

                // Reset text boxes and enable/disable states
                txtHourlyRate.IsEnabled = true;
                txtSalary.Visibility = Visibility.Visible;
                txtHourlyRate.Visibility = Visibility.Visible;
                txtSalary.IsEnabled = true;
                txtCommissionRate.IsEnabled = true;
                txtCommissionRate.Visibility = Visibility.Visible;
                txtHoursWorked.Visibility = Visibility.Visible;
                txtHoursWorked.IsEnabled = true;
                txtSalesAmt.IsEnabled = true;
                txtSalesAmt.Visibility = Visibility.Visible;

                txtFirstname.Text = "";
                txtLastname.Text = "";
                txtHourlyRate.Text = "";
                txtCommissionRate.Text = "";
                txtSin.Text = "";
                txtSalary.Text = "";
                txtHoursWorked.Text = "";
                txtSalesAmt.Text = "";
            }
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            MessageDialog msg;
            // Create a new employee based on the entered details
            Employee.Employee newEmployee = null;

            // Get the selected employee type from the combo box
            string selectedType = ((ComboBoxItem)cboNewEmployeeType.SelectedItem).Content.ToString();


            if (string.IsNullOrEmpty(selectedType))
            {
                // Display an error message or handle the situation accordingly
                msg = new MessageDialog("Select an employee type");
                msg.ShowAsync();
                return;
            }



            // Validate input fields based on the selected employee type
            if (selectedType == "Hourly")
            {
                if (!ValidateHourlyInput())
                {
                    // Display an error message or handle the situation accordingly
                    msg = new MessageDialog("Please Fill in all details");
                    msg.ShowAsync();
                    return;
                }
            }
            else if (selectedType == "Salary")
            {
                if (!ValidateSalaryInput())
                {
                    // Display an error message or handle the situation accordingly
                    msg = new MessageDialog("Please Fill in all details");
                    msg.ShowAsync();
                    return;
                }
            }
            else if (selectedType == "SalesPerson")
            {
                if (!ValidateSalesPersonInput())
                {
                    // Display an error message or handle the situation accordingly
                    msg = new MessageDialog("Please Fill in all details");
                    msg.ShowAsync();
                    return;
                }
            }

            



            // Determine the type of employee based on user input (you can use combo boxes or radio buttons for this)
            if (selectedType == "Hourly")
            {

                // Create a new Hourly employee instance and set its properties based on the user input
                Hourly hourlyEmployee = new Hourly(txtNewSin.Text, txtNewFirstName.Text, txtNewLastName.Text, txtNewDateStarted.Date.UtcDateTime, Decimal.Parse(txtNewHourlyRate.Text), Decimal.Parse(txtNewHoursWorked.Text));

                // Assign the new employee instance
                newEmployee = hourlyEmployee;
            }
            else if (selectedType == "Salary")
            {

                // Create a new Salary employee instance and set its properties based on the user input
                Salary salaryEmployee = new Salary(txtNewSin.Text, txtNewFirstName.Text, txtNewLastName.Text, txtNewDateStarted.Date.UtcDateTime, Decimal.Parse(txtNewSalary.Text));

                // Assign the new employee instance
                newEmployee = salaryEmployee;
            }
            else if (selectedType == "SalesPerson")
            {

                // Create a new SalesPerson employee instance and set its properties based on the user input
                SalesPerson salesPersonEmployee = new SalesPerson(txtNewSin.Text, txtNewFirstName.Text, txtNewLastName.Text, txtNewDateStarted.Date.UtcDateTime, Decimal.Parse(txtNewSalesAmt.Text), Decimal.Parse(txtNewCommission.Text));
                
                // Assign the new employee instance
                newEmployee = salesPersonEmployee;
            }

            // Add the new employee to the collection
            employees.Add(newEmployee);

            FilterEmployees(); // This will refresh the list with the new employee added
        }


        // Validation methods for each employee type
        private bool ValidateHourlyInput()
        {
            if (txtNewSin.Text == ""||
                txtNewFirstName.Text == "" ||
                txtNewLastName.Text == "" ||
                txtNewHourlyRate.Text == "" ||
               txtNewHoursWorked.Text == ""
                )
            {
                return false;
            }
            return true;
        }

        private bool ValidateSalaryInput()
        {
            if (txtNewSin.Text == "" ||
                txtNewFirstName.Text == "" ||
                txtNewLastName.Text == "" ||
                txtNewSalary.Text == ""
                )
            {
                return false;
            }
            return true;
        }

        private bool ValidateSalesPersonInput()
        {
            if (txtNewSin.Text == "" ||
                txtNewFirstName.Text == "" ||
                txtNewLastName.Text == "" ||
                txtNewSalesAmt.Text == "" ||
                txtNewCommission.Text == "")
            {
                return false;
            }
            return true;
        }

        private void HyperlinkButton_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            // Navigate to the target page
            Frame.Navigate(typeof(Payrollinformation));
        }

      
    }
}

