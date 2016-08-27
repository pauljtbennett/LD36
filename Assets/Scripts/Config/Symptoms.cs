using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using UnityEngine;

namespace LD36.Config
{
    public class Symptoms
    {
        private static Symptoms _instance;
        public static Symptoms instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new Symptoms();
                }
                return _instance;
            }
        }

        private Dictionary<string, Symptom> symptoms;
        
        public void Init(string filename)
        {
            symptoms = new Dictionary<string, Symptom>();
            string resourcePath = Path.Combine("Config", filename);
            TextAsset t = Resources.Load<TextAsset>(resourcePath);
            var symptomsObj = JToken.Parse(t.text);
            foreach (var symptomObj in symptomsObj["symptoms"].Children())
            {
                Symptom s = new Symptom();
                s.name = symptomObj["name"].ToString();
                if (symptomObj["notes"] != null) s.notes = symptomObj["notes"].ToString();
                s.curedBy = new List<Ingredient>();
                foreach (var i in symptomObj["curedBy"].Children())
                {
                    s.curedBy.Add(Ingredients.instance.GetConfig(i.ToString()));
                }
                symptoms.Add(s.name, s);
            }
        }

        public Symptom GetConfig(string name)
        {
            return symptoms[name];
        }

        public List<Symptom> GetAllConfigs()
        {
            return symptoms.Values.ToList();
        }
    }
}