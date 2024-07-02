using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

namespace RhythmGame {
    public class ProgressBar : MonoBehaviour, IGameStartListener, IGamePauseListener, IGameResumeListener, IGameUpdateListener
    {
        [Header("UI Elements")]
        [SerializeField] private Image image;
        public LaneBtn[] buttons;

        [Header("Properties")]
        public int value;
        public int maxValue;
        private bool isCorrectlyConfigured = false;
        public bool isActive = false;
        public float isActiveAfterTime;
        public float activeTime;
        public GameObject healthBar;

        //Валидация конфигурации (тест)
        private void Awake()
        {
            if (image.type == Image.Type.Filled & image.fillMethod == Image.FillMethod.Vertical & maxValue > 0)
            {
                isCorrectlyConfigured = true;
            }
            else
            {
                Debug.Log("{GameLog} => {ProgressBar} - {<color=red>Error</color>} -> Components Parameters are configurred incorrectly \n" +
                        "Required type: Filled \n" +
                        "Required FillMethod: Vertical \n" +
                        "Required maxValue: > 0");
            }
        }

        private void Start() {
            if (!isCorrectlyConfigured) return;

            IGameListener.Register(this);
        }
        public void OnStartGame()
        {
            GetComponent<Transparency>().defaultTransparency = 0;

            foreach (var item in buttons) item.isActive = false;
        }
        public void OnUpdate(float deltaTime)
        {

                if (isActive)
                {
                    //планируется добавить полноценную логику с обратным отсчетом
                    isActiveAfterTime -= deltaTime;
                    GetComponent<Transparency>().changeSpeed = 0.5f;

                    if (isActiveAfterTime <= 0)
                    {
                        foreach (var item in buttons) item.isActive = true;

                        if (Input.GetKeyDown("x") || Input.GetKeyDown("c")) value++;

                        image.fillAmount = (float) value / maxValue;
                        activeTime -= deltaTime;

                        if (image.fillAmount == 1 || activeTime <= 0)
                        {
                            GetComponent<Transparency>().changeSpeed = -2.5f;
                            foreach (var item in buttons) item.isActive = false;
                        }

                        if (image.fillAmount != 1 & activeTime <= 0) healthBar.GetComponent<HealthBar>().setHealthCount(-5);
                    }
                }
        }
        public void OnPauseGame()
        {
            if (isActive & isActiveAfterTime != 0)
            {
                isActive = false;
                foreach (var item in buttons) item.isActive = false;
            }
        }
        public void OnResumeGame()
        {
            if (!isActive & isActiveAfterTime != 0)
            {
                isActive = true;
                foreach (var item in buttons) item.isActive = true;
            }
        }
        public void SetValue(int value) => this.value = value;
        public void SetMaxValue(int maxValue) => this.maxValue = maxValue;
    }
}