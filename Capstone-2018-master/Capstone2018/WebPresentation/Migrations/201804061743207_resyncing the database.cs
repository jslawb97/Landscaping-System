namespace WebPresentation.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class resyncingthedatabase : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ServicePackages",
                c => new
                    {
                        ServicePackageID = c.Int(nullable: false, identity: true),
                        TaskID = c.Int(nullable: false),
                        ServiceItemID = c.Int(nullable: false),
                        Name = c.String(),
                        Description = c.String(),
                        Active = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ServicePackageID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.ServicePackages");
        }
    }
}
