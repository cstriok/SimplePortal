using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SimplePortal.Areas.Admin.Models
{
    public class ChangePasswordViewModel
    {
        public Guid Uid { get; set; }
        public string OldPassword { get; set; }
        public string NewPassword { get; set; }
    }
}