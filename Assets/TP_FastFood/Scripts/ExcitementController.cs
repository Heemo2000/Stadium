using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using TMPro;
using Random = UnityEngine.Random;
namespace Game
{
    public class ExcitementController : MonoBehaviour
    {
        public enum ExcitementState
        {
            
            NoExcitement = 1,
            VeryLittleExcitement,
            LittleExcitment,
            AlmostExcitement,
            FullExcitement
        }

        //[SerializeField]private ExcitementState currentState = ExcitementState.NoExcitement;
        [SerializeField]private TMP_Dropdown stateDropdown;
        private BezierAudienceGenerator[] _bezierAudienceGenerators;
        private static float[] _excitementPercentages = {0.0f, 0.33f, 0.66f, 1.0f};
        
        //private ExcitementState _previousState;
        /*
        Sitting idle = 0%
        Idle = 33.3 %
        Sitting cheering = 66.6 %
        Cheering = 100 %
        */

        /*
        Required 4 States from audiance
        1. Everyone sitting
        2. Few are standing and sitting 30% Cheer
        3. Few more are standing and cheering 50%
        4. Almost members are standing are cheering 70%
        5. Everyone is standing and cheering 100%
        */

        public void ControlExcitement(ExcitementState excitementState)
        {
            switch(excitementState)
            {
                case ExcitementState.NoExcitement:
                                                    foreach(var bag in _bezierAudienceGenerators)
                                                    {
                                                        foreach(var person in bag.AudiencePersons)
                                                        {
                                                            person.SetExcitement(_excitementPercentages[0]);
                                                        }
                                                    }
                                                    break;

                case ExcitementState.VeryLittleExcitement:
                case ExcitementState.LittleExcitment:
                case ExcitementState.AlmostExcitement:
                                                    float cheerPercent = 0.0f;
                                                    if(excitementState == ExcitementState.VeryLittleExcitement)
                                                    {
                                                        cheerPercent = 0.3f;
                                                    }
                                                    else if(excitementState == ExcitementState.LittleExcitment)
                                                    {
                                                        cheerPercent = 0.5f;
                                                    }
                                                    else if(excitementState == ExcitementState.AlmostExcitement)
                                                    {
                                                        cheerPercent = 0.7f;
                                                    }

                                                    int count = 0;
                                                    int numberOfPeople = Mathf.RoundToInt(cheerPercent * GetTotalAudienceCount());
                                                    
                                                    foreach(var bag in _bezierAudienceGenerators)
                                                    {
                                                        
                                                        foreach(var person in bag.AudiencePersons)
                                                        {
                                                            bool toChoosePerson = Random.value >= 0.5f;
                                                            if(!toChoosePerson)
                                                            {
                                                                continue;
                                                            }
                                                            
                                                            if(count <= numberOfPeople)
                                                            {
                                                                bool shouldBeStanding = Random.value >= 0.5f;
                                                                if(shouldBeStanding)
                                                                {
                                                                    person.SetExcitement(_excitementPercentages[3]);
                                                                }
                                                                else
                                                                {
                                                                    person.SetExcitement(_excitementPercentages[2]);
                                                                }
                                                            }
                                                            else
                                                            {
                                                                bool shouldBeStanding = Random.value >= 0.4f;
                                                                if(shouldBeStanding)
                                                                {
                                                                    person.SetExcitement(_excitementPercentages[1]);
                                                                }
                                                                else
                                                                {
                                                                    person.SetExcitement(_excitementPercentages[0]);
                                                                }
                                                            }
                                                            count++;
                                                        }
                                                    }
                                                    break;

                case ExcitementState.FullExcitement:
                                                    foreach(var bag in _bezierAudienceGenerators)
                                                    {
                                                        foreach(var person in bag.AudiencePersons)
                                                        {
                                                            person.SetExcitement(_excitementPercentages[3]);
                                                        }
                                                    }
                                                    break;
                
                                                    
            }
        }
        private int GetTotalAudienceCount()
        {
            int total = 0;
            foreach(var bag in _bezierAudienceGenerators)
            {
                total += bag.AudienceCount;
            }

            return total;
        }

        private IEnumerator InitializeCrowd()
        {
            yield return new WaitForSeconds(1.0f);
            ControlExcitement(ExcitementState.NoExcitement);
        }

        private void SelectCrowdState(int index)
        {
            string stateString = stateDropdown.options[index].text;
            bool foundState = false;
            switch(stateString)
            {
                case "State 1":
                                foundState = true;
                                ControlExcitement(ExcitementState.NoExcitement);
                                break;
                case "State 2":
                                foundState = true;
                                ControlExcitement(ExcitementState.VeryLittleExcitement);
                                break;
                case "State 3":
                                foundState = true;
                                ControlExcitement(ExcitementState.LittleExcitment);
                                break;
                case "State 4":
                                foundState = true;
                                ControlExcitement(ExcitementState.AlmostExcitement);
                                break;
                case "State 5":
                                foundState = true;
                                ControlExcitement(ExcitementState.FullExcitement);
                                break;
            }

            if(foundState)
            {
                Debug.Log("Found state");
            }

        }


        // Start is called before the first frame update
        void Start()
        {
            _bezierAudienceGenerators = FindObjectsByType<BezierAudienceGenerator>(FindObjectsInactive.Include, FindObjectsSortMode.InstanceID);
            StartCoroutine(InitializeCrowd());
            stateDropdown.onValueChanged.AddListener(SelectCrowdState);
        }

        private void Update()
        {
            /*
            if(currentState != _previousState)
            {
                ControlExcitement(currentState);
            }

            _previousState = currentState;
            */
        } 

        private void OnDestroy() 
        {
            stateDropdown.onValueChanged.RemoveListener(SelectCrowdState);    
        }
    }
}
