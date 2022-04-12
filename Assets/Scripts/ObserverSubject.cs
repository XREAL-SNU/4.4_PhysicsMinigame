public interface Subject
{
    void GameStateNotify();
}
public interface Observer
{
    void GameStateUpdate(GameManager.GameState gameState);
}