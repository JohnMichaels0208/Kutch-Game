using System;
public class IsFullScreenOption : BoolOption
{
    private void OnEnable()
    {
        SetValue(SaveSystemScript.LoadOptionData().isFullScreen);
    }

    private void Start()
    {
        Init();
    }

    protected override void OnApplyOption(object sender, EventArgs eventArgs)
    {
        OptionPanelScript.instance.optionDataToSave.isFullScreen = castedValue;
    }
}
