using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageService.ImageService.Infrastructure
{
    public class ReadAppConfig
    {
        //the function takes a key string and returns the value of it from app.config file
        public static string ReadSetting(string key)
        {
            try
            {
                string result = ConfigurationManager.ConnectionStrings[key].ConnectionString;
                return result;
            }
            catch (ConfigurationErrorsException)
            {
                Console.WriteLine("Error reading app settings");
                return "";
            }
        }
        //the function takes a key string and returns the size of the thumbnail
        public static int ReadThumbnailSize(string key)
        {
            try
            {
                int result = Int32.Parse(ConfigurationManager.AppSettings[key]);
                //if given size is negative, will turn in to positive
                if (result < 0)
                {
                    result = -result;
                }
                return result;
            }
            //catch if failed to convert from string to int
            catch (FormatException)
            {
                return -1;
            }
        }

        //the function takes a key string and return the source or the log (depends on the sent key)
        public static string readAppSettings(string key)
        {
            try
            {
                string result = ConfigurationManager.AppSettings[key];
                return result;
            }
            catch (ConfigurationErrorsException)
            {
                Console.WriteLine("error reading app settings");
                return "";
            }
        }
    }
}
