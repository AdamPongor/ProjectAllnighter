using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bonfire : MonoBehaviour
{

    public string bonfireName;
    public GameObject BF;
    // Start is called before the first frame update
    void Start()
    {
        BF = gameObject;
    }

    public void Rest(PlayerData player)
    {
        player.Heal(1000);
        player.AddBonfire(player.GetLastInteracted());
    }
}
