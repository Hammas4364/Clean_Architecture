using SharedKernel.Constants;
namespace SharedKernel.Exceptions;

public static class RepositoryExceptions
{
    public static AppException NotFoundException<TEntity>() => new AppException(typeof(TEntity).Name + Messages.CommonMessages.NotFound);
    public static AppException AlreadyExistException<TEntity>() => new AppException(typeof(TEntity).Name + Messages.CommonMessages.AlreadyExist);
}
