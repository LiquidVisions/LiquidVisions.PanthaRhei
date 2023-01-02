using NS.Api;
using Microsoft.AspNetCore.Builder;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddApi(builder.Configuration);

builder.Build()
    .RunApi();
