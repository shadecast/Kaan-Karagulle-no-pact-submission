using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    PlayableCharacter player;

    private void Awake()
    {
        player = FindObjectOfType<PlayableCharacter>();    
    }
    void Update()
    {
        if (player != null)
        {
            gameObject.transform.position = new Vector3(player.transform.position.x,
                                                        player.transform.position.y + 1.5f,
                                                        gameObject.transform.position.z);
        }
    }
}
