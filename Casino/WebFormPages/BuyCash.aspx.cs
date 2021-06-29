using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class WebFormPages_BuyCash : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["PrimaryKey"]!=null)
        {
            if (Session["CashYDDL"] == null)
                Session["CashYDDL"] = false;
            if (Session["CashMDDL"] == null)
                Session["CashMDDL"] = false;
            AddYears();
            AddMonths();
        }
        else
        {
            Response.Redirect("/WebFormPages/HomePage.aspx");
        }
        
    }
    protected void AddYears()
    {
        if ( !((bool)Session["CashYDDL"]))
        {
            for (int i = 2018; i <= 2030; i++)
                Year_DDL.Items.Add(i.ToString());
        }
        Session["CashYDDL"] = true;
    }//מוסיף שנים לרשימה
    protected void AddMonths()
    {
        if (!((bool)Session["CashMDDL"]))
            for (int i = 1; i <= 12; i++)
                Month_DDL.Items.Add(i.ToString());
        Session["CashMDDL"] = true;
    }//מוסיף חודשים לרשימה

    protected void Buy_Btn_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
            string primaryKey = Session["PrimaryKey"].ToString(),
                userName = CasinoDataBase.GetStringData(primaryKey, StringFunctions.CheckPrimaryKey(primaryKey), "UserName", "Users");
            int amount=int.Parse(Amount_TB.Text), currentCash = int.Parse(CasinoDataBase.GetStringData(userName, "UserName", "Cash", "Users"));
            CasinoDataBase.UpdateValue(userName, "UserName", (currentCash + amount).ToString(), "Cash", "Users");
            CasinoDataBase.CreateCashLog(userName, amount);


            string emailMessege = "Recipt:\n\n" +
                "Card Owner Name:" + CreditCardOwner_TB.Text + "\n" +
                "Credit Card Number:"+CreditCard(CreditCardNumber_TB.Text)+"\n\n" +
                "Amount:"+amount+"\n\n\n" +
                "You really think you can make profit???"
                ,email=CasinoDataBase.GetStringData(userName,"UserName","Email","Users");
            CasinoDataBase.SendMail(email, "Cash Recipt", emailMessege);

            Session["CashYDDL"] = false;
            Session["CashMDDL"] = false;
            Response.Redirect("/WebFormPages/UsersData.aspx");
        }
    }
    protected string CreditCard(string creditCard)
    {
        char[] arr = creditCard.ToCharArray();
        for (int i = 0; i < arr.Length-4; i++)
            arr[i] = '*';
        string s="";
        for (int i = 0; i < arr.Length; i++)
            s += arr[i];
        return s;
            
    }//משאיר רק ארבע ספרות אחרונות במספר כרטיס אשראי גלויות
}