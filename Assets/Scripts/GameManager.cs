using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Game
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField]private bool showUIControls = true;
        [SerializeField]private GameObject[] uiControls;
        [SerializeField]private PlayersSitter playersSitter;
        [SerializeField]private DollyTrackFollower stadiumDollyTrackFollower;
        [SerializeField]private PlayerScore[] playerScores;

        private StateMachine _gameSM;
        private PlayersSittingState _playersSittingState;
        private PlayingState _playingState;
        private StadiumShowingState _stadiumShowingState;

        public PlayersSitter PlayersSitter { get => playersSitter;  }
        public DollyTrackFollower StadiumDollyTrackFollower { get => stadiumDollyTrackFollower; }

        public void SetPlayerScoresActiveStatus(bool status)
        {
            foreach(var playerScore in playerScores)
            {
                playerScore.gameObject.SetActive(status);
            }
        }

        private void Awake() {
            _gameSM = new StateMachine();
            _stadiumShowingState = new StadiumShowingState(this);
            _playersSittingState = new PlayersSittingState(this);
            _playingState = new PlayingState(this);
        }

        // Start is called before the first frame update
        void Start()
        {
            Application.targetFrameRate = 60;
            if(playersSitter == null)
            {
                return;
            }
            _gameSM.AddTransition(_stadiumShowingState, _playersSittingState, ()=> stadiumDollyTrackFollower.ShownStadium);
            _gameSM.AddTransition(_playersSittingState, _playingState,()=> playersSitter.AreAllSitting);
            _gameSM.SetState(_stadiumShowingState);   
        }

        private void Update() {
            _gameSM.OnUpdate();
        }

        private void FixedUpdate() {
            _gameSM.OnFixedUpdate();
        }

        private void OnValidate() 
        {
            foreach(GameObject uiControl in uiControls)
            {
                uiControl.SetActive(showUIControls);
            }
        }
    }
}
