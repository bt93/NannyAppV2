using NannyModels.Enumerations;
using NannyModels.Models.ChildModels;

namespace NannyAPI.GraphQL.Children
{
    public class ChildType : ObjectType<Child>
    {
        protected override void Configure(IObjectTypeDescriptor<Child> descriptor)
        {
            descriptor.Description("The children on file");
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
