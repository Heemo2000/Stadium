

using UnityEngine;

namespace Game
{
    public interface IState
    {
        void OnUpdate();
        void OnFixedUpdate();
        void OnEnter();
        void OnExit();
    }

}
