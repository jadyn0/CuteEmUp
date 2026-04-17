using UnityEngine;

public class WaveBlockScript : MonoBehaviour
{
    public ExplosionScript explosion;
    public WaveController waveController;
    public PvpWaveController pvpWaveController;
    public AudioClip destroySound;
    public bool isMultiplayer = false;
    public void Hit()
    {
        SoundFXManager.instance.PlaySoundFXClip(destroySound, transform, 1f);
        if (isMultiplayer)
        {
            pvpWaveController.StartWaves();
        }
        else
        {
            waveController.StartWaves();
        }
        ExplosionScript newExplosion = Instantiate(explosion, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
