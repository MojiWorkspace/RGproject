using UnityEngine;

namespace RhythmGame
{
    public sealed class MoveController : MonoBehaviour
    {
        [SerializeField] private Character _character;
        [SerializeField] private InputManager _inputManager;

        void Update()
        {
            _character.Move(_inputManager.Move(), Time.deltaTime);
        }
    }
}