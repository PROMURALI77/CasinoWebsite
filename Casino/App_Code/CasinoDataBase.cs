using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Net.Mail;
using System.Web.UI.WebControls;
using System.Data;
using System.Xml;

/// <summary>
/// Summary description for Database
/// </summary>
public static class CasinoDataBase
{
    static SqlConnection c = new SqlConnection("Data Source = (LocalDB)\\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\\CasinoDB.mdf; Integrated Security = True");
    public static string SignIn(string username_email, string password)
    {
        c.Open();
        SqlCommand signIn = new SqlCommand("SELECT * FROM Users WHERE (Email=@Email OR UserName=@UserName) AND Password=@Password", c);
        signIn.Parameters.AddWithValue("@Email", username_email);
        signIn.Parameters.AddWithValue("@UserName", username_email);
        signIn.Parameters.AddWithValue("@Password", password);
        SqlDataReader reader = signIn.ExecuteReader();
        if (reader.HasRows)
        {
            c.Close();
            return "Signed in";
        }
        else
        {
            c.Close();
            return "Username/email do not match password,or do not exist";
        }
    }
    public static string Register(string UserName, string Password, string FirstName, string LastName, string Email)
    {
        Random rnd = new Random();
        if (CheckExists(UserName, "UserName", "Users"))
            return "UserName already exists";
        if (CheckExists(Email, "Email", "Users"))
            return "Email already exists";
        c.Open();
        SqlCommand insert = new SqlCommand("INSERT INTO Users (UserName,Password,FirstName,LastName,Email,ConfirmationKey) VALUES (@UserName,@Password,@FirstName,@LastName,@Email,@ConfirmationKey)", c);
        insert.Parameters.AddWithValue("@UserName", UserName);
        insert.Parameters.AddWithValue("@Password", Password);
        insert.Parameters.AddWithValue("@FirstName", FirstName);
        insert.Parameters.AddWithValue("@LastName", LastName);
        insert.Parameters.AddWithValue("@Email", Email);
        int num = rnd.Next(1, 1000000000);
        insert.Parameters.AddWithValue("@ConfirmationKey", num);
        insert.ExecuteNonQuery();
        c.Close();
        SendWelcomeMail(Email, UserName);
        return "Registerd";
    }//מבצע הרשמה של משתשמש חדש
    public static bool CheckExists(string data, string dataName, string table)
    {
        c.Open();
        SqlCommand CheckExists = new SqlCommand("SELECT * FROM " + table + " WHERE " + dataName + "=@Data", c);
        CheckExists.Parameters.AddWithValue("@Data", data);
        SqlDataReader reader = CheckExists.ExecuteReader();
        if ((reader.HasRows))
        {
            c.Close();
            return true;
        }
        else
        {
            c.Close();
            return false;
        }
    }//בודק האם קיימת שורה עם הערך הניתן
    public static SqlConnection GetConnection()
    {
        return c;
    }//מחזיר את מחרוזת ההתחברות הרלוונטית
    public static void CloseConnection()
    {
        c.Close();
    }//סוגר את החיבור לשרת  
    public static string GetStringData(string primaryKey, string primaryKeyName, string column, string table)//מחזיר ערך עבור ערך יחודי בטבלה
    {
        c.Open();
        SqlCommand getData = new SqlCommand("SELECT " + column + " FROM " + table + " WHERE " + primaryKeyName + "=@PrimaryKey", c);
        getData.Parameters.AddWithValue("@PrimaryKeyName", primaryKeyName);
        getData.Parameters.AddWithValue("@PrimaryKey", primaryKey);
        string data = getData.ExecuteScalar().ToString();
        c.Close();
        return data;
    }
    public static void DeleteRows(string value, string valueName, string table)//מחיקת עמודות בטבלה ששוות לערך מסוים
    {
        c.Open();
        SqlCommand delete = new SqlCommand("DELETE FROM " + table + " WHERE " + valueName + "=@Value", c);
        delete.Parameters.AddWithValue("@Value", value);
        delete.ExecuteNonQuery();
        c.Close();
    }
    public static void ResetTable(string table)//מאפס טבלה
    {
        SqlCommand reset = new SqlCommand("TRUNCATE TABLE " + table, c);
        c.Open();
        reset.ExecuteNonQuery();
        c.Close();
    }
    public static void UpdateValue(string primaryKey, string primaryKeyName, string value, string valueName, string table)
    {
        c.Open();
        SqlCommand update = new SqlCommand("UPDATE " + table + " Set " + valueName + "=@Value WHERE " + primaryKeyName + "=@PrimaryKey", c);
        update.Parameters.AddWithValue("@Value", value);
        update.Parameters.AddWithValue("@PrimaryKey", primaryKey);
        update.ExecuteNonQuery();
        c.Close();
    }//מעדכן ערך נתון בטבלה
    public static void SendWelcomeMail(string userEmail, string userName)
    {
        MailMessage WelcomeMail = new MailMessage("hadadscasino@gmail.com", userEmail);
        SmtpClient client = new SmtpClient();
        client.Port = 587;
        client.DeliveryMethod = SmtpDeliveryMethod.Network;
        client.UseDefaultCredentials = false;
        client.Host = "smtp.gmail.com";
        client.Credentials = new System.Net.NetworkCredential("hadadscasino@gmail.com", "TheHouseAlwaysWins");
        WelcomeMail.Subject = "Welcome";
        WelcomeMail.Body = "Prepare to lose al of your money\n\n\n\n ConfirmMail:http://localhost:51940/WebFormPages/ConfirmPage.aspx?" + GetStringData(userName, "UserName", "ConfirmationKey", "Users");
        client.Send(WelcomeMail);
    }//שולח מייל ראשוני
    public static void ConfirmMail(string confirmationKey)
    {
        SqlCommand update = new SqlCommand("UPDATE Users SET IsConfirmed='true' WHERE ConfirmationKey=" + confirmationKey, c);
        c.Open();
        update.ExecuteNonQuery();
        c.Close();
    }//מאשר את המשתמש בעל המפתח הנתון
    public static List<string> GetTableHeaders(string table)
    {
        List<string> headers = new List<string>();
        c.Open();
        SqlCommand GetTable = new SqlCommand("SELECT * FROM " + table, c);
        SqlDataReader reader = GetTable.ExecuteReader();
        GridView grTable = new GridView();
        grTable.DataSource = reader;
        grTable.DataBind();
        GridViewRow headerRow = grTable.HeaderRow;
        for (int i = 0; i < headerRow.Cells.Count; i++)
            headers.Add(headerRow.Cells[i].Text);
        c.Close();
        return headers;
    }//מחזיר את שם העמודות בטבלה
    public static string CreateXMLDocForTable(string table)
    {
        XmlDocument doc = new XmlDocument();
        c.Open();
        SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM " + table, c);
        DataSet ds = new DataSet();
        XmlWriter writer = XmlWriter.Create(table + ".xml");
        da.Fill(ds, "table");
        ds.WriteXml(writer);
        writer.Close();
        c.Close();
        return table + ".xml";
    }//יוצר קובץ אקסמל לטבלה

