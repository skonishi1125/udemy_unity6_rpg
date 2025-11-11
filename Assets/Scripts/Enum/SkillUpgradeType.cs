using UnityEngine;

public enum SkillUpgradeType
{
    None,

    // ----- Dash Tree -----
    Dash,
    Dash_CloneOnStart, // ダッシュ開始時、clone生成
    Dash_CloneOnStartAndArrival, // ダッシュ開始時、修了時のclone生成
    Dash_ShardOnStart, // ダッシュ開始時、shardの生成
    Dash_ShardOnStartAndArrival, // ダッシュ開始時、終了時のshard生成

    // ----- Shard Tree -----
    Shard,
    Shard_MoveToEnemy,
    Shard_MultiCast,
    Shard_Teleport,
    Shard_TeleportAndHeal

}
