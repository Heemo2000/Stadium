using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public class BezierAudienceGenerator : MonoBehaviour
    {
        [SerializeField]private Transform midPoint;
        [SerializeField]private Transform endPoint;
        [SerializeField]private GameObject[] audiencePersonPrefab;
        [SerializeField]private int segments = 10;
        [Range(0.0f,1.0f)]
        [SerializeField]private float spawnThreshold = 0.8f;
        


        // Start is called before the first frame update
        void Start()
        {
            GenerateAudience();
        }

        private void GenerateAudience()
        {
            Vector3 p0 = transform.position;
            Vector3 p1 = midPoint.position;
            Vector3 p2 = endPoint.position;

            float delta = 1/(float)segments;
            float t = 0.0f;
            Vector3 previousBezierPoint = p0;
            Vector3 bezierPoint = previousBezierPoint;
            
            t += delta;
            for(int i = 1; i < segments; i++)
            {
                bool whetherToSpawn = Random.value >= spawnThreshold;
                if(!whetherToSpawn)
                {
                    continue;
                }
                bezierPoint = p1 + (1.0f - t) * (1.0f - t) * (p0 - p1) + t * t * (p2 - p1);
                t += delta;
                
                var audiencePerson = Instantiate(audiencePersonPrefab[Random.Range(0, audiencePersonPrefab.Length)], bezierPoint, Quaternion.identity);
                
                Vector3 direction = (bezierPoint - previousBezierPoint).normalized;
                audiencePerson.transform.right = -direction;
                audiencePerson.transform.parent = transform;
                previousBezierPoint = bezierPoint;
            }

            var lastAudiencePerson =  Instantiate(audiencePersonPrefab[Random.Range(0, audiencePersonPrefab.Length)], p2, Quaternion.identity);
                
            Vector3 lastDirection = (p2- bezierPoint).normalized;
            lastAudiencePerson.transform.right = -lastDirection;
            lastAudiencePerson.transform.parent = transform;

        }

        private void OnDrawGizmos() 
        {
            if(midPoint == null || endPoint == null)
            {
                return;
            }
            Vector3 p0 = transform.position;
            Vector3 p1 = midPoint.position;
            Vector3 p2 = endPoint.position;

            Gizmos.color = Color.red;
            Gizmos.DrawSphere(p0, 0.1f);

            Gizmos.color = Color.yellow;
            Gizmos.DrawSphere(p1, 0.1f);

            Gizmos.color = Color.blue;
            Gizmos.DrawSphere(p2, 0.1f);

            Gizmos.color = Color.white;

            float delta = 1/(float)segments;
            float t = 0.0f;
            Vector3 previousBezierPoint = p0;
            Vector3 bezierPoint = previousBezierPoint;
            t += delta;
            for(int i = 1; i < segments; i++)
            {
                bezierPoint = p1 + (1.0f - t) * (1.0f - t) * (p0 - p1) + t * t * (p2 - p1);
                t += delta;
                
                Gizmos.DrawSphere(bezierPoint, 0.1f);
                Gizmos.DrawLine(previousBezierPoint, bezierPoint);
                previousBezierPoint = bezierPoint;
            }

            Gizmos.DrawLine(bezierPoint, p2);
        }
    }
}
