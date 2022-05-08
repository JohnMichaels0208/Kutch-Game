using UnityEngine.UI;
public abstract class FloatOption : OptionBase
{
    private Slider slider { get { return GetComponent<Slider>(); } }

    public override object value { get; protected set; }

    public float castedValue { get { return (float)value; } }

    public override void OnValueChanged()
    {
        value = slider.value;
    }

    public override void SetValue(object value)
    {
        base.SetValue(value);
        slider.value = castedValue;
    }
}
