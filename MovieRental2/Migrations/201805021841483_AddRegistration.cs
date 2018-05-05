namespace MovieRental2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddRegistration : DbMigration
    {
        public override void Up()
        {
                  
            CreateTable(
                "dbo.Registrations",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserName = c.String(nullable: false, maxLength: 50),
                        Password = c.String(nullable: false, maxLength: 50),
                        Email = c.String(nullable: false, maxLength: 128),
                        Date = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
      
            
        }
        
        public override void Down()
        {
           
            DropTable("dbo.Registrations");
          
        }
    }
}
