using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // じゃんけんの手の種類
    public enum HANDS {
        ROCK,
        SCISSORS,
        PAPER
    }
    // 勝敗
    public enum JUDGE {
        WIN,
        LOSE,
        DRAW
    }
    
    private HANDS _myHand;
    private HANDS _enemyHand;
    [SerializeField] private GameObject _endPanel;
    [SerializeField] private GameObject[] _enemyHandsImage;
    private int _time = 0;

    private void Start() {
        _endPanel.SetActive(false);
        InvokeRepeating("SetEnemyHandImageOrder", 0, 0.1f);
    }

    /// <summary>
    /// 自分の手を決定
    /// </summary>
    /// <param name="hand"></param>
    public void SetMyHand(int hand) {
        _myHand = (HANDS)hand;
        SetEnemyHand();

    }

    /// <summary>
    /// 相手の手を決定
    /// </summary>
    /// <param name="hand"></param>
    public void SetEnemyHand() {
        _enemyHand = GetRandomeHands();
        CancelInvoke();
        SetEnemyHandImage(_enemyHand);
        Judge();
    }

    /// <summary>
    /// 勝敗を決定
    /// </summary>
    public void Judge() {
        // 引き分け
        if(_myHand == _enemyHand) {
            SetJudgeText(JUDGE.DRAW);
            return;
        }
        // 負け
        if(_myHand == HANDS.ROCK     && _enemyHand == HANDS.PAPER || 
           _myHand == HANDS.SCISSORS && _enemyHand == HANDS.ROCK  || 
           _myHand == HANDS.PAPER && _enemyHand == HANDS.SCISSORS
        ) {
            SetJudgeText(JUDGE.LOSE);
            return;
        }
        // 勝ち
        SetJudgeText(JUDGE.WIN);
    }

    /// <summary>
    /// 勝敗をテキストに表示
    /// </summary>
    /// <param name="judge"></param>
    public void SetJudgeText(JUDGE judge) {
        _endPanel.SetActive(true);
        Text text = _endPanel.GetComponentInChildren<Text>();
        switch(judge){
            case JUDGE.WIN:
                text.text = "YOU WIN!!";
                break;
            case JUDGE.LOSE:
                text.text = "YOU LOSE";
                break;
            case JUDGE.DRAW:
                text.text = "DRAW";
                break;
        }
    }

    /// <summary>
    /// 相手の手の画像を表示
    /// </summary>
    /// <param name="hand"></param>
    public void SetEnemyHandImage(HANDS hand) {
        int index = 0;
        foreach(GameObject obj in _enemyHandsImage) {
            if((HANDS)index == hand) obj.SetActive(true);
            else obj.SetActive(false);
            index++;
        }
    }

    /// <summary>
    /// 相手の手の画像を順番に表示
    /// </summary>
    public void SetEnemyHandImageOrder() {
        SetEnemyHandImage((HANDS)(_time++ % 3));
    }

    /// <summary>
    /// ランダムな手を返す
    /// </summary>
    /// <returns></returns>
    public HANDS GetRandomeHands() {
        return (HANDS)Random.Range(0, 3);
    }

    /// <summary>
    /// シーン再読み込み
    /// </summary>
    public void Retry() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
