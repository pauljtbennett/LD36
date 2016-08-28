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
                if (iCopy.heat != 0) tooltipContent += string.Format("<color=yellow>{0} heat</color>\n", iCopy.heat == -1 ? "Never" : "Always");
                if (iCopy.stir != 0) tooltipContent += string.Format("<color=yellow>{0} stir</color>\n", iCopy.stir == -1 ? "Never" : "Always");
                if (iCopy.crush != 0) tooltipContent += string.Format("<color=yellow>{0} crush</color>\n", iCopy.crush == -1 ? "Never" : "Always");
                if (iCopy.neverMix != null)
                {
                    tooltipContent += "<color=red>Never mix with: ";
                    tooltipContent += string.Join(", ", iCopy.neverMix.ToArray());
                    tooltipContent += "</color>";
                }
                TooltipSpawner spawner = go.GetComponent<TooltipSpawner>();
                spawner.SetContent(tooltipContent);
            }
        }

        public void IngredientSelected(Ingredient ingredient)
        {
            GameManager.instance.AddIngredientToCure(ingredient);
        }
    }
}