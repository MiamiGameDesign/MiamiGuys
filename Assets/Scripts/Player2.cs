using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player2 : MonoBehaviour
{
    public void StartGame()
    {
        SceneManager.LoadScene("2Player");
    }
}
