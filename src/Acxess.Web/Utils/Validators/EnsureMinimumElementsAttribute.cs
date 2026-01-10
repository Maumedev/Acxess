using System.ComponentModel.DataAnnotations;
using System.Collections;
public class EnsureMinimumElementsAttribute : ValidationAttribute
{
    private readonly int _minElements;
    public EnsureMinimumElementsAttribute(int minElements)
    {
        _minElements = minElements;
    }


    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {

        var list = value as IList;

        if (list == null)
        {
            return ValidationResult.Success; 
        }

        if (list.Count < _minElements)
        {
            return new ValidationResult(ErrorMessage ?? $"Debes seleccionar al menos {_minElements} opción.");
        }

        return ValidationResult.Success;
    }

}