using System;
using LD36.Config;
using UnityEngine;

namespace LD36
{
    public class AudioManager : MonoBehaviour
    {
        public static AudioManager instance;

        public AudioClip goodCure;
        public AudioClip badCure;
        public AudioClip goodScore;
        public AudioClip badScore;
        public AudioClip death;
        public AudioClip ingredientAdded;
        public AudioClip cureHeated;
        public AudioClip cureStirred;
        public AudioClip cureCrushed;

        private AudioSource audioSource;

        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
                DontDestroyOnLoad(gameObject);
            }

            audioSource = GetComponent<AudioSource>();
        }

        private void Start()
        {
            GameManager.instance.OnGameStart += HandleGameStart;
            GameManager.instance.OnGameOver += HandleGameOver;
            GameManager.instance.OnScenarioUpdated += HandleScenarioUpdated;
            GameManager.instance.OnCureFinished += HandleCureFinished;
            GameManager.instance.OnCureIngredientAdded += HandleAddIngredient;
            GameManager.instance.OnCureHeated += HandleHeat;
            GameManager.instance.OnCureStirred += HandleStir;
            GameManager.instance.OnCureCrushed += HandleCrush;
        }

        private void HandleGameStart()
        {
            
        }

        private void HandleGameOver(float score, int deaths)
        {
            PlayClip(score > 0 ? goodScore : badScore);
        }

        private void HandleScenarioUpdated(Scenario scenario)
        {
            
        }

        private void HandleCureFinished(Cure cure)
        {
            PlayClip(cure.CalculateEffectiveness() > 0 ? goodCure : badCure);
        }

        private void HandleAddIngredient(Ingredient ingredient)
        {
            PlayClip(ingredientAdded);
        }

        private void HandleHeat()
        {
            PlayClip(cureHeated);
        }

        private void HandleStir()
        {
            PlayClip(cureStirred);
        }

        private void HandleCrush()
        {
            PlayClip(cureCrushed);
        }

        private void PlayClip(AudioClip clip)
        {
            if (clip == null) return;
            audioSource.PlayOneShot(clip);
        }
    }
}