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
    float defaultWaitTime = .7f;
    float waitTime;
    Vector3 newPositionOffsetX;
    float reduceWaitTime = 0f;
    float lastPositionOffset;

    int rockCount;
    int moneyCount;
    bool wasPositive;
    GameObject chosenPrefab;
    Vector3 pos;
    Transform prefabParent;
    private void Awake()
    {
        GM = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        player = GameObject.FindGameObjectWithTag("Player");
        StartCoroutine("Spawn");
    }
    Vector3 offset;
    private IEnumerator Spawn()
    {
        int number = Random.Range(1, 6);
        //Rock counter
        if(rockCount >= 3)
        {
            offset = new Vector3(transform.position.x + GetPositionOffsetX(-1.7f,1.71f), transform.position.y);
            chosenPrefab = money;
            pos = transform.position + offset;
            prefabParent = moneyParent.transform;
            rockCount = 0;
        }
        if(moneyCount >= 2)
        {
            offset = new Vector3(transform.position.x + GetPositionOffsetX(-1.7f, 1.71f), transform.position.y);
            chosenPrefab = rock;
            pos = transform.position + offset;
            prefabParent = rockParent.transform;
        }
        //Rock random pos
        else if (number == 1 || number == 2 || number == 3)
        {
             offset = new Vector3(transform.position.x + GetPositionOffsetX(-1.7f,1.71f), transform.position.y);
            chosenPrefab = rock;
            pos = transform.position + offset;
            prefabParent = rockParent.transform;
        }
        //Rock close to player pos
       else if (number == 4)
        {
             offset = new Vector3(player.transform.position.x + GetPositionOffsetX(-.5f,.51f), transform.position.y);
            chosenPrefab = rock;
            pos = transform.position + offset;
            prefabParent = rockParent.transform;
        }
        //Money random pos
        else if (number == 5)
        {
            
            positionOffsetX = Random.Range(-1.7f, 1.71f);
             offset = new Vector3(transform.position.x + positionOffsetX, transform.position.y);
            chosenPrefab = money;
            pos = transform.position + offset;
            prefabParent = moneyParent.transform;
            rockCount = 0;
            moneyCount++;
        }
        //rock duplicater
        if (chosenPrefab == rock)
        {
            if(Random.Range(1, 5) == 1)
            {
                while( offset.x < -.2f || offset.x > .2f)
                offset = new Vector3(Random.Range(-1.5f, 1.5f), Random.Range(1.1f,3));
                positionOffsetX = offset.x;
                Instantiate(chosenPrefab, pos+offset, Quaternion.identity, prefabParent);
            }
            rockCount++;
            moneyCount = 0;
        }

        //Spawn object
        Instantiate(chosenPrefab, pos, Quaternion.identity, prefabParent);


        //was offset positive?
        if(positionOffsetX >= 0)
        {
            wasPositive = true;
        }
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
            waitTime =- .05f;
            count++;
        }
        waitTime = defaultWaitTime - reduceWaitTime;
        if (waitTime < .3f) { waitTime = .3f; }
        Debug.Log(waitTime);
        return waitTime;
    }
   public void ShortenWaitTime()
    {
        reduceWaitTime += 0.005f;
    }

    public float GetPositionOffsetX(float min,float max)
    {
        while (Mathf.Abs(lastPositionOffset - positionOffsetX) <= .15f)
        {
                positionOffsetX = Random.Range(min, max);
        }
        lastPositionOffset = positionOffsetX;
        return positionOffsetX;
        
    }
}
