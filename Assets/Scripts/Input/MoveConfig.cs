using UnityEngine;

namespace RhythmGame
{
    [CreateAssetMenu(
            fileName = "InputConfig",
            menuName = "Gameplay/New InputConfig"
    )]
    public sealed class MoveConfig : ScriptableObject
    {
        public KeyCode left;
        public KeyCode right;
        public KeyCode forward;
        public KeyCode back;
    }
}