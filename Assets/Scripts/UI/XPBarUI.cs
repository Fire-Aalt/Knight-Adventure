using MoreMountains.Feedbacks;
using RenderDream.GameEssentials;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace Game
{
    public class XPBarUI : MonoBehaviour
    {
        public static event Action<int> OnNewLevelUI;

        [SerializeField] private float _updateDuration;
        [SerializeField] private AnimationCurve _updateCurve;
        [SerializeField] private Slider _slider;
        [SerializeField] private MMF_Player _levelUpFeedback;

        private float _elapsedTime;
        private float _startValue, _currentValue, _endValue;

        public bool IsUpdating { get; private set; }
        private int _currentLevel;

        private void Start()
        {
            _slider.value = 0f;
        }

        private void Update()
        {
            if (IsUpdating)
            {
                if (_elapsedTime < _updateDuration)
                {
                    _elapsedTime += Time.deltaTime;

                    _currentValue = Mathf.Lerp(_startValue, _endValue, _updateCurve.Evaluate(_elapsedTime / _updateDuration));
                    if (_currentValue >= 1f)
                    {
                        _currentValue -= 1f;
                    }
                    _slider.value = _currentValue;
                }
                else
                {
                    IsUpdating = false;
                }
            }
        }

        public void ProcessNewLevel()
        {
            OnNewLevelUI?.Invoke(_currentLevel);
        }

        public void UpdateBarValue(float newValue)
        {
            _startValue = _currentValue;
            _endValue = newValue;
            if (newValue < _slider.value)
            {
                _endValue += 1f;
            }
            _elapsedTime = 0f;
            IsUpdating = true;
        }

        public void LevelUpFeedback(int newLevel)
        {
            _currentLevel = newLevel;
            _levelUpFeedback.PlayFeedbacks();
        }

        private void OnEnable()
        {
            XPManager.OnLevelPercentChanged += UpdateBarValue;
            XPManager.OnLevelChanged += LevelUpFeedback;
        }

        private void OnDisable()
        {
            XPManager.OnLevelPercentChanged -= UpdateBarValue;
            XPManager.OnLevelChanged -= LevelUpFeedback;
        }
    }
}
