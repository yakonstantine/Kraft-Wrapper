namespace KraftWrapper.Interfaces
{
    public interface ISitecoreFactory
    {
        ISitecoreDatabase GetDatabase(string databaseName);
        ISitecoreDatabase GetDatabase(string databaseName, bool assert);
    }
}
