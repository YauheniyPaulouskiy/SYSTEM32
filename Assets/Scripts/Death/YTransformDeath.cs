using System;
using UnityEngine;

namespace Death
{
    public class YTransformDeath : MonoBehaviour, IDeath
    {
        public event Action Death;

        private void Update()
        {
            YTransform();
        }

        private void YTransform()
        {
            if (transform.position.y == -10f)
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