using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Spatial;
using System.ComponentModel;

namespace MovieRental2.Models
{
    public class Registration
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "what is your user name?")]
        [StringLength(50)]
        public string UserName { get; set; }

        [Required(ErrorMessage = "what is your password?")]
        [StringLength(50)]
       // [RegularExpression("^[0-9]{9}$")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [DisplayName("CofirmPassword")]
        [Compare("Password")]
        public string CofirmPassword { get; set; }

        [Required(ErrorMessage = "what is your Email?")]
        [StringLength(128)]
        [RegularExpression(@"^[a-z,A-Z]{1,10}((-|.)\w+)*@\w+.\w{3}$")]
        public string Email { get; set; }

         public DateTime Date { get; set; }
        //public DateTime Date  = DateTime.Now; 
    }
}