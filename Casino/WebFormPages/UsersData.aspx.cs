using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Threading;

public partial class WebFormPages_UsersData : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["PrimaryKey"] == null)
        {
            Content_Div.InnerHtml = "<Label>Please sign in to use this page</Label>";
        }
        else
        {
            Refresh();
        }
    }
    protected void Refresh()
    {
        ContentPlaceHolder placeHolder = (ContentPlaceHolder)this.Master.FindControl("ContentPlaceHolder1");
        Control linkButtonDiv = placeHolder.FindControl("LinkButton_Div");
        string primaryKey = Session["PrimaryKey"].ToString();
        foreach (Control c in linkButtonDiv.Controls)
        {
            if (c is LinkButton)
            {
                string name = StringFunctions.DeleteIdExtention(c.ID);
                string value = CasinoDataBase.GetStringData(primaryKey, StringFunctions.CheckPrimaryKey(primaryKey), name, "Users");
                if (name != "Password")
                    ((LinkButton)c).Text = value;
                else
                    ((LinkButton)c).Text = "Password";
                HtmlControl div = (HtmlControl)placeHolder.FindControl(name + "_Div");
                div.Style.Add("display", "none");
                /*TextBox textBox = (TextBox)placeHolder.FindControl(name + "_TB");
                textBox.Text = value;*/
            }
            if (c is Label)
            {
                string name = StringFunctions.DeleteIdExtention(c.ID);
                if (name == "IsConfirmed")
                {
                    string value = CasinoDataBase.GetStringData(primaryKey, StringFunctions.CheckPrimaryKey(primaryKey), name, "Users");
                    if (value == "True")
                        ((Label)c).Text = "Your mail is confirmed";
                    else
                        ((Label)c).Text = "Your mail isn't confirmed";
                }
            }
            ProfilePicture_Img.ImageUrl = CasinoDataBase.GetStringData(primaryKey, StringFunctions.CheckPrimaryKey(primaryKey), "ProfileImage", "Users");
            //ProfilePicture_Div.Style.Add("display", "none");
        }
        if (Session["IsAdmin"].ToString() == "True")
            Admin_a.Style.Add("display", "unset");
    }
    protected void DisplayWanted(object sender, EventArgs e)
    {
        Refresh();
        string senderId = ((WebControl)sender).ID;
        ContentPlaceHolder placeHolder = (ContentPlaceHolder)this.Master.FindControl("ContentPlaceHolder1");
        Control div = placeHolder.FindControl(StringFunctions.DeleteIdExtention(senderId) + "_Div");
        ((HtmlControl)div).Style.Add("display", "unset");
    }
    protected void UpdateValue(object sender, EventArgs e)
    {
        ContentPlaceHolder placeHolder = (ContentPlaceHolder)this.Master.FindControl("ContentPlaceHolder1");
        string primaryKey = Session["PrimaryKey"].ToString();
        string valueName = StringFunctions.DeleteIdExtention(((Button)sender).ID);
        string value = ((TextBox)placeHolder.FindControl(valueName + "_TB")).Text;
        Page.Validate(valueName);
        if (Page.IsValid)
        {
            if (valueName == "Email"|| valueName == "UserName")
            {
                if (!CasinoDataBase.CheckExists(value, valueName, "Users"))
                {
                    CasinoDataBase.UpdateValue(primaryKey, StringFunctions.CheckPrimaryKey(primaryKey), value, valueName, "Users");
                    if (valueName == "UserName")
                        Session["PrimaryKey"] = value;
                    Response.Redirect("/WebFormPages/UsersData.aspx");
                }
                else
                {
                    string s="Already exists";
                    ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('" + s + "');", true);
                }
                    
            }
            else
            {
                CasinoDataBase.UpdateValue(primaryKey, StringFunctions.CheckPrimaryKey(primaryKey), value, valueName, "Users");
                Response.Redirect("/WebFormPages/UsersData.aspx");
            }                        
        }
        else
        {
            HtmlControl div = (HtmlControl)placeHolder.FindControl(valueName + "_Div");
            div.Style.Add("display", "unset");
        }
    }

    protected void SignOut_Btn_Click(object sender, EventArgs e)
    {
        string userName = Session["PrimaryKey"].ToString();
        CasinoDataBase.AddExitTime((int)Session["LogNum"], userName);
        Session.Clear();
        Response.Redirect("~/WebFormPages/HomePage.aspx");
    }

    protected void ProfilePicture_Img_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("~/WebFormPages/AddUserPicture.aspx");
    }

    protected void BuyCash_Btn_Click(object sender, EventArgs e)
    {       
        string userName = Session["PrimaryKey"].ToString();
        if (bool.Parse(CasinoDataBase.GetStringData(userName, "UserName", "IsConfirmed", "Users")))
        { 
        Session["CashYDDL"] = false;
        Session["CashMDDL"] = false;
        Response.Redirect("/WebFormPages/BuyCash.aspx");
        }
        else
        {
            string s = "Confirm your account first";
            ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('" +s + "');", true);
        }
    }
}
