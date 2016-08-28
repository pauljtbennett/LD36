using UnityEngine;

namespace LD36
{
    public class LightFlicker : MonoBehaviour
    {
        private Light light;
        private float baseIntensity;

        private void Start()
        {
            light = GetComponent<Light>();
            baseIntensity = light.intensity;
        }

        private void Update()
        {
            if (Random.Range(0f, 1f) > 0.75)
            {
                light.intensity = baseIntensity + Random.Range(-0.5f, 0.5f);
            }
        }
    }
}