using UnityEngine;

namespace Player.Camera
{
    [RequireComponent(typeof(UnityEngine.Camera))]
    public class CameraBinding : MonoBehaviour
    {
        [Header("Binding Point")]
        [SerializeField] private Transform _cameraBindingPoint;

        private void Update()
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