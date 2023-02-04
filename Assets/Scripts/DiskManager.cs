using System.Collections.Generic;
using UnityEngine;

public class DiskManager : MonoBehaviour
{
    [SerializeField] private GameObject diskPrefab;
    
    public List<Disk> _disks;
    
    public void InitializeDisks(int diskCount,float height,float minimumRadius,float radiusIncrementAmount)
    {
        if (_disks?.Count>0)
        {
            print("destroying");
            foreach (var disk in _disks)
            {
                Destroy(disk.gameObject);
            }
        }
        Color[] colors = GenerateDistinctColors(diskCount);
        _disks = new List<Disk>();
        for (int i = 0; i < diskCount; i++)
        {
            float currentRadius = minimumRadius + (diskCount - i) * radiusIncrementAmount;
            GameObject diskObj = Instantiate(diskPrefab);
            diskObj.name = i.ToString();
            Disk disk = diskObj.GetComponent<Disk>();
            disk.Initialize(colors[i],height,currentRadius,HanoiManager.PileTag.A);
            SetInitialPosition(disk);
            _disks.Add(disk);
        }
        _disks.Reverse();
    }
    

    void SetInitialPosition(Disk disk)
    {
        disk.transform.position = HanoiManager.Instance.GetPilePositions(HanoiManager.PileTag.A).Item2;
        HanoiManager.Instance.onDiskAdded(HanoiManager.PileTag.A);
        
    }
    public  Color[] GenerateDistinctColors(int count)
    {
        Color[] colors = new Color[count];
        float step = 360f / count;
        for (int i = 0; i < count; i++)
        {
            colors[i] = Color.HSVToRGB(i * step / 360f, 0.7f, 0.9f);
        }
        return colors;
    }
}
