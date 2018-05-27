using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceGuiComunication
{
    //converts objects to json and vice versa for the data transfer between the server and the clients
    public class JsonConvertor
    {
        //creates a JsonCommand obj by a string
        public static JsonCommand GenerateJsonCommandObject(string json)
        {
            JObject jsonObj = JObject.Parse(json);
            JsonCommand jc = new JsonCommand((int)jsonObj["commandID"], jsonObj["args"].ToObject<string[]>(), (bool)jsonObj["result"], (string)jsonObj["jsonData"]);

            return jc;
        }

        //creates a string to represent a jsoncommand obj
        public static string GenerateJsonCommandString(JsonCommand jc)
        {
            JObject json = new JObject();
            json["commandID"] = jc.CommandID;
            json["args"] = new JArray(jc.Args);
            json["result"] = jc.Result;
            json["jsonData"] = jc.JsonData;
            return json.ToString();
        }

        //converts a string to ConfigFileData obj
        public static string ConvertToJson(ConfigFileData cfd)
        {
            JObject json = new JObject();
            json["outputDir"] = cfd.OutputDir;
            json["sourceDir"] = cfd.SourceDir;
            json["logName"] = cfd.LogName;
            json["thumnailSize"] = cfd.ThumnailSize;
            json["inputDirs"] = new JArray(cfd.InputDirs);

            return json.ToString();
        }

        //generates a string representation for a ConfigFileData obj
        public static ConfigFileData ConvertToConfigFileData(string json)
        {
            JObject jsonObj = JObject.Parse(json);
            ConfigFileData cfd = new ConfigFileData();
            cfd.OutputDir = (string)jsonObj["outputDir"];
            cfd.SourceDir = (string)jsonObj["sourceDir"];
            cfd.LogName = (string)jsonObj["logName"];
            cfd.ThumnailSize = (int)jsonObj["thumnailSize"];
            JArray inputDirs = JArray.Parse(jsonObj["inputDirs"].ToString());


            cfd.InputDirs = new string[inputDirs.Count];
            int i = 0;
            foreach (JValue val in inputDirs)
            {
                cfd.InputDirs[i++] = val.ToString();
            }

            return cfd;
        }

        //generates a string representation for a LogData obj
        public static string ConvertToJson(LogData ld)
        {
            JObject json = new JObject();
            json["message"] = ld.Message;
            json["type"] = ld.Type;

            return json.ToString();
        }

        //converts a string to LogData obj
        public static LogData ConvertToLogData(string json)
        {
            JObject jsonObj = JObject.Parse(json);
            LogData ld = new LogData();
            ld.Message = (string)jsonObj["message"];
            ld.Type = (int)jsonObj["type"];

            return ld;
        }

        //generates a string representation for a List<LogData> obj
        public static string ConvertToJson(List<LogData> lds)
        {
            JArray logs = new JArray();
            foreach (LogData ld in lds)
            {
                JObject log = new JObject();
                log["type"] = ld.Type;
                log["message"] = ld.Message;
                logs.Add(log);
            }

            return logs.ToString();
        }

        //converts a string to List<LogData> obj
        public static List<LogData> ConvertToLogDataList(string jarray)
        {
            JArray logs = JArray.Parse(jarray);
            List<LogData> lds = new List<LogData>();
            foreach (JObject obj in logs)
            {
                LogData data = new LogData();
                data.Message = (string)obj["message"];
                data.Type = (int)obj["type"];
                lds.Add(data);
            }
            return lds;
        }
    }
}
