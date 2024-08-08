using UnityEngine;
using UnityEngine.UI;
using Cinemachine;
using Game.UI;
using System.Collections;

namespace Game
{
    public class FirstPersonManager : MonoBehaviour
    {
        
        [SerializeField]private Button[] playerBtns;

        [SerializeField]private Button leftCameraBtn;
        [SerializeField]private Button sitBtn;
        [SerializeField]private Button focusFrontBtn;

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
                    
                    sitBtn.onClick.AddListener(()=> {
                        SwitchToCentreCamera(player);
                        player.Sit();
                        SetCentreCameraWide(player,sitBtn);
                    });

                    focusFrontBtn.onClick.AddListener(()=> {
                        SwitchToCentreCamera(player);
                        player.Stand();
                        SetCentreCameraLow(player, focusFrontBtn);
                    });
                    
                    rightCameraBtn.onClick.AddListener(()=> SwitchToRightCamera(player));

                });
                i++;
            }

            goBackBtn.onClick.AddListener(()=>{
                CameraSwitcher.Instance.ChangeToCamera(topCamera);
                uiManager.PopPage();

                leftCameraBtn.onClick.RemoveAllListeners();
                sitBtn.onClick.RemoveAllListeners();
                rightCameraBtn.onClick.RemoveAllListeners();
            });
        }

        private void SetCentreCameraLow(Player player, Button button)
        {
            TaskManager.TaskState taskState = TaskManager.CreateTask(SetCameraFOV(player.GetCentreCamera(),button, 20.0f, 1.0f));
            taskState.Start();   
        }
        private void SetCentreCameraWide(Player player, Button button)
        {
            TaskManager.TaskState taskState = TaskManager.CreateTask(SetCameraFOV(player.GetCentreCamera(),button, 60.0f, 1.0f));
            taskState.Start();
        }

        private IEnumerator SetCameraFOV(CinemachineVirtualCamera camera,Button button, float finalFOV, float speed)
        {
            float initialFOV = camera.m_Lens.FieldOfView;
            float delta = 0.0f;
            button.interactable = false;
            while(delta <= 1.0f)
            {
                camera.m_Lens.FieldOfView = Mathf.Lerp(initialFOV, finalFOV, delta);
                delta += speed * Time.deltaTime;
                yield return null;
            }
            button.interactable = true;
            camera.m_Lens.FieldOfView = finalFOV;
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
