using UnityEngine;
using UnityEngine.UI;

public class Option : MonoBehaviour
{
    [SerializeField] private GameObject volumeSliderGameObject;
    private Slider volumeSliderComponent;

    public float volume = 0;

    public void SaveOption()
    {
        SaveSystemScript.SaveOption(this);
    }

    public void LoadOption()
    {
        OptionData optionData = SaveSystemScript.LoadOption();
        this.volume = optionData.volume;
    }

    private void OnEnable()
    {
        LoadOption();
        volumeSliderComponent = volumeSliderGameObject.GetComponent<Slider>();
        volumeSliderComponent.value = volume;
    }

    public void UpdateOptionVolume()
    {
        volume = volumeSliderComponent.value;
    }
}