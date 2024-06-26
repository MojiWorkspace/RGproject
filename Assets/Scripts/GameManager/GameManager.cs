using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace RhythmGame
{        
    public enum GameState
    {
        Start,
        Finish,
        Pause,
        Resume,
        Off
    }
    
 public class GameManager : MonoBehaviour
    {
        [SerializeField, ReadOnly]
        private GameState _gameState;
        
        private readonly List<IGameListener> _gameListeners = new();
        private readonly List<IGameUpdateListener> _gameUpdateListeners = new();
        private readonly List<IGameFixedUpdateListener> _gameFixedUpdateListeners = new();
        
        public event Action OnStartGame;

        private void Awake()
        {
            _gameState = GameState.Off;
            IGameListener.onRegister += AddListener;
        }
        
        private void OnDestroy()
        {
            _gameState = GameState.Finish;
            IGameListener.onRegister -= AddListener;
        }
        
        private void Update()
        {
            if (_gameState != GameState.Start)
            {
                return;
            }
        
            var deltaTime = Time.deltaTime;
            for (var i = 0; i < _gameUpdateListeners.Count; i++)
            {
                _gameUpdateListeners[i].OnUpdate(deltaTime);
            }
        }
        private void FixedUpdate()
        {
            if (_gameState != GameState.Start)
            {
                return;
            }

            var deltaTime = Time.deltaTime;
            for (var i = 0; i < _gameFixedUpdateListeners.Count; i++)
            {
                _gameFixedUpdateListeners[i].OnFixedUpdate(deltaTime);
            }
        }

        
        private void AddListener(IGameListener gameListener)
        {
            _gameListeners.Add(gameListener);
            
            if (gameListener is IGameUpdateListener gameUpdateListener)
            {
                _gameUpdateListeners.Add(gameUpdateListener);
            }   
            
            if (gameListener is IGameFixedUpdateListener gameFixedUpdateListener)
            {
                _gameFixedUpdateListeners.Add(gameFixedUpdateListener);
            }
        }
        
        [Button]
        public void StartGame()
        {
            foreach (var gameListener in _gameListeners)
            {
                if (gameListener is IGameStartListener gameStartListener)
                {
                    gameStartListener.OnStartGame();
                }
            }
            
            
            _gameState = GameState.Start;
            
            //Такая конструкция нужна для делея в 3 секунды
            //OnStartGame?.Invoke(); 
            //Invoke("StartGameAfterCountdown", 1f);
            
            Debug.Log("StartGame");
        }

        private void StartGameAfterCountdown()
        {
            _gameState = GameState.Start;
        }

        [Button]
        public void FinishGame()
        {
            foreach (var gameListener in _gameListeners)
            {
                if (gameListener is IGameFinishListener gameFinishListener)
                {
                    gameFinishListener.OnFinishGame();
                }
            }
            
            Time.timeScale = 0;
            Debug.Log("FinishGame");
            _gameState = GameState.Finish;
        }
        
        [Button]
        public void PauseGame()
        {
            foreach (var gameListener in _gameListeners)
            {
                if (gameListener is IGamePauseListener gamePauseListener)
                {
                    gamePauseListener.OnPauseGame();
                }
            }
            Time.timeScale = 0;
            Debug.Log("PauseGame");
            _gameState = GameState.Pause;
        }   
        
        [Button]
        public void ResumeGame()
        {
            foreach (var gameListener in _gameListeners)
            {
                if (gameListener is IGameResumeListener gameResumeListener)
                {
                    gameResumeListener.OnResumeGame();
                }
            }
            Time.timeScale = 1;
            Debug.Log("ResumeGame");
            _gameState = GameState.Start;
        }
    }
}