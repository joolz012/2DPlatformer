using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    public GameObject[] tutorialObj;
    public int index = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Next()
    {
        if(index < tutorialObj.Length)
        {
            tutorialObj[index].SetActive(false);
            index++;
            if(index != 2)
            {
                tutorialObj[index].SetActive(true);
            }
            else
            {
                PlayerMovement.Instance.enabled = true;
                gameObject.SetActive(false);
                Debug.Log("Go!");
            }
        }
    }
}
