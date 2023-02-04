
using System.Collections.Generic;
using UnityEngine;
using Debug = UnityEngine.Debug;

public class IterativeSolution : Solution
{
    public override void Solve(int n, HanoiManager.PileTag source, HanoiManager.PileTag target,
        HanoiManager.PileTag aux)
    {
        ActionQueue = new Queue<HanoiMove>();
        poles = new Dictionary<HanoiManager.PileTag, Stack<int>>();
        
        int numberOfMoves = (int)Mathf.Pow(2, n)-1;
        if (n%2==0)
        {
            HanoiManager.PileTag tmp = aux;
            aux = target;
            target = tmp;
        }
        poles.Add(source,new Stack<int>());
        poles.Add(aux,new Stack<int>());
        poles.Add(target,new Stack<int>());
        for (int i = n-1; i>=0; i--)
        {
            Debug.Log(i+ " added to source");
            poles[source].Push(i);
        }
        for (int i = 1; i <= numberOfMoves; i++)
        {
           
            //S to D
            if (i%3 == 1)
            {

                HanoiMove move = DolegalMove(source, target);
                ActionQueue.Enqueue(move);
                print(move.Disk+ " moves from "+move.SourceTag+" to "+move.TargetTag);
            }
            //S to A
            if (i%3 == 2)
            {
                HanoiMove move = DolegalMove(source, aux);
                ActionQueue.Enqueue(move);
                print(move.Disk+ " moves from "+move.SourceTag+" to "+move.TargetTag);
            }
            //A to D
            if (i%3 == 0)
            {
                HanoiMove move = DolegalMove(aux, target);
                ActionQueue.Enqueue(move);
                print(move.Disk+ " moves from "+move.SourceTag+" to "+move.TargetTag);
            }
        }
    }

    HanoiMove DolegalMove(HanoiManager.PileTag source,HanoiManager.PileTag target)
    {
        int disk1,disk2;
        //check if src is not empty
        bool srcHasDisk = poles[source].TryPeek(out disk1);
        bool dstHasDisk = poles[target].TryPeek(out disk2);
        if (!srcHasDisk)
        {
            disk2 = poles[target].Pop();
            poles[source].Push(disk2);
            return new HanoiMove(disk2, target, source);
        }
        if (!dstHasDisk)
        {
            disk1 = poles[source].Pop();
            poles[target].Push(disk1);
            return new HanoiMove(disk1, source, target);
        }
        if (disk1 > disk2)
        {
            disk2 = poles[target].Pop();
            poles[source].Push(disk2);
            return new HanoiMove(disk2, target, source);
        }
        disk1 = poles[source].Pop();
        poles[target].Push(disk1);
        return new HanoiMove(disk1, source, target);
        
    }
    
}

