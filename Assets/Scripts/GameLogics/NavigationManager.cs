using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/// <summary>
/// This class is the UI controller.
/// It repesents the entry point of all the button callback in the UI.
/// The public list of page can be edited directeley on the editor.
/// By convention, the first page of the list is the active one on start.
/// The activation/desactivation of pages are handled.
/// </summary>
public class NavigationManager : MonoBehaviour
{
    /// <summary>
    /// The static instance of the Singleton for external access
    /// </summary>
    public static NavigationManager instance = null;

    [SerializeField]
    private Canvas canvasPause;
    private bool isActive = false;// the apuse menu is active 

    /// <summary>
    /// Enforce Singleton properties
    /// </summary>
    void Awake()
    {
        //Check if instance already exists and set it to this if not
        if (instance == null)
        {
            instance = this;
        }

        //Enforce the unicity of the Singleton
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }

    /// <summary>
    /// The list of all pages componing the UI
    /// </summary>
    public List<GameObject> pages = new List<GameObject>();

    /// <summary>
    /// Set the first page of the "pages" list the active one
    /// </summary>
    void Start()
    {
        foreach (GameObject page in pages)
        {
            page.SetActive(false);
        }
        pages[0].SetActive(true);
    }

    /// <summary>
    /// Set the given page the active one and set selected his first button.
    /// The previous page is set inactive.
    /// Note : This method is called on buttons callbacks
    /// </summary>
    /// <param name="newPage">The new page reference</param>
    public void SwitchPage(GameObject newPage)
    {
        foreach (GameObject page in pages)
        {
            if (page == newPage)
            {
                page.SetActive(true);
                Button[] btns = page.GetComponentsInChildren<Button>();
                if (btns.Length > 0)
                {
                    btns[0].Select();
                }
            }
            else
            {
                page.SetActive(false);
            }
        }
    }

    public void Exit()
    {
        Application.Quit();
    }

    public void LoadScene(string lvlName)
    {
        if (lvlName == "menuScene")
        {
            LobbyManager.instance.Kill();
        }
        SceneManager.LoadScene(lvlName);
    }

    public void pauseMenu()
    {      
        if (!isActive)
        {            
            Time.timeScale = 0f;
            canvasPause.gameObject.SetActive(true);
        }
            else
        {            
            Time.timeScale = 1f;
            canvasPause.gameObject.SetActive(false);
        }
        isActive = !isActive;
    }
    public void resumeGame()
    {
        Time.timeScale = 1f;
        canvasPause.gameObject.SetActive(false);
    }
    public void quitMenu()
    {
        Time.timeScale = 0f;
        Exit();
    }
}
