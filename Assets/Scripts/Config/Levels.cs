using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using UnityEngine;

namespace LD36.Config
{
    public class Levels
    {
        private static Levels _instance;
        public static Levels instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new Levels();
                }
                return _instance;
            }
        }

        private Dictionary<string, Level> levels;
        
        public void Init(string filename)
        {
            levels = new Dictionary<string, Level>();
            string resourcePath = Path.Combine("Config", filename);
            TextAsset t = Resources.Load<TextAsset>(resourcePath);
            var levelsObj = JToken.Parse(t.text);
            foreach (var levelObj in levelsObj["levels"].Children())
            {
                Level l = new Level(levelObj);
                levels.Add(l.name, l);
            }
        }

        public Level GetConfig(string name)
        {
            return levels[name];
        }
    }
}