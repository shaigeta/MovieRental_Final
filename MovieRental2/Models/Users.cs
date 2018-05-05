
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Spatial;
namespace MovieRental2.Models
{
    

    public partial class Users
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "what is your user name?")]
        [StringLength(50)]
        public string UserName { get; set; }

        [Required(ErrorMessage = "what is your password?")]
        [StringLength(50)]
        public string Password { get; set; }


       
    }
}
