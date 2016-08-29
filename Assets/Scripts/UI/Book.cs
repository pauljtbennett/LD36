using System.Linq;
using LD36.Config;
using UnityEngine;
using UnityEngine.UI;

namespace LD36.UI
{
    public class Book : MonoBehaviour
    {
        public Text pageLeft;
        public Text pageRight;
        public Button closeButton;

        private int currentPage;
        private int perPage = 4;

        public void ShowPage()
        {
            string pageTextLeft = "";
            string pageTextRight = "";
            int index = 0;

            foreach (var symptom in Symptoms.instance.GetAllConfigs().Skip(currentPage * perPage))
            {
                if (index < (perPage / 2))
                {
                    pageTextLeft += symptom.name + "\n~\n";
                    if (!string.IsNullOrEmpty(symptom.notes)) pageTextLeft += symptom.notes + "\n";
                    pageTextLeft += "\n";
                }
                else if (index < perPage)
                {
                    pageTextRight += symptom.name + "\n~\n";
                    if (!string.IsNullOrEmpty(symptom.notes)) pageTextRight += symptom.notes + "\n";
                    pageTextRight += "\n";
                }
                else break;
                index++;
            }

            pageLeft.text = pageTextLeft;
            pageRight.text = pageTextRight;
        }

        public void Open()
        {
            gameObject.SetActive(true);
            ShowPage();
        }

        public void Close()
        {
            gameObject.SetActive(false);
        }

        public void NextPage()
        {
            if (currentPage >= Mathf.CeilToInt((float)Symptoms.instance.GetAllConfigs().Count / (float)perPage) - 1) return;
            currentPage++;
            ShowPage();
        }

        public void PreviousPage()
        {
            if (currentPage <= 0) return;
            currentPage--;
            ShowPage();
        }
    }
}