using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Status_Enemy : ObjectBase
{

    /// HP表示のオブジェクト + トランスフォーム + スライダ
    [SerializeField]
    GameObject HP_Object;
    Transform HP_Transform;
    [SerializeField]
    Transform myCanvas;
    Slider HP_Slider;
    /// HP表示を向かせるターゲット
    [SerializeField]
    Transform target;

    /// 初期化
    new void Start()
    {
        base.Start();
        HP_Transform = HP_Object.transform;
        HP_Slider = HP_Object.GetComponent<Slider>();
        HP_Slider.maxValue = max_health;
        HP_Slider.value = max_health;
        target = GameStatus.PlayerTransform;
    }
    new void Init()
    {
        base.Init();
        HP_Slider.value = max_health;
    }

    /// メインループ
    void Update()
    {
        if (GameManager.isPlay())
        {
            // ターゲットを設定したら、その方向に向く
            if (target == null) target = GameStatus.PlayerTransform;
            myCanvas.LookAt(target);
        }
        else Init();

    }

    /// ダメージ処理
    public override void Damage(float dmg)
    {
        base.Damage(dmg);
        HP_Slider.value = health;
        GameStatus.HitCount++;
    }

    /// 死亡した際のコールバック
    public override void OnDied()
    {
        base.OnDied();
        GetComponent<Move_Enemy>().enabled = false;
        GetComponentInChildren<TargetAiming_Enemy>().enabled = false;
        GetComponent<UnityEngine.AI.NavMeshAgent>().enabled = false;
        StartCoroutine(CallGameEnd());
    }

    IEnumerator CallGameEnd()
    {
        yield return new WaitForSeconds(4);
        GameManager.GameEnd();
        GetComponent<Move_Enemy>().enabled = true;
        GetComponentInChildren<TargetAiming_Enemy>().enabled = true;
        GetComponent<UnityEngine.AI.NavMeshAgent>().enabled = true;
    }
}
