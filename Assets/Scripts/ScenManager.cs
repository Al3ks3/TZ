using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScenManager : MonoBehaviour
{
  public void Scene0()
    {
        SceneManager.LoadScene(0);
        Time.timeScale = 1;
    }
}
