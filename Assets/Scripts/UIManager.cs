using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public class UIManager : MonoBehaviour
    {
        [SerializeField]private Canvas[] canvases;
        [SerializeField]private Canvas startingCanvas;

        private Canvas _lastCanvas;
        public void Open(Canvas canvas)
        {
            if(canvas == null)
            {
                return;
            }
            DisableAllCanvases();
            canvas.gameObject.SetActive(true);
            _lastCanvas = canvas;
        }
        
        public void CloseLastCanvas()
        {
            if(_lastCanvas == null)
            {
                return;
            }

            _lastCanvas.gameObject.SetActive(false);
        }
        private void DisableAllCanvases()
        {
            for(int i = 0; i < canvases.Length; i++)
            {
                canvases[i].gameObject.SetActive(false);
            }
        }

        private void Start() 
        {
            DisableAllCanvases();
            if(startingCanvas != null)
            {
                Open(startingCanvas);
            }
        }


    }
}
