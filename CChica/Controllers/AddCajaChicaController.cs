using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Linq.SqlClient;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using CChica.Extensiones_Method;
using CChica.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.ReportingServices.ReportProcessing.ReportObjectModel;
using static CChica.Controllers.ManageController;

namespace CChica.Controllers
{
    [Authorize]
    public class AddCajaChicaController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();


        // GET: AddCajaChica

        [HttpGet]

       
        public ActionResult Index(ManageMessageId? message, string totalCierre , string idCaja, bool? validar)
        {

            if (User.IsInRole("admin") || User.IsInRole("user"))
            {



                var idCajaVb = (from e in db.addCajas
                                where e.IdentityCajaChica == idCaja
                                select e.Id).Count();




                if (idCajaVb != 0)
                {
                    //if (validar == null)
                    //{
                    //  return  RedirectToAction("ValidarForm");
                    //}
                    var usuario = (from t in db.addCajas
                                   where t.IdentityCajaChica == idCaja
                                   select t.CreadoPor).First();

                    ViewBag.usuario = usuario;
                    if (User.Identity.Name == usuario || User.IsInRole("admin"))
                    {
                        AddCajaChica add = new AddCajaChica();
                        var idCajaV = (from e in db.addCajas
                                       where e.IdentityCajaChica == idCaja
                                       select e.Id).First();

                        var valordinero = (from d in db.addCajas
                                           where d.Id == idCajaV
                                           select d.CierreCaja).First();

                        var dinero = Convert.ToString(valordinero);


                        var ConsultaCaja = db.addCajas.Where(a => a.Id == idCajaV).ToList();

                        var fechaCierre = db.addCajas.Where(a => a.Id == idCajaV).Select(a => a.FechaCierre).First().ToString();

                      //  var totalC = db.addCajas.Where(a => a.Id == idCajaV).Select(a => a.CierreCaja).First().ToString();


                        var validarCCH = db.addCajas.Where(a => a.Id == idCajaV).Select(a => a.Validada).First().ToString();

                        //Variable declarada 
                       

                        
                        
                      


                        foreach (var caja in ConsultaCaja)
                        {
                            caja.CierreCaja = Convert.ToDecimal(caja.CierreCaja.ToString().Replace(dinero, totalCierre));
                            caja.FechaCierre = Convert.ToDateTime(caja.FechaCierre.ToString().Replace(fechaCierre, Convert.ToString(DateTime.Now)));
                            caja.Validada = Convert.ToBoolean(caja.Validada.ToString().Replace(validarCCH, Convert.ToString(validar)));


                        }
                        
                    }
                    else
                    {
                        ViewBag.auten = false;
                        ViewBag.StatusMessage =
           message == ManageMessageId.ChangePasswordSuccess ? "La contraseña del usuario se ha cambiado exitosamente."
           :
           "";

                        return View();
                    }

                } //Fin de la condición

                db.SaveChanges();
                ViewBag.StatusMessage =
   message == ManageMessageId.ChangePasswordSuccess ? "La contraseña del usuario se ha cambiado exitosamente."
   :
   "";
                return View();


            }
            else
            {
                return RedirectToAction("Login", "Account");
            }


        }

        public JsonResult Indexd()
        {





            var consulta = (from i in db.addCajas
                                //where i.nameCajaChica.Contains(buscar) || i.fecha.Contains(buscar)
                            orderby i.Id ascending
                            select i);

            return Json(consulta.ToList(), JsonRequestBehavior.AllowGet);





        }





        [HttpGet]
        public ActionResult Details(int id)
        {
            var consulta = db.addCajas.Single(a => a.Id == id);

            return View(consulta);

        }

