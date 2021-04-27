using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Throw : MonoBehaviour
{
    public Transform spawnPoint;
    public GameObject ballPrefab; 

    public float throwCooldown = 3;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    void Shoot()
    {
        GameObject ball = GameObject.Instantiate<GameObject>(ballPrefab);
        ball.transform.position = spawnPoint.position;
        ball.transform.rotation = this.transform.rotation;
    }

    void OnEnable()
    {
        StartCoroutine(ThrowingCoroutine());
    }

    bool shooting = false;

    System.Collections.IEnumerator ThrowingCoroutine()
    {
        while(true)
        {
            if (Input.GetKeyUp(KeyCode.Space))
            {
                Shoot();
                yield return new WaitForSeconds(throwCooldown);
            }
            yield return null;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}