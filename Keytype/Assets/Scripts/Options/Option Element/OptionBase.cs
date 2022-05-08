using UnityEngine;
public abstract class OptionBase : MonoBehaviour
{
    public abstract object value { get; protected set; }

    protected void Init()
    {
        OptionPanelScript.instance.applyOptionEvent += OnApplyOption;
    }

    public virtual void SetValue(object value)
    {
        this.value = value;
    }

    protected abstract void OnApplyOption(object sender, System.EventArgs eventArgs);

    public abstract void OnValueChanged();
}
