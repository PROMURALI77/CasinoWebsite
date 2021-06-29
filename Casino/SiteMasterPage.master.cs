using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class SiteMasterPage : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        SignIn_Btn1.Attributes.Add("onclick", "return false");
        CheckSignedIn();
        
    }
    protected void SignIn_Btn2_Click(object sender, EventArgs e)
    {
       if (Page.IsValid)
         {
            string result = CasinoDataBase.SignIn(UserName_Email_TB.Text, Password_TB.Text);
            if (result == "Signed in")
            {
                string primaryKey = UserName_Email_TB.Text;
                string userName = CasinoDataBase.GetStringData(primaryKey, StringFunctions.CheckPrimaryKey(primaryKey), "UserName", "Users");
                Session["PrimaryKey"] =userName;
                Session["IsAdmin"] = CasinoDataBase.GetStringData(primaryKey, StringFunctions.CheckPrimaryKey(primaryKey), "IsAdmin", "Users");
                SigningModal.Style.Add("display", "none");
                CasinoDataBase.CreateNewLog(userName);
                Session["LogNum"] = CasinoDataBase.GetLogMaxNum(userName);
                Page.Response.Redirect(Page.Request.Url.ToString(), true);
                //Page_Load(sender, e);               
            }
            else
            {
                SignIn_Lbl.Text = result;
                SigningModal.Style.Add("display", "block");
            }
        }
    }
    public void CheckSignedIn()
    {
        if (Session["PrimaryKey"] != null)
        {
            SignIn_Btn1.Style.Add("display", "none");
            Name_Lbl.Style.Add("display", "unset");
            Cash_Lbl.Style.Add("display", "unset");
            Name_Lbl.Text = CasinoDataBase.GetStringData(Session["PrimaryKey"].ToString(), StringFunctions.CheckPrimaryKey(Session["PrimaryKey"].ToString()), "UserName", "Users");
            Cash_Lbl.Text ="Cash:"+ CasinoDataBase.GetStringData(Session["PrimaryKey"].ToString(), StringFunctions.CheckPrimaryKey(Session["PrimaryKey"].ToString()), "Cash", "Users");
        }
       
    }

    protected void Name_Lbl_Click(object sender, EventArgs e)
    {
        Response.Redirect("/WebFormPages/UsersData.aspx");
    }
}
