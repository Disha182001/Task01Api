namespace Tak01Api.Repository.Interface_Repo
{
    public class CRUD:ICRUD
    {
        private readonly IConfiguration _configuration;
        public CRUD(IConfiguration configuration)
        {
            _configuration = configuration;
        }


        public string GetString(string str1)
        {
            string str = _configuration[str1];
            return str;
        }
    }
}
