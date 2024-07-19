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
        
        private ObjectPool<Food> _pool;
        
        private Food CreateFood()
        {
            Food food = Instantiate(foodPrefabs[Random.Range(0, foodPrefabs.Length)], Vector3.zero, Quaternion.identity);
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

        private void Awake() {
            _pool = new ObjectPool<Food>(CreateFood, OnFoodGet, OnFoodRelease, OnFoodDestroy, true, MinPoolSize, MaxPoolSize);
        }


        // Start is called before the first frame update
        void Start()
        {
        
        }

        // Update is called once per frame
        void Update()
        {
        
        }
    }
}
