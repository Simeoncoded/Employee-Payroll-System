using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Employee
{
    public sealed class Hourly : Employee, IDeduction
    {
        private decimal rate;
        private decimal hours;


        // Properties
        public decimal Rate { get; set; }
        public decimal Hours { get; set; }

        public Hourly(string sin) : base(sin) { }

        public Hourly(string sin, string first, string last) : base(sin, first, last) { }
       

        public Hourly(string sin, string first, string last, DateTime hire, decimal rate,decimal hours) 
            : base(sin, first, last, hire)
        {
            Rate = rate;
            Hours = hours;
        }

        // Override ToString() to return a string representation of the Hourly employee
        public override string ToString()
        {
            return $"Hourly Employee: {base.ToString()}, Rate: {Rate}, Hours Worked: {Hours}";
        }

        // Override Bonus() method to calculate bonus for Hourly employee
        public override decimal Bonus()
        {
            // Default bonus amount for Hourly employee
            return 0m;
        }

        // Override CalculatePay() method to calculate pay for Hourly employee
        public override decimal Calculate()
        {
            // Calculate pay for Hourly employee based on hourly rate and hours worked
            return Rate * Hours;
        }

        // Implement IDeduction interface method to calculate Pension
        public override decimal Pension()
        {
            // Assuming 5% of the employee's pay goes towards pension
            // This is just a hardcoded example; actual pension calculation may vary
            return Calculate() * 0.05m;
        }

        // Implement IDeduction interface method to calculate Union Dues
        public override decimal UnionDues()
        {
            // Assuming a fixed amount of $10 per week for union dues
            // This is a hardcoded value; actual union dues may vary
            return 10m;
        }

        // Implement IDeduction interface method to calculate Insurance
        public override decimal Insurance()
        {
            // Assuming a fixed amount of $50 per month for insurance
            // This is a hardcoded value; actual insurance calculation may vary
            return 50m;
        }
    }
}
