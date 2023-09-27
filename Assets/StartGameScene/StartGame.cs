using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class StartGame : MonoBehaviour
{
    [SerializeField] private string sceneName;
    [SerializeField] private AudioSource bgm;
    [SerializeField] private AudioSource mStart;
    [SerializeField] private AudioSource btnPressed;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void PlayPressed()
    {
        bgm.Stop();
        mStart.Play();
        StartCoroutine(changeScene());
    }

    public void ResetLevels()
    {
        PlayerPrefs.DeleteKey("levelsUnlocked");
    }

    public void QuitPressed()
    {
        btnPressed.Play();
        ResetLevels();
        Application.Quit();
    }


    private IEnumerator changeScene()
    {
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene(sceneName);
    }

}
