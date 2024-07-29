using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.UI
{
    public class UIManager : MonoBehaviour
    {
        [SerializeField]
        private Page InitialPage;
    
        private Stack<Page> _pageStack;

        private void Awake() {
            _pageStack = new Stack<Page>();
        }
        private void Start()
        {
            if (InitialPage != null)
            {
                PushPage(InitialPage);
            }
        }

        public bool IsPageInStack(Page page)
        {
            return _pageStack.Contains(page);
        }

        public bool IsPageOnTopOfStack(Page page)
        {
            return _pageStack.Count > 0 && page == _pageStack.Peek();
        }

        public void PushPage(Page page)
        {
            
            if(!page.gameObject.activeInHierarchy)
            {
                page.gameObject.SetActive(true);
            }
            page.Enter();

            if (_pageStack.Count > 0)
            {
                Page currentPage = _pageStack.Peek();

                if (currentPage.exitOnNewPagePush)
                {
                    currentPage.Exit();
                }
            }

            _pageStack.Push(page);
        }

        public void PopPage()
        {
            if (_pageStack.Count > 1)
            {
                Page page = _pageStack.Pop();
                page.Exit();

                Page newCurrentPage = _pageStack.Peek();
                if (newCurrentPage.exitOnNewPagePush)
                {
                    newCurrentPage.Enter();
                }
            }
            else
            {
                Debug.LogWarning("Trying to pop a page but only 1 page remains in the stack!");
            }
        }

        public void PopAllPages()
        {
            for (int i = 1; i < _pageStack.Count; i++)
            {
                PopPage();
            }
        }
    }
}
