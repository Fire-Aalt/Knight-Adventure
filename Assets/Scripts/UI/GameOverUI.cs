using MoreMountains.Feedbacks;
using UnityEngine;
using UnityEngine.UI;
using RenderDream.GameEssentials;

namespace Game
{
    public class GameOverUI : MonoBehaviour
    {
        [SerializeField] private MMF_Player _showOverlayPlayer;
        [SerializeField] private MMF_Player _hideOverlayPlayer;

        private void ShowOverlay()
        {
            _showOverlayPlayer.PlayFeedbacks();
        }

        public void MainMenu()
        {
            if (!SceneLoader.IsTransitioning)
            {
                SceneLoader.Current.LoadSceneWithTransition(SceneType.MainMenu).Forget();
                _hideOverlayPlayer.PlayFeedbacks();
            }
        }

        private void OnEnable()
        {
            Player.OnDeath += ShowOverlay;
        }

        private void OnDisable()
        {
            Player.OnDeath -= ShowOverlay;
        }
    }
}
