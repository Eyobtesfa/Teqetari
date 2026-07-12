public class ModelValidationException : Exception
{
    public string FieldName { get; }
    public object AttemptedValue { get; }

    public ModelValidationException(string fieldName, object attemptedValue, string message) : base(message)
    {
        FieldName = fieldName;
        AttemptedValue = attemptedValue;
    }
    public ModelValidationException(string fieldName, object attemptedValue, string message, Exception innerException) : base(message, innerException)
    {
        FieldName = fieldName;
        AttemptedValue = attemptedValue;
    }
}
public class InvalidModelFieldException : ModelValidationException
{
    public InvalidModelFieldException(string fieldName, object attemptedValue, string message)
    : base(fieldName, attemptedValue, $"Invalid value for field '{fieldName}' ['{attemptedValue}']: {message}")
    {
    }
}

public class ValueOutOfRangeException : ModelValidationException
{
    public ValueOutOfRangeException(string fieldName, object attemptedValue, string message)
    : base(fieldName, attemptedValue, $"Value out of range for field '{fieldName}' ['{attemptedValue}']: {message}")
    {
    }
}