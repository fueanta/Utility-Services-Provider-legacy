using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace USP_Application.ViewModels
{
    public class LabourAssignedFormViewModel
    {
        public LabourAssigned LabourAssigned { get; set; }
        public IEnumerable<Labour> Labours { get; set; }
        public IEnumerable<Employee> Employees { get; set; }
        public IEnumerable<ServiceRequest> ServiceRequests { get; set; }
    }
}