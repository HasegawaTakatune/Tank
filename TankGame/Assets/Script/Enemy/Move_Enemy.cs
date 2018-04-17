using UnityEngine;
using UnityEngine.AI;

public class Move_Enemy : MonoBehaviour
{
    /// ナビメッシュエージェント
    [SerializeField]
    NavMeshAgent agent;
    /// 追尾ターゲット
    [SerializeField]
    Transform target;
    /// Transformキャッシュ
    [SerializeField]
    Transform mytrns;

    /// 初期座標
    Vector3 initPosition;

    /// 初期化
    void Start()
    {
        target = GameStatus.PlayerTransform;
        mytrns = transform;
        initPosition = mytrns.position;
    }
    void Init()
    {
        mytrns.position = initPosition;
    }

    /// メインループ
    void Update()
    {
        if (GameManager.isPlay())
        {

            // ターゲットの設定
            if (target == null) target = GameStatus.PlayerTransform;
            // ターゲットの座標を設定
            if (agent != null) agent.SetDestination(target.position);
        }
        else Init();
    }
}
