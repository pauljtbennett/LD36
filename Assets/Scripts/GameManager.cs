using System.Collections;
using System.Collections.Generic;
using LD36.Config;
using UnityEngine;

namespace LD36
{
    public delegate void GameStartHandler();
    public delegate void GameOverHandler(float score, int deaths);
    public delegate void TimeLeftUpdateHandler(float timeLeft, float percentageLeft);
    public delegate void ScenarioUpdateHandler(Scenario scenario);
    public delegate void CureIngredientAddedHandler(Ingredient ingredient);
    public delegate void CureUpdatedHandler();
    public delegate void CureFinishedHandler(Cure cure);

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
        public event CureFinishedHandler OnCureFinished;

        public GameObject introBook;
        public GameObject gameHUD;
        public Animator environmentAnimator;

        
        private float timeLeft;
        private bool gameOver;
        private List<Cure> cures;
        public Level currentLevel { get; private set; }
        public Scenario currentScenario { get; private set; }
        public Cure currentCure { get; private set; }

        // Keep track of this to avoid getting the same one again
        private Scenario lastScenario;

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
            introBook.SetActive(true);
            gameHUD.SetActive(false);
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
                    int deaths = 0;

                    // Do some scoring
                    foreach (var cure in cures)
                    {
                        float score = cure.CalculateEffectiveness();
                        if (score == float.NegativeInfinity)
                        {
                            deaths++;
                            totalScore -= 500f; // Lets just say you lose 500 points for killing someone, seems fair
                        }
                        else
                        {
                            totalScore += score;
                        }
                    }

                    int highScore = PlayerPrefs.GetInt("highScore", 0);
                    if (Mathf.RoundToInt(totalScore) > highScore)
                    {
                        PlayerPrefs.SetInt("highScore", Mathf.RoundToInt(totalScore));
                    }

                    currentLevel = null;
                    if (OnGameOver != null) OnGameOver(totalScore, deaths);
                    return;
                }

                if (currentScenario == null)
                {
                    List<Scenario> possibleScenarios = Scenarios.instance.GetAllConfigs();
                    possibleScenarios.Remove(lastScenario);
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
            introBook.SetActive(false);
            gameHUD.SetActive(true);
            environmentAnimator.SetTrigger("MoveToGame");
            if (OnGameStart != null) OnGameStart();
        }

        public void AddIngredientToCure(Ingredient ingredient)
        {
            if (currentCure != null) 
            {
                if (currentCure.AddIngredient(ingredient))
                {
                    if (OnCureIngredientAdded != null) OnCureIngredientAdded(ingredient);
                }
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
            if (OnCureFinished != null) OnCureFinished(currentCure);

            // Reset cure but leave scenario active for a few seconds
            currentCure = null;
            StartCoroutine(DelayedNewScenario(6f));
        }

        private IEnumerator DelayedNewScenario(float delay)
        {
            yield return new WaitForSeconds(delay);
            lastScenario = currentScenario;
            currentScenario = null;
        }
    }
}