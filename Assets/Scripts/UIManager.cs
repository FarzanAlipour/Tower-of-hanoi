
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private TMP_Dropdown solutionType;
    [SerializeField] private TMP_Text messageText, moveCount,timer;

    public void SetTimer(long time)
    {
        timer.text = "Timer: "+time + " ms";
    }
    public void OnDiskCountChanged(string input)
    {
        int count = int.Parse(input);
        if (solutionType.value == 0)
        {
            if (count > 10)
            {
                messageText.gameObject.SetActive(true);
            }
            else
            {
                messageText.gameObject.SetActive(false);
            }
        }

        moveCount.text = "Total moves: " + Mathf.Pow(2, count);
        HanoiManager.Instance.SetDisksandPoles(count);
    }
    public void OnSolutionChanged(int option)
    {
        print(solutionType.value);
        HanoiManager.Instance.SetSolutionType(solutionType.value);
    }
}
