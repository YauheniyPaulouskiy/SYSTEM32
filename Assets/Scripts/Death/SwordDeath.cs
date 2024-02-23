using System;
using UnityEngine;

namespace Death
{
    public class SwordDeath : MonoBehaviour, IDeath
    {
        [SerializeField] private string _findTage;

        public event Action Death;

        private void OnTriggerEnter(Collider other)
        {
            if (other.transform.CompareTag(_findTage))
            {
                OnDeath();
            }
        }

        public void OnDeath()
        {
            Death?.Invoke();
        }
    }
}