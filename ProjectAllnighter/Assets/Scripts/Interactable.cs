using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Interactable : MonoBehaviour
{

    private bool inRange;
    public KeyCode interactKey;
    public UnityEvent interactAction;
    public string text;
    private Popup prompt;
    public GameObject PromptBox;

    private void Start()
    {
        prompt = PromptBox.GetComponent<Popup>();
        
    }

    // Update is called once per frame
    void Update()
    {
        if (inRange)
        {
            if (Input.GetKeyDown(interactKey))
            {
                interactAction.Invoke();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            
            prompt.Pop(text);
            inRange = true;
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
