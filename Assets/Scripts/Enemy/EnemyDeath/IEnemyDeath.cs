using System;
using UnityEngine;

public interface IEnemyDeath
{
    public event Action EnemyDeath;

    public void OnDeath();
}