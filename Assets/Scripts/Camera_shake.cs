using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_shake : MonoBehaviour
{
    public bool start = false;
    public AnimationCurve curve;
    public float duration = 1f;
    private AudioSource audioPlayer;
    public AudioClip enemy_die;

    private void Start()
    {
        audioPlayer = GetComponent<AudioSource>();
    }
    private void Update() {
        if (start) {
            PlayEnemy_Die();
            start = false;
            StartCoroutine(Shaking());
        }
    }

    IEnumerator Shaking() { 
        Vector3 startPosition = transform.position;
        float elapsedTime = 0f;

        while (elapsedTime < duration) {
            elapsedTime += Time.deltaTime;
            float strength = curve.Evaluate(elapsedTime / duration);
            transform.position = startPosition + Random.insideUnitSphere/*(new Vector3(Random.Range(0f, 2f), Random.Range(0f, 2f), 0))*/ * strength;
            yield return null;
        }
        transform.position = startPosition;
    }


    public void PlayEnemy_Die()
    {
        audioPlayer.PlayOneShot(enemy_die);
    }
}