

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

    public override string ToString(){
        return String.Format("({0},{1}); {2}; {3}", state.X, state.Y, action.ToString(), Q);
    }  
}

enum Action
{
    UP, DOWN, LEFT, RIGHT
}