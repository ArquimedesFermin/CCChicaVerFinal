using CChica.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CChica.Controllers
{
    [Authorize]
    public class CProcController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();


        // GET: CProc
        public ActionResult Index()
        {
            return View();
        }


        [HttpGet]
        public ActionResult addIngreso(string indentityCC)
        {


            var Identificador = (from i in db.addCajas
                                 where i.IdentityCajaChica == indentityCC
                                 select i.Id).First();

            ViewBag.Iden = indentityCC;
            ViewBag.IdentificadorId = Identificador;
            return View();
        }

        [HttpPost]
        public ActionResult addIngreso(FormCollection collection, string Indetificador)
        {



            var IdentificadorV = (from i in db.addCajas
                                  where i.IdentityCajaChica == Indetificador
                                  select i.Id).First();

            var validar = (from d in db.egresoIngresos
                           where d.IdCajaChica == IdentificadorV
                           select d).Count();


            //var CodigoId = (from d in db.egresoIngresos
            //               where d.IdCajaChica == IdentificadorV
            //               select d.Id).First();

            EgresoIngreso egresoIngreso = new EgresoIngreso();


            try
            {
                if (ModelState.IsValid)
                {
                    //Dinero
                    var money = Convert.ToDecimal(collection["Dinero"]);
                    egresoIngreso.Dinero = money;

                    

                    //IdCajaChica
                    var chica = Convert.ToInt32(IdentificadorV);
                    egresoIngreso.IdCajaChica = chica;

                    //Estado
                    int estado = 1;
                    egresoIngreso.IdEstado = estado;

                    //fecha

                    egresoIngreso.Fecha = DateTime.Now.Date;

                    //Time
                    egresoIngreso.Time = Convert.ToDateTime(DateTime.Now.ToString("hh:mm tt"));


                    //concepto
                    egresoIngreso.Concepto = collection["Concepto"];

                    //Descripcion
                    egresoIngreso.Descripcion = collection["Descripcion"];

                    var consulta = (from d in db.egresoIngresos
                                    where d.IdCajaChica == IdentificadorV
                                    orderby d.Id descending
                                    select d).Count();


                    if (consulta == 0)
                    {
                        var ConsultaCaja = db.addCajas.Where(a => a.Id == IdentificadorV).ToList();


                        foreach (var caja in ConsultaCaja)
                        {
                            caja.AperturaCaja = Convert.ToDecimal(caja.AperturaCaja.ToString().Replace("0.00", money.ToString()));


                        }
                    }

                    db.egresoIngresos.Add(egresoIngreso);


                    db.SaveChanges();

                }
                return RedirectToAction("ES", "AddCajaChica", new { id = IdentificadorV });

            }
            catch (Exception)
            {

                return View();
            }





        }



        [HttpGet]
        public ActionResult addGasto(string indentityCC)
        {


            var Identificador = (from i in db.addCajas
                                 where i.IdentityCajaChica == indentityCC
                                 select i.Id).First();

            ViewBag.t = (from r in db.egresoIngresos
                         where r.IdCajaChica == Identificador
                         select r.IdCajaChica).Count();

            ViewBag.Iden = indentityCC;

            ViewBag.IdentificadorId = Identificador;

            return View();
        }

        [HttpPost]
        public ActionResult addGasto(FormCollection collection, string Indetificador)
        {





            var IdentificadorV = (from i in db.addCajas
                                  where i.IdentityCajaChica == Indetificador
                                  select i.Id).First();




            EgresoIngreso egresoIngreso = new EgresoIngreso();


            try
            {
                if (ModelState.IsValid)
                {
                    //Dinero
                    var money = Convert.ToInt32(collection["Dinero"]);
                    egresoIngreso.Dinero = money;

                    //IdCajaChica

                    //IdCajaChica
                    var chica = Convert.ToInt32(IdentificadorV);
                    egresoIngreso.IdCajaChica = chica;

                    //Estado
                    int estado = 2;
                    egresoIngreso.IdEstado = estado;

                    //fecha

                    egresoIngreso.Fecha = DateTime.Now.Date;

                    //Time
                    egresoIngreso.Time = Convert.ToDateTime(DateTime.Now.ToString("hh:mm tt"));


                    //concepto
                    egresoIngreso.Concepto = collection["Concepto"];

                    //Descripcion
                    egresoIngreso.Descripcion = collection["Descripcion"];


                    db.egresoIngresos.Add(egresoIngreso);
                    db.SaveChanges();

                }
                return RedirectToAction("ES", "AddCajaChica", new { id = IdentificadorV });

            }
            catch (Exception)
            {

                return View();
            }

        }


    }
}