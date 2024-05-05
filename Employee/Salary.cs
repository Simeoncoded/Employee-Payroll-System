using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Employee
{
    public class Salary : Employee, IDeduction
    {
        private decimal amount;

        public decimal Amount
        {
            get { return amount; }
            set { amount = value; }
        }

        public Salary(string sin) : base(sin) { }
        public Salary(string sin, string first, string last) : base(sin, first, last) { }

        public Salary(string sin,string first,string last,DateTime hire,decimal amount): base(sin, first, last,hire)
        {
            Amount = amount;
        }

        // Override ToString() to return a string representation of the Salary employee
        public override string ToString()
        {
            return $"Salary Employee: {base.ToString()}, Yearly Salary: {Amount:C}";
        }

        // Override Bonus() method to calculate bonus for Salary employee based on length of service
        public override decimal Bonus()
        {
            // Calculate bonus based on length of service (100.00 per year based on start date)
            DateTime today = DateTime.Today;
            DateTime anniversary = HireDate.AddYears(today.Year - HireDate.Year);

            if (today < anniversary)
            {
                anniversary = anniversary.AddYears(-1);
            }

            int yearsOfService = today.Year - HireDate.Year;
            decimal bonus = yearsOfService * 100.00m;
            return bonus;
        }

        // Override CalculatePay() method to calculate pay for Salary employee
        public override decimal Calculate()
        {
            // Salary employee's pay is their fixed yearly salary amount
            return Amount / 26m;  //divide by 26cfor biweeky;
        }

        // Implement IDeduction interface method to calculate Pension
        public override decimal Pension()
        {
            // Assuming 8% of the employee's pay goes towards pension

            return Calculate() * 0.08m;
        }

        // Implement IDeduction interface method to calculate Union Dues
        public override decimal UnionDues()
        {
            // Assuming a fixed amount of $20 per month for union dues
            // This is a hardcoded value; actual union dues may vary
            return 20m;
        }

        // Implement IDeduction interface method to calculate Insurance
        public override decimal Insurance()
        {
            // Assuming a fixed amount of $100 per month for insurance
            // This is a hardcoded value; actual insurance calculation may vary
            return 100m;
        }
    }
}
