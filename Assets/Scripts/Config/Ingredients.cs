using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using UnityEngine;

namespace LD36.Config
{
    public class Ingredients
    {
        private static Ingredients _instance;
        public static Ingredients instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new Ingredients();
                }
                return _instance;
            }
        }

        private Dictionary<string, Ingredient> ingredients;
        
        public void Init(string filename)
        {
            ingredients = new Dictionary<string, Ingredient>();
            string resourcePath = Path.Combine("Config", filename);
            TextAsset t = Resources.Load<TextAsset>(resourcePath);
            var ingredientsObj = JToken.Parse(t.text);
            foreach (var ingredientObj in ingredientsObj["ingredients"].Children())
            {
                Ingredient i = JsonConvert.DeserializeObject<Ingredient>(ingredientObj.ToString());
                ingredients.Add(i.name, i);
            }
        }

        public Ingredient GetConfig(string name)
        {
            return ingredients[name];
        }

        public List<Ingredient> GetAllConfigs()
        {
            return ingredients.Values.ToList();
        }
    }
}