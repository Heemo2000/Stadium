using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
namespace Game
{
    public class DollyTrackFollower : MonoBehaviour
    {

        [SerializeField]private CinemachineSmoothPath smoothPath;

        [SerializeField]private float moveSpeed = 10.0f;
        [SerializeField]private CameraSwitcher cameraSwitcher;

        private CinemachineVirtualCamera _dollyCamera;
        private Coroutine _followCoroutine;

        private bool _shownStadium = false;
        private GameObject _followGO;
        public bool ShownStadium { get => _shownStadium; }

        public void FollowDollyTrack()
        {
            if(_followCoroutine == null)
            {
                _followCoroutine = StartCoroutine(FollowTrack());
            }
        }
        private IEnumerator FollowTrack()
        {
            yield return new WaitForSeconds(0.1f);
            cameraSwitcher.ChangeToCamera(_dollyCamera);

            _dollyCamera.Follow = _followGO.transform;
            _dollyCamera.LookAt = _followGO.transform;
            float delta = 0.0f;
            Vector3 previousPosition = Vector3.zero;
            while(delta <= smoothPath.MaxPos)
            {
                Vector3 newPosition = smoothPath.EvaluatePosition(delta);
                Vector3 direction = (newPosition - previousPosition).normalized;
                //transform.right = direction;

                _followGO.transform.position = newPosition;
                _followGO.transform.right = direction;                
                delta += moveSpeed * Time.deltaTime;

                Debug.Log("Delta: " + delta);
                previousPosition = newPosition;
                yield return null;
            }

            _shownStadium = true;
        }

        private void Awake() {
            _dollyCamera = GetComponent<CinemachineVirtualCamera>();
        }

        private void Start() 
        {
            _followGO = new GameObject("Follow GO");
            _followGO.transform.position = transform.position;
            var trackedDolly = _dollyCamera.GetCinemachineComponent<CinemachineTrackedDolly>();
            trackedDolly.m_PathPosition = 0;
        }
        
    }
}
