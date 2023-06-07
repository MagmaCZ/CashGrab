using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Player : MonoBehaviour
{

    public GameObject spawner;
    public bool isDead;
    float targetX;


    void Update()
    {
        if (isDead)
            return;
        if (GameManager.isOnMobile)
        {
            Vector3 touch = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);
            float touchX = Mathf.Clamp(touch.x, -1.5f, 1.5f);
            touchX = targetX;
            if (Input.touchCount > 0 || touchX != transform.position.x)
            {
                Move(touchX);

            }
            else if (targetX == transform.position.x)
            {
                gameObject.GetComponent<Animator>().SetBool("isMoving", false);
                /*  notMovedCountdown += Time.deltaTime;
                  if(notMovedCountdown >= .75f)
                  {
                      spawner.GetComponent<Spawner>().PlayerAFKPosition(transform.position);
                  }*/
            }
        }
       else
        {
            Vector3 mouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            float mouseX = Mathf.Clamp(mouse.x, -1.5f, 1.5f);
            if (mouseX < transform.position.x)
            {
                transform.position = new Vector3(Mathf.Lerp(transform.position.x, mouseX, (transform.position.x - mouseX) / 1.5f), transform.position.y, 0f);
            }
            else
            {
                transform.position = new Vector3(Mathf.Lerp(transform.position.x, mouseX, (mouseX - transform.position.x) / 1.5f), transform.position.y, 0f);
            }
            if (mouseX < transform.position.x)
            {
                gameObject.GetComponent<SpriteRenderer>().flipX = true;
            }
            if (mouseX > transform.position.x)
            {
                transform.position = new Vector3(Mathf.Lerp(transform.position.x, mouseX, (transform.position.x - mouseX) / 1.5f), transform.position.y, 0f);

                gameObject.GetComponent<SpriteRenderer>().flipX = false;
            }
            if (transform.position.x + .025f >= mouseX && transform.position.x - .025 <= mouseX)
            {
                gameObject.GetComponent<Animator>().SetBool("isMoving", false);
                /* notMovedCountdown += Time.deltaTime;
                 if (notMovedCountdown >= .75f)
                 {
                     spawner.GetComponent<Spawner>().PlayerAFKPosition(transform.position);
                 }*/
            }
            else
            {
                gameObject.GetComponent<Animator>().SetBool("isMoving", true);

            }
        }

    }
    private void Move(float targetX)
    {
        if (targetX < transform.position.x)
        {
            transform.position = new Vector3(Mathf.Lerp(transform.position.x, targetX, (transform.position.x - targetX) / 1.5f), transform.position.y, 0f);
        }
        else
        {
            transform.position = new Vector3(Mathf.Lerp(transform.position.x, targetX, (targetX - transform.position.x) / 1.5f), transform.position.y, 0f);
        }
        if (targetX < transform.position.x)
        {
            gameObject.GetComponent<SpriteRenderer>().flipX = true;
        }
        if (targetX > transform.position.x)
        {
            gameObject.GetComponent<SpriteRenderer>().flipX = false;
        }
        if (transform.position.x + .025f >= targetX && transform.position.x - .025f <= targetX)
        {
            gameObject.GetComponent<Animator>().SetBool("isMoving", false);
        }
        else
        {
            gameObject.GetComponent<Animator>().SetBool("isMoving", true);

        }
    }

}