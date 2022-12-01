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
    public GameObject teleportMenu;
    //public TMP_Text buttontext;

    private void OnEnable()
    {
        {
            for (int i = 0; i < buttonParent.transform.childCount; i++)
            {
                Destroy(buttonParent.transform.GetChild(i).gameObject);
            }
            foreach (GameObject b in player.GetComponent<PlayerData>().GetBonfires())
            {
                GameObject newButton = Instantiate(teleportButton, buttonParent.transform);
                newButton.GetComponentInChildren<TMP_Text>().text = b.GetComponent<Bonfire>().bonfireName;
                //buttontext.text = b.bonfireName;
                newButton.GetComponent<Button>().onClick.AddListener(() => onTeleport(b.transform));
            }
        }
    }

    private void onTeleport(Transform t)
    {
        teleportMenu.SetActive(false);
        player.GetComponent<PlayerData>().Teleport(t.position + new Vector3(0.0f, 0.3f, 0.0f));
        player.GetComponent<PlayerController>().InMenu(false);
    }
}
