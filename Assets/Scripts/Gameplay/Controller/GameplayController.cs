using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BoxBoyGame.Gameplay.Controller 
{
    public class GameplayController : MonoBehaviour
    {
        [Header("Controllers")]
        [SerializeField] private BoxSpawnerController boxSpawnerController;
        [SerializeField] private GameUIController UIController;

        [Header("Player Controller")]
        [SerializeField] private PlayerController playerControllerPrefab;
        [SerializeField] private Transform playerSpawnPoint;

        private PlayerController playerController;

        #region Private Methods
        private void Awake()
        {
            Vector2 startPos = new Vector2(playerSpawnPoint.position.x, playerSpawnPoint.position.y);
            playerController = Instantiate(playerControllerPrefab, startPos, Quaternion.identity);
        }

        private void Start()
        {
            UIController.AddEventLeftMoveAction(playerController.MoveCharacter);
            UIController.AddEventRightMoveAction(playerController.MoveCharacter);
        }

        private void PauseGame() 
        {
            boxSpawnerController.Pause();
            playerController.Pause();
        }
        #endregion

        #region Public Methods

        #endregion
    }

}
