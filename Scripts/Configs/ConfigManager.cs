using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using SimpleJSON;

namespace Configs
{
    public enum OBJECT_TYPE { MISC, }

    public interface IConfigObject
    {
        OBJECT_TYPE Type { get; }
        string Id { get; }
    }

    public static class ConfigManager
    {
        private static Dictionary<OBJECT_TYPE, List<IConfigObject>> _db = new Dictionary<OBJECT_TYPE, List<IConfigObject>>();
        private const string DirPath = "./Configs/";

        public static void Init()
        {
            try
            {
                Load<SettingsCO>(DirPath + "settings.json");
            }
            catch (Exception e)
            {
                Core.Logger.Error(e);
            }
        }

        private static string Add(IConfigObject obj)
        {
            if (!_db.ContainsKey(obj.Type))
                _db.Add(obj.Type, new List<IConfigObject>());

            if (GetOrNull(obj.Type, obj.Id) != null)
                return "object, type: " + obj.Type.ToString() + " id: " + obj.Id + " already is exists";

            _db[obj.Type].Add(obj);
            Core.Logger.Print($">> Config {obj.Type.ToString()}, id: {obj.Id} loaded");
            return null;
        }

        private static void Load<T>(string path) where T : IConfigObject
        {
            var tObj = File.ReadAllText(path);
            var jObj = JSON.Parse(tObj).AsArray;
            if (jObj == null)
                throw new Exception("can't parse config object: " + path);

            foreach (var kv in jObj)
            {
                var val = kv.Value;
                if (val == null)
                    throw new Exception("Wrong json object" + path);

                var obj = (IConfigObject)Activator.CreateInstance(typeof(T), new object[] { val });
                if (obj == null)
                    throw new Exception("Wrong object: " + path);

                var err = Add(obj);
                if (err != null)
                    throw new Exception(err);
            }
        }

        public static IConfigObject GetOrNull(OBJECT_TYPE type, string id)
        {
            if (!_db.ContainsKey(type) || _db[type] == null)
                return null;

            return _db[type].FirstOrDefault(o => o.Id == id);
        }
    }
}
