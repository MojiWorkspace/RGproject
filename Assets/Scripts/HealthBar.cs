using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

namespace RhythmGame {
    public class HealthBar : MonoBehaviour, IGameUpdateListener
    {
        int _healthCount = 5;
        public TMPro.TextMeshPro healthString;
        public GameManager gameManager;
        public void setHealthCount(int number)
        {
            _healthCount += number;
            if (_healthCount < 0) _healthCount = 0;
        }
        void Start() => IGameListener.Register(this);
        public void OnUpdate(float deltaTime)
        {
            healthString.text = "Health: " + _healthCount.ToString();
            if (_healthCount == 0) gameManager.FinishGame();
        }

        public void OnFinishGame(float deltaTime) => healthString.text = "";
    }
}