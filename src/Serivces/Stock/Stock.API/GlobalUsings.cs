global using System.ComponentModel;
global using System.ComponentModel.DataAnnotations;
global using System.Diagnostics;
global using System.Text.Json.Serialization;
global using System.Xml.Serialization;
global using AutoMapper;
global using Stock.API.SyncDataServices.Grps;
global using Google.Protobuf.WellKnownTypes;
global using System.Reflection;

global using Grpc.Core;
global using Grpc.Net.Client;

global using Microsoft.AspNetCore;
global using Microsoft.AspNetCore.Mvc;
global using Microsoft.AspNetCore.Mvc.Filters;
global using Microsoft.Extensions.Options;
global using Microsoft.OpenApi.Models;

global using Newtonsoft.Json.Converters;

global using Serilog;

global using Stock.API.Configuration;
global using Stock.API.DTOs;
global using Stock.API.Model;
global using Stock.API.SyncDataServices.Soap;
global using Stock.API.Infrastracture.Enums;
global using Stock.API.Infrastracture;
global using Stock.API.Infrastracture.Exceptions;
global using Stock.API.Infrastracture.Extensions;

global using CentralBankSDK;
global using CentralBankSDK.Model;
global using CentralBankSDK.Model.CursOnDateResponse;

global using Tinkoff.InvestApi.V1;
global using Stock.API.SyncDataServices;