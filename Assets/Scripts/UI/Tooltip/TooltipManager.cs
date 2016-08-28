using UnityEngine;

namespace LD36.UI.Tooltip
{
    public class TooltipManager : MonoBehaviour
    {
        public static TooltipManager instance;
        public Tooltip tooltip;

        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
            }
        }
    }
}