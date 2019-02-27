using log4net;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApplicationLog4Net.Controllers
{
    public class HomeController : Controller
    {
        private static readonly ILog mLog = LogManager.GetLogger(typeof(HomeController));

        Logger logger = new Logger();

        public ActionResult Index()
        {
            //Debe ir donde inicia el proyecto
            //Se inicializa una sola vez
            logger.iniciarConfiguracion();

            mLog.Warn(logger.FormatMessage("Error DB","Información de mensaje WARNING!!!"));

            return View();
        }

        public ActionResult About()
        {
            mLog.Info(logger.FormatMessage("Error APP", "Información de mensaje INFO!!!"));

            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}