using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class winner : MonoBehaviour
{

    public GameObject textBox;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Sequence());
    }

    IEnumerator Sequence()
    {
        if (PlayerController.oneWins)
        {
            yield return new WaitForSeconds(5);
            textBox.GetComponent<Text>().text = "Player 1 Wins!!!! :D";
        }
        else if (PlayerController.twoWins)
        {
            yield return new WaitForSeconds(5);
            textBox.GetComponent<Text>().text = "Player 2 Wins!!!! :D";
        }
        yield break;
    }
}
