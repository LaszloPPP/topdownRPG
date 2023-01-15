using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//
using UnityEngine.UI;


public class NPCText : Collidable
{
    public string message;
    private float cooldown = 4.0f;
    private float lastShout = -4.0f;
    //new
    public Image prefabBubble;
    private Image bubble;

    private Vector3 offset = new Vector3(0.16f, 0.2f, 0);

    protected override void Start()
    {
        base.Start();
        bubble = Instantiate(prefabBubble, FindObjectOfType<Canvas>().transform).GetComponent<Image>();
    }
    
    //
    protected override void OnCollide(Collider2D coll)
    {

        if (Time.time - lastShout > cooldown)
        {
            lastShout = Time.time;
            bubble.transform.position = Camera.main.WorldToScreenPoint(transform.position + offset);
            /*
            lastShout = Time.time;
            //GameManager.instance.ShowText(message, 14, Color.white, Camera.main.WorldToScreenPoint(transform.position), Vector3.zero, cooldown);
            GameManager.instance.ShowText(message, 14, Color.white, transform.position + new Vector3(0, 0.16f, 0), Vector3.zero, cooldown);
            */
        }

    }
}
