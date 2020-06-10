namespace CChica.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddingFechaString : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.AddCajaChicas", "Fecha", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.AddCajaChicas", "Fecha", c => c.DateTime(nullable: false));
        }
    }
}
