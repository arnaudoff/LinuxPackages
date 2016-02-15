namespace LinuxPackages.Services.Data.Contracts.Savers
{
    public interface IAvatarSaver
    {
        void Save(string userId, string fileName, byte[] contents);

        byte[] Read(string userId, int avatarId, int? width = null, int? height = null);
    }
}