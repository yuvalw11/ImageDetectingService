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
        public JArray GetTumbnailData()
        {
            JArray data = new JArray();
            string path = this.model.OutputDir + @"\thumbnails";
            string[] dates = Directory.GetDirectories(path);
            for(int i = 0; i < dates.Length; i++)
            {
                JObject d = new JObject();
                d["date"] = Path.GetDirectoryName(dates[i]);
                JArray photos = new JArray();
                string[] photosUrl = Directory.GetFiles(dates[i]);
                for(int j = 0; j < photosUrl.Length; j++)
                {
                    photos.Add(photosUrl[j]);
                }
                d["photos"] = photos;
            }
            return data;
        }
    }
}