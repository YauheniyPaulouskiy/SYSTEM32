using UnityEngine;
using UnityEngine.Events;

namespace Enemy.Death
{
    public class EnemyDeath : MonoBehaviour
    {
        private IEnemyDeath[] _enemyDeathConditions;

        public UnityEvent<EnemyDeath> DeathEnemy;

        #region [Initialization]
        private void Awake()
        {
            _enemyDeathConditions = GetComponents<IEnemyDeath>();
        }
        #endregion

        private void Start()
        {
            SubscribeToIDeath();
        }

        private void SubscribeToIDeath()
        {
            foreach (var enemyDeath in _enemyDeathConditions)
            {
                enemyDeath.EnemyDeath += Death;
            }
        }

        private void Death()
        {
            DeathEnemy.Invoke(this);
        }
    }
}