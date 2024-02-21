using UnityEngine;

namespace Player.Camera
{
    [RequireComponent(typeof(UnityEngine.Camera))]
    public class CameraBinding : MonoBehaviour
    {
        [Header("Binding Point")]
        [SerializeField] private Transform _cameraBindingPoint;

        private void LateUpdate()
        {
            Binding();
        }

        private void Binding()
        {
            transform.transform.position = _cameraBindingPoint.position;
            transform.localRotation = _cameraBindingPoint.rotation;
        }
    }
}