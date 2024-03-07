

class Agent
{


    public State currentPos;
    public QValue[] qvalues = [];

    float epsilon;
    float alpha;
    private Random randomizer = new Random();

    public Agent(State startPos, float alpha, float epsilon)
    {
        currentPos = startPos;
        this.alpha = alpha;
        this.epsilon = epsilon;
    }
    public void next(Environment env)
    {
        State newPos = new State(currentPos.X, currentPos.Y);

        float randomFloat = (float)randomizer.NextDouble();
        Action action = randomFloat < epsilon ? selectRandomAction() : selectAction();

        switch (action)
        {
            case Action.UP:
                newPos.Y = Math.Clamp(currentPos.Y + 1, 0, env.boardSize);
                break;
            case Action.DOWN:
                newPos.Y = Math.Clamp(currentPos.Y - 1, 0, env.boardSize);

                break;
            case Action.RIGHT:
                newPos.X = Math.Clamp(currentPos.X + 1, 0, env.boardSize);

                break;
            case Action.LEFT:
                newPos.X = Math.Clamp(currentPos.X - 1, 0, env.boardSize);
                break;
        }

        var incomingReward = env.getRewardValue(newPos);
        updateQState(currentPos, action, newQvalue(currentPos, newPos, action, incomingReward, alpha));
        currentPos = newPos;
    }
    Action selectAction()
    {
        Action action;
        try
        {
            action = qvalues.Where(QValue => QValue.state == currentPos).OrderByDescending(QValue => QValue.Q).First().action;
        }
        catch
        {
            action = selectRandomAction();
        }
        return action;


    }

    Action selectRandomAction()
    {

        Action[] values = (Action[])Enum.GetValues(typeof(Action));
        return values[randomizer.Next(values.Length)];

    }

    float newQvalue(State state, State nextState, Action action, float reward, float alpha)
    {
        float prevQ;
        try
        {
            prevQ = qvalues.First((Q) => { return Q.state == state && Q.action == action; }).Q;

        }
        catch
        {
            prevQ = 0;
        }

        float maxNextQ;
        try
        {
            maxNextQ = qvalues.Where((Q) => { return Q.state == nextState; }).Max((Q) => { return Q.Q; });

        }
        catch
        {
            maxNextQ = 0;
        }
        return (1 - alpha) * prevQ + alpha * (reward + maxNextQ);
    }

    void updateQState(State state, Action action, float Q)
    {
        try
        {
            qvalues.First(QValue => QValue.state == state && QValue.action == action);
            foreach (QValue val in qvalues.Where(QValue => QValue.state == state && QValue.action == action))
            {
                val.Q = Q;
            }
        }
        catch
        {
            qvalues = [..qvalues, new QValue(state, action, Q)];
        }
    }


}
