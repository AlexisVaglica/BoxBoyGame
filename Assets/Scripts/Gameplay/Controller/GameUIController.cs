using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace BoxBoyGame.Gameplay.Controller
{
    public class GameUIController : MonoBehaviour
    {
        [Header("Movement Buttons")]
        [SerializeField] private Button leftButton;
        [SerializeField] private Button rightButton;

        private Action<float> OnLeftMovePressed;
        private Action<float> OnRightMovePressed;

        private void Start()
        {
            leftButton.onClick.AddListener(LeftMoveButtonClicked);
            rightButton.onClick.AddListener(RightMoveButtonClicked);
        }

        private void LeftMoveButtonClicked() 
        {
            OnLeftMovePressed?.Invoke(-1);
        }

        private void RightMoveButtonClicked()
        {
            OnRightMovePressed?.Invoke(1);
        }

        public void AddEventLeftMoveAction(Action<float> newEvent) 
        {
            OnLeftMovePressed += newEvent;
        }

        public void RemoveEventLeftMoveAction(Action<float> eventToRemove)
        {
            OnLeftMovePressed -= eventToRemove;
        }

        public void AddEventRightMoveAction(Action<float> newEvent) 
        {
            OnRightMovePressed += newEvent;
        }

        public void RemoveEventRightMoveAction(Action<float> eventToRemove) 
        {
            OnRightMovePressed -= eventToRemove;
        }
    }
}