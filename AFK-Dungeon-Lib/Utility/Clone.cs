using Newtonsoft.Json;

namespace AFK_Dungeon_Lib.Utility;

public static class Clone
{
	public static T? CloneObject<T>(T obj)
	{
		string serialized = JsonConvert.SerializeObject(obj);
		return JsonConvert.DeserializeObject<T>(serialized);
	}
}
