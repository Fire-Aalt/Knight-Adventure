using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Game
{
    public class HUD : MonoBehaviour, IGameDataPersistence
    {
        [SerializeField] private Slider _healthBar;
        [SerializeField] private TextMeshProUGUI _timeText;

        public static float ScoreTime { get; private set; }

        private void Start()
        {
            ScoreTime = 0f;
        }

        private void Update()
        {
            ScoreTime += Time.deltaTime;
            _timeText.text = FormatTime(ScoreTime);
        }

        public static string FormatTime(float time)
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

        public void LoadData(GameData data)
        {

        }

        public void SaveData(GameData data)
        {
            data.score = Mathf.Max(data.score, ScoreTime);
        }
    }
}

