using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


/// <summary>
/// For menu buttons
/// </summary>
public class LoadOnClick : MonoBehaviour
{

    /// <summary>
    /// You can load any scene you want
    /// </summary>
    /// <param name="sceneName"></param>
    public void SceneLoad(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
    /// <summary>
    /// Quit the application...
    /// </summary>
    public void Exit()
    {
        Application.Quit();
    }
}
