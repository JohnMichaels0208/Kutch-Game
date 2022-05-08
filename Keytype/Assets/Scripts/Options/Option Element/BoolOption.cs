using UnityEngine.UI;
public abstract class BoolOption : OptionBase
{
    private Toggle toggle { get { return GetComponent<Toggle>(); } }

    public override object value { get; protected set; }

    public bool castedValue { get { return (bool)value; } }

    public override void OnValueChanged()
    {
        value = toggle.isOn;
    }

    public override void SetValue(object value)
    {
        base.SetValue(value);
        toggle.isOn = castedValue;
    }
}
