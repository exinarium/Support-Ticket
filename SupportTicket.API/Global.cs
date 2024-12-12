global using Microsoft.AspNetCore.Mvc;
global using Microsoft.AspNetCore.Authentication.JwtBearer;
global using Microsoft.EntityFrameworkCore;
global using Microsoft.EntityFrameworkCore.Metadata.Builders;
global using Microsoft.Extensions.Options;
global using Microsoft.IdentityModel.Tokens;

global using System.Net;
global using System.Net.Mail;
global using System.Net.Mime;
global using System.Text.Json;
global using System.Security.Cryptography;
global using System.ComponentModel.DataAnnotations;
global using System.ComponentModel.DataAnnotations.Schema;
global using System.Globalization;
global using System.IdentityModel.Tokens.Jwt;
global using System.Security.Claims;
global using System.Text;

global using Hangfire;
global using Hangfire.PostgreSql;

global using GraphQL;
global using GraphQL.Types;

global using SupportTicket.SDK.Enums;
global using SupportTicket.SDK.Models;
global using SupportTicket.SDK.Models.Requests.Auth;

global using SupportTicket.API.Domain.Config;
global using SupportTicket.API.Domain.GraphQL;
global using SupportTicket.API.Domain.Helpers;
global using SupportTicket.API.Domain.Repository.Models;
global using SupportTicket.API.Domain.Services;
global using SupportTicket.API.Domain.Services.Account;
global using SupportTicket.API.Domain.Services.Auth;
global using SupportTicket.API.Domain.Services.Base;
global using SupportTicket.API.Domain.Services.Email;
global using SupportTicket.API.Domain.Services.User;