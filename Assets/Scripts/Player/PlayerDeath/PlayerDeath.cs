using Death;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDeath : MonoBehaviour
{
    [SerializeField] private int _hp;
    [SerializeField] private int _maxHp;
    [SerializeField] private float _reloadHealthTimer;

    private bool _isStartRutine;
 
    private IDeath[] _playerDeathConditions;

    private void Awake()
    {
        _playerDeathConditions = GetComponents<IDeath>();
    }

    private void Start()
    {
        SubscribeToDeath();
    }

    private void SubscribeToDeath()
    {
        foreach (var playerDeath  in _playerDeathConditions)
        {
            playerDeath.Death += Health;
        }
    }

    private void Health()
    {
        Debug.Log("Damaged");
        _hp -= 1;
        if (_hp < _maxHp && !_isStartRutine)
        {
            IReloadHealth();
        }

        if (_hp <= 0)
        {
            Death();
        }
    }

    private void Death()
    {
        Debug.Log("PlayerDeath");
    }

    private void ApplyRutine()
    {
        _isStartRutine = !_isStartRutine;
    }

    private IEnumerator IReloadHealth()
    {
        ApplyRutine();
        yield return new WaitForSeconds(_reloadHealthTimer);
        _hp += 1;
        ApplyRutine();
    }
}
