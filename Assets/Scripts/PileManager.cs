
using UnityEngine;

public class PileManager : MonoBehaviour
{
    public Pile[] piles;
    [SerializeField] private CameraManager _cameraManager;
    public void SetPilesPosition(float radius)
    {
        piles[1].transform.position = Vector3.zero;
        piles[0].transform.localPosition = new Vector3(-(radius + 0.3f), 0, 0);
        piles[2].transform.localPosition = new Vector3((radius + 0.3f), 0, 0);
    }
    public (Vector3, Vector3) GetPilePositions(HanoiManager.PileTag pTag)
    {
        return (piles[(int) pTag].entryPosition, piles[(int) pTag].availableDiskPosition);
    }

    void DiskAddedTo(HanoiManager.PileTag to)
    {
        piles[(int) to].DiskAdded();
    }
    void DiskRemovedFrom(HanoiManager.PileTag from)
    {
        piles[(int) from].DiskRemoved();
    }

    private void Start()
    {
        HanoiManager.Instance.onDiskAdded += DiskAddedTo;
        HanoiManager.Instance.onDiskRemoved += DiskRemovedFrom;
    }
    private void OnDisable()
    {
        HanoiManager.Instance.onDiskAdded -= DiskAddedTo;
        HanoiManager.Instance.onDiskRemoved -= DiskRemovedFrom;
    }

    public void Initialize(int diskCount, float diskHeight,float minDiskRad,float incrementRadius)
    {
        float standRadius = diskCount * incrementRadius + minDiskRad + 0.5f;
        _cameraManager.FitToCamera(standRadius);
        SetPilesPosition(standRadius);
        foreach (var pile in piles)
        {
            pile.Initialize(diskCount,diskHeight,standRadius);
        }
        
    }
}
