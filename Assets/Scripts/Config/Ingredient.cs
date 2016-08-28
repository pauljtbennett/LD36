using System.Collections.Generic;
using Newtonsoft.Json.Linq;

namespace LD36.Config
{
    public class Ingredient
    {
        public string name;
        public string icon;
        public int order;
        public int heat;
        public int stir;
        public int crush;
        public List<string> neverMix;

        public Ingredient()
        {
            
        }

        public Ingredient(JToken token)
        {
            name = token.Value<string>("name");
            icon = token.Value<string>("icon");
            order = token.Value<int>("order");
            heat = token.Value<int>("heat");
            stir = token.Value<int>("stir");
            crush = token.Value<int>("crush");
            if (token["neverMix"] != null)
            {
                neverMix = new List<string>();
                foreach (var n in token["neverMix"].Children())
                {
                    neverMix.Add(n.ToString());
                }
            }
        }
    }
}