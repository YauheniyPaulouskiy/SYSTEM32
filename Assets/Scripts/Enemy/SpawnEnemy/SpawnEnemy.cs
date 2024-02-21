using ObjectPool;
using UnityEngine;

public class SpawnEnemy : MonoBehaviour
{
    private ObjectPool<EnemyMove> _enemyPool;
    [SerializeField] private EnemyMove _enemy;
    [SerializeField] private int _count;

    private void Awake()
    {
        _enemyPool = new ObjectPool<EnemyMove>(Preload, GetAction, ReturnAction, _count);
    }

    private void Start()
    {
        var _enemySpawn = _enemyPool.Get();
        DispawnEnemy(_enemySpawn);
    }

    private void DispawnEnemy(EnemyMove _enemySpawn)
    {
        _enemyPool.Return(_enemySpawn);
    }

    public EnemyMove Preload()
    {
        return Instantiate(_enemy, transform.position, Quaternion.identity);
    }

    public void GetAction(EnemyMove _enemyMove)
    {
        _enemyMove.gameObject.SetActive(true);
    }

    public void ReturnAction(EnemyMove _enemyMove)
    {
        _enemyMove.gameObject.SetActive(false);
    }
}
