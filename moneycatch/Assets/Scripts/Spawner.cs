using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject rock;
    public Transform rockParent;
    public GameObject money;
    public GameObject moneyParent;

    GameManager GM;
    GameObject player;
    float positionOffsetX;
    float defaultWaitTime = .75f;
    float waitTime;
    Vector3 newPositionOffsetX;
    float reduceWaitTime = 0f;


    GameObject chosenPrefab;
    Vector3 pos;
    Transform prefabParent;
    private void Awake()
    {
        GM = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        player = GameObject.FindGameObjectWithTag("Player");
        StartCoroutine("Spawn");
    }

    private IEnumerator Spawn()
    {
        int number = Random.Range(1, 6);
        //Rock random pos
        if (number == 1 || number == 2 || number == 3)
        {
            positionOffsetX = Random.Range(-1.7f, 1.71f);
            Vector3 offset = new Vector3(transform.position.x + positionOffsetX, transform.position.y);
            chosenPrefab = rock;
            pos = transform.position + offset;
            prefabParent = rockParent.transform;
        }
        //Rock close to player pos
       else if (number == 4)
        {
            positionOffsetX = Random.Range(-.5f, .51f);
            Vector3 offset = new Vector3(player.transform.position.x + positionOffsetX, transform.position.y);
            chosenPrefab = rock;
            pos = transform.position + offset;
            prefabParent = rockParent.transform;
        }
        //Money random pos
        else if (number == 5)
        {
            positionOffsetX = Random.Range(-1.7f, 1.71f);
            Vector3 offset = new Vector3(transform.position.x + positionOffsetX, transform.position.y);
            chosenPrefab = money;
            pos = transform.position + offset;
            prefabParent = moneyParent.transform;
        }

        if (chosenPrefab == rock)
        {
            if(Random.Range(1, 5) = 1)
            {

            }

        }
        Instantiate(chosenPrefab, pos, Quaternion.identity, prefabParent);
        Debug.Log(waitTime);
        yield return new WaitForSeconds(GetWaitTime());
        ShortenWaitTime();
        StartCoroutine("Spawn");
    }
    int count;
    public float GetWaitTime()
    {
        if(GM.score == 10  && count !=2)
        {
            defaultWaitTime =- .05f;
            count++;
        }
        waitTime = defaultWaitTime - reduceWaitTime;
        if (waitTime < .2f) { waitTime = .2f; }
        Debug.Log(waitTime);
        return waitTime;
    }
   public void ShortenWaitTime()
    {
        reduceWaitTime += 0.001f;
    }
}
