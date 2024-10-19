using System.Collections.Generic;
using SGS29.Utilities;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Sirenix.OdinInspector;
using cdvproject.DataManagement;

namespace cdvproject.Demo.DataManagement
{
    /// <summary>
    /// This script is part of a demonstration to showcase the functionality of a data management system
    /// in games or applications. It demonstrates how user data, such as username and level, can be saved
    /// and loaded using an abstract storage system.
    /// 
    /// The script uses <see cref="StorageService"/> to handle data persistence, making it easy to swap out 
    /// storage implementations.
    /// 
    /// The purpose of this file is to **demonstrate** the flow of data handling, including saving and loading
    /// operations for simple game data. It can serve as a practical example for testing or integrating a 
    /// more comprehensive data management system.
    /// </summary>
    public class DemoUI : MonoBehaviour
    {
        [Title("User Data Display")]

        /// <summary>
        /// Displays the username of the player.
        /// </summary>
        [SerializeField, Tooltip("Displays the username of the player.")]
        private TextMeshProUGUI text_UserName; // Displays the username

        /// <summary>
        /// Displays the level of the player.
        /// </summary>
        [SerializeField, Tooltip("Displays the level of the player.")]
        private TextMeshProUGUI text_UserLevel; // Displays the user level

        [Title("User Data Management")]

        /// <summary>
        /// Button for loading user data.
        /// </summary>
        [SerializeField, Tooltip("Button for loading user data.")]
        private Button button_Load; // Button for loading data

        /// <summary>
        /// Button for saving user data.
        /// </summary>
        [SerializeField, Tooltip("Button for saving user data.")]
        private Button button_Save; // Button for saving data

        // List of possible names for random selection
        private List<string> names = new List<string>
        {
            "Dmillov",
            "Qpiopl",
            "Andrii",
            "Ivan_shevchuk00",
            "Radk1991"
        };

        // Keys for saving data
        private const string KEY_USER_NAME = "usernamekey"; // Key for username
        private const string KEY_USER_LEVEL = "userlevelkey"; // Key for user level

        /// <summary>
        /// Initializes the buttons and subscribes to their click events.
        /// This method demonstrates how the user can interact with the system to save and load data.
        /// </summary>
        void Awake()
        {
            // Subscribe to button click events
            button_Load.onClick.AddListener(OnLoad);
            button_Save.onClick.AddListener(OnSave);
        }

        /// <summary>
        /// Sets the filename for storing demo data upon starting the script.
        /// This ensures the data is saved under a consistent name for the demonstration.
        /// </summary>
        void Start()
        {
            SM.Instance<StorageService>().SetFileName("demoData");
        }

        /// <summary>
        /// Demonstrates saving the user's data, such as a randomly generated username and user level.
        /// Uses <see cref="StorageService"/> for data persistence.
        /// </summary>
        private void OnSave()
        {
            // Save a random username and user level
            SM.Instance<StorageService>().SaveData(KEY_USER_NAME, GetRandomName());
            SM.Instance<StorageService>().SaveData(KEY_USER_LEVEL, GetRandomLevel());
        }

        /// <summary>
        /// Demonstrates loading the saved user data and displaying it on the screen.
        /// Retrieves the username and level from storage and formats them for display.
        /// </summary>
        private void OnLoad()
        {
            // Display the username and level on the screen
            text_UserName.text = GetFormatUserName();
            text_UserLevel.text = GetFormatUserLevel();
        }

        /// <summary>
        /// Retrieves the formatted username from the saved data.
        /// Formats the username to be displayed on the UI.
        /// </summary>
        /// <returns>Formatted username to display on the UI.</returns>
        private string GetFormatUserName()
        {
            string userName = SM.Instance<StorageService>().GetData<string>(KEY_USER_NAME);
            string formatText = $"User Name: {userName}";
            return formatText;
        }

        /// <summary>
        /// Retrieves the formatted user level from the saved data.
        /// Formats the user level to be displayed on the UI.
        /// </summary>
        /// <returns>Formatted user level to display on the UI.</returns>
        private string GetFormatUserLevel()
        {
            string userLevel = SM.Instance<StorageService>().GetDataAsString(KEY_USER_LEVEL);
            string formatText = $"User Level: {userLevel}";
            return formatText;
        }

        /// <summary>
        /// Generates a random username from the predefined list of names.
        /// This simulates the user input for testing purposes.
        /// </summary>
        /// <returns>Random username.</returns>
        private string GetRandomName() => names[Random.Range(0, names.Count)];

        /// <summary>
        /// Generates a random user level between 0 and 999.
        /// This simulates user progression in a game.
        /// </summary>
        /// <returns>Random user level.</returns>
        private int GetRandomLevel() => Random.Range(0, 1000);
    }
}