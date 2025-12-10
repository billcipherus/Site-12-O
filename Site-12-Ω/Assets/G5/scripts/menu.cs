using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.SceneManagement;

public class menu : MonoBehaviour
{
    [SerializeField] GameObject menuButtons;
    [SerializeField] GameObject choiseButtons;
    [SerializeField] GameObject soldierProtocol;
    [SerializeField] GameObject scientistProtocol;
    [SerializeField] GameObject settingsButtons;
    public void startGame()
    {
        menuButtons.SetActive(false);
        choiseButtons.SetActive(true);
        soldierProtocol.SetActive(false);
        scientistProtocol.SetActive(false);
    }

    public void soldierChoisen()
    {
        choiseButtons.SetActive(false);
        soldierProtocol.SetActive(true);
    }
    public void scientistChoisen()
    {
        choiseButtons.SetActive(false);
        scientistProtocol.SetActive(true);
    }

    public void settings()
    {
        menuButtons.SetActive(false);
        settingsButtons.SetActive(true);
    }

    public void returnBackMenu()
    {
        menuButtons.SetActive(true);
        settingsButtons.SetActive(false);
        choiseButtons.SetActive(false);     
    }

    public void startRealGame()
    {
        SceneManager.LoadScene(0);
    }
}
