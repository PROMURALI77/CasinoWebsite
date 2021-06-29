using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Web.UI.WebControls;



/// <summary>
/// Summary description for DataBaseFunctions
/// </summary>
/// 
public static class DataBase
{
    static SqlConnection c = new SqlConnection("Data Source = (LocalDB)\\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\\WebService.mdf; Integrated Security = True");
    public static void CloseConnetion()//סוגר את החיבור לשרת
    {
        c.Close();
    }

    public static void CreatePlayerRow(string userName, int cash,bool IsAI)
    {
        SqlCommand insert = new SqlCommand("INSERT INTO Players (Player,Cash,IsAI) VALUES(@Player,@Cash,@IsAI)", c);
        insert.Parameters.AddWithValue("@Player", userName);
        insert.Parameters.AddWithValue("@Cash", cash);
        insert.Parameters.AddWithValue("@IsAI", IsAI);
        c.Open();
        insert.ExecuteNonQuery();
        c.Close();
    }//יוצר עמודה לטבלת השחקנים
    public static void CreateSessionRow(string game)
    {
        SqlCommand insert = new SqlCommand("INSERT INTO Sessions (Game,State) VALUES (@Game,'True')", c);
        insert.Parameters.AddWithValue("@Game", game);
        c.Open();
        insert.ExecuteNonQuery();
        c.Close();
    }//יוצר עמודה לטבלת המשחקים
    public static void CreatePlayerCardsRow(Card[] cards,string player)
    {
        if (cards.Length == 2)
        {
            if (CheckExists(player, "Player", "PlayerCards"))
                DeleteRows(player, "Player", "PlayerCards");
            SqlCommand insert = new SqlCommand("INSERT INTO PlayerCards (Player,Card1Num,Card2Num,Card1Sign,Card2Sign) VALUES (@Player,@n1,@n2,@s1,@s2)", c);
            insert.Parameters.AddWithValue("@Player", player);
            for (int i = 0; i < cards.Length; i++)
            {
                insert.Parameters.AddWithValue("@n" + (i+1).ToString(), (int)cards[i].GetValue());
                insert.Parameters.AddWithValue("@s" + (i+1).ToString(), cards[i].GetSign().ToString());
            }
            c.Open();
            insert.ExecuteNonQuery();
            c.Close();
        }
    }//יוצר עמודה לטבלת קלפי השחקנים
    public static void CreateSessionCardsRow(Card[] cards, int sessionId)
    {
        if (cards.Length == 5)
        {
            if (CheckExists(sessionId,"SessionId","SessionCards"))
                DeleteRows(sessionId, "SessionId", "SessionCards");
            SqlCommand insert = new SqlCommand("INSERT INTO SessionCards (SessionId,Card1Num,Card2Num,Card3Num,Card4Num,Card5Num,Card1Sign,Card2Sign,Card3Sign,Card4Sign" +
                ",Card5Sign) VALUES (@SessionId,@n1,@n2,@n3,@n4,@n5,@s1,@s2,@s3,@s4,@s5)", c);
            insert.Parameters.AddWithValue("@SessionId",sessionId);
            for (int i = 0; i < cards.Length; i++)
            {
                insert.Parameters.AddWithValue("@n" + (i + 1).ToString(), cards[i].GetValue());
                insert.Parameters.AddWithValue("@s" + (i + 1).ToString(), cards[i].GetSign());
            }
            c.Open();
            insert.ExecuteNonQuery();
            c.Close();
        }
    }//יוצר עמודה לטבלת קלפי המשחק
    public static void CreateMoveLog(int sessionId,string player,string move)
    {
        SqlCommand insert = new SqlCommand("INSERT INTO MoveLog (Player,SessionId,Move,Time) VALUES (@1,@2,@3,@4)", c);
        insert.Parameters.AddWithValue("@1", player);
        insert.Parameters.AddWithValue("@2", sessionId);
        insert.Parameters.AddWithValue("@3", move);
        insert.Parameters.AddWithValue("@4", DateTime.Now);
        c.Open();
        insert.ExecuteNonQuery();
        c.Close();
    }//יוצר עמודה ללוג המהלכים
    public static void CreateHandLog(string player,string hand)
    {
        SqlCommand insert = new SqlCommand("INSERT INTO HandsLog (Player,Hand) VALUES (@1,@2)", c);
        insert.Parameters.AddWithValue("@1", player);
        insert.Parameters.AddWithValue("@2", hand);
        c.Open();
        insert.ExecuteNonQuery();
        c.Close();
    }//יוצר עמודה ללוג הידיים של שחקן


