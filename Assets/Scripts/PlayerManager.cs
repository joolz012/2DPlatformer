using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;
using System;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager Instance;

    private int maxHealth = 100;
    private IntReactiveProperty hp = new IntReactiveProperty();
    private IntReactiveProperty coins = new IntReactiveProperty();

    public IObservable<int> HP => hp;
    public IObservable<int> Coins => coins;
    public IObservable<Unit> OnDeath => onDeath;
    public IObservable<Unit> OnGameFinished => onGameFinished;

    private Subject<Unit> onDeath = new Subject<Unit>();
    private Subject<Unit> onGameFinished = new Subject<Unit>();

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        hp.Value = maxHealth;
        coins.Value = 0;
    }

    public void TakeDamage(int damage)
    {
        hp.Value -= damage;

        if (hp.Value <= 0)
        {
            hp.Value = 0;
            onDeath.OnNext(Unit.Default);
        }
    }
    public void GainHealth(int amount)
    {
        if (hp.Value > 0)
        {
            hp.Value += amount;
            if (hp.Value > maxHealth)
                hp.Value = maxHealth;
        }
    }


    public void AddCoins(int amount)
    {
        coins.Value += amount;
        if (coins.Value >= 5)
        {
            onGameFinished.OnNext(Unit.Default);
        }
    }

}

