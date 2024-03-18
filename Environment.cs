
class Environment
{
    public int boardSize;
    public Reward[] rewards;


    public Environment(int boardSize, Reward[] rewards)
    {
        this.boardSize = boardSize;
        this.rewards= rewards;

    }


    public float getRewardValue(State state){
        float reward;
        try
        {
            reward = rewards.First(Q => Q.state == state).reward;
        }
        catch 
        {
            reward = 0;
        }
        return reward;
    }

    
}
