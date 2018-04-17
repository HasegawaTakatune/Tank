using UnityEngine;

public class MyConst : MonoBehaviour {
}

/// 方向関係の定数
namespace Direction{
	public class Direc{
		public const int Up = 1,Down = -1,Right = 1,Left = -1;
	}
}

/// プレイヤ情報の置き場所
public static class GameStatus
{
    static GameObject player_obj;
    static Transform player_trns;
    public static GameObject PlayerObject { get { return player_obj; }set { player_obj = value;player_trns = player_obj.transform; } }
    public static Transform PlayerTransform { get { return player_trns; } }

    public static float ShotCount = 0;
    public static float HitCount = 0;

    public static void Init()
    {
        ShotCount = 0;
        HitCount = 0;
    }
}

public static class GameManager
{
    public static byte Status = Title;
    public const byte Title = 0, Game = 1, Result = 2, GameOver = 3,Wait = 99;

    public static void GameEnd()
    {
        Status = Result;
    }
    public static void Lose()
    {
        Status = GameOver;
    }

    public static bool isPlay() { return Status == Game; }
}
