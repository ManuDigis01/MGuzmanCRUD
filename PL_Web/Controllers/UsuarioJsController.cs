using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PL_Web.Controllers
{
    public class UsuarioJsController : Controller
    {
        // GET: UsuarioJs
        public ActionResult GetAll()
        {
           ViewBag.UsuarioUrl = ConfigurationManager.AppSettings["UsuariosUrl"];
            return View();
        }

     
        

    }
}