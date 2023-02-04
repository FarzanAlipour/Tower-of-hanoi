using System;
using System.Diagnostics;
using UnityEngine;

public class HanoiManager : MonoBehaviour
{
    public static HanoiManager Instance;
    
    public int DiskCount
    {
        get => diskCount;
    }

    [SerializeField] private SolutionType _solutionType;
    [SerializeField] private int diskCount;
    [SerializeField] private Solution iterativeSolution,recursiveSolution;
    private Solution _solution;
    private long timer;
    [SerializeField] private float smallestDiskRadius,radiusIncrementAmount,diskHeight;
    [SerializeField] private PileManager _pileManager;
    [SerializeField] private DiskManager _diskManager;
    [SerializeField] private MoveAnimationHelper _moveAnimationHelper;
    [SerializeField] private UIManager _uiManager;
    public Action<PileTag> onDiskRemoved,onDiskAdded;
    public (Vector3, Vector3) GetPilePositions(PileTag pTag)
    {
        return _pileManager.GetPilePositions(pTag);
    }
    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(Instance);
        }

        Instance = this;
        
    }

    private void Start()
    {
        //OnStart();
    }

    public enum PileTag
    {
        A = 0,
        B = 1,
        C = 2,
        N = 3
    }

    enum SolutionType
    {
        Recursive,
        Iterative
    }

    public void SetDisksandPoles(int count)
    {
        print("Setting the Disks");
        diskCount = count;
        _pileManager.Initialize(diskCount,diskHeight,smallestDiskRadius,radiusIncrementAmount);
        _diskManager.InitializeDisks(diskCount,diskHeight,smallestDiskRadius,radiusIncrementAmount);
    }
    public void OnStart()
    {
        _solution = _solutionType == SolutionType.Iterative ? iterativeSolution : recursiveSolution;
        
        _solution.Initialize();
        Stopwatch st = new Stopwatch();
        st.Start();
        _solution.Solve(diskCount,PileTag.A,PileTag.C,PileTag.B);
        st.Stop();
        timer = st.ElapsedMilliseconds;
        _uiManager.SetTimer(timer);
        _moveAnimationHelper.SetMoves(_solution.ActionQueue,_diskManager,_pileManager);
        _moveAnimationHelper.StartAnimating();
        
    }
    
    private void PrintMoves()
    {
        while (_solution.ActionQueue.Count>0)
        {
            HanoiMove move = _solution.ActionQueue.Dequeue();
            print(move.Disk + " moves from "+ move.SourceTag + " to "+move.TargetTag);
        }
    }

    public void SetSolutionType(int solutionTypeValue)
    {
        _solutionType = (SolutionType) solutionTypeValue;
    }
}
