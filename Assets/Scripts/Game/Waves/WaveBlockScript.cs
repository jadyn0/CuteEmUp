using UnityEngine;

public class WaveBlockScript : MonoBehaviour
{
    public ExplosionScript explosion;
    public WaveController waveController;
    public AudioClip destroySound;
    public void Hit()
    {
        SoundFXManager.instance.PlaySoundFXClip(destroySound, transform, 1f);
        waveController.StartWaves();
        ExplosionScript newExplosion = Instantiate(explosion, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
