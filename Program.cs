using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using SistemaGestionInventario.Data;
using SistemaGestionInventario.Services.Security;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorPages();
builder.Services.AddDbContext<SistemaGestionInventarioContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("SistemaGestionInventarioContext") ?? throw new InvalidOperationException("Connection string 'SistemaGestionInventarioContext' not found.")));

//builder.Services.AddSingleton<IAuthorizationPolicyProvider, PermissionPolicyProvider>();
builder.Services.AddSingleton<IAuthorizationHandler, PermissionAuthorizationHandler>();

builder.Services.AddDistributedMemoryCache();

builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(60);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/auth/login";
        options.ExpireTimeSpan = TimeSpan.FromMinutes(60);
    });

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("Permission.ART_VIEW", policy =>
        policy.Requirements.Add(new PermissionRequirement(new[] { "ART_VIEW" })));
    options.AddPolicy("Permission.ART_EDIT", policy =>
        policy.Requirements.Add(new PermissionRequirement(new[] { "ART_EDIT" })));
    options.AddPolicy("Permission.ART_CREATE", policy =>
        policy.Requirements.Add(new PermissionRequirement(new[] { "ART_CREATE" })));
    options.AddPolicy("Permission.ART_DELETE", policy =>
        policy.Requirements.Add(new PermissionRequirement(new[] { "ART_DELETE" })));

    options.AddPolicy("Permission.INVTYPE_VIEW", policy =>
        policy.Requirements.Add(new PermissionRequirement(new[] { "INVTYPE_VIEW" })));
    options.AddPolicy("Permission.INVTYPE_EDIT", policy =>
        policy.Requirements.Add(new PermissionRequirement(new[] { "INVTYPE_EDIT" })));
    options.AddPolicy("Permission.INVTYPE_CREATE", policy =>
        policy.Requirements.Add(new PermissionRequirement(new[] { "INVTYPE_CREATE" })));
    options.AddPolicy("Permission.INVTYPE_DELETE", policy =>
        policy.Requirements.Add(new PermissionRequirement(new[] { "INVTYPE_DELETE" })));

    options.AddPolicy("Permission.WH_VIEW", policy =>
        policy.Requirements.Add(new PermissionRequirement(new[] { "WH_VIEW" })));
    options.AddPolicy("Permission.WH_EDIT", policy =>
        policy.Requirements.Add(new PermissionRequirement(new[] { "WH_EDIT" })));
    options.AddPolicy("Permission.WH_CREATE", policy =>
        policy.Requirements.Add(new PermissionRequirement(new[] { "WH_CREATE" })));
    options.AddPolicy("Permission.WH_DELETE", policy =>
        policy.Requirements.Add(new PermissionRequirement(new[] { "WH_DELETE" })));

    options.AddPolicy("Permission.USR_VIEW", policy =>
        policy.Requirements.Add(new PermissionRequirement(new[] { "USR_VIEW" })));
    options.AddPolicy("Permission.USR_EDIT", policy =>
        policy.Requirements.Add(new PermissionRequirement(new[] { "USR_EDIT" })));
    options.AddPolicy("Permission.USR_CREATE", policy =>
        policy.Requirements.Add(new PermissionRequirement(new[] { "USR_CREATE" })));
    options.AddPolicy("Permission.USR_DELETE", policy =>
        policy.Requirements.Add(new PermissionRequirement(new[] { "USR_DELETE" })));

    options.AddPolicy("Permission.ROLE_VIEW", policy =>
        policy.Requirements.Add(new PermissionRequirement(new[] { "ROLE_VIEW" })));
    options.AddPolicy("Permission.ROLE_EDIT", policy =>
        policy.Requirements.Add(new PermissionRequirement(new[] { "ROLE_EDIT" })));
    options.AddPolicy("Permission.ROLE_CREATE", policy =>
        policy.Requirements.Add(new PermissionRequirement(new[] { "ROLE_CREATE" })));
    options.AddPolicy("Permission.ROLE_DELETE", policy =>
        policy.Requirements.Add(new PermissionRequirement(new[] { "ROLE_DELETE" })));

    options.AddPolicy("Permission.INVWHEX_VIEW", policy =>
    policy.Requirements.Add(new PermissionRequirement(new[] { "INVWHEX_VIEW" })));
    options.AddPolicy("Permission.INVWHEX_EDIT", policy =>
        policy.Requirements.Add(new PermissionRequirement(new[] { "INVWHEX_EDIT" })));
    options.AddPolicy("Permission.INVWHEX_CREATE", policy =>
        policy.Requirements.Add(new PermissionRequirement(new[] { "INVWHEX_CREATE" })));
    options.AddPolicy("Permission.INVWHEX_DELETE", policy =>
        policy.Requirements.Add(new PermissionRequirement(new[] { "INVWHEX_DELETE" })));

    options.AddPolicy("Permission.INVMNMNT_VIEW", policy =>
        policy.Requirements.Add(new PermissionRequirement(new[] { "INVMNMNT_VIEW" })));
    options.AddPolicy("Permission.INVMNMNT_EDIT", policy =>
        policy.Requirements.Add(new PermissionRequirement(new[] { "INVMNMNT_EDIT" })));
    options.AddPolicy("Permission.INVMNMNT_CREATE", policy =>
        policy.Requirements.Add(new PermissionRequirement(new[] { "INVMNMNT_CREATE" })));
    options.AddPolicy("Permission.INVMNMNT_DELETE", policy =>
        policy.Requirements.Add(new PermissionRequirement(new[] { "INVMNMNT_DELETE" })));

    options.AddPolicy("Permission.TRSCTN_VIEW", policy =>
        policy.Requirements.Add(new PermissionRequirement(new[] { "TRSCTN_VIEW" })));
    options.AddPolicy("Permission.TRSCTN_EDIT", policy =>
        policy.Requirements.Add(new PermissionRequirement(new[] { "TRSCTN_EDIT" })));
    options.AddPolicy("Permission.TRSCTN_CREATE", policy =>
        policy.Requirements.Add(new PermissionRequirement(new[] { "TRSCTN_CREATE" })));
    options.AddPolicy("Permission.TRSCTN_DELETE", policy =>
        policy.Requirements.Add(new PermissionRequirement(new[] { "TRSCTN_DELETE" })));

    options.AddPolicy("Permission.ART", policy =>
        policy.Requirements.Add(new PermissionRequirement(
            new[] { "ART_VIEW", "ART_EDIT", "ART_CREATE", "ART_DELETE" })));

    options.AddPolicy("Permission.INVTYPE", policy =>
        policy.Requirements.Add(new PermissionRequirement(
            new[] { "INVTYPE_VIEW", "INVTYPE_EDIT", "INVTYPE_CREATE", "INVTYPE_DELETE" })));

    options.AddPolicy("Permission.WH", policy =>
        policy.Requirements.Add(new PermissionRequirement(
            new[] { "WH_VIEW", "WH_EDIT", "WH_CREATE", "WH_DELETE" })));

    options.AddPolicy("Permission.AC", policy =>
        policy.Requirements.Add(new PermissionRequirement(
            new[] { "USR_VIEW", "USR_EDIT", "USR_CREATE", "USR_DELETE", "ROLE_VIEW", "ROLE_EDIT", "ROLE_CREATE", "ROLE_DELETE" })));

    options.AddPolicy("Permission.INVWHEX", policy =>
        policy.Requirements.Add(new PermissionRequirement(
            new[] { "INVWHEX_VIEW", "INVWHEX_EDIT", "INVWHEX_CREATE", "INVWHEX_DELETE" })));

    options.AddPolicy("Permission.INVMNMNT", policy =>
        policy.Requirements.Add(new PermissionRequirement(
            new[] { "INVMNMNT_VIEW", "INVMNMNT_EDIT", "INVMNMNT_CREATE", "INVMNMNT_DELETE" })));

    options.AddPolicy("Permission.TRSCTN", policy =>
        policy.Requirements.Add(new PermissionRequirement(
            new[] { "TRSCTN_VIEW", "TRSCTN_EDIT", "TRSCTN_CREATE", "TRSCTN_DELETE" })));
});

var app = builder.Build();

app.UseSession();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseSession();
app.UseAuthentication();

app.Use(async (context, next) =>
{
    var path = context.Request.Path;
    if (!path.StartsWithSegments("/auth/logout"))
    {
        if ((path.StartsWithSegments("/auth")) &&
        context.User.Identity?.IsAuthenticated == true)
        {
            context.Response.Redirect("/");
            return;
        }
    }

    await next();
});


app.UseAuthorization();

app.MapRazorPages()
   .WithStaticAssets();

app.Run();
