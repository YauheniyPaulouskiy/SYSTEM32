using Player.Controller;
using UnityEngine;
using Zenject;

namespace Player.Camera
{
    public class CameraBinding : MonoBehaviour
    {
        [Header("Binding Point")]
        [SerializeField] private Transform _cameraBindingPoint;

        [Inject]
        private void Container(PlayerMover player)
        {
            _cameraBindingPoint = player.transform.Find("CameraBindingPoint").GetComponent<Transform>();
        }

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