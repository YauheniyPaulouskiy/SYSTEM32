using GameManager.PauseGame;
using Player.Controller;
using Player.Death;
using UnityEngine;

namespace Player.Camera
{
    public class CameraRotation : MonoBehaviour
    {
        [Header("Sensetivity Value")]
        [SerializeField] private float _mouseSensetivity;

        [Header("Rotation Angle Value")]
        [SerializeField] private Vector2 _minMaxHorizontalAngle;

        [Header("Player Rotation Object")]
        [SerializeField] private PlayerRotation _playerRotation;

        private Vector3 _scaleRotation;
        private float _xRotation;

        private PlayerInputs _playerInputs;

        #region [Initialization]
        private void Awake()
        {
            _playerInputs = new PlayerInputs();
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

        private void Update()
        {
            if (PauseGame.instance._isPaused)
            {
                return;
            }

            Rotation();
        }

        private void Rotation()
        {
            
            CalcRotation();

            transform.localRotation = Quaternion.Euler(_xRotation, 0f, 0f);

            _playerRotation.Rotation(_scaleRotation.x);
        }

        private void CalcRotation()
        {
            var inputsRotation = _playerInputs.Player.Rotation.ReadValue<Vector2>();
            _scaleRotation = inputsRotation * _mouseSensetivity * Time.deltaTime;

            _xRotation -= _scaleRotation.y;
            _xRotation = Mathf.Clamp(_xRotation, _minMaxHorizontalAngle.x, _minMaxHorizontalAngle.y);
        }
    }
}