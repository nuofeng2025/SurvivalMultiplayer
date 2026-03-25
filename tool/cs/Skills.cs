//
// Auto Generated Code By excel2json
// https://neil3d.gitee.io/coding/excel2json.html
// 1. 每个 Sheet 形成一个 Struct 定义, Sheet 的名称作为 Struct 的名称
// 2. 表格约定：第一行是变量名称，第二行是变量类型，第3行是注释

// Generate From Skills.xlsx

public class Skills
{
	public int ID; // id
	public int skillid; // skillid
	public string Name; // 名字
	public int Quality; // 品质（D-SSS）
	public string Icon; // 图标
	public string Skillact; // 技能动作
	public string Race; // 生效种族
	public float Effectsize; // 特效大小
	public int Type; // 1英雄天赋2外部技能3战斗共用池4战斗英雄专属5挑战技能6防御塔技能7士兵技能8装备技能
	public int Level; // 等级
	public int Mana; // 能量消耗
	public int MaxLv; // 最大等级
	public int UseKind; // 1主动/2被动/3光环/4触发/5主动（点击释放）/6普攻出发/7技能触发
	public int Cooldown; // 冷却时间
	public int Range; // 施法距离
	public int AOERange; // 法术范围
	public int IsEffectByWisdom; // 技能受何属性影响
	public float SkillRatio; // 伤害技能系数
	public int SkillBase; // 技能基础值
	public int Probability; // 触发概率
	public string Buffandid; // 关联buffid
	public string Unlockskill; // 前置技能
	public int SkillFactor; // 治疗技能系数
	public float Effecttime; // 特效持续时间
	public int Effectdian; // 技能是否跟人走
	public int ImpactType; // 0队友军1对敌方
	public int SpawnPlace; // 0从玩家身上1鼠标点
	public string Detail; // 文字描述
	public string MaxDetail; // 文字描述
	public int BuyPrice; // 购买价格
	public int PoolKind; // 技能池分类0无1战士2法师3领袖4变身
	public int Level_1; // 等级
	public int MaxLv_1; // 最大等级
	public int GetSkillPoint; // 技能传承消耗
	public int MagicSys; // 魔法系
	public int LearnCost; // 学习消耗
	public int Repay; // 遗忘返还
	public int Fighting; // 战力附加
	public int BuffBase; // 特效参数1
	public int BuffBase2; // 特效参数2
	public int BuffDuration; // buff持续时间
	public int BuffID; // 对应BUFFid
	public int BuffHitChance; // 中BUFF概率
}


// End of Auto Generated Code
