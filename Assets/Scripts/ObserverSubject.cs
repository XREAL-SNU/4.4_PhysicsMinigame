public interface Subject
{
    void GameStateNotify();
    void RoundNotify();
}
public interface Observer
{
    void GameStateUpdate(GameManager.GameState gameState);
    void RoundUpdate(int round);
}

public interface MosquitoObserver : Observer
{
    void IsEndUpdate();
}