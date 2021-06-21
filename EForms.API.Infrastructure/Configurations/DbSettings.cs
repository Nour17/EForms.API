namespace EForms.API.Infrastructure.Configurations
{
    public class DbSettings
    {
        private string _connectionString = string.Empty;
        public string ConnectionString
        {
            get
            {
                if (IsContained) { return Container; }
                return _connectionString;
            }
            set { _connectionString = value; }
        }
        public string Container { get; set; }
        public bool IsContained { get; set; }
        public string Database { get; set; }
    }
}
