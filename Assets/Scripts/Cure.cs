using System.Collections.Generic;
using System.Linq;
using LD36.Config;
using UnityEngine;

namespace LD36
{
    public class Cure
    {
        public Scenario scenario { get; private set; }
        public List<Ingredient> ingredients { get; private set; }

        // Lists of indexes of actions
        private List<int> heatedAt;
        private List<int> stirredAt;
        private List<int> crushedAt;

        public Cure(Scenario scenario)
        {
            this.scenario = scenario;
            this.ingredients = new List<Ingredient>();
        }

        public void AddIngredient(Ingredient ingredient)
        {
            if (!ingredients.Contains(ingredient)) ingredients.Add(ingredient);
        }

        public void Heat()
        {
            if (!heatedAt.Contains(ingredients.Count)) heatedAt.Add(ingredients.Count);
        }

        public void Stir()
        {
            if (!stirredAt.Contains(ingredients.Count)) stirredAt.Add(ingredients.Count);
        }

        public void Crush()
        {
            if (!crushedAt.Contains(ingredients.Count)) crushedAt.Add(ingredients.Count);
        }

        // Calculate an effectiveness score for a given scenarios
        // Correct ingredient +100
        // Missing ingredient -25
        // Incorrect ingredient -50
        // Correct order, heat, stir, crush +25
        // Incorrect order, heat, stir, crush -25
        // Incorrect mix = death!
        public float CalculateEffectiveness()
        {
            float total = 0;
            List<Ingredient> usedIngredients = new List<Ingredient>();

            foreach (var s in scenario.symptoms)
            {
                foreach (var c in s.curedBy)
                {
                    // Correct ingrient
                    if (ingredients.Contains(c))
                    {
                        total += 100;
                        if (!usedIngredients.Contains(c)) usedIngredients.Add(c);
                        Debug.Log("Correct ingredient: " + c.name + " for symptom: " + s.name);
                    }
                    // Missing ingrient
                    else
                    {
                        total -= 50;
                        Debug.Log("Missing ingredient: " + c.name + " for symptom: " + s.name);
                    }
                }
            }

            int leftOvers = ingredients.Count - usedIngredients.Count;

            // Incorrect ingredients
            total -= (leftOvers * 50);

            return total;
        }
    }
}