using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

namespace RhythmGame {
    public class ScoreManager : MonoBehaviour, IGameUpdateListener, IGameFinishListener
    {
        static int comboScore;

        public static ScoreManager Instance;
        public AudioSource missSFX;
        public TMPro.TextMeshPro scoreText;
        public GameObject healthBar;

        private void Start() {
            IGameListener.Register(this);
            Instance = this;
            comboScore = 0;
        }
        public void Hit() => comboScore += 1;
        public void Miss()
        {
            comboScore = 0;
            healthBar.GetComponent<HealthBar>().setHealthCount(-1);
            Instance.missSFX.Play();
        }
        public void OnUpdate(float deltaTime)
        {
            if (comboScore > 3)
            {
                scoreText.text = "Combo: " + comboScore.ToString();
            }
            else
            {
                scoreText.text = "";
            }
        }
        public void OnFinishGame() => scoreText.text = "";
    }
}
