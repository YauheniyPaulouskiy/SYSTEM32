using System.Collections.Generic;
using UnityEngine;

namespace GameManager.PauseGame
{
    public class PauseGame : MonoBehaviour
    {
        public bool _isPaused { get; private set; }
        public bool _isPausedFromDeath { get; private set; }

        public static PauseGame instance;

        private List<IPausedHandler> _pauses = new List<IPausedHandler>();
        private List<IPausedFromDeathHandler> _pausesFromDeath = new List<IPausedFromDeathHandler>();

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

        private void OnDisable()
        {
            if (instance == this)
            {
                instance = null;
            }
        }
        #endregion

        public void SetPause(bool isPaused, bool isPausedFromDeath)
        {
            _isPaused = isPaused;
            _isPausedFromDeath = isPausedFromDeath;

            foreach (var pause in _pauses)
            {
                pause.IsPaused(isPaused);
            }
            foreach (var pause in _pausesFromDeath)
            {
                pause.IsPausedFromDeath(isPausedFromDeath);
            }
        }

        public void AddList(IPausedHandler pausedHandler)
        {
            _pauses.Add(pausedHandler);
        }

        public void AddList(IPausedFromDeathHandler pausedFromDeathHandler)
        {
            _pausesFromDeath.Add(pausedFromDeathHandler);
        }

        public void RemoveList(IPausedHandler pausedHandler)
        {
            _pauses.Remove(pausedHandler);
        }

        public void RemoveList(IPausedFromDeathHandler pausedFromDeathHandler)
        {
            _pausesFromDeath.Remove(pausedFromDeathHandler);
        }
    }
}