using UnityEngine;

public enum SkillUnpgradeType
{
    // ----- Dash Tree -----
    Dash,
    Dash_CloneOnStart, // ダッシュ開始時、clone生成
    Dash_CloneOnStartAndArrival, // ダッシュ開始時、修了時のclone生成
    Dash_ShardOnStart, // ダッシュ開始時、shardの生成
    Dash_ShardOnStartAndArrival // ダッシュ開始時、終了時のshard生成

}
