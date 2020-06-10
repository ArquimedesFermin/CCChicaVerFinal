namespace CChica.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addTimeString : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.EgresoIngresoes", "Time", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.EgresoIngresoes", "Time", c => c.DateTime(nullable: false));
        }
    }
}
