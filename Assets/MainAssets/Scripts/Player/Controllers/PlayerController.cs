using cdvproject.Camera;
using UnityEngine;
using Sirenix.OdinInspector;

namespace cdvproject.Player
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private PlayerInputHandler inputHandler;
        [SerializeField] private PlayerMover playerMover;
        [SerializeField] private PlayerStateController playerStateController;
        [SerializeField] private CameraController cameraController;

        private PlayerState currentState;

        // Ця властивість повертає текстовий стан гравця
        public string CurrentStateText
        {
            get
            {
                switch (currentState)
                {
                    case PlayerState.Running:
                        return "running";
                    case PlayerState.Walking:
                        return "walking";
                    case PlayerState.Idle:
                        return "idle";
                    default:
                        return "Unknown";
                }
            }
        }

        private void FixedUpdate()
        {
            Vector2 movementInput = inputHandler.CaptureMovementInput();

            // Нормалізуємо вектор руху, щоб його довжина не перевищувала 1
            Vector2 normalizedInput = movementInput.magnitude > 1f ? movementInput.normalized : movementInput;

            bool isRunning = movementInput.magnitude > 5f;

            // Оновлюємо стан гравця
            if (isRunning)
            {
                currentState = PlayerState.Running;
                playerStateController.ChangeState(PlayerState.Running);
                playerMover.SetRunning(true);
                cameraController.SetCameraZoom(true); // Зменшуємо камеру при бігу
            }
            else if (normalizedInput.magnitude > 0f)
            {
                currentState = PlayerState.Walking;
                playerStateController.ChangeState(PlayerState.Walking);
                playerMover.SetRunning(false);
                cameraController.SetCameraZoom(false); // Відновлюємо камеру при ходьбі
            }
            else
            {
                currentState = PlayerState.Idle;
                playerStateController.ChangeState(PlayerState.Idle);
                playerMover.SetRunning(false);
                cameraController.SetCameraZoom(false); // Відновлюємо камеру при бездіяльності
            }

            playerMover.Move(normalizedInput); // Рухаємо з нормалізованим введенням
        }
    }
}