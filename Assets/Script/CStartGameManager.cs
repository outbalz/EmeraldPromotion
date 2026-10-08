using UnityEngine;

public class CStartGameManager : MonoBehaviour
{
    private void Awake()
    {
        Time.timeScale = 0;
    }

    public void GameStart()
    {
        Time.timeScale = 1;
    }
}
