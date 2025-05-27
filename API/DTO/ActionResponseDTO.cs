namespace API.DTO;

public class ActionResponseDTO<T>
{
    public bool WasSuccess { get; set; }

    public string? Message { get; set; }

    public T? Result { get; set; }
}