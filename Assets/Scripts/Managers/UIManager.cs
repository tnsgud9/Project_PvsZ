using System;
using Collections;
using Enums;
using UnityEngine;

namespace Managers
{
    public class UIManager : DestoryableSingleton<UIManager>
    {
        public GameObject gameOverView;
        public GameObject gameClearView;
        public GameObject plantView;


        private void Start()
        {
            EventBus<GameEventType>.Subscribe(GameEventType.GameOver, () =>
            {
                gameOverView.SetActive(true);
                plantView.SetActive(false);
                gameClearView.SetActive(false);
            });
            EventBus<GameEventType>.Subscribe(GameEventType.GameStart, () =>
            {
                gameOverView.SetActive(false);
                plantView.SetActive(true);
                gameClearView.SetActive(false);
            });
            EventBus<GameEventType>.Subscribe(GameEventType.GameClear, () =>
            {
                gameOverView.SetActive(false);
                plantView.SetActive(false);
                gameClearView.SetActive(true);
            });
        }
    }
}