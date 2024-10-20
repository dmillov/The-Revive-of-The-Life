using UnityEngine;

namespace cdvproject.Player
{
    public class PlayerInputHandler : MonoBehaviour
    {
        public Vector2 CaptureMovementInput()
        {
            float horizontal = Input.GetAxisRaw("Horizontal");
            float vertical = Input.GetAxisRaw("Vertical");
            Vector2 movementInput = new Vector2(horizontal, vertical);

            // Якщо затиснуто Shift, множимо рух на коефіцієнт швидкості
            if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
            {
                movementInput *= 10; // Збільште коефіцієнт, якщо потрібно
            }

            return movementInput;
        }

        public bool IsHarvestingPressed()
        {
            return Input.GetButtonDown("Fire1"); // Змінити на вашу кнопку збору врожаю
        }
    }
}