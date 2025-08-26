using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TAR1DPWHDATA.DataModels
{
    public class ChangePasswordModel
    {
        public int ID { get; set; }
        public string CurrentPassword { get; set; }
        [Required(ErrorMessage = "Old Password is required.")]
        public string OldPassword { get; set; }
        [Required(ErrorMessage = "New Password is required.")]
        public string NewPassword { get; set; }
        [Required(ErrorMessage = "Confirm Password is required.")]
        public string ConfirmPassword { get; set; }
    }
}