
namespace efscaffold.Entities;

public partial class Profiler
{
    public int Id { get; set; }

    public string Firstname { get; set; } = null!;

    public string Lastname { get; set; } = null!;

    public int Age { get; set; }

    public string Occupation { get; set; } = null!;

    public string City { get; set; } = null!;

    public string? Photourl { get; set; }

    public DateTime? Createdat { get; set; }

    public DateTime? Updatedat { get; set; }
}
