using System;

namespace Enemy.Death
{
    public interface IEnemyDeath
    {
        public event Action EnemyDeath;

        public void OnDeath();
    }
}