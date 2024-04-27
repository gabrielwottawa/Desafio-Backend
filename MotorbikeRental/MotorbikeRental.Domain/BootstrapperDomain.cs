﻿using MediatR;
using Microsoft.Extensions.DependencyInjection;
using MotorbikeRental.Domain.Commands.Auth;
using MotorbikeRental.Domain.Commands.Courier;
using MotorbikeRental.Domain.Commands.Motorbike;
using MotorbikeRental.Domain.Handlers.Auth;
using MotorbikeRental.Domain.Handlers.Courier;
using MotorbikeRental.Domain.Handlers.Motorbike;
using MotorbikeRental.Domain.Responses;

namespace MotorbikeRental.Domain
{
    public static class BootstrapperDomain
    {
        public static void AddDomainBootstrapper(this IServiceCollection services)
        {
            if (services == null)
                throw new ArgumentNullException(nameof(services));

            #region AuthController
            services.AddScoped<IRequestHandler<AuthCommand, CommandResult>, AuthHandler>();
            #endregion

            #region MotorbikeController
            services.AddScoped<IRequestHandler<CreateMotorbikeCommand, CommandResult>, CreateMotorbikeHandler>();
            services.AddScoped<IRequestHandler<GetMotorbikesCommand, CommandResult>, GetMotorbikesHandler>();
            services.AddScoped<IRequestHandler<UpdateMotorbikeCommand, CommandResult>, UpdateMotorbikeHandler>();
            services.AddScoped<IRequestHandler<DeleteMotorbikeCommand, CommandResult>, DeleteMotorbikeHandler>();
            #endregion

            #region
            services.AddScoped<IRequestHandler<CreateCourierCommand, CommandResult>, CreateCourierHandler>();
            services.AddScoped<IRequestHandler<PostDocumentCourierCommand, CommandResult>, PostDocumentCourierHandler>();
            #endregion
        }
    }
}