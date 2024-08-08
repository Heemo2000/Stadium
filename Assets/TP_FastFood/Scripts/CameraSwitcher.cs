using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using TMPro;
namespace Game
{
    public class CameraSwitcher : GenericSingleton<CameraSwitcher>
    {
        private const int MinPriority = 0;
        private const int MaxPriority = 10;

        [SerializeField]private List<CinemachineVirtualCameraBase> cameras;
        [SerializeField]private TMP_Text cameraNameIndicator;

        private int _currentIndex = 0;

        public void AddCamera(CinemachineVirtualCameraBase camera)
        {
            cameras.Add(camera);
        }
        public void SwitchToNextCamera()
        {
            _currentIndex++;
            if(_currentIndex >= cameras.Count)
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
                _currentIndex = cameras.Count - 1;
            }

            ChangeToCamera(cameras[_currentIndex]);
        }

        public void Focus(CinemachineVirtualCameraBase camera, Transform target)
        {
            ChangeToCamera(camera);
            camera.Follow = target;
            camera.LookAt = target;
        }

        public void ChangeToCamera(CinemachineVirtualCameraBase camera)
        {
            for(int i = 0 ; i < cameras.Count; i++)
            {
                cameras[i].m_Priority = MinPriority;
            }

            camera.m_Priority = MaxPriority;
            cameraNameIndicator.text = camera.name;
        }

        private void Start() {
            ChangeToCamera(cameras[0]);
        }
    }
}
