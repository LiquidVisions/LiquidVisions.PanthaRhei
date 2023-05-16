namespace LiquidVisions.PanthaRhei.Generated.Application.Validators
{
    internal interface IValidator<in TObjectToValidate>
        where TObjectToValidate : class
    {
        Response Validate(TObjectToValidate objectToValidate);
    }
}
