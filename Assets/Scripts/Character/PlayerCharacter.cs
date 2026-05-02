using UnityEngine;

public class PlayerCharacter : MonoBehaviour
{
    public CharacterData Data;

    private void Start()
    {
        if (Data != null)
        {
            Debug.Log($"{Data.Name}, {Data.DefaultStats.Strength}");
        }
    }
}