    public static bool CheckExists(string data, string dataName, string table)
    {
        c.Open();
        SqlCommand CheckExists = new SqlCommand("SELECT * FROM " + table + " WHERE " + dataName + "=@Data", c);
        CheckExists.Parameters.AddWithValue("@Data", data);
        SqlDataReader reader = CheckExists.ExecuteReader();
        if ((reader.HasRows))
        {
            c.Close();
            return true;
        }
        else
        {
            c.Close();
            return false;
        }
    }
    public static bool CheckExists(int data, string dataName, string table)
    {
        c.Open();
        SqlCommand CheckExists = new SqlCommand("SELECT * FROM " + table + " WHERE " + dataName + "=@Data", c);
        CheckExists.Parameters.AddWithValue("@Data", data);
        SqlDataReader reader = CheckExists.ExecuteReader();
        if ((reader.HasRows))
        {
            c.Close();
            return true;
        }
        else
        {
            c.Close();
            return false;
        }
    }
    public static string GetStringData(string primaryKey, string primaryKeyName, string column, string table)//מחזיר ערך עבור ערך יחודי בטבלה
    {
        c.Open();
        SqlCommand getData = new SqlCommand("SELECT " + column + " FROM " + table + " WHERE " + primaryKeyName + "=@PrimaryKey", c);
        getData.Parameters.AddWithValue("@PrimaryKeyName", primaryKeyName);
        getData.Parameters.AddWithValue("@PrimaryKey", primaryKey);
        string data = getData.ExecuteScalar().ToString();
        c.Close();
        return data;
    }
    public static int GetColumnMaxNum(string column, string table)//נותן ערך מקסימלי בעמודה
    {
        c.Open();
        SqlCommand getData = new SqlCommand("SELECT MAX ("+column+") FROM "+table , c);        
        int data = (int)getData.ExecuteScalar();
        c.Close();
        return data;
    }
    /*public static DataTable GetColumn(string column,string table,string condition,string conditionType)
    {
        SqlDataAdapter da = new SqlDataAdapter("SELECT (" + column + ") FROM " + table + " WHERE " + conditionType + "="+condition, c);
        DataTable ds  = new DataTable();
        c.Open();
        da.Fill(ds);
        c.Close();
        return ds;
    }*/
    public static void DeleteRows(string value, string valueName, string table)//מחיקת עמודות בטבלה ששוות לערך מסוים
    {
        c.Open();
        SqlCommand delete = new SqlCommand("DELETE FROM " + table + " WHERE " + valueName + "=@Value", c);
        delete.Parameters.AddWithValue("@Value", value);
        delete.ExecuteNonQuery();
        c.Close();
    }
    public static void DeleteRows(int value, string valueName, string table)//מחיקת עמודות בטבלה ששוות לערך מסוים
    {
        c.Open();
        SqlCommand delete = new SqlCommand("DELETE FROM " + table + " WHERE " + valueName + "=@Value", c);
        delete.Parameters.AddWithValue("@Value", value);
        delete.ExecuteNonQuery();
        c.Close();
    }
    public static void UpdateValue(string primaryKey, string primaryKeyName, string value, string valueName, string table)
    {
        c.Open();
        SqlCommand update = new SqlCommand("UPDATE " + table + " Set " + valueName + "=@Value WHERE " + primaryKeyName + "=@PrimaryKey", c);
        update.Parameters.AddWithValue("@Value", value);
        update.Parameters.AddWithValue("@PrimaryKey", primaryKey);
        update.ExecuteNonQuery();
        c.Close();
    }//מעדכן ערך נתון בטבלה
    public static void UpdateValue(string primaryKey, string primaryKeyName, int value, string valueName, string table)
    {
        c.Open();
        SqlCommand update = new SqlCommand("UPDATE " + table + " Set " + valueName + "=@Value WHERE " + primaryKeyName + "=@PrimaryKey", c);
        update.Parameters.AddWithValue("@Value", value);
        update.Parameters.AddWithValue("@PrimaryKey", primaryKey);
        update.ExecuteNonQuery();
        c.Close();
    }//מעדכן ערך נתון בטבלה
    public static void UpdateGamesLog(string wonPlayer,string lostPlayer)
    {
        UpdateValue(lostPlayer, "Player",int.Parse( GetStringData(lostPlayer, "Player", "GamesPlayed", "Players")) + 1
            , "GamesPlayed", "Players");
        UpdateValue(wonPlayer, "Player", int.Parse(GetStringData(wonPlayer, "Player", "GamesPlayed", "Players")) + 1
            , "GamesPlayed", "Players");
        UpdateValue(wonPlayer, "Player", int.Parse(GetStringData(wonPlayer, "Player", "GamesWon", "Players")) + 1
           , "GamesWon", "Players");
    }//מעדכן לוג משחקים
    public static Card[] GetPlayerFinalDeck(string player,int sessionId)
    {
        Card[] deck = new Card[7];
        int num;char sign;
        for (int i = 0; i < deck.Length; i++)
        {
            if (i <5)
            {
                num = int.Parse(GetStringData(sessionId.ToString(), "SessionId", "Card" + (i+1) + "Num", "SessionCards"));
                sign = char.Parse(GetStringData(sessionId.ToString(), "SessionId", "Card" + (i+1) + "Sign", "SessionCards"));
                deck[i] = new Card(num, sign);
            }
            else
            {
                num = int.Parse(GetStringData(player, "Player", "Card" + (7 - i) + "Num", "PlayerCards"));
                sign = char.Parse(GetStringData(player, "Player", "Card" + (7 - i) + "Sign", "PlayerCards"));
                deck[i] = new Card(num, sign);
            }
        }
        return deck;
    }//מחזיר  את היד של השחקן
    public static Card[] GetSessionCards(int sessionId)
    {
        int num;
        char sign;
        Card[] cards = new Card[5];
        for (int i = 0; i < 5; i++)
        {
            num = int.Parse(GetStringData(sessionId.ToString(), "SessionId", "Card" + (i + 1) + "Num", "SessionCards"));
            sign = char.Parse(GetStringData(sessionId.ToString(), "SessionId", "Card" + (i + 1) + "Sign", "SessionCards"));
            cards[i] = new Card(num, sign);
        }
        return cards;      
    }//מחזיר את קלפי המשחק
    public static Card[] GetPlayerCards(string player)
    {
        int num;
        char sign;
        Card[] cards = new Card[2];
        for (int i = 1; i < 3; i++)
        {
            num = int.Parse(GetStringData(player, "Player", "Card" + (i) + "Num", "PlayerCards"));
            sign = char.Parse(GetStringData(player, "Player", "Card" + (i) + "Sign", "PlayerCards"));
            cards[i-1] = new Card(num, sign);
        }
        return cards;
    }//מחזיר את קלפי השחקן
    public static List<string> GetSessionPlayers(int sessionId)
    {
        SqlCommand GetPlayers = new SqlCommand("SELECT (Player) FROM Players WHERE CurrentSessionId=@1", c);
        GetPlayers.Parameters.AddWithValue("@1", sessionId);
        c.Open();
        SqlDataReader reader = GetPlayers.ExecuteReader();       
        GridView players = new GridView();
        players.DataSource = reader;        
        players.DataBind();
        c.Close();
        List<string> playersList = new List<string>();
        for (int i = 0; i < players.Rows.Count; i++)
        {
            playersList.Add(players.Rows[i].Cells[0].Text);
        }
        return playersList;
    }//מחזיר את השחקנים במשחק
    public static List<string> GetSessionLog(int sessionId)
    {
        SqlCommand log = new SqlCommand("SELECT (Move) FROM MoveLog WHERE SessionId=@1",c);
        log.Parameters.AddWithValue("@1", sessionId);
        c.Open();
        SqlDataReader reader = log.ExecuteReader();
        GridView moves = new GridView();
        moves.DataSource = reader;
        moves.DataBind();
        c.Close();
        List<string> list = new List<string>();
        for (int i = 0; i < moves.Rows.Count; i++)
        {
            list.Add(moves.Rows[i].Cells[0].Text);
        }
        return list;
    }//מחזיר את הלוג של המשחק
    public static int[] GetHandCount()
    {
        string[] variations = new string[9]{"high card","pair","two pair","three of a kind",
            "straight","flush","full house","four of a kind","straight flush"};
        int[] monim= new int[9];
        for (int i = 0; i < variations.Length; i++)
        {
            SqlCommand count = new SqlCommand("SELECT COUNT(Hand) FROM HandsLog WHERE Hand=@1", c);
            count.Parameters.AddWithValue("@1", variations[i]);
            c.Open();
            monim[i] = (int)count.ExecuteScalar();
            c.Close();
        }
        return monim;
    }//מחזיר את כמות הרצפים הכוללת
    public static int[] GetHandCount(string userName)
    {
        string[] variations = new string[9]{"high card","pair","two pair","three of a kind",
            "straight","flush","full house","four of a kind","straight flush"};
        int[] monim = new int[9];
        for (int i = 0; i < variations.Length; i++)
        {
            SqlCommand count = new SqlCommand("SELECT COUNT(Hand) FROM HandsLog WHERE Hand=@1 AND Player=@2", c);
            count.Parameters.AddWithValue("@1", variations[i]);
            count.Parameters.AddWithValue("@2", userName);
            c.Open();
            monim[i] = (int)count.ExecuteScalar();
            c.Close();
        }
        return monim;
    }//מחזיר את כמות הרצפים עבור שחקן
    public static int[] GetGamesLog()
    {
        int[] gamesLog = new int[2];
        SqlCommand count1 = new SqlCommand("SELECT SUM(GamesPlayed) FROM Players WHERE IsAi=0", c);
        SqlCommand count2 = new SqlCommand("SELECT SUM(GamesWon) FROM Players WHERE IsAi=0", c);
        c.Open();
        gamesLog[0] = (int)count1.ExecuteScalar();
        gamesLog[1] = (int)count2.ExecuteScalar();
        c.Close();
        return gamesLog;
    }//מחזיר מספר משחקים ומספר נצחונות עבור כל השחקנים
    public static int[] GetGamesLog(string userName)
    {      
        int[] gamesLog = new int[2];
        if(CheckExists(userName,"GamesPlayed","Players"))
            return new int[2]{0,0};
        SqlCommand count1 = new SqlCommand("SELECT SUM(GamesPlayed) FROM Players WHERE  Player=@1", c);
        SqlCommand count2 = new SqlCommand("SELECT SUM(GamesWon) FROM Players WHERE Player=@1", c);
        count1.Parameters.AddWithValue("@1", userName);
        count2.Parameters.AddWithValue("@1", userName);
        c.Open();
        gamesLog[0] = (int)count1.ExecuteScalar();
        gamesLog[1] = (int)count2.ExecuteScalar();
        c.Close();
        return gamesLog;
    }//מחזיר מספר משחקים ומספר נצחונות עבור שחקן
}