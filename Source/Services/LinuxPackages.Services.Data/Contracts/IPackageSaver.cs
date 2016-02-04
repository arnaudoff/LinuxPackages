namespace LinuxPackages.Services.Data.Contracts
{
    public interface IPackageSaver
    {
        void Save(int id, string fileName, byte[] contents);
    }
}
