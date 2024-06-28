using UnityEngine;

namespace RhythmGame
{
    public sealed class MoveController : MonoBehaviour, IGameUpdateListener
    {
        [SerializeField] private Character _character;
        [SerializeField] private InputManager _inputManager;

        private void Start()
        {
            IGameListener.Register(this);
        }

        
        public void OnUpdate(float deltaTime)
        {
            _character.Move(_inputManager.Move(), Time.deltaTime);
            //Debug.Log("tick");
        }
    }
}