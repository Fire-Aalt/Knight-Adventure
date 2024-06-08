using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public class MobileInputHandler : MonoBehaviour
    {
        public Joystick Joystick;

        private PlayerInputHandler _inputHandler;

        private void Start()
        {
            _inputHandler = PlayerInputHandler.Active;
        }

        private void HandleInputChanged(Vector2 input)
        {
            _inputHandler.Movement = input;
        }

        private void OnEnable()
        {
            Joystick.OnInputChanged += HandleInputChanged;
        }

        private void OnDisable()
        {
            Joystick.OnInputChanged -= HandleInputChanged;
        }
    }
}
