using UnityEngine;

public class OptionPanelScript : MonoBehaviour
{
    public enum OptionValueType
    {
        toggle, 
        slider, 
        dropdown
    }
    [SerializeField] private GameObject option;
    [SerializeField] private GameObject panel;
    [Header("Option value type references")]
    [SerializeField] private GameObject optionSlider;
    [SerializeField] private GameObject optionDropdown;
    [SerializeField] private GameObject optionToggle;
    [Header("Option to create settings")]
    public OptionValueType optionValueType;

    public delegate void ApplyOptionEventHandler(object sender, System.EventArgs eventArgs);
    public event ApplyOptionEventHandler applyOptionEvent;

    public static OptionPanelScript instance { get; private set; }

    public OptionData optionDataToSave;
    private void Awake()
    {
        optionDataToSave = SaveSystemScript.LoadOptionData();

        // If there is an instance, and it's not me, delete myself.

        if (instance != null && instance != this)
        {
            Destroy(this);
        }
        else
        {
            instance = this;
        }
    }

    public void CreateOption()
    {
        GameObject instantiatedOption = Instantiate(option, panel.transform);
        switch (optionValueType)
        {
            case OptionValueType.toggle:
                Instantiate(optionToggle, instantiatedOption.transform);
                break;
            case OptionValueType.slider:
                Instantiate(optionSlider, instantiatedOption.transform);
                break;
            case OptionValueType.dropdown:
                Instantiate(optionDropdown, instantiatedOption.transform);
                break;
        }
    }

    public void OnApplyOption()
    {
        RaiseApplyOptionEvent();
        SaveSystemScript.SaveOption(optionDataToSave);
    }

    protected virtual void RaiseApplyOptionEvent()
    {
        applyOptionEvent?.Invoke(this, System.EventArgs.Empty);
    }
}