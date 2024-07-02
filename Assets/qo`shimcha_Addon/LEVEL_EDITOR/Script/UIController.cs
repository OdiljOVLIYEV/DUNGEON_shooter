using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public LevelEditor levelEditor;

    public void SelectObject(int index)
    {
        levelEditor.SetCurrentObjectIndex(index);
    }
}