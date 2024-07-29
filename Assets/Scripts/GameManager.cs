using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.UI;

namespace Game
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField]private PlayersSitter playersSitter;
        [SerializeField]private DollyTrackFollower stadiumDollyTrackFollower;
        
        [SerializeField]private UIManager uiManager;
        [SerializeField]private Page playPage;
        [SerializeField]private FoodRow[] foodRows;
        [SerializeField]private FirstPersonManager firstPersonManager;

        private StateMachine _gameSM;
        private PlayersSittingState _playersSittingState;
        private PlayingState _playingState;
        private StadiumShowingState _stadiumShowingState;

        public PlayersSitter PlayersSitter { get => playersSitter;  }
        public DollyTrackFollower StadiumDollyTrackFollower { get => stadiumDollyTrackFollower; }

        public void PopAllPages()
        {
            uiManager.PopAllPages();
            firstPersonManager.Setup();
        }

        public void GenerateFoodInitially()
        {
            for(int i = 0; i < foodRows.Length; i++)
            {
                Debug.Log("Generating food...");
                StartCoroutine(foodRows[i].AddFoodInitially());
            }
        }

        public void ShowPlayPage()
        {
            uiManager.PushPage(playPage);
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
    }
}
