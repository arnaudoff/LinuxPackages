namespace LinuxPackages.Common.Contracts
{
    public interface IRandomGenerator
    {
        string GenerateRandomString(int length);

        int GenerateRandomNumber(int minValue, int maxValue);
    }
}
