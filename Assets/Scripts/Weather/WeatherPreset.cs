using UnityEngine;

namespace Assets.Scripts.Weather
{
    [CreateAssetMenu(fileName = "WeatherPreset", menuName = "Weather/WeatherPreset")]
    public class WeatherPreset : ScriptableObject
    {
        [SerializeField] private float _intensity;
        [SerializeField] private float _fogDensity;
        [SerializeField] private float _windMultiplier = 1f;

        public float Intensity => _intensity;
        public float FogDensity => _fogDensity;
        public float WindMultiplier => _windMultiplier;
    }
}
