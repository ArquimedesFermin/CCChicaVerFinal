namespace CChica.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ArreglosLeo : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AddCajaChicas", "AperturaCaja", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.AddCajaChicas", "CierreCaja", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.EgresoIngresoes", "Concepto", c => c.String(nullable: false));
            AlterColumn("dbo.EgresoIngresoes", "Descripcion", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.EgresoIngresoes", "Descripcion", c => c.String());
            AlterColumn("dbo.EgresoIngresoes", "Concepto", c => c.String());
            DropColumn("dbo.AddCajaChicas", "CierreCaja");
            DropColumn("dbo.AddCajaChicas", "AperturaCaja");
        }
    }
}
