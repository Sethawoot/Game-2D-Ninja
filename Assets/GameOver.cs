using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOver : MonoBehaviour
{
    public void Retry()
    {
        Debug.Log("โหลดเกมใหม่");
        SceneManager.LoadScene("SampleScene");
    }

    public void Menu()
    {
        SceneManager.LoadScene(0);
    }

}
