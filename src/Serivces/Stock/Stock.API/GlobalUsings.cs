global using System.ComponentModel;
global using System.ComponentModel.DataAnnotations;
global using System.Diagnostics;
global using System.Net;
global using System.Reflection;
global using System.Xml.Serialization;

global using Autofac;

global using AutoMapper;

global using CentralBankSDK;
global using CentralBankSDK.Model.CursOnDateResponse;

global using Google.Protobuf.Collections;
global using Google.Protobuf.WellKnownTypes;

global using MediatR;

global using Microsoft.AspNetCore;
global using Microsoft.AspNetCore.Mvc;
global using Microsoft.AspNetCore.Mvc.ApiExplorer;
global using Microsoft.AspNetCore.Mvc.Filters;
global using Microsoft.Extensions.Options;

global using Newtonsoft.Json.Converters;

global using Serilog;

global using Tinkoff.InvestApi.V1;
global using FluentValidation;