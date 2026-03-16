using UnityEngine;

public class WaveBlockScript : MonoBehaviour
{
    public ExplosionScript explosion;
    public WaveController waveController;
    public void Hit()
    {
        waveController.StartWaves();
        ExplosionScript newExplosion = Instantiate(explosion, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
