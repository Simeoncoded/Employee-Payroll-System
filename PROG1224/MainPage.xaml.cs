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



// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace PROG1224
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private List<Employee.Employee> employees; 
       
        public MainPage()
        {
            this.InitializeComponent();

            // Call the method from the Data class to retrieve sample objects
            employees = new List<Employee.Employee>(Data.GenerateSampleEmployees());



        }

        private void HyperlinkButton_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            // Navigate to the target page
            Frame.Navigate(typeof(EmployeeSelection));
        }

        private void HyperlinkButton_Click_1(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            // Navigate to the target page
            Frame.Navigate(typeof(Payrollinformation));
        }

       
    }
}
