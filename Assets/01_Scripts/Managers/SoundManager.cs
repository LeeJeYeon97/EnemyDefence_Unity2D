using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;

    [Header("BGM")]
    public AudioClip[] bgmClips;
    
    public float bgmVolume;
    AudioSource bgmPlayer;
    AudioHighPassFilter highPassFilter;

    [Header("SFX")] // ȿ����
    public AudioClip[] sfxClips;
    
    public float sfxVolume;
    public int channels;
    AudioSource[] sfxPlayers;
    int channelIndex; // �� �������� �����ߴ� �����Source

    private void Awake()
    {
        instance = this;
        Init();
    }

    private void Init()
    {
        // ����� �÷��̾� �ʱ�ȭ
        GameObject bgmObject = new GameObject("BgmPlayer");
        bgmObject.transform.SetParent(transform);

        bgmPlayer = bgmObject.AddComponent<AudioSource>();
        bgmPlayer.playOnAwake = false;
        bgmPlayer.loop = true;
        bgmPlayer.volume = bgmVolume;

        // ȿ���� �÷��̾� �ʱ�ȭ
        GameObject sfxObject = new GameObject("SfxPlayer");
        sfxObject.transform.SetParent(transform);
        sfxPlayers = new AudioSource[channels];

        for(int i = 0; i < sfxPlayers.Length; i++)
        {
            sfxPlayers[i] = sfxObject.AddComponent<AudioSource>();
            sfxPlayers[i].playOnAwake = false;
            sfxPlayers[i].loop = false;
            sfxPlayers[i].bypassListenerEffects = true;
            sfxPlayers[i].volume = sfxVolume;
        }
        SoundLoad();

        DontDestroyOnLoad(gameObject);
    }
    private void SoundLoad()
    {
        int length = Enum.GetValues(typeof(Define.Sfx)).Length;
        sfxClips = new AudioClip[length];
        for(int i = 0; i < length; i++)
        {
            string clipName = Enum.GetName(typeof(Define.Sfx), i);
            AudioClip clip = Resources.Load<AudioClip>($"Sounds/Sfx/{clipName}");
            sfxClips[i] = clip;
        }

        length = Enum.GetValues(typeof(Define.Bgm)).Length;
        bgmClips = new AudioClip[length];
        for(int i = 0; i< length; i++)
        {
            string clipName = Enum.GetName(typeof(Define.Bgm), i);
            AudioClip clip = Resources.Load<AudioClip>($"Sounds/Bgm/{clipName}");
            bgmClips[i] = clip;
        }
    }
    public void PlaySfx(Define.Sfx sfx,float volume = 1.0f)
    {

        for(int i = 0; i < sfxPlayers.Length; i++)
        {
            int loopIndex = (i + channelIndex) % sfxPlayers.Length;

            if (sfxPlayers[loopIndex].isPlaying)
                continue;

            channelIndex = loopIndex;
            sfxPlayers[loopIndex].clip = sfxClips[(int)sfx];
            sfxPlayers[loopIndex].volume = volume;
            sfxPlayers[loopIndex].Play();
            break;
        }
    }
    public void StopBGM()
    {
        bgmPlayer.Stop();
    }
    public void PlayBgm(bool isPlay, Define.Bgm bgm = Define.Bgm.LobbyBgm)
    {
        if(isPlay)
        {
            bgmPlayer.clip = bgmClips[(int)bgm];
            bgmPlayer.Play();
        }
        else
        {
            bgmPlayer.Stop();
        }
    }
    public void FilterBgm(bool isPlay)
    {
        highPassFilter = Camera.main.GetComponent<AudioHighPassFilter>();
        highPassFilter.enabled = isPlay;
    }
}
