using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    int levelsUnlocked;
    [SerializeField] Button[] buttons;
    [SerializeField] private AudioSource bgm;
    [SerializeField] private AudioSource btnPress;

    // Start is called before the first frame update
    void Start()
    {
        levelsUnlocked = PlayerPrefs.GetInt("levelsUnlocked", 1);

        for(int a=0; a < buttons.Length; a++)
        {
            buttons[a].interactable = false;
        }

        for (int a = 0; a < levelsUnlocked; a++)
        {
            buttons[a].interactable = true;
        }

    }

    public void loadLevel(int levelIndex)
    {
        bgm.Stop();
        btnPress.Play();
        SceneManager.LoadScene(levelIndex);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
