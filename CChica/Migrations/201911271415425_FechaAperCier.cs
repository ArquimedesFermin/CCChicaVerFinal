namespace CChica.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FechaAperCier : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AddCajaChicas", "fechaApertura", c => c.DateTime(nullable: false));
            AddColumn("dbo.AddCajaChicas", "FechaCierre", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.AddCajaChicas", "FechaCierre");
            DropColumn("dbo.AddCajaChicas", "fechaApertura");
        }
    }
}
