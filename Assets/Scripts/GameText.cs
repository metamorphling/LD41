using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameText  {

    public enum Commands
    {
        None,
        Stop,
        Left,
        Right,
        Jump, 
        Help,
        Story,
        Kill
    }

    string[] gameScript = {
        "Sounds of broken glass and short scream.\r\nBoth were taken away by the wind, I couldn't <color=red>help</color> it.\r\nThough I was spared, I was not needed.\r\nAnd that was mistake, because I saw you.\r\nHaving nothing left, I went on a long journey to get my life back.",
        "How did it happen? Why me? I'm a simple man living a simple life.\r\nAnd yet my prayers did not help me.\r\nIt's now or never, if God forsaken me I will use my hands.\r\nQuestions were piling up, but legs kept on moving.",
        "Be it big mountain or a small swamp, everything looked same to me.\r\nMy mind stopped distinguishing colors, the whole world now is like a bunch of white stripes. It does not matter, I know the way. All vilagers know it, the way you should never go.",
        "Until the bloodsucker stole my love, everything was perfect.\r\nBut what can I, a simple man, do to him?\r\nI'm not even a thief to go inside unnoticed. In the end, he's an undead. But I know his tricks, too infamous to be invinsible.",
        "Castle is seen from faraway, it's like a north star for a traveler in this lands. At the same time, it's a scar mark for people living here. I always thought it won't happen to me, and yet here I am.\r\nI will definitely save her.",
        "I climbed up so high, fog is getting thicker.\r\nI can't see much, have to use my instincts.\r\nOnly thought of meeting my love makes me keep on going.\r\nHope she's okay...",
        "Leap of faith, there was no way around it.\r\nFog is left behind, while the giant castle rose in front of me.\r\nSure, it's big, yet my determination is even bigger. Í need to sneak inside.",
        "I know your secrets, you could have used some sorcery on her.\r\nYou've been stealing women from nearby villages for a long time, I have heard about your tricks.\r\nI will stand up, I am not afraid.",
        "There she is! And him! What do I do?"
    };

    string help = "<color=red>Help</color> - remind yourself what's really important\r\n<color=red>Left</color> - lose all your hope\r\n<color=red>Right</color> - move towards your hapiness\r\n<color=red>Stop</color> - doubt your actions\r\n<color=red>Jump</color> - show an act of bravery\r\n<color=red>Story</color> - check surroundings";
    string lastLevelHelp = "<color=red>KILL KILL KILL KILL KILL KILL KILL KILL KILL KILL KILL KILL KILL KILL KILL KILL KILL KILL KILL KILL KILL KILL KILL KILL KILL KILL KILL KILL KILL KILL KILL KILL KILL KILL KILL KILL KILL KILL KILL KILL KILL KILL KILL KILL KILL KILL KILL KILL KILL KILL KILL KILL KILL KILL KILL KILL</color>";
    string badEnd = "You decided to spend your days in a cursed world that is left to you\r\n\r\npress <color=red>return</color> to go out on a journey";
    string end = "You never listened to nobody\r\nNot even to the woman who got tired of you and ran away\r\nShe thought making it look like a kidnap will be better, but you were too dumb\r\nSoaking your hands in blood just made you so much worse\r\n\"Murderer, what have you done?!\",she kept on screaming, as your trembling lips tried to explain yourself\r\n\"Vampire?  What? Is it the shit you've been told at your church? Undead do not exist, you killer!\"\r\nThis makes no sense. This woman is nuts, they all are. Crazy idiots, what do they understand? Filled with sorrow you dashed through the halls, never to be seen again.\r\n\r\n You can try pressing return, but it won't <color=blue>return</color> you your life";
    string death = "Poor little man, outside world proved to be too harsh for you\r\nYou should have known your place\r\n\r\nPress <color=red>return</color> if you did not learn your lesson";


    int currentLevel;

    private static readonly GameText instance = new GameText();

    private GameText() { }

    public static GameText Instance
    {
        get
        {
            return instance;
        }
    }

    public string GetStory(int num)
    {
        currentLevel = num;
        return gameScript[num];
    }

    public string GetBadEnd()
    {
        return badEnd;
    }

    public string GetDeathEnd()
    {
        return death;
    }

    public string GetEndEnd()
    {
        return end;
    }

    public string GetHelp()
    {
        return currentLevel > 7 ? lastLevelHelp : help;
    }
}
