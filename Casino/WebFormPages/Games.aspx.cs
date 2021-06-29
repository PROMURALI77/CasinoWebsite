using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class WebFormPages_Games : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if(Session["primaryKey"]==null)
        {
            Error_Div.Style.Add("display", "unset");
            Games_Div.Style.Add("display", "none");
        }
        else
        {
            Error_Div.Style.Add("display", "none");
            Games_Div.Style.Add("display", "unset");
        }
    }
}