using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class WebFormPages_Statistics : System.Web.UI.Page
{ 
    static localhost.Pokerasmx ws = new localhost.Pokerasmx();
    public static string[] hands = new string[9] {"High card","Pair","Pwo pair",
        "Three of a kind","Straight","Flush","Full house","Four of a kind","Straight Flush" };
    public static string[] winLose = new string[2] { "Loses", "Wins" };
    protected void Page_Load(object sender, EventArgs e)
    {
        if (User())
            UserCashGraph_Div.Style.Add("display", "unset");
        else
            UserCashGraph_Div.Style.Add("display", "none");

    }
    protected int[] GeneralHandCount()
    {
        return ws.GetGeneralHandMonim();
    }
    protected int[] UserHandCount()
    {
        if (User())
            return ws.GetUserHandMonim(Session["PrimaryKey"].ToString());
        return null;
    }
    protected int[] GeneralWinRate()
    {
        int[] arr = ws.GetGeneralGamesLog();
        arr[0] = arr[0] - arr[1];
        return arr;
    }
    protected int[] UserWinRate()
    {
        try
        {
            if (User())
            {
                int[] arr = ws.GetUserGamesLog(Session["PrimaryKey"].ToString());
                arr[0] = arr[0] - arr[1];
                return arr;
            }
            return null;
        }
        catch
        {
            ws.CloseConnection();
            return null;
        }      
    }
    protected int[] CashLogValues()
    {
        if (User())
        {
            string userName = CasinoDataBase.GetStringData(Session["PrimaryKey"].ToString(),
                StringFunctions.CheckPrimaryKey(Session["PrimaryKey"].ToString()), "UserName", "Users");
            int cash = int.Parse(CasinoDataBase.GetStringData(userName, "UserName", "Cash", "Users"));
            string[,] cashLogArr = CasinoDataBase.GetUserCashLog(DateTime.Now.Date.AddDays(-10), DateTime.Now.Date, userName);            
            int[] cashGraphValues = new int[cashLogArr.GetLength(1)];
            cashGraphValues[cashGraphValues.Length - 1] = cash;
            for (int i = cashGraphValues.Length - 2; i >=0; i--)
            {
                cashGraphValues[i] = cash - int.Parse(cashLogArr[1, i+1]);
                cash = cashGraphValues[i];
            }
            return cashGraphValues;
        }
        return null;
    }
    protected string[] CashLogDates()
    {
        if (User())
        {
            string userName = CasinoDataBase.GetStringData(Session["PrimaryKey"].ToString(),
               StringFunctions.CheckPrimaryKey(Session["PrimaryKey"].ToString()), "UserName", "Users");
            
            string[,] cashLogArr = CasinoDataBase.GetUserCashLog(DateTime.Now.Date.AddDays(-10), DateTime.Now.Date, userName);
            string[] dates = new string[cashLogArr.GetLength(1)];
            for (int i = 0; i < dates.Length; i++)
            {
                dates[i] = cashLogArr[0, i];
            }
            return dates;
        }
        return null;
    }
    public bool User()
    {
        return Session["PrimaryKey"] != null;
    }
}

