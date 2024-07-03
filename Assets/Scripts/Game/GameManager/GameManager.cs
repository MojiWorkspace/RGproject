using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.SceneManagement;

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

        private void AddListener(IGameListener gameListener)
        {
            _gameListeners.Add(gameListener);

            if (gameListener is IGameUpdateListener gameUpdateListener) _gameUpdateListeners.Add(gameUpdateListener);

            if (gameListener is IGameFixedUpdateListener gameFixedUpdateListener) _gameFixedUpdateListeners.Add(gameFixedUpdateListener);
        }

        private void Awake() => IGameListener.onRegister += AddListener;
        private void Start() => StartGame();
        private void OnDestroy()
        {
            _gameState = GameState.Finish;
            IGameListener.onRegister -= AddListener;
        }
        private void Update()
        {
            var deltaTime = Time.deltaTime;

            if (_gameState == GameState.Off) return;

            for (var i = 0; i < _gameUpdateListeners.Count; i++)
            {
                if (_gameUpdateListeners[i] != null)  _gameUpdateListeners[i].OnUpdate(deltaTime);
            }
        }
        private void FixedUpdate()
        {
            var deltaTime = Time.deltaTime;

            // if (_gameState != GameState.Start)  return;
            if (_gameState == GameState.Off) return;

            for (var i = 0; i < _gameFixedUpdateListeners.Count; i++)  _gameFixedUpdateListeners[i].OnFixedUpdate(deltaTime);
        }

        [Button]
        public void StartGame()
        {
            Debug.Log("StartGame");
            _gameState = GameState.Start;
            Time.timeScale = 1;

            foreach (var gameListener in _gameListeners)
            {
                if (gameListener is IGameStartListener gameStartListener) gameStartListener.OnStartGame();
            }
            //Такая конструкция нужна для делея в 3 секунды
            //OnStartGame?.Invoke();
            //Invoke("StartGameAfterCountdown", 1f);
        }

        // private void StartGameAfterCountdown()
        // {
        //     _gameState = GameState.Start;
        // }

        [Button]
        public void FinishGame()
        {
            Debug.Log("FinishGame");
            _gameState = GameState.Finish;

            foreach (var gameListener in _gameListeners)
            {
                if (gameListener is IGameFinishListener gameFinishListener) gameFinishListener.OnFinishGame();
            }

            Time.timeScale = 0;
        }

        [Button]
        public void PauseGame()
        {
            Debug.Log("PauseGame");
            _gameState = GameState.Pause;

            foreach (var gameListener in _gameListeners)
            {
                if (gameListener is IGamePauseListener gamePauseListener) gamePauseListener.OnPauseGame();
            }

            Time.timeScale = 0;
        }

        [Button]
        public void ResumeGame()
        {
            Debug.Log("ResumeGame");
            _gameState = GameState.Resume;
            Time.timeScale = 1;

            foreach (var gameListener in _gameListeners)
            {
                if (gameListener is IGameResumeListener gameResumeListener) gameResumeListener.OnResumeGame();
            }
        }

        [Button]
        public void RestartGame()
        {
            Debug.Log("RestartGame");
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}