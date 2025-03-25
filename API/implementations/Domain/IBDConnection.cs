namespace API.implementations.Domain
{
    public interface IBDConnection
    {

        public string IP { get; set; }
        public string Pass { get; set; }
        public string User { get; set; }
        public string DBName { get; set; }
        public int Port { get; set; }

        public void Connect();
        public void Close();


    }
}
