namespace MovieRental2.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Orders
    {
        public int id { get; set; }

        public int UserId { get; set; }

        public int MovieId { get; set; }

        public DateTime date { get; set; }
    }
}
