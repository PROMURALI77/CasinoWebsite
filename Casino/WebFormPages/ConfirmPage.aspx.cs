using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class WebFormPages_ConfirmPage : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string url = Request.Url.AbsoluteUri;
        string confirmationKey = StringFunctions.GetUrlExtension(url);
        if (CasinoDataBase.CheckExists(confirmationKey, "ConfirmationKey", "Users"))
        {
            string userName = CasinoDataBase.GetStringData(confirmationKey, "ConfirmationKey", "UserName", "Users");
            CasinoDataBase.ConfirmMail(confirmationKey);
            Messege_Lbl.Style.Add("display", "unset");
            Messege_Lbl.Text = "Your account is confirmed";
            Vmark_Img.Style.Add("display", "unset");
            welcome_H1.Style.Add("display", "unset");
        }
        else
        {
            Messege_Lbl.Style.Add("display", "unset");
            Messege_Lbl.Text = "key is not valid please use the email we sent you";
            Xmark_Img.Style.Add("display", "unset");
        }          
    }
   
}