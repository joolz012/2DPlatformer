using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UniRx;

public class HUD : MonoBehaviour
{
    public Slider hpSlider;
    public TextMeshProUGUI coinsText;
    public GameObject deathText;
    public GameObject gameFinishedText;

    private void Start()
    {
        PlayerMovement playerMovement = FindObjectOfType<PlayerMovement>();
        PlayerManager playerManager = FindObjectOfType<PlayerManager>();

        playerManager.HP.Subscribe(hp =>
        {
            hpSlider.value = hp;
        }).AddTo(this);

        playerManager.Coins.Subscribe(coins =>
        {
            coinsText.text = "Coins: " + coins.ToString() + " / 5";
        }).AddTo(this);

        playerManager.OnDeath.Subscribe(_ =>
        {
            playerMovement.DisablePlayer();
            deathText.SetActive(true);
        }).AddTo(this);

        playerManager.OnGameFinished.Subscribe(_ =>
        {
            playerMovement.DisablePlayer();
            gameFinishedText.SetActive(true);
        }).AddTo(this);
    }
}
