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

        private void Start()
        {
            string pageText = "";

            foreach (var symptom in Symptoms.instance.GetAllConfigs())
            {
                pageText += symptom.name + "\n~\n";
                if (!string.IsNullOrEmpty(symptom.notes)) pageText += symptom.notes + "\n";
                pageText += "\n";
            }

            pageLeft.text = pageText;
        }

        public void Open()
        {
            gameObject.SetActive(true);
        }

        public void Close()
        {
            gameObject.SetActive(false);
        }
    }
}