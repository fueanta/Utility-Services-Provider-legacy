using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace USP_Application.ViewModels
{
    public class ClientListViewModel
    {
        public List<Client> Clients { get; set; }
        public List<UserLogin> UserLogins { get; set; }
    }
}