namespace Next.api_01.Exctensions
{
    public static class CorsExtensions
    {
        public static WebApplicationBuilder MapCors(this WebApplicationBuilder builder)
        {
            builder.AddClientOne();
            return builder;
        }

        public static WebApplicationBuilder AddClientOne(this WebApplicationBuilder builder)
        {
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("CilentOne",policy =>
                {
                    policy.WithOrigins("https://localhost:7019;http://localhost:5018")
                     .AllowAnyHeader()
                     .AllowAnyMethod();
                });
            });

            return builder;
        }
    }
}
