namespace MovieRental2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddCofirmPassword : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Registrations", "CofirmPassword", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Registrations", "CofirmPassword");
        }
    }
}
