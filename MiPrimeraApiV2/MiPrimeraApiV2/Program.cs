using System.Xml;

namespace MiPrimeraApiV2
{
    public class Program
    {



        public static void Main(string[] args)
        {
            Console.WriteLine("********************************************************************************");
            Console.WriteLine("" +
                " ALUMNO = BORDA ALESANDRO PATRICIO\r\n" +
                " TUTOR = RAUL AHUMADA\r\n" +
                " PROFESOR = JOSUE BURBANO\r\n" +
                " FECHA = 22/02/2023");
            Console.WriteLine("********************************************************************************");


            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            builder.Services.AddCors(policy =>
            {
                policy.AddDefaultPolicy(options =>
                {
                    options.AllowAnyHeader().AllowAnyOrigin().AllowAnyMethod();
                });
            });

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            app.UseCors();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}