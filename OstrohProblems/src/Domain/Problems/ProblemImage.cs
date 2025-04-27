namespace Domain.Problems;

public class ProblemImage
{
    public ProblemImageId Id { get; }
    public Problem? Problem { get; }
    public ProblemId ProblemId { get; }
    public string FilePath { get; private set; }

    private ProblemImage(ProblemImageId id, ProblemId productId, string filePath)
    {
        Id = id; 
        ProblemId = productId;
        FilePath = filePath;
    }

    public static ProblemImage New(ProblemImageId id, ProblemId productId, string filePath) 
        => new ProblemImage(id, productId, filePath);
}