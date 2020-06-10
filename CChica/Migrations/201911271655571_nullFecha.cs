namespace CChica.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class nullFecha : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.AddCajaChicas", "fechaApertura", c => c.DateTime());
            AlterColumn("dbo.AddCajaChicas", "FechaCierre", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.AddCajaChicas", "FechaCierre", c => c.DateTime(nullable: false));
            AlterColumn("dbo.AddCajaChicas", "fechaApertura", c => c.DateTime(nullable: false));
        }
    }
}
