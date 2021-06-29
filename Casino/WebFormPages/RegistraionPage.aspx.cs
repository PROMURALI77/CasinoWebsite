using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Threading;
using System.Web.UI.HtmlControls;


public partial class WebFormPages_RegistraionPage : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        HtmlButton btn =(HtmlButton)Master.FindControl("SignIn_Btn1");
        btn.Style.Add("display", "none");
    }

    protected void Register_Btn_Click(object sender, EventArgs e)
    {
        Page.Validate("ValidationGroup");
        if (Page.IsValid)
        {
           string result= CasinoDataBase.Register
               (
               UserName_TB.Text,
               Password_TB.Text,
               FirstName_TB.Text,
               LastName_TB.Text,
               Email_TB.Text
               );
            if(result== "Registerd")
            {
                Messege_Lbl.Text = "Registerd";
                Response.Redirect("/WebFormPages/HomePage.aspx");
            }
            else
            {
                Messege_Lbl.Text = result;
            }
        }
    }
}