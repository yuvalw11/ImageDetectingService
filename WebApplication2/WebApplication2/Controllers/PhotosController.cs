﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication2.Models;
namespace WebApplication2.Controllers
{
    public class PhotosController: Controller
    {
        ImageWebModel model;

        public PhotosController()
        {
            model = ImageWebModel.GetModel();
        }

        [HttpGet]
        public ActionResult PhotosView()
        {
            return View();
        }
    }
}