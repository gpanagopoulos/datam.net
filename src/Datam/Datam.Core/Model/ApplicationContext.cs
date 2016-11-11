namespace Datam.Core.Model
{
    public class ApplicationContext
    {
        public string Server { get; set; }
        public string Database { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string PatchesFolder { get; set; }
        public string PatchesRegex { get; set; }
        public OperationType OperationType { get; set; }
        public string Port { get; set; }
    }
}
