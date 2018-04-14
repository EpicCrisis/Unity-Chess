using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    //To create a restart button once there is checkmate.
    //Add current player turn and turn counter.

    [SerializeField] private GameObject restartButton = null;

    [SerializeField] private int startScene = 0;

    public void ButtonRestart()
    {
        SceneManager.LoadScene(startScene);
    }

    public GameObject GetRestartButton()
    {
        return restartButton;
    }
}
