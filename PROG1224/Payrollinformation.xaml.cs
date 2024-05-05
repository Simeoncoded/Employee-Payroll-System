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
using Windows.UI.StartScreen;
using System.ServiceModel.Channels;
using Windows.UI.Popups;
using Windows.UI.Xaml.Shapes;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace PROG1224
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Payrollinformation : Page
    {
        private List<Employee.Employee> employees; // Collection of employees
        public Payrollinformation()
        {
            this.InitializeComponent();

            LoadEmployeeData();

            // Call method to populate the collection of employees
            employees = new List<Employee.Employee>(Data.GenerateSampleEmployees());

            // Set the ListView's ItemSource to the collection of employees
            empListView.ItemsSource = employees;


        }


        private void btnProcessPay_Click(object sender, RoutedEventArgs e)
        {
            // Get the selected pay period date
            DateTime payPeriodDate = payPeriodDatePicker.Date.Date;

            // Get the selected employees from the ListView
            List<Employee.Employee> selectedEmployees = empListView.SelectedItems.Cast<Employee.Employee>().ToList();

            // Create an instance of PayrollProcess with the current pay period date and selected employees
            PayrollProcess<Employee.Employee> payrollProcess = new PayrollProcess<Employee.Employee>(payPeriodDate, selectedEmployees);

            // Subscribe to the PayrollAlert event
            payrollProcess.PayrollAlert += HandlePayrollAlert;


            // Generate the pay period string
            string payPeriodString = $"Pay for period ending {payPeriodDate.ToString("d")}";

            // Process payroll and get the payroll information
            List<string> payrollInfo = payrollProcess.ProcessPayroll();

            // Display the pay period string as the header of the payroll information TextBlock
            payrollInfoTextBlock.Text = payPeriodString + "\n";

            foreach (string info in payrollInfo)
            {
                payrollInfoTextBlock.Text += info + "\n";
            }
        }

        // Define the event handler method
        private void HandlePayrollAlert(object sender, PayrollAlertEventArgs e)
        {
            MessageDialog msg;
            msg = new MessageDialog(e.Message);
            msg.ShowAsync();


        }

        // Method to load and display employee data
        private void LoadEmployeeData()
        {
            // Retrieve your employee data (e.g., from a database or collection)
            var employees = new List<Employee.Employee>(Data.GenerateSampleEmployees()); // Example method to get employee data

            // Calculate required information using LINQ queries
            var totalEmployees = employees.Count;

            var totalActiveEmployees = employees.Count(emp => emp.Active);
            //var totalInactiveEmployees = employees.Count(emp => !emp.IsActive);

            // Find employee with the most seniority and least seniority
            var mostSeniorEmployee = employees.OrderByDescending(emp => emp.HireDate).FirstOrDefault();
            var leastSeniorEmployee = employees.OrderBy(emp => emp.HireDate).FirstOrDefault();
            // Find the employee with the highest pay
            var highestPaidEmployee = employees.OrderByDescending(emp => emp.Calculate()).FirstOrDefault();

            // Group employees by their types and calculate the total pay for each type
            var employeeTypeTotalPay = employees.GroupBy(emp => emp.GetType().Name)
                                                .Select(group => new
                                                {
                                                    Type = group.Key,
                                                    TotalPay = group.Sum(emp => emp.Calculate())
                                                });

            // Find the employee type with the highest total pay
            var highestPaidEmployeeType = employeeTypeTotalPay.OrderByDescending(group => group.TotalPay).FirstOrDefault();

            // Calculate the average pay 
            decimal averagePay = employees.Average(emp => emp.Calculate());

            lblTotalEmployees.Text = $"Total Employees: {totalEmployees}";
            lblTotalActive.Text = $"Total Active Employees: {totalActiveEmployees}";
            lblMostSeniorEmployee.Text = $"Most Senior Employee: {mostSeniorEmployee.FirstName} {mostSeniorEmployee.LastName},Type: {mostSeniorEmployee.GetType().Name},  Pay: {mostSeniorEmployee.Calculate()}";
            lblLeastSeniorEmployee.Text = $"Least Senior Employee: {leastSeniorEmployee.FirstName} {leastSeniorEmployee.LastName}, Type: {leastSeniorEmployee.GetType().Name},  Pay: {leastSeniorEmployee.Calculate()}";

            // Display the employee's information
            lblHighestPaidEmployee.Text = $"Highest Paid Employee: {highestPaidEmployee.FirstName} {highestPaidEmployee.LastName}, Type: {highestPaidEmployee.GetType().Name}, Pay: {highestPaidEmployee.Calculate()}";
            // Display the employee type's information
            lblHighestPaidEmployeeType.Text = $"Highest Paid Employee Type: {highestPaidEmployeeType.Type}, Total Pay: {highestPaidEmployeeType.TotalPay}";
  
            // Display the average pay
            lblAveragePay.Text = $"Average Pay: {averagePay:C}";
        }

        private void HyperlinkButton_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            // Navigate to the target page
            Frame.Navigate(typeof(EmployeeSelection));
        }
    }
}
