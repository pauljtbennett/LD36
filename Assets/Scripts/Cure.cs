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

        private List<Ingredient> heated;
        private List<Ingredient> stirred;
        private List<Ingredient> crushed;

        public Cure(Scenario scenario)
        {
            this.scenario = scenario;
            
            ingredients = new List<Ingredient>();
            heated = new List<Ingredient>();
            stirred = new List<Ingredient>();
            crushed = new List<Ingredient>();
        }

        public bool AddIngredient(Ingredient ingredient)
        {
            if (!ingredients.Contains(ingredient))
            {
                ingredients.Add(ingredient);
                return true;
            }
            return false;
        }

        public void Heat()
        {
            heated = new List<Ingredient>(ingredients);
        }

        public void Stir()
        {
            stirred = new List<Ingredient>(ingredients);
        }

        public void Crush()
        {
            crushed = new List<Ingredient>(ingredients);
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
            // First check for any ingredients that should never be mixed
            foreach (var i in ingredients)
            {
                if (i.neverMix != null)
                {
                    foreach (var other in ingredients)
                    {
                        if (i.neverMix.Contains(other.name))
                        {
                            Debug.Log("Cure killed patient by mixing " + i.name + " and " + other.name);
                            return float.NegativeInfinity;
                        }
                    }
                }
            }

            float total = 0;
            List<Ingredient> usedIngredients = new List<Ingredient>();

            foreach (var s in scenario.symptoms)
            {
                foreach (var c in s.curedBy)
                {
                    // Correct ingredient
                    if (ingredients.Contains(c))
                    {
                        total += 100;
                        if (!usedIngredients.Contains(c)) usedIngredients.Add(c);
                        Debug.Log("Correct ingredient: " + c.name + " for symptom: " + s.name);
                    }
                }
            }

            // Didn't match any ingredients? Death.
            if (usedIngredients.Count == 0)
            {
                Debug.Log("Cure killed patient by containg no useful ingredients");
                return float.NegativeInfinity;
            }

            // Incorrect ingredients
            int leftOvers = ingredients.Count - usedIngredients.Count;
            total -= (leftOvers * 50);

            // Check heated
            foreach (var h in heated)
            {
                total += (25 * h.heat);
            }

            // Check stirred
            foreach (var s in stirred)
            {
                total += (25 * s.stir); 
            }

            // Check crushed
            foreach (var c in crushed)
            {
                total += (25 * c.crush); 
            }

            // Check order
            foreach (var u in usedIngredients)
            {
                if (u.order != -1)
                {
                    total += (u.order == ingredients.IndexOf(u)) ? 50 : -50;
                }
            }

            return total;
        }
    }
}