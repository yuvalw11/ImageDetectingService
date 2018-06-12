using Infrustructure;
using ServiceGuiComunication;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Xml.Linq;

namespace WebApplication2.Models
{

    public class ImageWebModel
    {
        public static ImageWebModel model = null;

        public string OutputDir { get; set; }
        public string SourceDir { get; set; }
        public string LogName { get; set; }
        public int ThumnailSize { get; set; }
        public string[] InputDirs { get; set; }
        public List<LogData> logs { get; set; }
        private ComunicationClient client;

        public int PhotosNumber
        {
            get
            {
                if (this.OutputDir == null || !Directory.Exists(this.OutputDir))
                {
                    return 0;
                }
                string[] dirs = Directory.GetDirectories(this.OutputDir);
                int count = 0;
                foreach (string dir in dirs) 
                {
                    int num;
                    string dirName = Path.GetFileName(dir);
                    if (int.TryParse(dirName, out num))
                    {
                        foreach(string indir in Directory.GetDirectories(dir))
                        {
                            count += Directory.GetFiles(indir).Length;
                        }
                    }
                }
                return count;
            }
        }

        private ImageWebModel()
        {
            client = ComunicationClient.GetClient(8000);
        }
        public void HandlerToDelete(string handler)
        {
            string[] args = new string[1];
            args[0] = handler;
            client.sendCommand((int)CommandsEnum.RemoveDirCommand, args);

        }

        public void DeletePhoto(string name, string date)
        {
            string path = this.OutputDir + "/" + date + "/" + name;
            string thumbPath = this.OutputDir + "/thumbnails/" + date + "/" + name;
            File.Delete(path);
            File.Delete(thumbPath);
        }

        public static ImageWebModel GetModel()
        {
            if (model == null)
            {
                ImageWebModel.model = new ImageWebModel();
            }
            return ImageWebModel.model;
        }


        public string GetStudentData(string stu, string field)
        {
            var path = HttpContext.Current.Server.MapPath(@"..\\App_Data\\StudentsData.xml");
            XDocument doc = XDocument.Load(path);
            var students = doc.Element("students");
            var student = students.Element(stu);
            return student.Element(field).Value;
        }

        public string GetServiceStatus()
        {
            ComunicationClient client = ComunicationClient.GetClient(8000);
            if (client.isConnected())
            {
                return "active";
            }
            else
            {
                return "inactive";
            }
        }
    }
}