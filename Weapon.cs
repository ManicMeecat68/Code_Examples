using UnityEngine;
using System.Collections;

public class Weapon
{
    [Header("Meta Data")]
    protected string Name;
    protected int Level;

    [Header("Damage Stats")]
    protected float RawDamage;
    protected float DamageVarience;
    protected float ElementalDamage;
    protected Elements.Element ElementalType;

    [Header("Stat Modifiers")]
    protected float AttackSpeed;
    protected float AttackReach;
    protected float AttackRadius;
    protected float CritChance;
    protected float CritMultiplier = 1.25f;

    [Header("Graphics")]
    protected Sprite UiGraphic;
    protected Sprite HeldGraphic;

    public Weapon(string name, int level, float rawDamage, float damageVarience, float elementalDamage, Elements.Element elementalType, float attackSpeed, float attackReach, float attackRadius, float critChance, float critMultiplier)
    {
        Name = name;
        Level = level;
        RawDamage = rawDamage;
        DamageVarience = damageVarience;
        ElementalDamage = elementalDamage;
        ElementalType = elementalType;
        AttackSpeed = attackSpeed;
        AttackReach = attackReach;
        AttackRadius = attackRadius;
        CritChance = critChance;
        CritMultiplier = critMultiplier;

    }
    public Weapon(string name, int level, float rawDamage, float damageVarience, float elementalDamage, Elements.Element elementalType, float attackSpeed, float attackReach, float attackRadius, float critChance, float critMultiplier, Sprite uiGraphic, Sprite heldGraphic)
    {
        Name = name;
        Level = level;
        RawDamage = rawDamage;
        DamageVarience = damageVarience;
        ElementalDamage = elementalDamage;
        ElementalType = elementalType;
        AttackSpeed = attackSpeed;
        AttackReach = attackReach;
        AttackRadius = attackRadius;
        CritChance = critChance;
        CritMultiplier = critMultiplier;
        UiGraphic = uiGraphic;
        HeldGraphic = heldGraphic;
    }
}
