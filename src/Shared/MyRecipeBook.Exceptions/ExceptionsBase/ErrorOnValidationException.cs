namespace MyRecipeBook.Exceptions.ExceptionsBase;

public class ErrorOnValidationException : MyRecipeBookException
{
    public IList<string> ErrorMessages { get; set; }

    public ErrorOnValidationException(List<string> errorMessages)
    {
        ErrorMessages = errorMessages;
    }
}