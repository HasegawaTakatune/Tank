using UnityEngine;
using UnityEngine.UI;

public class Status_Player : ObjectBase
{

    /// 体力スライダー
	[SerializeField]
    Slider HP;

    /// 初期化
    new void Start()
    {
        base.Start();
        HP.maxValue = max_health;
        HP.value = max_health;

        GameStatus.PlayerObject = gameObject;
    }
    new void Init()
    {
        base.Init();
        HP.value = max_health;
    }

    /// メインループ
	void Update()
    {
        if (GameManager.isPlay())
        {

        }
        else Init();
    }

    /// ダメージ
	public override void Damage(float dmg)
    {
        base.Damage(dmg);
        HP.value = health;
    }

    public override void OnDied()
    {
        base.OnDied();
        GameManager.Lose();
    }
}
