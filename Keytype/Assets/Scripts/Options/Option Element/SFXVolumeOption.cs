using System;

public class SFXVolumeOption : FloatOption
{
    private void OnEnable()
    {
        SetValue(SaveSystemScript.LoadOptionData().soundEffectsVolume);
    }

    private void Start()
    {
        Init();
    }

    protected override void OnApplyOption(object sender, EventArgs eventArgs)
    {
        OptionPanelScript.instance.optionDataToSave.soundEffectsVolume = castedValue;
    }
}