        [HttpGet]
        public ActionResult Create()
        {
            ViewBag.user = db.Users.Select(a => new { a.UserName }).ToList();



            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public ActionResult Create(FormCollection collection, AddCajaChica add)
        {
            var fechaDefaul = "1900-01-01";
            int ultimoId;
            string convestr;
            string identt;

            ultimoId = (from i in db.addCajas
                        orderby i.Id descending
                        select i.Id).Count();


            if (ultimoId == 0)
            {
                ultimoId = 1;
            }
            else
            {
                ultimoId = (from i in db.addCajas
                            orderby i.Id descending
                            select i.Id).First();

                ultimoId++;
            }

            convestr = ultimoId.ToString();

            identt = "CC-0" + convestr;


            //-----------------------------------------------------------
            AddCajaChica addCajaChica = new AddCajaChica();

            if (ModelState.IsValid)
            {

                //Numero de identificacion de la caja chica

                addCajaChica.IdentityCajaChica = identt;

                //Nombre de la Caja Chica
                addCajaChica.nameCajaChica = Convert.ToString(collection["nameCajaChica"]);

                //Quien creo la caja chica
                addCajaChica.CreadoPor = User.Identity.GetUserName();

                //Validate
                addCajaChica.Validada = Convert.ToBoolean(0);

                //Fecha
                addCajaChica.fecha = DateTime.Today.Date;

                //Fecha Cierre
                addCajaChica.FechaCierre = Convert.ToDateTime(fechaDefaul);

                //Fecha apertura

                addCajaChica.fechaApertura = Convert.ToDateTime(fechaDefaul);

                db.addCajas.Add(addCajaChica);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            else
            {
                return View();
            }

        }

        [HttpGet]
        [System.Web.Mvc.OutputCache(NoStore = true, Duration = 0, VaryByParam = "*")]

        public ActionResult Edit(int? id, bool? url, bool? chk)
        {
            ViewBag.Validacion = (from d in db.addCajas
                                  where d.Id == id
                                  select d.Validada).First();



            var usuario = (from t in db.addCajas
                           where t.Id == id
                           select t.CreadoPor).First();

            ViewBag.Usuario = usuario;



            if (url != null)
            {
                if (User.Identity.Name == usuario || User.IsInRole("admin"))
                {

                    if (id == null)
                    {
                        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                    }

                    AddCajaChica consulta = db.addCajas.Find(id);

                    if (chk != null)
                    {
                        consulta.Validada = Convert.ToBoolean(chk);

                    }

                    if (consulta == null)
                    {
                        return HttpNotFound();
                    }
                    return View(consulta);
                }
                else
                {
                    ViewBag.auten = false;
                    return View();
                }
            }
            else
            {
                AddCajaChica consulta = db.addCajas.Find(id);

                return View(consulta);
            }

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,IdentityCajaChica,nameCajaChica,CreadoPor,Validada,fecha,AperturaCaja,fechaApertura,CierreCaja,FechaCierre")] AddCajaChica edit)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Entry(edit).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                else
                {
                    return View();
                }
            }
            catch (Exception)
            {

                return HttpNotFound();
            }


        }


        [HttpGet]
        [System.Web.Mvc.OutputCache(NoStore = true, Duration = 0, VaryByParam = "*")]

        public ActionResult Delete(int? Id)
        {
            ViewBag.Validacion = (from d in db.addCajas
                                  where d.Id == Id
                                  select d.Validada).First();

            if (Id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            AddCajaChica addCajaChica = db.addCajas.Find(Id);

            if (addCajaChica == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            }
            return View(addCajaChica);

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Delete")]
        public ActionResult DeleteConfirmation(int? Id)
        {



            if (Id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AddCajaChica addCajaChica = db.addCajas.Find(Id);
            db.addCajas.Remove(addCajaChica);
            db.SaveChanges();

            if (addCajaChica == null)
            {
                return HttpNotFound();
            }
            return RedirectToAction("DeleteEgreso", new { id = Id });


        }

        public ActionResult DeleteEgreso(int? id)
        {

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            // EgresoIngreso egresoIngreso = db.egresoIngresos.Single(a => a.IdCajaChica == id);
            db.egresoIngresos.RemoveRange(db.egresoIngresos.Where(c => c.IdCajaChica == id));
            db.SaveChanges();

            //if (egresoIngreso == null)
            //{
            //    return HttpNotFound();
            //}
            return RedirectToAction("Index");

        }
        [System.Web.Mvc.OutputCache(NoStore = true, Duration = 0, VaryByParam = "*")]
        public ActionResult ES(FormCollection collection, int? id)
        {
            ViewBag.Validacion = (from d in db.addCajas
                                  where d.Id == id
                                  select d.Validada).First();



            ViewBag.Validar = (from e in db.egresoIngresos
                               where e.IdCajaChica == id
                               select e).Count();


            if (ViewBag.Validar != 0)
            {
                ViewBag.PrimerId = (from e in db.egresoIngresos
                                    orderby e.Id descending
                                    where e.IdCajaChica == id
                                    select e.Id).Min();
            }
            else
            {
                ViewBag.PrimerId = null;
            }




            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var DetailsQuery = db.addCajas.Find(id);

            ViewBag.lista = DetailsQuery;


            //---------------------------------
            List<ConsultaCChica> joinE_I_Ts = new List<ConsultaCChica>();

            string valorBuscar = collection["buscar"];
            if (valorBuscar == null)
            {
                var consulta = (from t in db.egresoIngresos
                                join d in db.TipoEstados on t.IdEstado equals d.Id
                                where (t.IdCajaChica == id)

                                select new { t.Id, d.Estado, t.IdCajaChica, t.Concepto, t.Fecha, t.Time, t.Descripcion, t.Dinero }).ToList();

                foreach (var item in consulta)
                {

                    joinE_I_Ts.Add(new ConsultaCChica()
                    {
                        id = item.Id,
                        Descripcion = item.Descripcion,
                        idCajaChicau = Convert.ToInt32(item.IdCajaChica),
                        Concepto = item.Concepto,
                        fecha = Convert.ToDateTime(item.Fecha),
                        Time = Convert.ToDateTime(item.Time),
                        estado = item.Estado,
                        Dinero = item.Dinero


                    });

                }


                var pivotTable = joinE_I_Ts.ToPivotTable(
                    item => item.estado,
                    item => item.id,
                    items => items.Any() ? Convert.ToString(items.First().Dinero) : null);






                List<CcConsultaChicaInv> ccConsultaChicaInvs = new List<CcConsultaChicaInv>();


                var query = from r in ccConsultaChicaInvs
                            select r;

                string gasto;

                string Ingreso;

                foreach (DataRow row in pivotTable.Rows)
                {

                    if (row.ItemArray.Count() != 3)
                    {
                        gasto = "0.00";

                    }
                    else
                    {
                        gasto = row.ItemArray[2].ToString();
                    }


                    if (row.ItemArray[1].ToString() == "")
                    {
                        Ingreso = "0.00";
                    }
                    else
                    {
                        Ingreso = row.ItemArray[1].ToString();
                    }

                    if (gasto == "0.00")
                    {
                        gasto = "0.00";
                    }
                    else if (row.ItemArray[2].ToString() == "")
                    {
                        gasto = "0.00";
                    }
                    else
                    {
                        gasto = row.ItemArray[2].ToString();
                    }


                    ccConsultaChicaInvs.Add(new CcConsultaChicaInv()
                    {
                        id = row.ItemArray[0].ToString(),
                        Ingreso = Ingreso,
                        Gasto = gasto,
                        Fecha = joinE_I_Ts.Single(a => a.id == Convert.ToInt32(row[0])).fecha,
                        Time = joinE_I_Ts.Single(a => a.id == Convert.ToInt32(row[0])).Time.ToString(),
                        idCajaChica = joinE_I_Ts.Single(a => a.id == Convert.ToInt32(row[0])).idCajaChicau,

                        Descripcion = joinE_I_Ts.Single(a => a.id == Convert.ToInt32(row[0])).Descripcion,
                        Concepto = joinE_I_Ts.Single(a => a.id == Convert.ToInt32(row[0])).Concepto



                    });



                }

                ViewBag.EG = query.ToList();



                return View();

            }
            else
            {

                var consulta = (from t in db.egresoIngresos
                                join d in db.TipoEstados on t.IdEstado equals d.Id
                                where (t.IdCajaChica == id) && (t.Concepto.Contains(valorBuscar))

                                select new { t.Id, d.Estado, t.IdCajaChica, t.Concepto, t.Fecha, t.Time, t.Descripcion, t.Dinero }).ToList();

                foreach (var item in consulta)
                {

                    joinE_I_Ts.Add(new ConsultaCChica()
                    {
                        id = item.Id,
                        Descripcion = item.Descripcion,
                        idCajaChicau = Convert.ToInt32(item.IdCajaChica),
                        Concepto = item.Concepto,
                        fecha = Convert.ToDateTime(item.Fecha),
                        Time = Convert.ToDateTime(item.Time),
                        estado = item.Estado,
                        Dinero = item.Dinero


                    });

                }


                var pivotTable = joinE_I_Ts.ToPivotTable(
                    item => item.estado,
                    item => item.id,
                    items => items.Any() ? Convert.ToString(items.First().Dinero) : null);






                List<CcConsultaChicaInv> ccConsultaChicaInvs = new List<CcConsultaChicaInv>();


                var query = from r in ccConsultaChicaInvs
                            select r;

                string gasto;

                string Ingreso;

                foreach (DataRow row in pivotTable.Rows)
                {

                    if (row.ItemArray.Count() != 3)
                    {
                        gasto = "0.00";

                    }
                    else
                    {
                        gasto = row.ItemArray[2].ToString();
                    }


                    if (row.ItemArray[1].ToString() == "")
                    {
                        Ingreso = "0.00";
                    }
                    else
                    {
                        Ingreso = row.ItemArray[1].ToString();
                    }

                    if (gasto == "0.00")
                    {
                        gasto = "0.00";
                    }
                    else if (row.ItemArray[2].ToString() == "")
                    {
                        gasto = "0.00";
                    }
                    else
                    {
                        gasto = row.ItemArray[2].ToString();
                    }


                    ccConsultaChicaInvs.Add(new CcConsultaChicaInv()
                    {
                        id = row.ItemArray[0].ToString(),
                        Ingreso = Ingreso,
                        Gasto = gasto,
                        Fecha = joinE_I_Ts.Single(a => a.id == Convert.ToInt32(row[0])).fecha,
                        Time = joinE_I_Ts.Single(a => a.id == Convert.ToInt32(row[0])).Time.ToString(),
                        idCajaChica = joinE_I_Ts.Single(a => a.id == Convert.ToInt32(row[0])).idCajaChicau,

                        Descripcion = joinE_I_Ts.Single(a => a.id == Convert.ToInt32(row[0])).Descripcion,
                        Concepto = joinE_I_Ts.Single(a => a.id == Convert.ToInt32(row[0])).Concepto



                    });



                }

                ViewBag.EG = query.ToList();



                return View();
            }



        }







        [HttpGet]
        [System.Web.Mvc.OutputCache(NoStore = true, Duration = 0, VaryByParam = "*")]

        public ActionResult EditEgresoIngreso(int? Id, bool? url)
        {
            var idCajaChica = (from s in db.egresoIngresos
                               where s.Id == Id
                               select s.IdCajaChica).First();

            ViewBag.Validacion = (from d in db.addCajas
                                  where d.Id == idCajaChica
                                  select d.Validada).First();


            var IdCajaChica = (from i in db.egresoIngresos
                               where i.Id == Id
                               select i.IdCajaChica).First();

            var usuario = (from t in db.addCajas
                           where t.Id == IdCajaChica
                           select t.CreadoPor).First();

            ViewBag.Usuario = usuario;

            if (url != null)
            {
                if (User.Identity.Name == usuario || User.IsInRole("admin"))
                {

                    if (Id == null)
                    {
                        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                    }
                    ViewBag.estados = db.TipoEstados.Select(x => new { ID = x.Id, Estado = x.Estado }).ToList();

                    var idConsulta = db.egresoIngresos.Where(a => a.Id == Id).Select(e => e.IdCajaChica).First();

                    ViewBag.Id = idConsulta;

                    ViewBag.ConsultaCount = db.egresoIngresos.Where(a => a.IdCajaChica == idConsulta).Min(a => a.Id);

                    TempData["DataId"] = Id;


                    var consulta = db.egresoIngresos.Find(Id);

                    if (consulta == null)
                    {
                        HttpNotFound();
                    }


                    return View(consulta);
                }
                else
                {
                    ViewBag.auten = false;
                    return View();
                }
            }
            else
            {
                var urlEdit = "/CChica/AddCajaChica/EditEgresoIngreso?Id=" + Id + "&url=true";
                //var urlEdit = "/AddCajaChica/EditEgresoIngreso?Id=" + Id + "&url=true";

                return RedirectToAction("Login", "Account", new { returnUrl = urlEdit });
            }
        }


        [HttpPost]
        [ValidateAntiForgeryToken]

        public ActionResult EditEgresoIngreso(FormCollection collection, [Bind(Include = "Id,IdCajaChica,Fecha,Time,Concepto,IdEstado,Dinero,Descripcion")] EgresoIngreso egresoIngreso)
        {
            var fa = Convert.ToInt32(collection["IdCajaChica"]);

            var DineroAper = (from d in db.addCajas
                              where d.Id == fa
                              select d.AperturaCaja).First().ToString();

            var FechaApertura = (from d in db.addCajas
                                 where d.Id == fa
                                 select d.fechaApertura).First().ToString();

            var minimoValor = (from d in db.egresoIngresos
                               where d.IdCajaChica == fa
                               select d.Id).Min();


            try
            {
                if (ModelState.IsValid)
                {

                    db.Entry(egresoIngreso).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();
                    //Modificar apertura
                    var varId = Convert.ToInt32(collection["Id"]);

                    if (varId == minimoValor)
                    {
                        var f = Convert.ToInt32(collection["IdCajaChica"]);
                        var dineroApertura = db.addCajas.Where(a => a.Id == f).ToList();
                        foreach (var EI in dineroApertura)
                        {
                            EI.AperturaCaja = Convert.ToDecimal(EI.AperturaCaja.ToString().Replace(DineroAper, Convert.ToString(collection["Dinero"])));
                            EI.fechaApertura = Convert.ToDateTime(EI.fechaApertura.ToString().Replace(FechaApertura, Convert.ToString(DateTime.Now)));
                        }

                        db.SaveChanges();
                    }

                    //Fin de modificar apertura

                    return RedirectToAction("ES", new { id = egresoIngreso.IdCajaChica });
                }
                else
                {
                    return View();
                }


            }
            catch (Exception)
            {

                return View();
            }




        }


        [HttpGet]
        [System.Web.Mvc.OutputCache(NoStore = true, Duration = 0, VaryByParam = "*")]

        public ActionResult DeleteEgresiIngreso(int? id, bool? url)
        {
            var idCajaChica = (from s in db.egresoIngresos
                               where s.Id == id
                               select s.IdCajaChica).First();

            ViewBag.Validacion = (from d in db.addCajas
                                  where d.Id == idCajaChica
                                  select d.Validada).First();


            var IdCajaChica = (from i in db.egresoIngresos
                               where i.Id == id
                               select i.IdCajaChica).First();

            var usuario = (from t in db.addCajas
                           where t.Id == IdCajaChica
                           select t.CreadoPor).First();

            ViewBag.Usuario = usuario;

            if (url != null)
            {
                if (User.Identity.Name == usuario || User.IsInRole("admin"))
                {
                    var consulta = db.egresoIngresos.Find(id);

                    var idConsuta = db.egresoIngresos.Where(a => a.Id == id).Select(a => a.IdCajaChica).First();


                    ViewBag.consulta = idConsuta;


                    return View(consulta);
                }
                else
                {
                    ViewBag.auten = false;
                    return View();
                }
            }
            else
            {
                var urlEdit = "/CChica/AddCajaChica/DeleteEgresiIngreso?Id=" + id + "&url=true";

                //var urlEdit = "/AddCajaChica/DeleteEgresiIngreso?Id=" + id + "&url=true";
                return RedirectToAction("Login", "Account", new { returnUrl = urlEdit });
            }
        }



        [HttpPost]
        [ValidateAntiForgeryToken]

        public ActionResult DeleteEgresiIngreso(FormCollection collection, int? id)
        {


            var idCajaChica = (from x in db.egresoIngresos
                               where x.Id == id
                               select x.IdCajaChica).First();




            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var idVar = db.egresoIngresos.Where(a => a.Id == id).Select(e => e.IdCajaChica).First();


            var consulta = db.egresoIngresos.Find(id);
            db.egresoIngresos.Remove(consulta);
            db.SaveChanges();

            DateTime thisDate2 = new DateTime(1900, 1, 1, 00, 00, 00);

            var ConsultaCaja = db.addCajas.Where(a => a.Id == idCajaChica).ToList();
            var dineroApertuta = db.addCajas.Where(a => a.Id == idCajaChica).Select(a => a.AperturaCaja).First().ToString();
            var dineroCierre = db.addCajas.Where(a => a.Id == idCajaChica).Select(a => a.CierreCaja).First().ToString();
            var FechaApertura = db.addCajas.Where(a => a.Id == idCajaChica).Select(a => a.fechaApertura).First().ToString();
            var FechaCierre = db.addCajas.Where(a => a.Id == idCajaChica).Select(a => a.FechaCierre).First().ToString();

            if (ConsultaCaja != null)
            {
                foreach (var caja in ConsultaCaja)
                {
                    caja.AperturaCaja = Convert.ToDecimal(caja.AperturaCaja.ToString().Replace(dineroApertuta, "0.00"));
                    caja.CierreCaja = Convert.ToDecimal(caja.CierreCaja.ToString().Replace(dineroCierre, "0.00"));
                    caja.fechaApertura = Convert.ToDateTime(caja.fechaApertura.ToString().Replace(FechaApertura, thisDate2.ToString()));
                    caja.FechaCierre = Convert.ToDateTime(caja.FechaCierre.ToString().Replace(FechaCierre, thisDate2.ToString()));



                }
                db.SaveChanges();

            }





            if (consulta == null)
            {
                HttpNotFound();
            }



            return RedirectToAction("ES", new { id = idVar });

        }

        [HttpGet]
        public ActionResult BusquedaGeneral()
        {
            return View();

        }

        [HttpPost]


        public JsonResult BusquedaGeneralJ()
        {

            //---------------------------------
            List<ConsultaCChica> joinE_I_Ts = new List<ConsultaCChica>();



            var consulta = (from t in db.egresoIngresos
                            join d in db.TipoEstados on t.IdEstado equals d.Id
                            join caja in db.addCajas on t.IdCajaChica equals caja.Id

                            select new { t.Id, caja.IdentityCajaChica, d.Estado, t.IdCajaChica, t.Concepto, t.Fecha, t.Time, t.Descripcion, t.Dinero }).ToList();




            foreach (var item in consulta)
            {

                joinE_I_Ts.Add(new ConsultaCChica()
                {
                    id = item.Id,
                    cajachica = item.IdentityCajaChica,
                    Descripcion = item.Descripcion,
                    idCajaChicau = Convert.ToInt32(item.IdCajaChica),
                    Concepto = item.Concepto,
                    fecha = Convert.ToDateTime(item.Fecha),
                    Time = Convert.ToDateTime(item.Time),
                    estado = item.Estado,
                    Dinero = item.Dinero


                });

            }


            var pivotTable = joinE_I_Ts.ToPivotTable(
                item => item.estado,
                item => item.id,
                items => items.Any() ? Convert.ToString(items.First().Dinero) : null);






            List<CcConsultaChicaInv> ccConsultaChicaInvs = new List<CcConsultaChicaInv>();


            var query = from r in ccConsultaChicaInvs
                        select r;

            string gasto;

            string Ingreso;

            foreach (DataRow row in pivotTable.Rows)
            {

                if (row.ItemArray.Count() != 3)
                {
                    gasto = "0.00";

                }
                else
                {
                    gasto = row.ItemArray[2].ToString();
                }


                if (row.ItemArray[1].ToString() == "")
                {
                    Ingreso = "0.00";
                }
                else
                {
                    Ingreso = row.ItemArray[1].ToString();
                }

                if (gasto == "0.00")
                {
                    gasto = "0.00";
                }
                else if (row.ItemArray[2].ToString() == "")
                {
                    gasto = "0.00";
                }
                else
                {
                    gasto = row.ItemArray[2].ToString();
                }


                ccConsultaChicaInvs.Add(new CcConsultaChicaInv()
                {
                    id = row.ItemArray[0].ToString(),
                    Ingreso = Ingreso,
                    Gasto = gasto,
                    Fecha = joinE_I_Ts.Single(a => a.id == Convert.ToInt32(row[0])).fecha,
                    Time = joinE_I_Ts.Single(a => a.id == Convert.ToInt32(row[0])).Time.ToString(),
                    idCajaChica = joinE_I_Ts.Single(a => a.id == Convert.ToInt32(row[0])).idCajaChicau,
                    cajaChica = joinE_I_Ts.Single(a => a.id == Convert.ToInt32(row[0])).cajachica,
                    Descripcion = joinE_I_Ts.Single(a => a.id == Convert.ToInt32(row[0])).Descripcion,
                    Concepto = joinE_I_Ts.Single(a => a.id == Convert.ToInt32(row[0])).Concepto



                });



            }








            return Json(query.ToList(), JsonRequestBehavior.AllowGet);

        }



        public ActionResult Informacion(int? id)
        {
            DateTime thisDate2 = new DateTime(1900, 1, 1, 00, 00, 00);

            ViewBag.FechaEstandar = Convert.ToDateTime(thisDate2);



            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var consulta = db.addCajas.Find(id);


            return View(consulta);


        }


    }
}