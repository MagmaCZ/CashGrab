using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Money : MonoBehaviour
{
    GameObject GM;
    public GameObject pickUpEffect;
    private void Awake()
    {
        GM = GameObject.FindGameObjectWithTag("GameManager");
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("aasda");
        if (collision.tag == "Player")
        {
            if(collision.GetComponent<Player>().isDead)
            {
                return;
            }
            GM.GetComponent<GameManager>().GetMoney();
            GameObject effectGO = Instantiate(pickUpEffect, transform.position, Quaternion.identity);
            if(GM.GetComponent<AudioSource>().mute == true)
            {
                effectGO.GetComponent<AudioSource>().mute = true;
            }
            Destroy(effectGO, 1f);
            Destroy(gameObject);
        }
    }
    private void Update()
    {
        if (transform.position.y < -15)
        {
            Destroy(gameObject);
        }
    }
}
