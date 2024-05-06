using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using BoxBoyGame.Core.Interfaces;

namespace BoxBoyGame.Gameplay.Controller 
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class PlayerController : MonoBehaviour, IPausable
    {
        [SerializeField] private float speed;

        private Animator animator;
        private Rigidbody2D rb;

        private bool inPause = false;
        private float previousHorDir = 0;

        private void Start()
        {
            animator = GetComponent<Animator>();
            rb = GetComponent<Rigidbody2D>();
        }

        public void MoveCharacter(float horizontalDir) 
        {
            if (!inPause) 
            {
                rb.velocity = new Vector2(horizontalDir * speed, rb.velocity.y);
                Flip(horizontalDir);
            }
        }

        private void Flip(float horizontalDir) 
        {
            if (horizontalDir != 0) 
            {
                if (horizontalDir != previousHorDir) 
                {
                    Vector3 localScale = transform.localScale;
                    localScale.x *= -1f;
                    transform.localScale = localScale;

                    previousHorDir = horizontalDir;
                }
            } 
        }

        public void Pause()
        {
            inPause = true;
        }

        public void Resume()
        {
            inPause = false;
        }
    }
}

