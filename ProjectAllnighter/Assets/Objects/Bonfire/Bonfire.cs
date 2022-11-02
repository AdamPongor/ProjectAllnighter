using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bonfire : MonoBehaviour
{

    public string bonfireName;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Rest(PlayerData player)
    {
        player.Heal(1000);
        player.AddBonfire(this.gameObject);
    }
}
