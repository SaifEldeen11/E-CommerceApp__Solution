namespace Shared.ErrorModels
{
    public class ValidationError
    {
        public string FieldName { get; set; } = null!;

        public IEnumerable<string> Errors { get; set; } = [];
    }
}