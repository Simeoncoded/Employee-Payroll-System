using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Employee
{
    // Define a delegate for the event
    public delegate void PayrollAlertEventHandler(object sender, PayrollAlertEventArgs e);

    // Define event arguments for the custom event
    public class PayrollAlertEventArgs : EventArgs
    {
        public string Message { get; }

        public PayrollAlertEventArgs(string message)
        {
            Message = message;
        }
    }

    public class PayrollProcess<T> where T : Employee
    {

        // Define the event based on the delegate
        public event PayrollAlertEventHandler PayrollAlert;

        private DateTime currentPayPeriod;
        private List<T> employees;


        // Constructor
        public PayrollProcess(DateTime currentPayPeriod, List<T> employees)
        {
            this.currentPayPeriod = currentPayPeriod;
            this.employees = employees;
        }

        // Method to process payroll and return a list of formatted strings
        public List<string> ProcessPayroll()
        {
            List<string> payrollSummary = new List<string>();

            // Process each employee
            foreach (T employee in employees)
            {
                string employeeInfo = $"{employee.Sin} {employee.FirstName} {employee.LastName} - ";
                decimal pay = employee.Calculate();
                decimal bonus = employee.Bonus();
                decimal deductions = employee.IncomeTax() + employee.Pension() + employee.UnionDues() + employee.Insurance();
                decimal netPay = pay + bonus - deductions;

                // Check if the employee's pay exceeds a certain threshold
                if (netPay > 10000) // Example threshold, replace with your business logic
                {
                    // Trigger the event
                    OnPayrollAlert(new PayrollAlertEventArgs($"Warning: Employee {employee.FirstName} {employee.LastName} has exceeded the payroll threshold!"));
                }

                // Format the employee information along with pay, bonus, and deductions
                string formattedInfo = $"{employeeInfo} Net: {netPay:C} - Bonus: {bonus:C} - Deductions: {deductions:C}";
                payrollSummary.Add(formattedInfo);
            }

            return payrollSummary;
        }


        // Method to raise the event
        protected virtual void OnPayrollAlert(PayrollAlertEventArgs e)
        {
            PayrollAlert?.Invoke(this, e);
        }

        // Read-only properties for totals
        public int TotalEmployeeCount => employees.Count;

        

        //returns totalpay
        public decimal TotalPay
        {
            get
            {
                decimal totalPay = 0m;
                foreach (T employee in employees)
                {
                    totalPay += employee.Calculate();
                }
                return totalPay;
            }
        }

        //returns total bonus
        public decimal TotalBonus
        {
            get
            {
                decimal totalBonus = 0m;
                foreach (T employee in employees)
                {
                    totalBonus += employee.Bonus();
                }
                return totalBonus;
            }
        }

        //returns total deductions
        public decimal TotalDeductions
        {
            get
            {
                decimal totalDeductions = 0m;
                foreach (T employee in employees)
                {
                    totalDeductions += employee.IncomeTax() + employee.Pension() + employee.UnionDues() + employee.Insurance();
                }
                return totalDeductions;
            }
        }
    }
 
}
