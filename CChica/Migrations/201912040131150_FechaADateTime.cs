namespace CChica.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FechaADateTime : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.AddCajaChicas", "fecha", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.AddCajaChicas", "fecha", c => c.String());
        }
    }
}
