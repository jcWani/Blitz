using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] GameObject pauseMenu;
    [SerializeField] private AudioSource btnPressed;
    [SerializeField] private AudioSource bgm;
    [SerializeField] private AudioSource inP;
    [SerializeField] private AudioSource outP;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Pause()
    {
        pauseMenu.SetActive(true);
        inP.Play();
        bgm.Stop();
        Time.timeScale = 0f;
    }

    public void Resume()
    {
        pauseMenu.SetActive(false);
        outP.Play();
        bgm.Play();
        Time.timeScale = 1f;
    }

    public void Restart()
    {
        Time.timeScale = 1f;
        btnPressed.Play();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void Home(int sceneID)
    {
        Time.timeScale = 1f;
        btnPressed.Play();
        SceneManager.LoadScene(sceneID);
    }

    public void LvlSelect(int sceneID1)
    {
        Time.timeScale = 1f;
        btnPressed.Play();
        SceneManager.LoadScene(sceneID1);
    }



}
