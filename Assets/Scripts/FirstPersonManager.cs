using UnityEngine;
using UnityEngine.UI;
using Cinemachine;
using Game.UI;

namespace Game
{
    public class FirstPersonManager : MonoBehaviour
    {
        
        [SerializeField]private Button[] playerBtns;

        [SerializeField]private Button leftCameraBtn;
        [SerializeField]private Button centreCameraBtn;
        [SerializeField]private Button rightCameraBtn;
        [SerializeField]private Button goBackBtn;
        [SerializeField]private PlayersSitter playersSitter;
        [SerializeField]private UIManager uiManager;
        
        
        [SerializeField]private Page firstPersonPage;
        [SerializeField]private CinemachineVirtualCamera topCamera;


        public void Setup()
        {
            foreach(Player player in playersSitter.Players)
            {
                CameraSwitcher.Instance.AddCamera(player.GetLeftCamera());
                CameraSwitcher.Instance.AddCamera(player.GetCentreCamera());
                CameraSwitcher.Instance.AddCamera(player.GetRightCamera());
            }

            int i = 0;
            foreach(Button playerBtn in playerBtns)
            {
                Player player = playersSitter.Players[i];
                playerBtn.onClick.AddListener(()=> {
                    
                    SwitchToCentreCamera(player);
                    uiManager.PushPage(firstPersonPage);
                    
                    leftCameraBtn.onClick.AddListener(()=> SwitchToLeftCamera(player));
                    
                    centreCameraBtn.onClick.AddListener(()=> SwitchToCentreCamera(player));

                    rightCameraBtn.onClick.AddListener(()=> SwitchToRightCamera(player));

                });
            }

            goBackBtn.onClick.AddListener(()=>{
                CameraSwitcher.Instance.ChangeToCamera(topCamera);
                uiManager.PopPage();

                leftCameraBtn.onClick.RemoveAllListeners();
                centreCameraBtn.onClick.RemoveAllListeners();
                rightCameraBtn.onClick.RemoveAllListeners();
            });
        }

        private void SwitchToLeftCamera(Player player)
        {
            CameraSwitcher.Instance.ChangeToCamera(player.GetLeftCamera());            
        }

        private void SwitchToCentreCamera(Player player)
        {
            CameraSwitcher.Instance.ChangeToCamera(player.GetCentreCamera());
        }

        private void SwitchToRightCamera(Player player)
        {
            CameraSwitcher.Instance.ChangeToCamera(player.GetRightCamera());
        }

        
    }
}
