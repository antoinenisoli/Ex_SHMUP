using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEditor;
using UnityEngine;

public class Sc_SoundManager : MonoBehaviour
{
    [Serializable]
    public class GlobalAudio
    {
        public Dictionary<string, AudioClip> allSounds = new Dictionary<string, AudioClip>();
        public List<AudioClip> explosionSounds;
        public List<AudioClip> hurtSounds;
        public List<AudioClip> shootSounds;
        public List<AudioClip> otherSounds;
        public List<AudioClip> musics;
        public List<AudioClip> bipSounds;

        public void Init()
        {
            FillList(explosionSounds);
            FillList(hurtSounds);
            FillList(shootSounds);
            FillList(otherSounds);
            FillList(musics);
            FillList(bipSounds);
        }

        public void FillList(List<AudioClip> list)
        {
            foreach (AudioClip clip in list)
            {
                if (!allSounds.ContainsValue(clip))
                    allSounds.Add(clip.name, clip);
            }
        }

        #if UNITY_EDITOR

        public void RenameFiles(List<AudioClip> list, string folderName = "Assets/Audio/Explosions/", string newName = "Example0")
        {
            for (int i = 0; i < list.Count; i++)
            {
                //Debug.Log(AssetDatabase.RenameAsset(folderName + list[i].name + ".wav", newName + i));
                AssetDatabase.RenameAsset(folderName + list[i].name + ".wav", newName + i);
            }
        }
        #endif
    }

    public static Sc_SoundManager Instance;
    [SerializeField] GlobalAudio soundsData;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }

        soundsData.Init();
    }

    bool ContainSounds(AudioClip clip, out AudioSource outSource)
    {
        outSource = null;
        AudioSource[] sources = GetComponentsInChildren<AudioSource>();
        bool b = false;

        foreach (AudioSource source in sources)
        {
            if (source.clip == clip)
            {
                b = true;
                outSource = source;
                break;
            }
        }

        return b;
    }

    public void PlaySound(string soundKey, float volume, float pitch)
    {
        if (!soundsData.allSounds.ContainsKey(soundKey))
            return;

        AudioSource sourceToPlay;
        if (ContainSounds(soundsData.allSounds[soundKey], out AudioSource checkSource))
        {
            sourceToPlay = checkSource;
        }
        else
        {
            sourceToPlay = gameObject.AddComponent<AudioSource>();           
        }

        sourceToPlay.clip = soundsData.allSounds[soundKey];
        sourceToPlay.volume = volume;
        sourceToPlay.pitch = pitch;
        sourceToPlay.Play();
    }

    #if UNITY_EDITOR
    #region Rename
    [ContextMenu("Rename explosion sounds")]
    void RenameExplosions()
    {
        print("rename explosions !");
        soundsData.RenameFiles(soundsData.explosionSounds, "Assets/Audio/Explosions/", "Explosion0");
    }

    [ContextMenu("Rename hurt sounds")]
    void RenameHurts()
    {
        print("rename hurt !");
        soundsData.RenameFiles(soundsData.hurtSounds, "Assets/Audio/Hurts/", "Hurt0");
    }

    [ContextMenu("Rename shoot sounds")]
    void RenameShoots()
    {
        print("rename shoot !");
        soundsData.RenameFiles(soundsData.shootSounds, "Assets/Audio/Shoots/", "Shoot0");
    }

    [ContextMenu("Rename musics")]
    void RenameMusics()
    {
        print("rename musics !");
        soundsData.RenameFiles(soundsData.musics, "Assets/Audio/Musics/", "Music0");
    }

    [ContextMenu("Rename bips")]
    void RenameBips()
    {
        print("rename bips !");
        soundsData.RenameFiles(soundsData.bipSounds, "Assets/Audio/Bips/", "Bip0");
    }
    #endregion
    #endif
}
