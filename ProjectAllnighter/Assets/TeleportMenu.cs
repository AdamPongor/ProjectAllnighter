using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TeleportMenu : MonoBehaviour
{
    public GameObject player;
    public GameObject buttonParent;
    public GameObject teleportButton;
    //public TMP_Text buttontext;

    private void OnEnable()
    {
        {
            foreach (GameObject b in player.GetComponent<PlayerData>().GetBonfires())
            {
                GameObject newButton = Instantiate(teleportButton, buttonParent.transform);
                teleportButton.GetComponent<ButtonText>().buttonText.text = b.GetComponent<Bonfire>().bonfireName;
                //buttontext.text = b.bonfireName;
                newButton.GetComponent<Button>().onClick.AddListener(() => onTeleport(b.transform));
            }
        }
    }

    private void onTeleport(Transform t)
    {
        //player. = t;
    }
}
