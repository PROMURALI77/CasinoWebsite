using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class WebFormPages_AdminUpdate : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["PrimaryKey"] != null)
        { 
        string url = Request.Url.AbsoluteUri;
        string userName = StringFunctions.GetUrlExtension(url);
        if (Session["IsAdmin"].ToString()=="False")
            Response.Redirect("/WebFormPages/HomePage.aspx");
        if(userName == ""|| !CasinoDataBase.CheckExists(userName, "UserName", "Users"))
            Response.Redirect("/WebFormPages/AdminPage.aspx");
        UpdateUserName_Lbl.Text = userName;
        AddTableHeadersValues();
        }
        else
            Response.Redirect("/WebFormPages/HomePage.aspx");
    }
    protected void Update_Btn_Click(object sender, EventArgs e)
    {
        try
        {
            string username = UpdateUserName_Lbl.Text;
            string value = UpdateValue_TB.Text;
            if (value!="")
            {
                string valueName = UpdateValueName_DDL.Text;
                if (!CasinoDataBase.CheckExists(username, "UserName", "Users"))
                    Update_Lbl.Text = "User do not exist";
                else
                {
                    CasinoDataBase.UpdateValue(username, "UserName", value, valueName, "Users");
                    Update_Lbl.Text = "Value updated";
                    if (valueName == "IsAdmin" && (Session["PrimaryKey"].ToString() == username ||
                        Session["PrimaryKey"].ToString() == CasinoDataBase.GetStringData
                        (username, StringFunctions.CheckPrimaryKey(username), "Email", "Users")))
                        Session["IsAdmin"] = value;
                    Session["AdminDDL"] = false;
                    Response.Redirect("/WebFormPages/AdminPage.aspx");
                }
            }
            else
            {
                Update_Lbl.Text = "Value box is empty";
            }
        }

        catch
        {
            Update_Lbl.Text = "Invalid Query";
            CasinoDataBase.CloseConnection();
        }
    }
    protected void AddTableHeadersValues()
    {      
        if (!((bool)Session["AdminDDL"]))
        {
            List<string> headers = CasinoDataBase.GetTableHeaders("Users");
            foreach (string s in headers)
                UpdateValueName_DDL.Items.Add(s);
        }
        Session["AdminDDL"]=true;
    }   

}
