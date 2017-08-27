using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicController : MonoBehaviour
{

    public int battleCounter;
    public bool isBattle;
    private AudioClip activeClip;
    private AudioSource audioSource;
    private int level;
    private bool start;
    public AudioClip[] levelTracks;
    public AudioClip[] battleTracks;



    void Awake()
    {
        DontDestroyOnLoad(transform.gameObject);
    }

    private void OnLevelWasLoaded(int level)
    {
        this.level = level;
        start = true;
    }

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void FixedUpdate()
    {
        if (!isBattle && battleCounter > 0)
        {
            isBattle = true;
            audioSource.clip = battleTracks[Random.Range(0, battleTracks.Length)];
            audioSource.Play();

        }
        else if (isBattle && battleCounter == 0)
        {
            isBattle = false;
            audioSource.clip = levelTracks[level - 1];
            audioSource.Play();
        }
        if (start)
        {
            audioSource.clip = levelTracks[level - 1];
            audioSource.Play();
            start = false;
        }
    }

    public void SetBattle()
    {
        battleCounter++;
    }

    public void EndBattle()
    {
        if (battleCounter > 0)
        {
            battleCounter--;
        }
    }
}
