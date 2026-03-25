//
// Auto Generated Code By excel2json
// https://neil3d.gitee.io/coding/excel2json.html
// 1. 每个 Sheet 形成一个 Struct 定义, Sheet 的名称作为 Struct 的名称
// 2. 表格约定：第一行是变量名称，第二行是变量类型，第3行是注释

// Generate From MissionLeader.xlsx

public class MissionLeader
{
	public int id; // 引导任务id
	public int nextid; // 下个任务id
	public int uiIndex; // ui识别id
	public int type; // 0强引导1任务
	public string showTip; // 显示信息
	public int MissionType; // 任务类型
	public int UseNum; // 完成任务需要的数量
	public string UseId; // 任务涉及到的内容
	public string story1; // 任务故事
	public string story2; // 任务完成故事
	public string sound1; // 任务语音
	public string sound2; // 完成任务后语音
	public string Reward; // 任务奖励
}


// End of Auto Generated Code
