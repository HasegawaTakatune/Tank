using UnityEngine;

public class Bullet : MonoBehaviour
{

    /// Transformのキャッシュ
    Transform myTransform;
    /// 爆発エフェクト（オブジェクト）
    [SerializeField]
    GameObject Explosion_object;

    /// 威力
    [SerializeField]
    float dmg = 10;
    /// 速度
    [SerializeField]
    float speed = .3f;

    /// 正面のベクトル
    Vector3 forward;

    /// 初期化
    void Start()
    {
        myTransform = transform;
        forward = myTransform.forward * speed;
        Destroy(gameObject, 3);
    }

    /// メインループ
    void Update()
    {
        myTransform.position += forward;
    }

    /// 当たり判定
    void OnCollisionEnter(Collision collision)
    {
        var obj = collision.gameObject;
        // 爆発させる
        Instantiate(Explosion_object, myTransform.position, Quaternion.identity);
        // 戦車に当たった場合、ダメージを与える
        if (obj.tag == "Tank") obj.GetComponent<ObjectBase>().Damage(dmg);
        Destroy(gameObject);
    }
}
