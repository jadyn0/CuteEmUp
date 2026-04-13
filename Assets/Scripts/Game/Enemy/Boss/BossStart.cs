using UnityEngine;

public class BossStart : MonoBehaviour
{
    public GameObject bossHealthBar;
    public AudioClip bossSpawn;
    public AudioClip bossMusic;
    void Start()
    {
        MusicManager.instance.playMusic(bossMusic, 1f);
        SoundFXManager.instance.PlaySoundFXClip(bossSpawn, transform, 1f);
        bossHealthBar.SetActive(true);
    }
}
