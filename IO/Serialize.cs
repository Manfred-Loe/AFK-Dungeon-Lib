using System.Collections;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using AFK_Dungeon_Lib.Account;

namespace AFK_Dungeon_Lib.IO;
public static class Serialize
{
	public static void SerializeToFile(UserAccount user, string filename)
	{
		string output = JsonConvert.SerializeObject(user);
		//erase previous file
		if (File.Exists(filename))
		{
			File.Delete(filename);
		}
		FileStream f;
		StreamWriter sw;
		f = new FileStream(filename, FileMode.OpenOrCreate);
		sw = new StreamWriter(f);
		sw.Write(output);
		sw.Close();
		f.Close();
	}
	public static string SerializeFromFile(string filename)
	{
		FileStream f;
		StreamReader sr;
		f = new FileStream(filename, FileMode.OpenOrCreate);
		sr = new StreamReader(f);
		return sr.ReadToEnd();
	}
}
