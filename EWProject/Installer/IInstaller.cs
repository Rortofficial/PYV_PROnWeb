namespace Client.Installer
{
    public interface IInstaller
    {
        void Installer(IServiceCollection servieCollection, IConfiguration configuration);
    }
}
