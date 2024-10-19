using UnityEngine;
using DG.Tweening;
using Sirenix.OdinInspector;

namespace cdvproject.Demo.Camera
{
    /// <summary>
    /// The <see cref="CircularMovement2D"/> class provides functionality to move a game object in a circular path
    /// within a 2D space using the DOTween library. This class allows for smooth movement transitions and 
    /// speed adjustments during the circular path animation.
    /// 
    /// <para>This file is intended for demonstration purposes, implementing cyclic movement of an object 
    /// to facilitate tracking and adjusting the camera in a Unity project.</para>
    /// </summary>
    public class CircularMovement2D : MonoBehaviour
    {
        [Title("Movement Settings")]

        /// <summary>
        /// The radius of the circular path the object will follow. 
        /// This determines how far from the center the object will orbit.
        /// </summary>
        [SerializeField, Range(1f, 20f), Tooltip("The radius of the circle around which the object moves.")]
        private float radius = 5f;

        /// <summary>
        /// The duration in seconds required to complete one full revolution around the circular path. 
        /// Adjusting this value affects the speed of the circular movement.
        /// </summary>
        [SerializeField, Range(1f, 60f), Tooltip("The time it takes to complete one full revolution.")]
        private float duration = 20f;

        /// <summary>
        /// The center point of the circular path. The object will move around this specified point in the 2D space.
        /// </summary>
        [SerializeField, Tooltip("The center of the circular path around which the object will move.")]
        private Vector2 center = Vector2.zero;

        [FoldoutGroup("Tween Control"), ReadOnly, Tooltip("Reference to the tween controlling the circular movement.")]
        private Tween circularTween; // Tween reference for controlling the circular motion.

        /// <summary>
        /// Initializes the circular movement by calculating the waypoints of the circle and creating a looping tween 
        /// using DOTween to move the object along those waypoints.
        /// </summary>
        private void Start()
        {
            // Array of waypoints forming the circular path (8 points in this example)
            Vector3[] waypoints = new Vector3[8];
            for (int i = 0; i < waypoints.Length; i++)
            {
                // Calculate the angle for each point on the circle
                float angle = i * Mathf.PI * 2f / waypoints.Length;
                waypoints[i] = new Vector3(Mathf.Cos(angle), Mathf.Sin(angle), 0) * radius + (Vector3)center;
            }

            // Initialize the tween to move the object in a circular path
            circularTween = transform.DOPath(waypoints, duration, PathType.CatmullRom)
                .SetOptions(true) // Close the loop
                .SetEase(Ease.Linear) // Linear movement to maintain consistent speed
                .SetLoops(-1, LoopType.Restart); // Infinite loops, restarting after each complete circle
        }

        /// <summary>
        /// Adjusts the speed of the circular movement by modifying the tween's time scale.
        /// This allows for dynamic speed changes while the object is moving along its circular path.
        /// </summary>
        /// <param name="speedMultiplier">Multiplier to increase or decrease the speed of the circular movement. 
        /// A value greater than 1 increases the speed, while a value less than 1 decreases it.</param>
        public void ChangeSpeed(float speedMultiplier)
        {
            if (circularTween != null)
            {
                circularTween.timeScale = speedMultiplier; // Adjust the speed based on the multiplier
            }
        }
    }
}