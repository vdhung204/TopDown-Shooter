using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class loading : MonoBehaviour
{
    public Slider slider;
    void Start()
    {
        StartCoroutine(LoadSceneMain());
    }
    IEnumerator LoadSceneMain()
    {
        AsyncOperation loadOperation = SceneManager.LoadSceneAsync(SceneName.MainMenu.ToString());

        while (!loadOperation.isDone)
        {
            float progressValue = Mathf.Clamp01(loadOperation.progress / 1f);
            slider.value = progressValue;
            yield return null;
        }
    }
}
