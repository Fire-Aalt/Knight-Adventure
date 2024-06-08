using RenderDream.GameEssentials;

namespace Game
{
    public class GameData : GameDataModel
    {
        public float health;
        public float score;

        public GameData() 
        {
            health = 5f;
            score = 0;
        }
    }
}
