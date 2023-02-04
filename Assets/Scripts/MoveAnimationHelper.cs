using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveAnimationHelper : MonoBehaviour
{
    private Queue<HanoiMove> _moves;
    private DiskManager _diskManager;
    private PileManager _pileManager;
    private HanoiMove currentMove;
    [SerializeField]private float speed = 5;
    public void SetMoves(Queue<HanoiMove> moves,DiskManager diskManager,PileManager pileManager)
    {
        _diskManager = diskManager;
        _pileManager = pileManager;
        _moves = moves;
        
    }

    public void StartAnimating()
    {
        currentMove = _moves.Dequeue();
        StartCoroutine(Animate());
    }

    IEnumerator Animate()
    {
        while (currentMove != null)
        {
            yield return MoveDiskToSourcePoleEntry();
            HanoiManager.Instance.onDiskRemoved(currentMove.SourceTag);
            yield return MoveDiskToTargetEntry();
            yield return MoveDiskToTarget();
            HanoiManager.Instance.onDiskAdded(currentMove.TargetTag);
            yield return new WaitForSeconds(0.1f);
            if (!_moves.TryDequeue(out currentMove))
            {
                currentMove = null;
            }

            
        }
        
    }

    
    IEnumerator MoveDiskToSourcePoleEntry()
    {
        bool isDone = false;
        Disk disk = _diskManager._disks[currentMove.Disk];
        Vector3 from = _pileManager.piles[(int) currentMove.SourceTag].availableDiskPosition;
        Vector3 to = _pileManager.piles[(int) currentMove.SourceTag].entryPosition;
        float duration = (to - from).magnitude / speed;
        float flow = 0, timer = 0;
        while (!isDone)
        {
            timer += Time.deltaTime;
            flow = timer / duration;
            if (flow >= 1)
            {
                disk.transform.position = to;
                isDone = true;
            }
            else
            {
                disk.transform.position = Vector3.Lerp(from, to, flow);
            }

            yield return new  WaitForEndOfFrame();
        }
    }
    IEnumerator MoveDiskToTargetEntry()
    {
        bool isDone = false;
        Disk disk = _diskManager._disks[currentMove.Disk];
        Vector3 from = _pileManager.piles[(int) currentMove.SourceTag].entryPosition;
        Vector3 to = _pileManager.piles[(int) currentMove.TargetTag].entryPosition;
        float duration = (to - from).magnitude / speed;
        float flow = 0, timer = 0;
        while (!isDone)
        {
            timer += Time.deltaTime;
            flow = timer / duration;
            if (flow >= 1)
            {
                disk.transform.position = to;
                isDone = true;
            }
            else
            {
                disk.transform.position = Vector3.Lerp(from, to, flow);
            }

            yield return new  WaitForEndOfFrame();
        }
    }
    IEnumerator MoveDiskToTarget()
    {
        bool isDone = false;
        Disk disk = _diskManager._disks[currentMove.Disk];
        Vector3 from = _pileManager.piles[(int) currentMove.TargetTag].entryPosition;
        Vector3 to = _pileManager.piles[(int) currentMove.TargetTag].availableDiskPosition;
        float duration = (to - from).magnitude / speed;
        float flow = 0, timer = 0;
        while (!isDone)
        {
            timer += Time.deltaTime;
            flow = timer / duration;
            if (flow >= 1)
            {
                disk.transform.position = to;
                isDone = true;
            }
            else
            {
                disk.transform.position = Vector3.Lerp(from, to, flow);
            }

            yield return new  WaitForEndOfFrame();
        }
    }
}
