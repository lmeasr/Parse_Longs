namespace HotChocolateParseLong
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services
                .AddGraphQLServer()
                .AddQueryType<Query>()
                .AddMutationType<Mutation>()
                .BindRuntimeType<long, SnowflakeIdType>();

            var app = builder.Build();
            app.MapGraphQL();

            app.Run();
        }
    }
}
