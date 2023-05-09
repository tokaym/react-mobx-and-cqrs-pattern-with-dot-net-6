using Core.Security.Jwt;

namespace Core.Ldap
{
    public class LdapResult
    {
        public AccessToken AccessToken { get; set; }
        public bool UserAuth { get; set; }
        public string UserCode { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Title { get; set; }
        public string Plant { get; set; }
        public string Department { get; set; }
        public string Email { get; set; }
        public string Telephone { get; set; }
        public string Mobile { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string AccountFirstCreate { get; set; }
        public string UserAuthMessage { get; set; }
    }
}