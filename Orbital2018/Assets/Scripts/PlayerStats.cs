using UnityEngine;

public class PlayerStats : MonoBehaviour {

    public static int Attempt;
    public int startAttempt = 0;

    public static int attemptGained;

    public static int Money;
    public int startMoney = 999;

    public static int moneyEarned;

    public static int Lives;
    public int startLives = 5;

    public static int livesLost;

    public static int monstersKilled;
    public static int monstersKilledStage;

    void Awake () {
        Money = startMoney;
        Lives = startLives;
        Attempt = startAttempt;

        moneyEarned = 0;
        livesLost = 0;
        monstersKilled = 0;
        monstersKilledStage = 0;
	}

}
