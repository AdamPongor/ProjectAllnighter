using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class FloatingText : MonoBehaviour
{
    private float TTL = 0.6f;
    private float floatspeed = 100;
    private Vector3 direction = new Vector3(0, 1, 0);
    private float timeElapsed = 0.0f;
    private Color color = Color.red;

    public TextMeshProUGUI textmesh;
    RectTransform rTransform;
    
    public string Text
    {
        set
        {
            GetComponent<TMPro.TextMeshProUGUI>().text = value;
        }
    }

    public void SetColor(Color c)
    {
        color = c;
    }

    // Start is called before the first frame update
    void Start()
    {
        textmesh = GetComponent<TextMeshProUGUI>();
        rTransform = GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        timeElapsed += Time.deltaTime;
        rTransform.position += direction * floatspeed * Time.deltaTime;

        textmesh.color = new Color(color.r, color.g, color.b, color.a - timeElapsed / TTL);

        if (timeElapsed > TTL)
        {
            Destroy(gameObject);
        }
    }
}
