namespace ProfileUpdate.Infra.Context.Uow;

public interface IUnitOfWork
{
    Task CommitAsync();
}