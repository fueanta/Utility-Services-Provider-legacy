using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace USP_Application.ViewModels
{
    public class LabourServiceMapFormViewModel
    {
        public LabourServiceMap LabourServiceMap { get; set; }
        public IEnumerable<Labour> Labours { get; set; }
        public IEnumerable<Service> Services { get; set; }
    }
}