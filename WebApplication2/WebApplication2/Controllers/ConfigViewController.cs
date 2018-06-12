using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication2.Models;
using Newtonsoft.Json.Linq;
namespace WebApplication2.Controllers
{
    public class ConfigViewController : Controller
    {
        ImageWebModel model;
        string handlerToDelete = null;
        public ConfigViewController()
        {
            model = ImageWebModel.GetModel();
        }

        [HttpGet]
        public ActionResult ConfigView()
        {
            return View();
        }

        [HttpGet]
        public ActionResult HandlerToDeleteView(string handler)
        {
            this.handlerToDelete = handler;
            return View();
        }
        [HttpGet]
        public ActionResult DeleteHandler()
        {
            this.model.HandlerToDelete(this.handlerToDelete);
            return RedirectToAction("ConfigView");
        }
        [HttpGet]
        public ActionResult CancelHandlerDeletion()
        {
            return RedirectToAction("ConfigView");
        }
        [HttpGet]
        public JObject GetConfig()
        {
            JObject config = new JObject();
            config["OutputDir"] = this.model.OutputDir;
            config["SourceName"] = this.model.SourceDir;
            config["LogName"] = this.model.LogName;
            if (this.model.GetServiceStatus().Equals("inactive"))
            {
                config["ThumbnailSize"] = "";
            }
            else
            {
                config["ThumbnailSize"] = this.model.ThumnailSize;
            }
            return config;
        }
        [HttpGet]
        public JArray GetHandlers()
        {
            JArray handlers = new JArray();
            string[] handlersArr = this.model.InputDirs;
            try
            {
                foreach (string handler in handlersArr)
                {
                    JObject handlerObj = new JObject();
                    handlerObj["Handlers"] = handler;
                    handlers.Add(handlerObj);
                }
                return handlers;
            }
            catch (Exception)
            {
                return handlers;
            }
        }
    }
}