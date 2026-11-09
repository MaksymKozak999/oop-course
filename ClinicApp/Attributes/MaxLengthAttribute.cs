namespace ClinicApp.Attributes;

[AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
sealed class MaxLengthAttribute : Attribute
{
    public int Length {get;}
    public string ErrorMessage {get;}

    public MaxLengthAttribute(int length, string errorMessage = "")
    {
        this.Length = length;
        this.ErrorMessage = errorMessage;
    }

}