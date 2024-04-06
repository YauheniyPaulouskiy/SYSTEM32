using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace GameManager.PauseGame
{
    public class PauseGame : MonoBehaviour
    {
        public bool _isPaused { get; private set; }

        public static PauseGame instance;

        private List<IPausedHandler> _pauses = new List<IPausedHandler>();

        #region [Initialization]
        private void Awake()
        {
            if (instance != null && instance != this)
            {
                Destroy(this);
            }
            else
            {
                instance = this;
            }
        }
        #endregion

        public void SetPause(bool isPaused)
        {
            _isPaused = isPaused;

            foreach (var pause in _pauses)
            {
                pause.IsPaused(isPaused);
            }
        }

        public void AddList(IPausedHandler pausedHandler)
        {
            _pauses.Add(pausedHandler);
        }

        public void RemoveList(IPausedHandler pausedHandler)
        {
            _pauses.Remove(pausedHandler);
        }
    }
}