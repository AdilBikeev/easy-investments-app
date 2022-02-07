global using Autofac;
global using Autofac.Extensions.DependencyInjection;
global using AutoMapper;

global using Microsoft.AspNetCore;
global using Microsoft.AspNetCore.Mvc;
global using Microsoft.AspNetCore.Server.Kestrel.Core;
global using Microsoft.OpenApi.Models;
global using Microsoft.Extensions.Options;

global using Serilog;
global using System.Net;

global using Grpc.Core;
global using Grpc.Net.Client;
global using Google.Protobuf.WellKnownTypes;

global using Stock.API.Configuration;
global using Stock.API.DTOs;
global using Stock.API.Infrastracture.Enums;
global using Stock.API.Infrastracture.Helpers;

global using Tinkoff.InvestApi.V1;

global using Newtonsoft.Json.Converters;
global using System.ComponentModel;
global using System.ComponentModel.DataAnnotations;
global using System.Text.Json.Serialization;