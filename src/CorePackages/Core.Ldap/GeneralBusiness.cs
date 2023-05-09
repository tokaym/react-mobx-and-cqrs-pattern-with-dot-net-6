using Core.LDAP;

namespace Core.Ldap;

public class GeneralBusiness : IDisposable
{
    // SI_In_GetProducts_syncClient _mdmService = new SI_In_GetProducts_syncClient();
    // string _AzureLink = "";
    // string _AzureUser = "";
    // string _AzurePassword = "";
    // bool _AzureDisabled = false;
    // bool _AwsDisabled = false;

    public GeneralBusiness()
    {
        // _AzureLink = ConfigurationSettings.AppSettings["AzureLink"]?.ToString();
        // _AzureUser = ConfigurationSettings.AppSettings["AzureUser"]?.ToString();
        // _AzurePassword = ConfigurationSettings.AppSettings["AzurePassword"]?.ToString();

        // Boolean.TryParse(ConfigurationSettings.AppSettings["AzureDisabled"].ToString(), out _AzureDisabled);
        // Boolean.TryParse(ConfigurationSettings.AppSettings["AwsDisabled"].ToString(), out _AwsDisabled);
    }


    public ReturnModel<bool> LdapLogin(string userName, string password)
    {
        ReturnModel<bool> result = new ReturnModel<bool>();
        try
        {
            var aut = LdapUserHelper.LdapLogin(userName, password);

            switch (aut)
            {
                case "1":
                    // result.Data = true; // orjinali
                    result.Data.UserAuth = true; // Result içindeki Datanın tipini booldan ldapuserresult çevirdiğimden ldapuser classı içindeki girişin başarılı olduğu sonucunu döndürdüğünü düşündüğüm UserAuth özelliğini true yapıyorum.
                    result.Status = ReturnTypeStatus.Success;
                    break;
                case "-3":
                case "-1":
                    result.Status = ReturnTypeStatus.NotAuthorized;
                    result.Message = ErrorMessagesConstant.UserNotFound;
                    break;
                case "-2":
                    result.Status = ReturnTypeStatus.NotAuthorized;
                    result.Message = ErrorMessagesConstant.WrongPassword;
                    break;
                default:
                    break;
            }
        }
        catch (Exception ex)
        {
            result.ErrorCode = ErrorCodesConstant.UnexpectedException;
            result.ErrorMessage = ex.ToString();
        }
        return result;
    }



    // private string BuildQueryString(string startDate, string endDate, string plantCode, string applianceStockCode, string applianceSerialBarcode, string connectedCardType, string connectedCardBarcode, string connectivity, string brand, string shipment, string applianceType)
    // {
    //     string query = "?startDate={0}&endDate={1}&plantCode={2}";
    //     query = string.Format(query, startDate, endDate, plantCode);
    //     if (!string.IsNullOrWhiteSpace(applianceStockCode))
    //     {
    //         query += "&applianceStockCode=" + applianceStockCode;
    //     }

    //     if (!string.IsNullOrWhiteSpace(applianceSerialBarcode))
    //     {
    //         query += "&applianceSerialNo=" + applianceSerialBarcode;
    //     }
    //     if (!string.IsNullOrWhiteSpace(connectedCardType))
    //     {
    //         query += "&connectedCardType=" + connectedCardType;
    //     }
    //     if (!string.IsNullOrWhiteSpace(connectedCardBarcode))
    //     {
    //         query += "&connectedCardBarcode=" + connectedCardBarcode;
    //     }

    //     if (!string.IsNullOrWhiteSpace(connectivity))
    //     {
    //         query += "&connectivity=" + connectivity;
    //     }
    //     if (!string.IsNullOrWhiteSpace(brand))
    //     {
    //         query += "&brand=" + brand;
    //     }
    //     if (!string.IsNullOrWhiteSpace(shipment))
    //     {
    //         query += "&shipment=" + shipment;
    //     }
    //     if (!string.IsNullOrWhiteSpace(applianceType))
    //     {
    //         query += "&applianceType=" + applianceType;
    //     }
    //     return query;
    // }

    public void Dispose()
    {

    }
}
