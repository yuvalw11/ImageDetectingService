using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceGuiComunication
{
    public class JsonConvertor
    {
        public static JsonCommand GenerateJsonCommandObject(string json)
        {
            JObject jsonObj = JObject.Parse(json);
            JsonCommand jc = new JsonCommand((int)jsonObj["commandID"], jsonObj["args"].ToObject<string[]>(), (bool)jsonObj["result"], (string)jsonObj["jsonData"]);

            return jc;
        }

        public static string GenerateJsonCommandString(JsonCommand jc)
        {
            JObject json = new JObject();
            json["commandID"] = jc.CommandID;
            json["args"] = new JArray(jc.Args);
            json["result"] = jc.Result;
            json["jsonData"] = jc.JsonData;
            return json.ToString();
        }

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

        public static ConfigFileData ConvertToConfigFileData(string json)
        {
            JObject jsonObj = JObject.Parse(json);
            ConfigFileData cfd = new ConfigFileData();
            cfd.OutputDir = (string)jsonObj["outputDir"];
            cfd.SourceDir = (string)jsonObj["sourceDir"];
            cfd.LogName = (string)jsonObj["logName"];
            cfd.ThumnailSize = (int)jsonObj["thumnailSize"];
            JArray inputDirs = new JArray(jsonObj["outputDir"]);
            cfd.InputDirs = inputDirs.Select(jv => (string)jv).ToArray();

            return cfd;
        }

        public static string ConvertToJson(LogData ld)
        {
            JObject json = new JObject();
            json["message"] = ld.Message;
            json["type"] = ld.Type;

            return json.ToString();
        }

        public static LogData ConvertToLogData(string json)
        {
            JObject jsonObj = JObject.Parse(json);
            LogData ld = new LogData();
            ld.Message = (string)jsonObj["message"];
            ld.Type = (int)jsonObj["type"];

            return ld;
        }

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
