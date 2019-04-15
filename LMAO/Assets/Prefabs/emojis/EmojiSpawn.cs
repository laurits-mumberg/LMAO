using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmojiSpawn : MonoBehaviour
{

    public GameObject[] emoji;
    public float EmojiCooldown;
    private float EmojiTimer;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
    }

    public void SpawnEmoji(int selectedEmoji)
    {
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }

        Instantiate(emoji[selectedEmoji], transform);
    }
}