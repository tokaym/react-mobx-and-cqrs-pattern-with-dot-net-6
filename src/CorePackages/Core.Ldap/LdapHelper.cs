using System.DirectoryServices;
using Core.Ldap;
using Microsoft.VisualBasic;

namespace Core.LDAP;


public class LdapUserHelper
{
    public static object dsObject, DSUser;
    public static string strBase = "url";
    public static string basePath = "basePath";
    public static readonly string baseUser = "";

    public static string LdapLogin(object UserID, object Passwd)
    {


        // dsObject = Interaction.GetObject("LDAP:");

        string GetLDAPUserInfo = "1";

        var first = UserID.ToString().First().ToString().ToLower();
        string ou = string.Empty;
        switch (first)
        {
            case "c":
                ou = "contractors";
                break;
            case "a":
            default:
                ou = "employees";
                break;
        }
        var user = string.Format(baseUser, UserID, ou);

        try
        {
            DirectoryEntry entry = new DirectoryEntry(basePath, user, Passwd.ToString(), 0);
            object nativeObject = entry.NativeObject;
        }

        catch (Exception ed)
        {
            GetLDAPUserInfo = "-2";
        }
        return GetLDAPUserInfo;
    }
    public static LdapResult GetLDAPUserInfo(String UserCode, String Password, Boolean GetUserDetail = false)
    {
        LdapResult result = new LdapResult();
        //string GetLDAPUserInfo2 = "1";

        // dsObject = Interaction.GetObject("LDAP:");
        string strDSUserPath = "", strUserDN = "", StrBase1 = "";

        //Call Function to serach LDAP to find UserDN. strUid = AR002017
        strDSUserPath = Find_DSuserDN(UserCode);

        if ((strDSUserPath).Trim().Length <= 3)
        {
            result.UserAuth = false;
            //if (Convert.ToInt32(strDSUserPath) < -51)
            //{
            //    GetLDAPUserInfo2 = "-3";
            //}                    
            //else
            //{
            //    GetLDAPUserInfo2 = "-1";
            //}                    
        }

        if (strDSUserPath.ToUpper().IndexOf("EMPLOYEES") > 1)  //'Or InStr(strDSUserPath, "employees") > 1
        {
            if (Microsoft.VisualBasic.Strings.Left(UserCode, 2) == "AR")
            // if (UserCode.Substring(0, 2) == "AR")
            {
                strUserDN = Microsoft.VisualBasic.Strings.Mid(strDSUserPath, Microsoft.VisualBasic.Strings.InStr(Microsoft.VisualBasic.Strings.LCase(strDSUserPath), "uid"));
                StrBase1 = Microsoft.VisualBasic.Strings.Left(strDSUserPath, Microsoft.VisualBasic.Strings.InStr(Microsoft.VisualBasic.Strings.LCase(strDSUserPath), "uid") - 2);
                // strUserDN = strDSUserPath.Substring(strDSUserPath.ToLower().IndexOf("uid"));
                // StrBase1 = strDSUserPath.Substring(strDSUserPath.ToLower().IndexOf("uid") - 2);
            }
            else
            {
                strUserDN = Microsoft.VisualBasic.Strings.Mid(strDSUserPath, Microsoft.VisualBasic.Strings.InStr(Microsoft.VisualBasic.Strings.LCase(strDSUserPath), "uid"));
                StrBase1 = Microsoft.VisualBasic.Strings.Left(strDSUserPath, Microsoft.VisualBasic.Strings.InStr(Microsoft.VisualBasic.Strings.LCase(strDSUserPath), "uid") - 2);
                // strUserDN = strDSUserPath.Substring(strDSUserPath.ToLower().IndexOf("uid"));
                // StrBase1 = strDSUserPath.Substring(strDSUserPath.ToLower().IndexOf("uid") - 2);
            }
        }
        else
        {
            // if (Microsoft.VisualBasic.Strings.InStr(Microsoft.VisualBasic.Strings.UCase(strDSUserPath), "CONTRACTORS") > 1)
            if (strDSUserPath.ToUpper().IndexOf("CONTRACTORS") > 1)
            {
                strUserDN = Microsoft.VisualBasic.Strings.Mid(strDSUserPath, Microsoft.VisualBasic.Strings.InStr(Microsoft.VisualBasic.Strings.LCase(strDSUserPath), "uid"));
                StrBase1 = Microsoft.VisualBasic.Strings.Left(strDSUserPath, Microsoft.VisualBasic.Strings.InStr(Microsoft.VisualBasic.Strings.LCase(strDSUserPath), "uid") - 2);
                // strUserDN = strDSUserPath.Substring(strDSUserPath.ToLower().IndexOf("uid"));
                // StrBase1 = strDSUserPath.Substring(strDSUserPath.ToLower().IndexOf("uid") - 2);
            }
            else
            {
                //GetLDAPUserInfo2 = "-1";
                result.UserAuth = false;
            }
        }

        try
        {
            DirectoryEntry entry = new DirectoryEntry(StrBase1, strUserDN, Password, 0);
            object nativeObject = entry.NativeObject;
            result.UserAuth = true;
            object UserInfo = null;
            if (GetUserDetail)
            {
                //DirectorySearcher Dsearch = new DirectorySearcher(entry);
                DirectoryEntry detailEntry = new DirectoryEntry();
                DirectorySearcher Dsearch = new DirectorySearcher(detailEntry);
                Dsearch.Filter = "(&(objectClass=user)(cn=" + UserCode + "))";

                foreach (SearchResult sResultSet in Dsearch.FindAll())
                {
                    result.UserCode = GetProperty(sResultSet, "cn");
                    result.Name = GetProperty(sResultSet, "givenName");
                    result.Surname = GetProperty(sResultSet, "sn");
                    result.Title = GetProperty(sResultSet, "title");
                    result.Plant = GetProperty(sResultSet, "physicaldeliveryofficename");
                    result.Department = GetProperty(sResultSet, "department");
                    result.Email = GetProperty(sResultSet, "mail");
                    result.Telephone = GetProperty(sResultSet, "telephoneNumber");
                    result.Mobile = GetProperty(sResultSet, "mobile");
                    result.Country = GetProperty(sResultSet, "c");
                    result.City = GetProperty(sResultSet, "l");
                    result.AccountFirstCreate = GetProperty(sResultSet, "whenCreated");
                }
            }

            if (Strings.InStr(Strings.UCase(strDSUserPath), "CONTRACTORS") > 1)
            // if (strDSUserPath.ToUpper().IndexOf("CONTRACTORS") > 1)
            {
            }
            else
            {

            }
        }

        catch (Exception ex)
        {
            //GetLDAPUserInfo2 = "-2";
            result.UserAuth = false;
            result.UserAuthMessage = ex.Message;
        }
        return result;
    }

