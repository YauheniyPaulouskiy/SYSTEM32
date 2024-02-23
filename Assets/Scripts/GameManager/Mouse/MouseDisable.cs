using UnityEngine;

namespace GameManager.Mouse
{
    public class MouseDisable : MonoBehaviour
    {
        public void Enable()
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }

        public void Disable()
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }
}