using UnityEngine;

public class TestButtons : MonoBehaviour
{
    public void PlaySFX(string name)
    {
        AudioManager1.Instance.PlaySFX(name);
    }

    public void PlayRandomSFX()
    {
        // Randomly selects a sound numbered between 1 and 50
        string name = Random.Range(1, 51).ToString("00");
        AudioManager1.Instance.PlaySFX(name);
    }

    public void PlayAllSFX()
    {
        // Play all 50 sounds
        AudioManager1.Instance.PlayAllSFX();
    }
}
