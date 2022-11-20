using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
            GameObject newButton = Instantiate(upgradeButton, buttonParent.transform);
            newButton.GetComponent<ButtonText>().buttonText.text = weapon.displayName + " +" + weapon.Level;
            newButton.GetComponent<Button>().onClick.AddListener(() => onUpgrade(weapon));
        }
    }
}
