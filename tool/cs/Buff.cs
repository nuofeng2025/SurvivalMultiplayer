//
// Auto Generated Code By excel2json
// https://neil3d.gitee.io/coding/excel2json.html
// 1. 每个 Sheet 形成一个 Struct 定义, Sheet 的名称作为 Struct 的名称
// 2. 表格约定：第一行是变量名称，第二行是变量类型，第3行是注释

// Generate From Buff.xlsx

public class Buff
{
	public int ID; // id
	public int Buffid; // buff类型
	public int Debuff; // 是否为负面buff
	public int Ground; // 是否挂在人身上
	public string Effectmodel; // 特效
	public float Shyanchi; // 伤害延迟触发
	public int React; // 触发方式
	public float Modulus1; // 固定值系数
	public float Modulus2; // 百分比系数
	public float Modulus3; // 召唤物系数
	public float Modulus4; // 生效士兵
	public int Maxvalue; // 最大值
	public int Probability; // 触发概率
	public int Time; // 持续时间
	public int Skillid; // 关联技能id
	public string Buffzy; // 关联buffid
	public int Target; // 生效目标
	public string Desc; // buff描述1
}


// End of Auto Generated Code
