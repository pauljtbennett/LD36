using System.IO;
using LD36.Config;
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