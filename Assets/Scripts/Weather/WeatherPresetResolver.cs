using Assets.Scripts.Wind;
using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Assets.Scripts.Weather
{
    public class WeatherPresetResolver : MonoBehaviour
    {
        [Serializable] private class WeatherPresetEntry
        {
            public WeatherState WeatherState;
            public WeatherPreset WeatherPreset;
        }
        [SerializeField] private List<WeatherPresetEntry> _weatherPresets = new();
        [SerializeField] private WeatherState _currentWeatherState;
        public WeatherState CurrentWeatherState => _currentWeatherState;
        public WeatherPreset CurrentWeatherPreset { get; set; }
        [SerializeField] private WindState _windState;
        [SerializeField, Range(0f, 1f)] private float _baseWindStrength = 0.5f;
        [SerializeField] private TMP_Text _weatherText;


        private void OnValidate()
        {
            ApplyWeather(_currentWeatherState);
        }

        private void ApplyWeather(WeatherState weatherState)
        {
            WeatherPreset weatherPreset = FindPreset(weatherState);

            if (weatherPreset == null)
            {
                Debug.LogWarning($"Weather preset for {weatherState} was not found.", this);
                return;
            }
            _currentWeatherState = weatherState;
            CurrentWeatherPreset = weatherPreset;
            if (_windState != null) _windState.SetStrength(_baseWindStrength * weatherPreset.WindMultiplier);
            _weatherText.text = $"Weather: {_currentWeatherState}";
        }

        private WeatherPreset FindPreset(WeatherState weatherState)
        {
            foreach (WeatherPresetEntry entry in _weatherPresets)
            {
                if (entry.WeatherState == weatherState) return entry.WeatherPreset;
            }
            return null;
        }
    }
}
