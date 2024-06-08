using Game.UI;
using MoreMountains.Feedbacks;
using UnityEngine;
using RenderDream.GameEssentials;

namespace Game
{
    public class PauseInputUI : MonoBehaviour
    {
        [Header("Data")]
        [SerializeField] private PageController _settingsPageController;

        [Header("MMF Players")]
        [SerializeField] private MMF_Player _pauseMMF;
        [SerializeField] private MMF_Player _resumeMMF;

        private PageController _pageController;

        private void Awake()
        {
            _pageController = GetComponent<PageController>();
        }

        private void HandlePauseInput()
        {
            if (PauseManager.State == PauseStates.None)
            {
                Pause();
            }
            else if (PauseManager.State == PauseStates.PauseMenu)
            {
                Resume();
            }
        }

        private void Pause()
        {
            _resumeMMF.StopFeedbacks();
            _pauseMMF.PlayFeedbacks();
            _pageController.TransitionToFirstPage();
            PauseManager.UpdatePauseState(PauseStates.PauseMenu);
        }

        private void Resume()
        {
            _pauseMMF.StopFeedbacks();
            _resumeMMF.PlayFeedbacks();
            _pageController.TransitionOut();
            PauseManager.UpdatePauseState(PauseStates.None);
        }

        public void TransitionToSettingsMenu()
        {
            _pageController.TransitionToController(_settingsPageController);
        }


        public void Continue()
        {
            Resume();
        }

        public void ReturnToMainMenu()
        {
            if (!SceneLoader.IsTransitioning)
            {
                Resume();
                SceneLoader.Current.LoadSceneWithTransition(SceneType.MainMenu);
            }
        }

        private void OnEnable()
        {
            InputManager.OnPauseInput += HandlePauseInput;
        }

        private void OnDisable()
        {
            InputManager.OnPauseInput -= HandlePauseInput;
        }
    }
}
