using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using TMPro;

public class SceneManagerer : MonoBehaviour
{

    [SerializeField] private TMP_Dropdown resolutionDropdown;
    [SerializeField] Toggle fsToggle;

    private Resolution[] resolution;
    private List<Resolution> filteredResolutions;

    private float currentRefreshRate;
    private int currentResolutionIndex = 0;

    public GameObject loadingScreen;
    public Image loadingBarFill;

    public void LoadSceneByInt(int sceneId)
    {
        StartCoroutine(LoadSceneAsync(sceneId));

        // SceneManager.LoadScene(sceneId);
    }

    public void QuitGame()
    {
        Application.Quit();
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

    void Start()
    {

        resolution = Screen.resolutions;
        filteredResolutions = new List<Resolution>();

        resolutionDropdown.ClearOptions();
        currentRefreshRate = Screen.currentResolution.refreshRate;

        for (int i = 0; i < resolution.Length; ++i)
        {
            if (resolution[i].refreshRate == currentRefreshRate)
            {
                filteredResolutions.Add(resolution[i]);
            }
        }
        filteredResolutions.Reverse();

        List<string> options = new List<string>();
        for (int i = 0; i < filteredResolutions.Count; ++i)
        {
            string resolutionOption = filteredResolutions[i].width + "x" + filteredResolutions[i].height + " " + filteredResolutions[i].refreshRate + "hz";
            options.Add(resolutionOption);
            if (filteredResolutions[i].width == Screen.width && filteredResolutions[i].height == Screen.height)
            {
                currentResolutionIndex = i;
            }
        }

        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = currentResolutionIndex;
        resolutionDropdown.RefreshShownValue();

        LoadSettings();
    }

    public void SetResolution(int resolutionIndex)
    {
        Resolution resolution = filteredResolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, fsToggle.isOn);


        PlayerPrefs.SetFloat("ResolutionWidth", resolution.width);
        PlayerPrefs.SetFloat("ResolutionHeight", resolution.height);

        PlayerPrefs.Save();
    }
    public void WindowedMode()
    {
        Screen.fullScreenMode = fsToggle.isOn ? FullScreenMode.FullScreenWindow : FullScreenMode.Windowed;

        PlayerPrefs.SetInt("fullscreenMode", fsToggle.isOn ? 1 : 3);
        PlayerPrefs.Save();
    }

    public void LoadGameObject(GameObject item)
    {
        if (fsToggle.isOn == false)
        {
            item.SetActive(true);
        }
        else
        {
            item.SetActive(false);
        }
    }

    void LoadSettings()
    {
        fsToggle.isOn = PlayerPrefs.GetInt("fullscreenMode") == 1;
        // Screen.SetResolution((int)PlayerPrefs.GetFloat("ResolutionWidth"), (int)PlayerPrefs.GetFloat("ResolutionHeight"), fsToggle.isOn);
    }

    public void SelectedButton(GameObject button)
    {
        EventSystem.current.SetSelectedGameObject(button);
    }
}
