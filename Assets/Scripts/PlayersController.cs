using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
using UnityEngine.UI;

namespace Game
{
    
    public class PlayersController : MonoBehaviour
    {
        [SerializeField]private Transform[] tunnelSpawnPoints;
        [SerializeField]private Player[] playerPrefabs;
        [SerializeField]private Button[] playerButtons;
        [SerializeField]private Transform[] playerDestinations;
        [SerializeField]private Transform[] middlePoints;
        [SerializeField]private CameraSwitcher cameraSwitcher;
        [SerializeField]private CinemachineVirtualCameraBase focusCamera;

        private List<Player> _players;
        
        private void InitializePlayer(Player player, Button button, Transform destination, Transform middlePoint)
        {
            button.onClick.AddListener(()=> {
                player.SetDestination(destination.position, destination, middlePoint);
                cameraSwitcher.Focus(focusCamera, player.transform);
            });
        }
        private void SpawnPlayers()
        {
            int i = 0;
            foreach(Transform spawnPoint in tunnelSpawnPoints)
            {
                var player = Instantiate(playerPrefabs[Random.Range(0, playerPrefabs.Length)], spawnPoint.position, Quaternion.identity);
                _players.Add(player);
                InitializePlayer(player, playerButtons[i], playerDestinations[i], middlePoints[i]);
                i++;
            }
        }

        private void Awake() {
            _players = new List<Player>();
        }

        // Start is called before the first frame update
        void Start()
        {
            SpawnPlayers();
        }
    }
}
