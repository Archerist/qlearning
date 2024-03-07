

class QValue
{
    public State state;
    public Action action;
    public float Q = 0;
    public QValue(State state, Action action, float Q)
    {
        this.state = state;
        this.Q = Q;
        this.action = action;
    }
}

enum Action
{
    UP, DOWN, LEFT, RIGHT
}