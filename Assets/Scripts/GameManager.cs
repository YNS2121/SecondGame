using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    private Canvas canvas;
    [SerializeField] private TextMeshProUGUI CoinText;
    [SerializeField] private TextMeshProUGUI Timer;
    
    public int coins { get; private set; }
    [SerializeField] private PlayerMovement playerMovement;

    private void Awake()
    {
        if (Instance != null)
        {
            DestroyImmediate(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        canvas = GameObject.Find("Canvas").GetComponent<Canvas>();
        playerMovement = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
    }

    private void OnDestroy()
    {
        if (Instance == this)
        {
            Instance = null;
        }
    }

    void Update(){
        if(playerMovement.elapsedTime <= playerMovement.lifeTime){
            Timer.text = playerMovement.countDownTimer();
        }else{
            Timer.text = playerMovement.countUp();
        }
    }


    public void AddCoin()
    {
        coins+=100;
        CoinText.text = coins.ToString();
    }

}
