using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace Game
{
    public class FPSCounter : MonoBehaviour
    {
        [SerializeField]private TMP_Text fpsText;
        private int _lastFrameIndex;
        private float[] _frameUnscaledDeltaTimeArray;

        private void Awake() 
        {
            _frameUnscaledDeltaTimeArray = new float[50];    
        }

        // Update is called once per frame
        void Update()
        {
            _frameUnscaledDeltaTimeArray[_lastFrameIndex] = Time.unscaledDeltaTime;
            _lastFrameIndex = (_lastFrameIndex + 1) % _frameUnscaledDeltaTimeArray.Length;

            float total = 0.0f;
            foreach(float unscaledDeltaTime in _frameUnscaledDeltaTimeArray)
            {
                total += unscaledDeltaTime;
            }

            float fps = _frameUnscaledDeltaTimeArray.Length / total;
            if(fpsText != null)
            {
                fpsText.text = Mathf.RoundToInt(fps).ToString();
            }

        }


    }
}
