using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace USP_Application.ViewModels
{
    public class LoginFormViewModel
    {
        public UserLogin UserLogin { get; set; }
        public bool RememberMe { get; set; }
        public bool Invalid { get; set; }
    }
}