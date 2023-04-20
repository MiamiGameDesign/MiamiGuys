using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player2 : MonoBehaviour
{
    public static bool p2 = false;
    public void StartGame()
    {
        p2 = true;
        SceneManager.LoadScene("2Player");
    }
}
