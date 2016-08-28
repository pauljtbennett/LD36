using UnityEngine;
using UnityEngine.UI;

namespace LD36.UI
{
    public class Countdown : MonoBehaviour
    {
        public Text timeLeft;
        public Image bar;

        private void Start()
        {
            GameManager.instance.OnTimeLeftUpdated += HandleTimeLeftUpdated;
        }

        private void HandleTimeLeftUpdated(float time, float percent)
        {
            float secondsLeft = Mathf.Ceil(time);
            int minutesLeft = Mathf.FloorToInt(secondsLeft / 60f); 
            timeLeft.text = string.Format("{0}:{1:00}", minutesLeft, secondsLeft - (minutesLeft * 60));
            bar.fillAmount = percent;
        }
    }
}