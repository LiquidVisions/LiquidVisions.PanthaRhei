using System.Collections.Generic;
using System.Linq;

namespace LiquidVisions.PanthaRhei.Generated.Application
{
    public sealed class Response
    {
        private readonly ICollection<Error> errors = new List<Error>();
        private object parameter;

        public bool IsValid
            => !errors.Any();

        public IReadOnlyCollection<Error> Errors
            => errors as IReadOnlyCollection<Error>;

        public void AddError(ErrorCode code, string message)
            => errors.Add(new Error { ErrorCode = code, Message = message });

        public TParam GetParameter<TParam>() => (TParam)parameter;

        public void SetParameter<TParam>(TParam param) => parameter = param;
    }
}
