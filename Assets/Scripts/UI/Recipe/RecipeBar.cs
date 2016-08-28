using System.IO;
using LD36.Config;
using LD36.UI.Tooltip;
using LD36.Utils;
using UnityEngine;

namespace LD36.UI.Recipe
{
    public class RecipeBar : MonoBehaviour
    {
        public GameObject content;
        public GameObject stepPrefab;

        private void Start()
        {
            GameManager.instance.OnCureIngredientAdded += AddIngredient;
            GameManager.instance.OnCureHeated += AddHeat;
            GameManager.instance.OnCureStirred += AddStir;
            GameManager.instance.OnCureCrushed += AddCrush;
            GameManager.instance.OnScenarioUpdated += delegate(Scenario scenario) {
                Clear();
            };
        }
        
        public void AddIngredient(Ingredient ingredient)
        {
            GameObject go = content.AddChild(stepPrefab);

            // Set icon
            if (!string.IsNullOrEmpty(ingredient.icon))
            {
                string resourcePath = Path.Combine("Icons", ingredient.icon);
                Sprite s = Resources.Load<Sprite>(resourcePath);
                
                if (s != null)
                {
                    RecipeStep step = go.GetComponent<RecipeStep>();
                    step.icon.sprite = s;
                }
            }

            // Setup tooltip spawner
            TooltipSpawner spawner = go.GetComponent<TooltipSpawner>();
            spawner.SetContent(ingredient.name);
        }
        
        public void AddHeat()
        {
            GameObject go = content.AddChild(stepPrefab);
            RecipeStep step = go.GetComponent<RecipeStep>();
            step.icon.sprite = Resources.Load<Sprite>("Icons/fire");
            TooltipSpawner spawner = go.GetComponent<TooltipSpawner>();
            spawner.SetContent("Heat");
        }

        public void AddStir()
        {
            GameObject go = content.AddChild(stepPrefab);
            RecipeStep step = go.GetComponent<RecipeStep>();
            step.icon.sprite = Resources.Load<Sprite>("Icons/stir");
            TooltipSpawner spawner = go.GetComponent<TooltipSpawner>();
            spawner.SetContent("Stir");
        }

        public void AddCrush()
        {
            GameObject go = content.AddChild(stepPrefab);
            RecipeStep step = go.GetComponent<RecipeStep>();
            step.icon.sprite = Resources.Load<Sprite>("Icons/crush");
            TooltipSpawner spawner = go.GetComponent<TooltipSpawner>();
            spawner.SetContent("Crush");
        }

        public void Clear()
        {
            foreach (Transform child in transform)
            {
                GameObject.Destroy(child.gameObject);
            }
        }
    }
}