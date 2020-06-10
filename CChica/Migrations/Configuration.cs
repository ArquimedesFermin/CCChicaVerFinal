namespace CChica.Migrations
{
    using CChica.Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<CChica.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(CChica.Models.ApplicationDbContext context)
        {
            context.TipoEstados.AddOrUpdate(x => x.Id,
  
            new TipoEstado {Id=1,Estado = "Ingreso"},
            new TipoEstado {Id=2,Estado = "Gasto" }



            );
        }
    }
}
