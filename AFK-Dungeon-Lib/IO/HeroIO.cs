using Newtonsoft.Json;
using AFK_Dungeon_Lib.Pawns.Hero;

namespace AFK_Dungeon_Lib.IO;
internal static class HeroIO
{
	public static void SerializeToFile(Hero h, string filename)
	{
		FileStream f;
		StreamWriter sw;
		string output = JsonConvert.SerializeObject(h);
		//erase previous file
		if (File.Exists(filename))
		{
			File.Delete(filename);
		}

		f = new FileStream(filename, FileMode.OpenOrCreate);
		sw = new StreamWriter(f);
		sw.Write(output);
		sw.Close();
		f.Close();
	}

	public static string Serialize(Hero h)
	{
		return JsonConvert.SerializeObject(h);
	}

	public static Hero? Deserialize(string serial)
	{
		return JsonConvert.DeserializeObject<Hero>(serial);
	}

	/*public Hero CloneHero(Hero h)
    {
        return new Hero()
    }*/
}
