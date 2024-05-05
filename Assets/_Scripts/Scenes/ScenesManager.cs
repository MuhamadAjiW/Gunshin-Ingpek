using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class ScenesManager : MonoBehaviour
{

    public static ScenesManager Instance;

    public UIDocument LoadingScreen;

    public int MinimumTransitionTimeSeconds;
    public void Awake()
    {
        Instance = this;
    }

    public enum Scene
    {
        SplashScreen,
        MainMenu,
        Level_1,
    }

    public Scene[] SceneList
    {
        get => (Scene[])Enum.GetValues(typeof(Scene));
    }

    public void DisplayLoadingScreen()
    {
        VisualElement loadingScreenInnerContainer = UIManagement.GetInnerContainer(LoadingScreen);
        UIManagement.ToggleElementVisible(loadingScreenInnerContainer);
    }

    IEnumerator DelayLoadingScene()
    {
        yield return new WaitForSeconds(MinimumTransitionTimeSeconds);
    }

    public void LoadScene(Scene scene, bool withDelay = true, bool withLoadingScreen = true)
    {
        if (withLoadingScreen)
        {
            DisplayLoadingScreen();
        }
        if (withDelay)
        {
            StartCoroutine(DelayLoadingScene());
        }
        SceneManager.LoadSceneAsync(scene.ToString());
    }

    public void LoadNewGame()
    {
        LoadScene(Scene.Level_1);
    }

    public void LoadMainMenu()
    {
        LoadScene(Scene.MainMenu);
    }

    public void LoadNextScene()
    {
        int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;
        if (nextSceneIndex > SceneManager.sceneCountInBuildSettings - 1)
        {
            Debug.LogError("Trying to load non-existent scene");
            return;
        }
        LoadScene(SceneList[nextSceneIndex]);
    }
}

