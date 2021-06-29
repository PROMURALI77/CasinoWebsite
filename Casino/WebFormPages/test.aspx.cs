using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Globalization;
using System.Threading;
using System.Data.SqlClient;

public partial class WebFormPages_test : System.Web.UI.Page
{
    static SqlConnection c = new SqlConnection("Data Source = (LocalDB)\\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\\CasinoDB.mdf; Integrated Security = True");
    protected void Page_Load(object sender, EventArgs e)
    {

    }


    protected void Button1_Click(object sender, EventArgs e)
    {
        CasinoDataBase.ResetTable("CashLog");
    }

    protected void Button2_Click(object sender, EventArgs e)
    {
        CreateLog();
    }
    protected void CreateLog()
    {
        DateTime start = DateTime.Now.Date.AddDays(-2),end=DateTime.Now.Date;
        Random rnd = new Random();
        while(start.Day != end.Day || start.Month != end.Month || start.Year != end.Year)
        {
            for (int i = 0; i < 3; i++)
            {
                CasinoDataBase.CreateCashLog("TheTemani", rnd.Next(-300, 300),start);               
                Thread.Sleep(5);
            }
            start = start.AddDays(1);
        }
    }
}