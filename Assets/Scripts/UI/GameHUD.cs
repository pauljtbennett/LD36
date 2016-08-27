using System.Linq;
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
        public GameObject gameOver;
        public Text gameOverText;

        private void Start()
        {
            GameManager.instance.OnScenarioUpdated += HandleScenarioUpdated;
            GameManager.instance.OnGameOver += HandleGameOver;

            if (GameManager.instance.currentCure != null)
            {
                finishCureButton.onClick.AddListener(GameManager.instance.FinishCure);
                heatButton.onClick.AddListener(GameManager.instance.currentCure.Heat);
                stirButton.onClick.AddListener(GameManager.instance.currentCure.Stir);
                crushButton.onClick.AddListener(GameManager.instance.currentCure.Crush);
            }
        }

        private void HandleScenarioUpdated(Scenario scenario)
        {
            string output = "A customer is complaining of some nasty symptoms: \n";
            output += string.Join(", ", scenario.symptoms.Select(x => x.name).ToArray()) + "\n";
            output += "Help them by finding a cure!";
            scenarioText.text = output;
        }

        private void HandleGameOver(float score)
        {
            gameOver.SetActive(true);
        }
    }
}