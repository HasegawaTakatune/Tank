using UnityEngine;

public class ObjectBase : MonoBehaviour
{
    /// 認識番号
    public byte ID;
    /// 撃破時のエフェクト
    [SerializeField]
    GameObject Explosion;
    /// 最大体力
    [SerializeField]
    protected float max_health = 100;
    /// 体力
	[SerializeField]
    protected float health;

    /// 初期化
	public void Start()
    {
        health = max_health;
        gameObject.name = ID.ToString();
    }
    public void Init()
    {
        health = max_health;
    }

    /// メインループ
    void Update()
    {

    }

    /// ダメージ処理
    public virtual void Damage(float dmg)
    {
        health -= dmg;
        if (health <= 0) OnDied();
    }

    /// 死亡イベント
    public virtual void OnDied()
    {
        Instantiate(Explosion, transform.position, Quaternion.identity);
    }
}
