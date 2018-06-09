using Infrustructure;
using Newtonsoft.Json.Linq;
using ServiceGuiComunication;
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

        [HttpGet]
        public JArray getLogs()
        {
            JArray array = new JArray();
            List < LogData > logsData = this.model.logs;
            if (logsData == null)
            {
                return null;
            }
            List<string[]> logs = new List<string[]>();

            foreach(LogData logData in logsData)
            {
                JObject data = new JObject();
                data["type"] = ((MessageTypeEnum)logData.Type).ToString();
                data["message"] = logData.Message;
                array.Add(data);
            }
            return array;
        }
    }
}