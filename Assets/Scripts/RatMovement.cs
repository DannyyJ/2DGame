using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RatMovement : MonoBehaviour
{
    public Transform Player;
    public float Speed = 3f;
    // Start is called before the first frame update
    void Start()
    {
        
        Player = GameObject.Find("Player").transform;
        Debug.Log(Player);
    }
    void Update()
    {
        if (Player != null)
        {
            Vector3 direction = (Player.position - transform.position).normalized;
            transform.position += direction * Speed * Time.deltaTime;
        }
    }
}
