using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LevelUpMenu : MonoBehaviour
{
    public GameObject player;
    public TMP_Text LevelText;
    public TMP_Text VitalityText;
    public TMP_Text EnduranceText;
    public TMP_Text StrengthText;
    public TMP_Text DexterityText;
    public TMP_Text IntelligenceText;
    public TMP_Text LevelCost;
    public TMP_Text XPLeft;

    private PlayerData data;
    private int Level;
    private int Vitality;
    private int Endurance;
    private int Strength;
    private int Dexterity;
    private int Intelligence;
    private int PlayerXP;

    private int levelUpCost = 100;


    // Start is called before the first frame update
    void Start()
    {
        data = player.GetComponent<PlayerData>();
        Level = data.Level;
        Vitality = data.Vitality;
        Endurance = data.Endurance;
        Strength = data.Strength;
        Dexterity = data.Dexterity;
        Intelligence = data.Intelligence;
        PlayerXP = data.XP;
        UpdateMenu();
    }

    public void UpdateMenu()
    {
        LevelText.text = "Level: " + Level;
        VitalityText.text = "Vitality: " + Vitality;
        EnduranceText.text = "Endurance: " + Endurance;
        StrengthText.text = "Strength: " + Strength;
        DexterityText.text = "Dexterity: " + Dexterity;
        IntelligenceText.text = "Intelligence: " + Intelligence;
        LevelCost.text = levelUpCost.ToString();
        XPLeft.text = PlayerXP.ToString();
    }

    public void SetVitality(int i)
    {
        if ((PlayerXP >= levelUpCost || i<0) && Vitality+i > 0)
        {
            Level += i;
            Vitality += i;
            if(i>0)
                PlayerXP -= levelUpCost*i;
            levelUpCost += 100*i;
            if(i<0)
                PlayerXP -= levelUpCost * i;
            UpdateMenu();
        }
    }
    public void SetEndurance(int i)
    {
        if ((PlayerXP >= levelUpCost || i < 0) && Endurance + i > 0)
        {
            Level += i;
            Endurance += i;
            if (i > 0)
                PlayerXP -= levelUpCost * i;
            levelUpCost += 100 * i;
            if (i < 0)
                PlayerXP -= levelUpCost * i;
            UpdateMenu();
        }
    }
    public void SetStrength(int i)
    {
        if ((PlayerXP >= levelUpCost || i < 0) && Strength + i > 0)
        {
            Level += i;
            Strength += i;
            if (i > 0)
                PlayerXP -= levelUpCost * i;
            levelUpCost += 100 * i;
            if (i < 0)
                PlayerXP -= levelUpCost * i;
            UpdateMenu();
        }
    }
    public void SetDexterity(int i)
    {
        if ((PlayerXP >= levelUpCost || i < 0) && Dexterity + i > 0)
        {
            Level += i;
            Dexterity += i;
            if (i > 0)
                PlayerXP -= levelUpCost * i;
            levelUpCost += 100 * i;
            if (i < 0)
                PlayerXP -= levelUpCost * i;
            UpdateMenu();
        }
    }
    public void SetIntelligence(int i)
    {
        if ((PlayerXP >= levelUpCost || i < 0) && Intelligence + i > 0)
        {
            Level += i;
            Intelligence += i;
            if (i > 0)
                PlayerXP -= levelUpCost * i;
            levelUpCost += 100 * i;
            if (i < 0)
                PlayerXP -= levelUpCost * i;
            UpdateMenu();
        }
    }
    public void OnExit()
    {
        
    }
}
