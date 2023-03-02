using System.ComponentModel.DataAnnotations;

namespace UserMicroservice.ExtraAttributes;

[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter)]
public class OneOfAttribute : ValidationAttribute
{
    private readonly string[] _allowedValues;

    public OneOfAttribute(params string[] allowedValues)
    {
        _allowedValues = allowedValues ?? throw new ArgumentNullException(nameof(allowedValues));
    }

    public override bool IsValid(object value)
    {
        if (value == null)
        {
            return true;
        }

        if (value is not string stringValue)
        {
            return false;
        }

        return _allowedValues.Contains(stringValue);
    }

    public override string FormatErrorMessage(string name)
    {
        return $"{name} should be one of [{string.Join(", ", _allowedValues.Select(v => $"'{v}'"))}].";
    }
}
