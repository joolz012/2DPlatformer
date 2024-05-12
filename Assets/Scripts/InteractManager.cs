using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractManager : MonoBehaviour
{
    public GameObject[] objToInstantiate;
    public Transform[] objSpawn;
    private List<Transform> availableSpawnPoints = new List<Transform>();

    void Start()
    {
        StartCoroutine(MoveItems());

        availableSpawnPoints.AddRange(objSpawn);

        foreach (GameObject obj in objToInstantiate)
        {
            if(obj != objToInstantiate[2])
            {
                for (int i = 0; i < 5; i++)
                {
                    if (availableSpawnPoints.Count > 0)
                    {
                        int randomIndex = Random.Range(0, availableSpawnPoints.Count);
                        GameObject safeObj = Instantiate(obj, availableSpawnPoints[randomIndex].position, Quaternion.identity);
                        safeObj.transform.SetParent(transform);
                        availableSpawnPoints.RemoveAt(randomIndex);
                    }
                }
            }

            if(obj == objToInstantiate[2])
            {
                for (int i = 0; i < 10; i++)
                {
                    if (availableSpawnPoints.Count > 0)
                    {
                        int randomIndex = Random.Range(0, availableSpawnPoints.Count);
                        GameObject badObj = Instantiate(obj, availableSpawnPoints[randomIndex].position, Quaternion.identity);
                        badObj.transform.SetParent(transform);
                        availableSpawnPoints.RemoveAt(randomIndex);
                    }
                }
            }
            
        }
    }

    IEnumerator MoveItems()
    {
        while (true)
        {
            while (true)
            {
                while (transform.position.y < 0.1f)
                {
                    transform.Translate(Vector3.up * 0.3f * Time.deltaTime);
                    yield return null;
                }

                while (transform.position.y > -0.4f)
                {
                    transform.Translate(Vector3.down * 0.3f * Time.deltaTime);
                    yield return null;
                }
            }
        }
    }

}
