namespace ProfileUpdate.Infra.Repository.Users;

public class UpdateUserRepository : UpdateRepositoryBase<User>, IUpdateUserRepository
{
    public UpdateUserRepository(DatabaseContext databaseContext, ILogger<UpdateRepositoryBase<User>> logger) : base(databaseContext, logger)
    {
    }
}