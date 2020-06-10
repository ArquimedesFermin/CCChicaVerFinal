namespace CChica.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addTime : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.EgresoIngresoes", "Time", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.EgresoIngresoes", "Time");
        }
    }
}
