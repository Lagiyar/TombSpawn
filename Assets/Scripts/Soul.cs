using System;
using UnityEngine;

public class Soul : MonoBehaviour
{
    private int value;

    public static event Action<int> OnSoulCollected;
    [Header("Audio")]
    public AudioSource pickUpSound;
    private void Start()
    {
        GameObject sfxObject = GameObject.FindWithTag("PickUpSound");
        if (sfxObject != null)
        {
            pickUpSound = sfxObject.GetComponent<AudioSource>();
        }
    }
    public void SetValue(int newValue)
    {
        value = newValue;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (pickUpSound != null)
                pickUpSound.Play();
            OnSoulCollected?.Invoke(value);
            Destroy(gameObject);
        }
    }
}
