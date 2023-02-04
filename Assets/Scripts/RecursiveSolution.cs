

public class RecursiveSolution : Solution
{
    
    public override void Solve(int n, HanoiManager.PileTag source, HanoiManager.PileTag target,
        HanoiManager.PileTag aux)
    {
        
        if (n == 0) {
            return;
        }
        Solve(n - 1, source, aux,target);
        print("Move disk " + n + " from rod "
                          + source + " to rod " + target);
        ActionQueue.Enqueue(new HanoiMove(n-1,source,target));
        Solve(n - 1, aux, target, source);
    }
}
