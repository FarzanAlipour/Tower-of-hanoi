using UnityEngine;

public class Disk : MonoBehaviour
{
    public HanoiManager.PileTag pileTag;
    private Material diskMaterial;
    private MeshRenderer _meshRenderer;
    public void Initialize(Color color,float height,float radius,HanoiManager.PileTag pileTag)
    {
        _meshRenderer = GetComponent<MeshRenderer>();
        _meshRenderer.material.color = color;
        this.pileTag = pileTag;
        transform.localScale = new Vector3(radius, height, radius);

    }
}
