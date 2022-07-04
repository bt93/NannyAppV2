namespace NannyAPI.Miscellaneous.Errors
{
    public class GraphQLErrorFilter : IErrorFilter
    {
        public IError OnError(IError error)
        {
            return error.WithMessage(error.Exception?.Message ?? string.Empty);
        }
    }
}
