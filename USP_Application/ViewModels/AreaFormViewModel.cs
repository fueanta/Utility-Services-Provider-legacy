using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace USP_Application.ViewModels
{
    public class AreaFormViewModel
    {
        public Area Area { get; set; }
        public IEnumerable<City> Cities { get; set; }
    }
}