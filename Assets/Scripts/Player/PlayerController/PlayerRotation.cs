using UnityEngine;

namespace Player.Controller
{
    public class PlayerRotation : MonoBehaviour
    {
        public void Rotation(float scaleRotationX)
        {
            transform.Rotate(Vector3.up * scaleRotationX);
        }
    }
}