    public static void SendMail(string userEmail, string subject, string messege)
    {
        MailMessage mail = new MailMessage("hadadscasino@gmail.com", userEmail);
        SmtpClient client = new SmtpClient();
        client.Port = 587;
        client.DeliveryMethod = SmtpDeliveryMethod.Network;
        client.UseDefaultCredentials = false;
        client.Host = "smtp.gmail.com";
        client.Credentials = new System.Net.NetworkCredential("hadadscasino@gmail.com", "TheHouseAlwaysWins");
        mail.Subject = subject;
        mail.Body = messege;
        client.Send(mail);
    }//שולח מייל
    public static void SendMail(string userEmail, string subject, string messege, string fileName)
    {
        MailMessage mail = new MailMessage("hadadscasino@gmail.com", userEmail);
        SmtpClient client = new SmtpClient();
        client.Port = 587;
        client.DeliveryMethod = SmtpDeliveryMethod.Network;
        client.UseDefaultCredentials = false;
        client.Host = "smtp.gmail.com";
        client.Credentials = new System.Net.NetworkCredential("hadadscasino@gmail.com", "TheHouseAlwaysWins");
        mail.Subject = subject;
        mail.Body = messege;
        System.Net.Mail.Attachment attachment;
        attachment = new System.Net.Mail.Attachment(fileName);
        mail.Attachments.Add(attachment);
        client.Send(mail);
    }
    public static void SendMailToAllUsers(string subject, string messege)
    {
        c.Open();
        SqlDataAdapter getEmails = new SqlDataAdapter("SELECT * From Users", c);
        DataSet ds = new DataSet();
        getEmails.Fill(ds);
        DataTable table = ds.Tables[0];
        foreach (DataRow row in table.Rows)
            SendMail(row["Email"].ToString(), subject, messege);
        c.Close();
    }//שולח מייל לכל המשתמשים
    public static void SendMailToAllUsers(string subject, string messege, string file)
    {
        c.Open();
        SqlDataAdapter getEmails = new SqlDataAdapter("SELECT * From Users", c);
        DataSet ds = new DataSet();
        getEmails.Fill(ds);
        DataTable table = ds.Tables[0];
        foreach (DataRow row in table.Rows)
            SendMail(row["Email"].ToString(), subject, messege, file);
        c.Close();
    }

