using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class WebFormPages_AddUserPicture : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["primaryKey"] == null)
            Response.Redirect("/WebFormPages/HomePage.aspx");
    }
    protected void ProfilePicture_Btn_Click(object sender, EventArgs e)
    {
        try
        {
            if (ProfilePicture_FU.HasFile)
            {
                string primaryKey = (string)Session["primaryKey"], username = CasinoDataBase.GetStringData
               (primaryKey, StringFunctions.CheckPrimaryKey(primaryKey), "UserName", "Users");
                string imagePath = "/UserImages/" + username + "'sImage.png";
                CasinoDataBase.UpdateValue(primaryKey, "UserName", imagePath, "ProfileImage", "Users");
                ProfilePicture_FU.SaveAs(Server.MapPath(imagePath));
                Response.Redirect("/WebFormPages/UsersData.aspx");
            }
        }
        catch
        {
            Response.Redirect("/WebFormPages/UsersData.aspx");
        }
    }
}