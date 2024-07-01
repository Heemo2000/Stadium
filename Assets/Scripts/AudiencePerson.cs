using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Game
{
    public class AudiencePerson : MonoBehaviour
    {
        Animator _animator;

        
        private static string _excitementVariable = "excitement";
        
        public void SetExcitement(float excitementPercent)
        {
            _animator.SetFloat(_excitementVariable, excitementPercent);
        }
        private void Awake() {
            _animator = GetComponent<Animator>();
        }
        // Start is called before the first frame update
        void Start()
        {
            _animator.SetFloat(_excitementVariable, 0.0f);    
        }
    }

}
