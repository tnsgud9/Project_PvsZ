using System;
using Collections;
using Enums;
using Events;
using UnityEngine;

namespace Managers
{
    public class InputManager : DestoryableSingleton<InputManager>
    {
        
        private void Start()
        {
            EventBus<GameEventType>.Subscribe(GameEventType.GameStart, () =>
            {
                foreach (InputSystem inputSystem in FindObjectsOfType<InputSystem>())
                {
                    inputSystem.enabled = true;
                }
            });
            EventBus<GameEventType>.Subscribe(GameEventType.GameOver, () =>
            {
                foreach (InputSystem inputSystem in FindObjectsOfType<InputSystem>())
                {
                    inputSystem.enabled = false;
                }
            });
            EventBus<GameEventType>.Subscribe(GameEventType.GameClear, () =>
            {
                foreach (InputSystem inputSystem in FindObjectsOfType<InputSystem>())
                {
                    inputSystem.enabled = false;
                }
            });
        }
    }
}