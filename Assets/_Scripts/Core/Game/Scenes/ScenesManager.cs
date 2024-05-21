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
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;

        DontDestroyOnLoad(this);
    }

    public enum GameScene
    {
        SplashScreen,
        MainMenu,
        World,
    }

    public GameScene[] SceneList
    {
        get => (GameScene[])Enum.GetValues(typeof(GameScene));
    }

    public void DisplayLoadingScreen()
    {
        VisualElement loadingScreenInnerContainer = UIManagement.GetInnerContainer(LoadingScreen);
        UIManagement.ToggleElementVisible(loadingScreenInnerContainer, true);
        SceneManager.sceneLoaded += HideLoadingScreen;
    }

    public void HideLoadingScreen(Scene scene, LoadSceneMode mode)
    {
        SceneManager.sceneLoaded -= HideLoadingScreen;
        VisualElement loadingScreenInnerContainer = UIManagement.GetInnerContainer(LoadingScreen);
        UIManagement.ToggleElementVisible(loadingScreenInnerContainer, false);
    }

    IEnumerator DelayLoadingScene()
    {
        yield return new WaitForSeconds(MinimumTransitionTimeSeconds);
    }

    public void LoadScene(GameScene scene, bool withDelay = true, bool withLoadingScreen = true)
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

    public void LoadOverworld()
    {
        LoadScene(GameScene.World);
    }

    public void LoadMainMenu()
    {
        Time.timeScale = 1;
        LoadScene(GameScene.MainMenu);
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

