using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using TMPro;
namespace Game
{
    public class CameraSwitcher : MonoBehaviour
    {
        private const int MinPriority = 0;
        private const int MaxPriority = 10;

        [SerializeField]private CinemachineVirtualCameraBase[] cameras;
        [SerializeField]private TMP_Text cameraNameIndicator;

        private int _currentIndex = 0;

        private void Start() {
            ChangeToCamera(cameras[0]);
        }
        public void SwitchToNextCamera()
        {
            _currentIndex++;
            if(_currentIndex >= cameras.Length)
            {
                _currentIndex = 0;
            }

            ChangeToCamera(cameras[_currentIndex]);
        }

        public void SwitchToPreviousCamera()
        {
            _currentIndex--;
            if(_currentIndex < 0)
            {
                _currentIndex = cameras.Length - 1;
            }

            ChangeToCamera(cameras[_currentIndex]);
        }

        private void ChangeToCamera(CinemachineVirtualCameraBase camera)
        {
            for(int i = 0 ; i < cameras.Length; i++)
            {
                cameras[i].m_Priority = MinPriority;
            }

            camera.m_Priority = MaxPriority;
            cameraNameIndicator.text = camera.name;
        }
    }
}
