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
            JArray args = new JArray(jsonObj["args"]);
            JsonCommand jc = new JsonCommand((int)jsonObj["commandID"], args.Select(jv => (string)jv).ToArray(), (bool)jsonObj["result"], (string)jsonObj["jsonData"]);

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
    }
}
