using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static AudioClip shoot, dizzy, melee, death, move;
    static AudioSource audioSource;
    // Start is called before the first frame update
    void Start()
    {
        shoot = Resources.Load<AudioClip>("shoot");
        dizzy = Resources.Load<AudioClip>("dizzy");
        melee = Resources.Load<AudioClip>("melee");
        death = Resources.Load<AudioClip>("death");
        move = Resources.Load<AudioClip>("move");
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static void PlaySound(string clip){
        switch(clip){
            case "shoot":
            audioSource.PlayOneShot(shoot);
            break;
            case "dizzy":
            audioSource.PlayOneShot(dizzy);
            break;
            case "melee":
            audioSource.PlayOneShot(melee);
            break;
            case "death":
            audioSource.PlayOneShot(death);
            break;
            case "move":
            audioSource.PlayOneShot(move);
            break;
        }
    }
}
