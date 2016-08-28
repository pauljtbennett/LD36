using System;
using UnityEngine;
using UnityEngine.UI;

namespace LD36.UI
{
    public class Countdown : MonoBehaviour
    {
        public Text timeLeft;
        public Image bar;
        private int score;

        private void Start()
        {
            GameManager.instance.OnTimeLeftUpdated += HandleTimeLeftUpdated;
            GameManager.instance.OnCureFinished += HandleScoreUpdated;
        }

        private void HandleScoreUpdated(Cure cure)
        {
            score += Mathf.RoundToInt(cure.CalculateEffectiveness());
        }

        private void HandleTimeLeftUpdated(float time, float percent)
        {
            float secondsLeft = Mathf.Ceil(time);
            int minutesLeft = Mathf.FloorToInt(secondsLeft / 60f); 
            timeLeft.text = string.Format("Time left: {0}:{1:00} - Score: {2}", minutesLeft, secondsLeft - (minutesLeft * 60), score);
            bar.fillAmount = percent;
        }
    }
}