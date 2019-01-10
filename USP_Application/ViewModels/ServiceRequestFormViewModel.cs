using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace USP_Application.ViewModels
{
    public class ServiceRequestFormViewModel
    {
        public ServiceRequest ServiceRequest { get; set; }
        public IEnumerable<Client> Clients { get; set; }
        public IEnumerable<Service> Services { get; set; }
    }
}