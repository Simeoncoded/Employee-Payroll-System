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

            List<Employee.Employee> sourceList; // This will hold the correct list to use as the ItemsSource

            if (selectedType != null && selectedType.Content.ToString() == "All")
            {
                // Reset the employees list to its original state and use it as the source
                sourceList = new List<Employee.Employee>(Data.GenerateSampleEmployees());
            }
            else if (selectedType != null)
            {
                string selectedTypeName = selectedType.Content.ToString();
                List<Employee.Employee> filteredEmployees = new List<Employee.Employee>();

        
                foreach (var emp in Data.GenerateSampleEmployees()) // This ensures you're always starting from the full list
                {
                    if (emp.GetType().Name == selectedTypeName)
                    {
                        filteredEmployees.Add(emp);
                    }
                }

                if (!string.IsNullOrWhiteSpace(searchText))
                {
                    filteredEmployees = filteredEmployees.Where(emp =>
                        emp.FirstName.Contains(searchText, StringComparison.OrdinalIgnoreCase) ||
                        emp.LastName.Contains(searchText, StringComparison.OrdinalIgnoreCase) ||
                        emp.Sin.Contains(searchText, StringComparison.OrdinalIgnoreCase)).ToList();
                }

                // After filtering, this becomes the source list
                sourceList = filteredEmployees;
            }
            else
            {
                // Fallback to the full list if no valid selection is made (adjust according to your needs)
                sourceList = new List<Employee.Employee>(Data.GenerateSampleEmployees());
            }

            // Update the ListView's ItemsSource
            empList.ItemsSource = null; // Resetting to null to ensure the UI refreshes
            empList.ItemsSource = sourceList; // Assign the correct list
        }



    }
}






