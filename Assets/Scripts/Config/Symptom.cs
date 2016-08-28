using System.Collections.Generic;
using Newtonsoft.Json.Linq;

namespace LD36.Config
{
    public class Symptom
    {
        public string name;
        public string notes;
        public List<Ingredient> curedBy;

        public Symptom()
        {
            
        }

        public Symptom(JToken token)
        {
            name = token.Value<string>("name");
            notes = token.Value<string>("notes");
            curedBy = new List<Ingredient>();

            foreach (var i in token["curedBy"].Children())
            {
                curedBy.Add(Ingredients.instance.GetConfig(i.ToString()));
            }
        }
    }
}