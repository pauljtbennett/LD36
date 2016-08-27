using System.Collections.Generic;
using LD36.Config;

namespace LD36
{
    public class Cure
    {
        public Scenario scenario { get; private set; }
        public List<Ingredient> ingredients;

        // Lists of indexes of actions
        public List<int> heatedAt;
        public List<int> stirredAt;
        public List<int> crushedAt;

        public Cure(Scenario scenario)
        {
            this.scenario = scenario;
        }

        // Calculate an effectiveness score for a given scenarios
        public float CalculateEffectiveness()
        {
            return 0f;
        }
    }
}