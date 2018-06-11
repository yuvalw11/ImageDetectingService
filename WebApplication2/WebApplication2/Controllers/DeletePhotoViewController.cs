using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication2.Models;

namespace WebApplication2.Controllers
{
    public class DeletePhotoViewController : Controller
    {
        private ImageWebModel model;
        public DeletePhotoViewController()
        {
            model = ImageWebModel.GetModel();
        }

        // GET: DeletePhotoView
        public ActionResult DeletePhotoView()
        {
            return View();
        }

        [HttpPost]
        public void DeletePhoto(string name, string date)
        {
            this.model.DeletePhoto(name, date);
        }
    }
}