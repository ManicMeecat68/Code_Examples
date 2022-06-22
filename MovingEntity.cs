using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingEntity : MonoBehaviour
{
    [Header("Status")]
    protected float health = 100;
    protected float mana = 100;
    protected int level;

    [Header("Movement")]
    protected float moveSpeed = 5;
    protected float turnSpeed = 3;

    [Header("Stats")]
    protected Weapon weapon;
    protected float baseArmour;
    protected float baseAttackSpeed;
    protected Elements.Element elementalWeakness;
    protected Elements.Element elementalStrength;
    protected float weaknessDamageMultiplier = 1.5f;

    [Header("Graphics")]
    protected Sprite inGameGraphic;
    protected Sprite mapGraphic;
    protected GameObject deathSplat;

    protected void Die()
    {
        if (deathSplat != null)
        {
            Instantiate(deathSplat, transform.position, transform.rotation);
        }
    }
}


