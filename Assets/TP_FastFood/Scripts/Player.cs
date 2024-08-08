using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using Cinemachine;

namespace Game
{
    public class Player : MonoBehaviour
    {
        [Min(0.1f)]
        [SerializeField]private float closestDistance = 0.5f;
        [SerializeField]private AnimationClip dancingAnimation;
        [SerializeField]private Transform middlePoint;
        [SerializeField]private CinemachineVirtualCamera leftCamera;
        [SerializeField]private CinemachineVirtualCamera centreCamera;
        [SerializeField]private CinemachineVirtualCamera rightCamera;

        private NavMeshAgent _agent;
        private Animator _animator;
        private Vector3 _destination;
        private int _moveInputID;
        private int _isSittingID;

        private int _isDancingID;
        private Transform _pivot;
        private bool _isSitting = false;
        
        private Coroutine _destCoroutine;

        public bool IsSitting { get => _isSitting; }

        public void Sit()
        {
            _animator.SetBool(_isSittingID, true);
            _isSitting = true;
        }

        public void Stand()
        {
            _animator.SetBool(_isSittingID, false);
            _isSitting = false;
            _animator.SetFloat(_moveInputID, 0.0f);
        }

        public CinemachineVirtualCamera GetLeftCamera()
        {
            return leftCamera;
        }

        public CinemachineVirtualCamera GetCentreCamera()
        {
            return centreCamera;
        }

        public CinemachineVirtualCamera GetRightCamera()
        {
            return rightCamera;
        }
        
        public void SetDestination(Vector3 position, Transform pivot, Transform middlePoint)
        {
            _destination = position;
            _pivot = pivot;
            this.middlePoint = middlePoint;
            //_agent.SetDestination(position);
            if(_destCoroutine == null)
            {
                _destCoroutine = StartCoroutine(GoToDestination());
            }
        }

        private IEnumerator GoToDestination()
        {
            _animator.SetFloat(_moveInputID, 1.0f);
            _animator.SetBool(_isSittingID, false);
            _agent.SetDestination(middlePoint.position);
            _agent.isStopped = false;
            yield return new WaitUntil(()=> Vector3.SqrMagnitude(middlePoint.position - transform.position) <= closestDistance * closestDistance);
            
            Debug.Log("Player is stopped");
            _agent.isStopped = true;
            //_animator.SetBool(_isWalkingID, false);
            _animator.SetFloat(_moveInputID, 0.0f);
            _animator.SetBool(_isDancingID, true);

            float delta = 0.0f;
            while(delta < dancingAnimation.length)
            {
                delta += Time.deltaTime;
                yield return null;
            }

            _animator.SetBool(_isDancingID, false);
            _animator.SetFloat(_moveInputID, 1.0f);
            _animator.SetBool(_isSittingID, false);
            _agent.SetDestination(_destination);
            _agent.isStopped = false;

            Debug.Log("Player is moving again.");

            yield return new WaitUntil(()=> Vector3.SqrMagnitude(_destination - transform.position) <= closestDistance * closestDistance);

            _animator.SetFloat(_moveInputID, 0.0f);
            _animator.SetBool(_isSittingID, true);
            _agent.enabled = false;
            transform.parent = _pivot;
            transform.localPosition = Vector3.zero;
            transform.forward = _pivot.forward;

            _isSitting = true;
        }
        private void Awake() {
            _agent = GetComponent<NavMeshAgent>();
            _animator = GetComponent<Animator>();
        }

        private void Start() 
        {
            _moveInputID = Animator.StringToHash("move_input");
            _isSittingID = Animator.StringToHash("is_sitting");
            _isDancingID = Animator.StringToHash("is_dancing");
        }   
    }
    
}
