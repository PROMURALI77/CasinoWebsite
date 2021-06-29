using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Threading;
using System.Xml;

/// <summary>
/// Summary description for Pokerasmx
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
// [System.Web.Script.Services.ScriptService]
public class Pokerasmx : System.Web.Services.WebService
{
    static SqlConnection c = new SqlConnection("Data Source = (LocalDB)\\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\\WebService.mdf; Integrated Security = True");
    public Pokerasmx()
    {

        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }

    [WebMethod]
    public int GetPlayerCash(string player)
    {
        return int.Parse(DataBase.GetStringData(player, "Player", "Cash", "Players"));
    }
    [WebMethod]
    public void addPlayer(string player, int cash, bool IsAI)//מוסיף שחקן לטבלת השחקנים
    {
        if (DataBase.CheckExists(player, "Player", "Players"))
            DataBase.UpdateValue(player, "Player", cash, "Cash", "Players");
        else
            DataBase.CreatePlayerRow(player, cash, IsAI);
    }
    [WebMethod]
    public int joinPokerAISession(string player)//מכניס שחקן למשחק פוקר
    {
        DataBase.CreateSessionRow("Poker");
        int sessionId = DataBase.GetColumnMaxNum("Id", "Sessions");
        addPlayer("Session" + sessionId, 0, true);
        DataBase.UpdateValue("Session" + sessionId, "Player", sessionId, "CurrentSessionId", "Players");
        DataBase.UpdateValue(player, "Player", sessionId, "CurrentSessionId", "Players");
        return sessionId;
    }
    [WebMethod]
    public string game(int stage)//מזמן את הפעולה הרלוונטית עבור השלב במשחק
    {
        string[] arr = new string[1] { "Start Game" };
        return arr[stage];
    }
    [WebMethod]
    public void StartGame(int sessionId, string player)
    {
        Card[] pack = Poker.BuildPack();
        List<int> list = new List<int>();
        Card[] randomCards;
        int num;
        DataBase.UpdateValue(sessionId.ToString(), "Id", 0, "Cash", "Sessions");

        for (int i = 0; i < 2; i++)
        {
            randomCards = new Card[2];
            for (int k = 0; k < 2; k++)
            {
                num = Poker.RandomizeNum(list);
                randomCards[k] = pack[num - 1];
                list.Add(num);
            }
            Thread.Sleep(5);
            if (i == 1)
                DataBase.CreatePlayerCardsRow(randomCards, "Session" + sessionId);
            else
                DataBase.CreatePlayerCardsRow(randomCards, player);
        }

        randomCards = new Card[5];
        for (int i = 0; i < 5; i++)
        {
            num = Poker.RandomizeNum(list);
            randomCards[i] = pack[num - 1];
            list.Add(num);
        }
        DataBase.CreateSessionCardsRow(randomCards, sessionId);
    }//מתחיל משחק
    [WebMethod]
    public int Fold(string foldPlayer,string lastPlayer, int sessionId)
    {
        /*DataTable ds = DataBase.GetColumn("Player", "Players", "CurrentSessionId", sessionId.ToString());
        DataRow[] dr=ds.Select();
        for (int i = 0; i < dr.Length; i++)
        {
            if(dr[i]["Player"].ToString()==)
        }*/
        if(!IsAi(foldPlayer))
            DataBase.CreateMoveLog(sessionId, foldPlayer, foldPlayer+ " folded");
        else
            DataBase.CreateMoveLog(sessionId, foldPlayer, "Dealer folded");
        int playerCash = int.Parse(DataBase.GetStringData(lastPlayer, "Player", "Cash", "Players"))
            , sessionCash = int.Parse(DataBase.GetStringData(sessionId.ToString(), "Id", "Cash", "Sessions"));
        
        DataBase.UpdateValue(sessionId.ToString(), "Id", 0, "Cash", "Sessions");
        DataBase.UpdateValue(lastPlayer, "Player", playerCash + sessionCash, "Cash", "Players");
        DataBase.UpdateGamesLog(lastPlayer, foldPlayer);
        return sessionCash;
    }//מסיים משחק ומחזיר את ערך הכסף במשחק
    [WebMethod]
    public void CallOrRaise(string player, int amount,int currentCall, string sessionId,string s)
    {
        int playerCash = int.Parse(DataBase.GetStringData(player, "Player", "Cash", "Players"))
             , sessionCash = int.Parse(DataBase.GetStringData(sessionId.ToString(), "Id", "Cash", "Sessions"));
        DataBase.UpdateValue(player, "Player", playerCash - amount, "Cash", "Players");
        DataBase.UpdateValue(sessionId, "Id", sessionCash + amount, "Cash", "Sessions");
        if (s == "raise")
        {
            if (!IsAi(player))
                DataBase.CreateMoveLog(int.Parse(sessionId), player, player + " raised " + (amount - currentCall).ToString());
            else
                DataBase.CreateMoveLog(int.Parse(sessionId), player, "Dealer raised " + (amount - currentCall).ToString());
        }
        else
        {
            if(!IsAi(player))
                DataBase.CreateMoveLog(int.Parse(sessionId), player, player + " called " + amount.ToString());
            else
                DataBase.CreateMoveLog(int.Parse(sessionId), player, "Dealer called " + amount.ToString());
        }
    }//משווה או מעלה את ההימור
    [WebMethod]
    public void Check(int sessionId,string player)
    {
        if(!IsAi(player))
            DataBase.CreateMoveLog(sessionId, player, player + " checked");
        else
            DataBase.CreateMoveLog(sessionId, player,"Dealer checked");
    }
    [WebMethod]    
    public Object[] GetWinner(int sessionId)
    {
        List<string> players = DataBase.GetSessionPlayers(sessionId);
        string[] playersArr=players.ToArray();
        Int64[] nums = new Int64[playersArr.Length];
        Card[] deck = new Card[7];
        for (int i = 0; i < playersArr.Length; i++)
        {          
            deck=DataBase.GetPlayerFinalDeck(playersArr[i], sessionId);
            nums[i] = Poker.GetDeckNumber(deck);
            DataBase.CreateHandLog(playersArr[i], Poker.GetDeckVariation(nums[i]));
        }
        int maxIndex = 0;
        for (int i = 1; i < nums.Length; i++)
        {
            if (nums[i] > nums[maxIndex])
                maxIndex = i;
        }       
        if(!IsAi(playersArr[maxIndex]))
            DataBase.CreateMoveLog(sessionId, playersArr[maxIndex], playersArr[maxIndex]+" won with "
            +Poker.GetDeckVariation(nums[maxIndex]));
        else
            DataBase.CreateMoveLog(sessionId, playersArr[maxIndex], "Dealer won with "
                + Poker.GetDeckVariation(nums[maxIndex]));
        if (playersArr[maxIndex] == playersArr[0])
            DataBase.UpdateGamesLog(playersArr[0], playersArr[1]);
        else
            DataBase.UpdateGamesLog(playersArr[1], playersArr[0]);
        return new object[2] { nums[maxIndex], playersArr[maxIndex] };
    }//מחזיר את המנצח
    [WebMethod]
    public List<string[]> GetSessionCards(int sessionId)
    {
        return CardToObjectArr(DataBase.GetSessionCards(sessionId));
    }//מחזיר מערך קלפים של סשן
    [WebMethod]
    public List<string[]> GetPlayerCards(string player)
    {
        return CardToObjectArr(DataBase.GetPlayerCards(player));
    }//מחזיר מערך קלפים של שחקן
    [WebMethod]
    public bool IsAi(string player)
    {
        return bool.Parse(DataBase.GetStringData(player,"Player","IsAI","Players"));
    }//בודק האם השחקן אמיתי או ממוחשב
    [WebMethod]
    public int GetSessionCash(int sessionId)
    {
        return int.Parse(DataBase.GetStringData(sessionId.ToString(), "Id", "Cash", "Sessions"));
    }//מחזיר את כמות הכסף בסשן
    [WebMethod]
    public Int64 GetPlayerDeckNum(string player,int sessionId)
    {
        Card[] deck=DataBase.GetPlayerFinalDeck(player, sessionId);
        return Poker.GetDeckNumber(deck);
    }//מחזיר מספר המייצג יד של שחקן
    [WebMethod]
    public string GetLog(int sessionId)
    {
        string[] s= DataBase.GetSessionLog(sessionId).ToArray();
        string log="";
        for (int i = 0; i < s.Length; i++)
            log += s[i]+"<br />";
        return log;
    }//מחזיר את לוג המשחק
    [WebMethod]
    public string GetVariation(Int64 num)
    {
        return Poker.GetDeckVariation(num);
    }//מחזיר את סוג הרצף
    [WebMethod]
    public int[] GetGeneralHandMonim()
    {
       return DataBase.GetHandCount();
    }//מחזיר את כמות הרצפים הכוללת
    [WebMethod]
    public int[] GetUserHandMonim(string userName)
    {
        return DataBase.GetHandCount(userName);
    }//מחזיר את כמות הרצפים עבור שחקן
    [WebMethod]
    public int[] GetGeneralGamesLog()//מחזיר מספר משחקים ומספר נצחונות עבור כל השחקנים
    {
        return DataBase.GetGamesLog();
    }
    [WebMethod]
    public int[] GetUserGamesLog(string userName)//מחזיר מספר משחקים ומספר נצחונות עבור שחקן
    {
        return DataBase.GetGamesLog(userName);
    }
    [WebMethod]
    public void DeleteUser(string userName)//מוחק משתמש ואת תיעודי המשתמש
    {
        DataBase.DeleteRows(userName, "Player", "MoveLog");
        DataBase.DeleteRows(userName, "Player", "HandsLog");
        DataBase.DeleteRows(userName, "Player", "PlayerCards");
        DataBase.DeleteRows(userName, "Player", "Players");
    }
    [WebMethod]
    public void CloseConnection()//סוגר את החיבור לשרת
    {
        DataBase.CloseConnetion();
    }
    [WebMethod]
    public int[] test(string userName)
    {
        return DataBase.GetGamesLog(userName);
    }
    
    public List<string[]> CardToObjectArr(Card[] cards)
    {
        string[] values = new string[cards.Length], signs = new string[cards.Length];
        List<string[]> list = new List<string[]>();
        for (int i = 0; i < cards.Length; i++)
        {           
            values[i] = cards[i].GetValue().ToString();
            signs[i] = cards[i].GetSign().ToString();
        }
        list.Add(values);
        list.Add(signs);
        return list;

    }//ממיר מערך של קלפים למערך של אובייקטים
    
}