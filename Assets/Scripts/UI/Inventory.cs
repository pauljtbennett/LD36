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
                if (iCopy.order > -1)
                {
                    string order = "1st";
                    if (iCopy.order == 1) order = "2nd";
                    if (iCopy.order == 2) order = "3rd";
                    if (iCopy.order == 3) order = "4th";
                    if (iCopy.order == 4) order = "5th"; 
                    tooltipContent += string.Format("Must be added {0}\n", order);
                }
                if (iCopy.heat != 0) tooltipContent += string.Format("<color=yellow>Heating will {0} your score</color>\n", iCopy.heat == -1 ? "reduce" : "increase");
                if (iCopy.stir != 0) tooltipContent += string.Format("<color=yellow>Stirring will {0} your score</color>\n", iCopy.stir == -1 ? "reduce" : "increase");
                if (iCopy.crush != 0) tooltipContent += string.Format("<color=yellow>Crushing will {0} your score</color>\n", iCopy.crush == -1 ? "reduce" : "increase");
                if (iCopy.neverMix != null)
                {
                    tooltipContent += "<color=red>Mixing with ";
                    tooltipContent += string.Join(", ", iCopy.neverMix.ToArray());
                    tooltipContent += " will kill your patient!</color>";
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