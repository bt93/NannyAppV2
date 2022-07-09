using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using NannyAPI.DependencyInjection;
using NannyAPI.GraphQL;
using NannyAPI.GraphQL.Addresses;
using NannyAPI.GraphQL.Children;
using NannyAPI.GraphQL.Roles;
using NannyAPI.GraphQL.Sessions;
using NannyAPI.GraphQL.Users;
using NannyAPI.Miscellaneous.Errors;
using System.Text;
using static NannyAPI.GraphQL.Users.UserType;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(builder =>
    {
        builder.AllowAnyOrigin();
    });
});

builder.Services.AddAuthorization();

var plainSecret = builder.Configuration["JWTSecret"];
var jwtSecret = Encoding.ASCII.GetBytes(plainSecret);
builder.Services
    .AddAuthentication(x =>
    {
        x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(x =>
    {
        x.RequireHttpsMetadata = builder.Environment.IsDevelopment() ? false : true;
        x.SaveToken = true;
        x.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(jwtSecret),
            ValidateIssuer = builder.Environment.IsDevelopment() ? false : true,
            ValidateAudience = builder.Environment.IsDevelopment() ? false : true,
            NameClaimType = "name"
        };
    });


builder.Services
    .AddRepositories(builder.Configuration, plainSecret)
    .AddGraphQLServer()
    .AddAuthorization()
    .AddQueryType<Query>()
    .AddMutationType<Mutation>()
    .AddType<UserQueries>()
    .AddType<UserMutations>()
    .AddType<AddressQueries>()
    .AddType<UserType>()
    .AddType<AddressType>()
    .AddType<ChildType>()
    .AddType<ChildQueries>()
    .AddType<SessionQueries>()
    .AddType<SessionType>()
    .AddType<RoleQueries>()
    .AddType<GenderType>()
    .AddErrorFilter<GraphQLErrorFilter>()
    .AddFiltering()
    .AddSorting();



var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.UseRouting();

app.UseCors();

app.UseAuthentication();

app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapGraphQL();
});

app.UseGraphQLVoyager(path: "/schema");

app.Run();
