using CChica.Models;
using CChica.Report;
using Microsoft.Reporting.WebForms;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;

namespace CChica.Controllers
{
    [Authorize]
    public class ReportController : Controller
    {

        private ApplicationDbContext db = new ApplicationDbContext();


        // GET: Report
        public ActionResult Report(string id)
        {

            var consulta = db.addCajas.Where(a => a.IdentityCajaChica == id).Select(v => v.Id).First();

            ViewBag.Consulta = consulta;

            string Query = "SELECT convert(char(10), Fecha,103) Fecha,CONVERT(VARCHAR(5), Time, 108) + ' ' + RIGHT(CONVERT(VARCHAR(30), Time, 9),2) Hora ,Ingreso,Gasto,Concepto,Descripcion FROM   ( select e.Id, e.IdCajaChica, Dinero, t.Estado, e.Fecha, Time, e.Concepto, e.Descripcion from EgresoIngresoes e inner join TipoEstadoes t on e.IdEstado = t.Id inner join AddCajaChicas c on e.IdCajaChica = c.Id where e.IdCajaChica = "+consulta +" )p pivot ( Max(Dinero) for Estado IN ([Ingreso],[Gasto]) )  as v";



            CChicaDS dS = new CChicaDS(); 
            ReportViewer rptViewer = new ReportViewer();
            ReportParameter[] parameters = new ReportParameter[1];

            // ProcessingMode will be Either Remote or Local  
            rptViewer.ProcessingMode = ProcessingMode.Local;
            rptViewer.SizeToReportContent = true;
            rptViewer.Height = Unit.Pixel(1100);
          
            //Connection a sql server
            var connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

            SqlConnection cn = new SqlConnection(connectionString);
            SqlDataAdapter adapter = new SqlDataAdapter(Query, cn);
            adapter.Fill(dS,dS.CChica.TableName);

        
            rptViewer.LocalReport.DataSources.Add(new ReportDataSource("CChicaDataSet", dS.Tables[0]));
            rptViewer.LocalReport.ReportPath = @"Report/CChicaReport.rdlc";
            ViewBag.ReportViewer = rptViewer;
            return View();
        }
    }
}