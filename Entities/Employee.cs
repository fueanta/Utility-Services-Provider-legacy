using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class Employee : ParentUser
    {
        // Id
        // Name
        // Address
        // JoiningDate
        // Gender
        // ImagePath from ParentUser

        public double Salary { get; set; }
        public double Commission { get; set; }
    }
}
