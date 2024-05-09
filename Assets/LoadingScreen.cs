using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class LoadingScreen : MonoBehaviour
{

    public GameObject loadingScreen;
    public Image loadingBarFill;

    public void AsyncLoadSceneByInt(int sceneId)
    {
        StartCoroutine(LoadSceneAsync(sceneId));

        // SceneManager.LoadScene(sceneId);
    }
    public void LoadSceneByInt(int sceneId)
    {
         SceneManager.LoadScene(sceneId);
    }

    IEnumerator LoadSceneAsync(int sceneId)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneId);

        loadingScreen.SetActive(true);

        while (!operation.isDone)
        {
            float progressValue = Mathf.Clamp01(operation.progress / .9f);

            loadingBarFill.fillAmount = progressValue;

            yield return null;
        }
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
