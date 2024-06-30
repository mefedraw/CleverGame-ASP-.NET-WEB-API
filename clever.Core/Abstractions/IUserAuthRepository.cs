namespace clever.Core.Abstractions;

public interface IUserAuthRepository
{
    Task AuthUser(string tgId, string tgUsername);

    bool UserExists(string tgId);

   // Task FullAuth(string tgId);
} 
