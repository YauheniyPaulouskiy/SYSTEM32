using ObjectPool;
using Enemy.Death;
using System.Collections;
using UnityEngine;

namespace Enemy.Spawnpoint
{
    public class EnemySpawnManager : MonoBehaviour
    {
        [Header("Spawn Time Value")]
        [SerializeField] private float _timeSpawnl;

        [Header("Count Spawn Value")]
        [SerializeField] private int _countSpawn;

        [Header("Enemy Prefab")]
        [SerializeField] private EnemyDeath _enemyPrefab;

        private EnemySpawnpoint[] _enemySpawnpoint;

        private ObjectPool<EnemyDeath> _enemyPool;

        #region [Initialization]
        private void Awake()
        {
            _enemyPool = new ObjectPool<EnemyDeath>(Preload, GetAction, ReturnAction, _countSpawn);

            _enemySpawnpoint = FindObjectsOfType<EnemySpawnpoint>();
        }
        #endregion

        private void Start()
        {
            StartCoroutine(SpawnTimer());
        }

        private void SpawnEnemy()
        {
            var a = Random.Range(1, _enemySpawnpoint.Length);
            var enemyObject = _enemyPool.Get();
            enemyObject.DeathEnemy.AddListener(DispawnEnemy);

            enemyObject.transform.position = _enemySpawnpoint[a].transform.position;
            enemyObject.transform.rotation = _enemySpawnpoint[a].transform.rotation;
        }

        private void DispawnEnemy(EnemyDeath enemyDeath)
        {
            enemyDeath.DeathEnemy.RemoveListener(DispawnEnemy);
            _enemyPool.Return(enemyDeath);
        }

        public EnemyDeath Preload()
        {
            return Instantiate(_enemyPrefab, transform.position, transform.rotation);
        }

        public void GetAction (EnemyDeath enemyMove)
        {
            enemyMove.gameObject.SetActive(true);
        }

        public void ReturnAction(EnemyDeath enemyMove)
        {
            enemyMove.gameObject.SetActive(false);
        }

        private IEnumerator SpawnTimer()
        {
            while (true)
            {
                yield return new WaitForSeconds(_timeSpawnl);

                SpawnEnemy();
            }
        }
    }
}