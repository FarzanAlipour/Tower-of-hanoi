using System.Collections.Generic;
using UnityEngine;

public abstract class Solution:MonoBehaviour
{
   public Queue<HanoiMove> ActionQueue;
   public Dictionary<HanoiManager.PileTag, Stack<int>> poles;

   public void Initialize()
   {
      ActionQueue = new Queue<HanoiMove>();
   }
   public abstract void Solve(int n, HanoiManager.PileTag source, HanoiManager.PileTag target,
      HanoiManager.PileTag aux);
}
[System.Serializable]
public class HanoiMove
{
   public int Disk { get;}
   public HanoiManager.PileTag SourceTag { get;}
   public HanoiManager.PileTag TargetTag { get;}

   public HanoiMove(int disk, HanoiManager.PileTag sourcePies, HanoiManager.PileTag targetPile)
   {
      Disk = disk;
      SourceTag = sourcePies;
      TargetTag = targetPile;
   }
}

