using System.Collections.Generic;
using LD36.Config;
using UnityEngine;

namespace LD36
{
    public delegate void GameStartHandler();
    public delegate void GameOverHandler(float score);
    public delegate void TimeLeftUpdateHandler(float timeLeft, float percentageLeft);
    public delegate void ScenarioUpdateHandler(Scenario scenario);
    public delegate void CureIngredientAddedHandler(Ingredient ingredient);
    public delegate void CureUpdatedHandler();

    public class GameManager : MonoBehaviour
    {
        public static GameManager instance;

        public event GameStartHandler OnGameStart;
        public event GameOverHandler OnGameOver;
        public event TimeLeftUpdateHandler OnTimeLeftUpdated;
        public event ScenarioUpdateHandler OnScenarioUpdated;
        public event CureIngredientAddedHandler OnCureIngredientAdded;
        public event CureUpdatedHandler OnCureHeated;
        public event CureUpdatedHandler OnCureStirred;
        public event CureUpdatedHandler OnCureCrushed;

        
        private float timeLeft;
        private bool gameOver;
        private List<Cure> cures;
        public Level currentLevel { get; private set; }
        public Scenario currentScenario { get; private set; }
        public Cure currentCure { get; private set; }

        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
                DontDestroyOnLoad(gameObject);
            }

            // Load config
            Ingredients.instance.Init("ingredients");
            Symptoms.instance.Init("symptoms");
            Scenarios.instance.Init("scenarios");
            Levels.instance.Init("levels");
        }

        private void Start()
        {
            StartGame();
        }

        private void Update()
        {
            if (currentLevel != null)
            {
                timeLeft -= Time.deltaTime;
                float percentageLeft = (timeLeft / (currentLevel.time * 60f));
                if (OnTimeLeftUpdated != null) OnTimeLeftUpdated(timeLeft, percentageLeft);

                if (timeLeft <= 0)
                {
                    gameOver = true;
                    float totalScore = 0f;

                    // Do some scoring
                    foreach (var cure in cures)
                    {
                        totalScore += cure.CalculateEffectiveness();
                    }

                    currentLevel = null;
                    if (OnGameOver != null) OnGameOver(totalScore);
                    return;
                }

                if (currentScenario == null)
                {
                    List<Scenario> possibleScenarios = Scenarios.instance.GetAllConfigs();
                    currentScenario = possibleScenarios[Random.Range(0, possibleScenarios.Count)];
                    currentCure = new Cure(currentScenario);
                    cures.Add(currentCure);
                    if (OnScenarioUpdated != null) OnScenarioUpdated(currentScenario);
                }
            }
        }

        public void StartGame()
        {
            currentLevel = Levels.instance.GetConfig("Level 1");
            cures = new List<Cure>();
            timeLeft = currentLevel.time * 60f;
            if (OnGameStart != null) OnGameStart();
        }

        public void AddIngredientToCure(Ingredient ingredient)
        {
            if (currentCure != null) 
            {
                currentCure.AddIngredient(ingredient);
                if (OnCureIngredientAdded != null) OnCureIngredientAdded(ingredient);
            }

        }

        public void HeatCure()
        {
            if (currentCure != null)
            {
                currentCure.Heat();
                if (OnCureHeated != null) OnCureHeated();
            }
        }

        public void StirCure()
        {
            if (currentCure != null)
            {
                currentCure.Stir();
                if (OnCureStirred != null) OnCureStirred();
            }
        }

        public void CrushCure()
        {
            if (currentCure != null)
            {
                currentCure.Crush();
                if (OnCureCrushed != null) OnCureCrushed();
            }
        }

        public void FinishCure()
        {
            currentCure = null;
            currentScenario = null;
        }
    }
}