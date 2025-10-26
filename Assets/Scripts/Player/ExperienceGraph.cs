using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ExperienceGraph : MonoBehaviour
{

    [SerializeField] private AnimationCurve xpCurve;
    
    private Image fill;
    private TextMeshProUGUI levelTxt;
    private PlayerStats playerStats;
    private HealthBar healthBar;

    private int currentLvl = 0;
    private int totalXp = 0;

    // All code needs to be optimized better (too many useless ass methods)

    void Awake()
    {
        fill = GetComponent<Image>();
        levelTxt = GameObject.Find("Level").GetComponent<TextMeshProUGUI>();
        playerStats = GameObject.Find("Target").GetComponent<PlayerStats>();
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
            ApplyRandomStatBoost();
            UpdateLevel();
        }

        float prevXp = xpCurve.Evaluate(currentLvl);
        float nextXp = xpCurve.Evaluate(currentLvl + 1);
        fill.fillAmount = Mathf.InverseLerp(prevXp, nextXp, totalXp);
    }

    void ApplyRandomStatBoost()
    {
        float healthBoost = Random.Range(1f, 3f);
        float defenseBoost = Random.Range(0.1f, 0.3f);
        float speedBoost = Random.Range(0.05f, 0.06f);

        playerStats.maxHealth += healthBoost;
        playerStats.defense += defenseBoost;
        playerStats.moveSpeed += speedBoost;

        playerStats.currentHealth = playerStats.maxHealth;
        Debug.Log("Applied: +{healthBoost:F1} HP, +{defenseBoost:F1} DEF +{speedBoost:F2} SPD");
    }

    void UpdateLevel()
    {
        levelTxt.text = currentLvl.ToString();
    }
}
