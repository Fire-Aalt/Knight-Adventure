using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Game
{
    public class HUD : MonoBehaviour
    {
        [SerializeField] private Slider _healthBar;
        [SerializeField] private TextMeshProUGUI _timeText;

        public float ScoreTime = 0;

        private void Update()
        {
            ScoreTime += Time.deltaTime;
            _timeText.text = FormatTime(ScoreTime);
        }

        private string FormatTime(float time)
        {
            int seconds = Mathf.FloorToInt(time);
            int minutes = seconds / 60;
            seconds %= 60;

            string res = minutes + ":";
            if (seconds < 10)
            {
                res += "0";
            }
            res += seconds;

            return res;
        }


        public void UpdateHealthBar(int currentHealth)
        {
            _healthBar.value = currentHealth;
        }

    }
}

