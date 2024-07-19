using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public class StadiumShowingState : IState
    {
        private GameManager _gameManager;
        public StadiumShowingState(GameManager gameManager)
        {
            _gameManager = gameManager;
        }
        public void OnEnter()
        {
            _gameManager.StadiumDollyTrackFollower.FollowDollyTrack();
            _gameManager.SetPlayerScoresActiveStatus(false);
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
