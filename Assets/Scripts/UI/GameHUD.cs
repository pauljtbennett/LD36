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
        public GameOver gameOver;

        private void Start()
        {
            GameManager.instance.OnGameStart += HandleGameStart;
            GameManager.instance.OnGameOver += HandleGameOver;
            GameManager.instance.OnScenarioUpdated += HandleScenarioUpdated;
            GameManager.instance.OnCureFinished += HandleCureFinished;
        }

        private void HandleScenarioUpdated(Scenario scenario)
        {
            string output = "A customer is complaining of some nasty symptoms: \n";
            output += string.Join(", ", scenario.symptoms.Select(x => x.name).ToArray()) + "\n";
            output += "Help them by finding a cure!";
            scenarioText.GetComponent<TypedText>().UpdateText(output);
        }

        private void HandleCureFinished(Cure cure)
        {
            float score = cure.CalculateEffectiveness();
            string output = (score > 0) ? "Thanks, I feel much better!" : "Ouch, that did more harm than good!";
            if (score == float.NegativeInfinity) output = "Silence...";
            scenarioText.GetComponent<TypedText>().UpdateText(output);
        }

        private void HandleGameStart()
        {
            finishCureButton.onClick.AddListener(GameManager.instance.FinishCure);
            heatButton.onClick.AddListener(GameManager.instance.HeatCure);
            stirButton.onClick.AddListener(GameManager.instance.StirCure);
            crushButton.onClick.AddListener(GameManager.instance.CrushCure);
        }

        private void HandleGameOver(float score, int deaths)
        {
            gameOver.gameObject.SetActive(true);
            gameOver.UpdateScore(Mathf.RoundToInt(score), deaths);
            Debug.Log(score);
        }
    }
}