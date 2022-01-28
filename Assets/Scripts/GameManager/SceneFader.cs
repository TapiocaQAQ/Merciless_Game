using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneFader : MonoBehaviour
{
    #region Singleton
    
    public static SceneFader instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this);
        }
        else if (instance != this)
        {
            Debug.Log("Instance already exists, destroying object!");
            Destroy(this);
        }
    }

    #endregion

    public Image image;
    public AnimationCurve curve;

    public bool isLoadingGame;//wheather all player is loading

    public void SceneFadeOutIn(string scene)
    {
        StartCoroutine(FadeOut());
        SceneManager.LoadScene(scene);
        StartCoroutine(FadeIn());
    }

    public void LoadGameScene(string scene)
    {
        StartCoroutine(FadeOut());
        StartCoroutine(LoadAsynchronously(scene));
        StartCoroutine(CheckAllPlayerLoading());
        StartCoroutine(FadeIn());
    }

    IEnumerator FadeIn()
    {
        float t = 1f;

        while(t > 0f)
        {
            t -= Time.deltaTime;
            float a = curve.Evaluate(t);
            image.color = new Color(0f,0f,0f,a);
            yield return 0;
        } 
    }

    IEnumerator FadeOut()
    {
        float t = 0f;

        while(t < 1f)
        {
            t += Time.deltaTime;
            float a = curve.Evaluate(t);
            image.color = new Color(0f,0f,0f,a);
            yield return 0;
        }
    }

    IEnumerator LoadAsynchronously(string _scene)
    {
        AsyncOperation operation =  SceneManager.LoadSceneAsync(_scene);

        while(!operation.isDone){
            yield return null;
        }

        IsLoadingGame();
    }

    IEnumerator CheckAllPlayerLoading()
    {
        while(!isLoadingGame)
        {
            yield return null;
        }
        isLoadingGame = false;
    }

    public void IsLoadingGame()
    {
        //ClientSend.IsLoadingGame();
    }

}
