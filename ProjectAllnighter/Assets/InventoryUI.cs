// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
// using UnityEngine.Events;

// public class InventoryUI : MonoBehaviour
// {
//     public GameObject inventoryUI;
//     public UnityEvent inventory;
//     public GameObject player;
//     private PlayerController playerController;
    
//     void Start()
//     {
//         playerController = player.GetComponent<PlayerController>();
//     }
//     public void OnEnable()
//     {
//         inventory.Invoke();
//     }

    
//     void FixedUpdate()
//     {
//         if(Input.GetKeyDown(KeyCode.I) && !playerController.inMenu)
//         {
//             Time.timeScale=0;
//             inventoryUI.SetActive(true);
//         }
//     }
// }
