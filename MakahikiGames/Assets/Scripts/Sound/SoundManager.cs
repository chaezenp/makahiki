using System.Collections;
using UnityEngine;

public enum SoundType
{
    None,
    SPEARTHROW,
    SPEARWIND,
    SPEARHIT,
    SPEARMISS,
    WIN,
    LOSE,
    BACKGROUND,
    OPENWORLD,
    WavesSFX,
    SPEARSPLASH
}

public class SoundManager : MonoBehaviour
{
    [SerializeField] private AudioClip[] soundList;
    private static SoundManager instance;

    private AudioSource sfxSource;
    private AudioSource musicSource;
    private AudioSource sfxLoopSource; 


    // Volume controls
    public static float MasterVolume = 1f;
    public static float MusicVolume = 1f;
    public static float SFXVolume = 1f;
    public static float GetMasterVolume() => MasterVolume;
    public static float GetMusicVolume() => MusicVolume;
    public static float GetSFXVolume() => SFXVolume;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            MasterVolume = PlayerPrefs.GetFloat("MasterVolume", 0.5f);
            MusicVolume = PlayerPrefs.GetFloat("MusicVolume", 0.4f);
            SFXVolume = PlayerPrefs.GetFloat("SFXVolume", 0.5f);
            Debug.Log("Volume values: " + MasterVolume + " " + MusicVolume + " " + SFXVolume);
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        sfxSource = gameObject.AddComponent<AudioSource>();
        musicSource = gameObject.AddComponent<AudioSource>();
        sfxLoopSource = gameObject.AddComponent<AudioSource>();

        musicSource.loop = true;
        sfxLoopSource.loop = true;
        //musicSource.playOnAwake = false;

        ApplyMusicVolume();
        ApplySFXVolume(); // Initial volume setup
    }
    public static void Initialize()
    {
        if (instance == null) return;

        MasterVolume = PlayerPrefs.GetFloat("MasterVolume", 1f);
        MusicVolume = PlayerPrefs.GetFloat("MusicVolume", 1);
        SFXVolume = PlayerPrefs.GetFloat("SFXVolume", 1);
        ApplyMusicVolume();
        ApplySFXVolume();
}

public static void SetMasterVolume(float volume)
{
    MasterVolume = volume;
    PlayerPrefs.SetFloat("MasterVolume", volume);
    ApplyMusicVolume();
    ApplySFXVolume();
}

public static void SetMusicVolume(float volume)
{
    MusicVolume = volume;
    PlayerPrefs.SetFloat("MusicVolume", volume);
    Debug.Log("Music Vol: " + volume);
    ApplyMusicVolume();
}

public static void SetSFXVolume(float volume)
{
    SFXVolume = volume;
    PlayerPrefs.SetFloat("SFXVolume", volume);
    Debug.Log("SFX Vol: " + volume);
    ApplySFXVolume();
}


    private static void ApplyMusicVolume()
    {
        if (instance == null) return;

        instance.musicSource.volume = MusicVolume * MasterVolume;
    }
    private static void ApplySFXVolume()
    {
        if (instance == null) return;
        instance.sfxSource.volume = SFXVolume * MasterVolume;
        instance.sfxLoopSource.volume = SFXVolume * MasterVolume;
    }

    public static void PlayOneShot(SoundType sound, float volume = 1f)
    {
        AudioClip clip = instance.soundList[(int)sound];
        float finalVolume = volume * SFXVolume * MasterVolume;
        instance.sfxSource.PlayOneShot(clip, finalVolume);
    }
    public static void PlaySFXLoop(SoundType sound, float volume = 1f)
{
    AudioClip clip = instance.soundList[(int)sound];

    if (instance.sfxLoopSource.clip == clip && instance.sfxLoopSource.isPlaying)
        return; // Already playing this loop

    instance.sfxLoopSource.clip = clip;
    instance.sfxLoopSource.volume = volume * SFXVolume * MasterVolume;
    instance.sfxLoopSource.Play();
}

public static void StopSFXLoop()
{
    instance.sfxLoopSource.Stop();
}

    public static void PlayBackgroundMusic(SoundType musicType = SoundType.BACKGROUND, float targetVolume = 0.5f)
    {
        AudioClip newClip = instance.soundList[(int)musicType];
        instance.musicSource.clip = newClip;
        instance.musicSource.volume = targetVolume * MusicVolume * MasterVolume;
        instance.musicSource.Play();
        Debug.Log("Gets Called");
    }

        public static void StopSound()
    {
        instance.sfxSource.Stop();
    }
    public static void BGMusicSofter(float targetVolume = 0.1f)
    {
        instance.musicSource.volume = targetVolume * MusicVolume * MasterVolume;
    }
    private Coroutine musicFadeCoroutine;

public static void PlayBackgroundMusicFade(SoundType musicType, float fadeOutTime = 1f, float fadeInTime = 1f, float targetVolume = 1f)
{
    if (instance.musicFadeCoroutine != null)
    {
        instance.StopCoroutine(instance.musicFadeCoroutine);
    }

    AudioClip newClip = instance.soundList[(int)musicType];
    float targetVolume1 = targetVolume * MusicVolume * MasterVolume;

    instance.musicFadeCoroutine = instance.StartCoroutine(
        instance.FadeMusicRoutine(newClip, fadeOutTime, fadeInTime, targetVolume1)
    );
}

    private IEnumerator FadeMusicRoutine(AudioClip newClip, float fadeOutTime, float fadeInTime, float targetVolume)
    {
        AudioSource music = musicSource;

        // Fade out current music
        if (music.isPlaying)
        {
            float startVolume = music.volume;
            float t = 0f;

            while (t < fadeOutTime)
            {
                t += Time.deltaTime;
                float volume = Mathf.Lerp(startVolume, 0f, t / fadeOutTime);
                music.volume = volume;
                yield return null;
            }

            music.Stop();
        }

        // Switch to new music
        music.clip = newClip;
        music.Play();

        // Fade in new music with respect to user-defined volumes
        float fadeTargetVolume = targetVolume * MusicVolume * MasterVolume;

        float fadeTime = 0f;
        while (fadeTime < fadeInTime)
        {
            fadeTime += Time.deltaTime;
            music.volume = Mathf.Lerp(0f, fadeTargetVolume, fadeTime / fadeInTime);
            yield return null;
        }

        // Ensure final volume is set exactly
        music.volume = fadeTargetVolume;
    }
}