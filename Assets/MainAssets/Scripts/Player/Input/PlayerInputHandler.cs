using UnityEngine;

namespace cvdproject.Player.Movement
{
    public class PlayerInputHandler : MonoBehaviour
    {
        public Vector2 CaptureMovementInput()
        {
            float horizontal = Input.GetAxisRaw("Horizontal");
            float vertical = Input.GetAxisRaw("Vertical");
            return new Vector2(horizontal, vertical);
        }
    }
}