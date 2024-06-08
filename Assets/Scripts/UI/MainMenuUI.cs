using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RenderDream.GameEssentials;
using TMPro;

namespace Game
{
    public class MainMenuUI : MonoBehaviour, IGameDataPersistence
    {
        [SerializeField] private TextMeshProUGUI _score;
        [SerializeField] private string _format;

        public void Gameplay()
        {
            if (!SceneLoader.IsTransitioning)
            {
                SceneLoader.Current.LoadSceneWithTransition(SceneType.Gameplay).Forget();
            }
        }


        public void QuitApplication()
        {
            Application.Quit();
        }

        public void LoadData(GameData data)
        {
            _score.text = _format.Replace("{time}", HUD.FormatTime(data.score));
        }

        public void SaveData(GameData data)
        {
            
        }
    }
}
