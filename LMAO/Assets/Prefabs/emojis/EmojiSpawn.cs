using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmojiSpawn : MonoBehaviour
{

    public GameObject[] emoji;

    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponentInParent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            int curEmoji = animator.GetInteger("EmojiChange");
            SpawnEmoji(curEmoji);
        }
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