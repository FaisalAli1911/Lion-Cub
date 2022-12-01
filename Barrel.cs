using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barrel : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Rigidbody2D>().velocity=new Vector2(6f,0);
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Fire") )
        {
            Destroy(gameObject);
        }
        if (collision.gameObject.CompareTag("Player"))
        {
           Debug.Log("scoreing");
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("BorderL"))
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(6f, 0);
            Debug.Log("sss");
        }
        if (collision.gameObject.CompareTag("BorderR"))
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(-6f, 0);
            Debug.Log("sssr");
        }
        if (collision.gameObject.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
    }


}
