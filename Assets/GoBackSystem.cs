using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GoBackSystem : MonoBehaviour
{
    [SerializeField]
    public string sceneName; 


    public void startSystem()
    {
        StartCoroutine(LoadSceneAfterDelay());
    }
    public void GuguGaga()
    {
        FindObjectOfType<AdForReset>().ResetAdCount();
        LoadScene();
    }

    IEnumerator LoadSceneAfterDelay()
    {
        yield return new WaitForSeconds(0.2f);
        LoadScene();
    }

    public void LoadScene()
    {
        SceneManager.LoadScene(sceneName);
    }

    void Start()
    {
        UnityEngine.Rendering.DebugManager.instance.enableRuntimeUI = false;
    }
}
