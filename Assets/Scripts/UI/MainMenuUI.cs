using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RenderDream.GameEssentials;

namespace Game
{
    public class MainMenuUI : MonoBehaviour
    {
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
    }
}
