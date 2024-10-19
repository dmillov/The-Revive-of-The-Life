using UnityEngine;

namespace cvdproject.Player.Movement
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private PlayerInputHandler inputHandler;
        [SerializeField] private PlayerMover playerMover;

        private void Update()
        {
            Vector2 movementInput = inputHandler.CaptureMovementInput();
            playerMover.Move(movementInput);
        }
    }
}