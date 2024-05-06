using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using BoxBoyGame.Core.Interfaces;
using BoxBoyGame.Core.ObjectPool;
using BoxBoyGame.Gameplay.Box;

namespace BoxBoyGame.Gameplay.Controller
{
    public class BoxSpawnerController : MonoBehaviour, IPausable
    {
        [SerializeField] private Transform[] spawnPoints;
        [SerializeField] private BoxObject boxPrefab;

        [Header("Spawner Box")]
        [SerializeField] private float maxSpawnerSeconds = 3f;
        [SerializeField] private int maxSpawnObjects = 5;

        private float currentSpawnSeconds = 0;
        private GameObjectPool<BoxObject> boxObjectPool;

        private List<BoxObject> boxObjectsInScene;

        private int previousPointIndex = 0;
        private bool inPause = false;

        #region Initialize Methods
        private void Start()
        {
            boxObjectsInScene = new List<BoxObject>();
            boxObjectPool = new GameObjectPool<BoxObject>(BoxFactoryMethod, TurnOnBoxObject, TurnOffBoxObject, maxSpawnObjects, false);
        }

        private void Update()
        {
            if (!inPause) 
            { 
                SpawnNextBox();
            }
        }

        #endregion

        #region IPausable Methods
        public void Pause()
        {
            inPause = true;

            foreach (var boxObject in boxObjectsInScene)
            {
                boxObject.Pause();
            }
        }

        public void Resume()
        {
            inPause = false;

            foreach (var boxObject in boxObjectsInScene)
            {
                boxObject.Resume();
            }
        }
        #endregion

        #region Private Methods
        private BoxObject BoxFactoryMethod() 
        {
            Transform randomSpawnPoint = GetRandomSpawnPoint();

            return Instantiate(boxPrefab, randomSpawnPoint);
        }

        private void TurnOnBoxObject(BoxObject box) 
        {
            Transform randomPoint = GetRandomSpawnPoint();

            box.gameObject.transform.parent = randomPoint;
            box.gameObject.transform.position = randomPoint.position;

            box.gameObject.SetActive(true);

            box.AddEventOnBoxInCollision(BoxCollision);

            if (!boxObjectsInScene.Contains(box))
            {
                boxObjectsInScene.Add(box);
            }
        }

        private void TurnOffBoxObject(BoxObject box) 
        {
            box.gameObject.SetActive(false);

            box.RemoveEventOnBoxInCollision(BoxCollision);

            if (boxObjectsInScene.Contains(box)) 
            {
                boxObjectsInScene.Remove(box);
            }
        }

        private Transform GetRandomSpawnPoint() 
        {
            int randomIndex = 0;

            while (previousPointIndex == randomIndex) 
            {
                randomIndex = Random.Range(0, spawnPoints.Length);
            }

            previousPointIndex = randomIndex;

            return spawnPoints[randomIndex];
        }

        private void SpawnNextBox() 
        {
            if (currentSpawnSeconds > maxSpawnerSeconds)
            {
                boxObjectPool.TurnOnObject();
                currentSpawnSeconds = 0;
            }
            else
            {
                currentSpawnSeconds += Time.deltaTime;
            }
        }

        private void BoxCollision(BoxObject box) 
        {
            boxObjectPool.TurnOffObject(box);
        }
        #endregion
    }
}

