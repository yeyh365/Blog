namespace Blog.Domain.Repositories
{
    public interface IEntitiy<TPrimaryKey>
    {
        TPrimaryKey Id { get; set; }
    }
}