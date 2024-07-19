using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


namespace Game
{
    public class PlayerScore : MonoBehaviour
    {
        [SerializeField]private TMP_Text playerNameText;
        [SerializeField]private TMP_Text scoreText;

        public void SetNameAndScore(string name, float score)
        {
            playerNameText.text = name;
            scoreText.text = Mathf.RoundToInt(score).ToString();
        }
    }
}
