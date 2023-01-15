using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class managerNPCUI : MonoBehaviour
{
    private GameObject objPlayer;
    public Image prefabBubble;
    private Image bubble;
    private Vector3 offset = new Vector3(0.16f, 0.2f, 0);

    void Start()
    {
        objPlayer = GameObject.Find("Player");
        bubble = Instantiate(prefabBubble, FindObjectOfType<Canvas>().transform).GetComponent<Image>();
    }

    void Update()
    {
        float dist = Vector3.Distance(transform.position, objPlayer.transform.position);
        //Debug.Log(dist);
        if (dist <= 0.2f)
        {
            bubble.transform.position = Camera.main.WorldToScreenPoint(transform.position + offset);
            bubble.transform.localScale = new Vector3(1, 1, 1);

        }
        else
        {
            bubble.transform.localScale = new Vector3(0, 0, 0);
        }
    }


}
