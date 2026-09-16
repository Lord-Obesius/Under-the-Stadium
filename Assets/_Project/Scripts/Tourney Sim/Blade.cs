using UnityEngine;

[System.Serializable]
public class Blade
{
    [Header("Blade")]
    public string Name;

    [Header("Stats")]
    public int Attack;
    public int Defense;
    public int Stamina;

    [Header("Type")]
    public BladeType Type;

    [Header("Tags")]
    public ContestantTags Tags;

    public Blade(
        string name = "",
        int attack = 0,
        int defense = 0,
        int stamina = 0)
    {
        Name = name;
        Attack = attack;
        Defense = defense;
        Stamina = stamina;

        Tags = new ContestantTags();

        DetermineType();
    }

    public void DetermineType()
    {
        int highestStat = Mathf.Max(Attack, Defense, Stamina);

        int differenceFromLowest =
            highestStat - Mathf.Min(Attack, Defense, Stamina);

        // If the stats are relatively close, the blade is Balance.
        if (differenceFromLowest <= 20)
        {
            Type = BladeType.Balance;
            return;
        }

        if (Attack == highestStat)
        {
            Type = BladeType.Attack;
        }
        else if (Defense == highestStat)
        {
            Type = BladeType.Defense;
        }
        else
        {
            Type = BladeType.Stamina;
        }
    }

    public int GetStatPercentage()
    {
        int total = Attack + Defense + Stamina;

        int percentage = total * 100 / 600;

        return percentage;
    }
}