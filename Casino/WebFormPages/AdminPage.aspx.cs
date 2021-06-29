using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Data.SqlClient;


public partial class WebFormPages_AdminPage : System.Web.UI.Page
{
    static localhost.Pokerasmx ws = new localhost.Pokerasmx();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["IsAdmin"] == null || Session["IsAdmin"].ToString() == "False")
            Response.Redirect("/WebFormPages/HomePage.aspx");
        GetTableData("Users", UsersData_Tbl);
        GetTableData("Log", Log_Tbl, "EnterTime");
    }
    public void GetTableData(string table, GridView Gv)
    {
        SqlConnection c = CasinoDataBase.GetConnection();
        c.Open();
        SqlCommand getTableData = new SqlCommand("SELECT * FROM " + table, c);
        SqlDataReader reader = getTableData.ExecuteReader();
        Gv.DataSource = reader;
        Gv.DataBind();
        c.Close();
    }
    public void GetTableData(string table, GridView Gv, string columnSort)
    {
        SqlConnection c = CasinoDataBase.GetConnection();
        c.Open();
        SqlCommand getTableData = new SqlCommand("SELECT * FROM " + table + " ORDER BY " + columnSort + " DESC", c);
        SqlDataReader reader = getTableData.ExecuteReader();
        Gv.DataSource = reader;
        Gv.DataBind();
        c.Close();
    }
    protected void UsersData_Tbl_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        int i = Convert.ToInt32(e.CommandArgument);
        GridViewRow row = UsersData_Tbl.Rows[i];
        string username = row.Cells[2].Text;
        if (e.CommandName == "UpdateUser")
        {
            Session["AdminDDL"] = false;
            Response.Redirect("/WebFormPages/AdminUpdate.aspx?" + username);
        }
        if (e.CommandName == "DeleteUser")
        {
            string primaryKey = Session["PrimaryKey"].ToString();
            string primaryKeyName = StringFunctions.CheckPrimaryKey(primaryKey);
            if (username != CasinoDataBase.GetStringData(primaryKey, primaryKeyName, "UserName", "Users"))
            {
                CasinoDataBase.DeleteRows(username, "UserName", "Log");
                CasinoDataBase.DeleteRows(username, "UserName", "CashLog");
                CasinoDataBase.DeleteRows(username, "UserName", "Users");
                ws.DeleteUser(username);
            }
        }
        GetTableData("Users", UsersData_Tbl);
        GetTableData("Log", Log_Tbl);
    }

    protected void GetUserXMLFile_Btn_Click(object sender, EventArgs e)
    {
        string primaryKey = Session["PrimaryKey"].ToString();
        string fileName = CasinoDataBase.CreateXMLDocForTable("Users"), adminEmail = CasinoDataBase.GetStringData(primaryKey,
            StringFunctions.CheckPrimaryKey(primaryKey)
            , "Email"
            , "Users");
        CasinoDataBase.SendMail(adminEmail, "XmlFile", "xml file of user table is atachtted to this mail", fileName);
    }
    protected void SendEmail_Btn_Click(object sender, EventArgs e)
    {
        string subjet = EmailSubject_TB.Text, content = EmailContent_TA.InnerText;
        //if (EmailFile_FU == null)
            CasinoDataBase.SendMailToAllUsers(subjet, content);
        /*else
        {
            EmailFile_FU.SaveAs(Server.MapPath("~/") + EmailFile_FU.FileName);
            CasinoDataBase.SendMailToAllUsers(subjet, content,EmailFile_FU.FileName);
        }*/
    }
    protected void GetLogXmlFile_Click(object sender, EventArgs e)
    {
        string primaryKey = Session["PrimaryKey"].ToString();
        string fileName = CasinoDataBase.CreateXMLDocForTable("Log"), adminEmail = CasinoDataBase.GetStringData(primaryKey,
            StringFunctions.CheckPrimaryKey(primaryKey)
            , "Email"
            , "Users");
        CasinoDataBase.SendMail(adminEmail, "XmlFile", "xml file of log table is atachtted to this mail", fileName);
    }
}

/*protected void DisplayWanted(object sender, EventArgs e)
{
    string senderId = ((LinkButton)sender).ID;
    ContentPlaceHolder placeholder = (ContentPlaceHolder)this.Master.FindControl("ContentPlaceHolder1");
    Control div = placeholder.FindControl(StringFunctions.DeleteIdExtention(senderId) + "_Div");
    ((HtmlControl)div).Style.Add("display", "unset");
}*/
