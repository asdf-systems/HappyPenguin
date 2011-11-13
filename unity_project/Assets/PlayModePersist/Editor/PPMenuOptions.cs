using UnityEditor;
using UnityEngine;
class PPMenuOptions : MonoBehaviour
{
    // Add menu named "Do Something" to the main menu
    [MenuItem("Edit/PlayModePersist - Open Dropdown &#o")]
    static void OpenDropdown()
    {
        PPInspector.OpenCloseDropdown();
    }

    // Validate the menu item.
    // The item will be disabled if no transform is selected.
    [MenuItem("Edit/PlayModePersist - Open Dropdown &#o", true)]
    static bool ValidateOpenDropdown()
    {
        return (EditorApplication.isPlaying || EditorApplication.isPaused) && Selection.activeTransform != null;
    }

    // Add menu named "Do Something" to the main menu
    [MenuItem("Edit/PlayModePersist - Persist Selected GameObject &#p")]
    static void PersistSelectedGameObject()
    {
        PPInspector.PersistSelectedGameObject();
    }

    // Add menu named "Do Something" to the main menu
    [MenuItem("Edit/PlayModePersist - Persist Selected GameObject &#p",true)]
    static bool ValidatePersistSelectedGameObject()
    {
        return (EditorApplication.isPlaying || EditorApplication.isPaused) && Selection.activeTransform != null;
    }
}