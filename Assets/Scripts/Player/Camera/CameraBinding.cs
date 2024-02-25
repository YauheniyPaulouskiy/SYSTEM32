using Player.Controller;
using Zenject;
using UnityEngine;

namespace Player.Camera
{
    public class CameraBinding : MonoBehaviour
    {
        [Header("Binding Point")]
        [SerializeField] private Transform _cameraBindingPoint;

        #region [Initialization]
        [Inject]
        private void Container(PlayerMover player)
        {
            _cameraBindingPoint = player.transform.Find("CameraBindingPoint").GetComponent<Transform>();
        }
        #endregion

        private void LateUpdate()
        {
            Binding();
        }

        private void Binding()
        {
            transform.position = _cameraBindingPoint.position;
            transform.localRotation = _cameraBindingPoint.rotation;
        }
    }
}