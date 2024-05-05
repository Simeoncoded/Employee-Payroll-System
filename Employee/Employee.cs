using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Employee
{
    public abstract class Employee : IDeduction
    {
        private string sin;
        private string first;
        private string last;
        private DateTime hire;
        private DateTime birth;
        private string email;
        private string phone;
        private Address address;
        private bool status;
        public const string COMPANY = "Simeons Company"; 

        //properties 
        public string Sin
        {
            get { return sin; }
            set
            {
                //sin must be 9 digits and its required
                if (value == "")
                {
                    throw new ArgumentException("Social Insurance Number is required.");
                }
                else if (value.Length != 9)
                {
                    throw new ArgumentException("Social Insurance Number must be 9 digits");
                }
                else
                {
                    sin = value;
                }
            }
        }

        public string FirstName
        {
            get { return first; }
            
            set
            {
                if(value == "")
                {
                    throw new ArgumentException("First name cannot be empty");//validation
                }
                else
                {
                    first = value;
                }
            }
        }

        public string LastName
        {
            get { return last; }
            set
            {
                if (value == "")
                {
                    throw new ArgumentException("Last name cannot be empty");//validation
                }
                else
                {
                    last = value;
                }
            }
        }

        public DateTime HireDate
        {
            get { return hire; }
            set
            {
                // Hire date must not be empty 
                if (value != DateTime.MinValue)
                {
                    hire = value;
                }
                else
                {
                    throw new ArgumentException("Hire date is required."); 
                }
            }
        }
        
        public DateTime BirthDate
        {
            get { return birth; }
            set
            {
                // Hire date must not be empty 
                if (value != DateTime.MinValue)
                {
                    birth = value;
                }
                else
                {
                    throw new ArgumentException("Birth date is required.");
                }
            }
        }
        
        public string Email
        {
            get { return email; }
            set { email = value; }
        }

        public string Phone
        {
            get { return phone; }
            set
            {
                if(value == "")
                {
                    throw new ArgumentException("Phone cannot be empty");
                }
                else
                {
                    phone = value;
                }
            }
        }
        public Address Address
        {
            get { return address;}
            set { address = value; }
        }

        public bool Active
        {
            get { return status; }
            set { status = value; }
        }

       public Employee(string sin)
        {
            Sin = sin;
        }



        public Employee(string sin,string first, string last)
        {
            Sin = sin;
            FirstName = first;
            LastName = last;
        }


        public Employee(string sin, string first, string last, DateTime hire)
        {
            Sin = sin;
            FirstName = first;
            LastName = last;
            HireDate = hire.Date;
        }


        //base class ToString() returns the employees full name and phone number
        public override string ToString()
        {
            return $"{FirstName} {LastName} {Phone} DateStarted: {HireDate.ToShortDateString()}";
        }

        // Method bonus
        public virtual decimal Bonus()
        {
            //default bonus
            return 0m;
        }

        // Method Calculate
        public abstract decimal Calculate();

        // Method IncomeTax
        public decimal IncomeTax()
        {
            // Get the annual income for the employee
            decimal annualIncome = Calculate();

            // Calculate income tax based on the provided criteria
            decimal incomeTax = 0m;

            if (annualIncome <= 49000)
            {
                incomeTax = annualIncome * 0.15m;
            }
            else if (annualIncome <= 98000)
            {
                incomeTax = 49000 * 0.15m + (annualIncome - 49000) * 0.20m;
            }
            else if (annualIncome <= 151000)
            {
                incomeTax = 49000 * 0.15m + 49000 *  0.20m + (annualIncome - 98000) * 0.26m;
            }
            else if (annualIncome <= 215000)
            {
                incomeTax = 49000 * 0.15m + 49000 * 0.20m + 53000 * 0.26m + (annualIncome - 151000) * 0.29m;
            }
            else
            {
                incomeTax = 49000 * 0.15m + 49000 * 0.20m + 53000 * 0.26m + 64000 * 0.29m + (annualIncome - 215000) * 0.33m;
            }

            return incomeTax;
        }

        // Method Pension
        public virtual decimal Pension()
        {
            return 0m;
        }

        // Method Union Dues
        public virtual decimal UnionDues()
        {
            return 0m;
        }

        // Method Insurance
        public virtual decimal Insurance()
        {
            return 0m;
        }
}
}

