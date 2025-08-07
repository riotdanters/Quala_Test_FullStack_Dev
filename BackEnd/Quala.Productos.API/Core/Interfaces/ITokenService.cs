namespace Quala.Productos.API.Core.Interfaces
{
    public interface ITokenService
    {
        string GenerateToken(string userName);
    }
}
