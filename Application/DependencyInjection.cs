﻿using Application.Interfaces;
using Application.Services;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Application.Resources;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore;
using NLog;
using Application.Middleware;
using Microsoft.AspNetCore.Authorization;
using Application.Resources.Authentication;

namespace Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services, IConfiguration config)
        {

            var authenticationSettings = new AuthenticationSettings();
            config.GetSection("JWTConfig").Bind(authenticationSettings);

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = "Bearer";
                options.DefaultScheme = "Bearer";
                options.DefaultChallengeScheme = "Bearer";
            }).AddJwtBearer(cfg =>
            {
                cfg.RequireHttpsMetadata = false;
                cfg.SaveToken = true;
                cfg.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidIssuer = authenticationSettings.JWTIssuer,
                    ValidAudience = authenticationSettings.JWTIssuer,
                    IssuerSigningKey = new SymmetricSecurityKey (Encoding.UTF8.GetBytes (authenticationSettings.Secret)),
                };
            });

            /*services.AddAuthorization(options =>
            {
                options.add
            });*/
            
            services.AddSingleton(authenticationSettings);

            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            //services.AddScoped<IAuthorizationHandler, MinimumAgeRequirementsHandler>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IRoleService, RoleService>();
            services.AddScoped<IPostService, PostService>();

            services.AddScoped<IPasswordHasher<User>,PasswordHasher<User>>();

            services.AddScoped<ErrorHandlingMiddleware>();

            return services;
        }
    }
}
