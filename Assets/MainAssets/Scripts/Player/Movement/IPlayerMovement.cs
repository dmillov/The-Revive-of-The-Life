using UnityEngine;

namespace cdvproject.Player
{
    public interface IPlayerMovement
    {
        void Move(Vector2 input);
        void SetRunning(bool isRunning);
    }
}