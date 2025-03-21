using UnityEngine;

public class TestButtons2 : MonoBehaviour
{
    public void PlaySFX(string name)
    {
        AudioManager2.Instance.PlaySFX(name);
    }

    public void PlayRandomSFX()
    {
        // Randomly selects a sound numbered between 1 and 50
        string name = Random.Range(1, 51).ToString("00");
        AudioManager2.Instance.PlaySFX(name);
    }

    public void PlayAllSFX()
    {
        // Play all 50 sounds
        AudioManager2.Instance.PlayAllSFX();
    }
}