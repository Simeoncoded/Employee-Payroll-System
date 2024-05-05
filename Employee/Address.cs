    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    namespace Employee
    {
        public struct Address
        {
       
            // Properties
            public string Street { get; set; }
            public string City { get; set; }
            public string Province { get; set; }
            public string PostalCode { get; set; }

            // Constructor with parameters for all fields
            public Address(string street, string city, string province, string postalCode)
            {
                // Validation for street, city, province, and postal code
                if (street == "")
                    throw new ArgumentException("Street is required.");
                if (city == "")
                    throw new ArgumentException("City is required.");
                if (province == "")
                    throw new ArgumentException("Province is required.");
                if (postalCode == "")
                    throw new ArgumentException("Postal code is required.");

                Street = street;
                City = city;
                Province = province;
                PostalCode = postalCode;
            }

            // Override ToString() to return a complete formatted address
            public override string ToString()
            {
                return $"{Street}, {City}, {Province}, {PostalCode}";
            }
        }
    }

