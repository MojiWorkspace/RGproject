using UnityEngine;

namespace RhythmGame
{
    public class Test : MonoBehaviour, IGameUpdateListener
    {
        private void Start()
        {
            IGameListener.Register(this);
        }
        
        public void OnUpdate(float deltaTime)
        {
            Debug.Log("+");
        }
    }
}