    public static string Find_DSuserDN(string strUser)
    {
        string Find_DSuserDN = "";

        try
        {
            object ret;
            ADODB.Connection adoConnection = new ADODB.Connection();
            ADODB.Recordset dsRecordset = new ADODB.Recordset();
            string strFilter;
            object adStateOpen = 1;

            adoConnection.Provider = "ADsDSOObject";
            adoConnection.Open("", "", "");

            if (adoConnection != adStateOpen)
                Find_DSuserDN = "-99";

            strFilter = "(uid=" + strUser + ")";

            try
            {
                dsRecordset = adoConnection.Execute(strBase + ";" + strFilter + ";AdsPath;SubTree", out ret, 0);
            }
            catch (Exception ex)
            {
                Find_DSuserDN = "-98";
            }

            if (dsRecordset.EOF)
            {
                adoConnection.Close();
                Find_DSuserDN = "-50";
            }
            else
            {
                // '--- if user exist, get user's full dn
                Find_DSuserDN = dsRecordset.Fields[0].Value.ToString();
            }

            // '--- we don't need ADO anymore. Close it.
            adoConnection.Close();
        }
        catch
        {
            Find_DSuserDN = "-1";
        }

        return Find_DSuserDN;
    }

    public static string GetProperty(SearchResult searchResult, string PropertyName)
    {
        if (searchResult.Properties.Contains(PropertyName))
        {
            return searchResult.Properties[PropertyName][0].ToString();
        }
        else
        {
            return string.Empty;
        }
    }
}

