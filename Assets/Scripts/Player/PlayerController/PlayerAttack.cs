using Player.Animation;
using UnityEngine;

[RequireComponent(typeof(PlayerAttackAnimation))]
public class PlayerAttack : MonoBehaviour
{
    [Header("Sword Trigger")]
    [SerializeField] private BoxCollider _swordTriggerZone;

    private bool _isAttack = false;

    private PlayerInputs _palayerInputs;
    private PlayerAttackAnimation _playerAttackAnimation;

    #region [Initialization]
    private void Awake()
    {
        _palayerInputs = new PlayerInputs();
        _playerAttackAnimation = GetComponent<PlayerAttackAnimation>();

        _palayerInputs.Player.Attack.performed += context => Attack();
    }

    private void OnEnable()
    {
        _palayerInputs.Enable();
    }

    private void OnDisable()
    {
        _palayerInputs.Disable();
    }
    #endregion

    private void Start()
    {
        EnableTriggerZone(_isAttack);
    }

//Bug
    private void Attack()
    {
        if (!_isAttack) 
        {
            Debug.Log("Attack");
            _isAttack = true;
            EnableTriggerZone(_isAttack);
            _swordTriggerZone.enabled = true;
            _playerAttackAnimation.AttackAnimation();
            return;
        }
        _isAttack = false;
        EnableTriggerZone(_isAttack);
    }

    private void EnableTriggerZone(bool isAttack)
    {
        _swordTriggerZone.enabled = isAttack;
    }
}
