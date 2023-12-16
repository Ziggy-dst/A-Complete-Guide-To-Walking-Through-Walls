using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public CanvasGroup canvasGroup;
    public float fadeSpeed = 1.5f;

    public List<GameObject> titleList = new List<GameObject>();

    [HideInInspector] public bool isPlayerDead = false;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = false;
        StartCoroutine(FadeOut());
    }

    // Update is called once per frame
    void Update()
    {
        RestartLevel();
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        foreach (var title in titleList)
        {
            title.SetActive(false);
        }
        if (scene.buildIndex > 0)
        {
            titleList[scene.buildIndex-1].SetActive(true);
        }
        isPlayerDead = false;
        // print(scene);
        // fadePanel.color = new Color(0f, 0f, 0f, 0);
        StartCoroutine(FadeOut());
    }

    IEnumerator FadeIn()
    {
        float alpha = canvasGroup.alpha;

        while (alpha < 1f)
        {
            alpha += Time.deltaTime * fadeSpeed;
            canvasGroup.alpha = alpha;
            yield return null;
        }
    }

    IEnumerator FadeOut()
    {
        float alpha = canvasGroup.alpha;

        while (alpha > 0f)
        {
            print(alpha);
            alpha -= Time.deltaTime * fadeSpeed;
            canvasGroup.alpha = alpha;
            yield return null;
        }
    }

    private void RestartLevel()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            Restart();
            // SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        if (Input.GetMouseButtonDown(0))
        {
            if (SceneManager.GetActiveScene().buildIndex == 0) ProceedToNextLevel();
        }

        if (Input.GetKeyDown(KeyCode.A))
        {
            if (SceneManager.GetActiveScene().buildIndex - 1 >= 0)
            {
                StartCoroutine(LoadScene(SceneManager.GetActiveScene().buildIndex - 1));
                // SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
            }
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            ProceedToNextLevel();
        }
    }

    public void Restart()
    {
        StartCoroutine(LoadScene(SceneManager.GetActiveScene().buildIndex));
    }



    public void ProceedToNextLevel()
    {
        if (SceneManager.GetActiveScene().buildIndex + 1 < SceneManager.sceneCountInBuildSettings)
        {
            StartCoroutine(LoadScene(SceneManager.GetActiveScene().buildIndex + 1));
            // SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
        else
        {
            StartCoroutine(LoadScene(0));
            // SceneManager.LoadScene(0);
        }
    }

    private IEnumerator LoadScene(int buildIndex)
    {
        StartCoroutine(FadeIn());
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(buildIndex);
    }

}