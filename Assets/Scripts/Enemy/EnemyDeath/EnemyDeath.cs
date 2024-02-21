using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDeath : MonoBehaviour
{
    private IEnemyDeath[] _enemyDeathConditions;

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
        Debug.Log("Enemy Death");
    }
}
