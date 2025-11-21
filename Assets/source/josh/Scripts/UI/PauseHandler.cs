/*   Author: Josh Gillum              .
 *   Date: 6 November 2025           ":"         __ __
 *                                  __|___       \ V /
 *                                .'      '.      | |
 *                                |  O       \____/  |
 *~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~
 * This file stores the PauseHandler class, which handles pause functionality.
 * Controls the associated UI as well as the actual pause functions.
 *~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~
 */

using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.InputSystem;

public class PauseHandler : MonoBehaviour
   // Handles pause functionality and its associated UI elements.
{
   private VisualElement menu; // Pause Menu 
   private bool isPaused = false; // Stores whether the game is currently paused
   private InputAction Cancel; // Cancel action, ex: Escape key
   private Button buttonQuitGame; // Button to quit game
   private Button buttonResumeGame; // Button to resume game

   [SerializeField] private AudioSource backgroundMusic; // Background music player

   void Awake()
      // Finds the various elements of the menu and stores them in variables
   {
      UIDocument uiDocument = GetComponent<UIDocument>();
      menu = uiDocument.rootVisualElement;
      buttonQuitGame = menu.Q<Button>("ExitGame");
      buttonQuitGame.clicked += QuitGame;
      buttonResumeGame = menu.Q<Button>("ResumeGame");
      buttonResumeGame.clicked += Resume;
      Cancel = InputSystem.actions.FindAction("Cancel");

   }

   void Start()
      // Menu is visible by default
   {
      menu.visible = false;
   }

   void Update()
      // Pauses or resumes the game
   {
      if (Cancel.WasPressedThisFrame())
      {
         if (isPaused)
         {
            Resume();
         }
         else
         {
            Pause();
         }
      }
   }

   public void Resume()
      // Hides the menu, unpauses music, and resumes game time
   {
      Debug.Log("Resuming...");
      menu.visible = false;
      Time.timeScale = 1f; // Resume game time
      backgroundMusic.UnPause();
      isPaused = false;
   }

   void Pause()
      // Shows the menu, pauses music, and pauses game time
   {
      Debug.Log("Pausing...");
      menu.visible = true;
      Time.timeScale = 0f; // Freeze game time
      backgroundMusic.Pause();
      isPaused = true;
   }

   public void QuitGame()
      // Closes the game
   {
      Debug.Log("Quitting game...");
      Application.Quit();
   }
}
