namespace LinuxPackages.Web.Mvc.Infrastructure.Helpers.Contracts
{
    public interface IUrlIdentifierProvider
    {
        string GenerateIdentifierProviderSalt(int size);

        string EncodeEntityId(int entityId);

        int DecodeEntityId(string urlHash);

        bool IsHashValid(string urlHash);
    }
}
