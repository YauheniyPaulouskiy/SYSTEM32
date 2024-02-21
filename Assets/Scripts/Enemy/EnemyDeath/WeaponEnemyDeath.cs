using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
