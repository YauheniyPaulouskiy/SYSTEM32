using System.Collections.Generic;
using UnityEngine;

namespace Enemy.Animation
{
    public class EnemyRagdoll : MonoBehaviour
    {
        private List<Rigidbody> rigidbodies;

        private void Awake()
        {
            rigidbodies = new List<Rigidbody>(GetComponentsInChildren<Rigidbody>());
            Disable();
        }

        public void Enable()
        {
            foreach (var rigidbody in rigidbodies)
            {
                rigidbody.isKinematic = false;
            }
        }

        public void Disable()
        {
            foreach (var rigidbody in rigidbodies)
            {
                rigidbody.isKinematic = true;
            }
        }
    }
}