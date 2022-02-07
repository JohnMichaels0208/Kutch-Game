[System.Serializable]
public class GameCharacter
{
    public char label;
    public UnityEngine.KeyCode keyCode;
    public GameCharacter(char label, UnityEngine.KeyCode keyCode)
    {
        this.label = label;
        this.keyCode = keyCode;
    }
}
