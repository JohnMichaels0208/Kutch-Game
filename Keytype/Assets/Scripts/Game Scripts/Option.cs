using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class Option : MonoBehaviour
{
    [SerializeField] private GameObject volumeSliderGameObject;
    private Slider volumeSliderComponent;

    public float volume = 1;

    public void SaveOption()
    {
        Debug.Log("Saving");
        SaveSystemScript.SaveOption(this);
    }

    public void LoadOption()
    {
        OptionData optionData = SaveSystemScript.LoadOption();
        this.volume = optionData.volume;
    }

    private void OnEnable()
    {
        if (!File.Exists(Application.persistentDataPath + "/savefile.json"))
        {
            SaveOption();
        }
        LoadOption();
        volumeSliderComponent = volumeSliderGameObject.GetComponent<Slider>();
        volumeSliderComponent.value = volume;
    }

    public void UpdateOptionVolume()
    {
        volume = volumeSliderComponent.value;
    }
}