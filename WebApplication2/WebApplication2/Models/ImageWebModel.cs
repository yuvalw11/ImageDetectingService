using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Xml.Linq;

namespace WebApplication2.Models
{ 

    public class ImageWebModel
    {
        public string GetStudentData(string stu, string field)
        {
            var path = HttpContext.Current.Server.MapPath(@"..\\App_Data\\StudentsData.xml");
            XDocument doc = XDocument.Load(path);
            var students = doc.Element("students");
            var student = students.Element(stu);
            return student.Element(field).Value;
        }

        public int GetPhotosNumber()
        {
            return 0;
        }

        public string GetServiceStatus()
        {
            return "Inactive";
        }
    }
}