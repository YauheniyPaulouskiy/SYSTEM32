using System;
using UnityEngine;

namespace Enemy.Death
{
    public class WeaponEnemyDeath : MonoBehaviour, IEnemyDeath
    {
        public event Action EnemyDeath;

        private void OnTriggerEnter(Collider other)
        {
            if (other.transform.CompareTag("Weapon"))
            {
                OnDeath();
            }
        }

        public void OnDeath()
        {
            EnemyDeath?.Invoke();
        }
    }
}