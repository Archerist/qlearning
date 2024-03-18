
class Program
{

    static void Main(string[] args)
    {
        State startState = new State();
        State endState = new State(3, 5);
        int n = 10;
        float epsilon = 0.3f;
        bool linearEpsilon = false;
        float alpha = 0.7f;
        bool linearAlpha = false;
        float gamma = 0.96f;
        bool linearGamma = false;

        int maxSteps = 1000;
        int episodes = 100;
        Reward[] rewards = [
            new Reward(endState,100)
        ];


        Agent agent = new Agent(startState,alpha, epsilon, gamma);
        Environment env = new Environment(n,rewards);
        string lastEpResult ="";
        for (int e = 0; e < episodes; e++)
        {   
            if(linearEpsilon){
                var currentEpsilon = LinearDecrease(epsilon,0,episodes, e);
                agent.epsilon = currentEpsilon;
            }

            if(linearAlpha){
                var currentAlpha = LinearDecrease(alpha,0,episodes, e);
                agent.epsilon = currentAlpha;
            }
            if(linearGamma){
                var currentGamma = LinearDecrease(gamma,0,episodes, e);
                agent.epsilon = currentGamma;
            }


            agent.currentPos = new State();
            int stepPerCycle =0;
            for (int i = 0; i < maxSteps; i++){
                if(agent.currentPos == endState){
                    stepPerCycle = i;
                    break;
                };
                agent.next(env);

            }
            Console.WriteLine("Steps taking this episode: {0}", stepPerCycle);
            string qvalOutput ="";
            qvalOutput += "(X,Y); Action; Q\n";
            foreach (var qvalue in agent.qvalues)
            {   
                qvalOutput += qvalue.ToString() + "\n";
            }
            Console.WriteLine(qvalOutput);
            
            if (e == episodes-1)
            {
                lastEpResult = qvalOutput;
            }


            continue;
        }
            File.WriteAllText("./lastEpresult.csv", lastEpResult);

        return;

    }

    static float LinearDecrease(float startE, float stopE, int episodeLength, int currentEpisode){
        return (startE-stopE)/episodeLength * currentEpisode + startE;
    } 
}