using Player.Animation;
using UnityEngine;

[RequireComponent(typeof(PlayerAttackAnimation))]
public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private BoxCollider _swordTriggerZone;

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
        _swordTriggerZone.enabled = false;
    }

    private void Attack()
    {
        _swordTriggerZone.enabled = true;
        _playerAttackAnimation.AttackAnimation();
    }
}
