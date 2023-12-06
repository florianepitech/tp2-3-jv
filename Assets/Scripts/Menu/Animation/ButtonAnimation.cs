using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonEasingAnimation: MonoBehaviour
{
    public float duration = 0.5f; // Durée de l'animation
    public Vector3 hoverScale = new Vector3(1.1f, 1.1f, 1.1f); // Échelle lors du survol

    private Vector3 _originalScale;
    
    public Color defaultColor = new Color(1f, 1f, 1f); // Couleur par défaut du bouton
    public Color hoverColor = new Color(0.5f, 1f, 0.5f); // Couleur lors du survol du bouton

    private AudioSource _audioSource;
    public AudioClip audioClip;
    
    private void Start()
    {
        _audioSource = gameObject.AddComponent<AudioSource>();
        ChangeColorButton(defaultColor);
        _originalScale = transform.localScale;
        // Add Event triggers component to the button
        var eventTrigger = gameObject.AddComponent<EventTrigger>();
        // Add PointerEnter event listener
        var pointerEnter = new EventTrigger.Entry {eventID = EventTriggerType.PointerEnter};
        pointerEnter.callback.AddListener((data) => { OnMouseEnter(); });
        eventTrigger.triggers.Add(pointerEnter);
        // Add PointerExit event listener
        var pointerExit = new EventTrigger.Entry {eventID = EventTriggerType.PointerExit};
        pointerExit.callback.AddListener((data) => { OnMouseExit(); });
        eventTrigger.triggers.Add(pointerExit);
    }

    public void OnMouseEnter()
    {
        StopAllCoroutines(); // Arrête toute animation en cours
        StartCoroutine(AnimateScale(hoverScale, duration));
        // Change color of button to light green
        ChangeColorButton(hoverColor);
        PlaySound();
    }

    public void OnMouseExit()
    {
        StopAllCoroutines(); // Arrête toute animation en cours
        StartCoroutine(AnimateScale(_originalScale, duration));
        // Change color of button to white
        ChangeColorButton(defaultColor);
    }

    private void PlaySound()
    {
        // Load the sound in Assets/Musics/click.wav
        _audioSource.clip = audioClip;
        _audioSource.loop = false;
        _audioSource.volume = (float)MusicVolume.getMusicVolume(MusicType.VFX) / 100;
        _audioSource.Play();
    }
    
    IEnumerator AnimateScale(Vector3 target, float duration)
    {
        Vector3 startScale = transform.localScale;
        float time = 0;

        while (time < duration)
        {
            transform.localScale = Vector3.Lerp(startScale, target, time / duration);
            time += Time.deltaTime;
            yield return null;
        }

        transform.localScale = target; // Assurez-vous que l'échelle est exactement la cible à la fin
    }

    private void ChangeColorButton(Color color)
    {
        GetComponent<Image>().color = color;
    }
    
}