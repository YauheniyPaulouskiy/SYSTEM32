using System.Collections.Generic;
using UnityEngine;

namespace Enemy.Animation
{
    public class EnemyRagdoll : MonoBehaviour
    {
        private List<Rigidbody> rigidbodies;

        #region [Initialization]
        private void Awake()
        {
            rigidbodies = new List<Rigidbody>(GetComponentsInChildren<Rigidbody>());
        }
        #endregion

        private void Start()
        {
            Disable();
        }

        public void IsActivate(bool isActivate)
        {
            if (isActivate)
            {
                Enable();
            }
            else
            {
                Disable();
            }
        }

        private void Enable()
        {
            foreach (var rigidbody in rigidbodies)
            {
                rigidbody.isKinematic = false;
            }
        }

        private void Disable()
        {
            foreach (var rigidbody in rigidbodies)
            {
                rigidbody.isKinematic = true;
            }
        }
    }
}