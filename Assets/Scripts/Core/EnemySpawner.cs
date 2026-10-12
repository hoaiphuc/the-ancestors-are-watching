using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

namespace AncestorsWatching
{
    /// <summary>
    /// Spawns enemies just outside a random screen edge, faster the longer the run lasts.
    /// Enemies come from a pool because a busy run creates and removes hundreds of them.
    /// </summary>
    public class EnemySpawner : MonoBehaviour
    {
        [SerializeField] private Enemy enemyPrefab;
        [SerializeField] private EnemyData enemyData;
        [SerializeField] private Kin kin;
        [SerializeField] private Camera gameCamera;

        [Header("Spawn rate (seconds between spawns)")]
        [SerializeField] private float startInterval = 2f;
        [SerializeField] private float minInterval = 0.4f;
        [SerializeField] private float rampSeconds = 120f;

        [Header("Placement")]
        [SerializeField] private float edgeMargin = 1f;

        private ObjectPool<Enemy> pool;
        private readonly List<Enemy> active = new();
        private bool spawning;
        private float runTime;
        private float timer;

        private void Awake()
        {
            pool = new ObjectPool<Enemy>(
                createFunc: CreateEnemy,
                actionOnGet: enemy => enemy.gameObject.SetActive(true),
                actionOnRelease: enemy => enemy.gameObject.SetActive(false),
                actionOnDestroy: DestroyEnemy,
                defaultCapacity: 32,
                maxSize: 200);
        }

        private void OnEnable() => kin.Died += StopRun;

        private void OnDisable() => kin.Died -= StopRun;

        // Temporary: step 5 will start runs from the upgrade screen instead.
        private void Start() => StartRun();

        private void Update()
        {
            if (!spawning) return;

            runTime += Time.deltaTime;
            timer -= Time.deltaTime;
            if (timer > 0f) return;

            Spawn();
            // Reset rather than add, so a frame hitch doesn't release a burst of enemies.
            timer = CurrentInterval();
        }

        public void StartRun()
        {
            runTime = 0f;
            timer = startInterval;
            spawning = true;
        }

        public void StopRun()
        {
            spawning = false;

            foreach (var enemy in active) pool.Release(enemy);
            active.Clear();
        }

        private float CurrentInterval() => Mathf.Lerp(startInterval, minInterval, runTime / rampSeconds);

        private void Spawn()
        {
            var enemy = pool.Get();
            enemy.Initialize(enemyData, kin, RandomEdgePosition());
            active.Add(enemy);
        }

        private Vector3 RandomEdgePosition()
        {
            // Derived from the camera so enemies appear just off-screen at any resolution.
            float halfHeight = gameCamera.orthographicSize + edgeMargin;
            float halfWidth = gameCamera.orthographicSize * gameCamera.aspect + edgeMargin;
            var centre = gameCamera.transform.position;

            float x = Random.Range(-halfWidth, halfWidth);
            float y = Random.Range(-halfHeight, halfHeight);

            return Random.Range(0, 4) switch
            {
                0 => new Vector3(centre.x - halfWidth, centre.y + y, 0f), // left
                1 => new Vector3(centre.x + halfWidth, centre.y + y, 0f), // right
                2 => new Vector3(centre.x + x, centre.y - halfHeight, 0f), // bottom
                _ => new Vector3(centre.x + x, centre.y + halfHeight, 0f), // top
            };
        }

        private Enemy CreateEnemy()
        {
            var enemy = Instantiate(enemyPrefab, transform);
            // Subscribed once per instance, since a pooled enemy lives through many lives.
            enemy.Finished += OnEnemyFinished;
            return enemy;
        }

        private void DestroyEnemy(Enemy enemy)
        {
            enemy.Finished -= OnEnemyFinished;
            Destroy(enemy.gameObject);
        }

        private void OnEnemyFinished(Enemy enemy)
        {
            active.Remove(enemy);
            pool.Release(enemy);
        }
    }
}
