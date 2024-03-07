class State{
    public int X=0;
    public int Y=0;

    public State(){

    }

    public State(int X, int Y){
        this.X = X;
        this.Y = Y;
    }

    public static bool operator ==(State a, State b){
        return a.Y== b.Y && a.X == b.X;
    }

    public static bool operator !=(State a, State b){
        return !(a.Y== b.Y && a.X == b.X);
    }
}