using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public class PlayingState : IState
    {
        private GameManager _gameManager;
        public PlayingState(GameManager gameManager)
        {
            _gameManager = gameManager;
        }

        public void OnEnter()
        {
            Debug.Log("Now in playing state");
            _gameManager.SetPlayerScoresActiveStatus(true);
            _gameManager.GenerateFoodInitially();
        }

        public void OnExit()
        {
            
        }

        public void OnFixedUpdate()
        {
            
        }

        public void OnUpdate()
        {
            
        }
    }
}
