using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rock : MonoBehaviour
{
    GameObject GM;
    [SerializeField] List<Sprite> sprites;
    [SerializeField] GameObject hitEffect;
    
    
    private void Awake()
    {
         GM = GameObject.FindGameObjectWithTag("GameManager");
        GetComponent<SpriteRenderer>().sprite = sprites[Random.Range(0, sprites.Count)];
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            if(collision.GetComponent<Player>().isDead)
            {
                return;
            }
            GM.GetComponent<GameManager>().EndGame();
            collision.gameObject.GetComponent<Animator>().SetBool("PlayerDead", true);
            collision.GetComponent<Player>().isDead = true;
            Destroy(collision.gameObject, 1f);
        }
        if(collision.name.Equals("GroundCollider"))
        {
            GameObject effectGO = Instantiate(hitEffect, transform.position, Quaternion.identity);
            Destroy(effectGO, .2f);
            Destroy(gameObject);
        }
    }
    private void Update()
    {
        if(transform.position.y < -15)
        {
            Destroy(gameObject);
        }
    }
}
