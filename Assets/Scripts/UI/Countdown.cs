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
            timeLeft.text = Mathf.Ceil(time).ToString();
            bar.fillAmount = percent;
        }
    }
}