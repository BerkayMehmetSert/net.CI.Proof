namespace Api.Application.Request;

public class DeleteProductRequest
{
    public int Id { get; private set; }
    
    public DeleteProductRequest(int id)
    {
        Id = id;
    }
}