using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    private PlayerController player;
    public PlayerController Player => player;
    public static GameManager Instance
    {
        get
        {
            if (instance == null)
                return null;
            return instance;
        }
    }
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            if (instance != this) {
                Destroy(this.gameObject);
            }
        }
            
    }

    private void Start()
    {
        player = GameObject.Find("Player").GetComponent<PlayerController>();
    }
}
