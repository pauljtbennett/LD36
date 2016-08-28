using System.Collections.Generic;

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
    }
}