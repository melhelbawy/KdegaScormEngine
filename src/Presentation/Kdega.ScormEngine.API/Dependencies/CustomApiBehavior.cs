using Kdega.ScormEngine.Application.Behavior.Interceptors;

namespace Kdega.ScormEngine.API.Dependencies;
public static class CustomApiBehavior
{
    public static void AddCustomApiBehavior(this IMvcBuilder builder)
    {
        builder.ConfigureApiBehaviorOptions(options =>
        {
            options.InvalidModelStateResponseFactory = context =>
            {
                var errors = context.ModelState.Values
                    .SelectMany(e => e.Errors);
                return new ErrorActionResult(errors);
            };
        });
    }
}
