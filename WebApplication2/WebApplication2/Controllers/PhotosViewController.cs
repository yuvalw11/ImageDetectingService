using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication2.Models;
namespace WebApplication2.Controllers
{
    public class PhotosViewController: Controller
    {
        ImageWebModel model;

        public PhotosViewController()
        {
            model = ImageWebModel.GetModel();
        }

        [HttpGet]
        public ActionResult PhotosView()
        {
            return View();
        }

        [HttpGet]
        public JArray GetPhotosData()
        {
            JArray data = new JArray();
            string path = this.model.OutputDir;
            if (!Directory.Exists(path))
            {
                return null;
            }
            string[] dates = Directory.GetDirectories(path);
            for(int i = 0; i < dates.Length; i++)
            {
                string year = Path.GetFileName(dates[i]);
                string[] months = Directory.GetDirectories(dates[i]);
                for (int z = 0; z < months.Length; z++)
                {
                    JObject d = new JObject();
                    string month = Path.GetFileName(months[z]);
                    d["date"] = year + "/" + month;
                    JArray photos = new JArray();
                    string[] photosUrl = Directory.GetFiles(months[z]);
                    for (int j = 0; j < photosUrl.Length; j++)
                    {
                        photos.Add(photosUrl[j]);
                    }
                    d["photos"] = photos;
                    data.Add(d);
                }
               
            }
            return data;
        }
    }
}