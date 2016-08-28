using System.IO;
using LD36.Config;
using LD36.UI.Tooltip;
using LD36.Utils;
using UnityEngine;
using UnityEngine.UI;

namespace LD36.UI
{
    public class Inventory : MonoBehaviour
    {
        public GameObject contentScroller;
        public GameObject buttonPrefab;

        private void Start()
        {
            foreach (Ingredient i in Ingredients.instance.GetAllConfigs())
            {
                var iCopy = i;

                // Create button
                GameObject go = contentScroller.AddChild(buttonPrefab);
                Button button = go.GetComponent<Button>();                
                button.onClick.AddListener(delegate() {
                    IngredientSelected(iCopy);
                });

                // Set icon
                if (!string.IsNullOrEmpty(i.icon))
                {
                    string resourcePath = Path.Combine("Icons", i.icon);
                    Sprite s = Resources.Load<Sprite>(resourcePath);
                    
                    if (s != null)
                    {
                        Image icon = go.GetComponentInChildren<Image>();
                        icon.sprite = s;
                    }
                }

                // Setup tooltip spawner
                string tooltipContent = iCopy.name + "\n";
                if (iCopy.heat != 0) tooltipContent += string.Format("Should be heated: {0}", iCopy.heat == -1 ? "Never" : "Always");
                if (iCopy.stir != 0) tooltipContent += string.Format("Should be stirred: {0}", iCopy.stir == -1 ? "Never" : "Always");
                if (iCopy.crush != 0) tooltipContent += string.Format("Should be crushed: {0}", iCopy.crush == -1 ? "Never" : "Always");
                TooltipSpawner spawner = go.GetComponent<TooltipSpawner>();
                spawner.SetContent(tooltipContent);
            }
        }

        public void IngredientSelected(Ingredient ingredient)
        {
            Debug.Log("Selected " + ingredient.name);
            if (GameManager.instance.currentCure != null)
            {
                GameManager.instance.currentCure.AddIngredient(ingredient);
            }
        }
    }
}