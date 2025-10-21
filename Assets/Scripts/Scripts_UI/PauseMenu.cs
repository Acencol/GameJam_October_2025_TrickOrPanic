using UnityEngine;
using UnityEngine.SceneManagement;

//PauseMenu Class - Manages the pause menu UI
public class PauseMenu : MonoBehaviour {

    private bool isPaused = false;
    public GameObject pausePanel;

    //Start Method - Called before the first frame update
    void Start() {
        Time.timeScale = 1f;
        pausePanel.SetActive(false);
    } //End of Start Method

    //Update Method - Called once per frame
    void Update() {
        
        if(Input.GetKeyDown(KeyCode.Escape)) {
            if (isPaused) {
                ResumeGame(); //Call Method
            } else {
                PauseGame(); //Call Method
            }
        } 

    } //End of Update Method

    //PauseGame Method - 
    public void PauseGame() {
        Time.timeScale = 0f; // Pause the game by setting time scale to 0
        pausePanel.SetActive(true);
        isPaused = true;
        Debug.Log("Game Paused");
    } //End of PauseGame Method

    //ResumeGame Method - 
    public void ResumeGame() {
        Time.timeScale = 1f; //Resume the game by setting time scale back to 1
        pausePanel.SetActive(false);
        isPaused = false;
        Debug.Log("Game Resumed");
    } //End of ResumeGame Method

    public void QuitGame() {
        Debug.Log("Quitting Game...");
        SceneManager.LoadScene("MainMenu");
    } //End of QuitGame Method

} //End of PauseMenu Class
