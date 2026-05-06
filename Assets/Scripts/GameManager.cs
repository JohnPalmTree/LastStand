using UnityEngine;

public class GameManager : MonoBehaviour
{
    public WaveSystem waveSystem;

    public enum GameState { Playing, GameOver };
    public GameState state = GameState.Playing;
    
    public void OnPlayerDied()
    {
        if (state == GameState.GameOver) return;

        state = GameState.GameOver;
        Debug.Log("Game Over - Wave Reached: "  + waveSystem.roundNumber);

        Time.timeScale = 0f;

        // screen and replayability maybe?
    }
}
