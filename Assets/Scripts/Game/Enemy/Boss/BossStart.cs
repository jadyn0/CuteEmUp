using UnityEngine;

public class BossStart : MonoBehaviour
{
    public GameObject bossHealthBar;
    void Start()
    {
        bossHealthBar.SetActive(true);
    }
}
