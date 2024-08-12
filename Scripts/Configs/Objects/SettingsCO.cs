using SimpleJSON;

namespace Configs
{
    public class SettingsCO : IConfigObject
    {
        public OBJECT_TYPE Type => OBJECT_TYPE.MISC;
        public string Id => "Settings";
        public MysqlConfig Mysql => new MysqlConfig(_obj["mysql"]);

        private JSONNode _obj;

        public SettingsCO(JSONNode obj)
        {
            _obj = obj;
        }

        public static SettingsCO GetSettingsCO() => (SettingsCO)ConfigManager.GetOrNull(OBJECT_TYPE.MISC, "Settings");
    }

    public class MysqlConfig
    {
        public string Server => _obj["server"].Value;
        public string Schema => _obj["schema"].Value;
        public string User => _obj["user"].Value;
        public string Password => _obj["password"].Value;
        public int Port => _obj["port"].AsInt;

        private readonly JSONNode _obj;
        public MysqlConfig(JSONNode obj) => _obj = obj;
    }
}
