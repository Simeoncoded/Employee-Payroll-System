using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Employee
{

    //this is a salesperson class
    //salesperson has commission rate
  
    public class SalesPerson : Employee
    {
        private decimal salesAmt;
        private decimal commissionRate;

        public decimal SalesAmt
        {
            get { return salesAmt; }
            set
            {
                if (value < 0)
                {
                    throw new ArgumentException("Sales amount cannot be negative.");//validation
                }
                else
                {
                    salesAmt = value;
                }
            }
        }

        public decimal CommissionRate
        {
            get { return commissionRate; }
            set
            {
                if (value < 0 || value > 1)
                {
                    throw new ArgumentException("Commission rate must be between 0 and 1.");//validation
                }
                else
                {
                    commissionRate = value;
                }
            }
        }

        // Constructor with Social Insurance Number, first name, last name, sales amount, and commission rate parameters
        public SalesPerson(string sin, string firstName, string lastName,DateTime hire, decimal salesAmt, decimal commissionRate) : base(sin, firstName, lastName,hire)
        {
            SalesAmt = salesAmt;
            CommissionRate = commissionRate;
        }

        // Override ToString() to return a string representation of the SalesPerson
        public override string ToString()
        {
            return $"Sales Person: {base.ToString()}, Sales Amount: {SalesAmt:C}, Commission Rate: {CommissionRate:P}";
        }

        // Override CalculatePay() method to calculate pay for SalesPerson
        public override decimal Calculate()
        {
            // Calculate pay based on sales amount and commission rate
            return SalesAmt * CommissionRate;
        }
    }
}
