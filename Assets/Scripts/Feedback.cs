using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Feedback : MonoBehaviour
{
    public void PlayAgain()
    {
        SceneManager.LoadScene("Gameplay");
    }
}
