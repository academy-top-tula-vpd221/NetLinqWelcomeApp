using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetLinqWelcomeApp
{
    public class Employee
    {
        public string? Name { get; set; }
        public int Age { get; set; }
        public string? Phone { get; set; }
    }

    public class EmployeNameLengthComparer : IComparer<Employee>
    {
        public int Compare(Employee? x, Employee? y)
        {
            int lengthX = x?.Name?.Length ?? 0;
            int lengthY = y?.Name?.Length ?? 0;
            return lengthX - lengthY;
        }
    }
}
