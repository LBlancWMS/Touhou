using UnityEngine;
using UnityEngine.UI;

public class musicManager : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip[] musicTracks;
 //   private Slider musicSlider;
    private int currentTrackIndex = 0;
    private static musicManager instance;

    void Awake()
    {
        audioSource = GameObject.Find("musicManager").GetComponent<AudioSource>();
     //   musicSlider = GameObject.Find("musicSlider").GetComponent<Slider>();
     //   musicSlider.value = audioSource.volume;
        // if (instance == null)
        // {
        //     instance = this;
        //     DontDestroyOnLoad(this.gameObject);
        // }
        // else if (instance != this)
        // {
        //     Destroy(gameObject);
        //     return;
        // }
        PlayNextTrack();
    }

    public void setVolume()
    {
      //   audioSource.volume = musicSlider.value;
    }

    private void Update()
    {
        if (!audioSource.isPlaying)
        {
            PlayNextTrack();
        }
    }

    private void PlayNextTrack()
    {
        if (musicTracks.Length == 0)
        {
            Debug.LogError("No music tracks are assigned.");
            return;
        }

        audioSource.clip = musicTracks[currentTrackIndex];
        audioSource.Play();

        currentTrackIndex = (currentTrackIndex + 1) % musicTracks.Length;
    }
}
