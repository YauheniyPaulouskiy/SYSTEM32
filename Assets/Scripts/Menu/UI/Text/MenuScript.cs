using UnityEngine;
using UnityEngine.SceneManagement;

namespace UI.Menu.Manager
{
    public class MenuScript : MonoBehaviour
    {
        public void StartGame()
        {
            SceneManager.LoadScene(1);
        }

        public void ExitGame()
        {
            Application.Quit();
        }
    }
}