namespace Core.Ldap;

public class ReturnModel<T>
{
    //public T Data { get; set; } // orjinali
    public LdapResult Data { get; set; } // geriye LdapResult cinsinden kullanıcı bilgileri gelsin diye yazdığım property
    public ReturnTypeStatus Status { get; set; }
    public string Message { get; set; }
    public string ErrorCode { get; set; }
    public string ErrorMessage { get; set; }

}

public enum ReturnTypeStatus
{
    Error = 0,
    Success = 1,
    NotFound = 3,
    Working = 4,
    NotAuthorized = 5
}
