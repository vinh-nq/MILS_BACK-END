using MIS.Data.Providers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MIS.API.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Title = "Home Page";
            EthnicProvider ethnicProvider = new EthnicProvider();
            ethnicProvider.GetAllEthnic();
            return View();
        }
    }
}
