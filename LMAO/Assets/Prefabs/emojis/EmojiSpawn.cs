﻿using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmojiSpawn : MonoBehaviourPun
{

    public GameObject[] emoji;

    private Animator animator;

    public int curEmoji;

    public Transform emojiSpawnPoint;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponentInParent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!photonView.IsMine)
        {
            return;
        }

        if (Input.GetMouseButtonDown(1))
        {
            photonView.RPC("SpawnEmoji", RpcTarget.All, GetComponent<BallLook>().emojiNumber);
        }
    }

    [PunRPC]
    public void SpawnEmoji(int selectedEmoji)
    {
        foreach (Transform child in emojiSpawnPoint.transform)
        {
            Destroy(child.gameObject);
        }

        Instantiate(emoji[selectedEmoji], emojiSpawnPoint);
    }

}