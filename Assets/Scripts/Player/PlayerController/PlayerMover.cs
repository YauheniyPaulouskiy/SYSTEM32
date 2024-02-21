using Player.Animation;
using System;
using UnityEngine;

namespace Player.Controller
{
    [RequireComponent(typeof(Rigidbody))]
    [RequireComponent (typeof(PlayerMoveAnimation))]
    public class PlayerMover : MonoBehaviour, IUseStamina
    {
        [Header("Speed Value")]
        [SerializeField] private float _initialFrontSpeed;
        [SerializeField] private float _maxFrontSpeed;
        [SerializeField] private float _playerFrontAcceleration;
        [SerializeField] private float _initialSideAndRearSpeed;

        [Header("Dash Value")]
        [SerializeField] private float _dashSpeed;
        [SerializeField] private float _decreasedStamina;

        [Header("Jump Value")]
        [SerializeField] private float _jumpForce;

        [Header("Ground Mask")]
        [SerializeField] private LayerMask _ground;

        private float _speed;

        private bool _onGround;

        private Rigidbody _rb;
        private PlayerInputs _playerInputs;
        private PlayerMoveAnimation _playerMoveAnimation;

        private static Action<float> _useStamina;

        #region [Initialization]
        private void Awake()
        {
            _rb = GetComponent<Rigidbody>();
            _playerInputs = new PlayerInputs();
            _playerMoveAnimation = GetComponent<PlayerMoveAnimation>();
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

        private void FixedUpdate()
        {
            ApplayGround();
            Dash();
        }

        private void ApplayGround()
        {
            if (GroundCheck())
            {
                Move();
                Jump();
            }
        }

        private void Move()
        {
            var inputsDirection = _playerInputs.Player.Move.ReadValue<Vector2>();

            if (inputsDirection == Vector2.zero)
            {
                _rb.velocity = new Vector3(0f, _rb.velocity.y, 0f);
                _speed = 0f;
                return;
            }

            _playerMoveAnimation.MoveAnimation(inputsDirection);

            ChangeSpeed(inputsDirection);

            var moveDirection = transform.right * inputsDirection.x + transform.forward * inputsDirection.y;
            var scaleDirection = moveDirection * _rb.mass * (_speed * Time.deltaTime);

            _rb.velocity = new Vector3(scaleDirection.x, _rb.velocity.y, scaleDirection.z);
        }

        private void Jump()
        {
            if (_playerInputs.Player.Jump.ReadValue<float>() > 0 && GroundCheck())
            {
                _rb.AddForce(Vector3.up * _jumpForce * _rb.mass, ForceMode.Impulse);
            }
        }

        private void Dash()
        {
            UseStamina();
        }

        private void UseStamina()
        {
            _useStamina.Invoke(_decreasedStamina);
        }

        private void ChangeSpeed(Vector2 inputsDirection)
        {
            if (inputsDirection.y > 0 && inputsDirection.x == 0)
            {
                _speed += _playerFrontAcceleration * Time.deltaTime;
                _speed = Mathf.Clamp(_speed, _initialFrontSpeed, _maxFrontSpeed);
            }
            else
            {
                _speed = _initialSideAndRearSpeed;
            }
        }

        private bool GroundCheck()
        {
            var curPosition = transform.position;
            curPosition.y += 0.1f;

            _onGround = Physics.Raycast(curPosition, -Vector3.up, 0.2f, _ground);

            return _onGround;
        }
    }
}