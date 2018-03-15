using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LocalTheatre.ViewModels
{
    public class UserViewModel
    {
        public string ID { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        public bool IsAdmin { get; set; }
        public bool IsSuspended { get; set; }
    }
}