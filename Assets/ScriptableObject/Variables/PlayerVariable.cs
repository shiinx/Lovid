using System;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerVariable", menuName = "ScriptableObjects/Variables/PlayerVariable", order = 2)]
public class PlayerVariable : ScriptableObject {
    [SerializeField] private Vector2 playerBulletDirection;
    [SerializeField] private int playerCurrency;
    [SerializeField] private PlayerConstants.Primary playerEquipped;
    [SerializeField] private float playerHealth;
    
    [SerializeField] private float playerShootDistance;
    [SerializeField] private float playerRangeDamage;
    [SerializeField] private float playerFiringDelay;
    
    [SerializeField] private int playerConfused;
    [SerializeField] private bool playerSilenced;
    [SerializeField] private bool playerTurretDebuffed;
    
    [SerializeField] private bool attackTurretDelayed;
    [SerializeField] private bool defenseTurretDelayed;
    [SerializeField] private bool bombTurretDelayed;

    public bool AttackTurretDelayed {
        get => attackTurretDelayed;
        set => attackTurretDelayed = value;
    }

    public bool DefenseTurretDelayed {
        get => defenseTurretDelayed;
        set => defenseTurretDelayed = value;
    }

    public bool BombTurretDelayed {
        get => bombTurretDelayed;
        set => bombTurretDelayed = value;
    }

    public bool PlayerTurretDebuffed {
        get => playerTurretDebuffed;
        set => playerTurretDebuffed = value;
    }

    public bool PlayerSilenced {
        get => playerSilenced;
        set => playerSilenced = value;
    }

    public int PlayerConfused {
        get => playerConfused;
        set => playerConfused = value;
    }

    public Vector2 PlayerBulletDirection {
        get => playerBulletDirection;
        set => playerBulletDirection = value;
    }

    public int PlayerCurrency {
        get => playerCurrency;
        set => playerCurrency = value;
    }

    public PlayerConstants.Primary PlayerEquipped {
        get => playerEquipped;
        set => playerEquipped = value;
    }

    public float PlayerHealth {
        get => playerHealth;
        set => playerHealth = value;
    }

    public float PlayerShootDistance {
        get => playerShootDistance;
        set => playerShootDistance = value;
    }

    public float PlayerRangeDamage {
        get => playerRangeDamage;
        set => playerRangeDamage = value;
    }

    public float PlayerFiringDelay {
        get => playerFiringDelay;
        set => playerFiringDelay = value;
    }
}