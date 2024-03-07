
class Program
{

    static void Main(string[] args)
    {
        State startState = new State();
        State endState = new State(3, 5);
        int n = 10;
        float epsilon = 0.3f;
        float alpha = 0.7f;

        int maxSteps = 1000;
        int cycle = 100;
        Reward[] rewards = [
            new Reward(endState,100)
        ];


        Agent agent = new Agent(startState,alpha, epsilon);
        Environment env = new Environment(n,rewards);
        for (int c = 0; c < cycle; c++)
        {   
            agent.currentPos = new State();
            int stepPerCycle =0;
            for (int i = 0; i < maxSteps; i++){
                if(agent.currentPos == endState){
                    stepPerCycle = i;
                    break;
                };
                agent.next(env);
            }
            Console.WriteLine(stepPerCycle);
            continue;
        }

        return;

    }
}