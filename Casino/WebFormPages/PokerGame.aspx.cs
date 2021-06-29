using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Threading;



public partial class WebFormPages_PokerGame : System.Web.UI.Page
{
    static localhost.Pokerasmx ws = new localhost.Pokerasmx();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["PrimaryKey"] == null)
            Response.Redirect("http://localhost:51940/WebFormPages/HomePage.aspx");
        if (Session["gameActive"] == null || (bool)Session["gameActive"] == false)
        {
            EnterGame_Div.Style.Add("display", "unset");
            StartGame_Div.Style.Add("display", "none");
            Game_Div.Style.Add("display", "none");

        }
        else if ((int)Session["stage"] == -1)
        {
            EnterGame_Div.Style.Add("display", "none");
            StartGame_Div.Style.Add("display", "unset");
            Game_Div.Style.Add("display", "none");

        }
        else
        {
            EnterGame_Div.Style.Add("display", "none");
            StartGame_Div.Style.Add("display", "none");
            Game_Div.Style.Add("display", "unset");
        }

        if (Session["stage"] != null)
        {
            string[] players = (string[])Session["players"];
            if ((int)Session["stage"] == 0)
            {
                Buttons_Div.Style.Add("display", "unset");
                Continue_Div.Style.Add("display", "none");
                Player1Cards(players[0]);
                Player2Cards();
            }
            else if ((int)Session["stage"] == 4)
            {
                GetWinner();                   
            }
            else if ((int)Session["stage"] == 5)
            {
                Buttons_Div.Style.Add("display", "none");
                Continue_Div.Style.Add("display", "unset");
                Player1Cards(players[0]);
                Player2Cards(players[1]);
                SessionCards((int)Session["sessionId"], 5);
            }
            else if ((int)Session["stage"] > 0)
            {
                Buttons_Div.Style.Add("display", "unset");
                Continue_Div.Style.Add("display", "none");
                Player1Cards(players[0]);
                Player2Cards();
                SessionCards((int)Session["sessionId"], (int)Session["stage"] + 2);

            }
            if ((int)Session["stage"] >= 0)
            {
                TableMoney_Lbl.Text = "Money on the table:" + ((int)Session["tableMoney"]).ToString();
                PlayerMoney_Lbl.Text = "Cash:" + CasinoDataBase.GetStringData(((string[])Session["players"])[0], "UserName", "Cash", "Users")+"  "+Session["Stage"].ToString();
            }
            //else if((int)Session["stage"])
        }

        if (Session["currentCall"] != null && (int)Session["currentCall"] != 0)
        {
            int c = (int)Session["currentCall"];
            Check_Btn.Style.Add("display", "none");
            Call_Btn.Style.Add("display", "unset");
            CurrentCall_Lbl.Text = "CurrentCall:" + c;
        }
        else
        {
            Call_Btn.Style.Add("display", "none");
            Check_Btn.Style.Add("display", "unset");
        }
        if (Session["AiTurn"] != null && (bool)Session["AiTurn"])
            AiMove();
        if (Session["enoughMoneyFlag"] != null && (bool)Session["enoughMoneyFlag"])
        {
            Raise_Lbl.Text = "You dont have the needed amount";
            Session["enoughMoneyFlag"] = false;
        }
        else
            Raise_Lbl.Text = "";
        if (Session["winString"] != null)
            Winner_Lbl.Text = Session["winString"].ToString();
        if (Session["gameActive"] != null && (bool)Session["gameActive"] == true)
        {
            FillLog();
        }


    }
    protected void EnterGame()
    {
        string player1 = CasinoDataBase.GetStringData(Session["PrimaryKey"].ToString(),
            StringFunctions.CheckPrimaryKey(Session["PrimaryKey"].ToString()), "UserName", "Users");
        ws.addPlayer(player1, int.Parse(CasinoDataBase.GetStringData(player1, "UserName", "cash", "Users")), false);
        //string player1 = "TheTemani";
        Session["sessionId"] = ws.joinPokerAISession(player1);
        string[] players = new string[2] { player1, "Session" + Session["sessionId"].ToString() };
        Session["players"] = players;
        Session["gameActive"] = true;
        Session["stage"] = -1;
        Page.Response.Redirect(Page.Request.Url.ToString(), true);
    }//נכנס למשחק
    protected void StartGame()
    {
        string[] arr = (string[])Session["Players"];
        ws.StartGame((int)Session["sessionId"], arr[0].ToString());
        Session["stage"] = 0;
        Session["tableMoney"] = 0;
        Session["currentCall"] = 0;
        Page.Response.Redirect(Page.Request.Url.ToString(), true);
    }//מתחיל משחק
    protected void FillLog()
    {
        Log_p.InnerHtml = ws.GetLog((int)Session["sessionId"]);
    }//ממלא את חלונית הלוג


    protected void Fold(string player)
    {
        Session["winString"] = player + " folded";
        if (ws.IsAi(player))
        {
            string player1 = GetOtherPlayer((string[])Session["players"], player);
            int player1Cash = int.Parse(CasinoDataBase.GetStringData(player1, "UserName", "Cash", "Users"))
                , cash = player1Cash + ws.GetSessionCash((int)Session["sessionId"]);

            CasinoDataBase.UpdateValue(player1, "UserName", cash.ToString(), "Cash", "Users");
            CasinoDataBase.CreateCashLog(player1, ws.GetSessionCash((int)Session["sessionId"]));
            ws.Fold(player, player1, (int)Session["sessionId"]);
            Session["stage"] = -1;
            Page.Response.Redirect(Page.Request.Url.ToString(), true);
        }
        else
        {
            ws.Fold(player, GetOtherPlayer((string[])Session["players"], player), (int)Session["sessionId"]);
            Session["stage"] = -1;
            Page.Response.Redirect(Page.Request.Url.ToString(), true);
        }
    }//פרישה מהמשחק
    protected void Call(string player)
    {
        if (!ws.IsAi(player))
        {
            int playerCash = int.Parse(CasinoDataBase.GetStringData(player, "UserName", "Cash", "Users"));
            int amount = playerCash - (int)Session["currentCall"];
            CasinoDataBase.UpdateValue(player, "UserName", amount.ToString(), "Cash", "Users");
            CasinoDataBase.CreateCashLog(player, -(int)Session["currentCall"]);

        }
        ws.CallOrRaise(player, (int)Session["currentCall"], (int)Session["currentCall"], Session["sessionId"].ToString(), "call");
        Session["tableMoney"] = (int)Session["tableMoney"] + (int)Session["currentCall"];
        Session["currentCall"] = 0;
        Session["AiTure"] = false;
        Session["stage"] = (int)Session["stage"] + 1;
            

        Page.Response.Redirect(Page.Request.Url.ToString(), true);
    }//השוואה
    protected void Raise(string player, int raiseAmount)
    {
        int currentcall = 0;
        string otherplayer = GetOtherPlayer((string[])Session["players"], player);

        if (Session["currentCall"] != null)
            currentcall = (int)Session["currentCall"];
        else
            currentcall = 0;
        if (!ws.IsAi(player))
        {
            int playerCash = int.Parse(CasinoDataBase.GetStringData(player, "UserName", "Cash", "Users")),
                cash = playerCash - raiseAmount - currentcall;
            CasinoDataBase.UpdateValue(player, "UserName", cash.ToString(), "Cash", "Users");
            CasinoDataBase.CreateCashLog(player, -raiseAmount - currentcall);
        }
        else
        {
            if (int.Parse(CasinoDataBase.GetStringData(otherplayer, "UserName", "Cash", "Users")) == 0)
                Check(player);
            else if (!EnoughMoney(otherplayer, raiseAmount))
                raiseAmount = int.Parse(CasinoDataBase.GetStringData(otherplayer, "UserName", "Cash", "Users"));
        }
        ws.CallOrRaise(player, raiseAmount + currentcall, currentcall, Session["SessionId"].ToString(), "raise");
        Session["tableMoney"] = (int)Session["tableMoney"] + raiseAmount + currentcall;
        Session["currentCall"] = raiseAmount - currentcall;
        if (!ws.IsAi(player))
            Session["AiTurn"] = true;
        Page.Response.Redirect(Page.Request.Url.ToString(), true);
    }//העלאה
    protected void Check(string player)
    {
        ws.Check((int)Session["sessionId"], player);
        if (!ws.IsAi(player))
            Session["AiTurn"] = true;
        //AiMove();
        else
            Session["stage"] = (int)Session["stage"] + 1;
        Page.Response.Redirect(Page.Request.Url.ToString(), true);
    }//צק במשחק
    protected void AiMove()
    {
        int aiNum = -4;
        int sessionId = (int)Session["sessionId"];
        string player = "Session" + sessionId;
        Random rnd = new Random();
        Session["AiTurn"] = false;
        if (Session["currentCall"] != null && (int)Session["currentCall"] != 0)
        {
            Int64 num = ws.GetPlayerDeckNum(player, sessionId) / 10000000000;
            num += rnd.Next(1, 4);
            int d = (int)Session["currentCall"] / 100;
            num -= d;
            if (num >= 9 + aiNum)
            {
                Raise(player, (int)(num) * 100 + 100);
            }
            else if (num >= 4 + aiNum)
            {
                Call(player);
                //Session["stage"] = (int)Session["stage"] + 1;
            }
            else
            {
                Fold(player);
            }
        }
        else
        {
            Int64 num = ws.GetPlayerDeckNum(player, sessionId) / 10000000000;
            num = rnd.Next(1, 4);
            if (num >= 6 + aiNum)
                Raise(player, (int)(num) * 100);
            else if (num >= 2 + aiNum)
                Check(player);
            else
                Fold(player);
        }
    }//הדילר מבצע מהלך
    protected void GetWinner()
    {
        string[] players = (string[])Session["players"];
        Object[] obj = ws.GetWinner((int)Session["sessionId"]);
        Int64 deckNum = Int64.Parse(obj[0].ToString());
        string player = obj[1].ToString();
        if (!ws.IsAi(player))
        {
            int playerCash = int.Parse(CasinoDataBase.GetStringData(player, "UserName", "Cash", "Users"))
                , cash = playerCash + ws.GetSessionCash((int)Session["sessionId"]);

            CasinoDataBase.UpdateValue(player, "UserName", cash.ToString(), "Cash", "Users");
            CasinoDataBase.CreateCashLog(player, ws.GetSessionCash((int)Session["sessionId"]));
        }
        
        if(!ws.IsAi(player))
            Session["winString"] = player + " won";
        else
            Session["winString"] = "Dealer won";
        Session["tableMoney"] = 0;
        Session["currentCall"] = 0;
        Session["Stage"] = 5;
        Thread.Sleep(5);
        Page.Response.Redirect(Page.Request.Url.ToString(), true);
    }//מחזיר את המנצח במשחק

    protected void EnterSession_Btn_Click(object sender, EventArgs e)
    {
        EnterGame();
    }
    protected void Player1Cards(string player)
    {
        Image image;
        string[][] cards = ws.GetPlayerCards(player);
        //Card[] cards = objectToCardArr(ws.GetPlayerCards(player));
        /* for (int i = 0; i < cards.Length; i++)
         {
             value += cards[i].ToString() + ",";
         }*/
        for (int i = 0; i < cards.Length; i++)
        {
            image = new Image();
            image.ImageUrl = "../Images/Cards/" + cards[0][i] + cards[1][i] + ".png";
            image.Width = 125;
            image.Height = 181;
            Player1_Div.Controls.Add(image);
        }
    }//קלפים של השחקן
    protected void Player2Cards()
    {
        Image image;
        for (int i = 0; i < 2; i++)
        {
            image = new Image();
            image.ImageUrl = "../Images/Cards/BlankCard.png";
            image.Width = 94;
            image.Height = 134;
            Player2_Div.Controls.Add(image);
        }
    }//קלפים של הדילר
    protected void Player2Cards(string aiPlayer)
    {
        Image image;
        string[][] cards = ws.GetPlayerCards(aiPlayer);
        for (int i = 0; i < cards.Length; i++)
        {
            image = new Image();
            image.ImageUrl = "../Images/Cards/" + cards[0][i] + cards[1][i] + ".png";
            image.Width = 94;
            image.Height = 134;
            Player2_Div.Controls.Add(image);
        }
    }//קלפים של הדילר
    protected void SessionCards(int sessionId, int cardsNum)
    {
        /* string value = "";
         Card[] cards = objectToCardArr(ws.GetSessionCards(sessionId));
         for (int i = 0; i < cardsNum; i++)
         {
             value += cards[i].ToString() + ",";
         }
         return value;*/
        Image image;
        string[][] cards = ws.GetSessionCards(sessionId);
        for (int i = 0; i < cardsNum; i++)
        {
            image = new Image();
            image.ImageUrl = "../Images/Cards/" + cards[0][i] + cards[1][i] + ".png";
            image.Width = 125;
            image.Height = 181;
            GameCards_Div.Controls.Add(image);
        }
    }//קלפי המשחק
    protected Card[] objectToCardArr(string[][] cardsStringArr)
    {
        string[][] temp = cardsStringArr;
        Card[] cards = new Card[cardsStringArr[0].Length];
        int value; char sign;
        for (int i = 0; i < cards.Length; i++)
        {
            value = int.Parse(cardsStringArr[0][i]);
            sign = char.Parse(cardsStringArr[1][i]);
            cards[i] = new Card(value, sign);
        }
        return cards;
    }// ממיר את הנתונים לקלפי המשחק 
    protected string GetOtherPlayer(string[] players, string player)
    {
        if (players[0] == player)
            return players[1];
        return players[0];
    }//מחזיר את השחקן השני במשחק
    protected bool EnoughMoney(string player, int amount)
    {
        return int.Parse(CasinoDataBase.GetStringData(player, "UserName", "Cash", "Users")) >= amount;
    }//בודק האם לשחקן יש מספיק כסף



    /* public static string CreateStringAddon(string player1, string player2, int sessionId)
     {
         if(player1!=null)
             return "Player1=" + player1 + ",player2=" + player2 + ",sessionId=" + sessionId;
         return "";
     }*/



    protected void StartGame_Btn_Click(object sender, EventArgs e)
    {
        StartGame();
    }

    protected void Fold_Btn_Click(object sender, EventArgs e)
    {
        Fold(((string[])Session["players"])[0]);
        StartGame();
    }
    protected void Call_Btn_Click(object sender, EventArgs e)
    {
        if (Session["currentCall"] != null && (int)Session["currentCall"] != 0 && EnoughMoney(((string[])Session["players"])[0], (int)Session["currentCall"]))
        {
            Call(((string[])Session["players"])[0]);
            Error_Lbl.Text = "";
            AiMove();
        }
        else if (!EnoughMoney(((string[])Session["players"])[0], (int)Session["currentCall"]))
        {
            Error_Lbl.Text = "You dont have enough money";
        }
        else
        {
            Error_Lbl.Text = "There is no active call";
        }
    }
    protected void Raise_Btn_Click(object sender, EventArgs e)
    {
        if (RaiseValue_Val.IsValid && RaiseValue_RFV.IsValid)
        {
            int currentCall = 0;
            if (Session["currentCall"] != null)
                currentCall = (int)Session["currentCall"];
            int neededAmount = currentCall + int.Parse(RaiseValue_TB.Text);
            if (EnoughMoney(((string[])Session["players"])[0], neededAmount))
                Raise(((string[])Session["players"])[0], int.Parse(RaiseValue_TB.Text));
            else
            {
                Session["enoughMoneyFlag"] = true;
                Page.Response.Redirect(Page.Request.Url.ToString(), true);
            }
        }
    }
    protected void Check_Btn_Click(object sender, EventArgs e)
    {
        ws.Check((int)Session["sessionId"], ((string[])Session["players"])[0]);
        AiMove();
    }

    protected void ExitGame_Btn_Click(object sender, ImageClickEventArgs e)
    {
        Session["gameActive"] = false;
        Response.Redirect("/WebFormPages/Games.aspx");
    }
}