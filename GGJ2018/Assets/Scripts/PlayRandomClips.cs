using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayRandomClips : MonoBehaviour
{
    [SerializeField]
    private Object[] clipsList;

    private void Awake()
    {
        //load all the music in the folder specified in parameter\\
        GetComponent<AudioSource>().clip = clipsList[0] as AudioClip;
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        GetComponent<AudioSource>().Play();	
	}
    
	private void Update()
    {
		if (!GetComponent<AudioSource>().isPlaying)
        {
            playRandomClip();
        }
	}

    private void playRandomClip()
    {
        GetComponent<AudioSource>().clip = clipsList[Random.Range(0, clipsList.Length)] as AudioClip;
        GetComponent<AudioSource>().Play();
    }
}
