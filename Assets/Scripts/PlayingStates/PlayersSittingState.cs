using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public class PlayersSittingState : IState
    {
        private GameManager _gameManager;
        public PlayersSittingState(GameManager gameManager)
        {
            _gameManager = gameManager;
        }

        public void OnEnter()
        {
            _gameManager.PlayersSitter.MakeAllPlayersSit();
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
