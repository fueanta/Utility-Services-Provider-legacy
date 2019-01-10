using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace USP_Application.Models
{
    public class EmployeeUserLoginModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public double Salary { get; set; }
        public double Commission { get; set; }
    }
}