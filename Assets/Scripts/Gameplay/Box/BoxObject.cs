using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using BoxBoyGame.Core.Interfaces;

namespace BoxBoyGame.Gameplay.Box 
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class BoxObject : MonoBehaviour, IPausable
    {
        private Action<BoxObject> OnBoxInCollision;

        private Rigidbody2D rb;
        private bool inPause;

        #region IPausable Methods

        public void Pause()
        {
            inPause = true;
        }

        public void Resume()
        {
            inPause = false;
        }
        #endregion

        #region Private Methods

        private void Start()
        {
            rb = GetComponent<Rigidbody2D>();
        }

        private void FixedUpdate()
        {
            if (!inPause)
            {
                rb.AddForce(Vector2.down);
            }
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            OnBoxInCollision?.Invoke(this);
        }
        #endregion

        #region Public Methods
        public void AddEventOnBoxInCollision(Action<BoxObject> newEvent) 
        {
            OnBoxInCollision += newEvent;
        }

        public void RemoveEventOnBoxInCollision(Action<BoxObject> eventToRemove) 
        {
            OnBoxInCollision -= eventToRemove;
        }
        #endregion
    }
}