    public static void CreateNewLog(string userName)
    {
       
        SqlCommand createLog = new SqlCommand("INSERT INTO LOG (UserName,EnterTime,LogNum)Values(@UserName,@EnterTime,@LogNum)", c);
        createLog.Parameters.AddWithValue("@UserName", userName);
        createLog.Parameters.AddWithValue("@EnterTime", DateTime.Now);
        createLog.Parameters.AddWithValue("@LogNum", GetLogMaxNum(userName) + 1);
        c.Open();
        createLog.ExecuteNonQuery();
        c.Close();
    }//יוצר לוג התחברות
    public static int GetLogMaxNum(string userName)
    {
        c.Open();
        SqlCommand getMax = new SqlCommand("SELECT COALESCE(MAX(LogNum),0) FROM Log WHERE UserName=@UserName", c);
        getMax.Parameters.AddWithValue("@UserName", userName);
        int num = (int)getMax.ExecuteScalar();
        c.Close();
        return num;
    }//מקבל הלוג בעל המספר המקסימלי
    public static void AddExitTime(int logNum,string userName)
    {      
        SqlCommand addExitTime = new SqlCommand("UPDATE Log SET ExitTime=@ExitTime WHERE UserName=@UserName AND LogNum=@LogNum", c);
        addExitTime.Parameters.AddWithValue("@ExitTime", DateTime.Now);
        addExitTime.Parameters.AddWithValue("@UserName", userName);
        addExitTime.Parameters.AddWithValue("@LogNum",logNum );
        c.Open();
        addExitTime.ExecuteNonQuery();
        c.Close();
    }//מוסיף זמן יציאה ללוג ההתחברות

    public static void CreateCashLog(string userName,int cash)//מוסיף לוג כסף חדש
    {
        SqlCommand cashLog=new SqlCommand("INSERT INTO CashLog (UserName,Cash,Date) VALUES (@1,@2,@3)",c);
        cashLog.Parameters.AddWithValue("@1", userName);
        cashLog.Parameters.AddWithValue("@2", cash);
        cashLog.Parameters.AddWithValue("@3", DateTime.Now.Date);
        c.Open();
        cashLog.ExecuteNonQuery();
        c.Close();
    }
    public static void CreateCashLog(string userName, int cash,DateTime date)//מוסיף לוג כסף חדש
    {
        SqlCommand cashLog = new SqlCommand("INSERT INTO CashLog (UserName,Cash,Date) VALUES (@1,@2,@3)", c);
        cashLog.Parameters.AddWithValue("@1", userName);
        cashLog.Parameters.AddWithValue("@2", cash);
        cashLog.Parameters.AddWithValue("@3", date);
        c.Open();
        cashLog.ExecuteNonQuery();
        c.Close();
    }
    public static string[,] GetUserCashLog(DateTime start,DateTime end,string userName)
    {
        int count = CountDays(start, end);
        string[,] cashLog=new string[2,count];
        for (int i = 0; i < count; i++)
        {
            cashLog[0, i] = start.Date.ToString("d/M/yy");
            SqlCommand CashLog = new SqlCommand("SELECT SUM(Cash) FROM CashLog WHERE Date=@1 AND UserName=@2", c);
            CashLog.Parameters.AddWithValue("@1", start);
            CashLog.Parameters.AddWithValue("@2", userName);
            c.Open();
            cashLog[1, i] = CashLog.ExecuteScalar().ToString();
            c.Close();
            if (cashLog[1, i] == "")
                cashLog[1, i] = "0";
            start = start.AddDays(1);
        }        
        return cashLog;
    }
    public static int CountDays(DateTime start, DateTime end)
    {
        int count = 1;
        while (start.Day != end.Day || start.Month != end.Month || start.Year != end.Year)
        {
            count++;
            start = start.AddDays(1);
        }
        return count;
    }//סופר ימים
}