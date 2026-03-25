//
// Auto Generated Code By excel2json
// https://neil3d.gitee.io/coding/excel2json.html
// 1. 每个 Sheet 形成一个 Struct 定义, Sheet 的名称作为 Struct 的名称
// 2. 表格约定：第一行是变量名称，第二行是变量类型，第3行是注释

// Generate From Migong.xlsx

public class Migong
{
	public int MiGongID; // 迷宫关卡章节对应id
	public int LevelType; // 0常规1幻境2无尽守3攻城战4守城战5拯救友军6护送友军
	public string Name; // 名称
	public string Icon; // 图标
	public int NextLevel; // 下一个关卡id
	public int ChaptersLv; // 简单关卡等级(参与）
	public int ChaptersLv1; // 困难关卡等级(参与）
	public string BossReward; //   扫荡#首通#三星
	public string Pool; // 装备掉落
	public string Showitem; // 挑战怪奖励展示
	public int Energy; // 消耗体力
	public string Mapaudio; // 背景音乐
	public int ChaptersId; // 当前章节
	public int DiffcultId; // 关卡难度
	public int Lv; // 简单怪物等级
	public int Lv1; // 困难怪物等级
	public int StartCoin; // 初始金币
	public int Exp; // 每关怪物经验
	public int OutMonster; // 初始出怪时间(毫秒)
	public int RoundOut; // 回合出怪间隔(毫秒)
	public int Times; // 关卡防守总波次
	public string BOSS; // BOSS出现波次
	public string Export; // 出怪
	public string TroopLib; // 部队库
	public string BossMonster; // 层数#部队库#层数增长系数
	public string DiffXiShu; // 关卡波次属性增长值（生命#双攻#双防）
	public string Reward; // 挑战怪奖励
	public string FirstSklillLib; // 优先必出技能，|对应等级，2
	public string Challenge; // 挑战怪
	public int ChallengeNum; // 挑战怪数量
	public string ChildLevelName; // 关卡难度描述
	public string ChallengePos; // 关卡挑战怪点
	public string Box; // 宝箱坐标点
	public string Pos; // 关卡坐标点位
	public string LevelSequence; // 固定事件列表
	public string BianShenSkillLib; // 变身商店要展示的技能 （1|101:102:103   2|101-103）
	public int JuQingType; // 0无 1开始有 2结束有 3开始结束都有
	public string BoxAndMonsterReward; // 宝箱奖励，概率：类型：数量，概率+黑金石+数量，概率+装备+数量
	public string Story; // 章节背景
	public string LevelStory; // 小关卡故事
	public int Nandu; // 难度描述
	public float Difficult; // 关卡难度系数
	public int Nandu_1; // 难度描述
	public string BossAlly; // boss关友军(友军id|友军id+数量+强度+队伍人数)
	public string BossLib; // bossId+强度
	public string BossSkill; // boss技能
	public int BossMonsterNum; // boss关怪物队伍数量
	public string Mapmodel; // 地图
	public string MapName; // 章节背景
}


// End of Auto Generated Code
