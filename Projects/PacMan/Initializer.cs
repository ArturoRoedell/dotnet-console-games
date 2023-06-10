using System;
using System.Collections.Generic;
using System.IO;
using static FileUtilitiesXT.Types;
using static BoringStuff;
using LittleHelpersLibrary;

class HeliMenu
{
	public static void Main(string[] args)
	{
		Start.Begin();
	}
}

class Start
{
	public static void Begin()
	{
		CustomJsonFile<NameAndScoreSet> myJsonFile = new CustomJsonFile<NameAndScoreSet>();
		FileUtilitiesXT fileUtilitiesXt = new FileUtilitiesXT();

		myJsonFile.FileName = "Helicopter Scores";
		myJsonFile.DirPath = Directory.GetCurrentDirectory() + @"\HighScoresFolder";
		fileUtilitiesXt.LoadFileToListThenSortAndCap(myJsonFile, x => x.Score);
		bool play = true;
		while (play)
		{
			int score = GameState.Begin();
			myJsonFile.ListData = SudoGUI_HighScore(myJsonFile.ListData, score);
			fileUtilitiesXt.CreateFileSortWriteToJson(myJsonFile, x => x.Score);
			play = selectionTools.YesNoSelection("\n\nDo You Want To Continue Playing?");
		}
		Show_HighScore(myJsonFile.ListData);
		bool clearHighScores = selectionTools.YesNoSelection("Do You Want To Clear High Scores?");
		if (clearHighScores)
		{
			myJsonFile.ListData.Clear();
			fileUtilitiesXt.CreateFileSortWriteToJson(myJsonFile, x => x.Score);
			Console.WriteLine("High Scores cleared!");
			Console.WriteLine("Press Any Key to exit");
			Console.ReadKey(true);
		}
	}
}

class BoringStuff
{
	public static List<NameAndScoreSet> SudoGUI_HighScore(List<NameAndScoreSet> HighScoreList, int score)
	{
		Console.Write("\n\nEnter Player Name: ");
		Console.CursorVisible = true;
		string playerName = Console.ReadLine();
		Console.CursorVisible = false;
		Console.Clear();
		AddNamesAndScoresToList(playerName, score, HighScoreList);
		return HighScoreList;
	}

	public static void Show_HighScore(List<NameAndScoreSet> HighScoreList)
	{
		Console.WriteLine("###### TOP SCORES ########");
		string name;
		int cap = HighScoreList.Count < 7 ? HighScoreList.Count : 7;
		for (int i = 0; i < cap; i++)
		{
			name = StringTrimPad(HighScoreList[i].Name, 14);
			Console.WriteLine($"#{i + 1}: {name} \nScore:  {HighScoreList[i].Score} \n");
		}
	}

	public static void AddNamesAndScoresToList(String name, int score, List<NameAndScoreSet> list)
	{
		list.Add(new NameAndScoreSet(name, score));
	}

	public static string StringTrimPad(string str, int maxLength)
	{
		int remove = str.Length - maxLength;
		if (remove > 0)
		{
			str = str.Remove(maxLength, remove);
		}
		else
		{
			str = str.PadRight(maxLength - str.Length);
		}
		return str;
	}
}