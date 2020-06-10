namespace CChica.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class quitarRequired : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.AddCajaChicas", "CreadoPor", c => c.String(maxLength: 50));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.AddCajaChicas", "CreadoPor", c => c.String(nullable: false, maxLength: 50));
        }
    }
}
