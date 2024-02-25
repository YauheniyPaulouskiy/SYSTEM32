using Zenject;
using ObjectPool;
using Enemy.AI;
using System.Collections;
using UnityEngine;
using GameManager.PauseGame;

namespace Enemy.Spawnpoint
{
    public class EnemySpawnManager : MonoBehaviour
    {
        [Header("Spawn Time Value")]
        [SerializeField] private float _timeSpawnl;

        [Header("Count Spawn Value")]
        [SerializeField] private int _countSpawn;

        [Header("Enemy Prefab")]
        [SerializeField] private EnemyAI _enemyPrefab;

        private EnemySpawnpoint[] _enemySpawnpoint;

        private ObjectPool<EnemyAI> _enemyPool;

        private Transform _transform;

        [Inject]
        private DiContainer _container;

        #region [Initialization]
        private void Awake()
        {
            _enemyPool = new ObjectPool<EnemyAI>(Preload, GetAction, ReturnAction, _countSpawn);

            _enemySpawnpoint = FindObjectsOfType<EnemySpawnpoint>();
        }
        #endregion

        private void Start()
        {
            StartCoroutine(SpawnTimer());
        }

        private void SpawnEnemy()
        {
            if (PauseGame.instance._isPaused)
            {
                return;
            }

            var a = Random.Range(1, _enemySpawnpoint.Length);
            var enemyObject = _enemyPool.Get();
            enemyObject.StandUp();
            enemyObject.GetIsDeath(false);
            enemyObject.DeathEnemy.AddListener(DispawnEnemy);

            enemyObject.transform.position = _enemySpawnpoint[a].transform.position;
            enemyObject.transform.rotation = _enemySpawnpoint[a].transform.rotation;
        }

        private void DispawnEnemy(EnemyAI enemyDeath)
        {
            enemyDeath.DeathEnemy.RemoveListener(DispawnEnemy);
            _enemyPool.Return(enemyDeath);
        }

        public EnemyAI Preload()
        {
            return _container.InstantiatePrefabForComponent<EnemyAI>(_enemyPrefab, _transform);
        }

        public void GetAction (EnemyAI enemyMove)
        {
            enemyMove.gameObject.SetActive(true);
        }

        public void ReturnAction(EnemyAI enemyMove)
        {
            enemyMove.gameObject.SetActive(false);
        }

        #region [Timer]
        private IEnumerator SpawnTimer()
        {
            while (true)
            {
                yield return new WaitForSeconds(_timeSpawnl);

                SpawnEnemy();
            }
        }
        #endregion
    }
}