using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Employee
{
    public interface IDeduction
    {
        decimal IncomeTax();
        decimal Pension();
        decimal UnionDues();

        decimal Insurance();
    }
}
