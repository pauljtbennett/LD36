using UnityEngine;
using UnityEngine.UI;

namespace LD36.UI
{
    public class GameOver : MonoBehaviour
    {
        public Text scoreText;

        public void UpdateScore(int score, int deaths)
        {
            string content = string.Format("You scored {0} points.", score);
            if (deaths > 0)
            {
                content += string.Format(" {0} of your patients died!", deaths);
            }
            else 
            {
                content += " All of your patients survived!";
            }
            scoreText.text = content;
        }
    }
}