using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace QueryRoomAPI.Models
{
    public class accountClass
    {
        
        public string USERNAME { get; set; }
        [Required(ErrorMessage = "Please Enter The Name")]
        [Display(Name = "Your Name")]
        public string NAME { get; set; }

        [Required(ErrorMessage = "Please Enter The Email ID")]
        [Display(Name = "Email ID")]
        public string EMAIL { get; set; }

        [Required(ErrorMessage = "Please Enter The Password")]
        [Display(Name = "Password")]
        [DataType(DataType.Password)]
        [MinLength(8,ErrorMessage ="Minimum 8 Charcaters Required")]
        public string PASSWORD { get; set; }

        [Required(ErrorMessage = "Please Enter The Contact Number")]
        [Display(Name = "Contact No.")]
        //[MinLength(10, ErrorMessage = "Enter Valid Contact Number")]
        public long PHONE_NO { get; set; }
    }
}