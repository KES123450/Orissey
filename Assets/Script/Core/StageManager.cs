using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageManager : MonoBehaviour
{
    private static StageManager instance;
    private PlayerController player;
    public PlayerController Player => player;
    public static StageManager Instance
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

        player = GameObject.Find("Player").GetComponent<PlayerController>();

    }
}
