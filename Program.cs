
class Program
{

    static void Main(string[] args)
    {
        State startState = new State();
        State endState = new State(20,20);
        int n = 30;
        float startEpsilon = 0.00001f;
        bool linearEpsilon = false;
        float stopEpsilon = 0.00003f;

        float startAlpha = 0.7f;
        bool linearAlpha = false;
        float stopAlpha = 0.00003f;
        
        float startGamma = 0.96f;
        bool linearGamma = false;
        float stopGamma = 0.00003f;

        int maxSteps = 1000;
        int episodes = 100;
        Reward[] rewards = [
            new Reward(endState,100)
        ];


        Agent agent = new Agent(startState,startAlpha, startEpsilon, startGamma);
        Environment env = new Environment(n,rewards);
        string lastEpResult ="";
        string steps ="";
        for (int e = 0; e < episodes; e++)
        {   
            if(linearEpsilon){
                var currentEpsilon = LinearDecrease(startEpsilon,stopEpsilon,episodes, e);
                agent.epsilon = currentEpsilon;
            }

            if(linearAlpha){
                var currentAlpha = LinearDecrease(startAlpha,stopAlpha,episodes, e);
                agent.epsilon = currentAlpha;
            }
            if(linearGamma){
                var currentGamma = LinearDecrease(startGamma,stopGamma,episodes, e);
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
            steps += stepPerCycle + "\n";
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
            File.WriteAllText("./out/lastEpresult.csv", lastEpResult);
            File.WriteAllText("./out/Steps.csv", steps);

        return;

    }

    static float LinearDecrease(float startE, float stopE, int episodeLength, int currentEpisode){
        return (startE-stopE)/episodeLength * currentEpisode + startE;
    } 
}