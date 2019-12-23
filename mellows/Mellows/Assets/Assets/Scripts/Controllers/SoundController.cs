using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SoundEffectIDs {
    CHARACTER_ATTACK=0,
    CHARACTER_ATTACK2=1,
    CHARACTER_DAMAGE=2,
    CHARACTER_DAMAGE2=3,
    CHARACTER_DISCOVERYSPARKLE=4,
    CHARACTER_SELECTATTACK=5,
    CHARACTER_SELECTTARGET=6,
    CHARACTER_MATCHENDS=7,
    CHARACTER_MATCHSTARTS=8,

}
public class SoundController : Singleton<SoundController>
{
    /// <summary>
    /// The theme song audio source
    /// </summary>
    public AudioSource ThemeSong;
    /// <summary>
    /// All the sound effects audio sources
    /// </summary>
    /// <typeparam name="AudioSource"></typeparam>
    public List <AudioSource> SoundEffects = new List<AudioSource>();

    /// <summary>
    /// Plays the theme song
    /// </summary>
    public void Start(){
        // ThemeSong.Play();
    }

    /// <summary>
    /// Plays the sound effect once.
    /// </summary>
    /// <param name="index"></param>
    public void PlaySoundEffect(SoundEffectIDs index){
        SoundEffects[(int)index].Play();
    }
}
