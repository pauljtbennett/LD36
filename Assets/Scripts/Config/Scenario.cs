using System.Collections.Generic;
using Newtonsoft.Json.Linq;

namespace LD36.Config
{
    public class Scenario
    {
        public List<Symptom> symptoms;

        public Scenario()
        {
            
        }

        public Scenario(JToken token)
        {
            symptoms = new List<Symptom>();

            foreach (var s in token["symptoms"].Children())
            {
                symptoms.Add(Symptoms.instance.GetConfig(s.ToString()));
            }
        }
    }
}