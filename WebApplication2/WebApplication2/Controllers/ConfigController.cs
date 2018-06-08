﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication2.Models;
namespace WebApplication2.Controllers
{
    public class ConfigController: Controller
    {
        ImageWebModel model;

        public ConfigController()
        {
            model = ImageWebModel.GetModel();
        }

        [HttpGet]
        public ActionResult ConfigView()
        {
            return View();
        }
    }
}