using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour, IInteractable
{
    public GameObject effect;
    public float explodeDmg;
    public GameObject[] obj;
    void Awake()
    {
        int random = Random.Range(0, obj.Length);
        obj[random].SetActive(true);
    }

    void ExplodeBomb()
    {
        if (PlayerManager.Instance != null)
        {
            PlayerManager.Instance.TakeDamage((int)explodeDmg);
            PlayerMovement.Instance.OnInteract(); 
            GameObject obj = Instantiate(effect, transform.position, Quaternion.identity);
            obj.SetActive(true);
            Destroy(obj, 0.6f);
            Destroy(gameObject);
            Debug.Log("Explode");
        }
    }

    public void OnInteract()
    {
        ExplodeBomb();
    }
}
