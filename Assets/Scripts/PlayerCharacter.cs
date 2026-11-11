using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerCharacter : MonoBehaviour
{
    [SerializeField] private AttackData _startingAttackData;

    private void Awake()
    {
        //_gameController = FindAnyObjectByType<GameController>();
    }

    private void Start()
    {
        CreateAttack(_startingAttackData);
    }

    public void CreateAttack(AttackData attackData)
    {
        AttackSystem newAttackSystem =
            gameObject.AddComponent<AttackSystem>();
        newAttackSystem.SetupAttack(attackData);
    }

}
