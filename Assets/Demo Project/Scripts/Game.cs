using UnityEngine;
using GamePush;

public class Game : MonoBehaviour
{
    private void Start()
    {
        GP_Game.GameReady();
    }
    
    public void GameReady()
    {
        GP_Game.GameReady();
        Debug.Log("GAME: READY");
    }
}
