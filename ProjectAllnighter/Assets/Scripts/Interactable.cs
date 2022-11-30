using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Interactable : MonoBehaviour
{

    private bool inRange;
    public List<KeyCode> interactKeys;
    public UnityEvent interactAction;
    public string text;
    private Popup prompt;
    public GameObject PromptBox;
    public int LevelRequirement;

    private void Start()
    {
        prompt = PromptBox.GetComponent<Popup>();
        
    }

    // Update is called once per frame
    void Update()
    {
        if (inRange)
        {
            foreach (KeyCode kc in interactKeys)
            {
                if (Input.GetKeyDown(kc))
                {
                    interactAction.Invoke();
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            
            PlayerData player = collision.gameObject.GetComponent<PlayerData>();
            if (player.Level >= LevelRequirement)
            {
                prompt.Pop(text);
                inRange = true;
            } else
            {
                prompt.Pop("Min: lvl " + LevelRequirement);
            }
            
            
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            prompt.Close();
            inRange = false;
        }
    }
}
