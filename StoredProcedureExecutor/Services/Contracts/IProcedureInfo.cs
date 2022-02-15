namespace StoredProcedureExecutor.Services.Contracts
{
    public interface IProcedureInfo
    {
        public string Server { get; set; }
        public string Database { get; set; }
        public string Schema { get; set; }
        public string Name { get; set; }
    }
}
