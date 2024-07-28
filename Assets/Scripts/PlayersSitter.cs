using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

namespace Game
{
    public class PlayersSitter : MonoBehaviour
    {
        [SerializeField]private Transform[] tunnelSpawnPoints;
        [SerializeField]private Player[] playerPrefabs;
        [SerializeField]private Transform[] playerDestinations;
        [SerializeField]private Transform[] middlePoints;
        [SerializeField]private CinemachineVirtualCameraBase[] focusCameras;
        [SerializeField]private CinemachineVirtualCameraBase topCamera;

        private List<Player> _players;
        private bool _areAllSitting = false;
        private Coroutine _sittingCoroutine;

        public bool AreAllSitting { get => _areAllSitting; }

        public void MakeAllPlayersSit()
        {
            if(_sittingCoroutine == null && !_areAllSitting)
            {
                _sittingCoroutine = StartCoroutine(MakePlayersSit());
            }
        }

        private void SpawnPlayers()
        {
            int i = 0;
            foreach(Transform spawnPoint in tunnelSpawnPoints)
            {
                var player = Instantiate(playerPrefabs[Random.Range(0, playerPrefabs.Length)], spawnPoint.position, Quaternion.identity);
                _players.Add(player);
                i++;
            }
        }

        private CinemachineVirtualCameraBase GetClosestDollyCamera(Vector3 position)
        {
            float closestSqrDistance = float.MaxValue;
            CinemachineVirtualCameraBase dollyCamera = null;

            for(int i = 0; i < focusCameras.Length; i++)
            {
                float sqrDistance = Vector3.SqrMagnitude(focusCameras[i].transform.position - position);
                if(sqrDistance < closestSqrDistance)
                {
                    dollyCamera = focusCameras[i];
                    closestSqrDistance = sqrDistance;
                }
            }

            return dollyCamera;
        }

        private void InitializePlayer(Player player, Transform destination, Transform middlePoint)
        {
            
            player.SetDestination(destination.position, destination, middlePoint);
            var focusCamera = GetClosestDollyCamera(player.transform.position);
            CameraSwitcher.Instance.Focus(focusCamera, player.transform);
        }

        private IEnumerator MakePlayersSit()
        {
            WaitForSeconds wait = new WaitForSeconds(1.0f);
            yield return wait;
            for(int i = 0; i < tunnelSpawnPoints.Length; i++)
            {
                Player player = _players[i];
                Transform destination = playerDestinations[i];
                Transform middlePoint = middlePoints[i];

                player.SetDestination(destination.position, destination, middlePoint);
                var focusCamera = GetClosestDollyCamera(player.transform.position);
                CameraSwitcher.Instance.Focus(focusCamera, player.transform);

                yield return new WaitUntil(()=> player.IsSitting == true);
                yield return wait;
            }

            yield return wait;
            CameraSwitcher.Instance.ChangeToCamera(topCamera);
            _areAllSitting = true;
        }

        // Start is called before the first frame update
        void Start()
        {
            _players = new List<Player>();
            SpawnPlayers();
        }
    }
}
