using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuReturn : MonoBehaviour
{

    public string MenuScene;

    public void GotoMenuScene()
    {
        SceneManager.LoadScene(MenuScene);
    }
}
