using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Medkit : MonoBehaviour, IInteractable
{
    public GameObject effect;
    public float healAmount;
    void HealPlayer()
    {
        if (PlayerManager.Instance != null)
        {
            PlayerManager.Instance.GainHealth((int)healAmount); 
            GameObject obj = Instantiate(effect, transform.position, Quaternion.identity);
            obj.SetActive(true);
            Destroy(obj, 0.4f);
            Destroy(gameObject);
            Debug.Log("Player healed");
        }
    }

    public void OnInteract()
    {
        HealPlayer();
    }
}
