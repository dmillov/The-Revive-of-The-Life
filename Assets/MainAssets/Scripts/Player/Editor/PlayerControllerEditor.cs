using UnityEditor;
using Sirenix.OdinInspector.Editor; // Підключаємо UnityEditor для кастомного інспектора

namespace cdvproject.Player
{

    // Створюємо кастомний інспектор за допомогою Editor класу та Odin
    namespace cdvproject.Player
    {
        [CustomEditor(typeof(PlayerController))]
        public class PlayerControllerEditor : OdinEditor
        {
            private PlayerController playerController;

            protected override void OnEnable()
            {
                base.OnEnable();
                playerController = (PlayerController)target;

                // Додаємо оновлення для редактора  
                EditorApplication.update += UpdateInspector;
            }

            protected override void OnDisable()
            {
                base.OnEnable();

                // Прибираємо оновлення при відключенні редактора
                EditorApplication.update -= UpdateInspector;
            }

            private void UpdateInspector()
            {
                // Перерисовуємо інспектор, щоб відобразити зміни
                Repaint();
            }

            public override void OnInspectorGUI()
            {
                // Викликаємо стандартний інспектор Odin
                base.OnInspectorGUI();

                // Додаємо кастомний текстовий індикатор стану гравця
                EditorGUILayout.Space();

                // Виводимо текстовий стан гравця без стилізації
                EditorGUILayout.LabelField("Current Player State:", playerController.CurrentStateText);
            }
        }
    }
}