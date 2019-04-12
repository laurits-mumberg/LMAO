using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmojiSpawn : MonoBehaviour
{

    public GameObject curEmoji;
    public float EmojiCooldown;
    private float EmojiTimer;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (EmojiTimer <= 0)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                Instantiate(curEmoji, transform);
                EmojiTimer = EmojiCooldown;
            }
        }
        else
        {
            EmojiTimer -= Time.deltaTime;
        }
    }
}