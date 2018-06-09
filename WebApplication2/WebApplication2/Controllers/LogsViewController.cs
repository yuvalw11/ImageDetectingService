using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication2.Models;
namespace WebApplication2.Controllers
{
    public class LogsViewController: Controller
    {
        ImageWebModel model;

        public LogsViewController()
        {
            model = ImageWebModel.GetModel();
        }

        [HttpGet]
        public ActionResult LogsView()
        {
            return View();
        }
    }
}