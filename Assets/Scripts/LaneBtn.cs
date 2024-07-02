using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RhythmGame {
    public class LaneBtn : MonoBehaviour, IGameUpdateListener
    {
        private Color colorDefault = new Color(100/255f, 100/255f, 100/255f, 1);
        private Color colorOnPress = new Color(60/255f, 60/255f, 60/255f, 1);

        public static LaneBtn Instance;
        public KeyCode input;
        public Color outlineColor;
        public bool isActive;
        private void Start() {
            IGameListener.Register(this);
            GetComponent<SpriteRenderer>().color = colorDefault;
            GetComponent<SpriteRenderer>().material.SetColor("_SolidOutline", outlineColor);
        }

        public void OnUpdate(float deltaTime)
        {
            if (isActive)
            {
                if (Input.GetKeyDown(input))
                {
                    GetComponent<SpriteRenderer>().color = colorOnPress;
                    GetComponent<SpriteRenderer>().material.SetFloat("_OutlineEnabled", 1);
                }

                if (Input.GetKeyUp(input))
                {
                    GetComponent<SpriteRenderer>().color = colorDefault;
                    GetComponent<SpriteRenderer>().material.SetFloat("_OutlineEnabled", 0);
                }
            }
        }
    }
}