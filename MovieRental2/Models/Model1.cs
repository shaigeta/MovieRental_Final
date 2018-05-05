namespace MovieRental2.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class Model1 : DbContext
    {
        public Model1()
            : base("name=Model1")
        {
        }

        public virtual DbSet<Genre> Genre { get; set; }
        public virtual DbSet<Movies> Movies { get; set; }
        public virtual DbSet<Orders> Orders { get; set; }
        public virtual DbSet<Users> Users { get; set; }
        public virtual DbSet<Registration> Registration { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Genre>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<Movies>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<Movies>()
                .Property(e => e.Description)
                .IsUnicode(false);

            modelBuilder.Entity<Users>()
                .Property(e => e.UserName)
                .IsUnicode(false);

            modelBuilder.Entity<Users>()
                .Property(e => e.Password)
                .IsUnicode(false);


            //modelBuilder.Entity<Registration>()
            //   .Property(e => e.UserName)
            //   .IsUnicode(false);

            //modelBuilder.Entity<Registration>()
            //    .Property(e => e.Password)
            //    .IsUnicode(false);
            //modelBuilder.Entity<Registration>()
            //    .Property(e => e.Email)
            //    .IsUnicode(false);
        }
    }
}
