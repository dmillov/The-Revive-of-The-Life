using System;
using System.Collections.Generic;
using UnityEngine;

namespace cdvproject.Player
{
    public class PlayerStateController : MonoBehaviour
    {
        // Поточний стан гравця
        private PlayerState currentState;

        // Список слухачів
        private List<IPlayerStateListener> listeners = new List<IPlayerStateListener>();

        // Метод для зміни стану
        public void ChangeState(PlayerState newState)
        {
            // Якщо стан не змінився, нічого не робимо
            if (currentState == newState) return;

            // Оновлюємо поточний стан
            currentState = newState;

            // Сповіщаємо всіх слухачів про зміни стану
            NotifyListeners(currentState);
        }

        // Метод для додавання слухача
        public void RegisterListener(IPlayerStateListener listener)
        {
            if (!listeners.Contains(listener))
            {
                listeners.Add(listener);
            }
        }

        // Метод для видалення слухача
        public void UnregisterListener(IPlayerStateListener listener)
        {
            if (listeners.Contains(listener))
            {
                listeners.Remove(listener);
            }
        }

        // Метод для сповіщення слухачів
        private void NotifyListeners(PlayerState newState)
        {
            foreach (var listener in listeners)
            {
                listener.OnPlayerStateChanged(newState);
            }
        }

        // Отримати поточний стан
        public PlayerState GetCurrentState()
        {
            return currentState;
        }
    }

    // Перелік можливих станів
    public enum PlayerState
    {
        Idle,
        Running,
        Walking,
        Harvesting
    }

    public interface IPlayerStateListener
    {
        void OnPlayerStateChanged(PlayerState newState);
    }
}