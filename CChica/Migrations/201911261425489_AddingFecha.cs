namespace CChica.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddingFecha : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AddCajaChicas", "Fecha", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.AddCajaChicas", "Fecha");
        }
    }
}
