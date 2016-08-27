using System.Collections.Generic;
using LD36.Config;

namespace LD36
{
    public class Cure
    {
        public Scenario scenario { get; private set; }
        public List<Ingredient> ingredients;

        // Lists of indexes of actions
        private List<int> heatedAt;
        private List<int> stirredAt;
        private List<int> crushedAt;

        public Cure(Scenario scenario)
        {
            this.scenario = scenario;
        }

        public void Heat()
        {
            if (ingredients.Count == 0) return;
            heatedAt.Add(ingredients.Count - 1);
        }

        public void Stir()
        {
            if (ingredients.Count == 0) return;
            stirredAt.Add(ingredients.Count - 1);
        }

        public void Crush()
        {
            if (ingredients.Count == 0) return;
            crushedAt.Add(ingredients.Count - 1);
        }

        // Calculate an effectiveness score for a given scenarios
        public float CalculateEffectiveness()
        {
            return 0f;
        }
    }
}