﻿namespace Console_Monsters.Quests;

public class StartQuest : QuestBase
{
	public StartQuest()
	{
		QuestName = "Start Quest";
		QuestDescription = "The first quest";
		QuestId = 1;
	}

	public override void TriggerQuestComplete()
	{
		var lastNPC = QuestNPCs[QuestNPCs.Count];

		lastNPC.QuestDialogue = new string[]
		{
			"Congrats on clearing this quest",
			"Here is your reward:",
			"[1x Candle]"
		};

		PlayerInventory.TryAdd(Candle.Instance);
	}
}
