using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Anvil : MonoBehaviour
{
    public GameObject player;
    private List<WeaponData> weapons = new List<WeaponData>();
    public GameObject buttonParent;
    public GameObject upgradeButton;

    public void OnEnable()
    {
        weapons.Clear();
        foreach (GameObject w in player.GetComponent<PlayerController>().weapons)
        {
            weapons.Add(w.GetComponent<WeaponData>());
        }

        RefreshButtons();
    }

    private void onUpgrade(WeaponData w)
    {
        w.Upgrade();
        RefreshButtons();
    }
    
    private void RefreshButtons()
    {
        for (int i = 0; i < buttonParent.transform.childCount; i++)
        {
            Destroy(buttonParent.transform.GetChild(i).gameObject);
        }

        foreach (WeaponData weapon in weapons)
        {
            if (weapon.Equipped)
            {
                GameObject newButton = Instantiate(upgradeButton, buttonParent.transform);
                List<TMP_Text> texts = new List<TMP_Text>();
                texts.AddRange(newButton.GetComponentsInChildren<TMP_Text>());

                texts[0].text = weapon.displayName + " +" + weapon.Level;

                if (weapon.UpgradeAble())
                {
                    texts[1].text = weapon.getUpgradeCost().ToString();
                    newButton.GetComponentInChildren<Button>().onClick.AddListener(() => onUpgrade(weapon));
                }
                else
                {
                    texts[1].text = "Max";
                }
            }
        }
    }
}
