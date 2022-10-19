using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class FloatingText : MonoBehaviour
{
    private float TTL = 0.4f;
    private float floatspeed = 200;
    private Vector3 direction = new Vector3(0, 1, 0);
    private float timeElapsed = 0.0f;

    public TextMeshProUGUI textmesh;
    RectTransform rTransform;
    
    public string Text
    {
        set
        {
            GetComponent<TMPro.TextMeshProUGUI>().text = value;
        }
        
    }

    // Start is called before the first frame update
    void Start()
    {
        textmesh = GetComponent<TextMeshProUGUI>();
        rTransform = GetComponent<RectTransform>();
        textmesh.color = new Color(1, 0, 0, 1);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        timeElapsed += Time.deltaTime;
        rTransform.position += direction * floatspeed * Time.deltaTime;

        textmesh.color = new Color(1, 0, 0, 1 - timeElapsed / TTL);

        if (timeElapsed > TTL)
        {
            Destroy(gameObject);
        }
    }
}
