using UnityEngine;
using UnityEngine.UI;

public class Result : MonoBehaviour {

    /// リザルトの表示テキスト
    [SerializeField]
    Text ResultText;
    
    /// オブジェクトが有効になった際のコールバック
    void OnEnable()
    {
        // ヒット率/ランク表示をする
        float rate = 100 / (GameStatus.ShotCount / GameStatus.HitCount);
        string rank = (rate >= 90) ? "S" : (rate >= 80) ? "A" : (rate >= 50) ? "B" : "C";
        ResultText.text = GameStatus.ShotCount.ToString() + "発中　" + GameStatus.HitCount + "発命中\n命中率　" + rate.ToString() + "%\nランク" + rank;
    }
}
