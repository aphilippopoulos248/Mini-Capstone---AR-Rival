using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "ButtonHandler", menuName = "Scriptable Objects/ButtonHandler")]
public class ButtonHandler : ScriptableObject
{
    public void OnClick_PlayGame()
    {
        SceneManager.LoadScene("MapScene");
    }
}
