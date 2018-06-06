using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json.Linq;
using WebApplication2.Models;

namespace WebApplication2.Controllers
{
    public class ImageWebViewController : Controller
    {
        ImageWebModel model;

        public ImageWebViewController()
        {
            model = ImageWebModel.GetModel();
        }

        [HttpGet]
        public ActionResult ImageWebView()
        {
            return View();
        }

        [HttpGet]
        public JObject GetServiceData()
        {
            JObject data = new JObject();
            data["status"] = this.model.GetServiceStatus();
            data["imageNum"] = this.model.PhotosNumber;
            return data;
        }

        [HttpGet]
        public JObject GetStudentsData()
        {
            JObject data = new JObject();
            JObject stu1 = new JObject();
            JObject stu2 = new JObject();
            JArray arr = new JArray();
            stu1["id"] = this.model.GetStudentData("student1", "id");
            stu1["firstName"] = this.model.GetStudentData("student1", "firstName");
            stu1["lastName"] = this.model.GetStudentData("student1", "lastName");
            stu2["id"] = this.model.GetStudentData("student2", "id");
            stu2["firstName"] = this.model.GetStudentData("student2", "firstName");
            stu2["lastName"] = this.model.GetStudentData("student2", "lastName");
            arr.Add(stu1);
            arr.Add(stu2);
            data["students"] = arr;
            return data;
        }
    }
}