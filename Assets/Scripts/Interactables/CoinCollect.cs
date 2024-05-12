using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinCollect : MonoBehaviour, IInteractable
{
    public GameObject effect;
    public float addMoney;

    void AddMoney()
    {
        if (PlayerManager.Instance != null)
        {
            PlayerManager.Instance.AddCoins((int)addMoney);
            GameObject obj = Instantiate(effect, transform.position, Quaternion.identity);
            obj.SetActive(true);
            Destroy(obj, 0.4f);
            Destroy(gameObject);
            Debug.Log("Coins Collected");
        }
    }

    public void OnInteract()
    {
        AddMoney();
    }
}
