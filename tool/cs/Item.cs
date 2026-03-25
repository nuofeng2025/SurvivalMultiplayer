//
// Auto Generated Code By excel2json
// https://neil3d.gitee.io/coding/excel2json.html
// 1. 每个 Sheet 形成一个 Struct 定义, Sheet 的名称作为 Struct 的名称
// 2. 表格约定：第一行是变量名称，第二行是变量类型，第3行是注释

// Generate From Item.xlsx

public class Item
{
	public int ID; // id
	public string Name; // 名字
	public string Conditiontext; // 文本说明
	public int Quality; // 品质
	public int Star; // 装备初始星级
	public string Icon; // 图标
	public int Type; // 1道具/2装备
	public int Type2; // 道具:1道具2材料3碎片4消耗5英雄经验|装备:3武器4护甲5鞋子2饰品
	public int Type3; // 职业划分：1物理2法术（区分装备部位类型，对应职业可以装备部位）
	public string Ids; // 使用道具获得另外道具（后端处理）
	public string Effect; // 特殊效果
	public int Bagshow; // 0不显示背包1显示背包
	public int Overlap; // 是否叠放
	public int Used; // 是否可使用
	public int Usedlv; // 使用等级
	public string PowerLimit; // 1武将生命2武将攻击3武将防御|4武将智力|5移速|6射程|7攻速|8减伤|9增伤|10技能增伤|11技能减伤|12暴击|13暴击伤害14吸血|15晕击|16暴击抵抗|17爆伤抵抗|18闪避|19命中|20反伤|21真实伤害|22部队生命|23部队攻击|24部队防御|25武将生命恢复|26部队生命恢复
	public string EquipSkill; // 装备技能
	public int tracking; // 是否埋点
	public int Fighting; // 战力附加
}


// End of Auto Generated Code
