using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public class LinearAudienceGenerator : MonoBehaviour
    {
        
        [Range(0.0f,1.0f)]
        [SerializeField]private float spawnThreshold = 0.8f;
        [SerializeField]private GameObject[] audiencePersons;
        [Min(0.0f)]
        [SerializeField]private float maxSpawnRange = 10.0f;
        [Min(0.0f)]
        [SerializeField]private float spacing = 2.0f;

        private IEnumerator SpawnAudience()
        {
            float currentRange = 0.0f;
            while(currentRange < maxSpawnRange)
            {
                bool whetherToSpawn = Random.value >= spawnThreshold;
                if(whetherToSpawn)
                {
                    Vector3 spawnPosition = transform.position + transform.right * currentRange;
                    var human = Instantiate(audiencePersons[Random.Range(0, audiencePersons.Length)], spawnPosition, Quaternion.identity);
                    human.transform.right = -transform.right;
                    human.transform.parent = transform;
                }
                yield return null;
                currentRange += spacing;
            }
        }

        // Start is called before the first frame update
        void Start()
        {
            StartCoroutine(SpawnAudience());
        }



        private void OnDrawGizmos() 
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawLine(transform.position, transform.position + transform.right * maxSpawnRange);    
        }
    }

}
