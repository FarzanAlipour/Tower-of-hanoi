using UnityEngine;

public class Pile : MonoBehaviour
{
    
    [SerializeField] private Transform stand, pole;
    public Vector3 entryPosition,availableDiskPosition;
    private float diskHeight;
    public Transform StandTransform
    {
        get
        {
            return stand;
        }
    }
    public Transform PoleTransform
    {
        get
        {
            return pole;
        }
    }
    public void Initialize(int diskCount,float diskHeight,float standRadius)
    {
        Vector3 poleScale = pole.localScale;
        this.diskHeight = diskHeight;
        availableDiskPosition = stand.position;
        availableDiskPosition.y += 0.3f + diskHeight;
         
        pole.localScale = new Vector3(poleScale.x, diskCount * diskHeight + 1, poleScale.z);
        pole.position = new Vector3(stand.position.x, pole.localScale.y + stand.position.y, stand.position.z);
        entryPosition = pole.position;
        entryPosition.y += (pole.localScale.y + 0.3f);
        Vector3 standScale = stand.localScale;
        //calculate stand radius
        stand.localScale = new Vector3(standRadius, standScale.y, standRadius);
        
    }

    public void DiskAdded()
    {
        availableDiskPosition.y += 2*diskHeight;
    }

    public void DiskRemoved()
    {
        availableDiskPosition.y -= 2*diskHeight;
    }
}
