using NannyModels.Enumerations;
using NannyModels.Models;

namespace NannyAPI.GraphQL.Children
{
    public class ChildType : ObjectType<Child>
    {
        protected override void Configure(IObjectTypeDescriptor<Child> descriptor)
        {
            descriptor
             .Field("genderID")
             .Argument("genderID", a => a.Type<GenderType>())
             .Resolve(context => context.ArgumentValue<Gender>("genderID"));
        }
    }

    public class Resolvers
    {

    }

    public class GenderType : EnumType<Gender> { }
}
