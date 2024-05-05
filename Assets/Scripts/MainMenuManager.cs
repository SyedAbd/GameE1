using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    public GameObject OptionMenu;
    public GameObject MainMenu;
    public void OnClickPlay()
    {
        SceneManager.LoadScene("Survival_Yue");
    }

    public void OnClickOptions()
    {
        MainMenu.SetActive(false);
        OptionMenu.SetActive(true);
    }

    public void OnClickQuit()
    {
        Application.Quit();
    }

    public void OnClickBackOptions()
    {
        OptionMenu.SetActive(false);
        MainMenu.SetActive(true);
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
