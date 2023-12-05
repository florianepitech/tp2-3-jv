using UnityEngine;
using UnityEngine.UI;

public class MusicSlider : MonoBehaviour
{
    public MusicType musicType;
    public Slider slider;
    
    // Start is called before the first frame update
    void Start()
    {
        // Add Listener to the slider
        slider.onValueChanged.AddListener(OnSliderValueChanged);
        if (!PlayerPrefs.HasKey(musicType.ToString()))
        {
            MusicValue.setMusicVolume(musicType, 100);
            Debug.Log("Music volume for " + musicType + " music is " + MusicValue.getMusicVolume(musicType));
        }
        var volume = MusicValue.getMusicVolume(musicType);
        slider.value = volume;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    private void OnSliderValueChanged(float value)
    {
        Debug.Log("Slider value changed to " + value + " for " + musicType + " music");
        MusicValue.setMusicVolume(musicType, (int) value);
    }
    
}
