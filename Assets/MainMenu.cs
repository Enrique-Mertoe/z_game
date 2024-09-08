using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public TMP_Text highScoreUi;
    public string newGameScene = "SampleScene";
    public Canvas game;
    public Canvas welcome;

    public TMP_Text loader;

    void Start()
    {
        var score = SaveLoadManger.Instance.LoadHighScore();
        highScoreUi.text = $"Highest Score: {score}";
    }

    public void StartApp()
    {
        welcome.gameObject.SetActive(false);
        game.gameObject.SetActive(true);
    }

    public void BackToStart()
    {
        welcome.gameObject.SetActive(true);
        game.gameObject.SetActive(false);
    }

    private void MakeIntent(string target)
    {
        StartCoroutine(LoadSceneAsync(target));
    }

    public void PlayGame()
    {
        MakeIntent(newGameScene);
    }

    private IEnumerator LoadSceneAsync(string sceneName)
    {
        // Activate the loading text
        loader.gameObject.SetActive(true);

        // Start loading the scene asynchronously
        var asyncLoad = SceneManager.LoadSceneAsync(sceneName);
        if (asyncLoad != null)
            while (!asyncLoad.isDone)
                yield return null;


        // Deactivate the loading text after the scene has loaded
        loader.gameObject.SetActive(false);
    }


    public void ExitApp()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;

#else
        Application.Quit();
#endif
    }
}