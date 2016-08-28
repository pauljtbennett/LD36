using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using UnityEngine;

namespace LD36.Config
{
    public class Scenarios
    {
        private static Scenarios _instance;
        public static Scenarios instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new Scenarios();
                }
                return _instance;
            }
        }

        private List<Scenario> scenarios;
        
        public void Init(string filename)
        {
            scenarios = new List<Scenario>();
            string resourcePath = Path.Combine("Config", filename);
            TextAsset t = Resources.Load<TextAsset>(resourcePath);
            var scenariosObj = JToken.Parse(t.text);
            foreach (var scenarioObj in scenariosObj["scenarios"].Children())
            {
                Scenario s = new Scenario(scenarioObj);
                scenarios.Add(s);
            }
        }

        public List<Scenario> GetAllConfigs()
        {
            return scenarios;
        }
    }
}