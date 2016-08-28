using Newtonsoft.Json.Linq;

namespace LD36.Config
{
    public class Level
    {
        public string name;
        public float time;

        public Level()
        {
            
        }

        public Level(JToken token)
        {
            name = token.Value<string>("name");
            time = token.Value<float>("time");
        }
    }
}