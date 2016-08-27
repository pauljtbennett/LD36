using LD36.Config;
using UnityEngine;
using UnityEngine.UI;

namespace LD36.UI
{
    public class GameHUD : MonoBehaviour
    {
        public Button heatButton;
        public Button stirButton;
        public Button crushButton;
        public Button finishCureButton;

        public GameObject scenario;
        public Text scenarioText;

        private void Start()
        {
            finishCureButton.onClick.AddListener(GameManager.instance.FinishCure);
            GameManager.instance.OnScenarioUpdated += HandleScenarioUpdated;
        }

        private void HandleScenarioUpdated(Scenario scenario)
        {
            foreach (var symptom in scenario.symptoms)
            {
                Debug.Log(symptom.name);
            }
        }
    }
}