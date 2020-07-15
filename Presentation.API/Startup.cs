using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Contracts;
using DAL.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Domain.Contracts.Repositories;
using Domain.Contracts.Services;
using Domain.Services;
using AutoMapper;

namespace GestionCartera.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            services.AddSingleton<IClienteRepository, ClienteRepository>();
            services.AddSingleton<ICreditoRepository, CreditoRepository>();
            services.AddSingleton<ICarteraRepository, CarteraRepository>();
            services.AddSingleton<IFondeadorRepository, FondeadorRepository>();
            services.AddSingleton<IUsuarioRepository, UsuarioRepository>();
            services.AddSingleton<IReporteRepository, ReporteRepository>();
            services.AddSingleton<IProductoRepository, ProductoRepository>();
            services.AddSingleton<IRecompraRepository,  RecompraRepository>();
            services.AddSingleton<IPagoRepository,      PagoRepository>();
            services.AddSingleton<ICronogramaRepository, CronogramaRepository>();
            services.AddSingleton<IAmortizacionRepository, AmotizacionRepository>();
            services.AddSingleton<ICuotaRepository, CuotaRepository>();

            services.AddSingleton<IClienteService, ClienteService>();
            services.AddSingleton<ICreditoService, CreditoService>();
            services.AddSingleton<ICarteraService, CarteraService>();
            services.AddSingleton<IFondeadorService, FondeadorService>();
            services.AddSingleton<IUsuarioService, UsuarioService>();
            services.AddSingleton<IReporteService, ReporteService>();
            services.AddSingleton<IProductoService, ProductoService>();
            services.AddSingleton<IRecompraService,  RecompraService>();
            services.AddSingleton<IPagoService,  PagoService>();
            services.AddSingleton<ICronogramaService, CronogramaService>();
            services.AddSingleton<IAmortizacionService, AmortizacionService>();
            services.AddSingleton<ICuotaService, CuotaService>();

            services.AddCors(options => {
                options.AddPolicy("default", builder => {
                    builder.AllowAnyOrigin();
                    builder.AllowAnyMethod();
                    builder.AllowAnyHeader();
                });
            });

            services.AddAutoMapper(typeof(AutoMapping));

            OpenApiInfo info = new OpenApiInfo { 
                Title = "Gestión de Cartera. API", 
                Version = "v1" 
            };

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", info);
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();
            
            app.UseCors("default");

            app.UseAuthorization();
            
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
            
            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("v1/swagger.json", "Palante API");
            });

        }
    }
}
