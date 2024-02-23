using System;

namespace Death
{
    public interface IDeath
    {
        public event Action Death;

        public void OnDeath();
    }
}