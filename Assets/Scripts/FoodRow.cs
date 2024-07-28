using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

namespace Game
{
    public class FoodRow : MonoBehaviour
    {
        private const int MinPoolSize = 10;
        private const int MaxPoolSize = 20;

        [SerializeField]private Transform startingPoint;
        [SerializeField]private Food[] foodPrefabs;

        [Min(1)]
        [SerializeField]private int countLimit = 5;

        [Min(1.0f)]
        [SerializeField]private float rowLength = 5.0f;
        
        private ObjectPool<Food> _pool;
        private List<Food> _queue;

        public IEnumerator AddFoodInitially()
        {
            for(int i = 1; i <= countLimit; i++)
            {
                TaskManager.TaskState taskState = TaskManager.CreateTask(AddFood());
                taskState.Start();       
                yield return new WaitUntil(()=> !taskState.Running);
            }
        }

        public void RemoveFood()
        {

        }

        private IEnumerator AddFood()
        {
            float delta = rowLength/(float)countLimit;
            Debug.Log("Delta: " + delta);
            //Move present items in queue to specific distance towards the player.
            for(int i = _queue.Count - 1; i >= 0; i--)
            {
                Food current = _queue[i];
                Vector3 finalPosition = current.transform.position + transform.forward * delta;
                current.transform.position = finalPosition;
                yield return new WaitForSeconds(1.0f);
            }

            yield return new WaitForSeconds(1.0f);
            
            Food newFood = null;

            if(newFood == null)
            {
                newFood = _pool.Get();
            }

            Vector3 final = startingPoint.position + transform.forward * delta;
            newFood.transform.position = final;
              
            
            _queue.Insert(0, newFood);
        }
        private Food CreateFood()
        {
            Food food = Instantiate(foodPrefabs[Random.Range(0, foodPrefabs.Length)], startingPoint.position, Quaternion.identity);
            food.transform.parent = transform;
            food.gameObject.SetActive(false);
            return food;
        }

        private void OnFoodGet(Food food)
        {
            food.gameObject.SetActive(true);
        }

        private void OnFoodRelease(Food food)
        {
            food.gameObject.SetActive(false);
        }

        private void OnFoodDestroy(Food food)
        {
            Destroy(food.gameObject);
        }

        private void Awake() 
        {
            _queue = new List<Food>();
        }

        private void Start() 
        {
            _pool = new ObjectPool<Food>(CreateFood, OnFoodGet, OnFoodRelease, OnFoodDestroy, true, MinPoolSize, MaxPoolSize);    
        }


        private void OnDrawGizmos() 
        {
            Gizmos.color = Color.green;
            Gizmos.DrawLine(transform.position, transform.position + transform.forward * rowLength);    
        }
    }
}
