//
// Auto Generated Code By excel2json
// https://neil3d.gitee.io/coding/excel2json.html
// 1. 每个 Sheet 形成一个 Struct 定义, Sheet 的名称作为 Struct 的名称
// 2. 表格约定：第一行是变量名称，第二行是变量类型，第3行是注释

// Generate From EquipRand.xlsx

public class EquipRand
{
	public int RandId; // 随机属性id
	public int Type; // 1武将生命2武将攻击3武将防御|4武将智力|5移速|6射程|7攻速|8减伤|9增伤|10技能增伤|11技能减伤|12暴击|13暴击伤害14吸血|15晕击|16韧性|17魔穿|18闪避|19命中|20反伤|21真实伤害|22部队生命|23部队攻击|24部队防御}25武将生命恢复|26部队生命恢复
	public int Lv10; // 等级1-10
	public int Lv20; // 等级11-20
	public int Lv30; // 等级21-30
	public int Lv40; // 等级31-40
	public int Lv50; // 等级41-50
	public int XiShu; // 要除的系数
	public int NumValue; // 对应的价值
}


// End of Auto Generated Code
