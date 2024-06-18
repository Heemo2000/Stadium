using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Game
{
    public class AudiencePerson : MonoBehaviour
    {
        Animation _animation;
        private static string[] animationNames = {"idle", "applause", "applause2", "celebration", "celebration2", "celebration3"};
        private void Awake() {
            _animation = GetComponent<Animation>();
        }
        // Start is called before the first frame update
        void Start()
        {
            _animation.Play(animationNames[Random.Range(0, animationNames.Length)]);
        }
    }

}
