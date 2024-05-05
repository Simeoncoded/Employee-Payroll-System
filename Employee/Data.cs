using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Employee
{
    public static class Data
    {
        // Method to generate sample employee data
        public static Employee[] GenerateSampleEmployees()
        {
            // Create an array to store sample employees
            Employee[] employees = new Employee[15]; // Adjust based on actual types and counts

            //employees[0] = new Hourly("111111111", "John", "Doe", 15m, 40, new DateTime(2023, 1, 1));
            //employees[1] = new Hourly("222222222", "Jane", "Smith", 20m, 35, new DateTime(2024, 2, 1));
            //employees[2] = new Hourly("333333333", "Michael", "Johnson", 18m, 38, new DateTime(2024, 3, 1));
            //employees[3] = new Hourly("444444444", "Emily", "Brown", 17m, 42, new DateTime(2024, 4, 1));
            //employees[4] = new Hourly("555555555", "Davd", "Lee", 16m, 37, new DateTime(2024, 6, 1));

            // Add sample Hourly employees
            employees[0] = new Hourly("111111111", "John", "Doe", new DateTime(2023, 1, 1),15m, 40);
            employees[1] = new Hourly("222222222", "Jane", "Smith", new DateTime(2024, 2, 1).Date, 20m, 35);
            employees[2] = new Hourly("333333333", "Michael", "Johnson", new DateTime(2024, 3, 1).Date, 18m, 38);
            employees[3] = new Hourly("444444444", "Emily", "Brown", new DateTime(2024, 4, 1).Date, 17m, 42);
            employees[4] = new Hourly("555555555", "David", "Lee", new DateTime(2024, 6, 1).Date, 16m, 37);

            // Add sample Salary employees
            employees[5] = new Salary("666666666", "Alice", "Williams", new DateTime(2023, 3, 4), 55000m);
            employees[6] = new Salary("777777777", "Robert", "Jones", new DateTime(2023, 5, 7), 60000m);
            employees[7] = new Salary("888888888", "Karen", "Davis", new DateTime(2023, 2, 1), 65000m);
            employees[8] = new Salary("999999999", "Daniel", "Miller", new DateTime(2023, 3, 4), 70000m);
            employees[9] = new Salary("101010101", "Linda", "Wilson", new DateTime(2023, 5, 6), 75000m);

            // Add sample SalesPerson employees
            employees[10] = new SalesPerson("111222333", "James", "Taylor", new DateTime(2023, 3, 4), 80000m, 0.07m);
            employees[11] = new SalesPerson("444555666", "Sarah", "Martinez", new DateTime(2023, 7, 2), 85000m, 0.08m);
            employees[12] = new SalesPerson("777888999", "Kevin", "Anderson", new DateTime(2023, 4, 1), 90000m, 0.09m);
            employees[13] = new SalesPerson("111222333", "Emma", "Garcia", new DateTime(2023, 8, 3), 95000m, 0.1m);
            employees[14] = new SalesPerson("444555666", "Jason", "Hernandez", new DateTime(2023, 9, 1), 100000m, 0.11m);

            return employees;
        }
    }
}
