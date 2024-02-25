using GameManager.PauseGame;
using Player.Animation;
using UnityEngine;

namespace Player.Controller
{
    [RequireComponent(typeof(PlayerAnimation))]
    public class PlayerAttack : MonoBehaviour
    {
        [Header("Sword Trigger")]
        [SerializeField] private BoxCollider _swordTriggerZone;

        private PlayerInputs _playerInputs;

        private PlayerAnimation _playerAnimation;

        #region [Initialization]
        private void Awake()
        {
            _playerInputs = new PlayerInputs();
            _playerAnimation = GetComponent<PlayerAnimation>();

            _playerInputs.Player.Attack.performed += context => StartAttack();
        }

        private void OnEnable()
        {
            _playerInputs.Enable();
        }

        private void OnDisable()
        {
            _playerInputs.Disable();
        }
        #endregion

        private void Start()
        {
            ActiveTriggerZone(false);
        }

        private void StartAttack()
        {
            if (PauseGame.instance._isPaused)
            {
                return;
            }

            ActiveTriggerZone(true);

            _playerAnimation.AttackAnimation();
        }

        //Attack_1 and Attack_2 Animation Event 
        private void EndAttack()
        {
            ActiveTriggerZone(false);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Enemy"))
            {
                ActiveTriggerZone(false);
            }
        }

        private void ActiveTriggerZone(bool isAttack)
        {
            _swordTriggerZone.enabled = isAttack;
        }
    }
}