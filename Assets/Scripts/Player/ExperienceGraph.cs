using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ExperienceGraph : MonoBehaviour
{

    [SerializeField] private AnimationCurve xpCurve;
    
    private Image fill;
    private TextMeshProUGUI levelTxt;

    private int currentLvl = 0;
    private int totalXp = 0;

    // All code needs to be optimized better (too many useless ass methods)

    void Awake()
    {
        fill = GetComponent<Image>();
        levelTxt = GameObject.Find("Level").GetComponent<TextMeshProUGUI>();
    }

    void Update()
    {
        // To test if xp works
        if (Input.GetKeyUp(KeyCode.E))
        {
            AddXp(67);
        }
    }

    public void AddXp(int amount)
    {
        totalXp += amount;

        int nextLvl = currentLvl;
        int safe = 0;

        while (totalXp >= xpCurve.Evaluate(nextLvl + 1))
        {
            nextLvl++;
            safe++;

            if (safe > 1000)
            {
                Debug.Log("Xp Curve is not configured correctly.");
                break;
            }
        }

        if (nextLvl != currentLvl)
        {
            currentLvl = nextLvl;
            UpdateLevel();
        }

        float prevXp = xpCurve.Evaluate(currentLvl);
        float nextXp = xpCurve.Evaluate(currentLvl + 1);
        fill.fillAmount = Mathf.InverseLerp(prevXp, nextXp, totalXp);
    }

    void UpdateLevel()
    {
        levelTxt.text = currentLvl.ToString();
    }
}
