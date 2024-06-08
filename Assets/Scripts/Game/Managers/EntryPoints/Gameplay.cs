using Cysharp.Threading.Tasks;
using RenderDream.GameEssentials;
using UnityEngine;

namespace Game
{
    public class Gameplay : MonoBehaviour
    {
        private EventBinding<ScenesLoadedEvent> _scenesLoaded;

        public static bool Initialized { get; private set; }

        private void HandleScenesLoaded(ScenesLoadedEvent @event)
        {
            InitializeGameplay().Forget();
        }

        private async UniTaskVoid InitializeGameplay()
        {
            await UniTask.WaitForEndOfFrame(this, this.GetCancellationTokenOnDestroy());

            // Set AudioListenerManager Follow Target
            AudioListenerManager.Current.SetFollowTarget(Player.Active.transform);
            Initialized = true;
        }

        //private void SetActiveBlits(bool isActive)
        //{
        //    UniversalRenderPipelineUtils.SetRendererFeatureActive("FullScreenPassRendererFeature", "LowHealth", isActive);
        //}

        private void OnEnable()
        {
            _scenesLoaded = new EventBinding<ScenesLoadedEvent>(HandleScenesLoaded);
            EventBus<ScenesLoadedEvent>.Register(_scenesLoaded);
            //SetActiveBlits(true);
        }

        private void OnDisable()
        {
            EventBus<ScenesLoadedEvent>.Deregister(_scenesLoaded);
            //SetActiveBlits(false);
        }
    }
}
