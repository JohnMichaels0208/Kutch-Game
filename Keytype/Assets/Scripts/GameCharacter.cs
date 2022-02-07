
public class GameCharacter
{
    public readonly char label;
    public readonly UnityEngine.KeyCode keyCode;
    public GameCharacter(char label, UnityEngine.KeyCode keyCode)
    {
        this.label = label;
        this.keyCode = keyCode;
    }
}
