namespace Practice.Middleware
{
    public class MyAuthorizationMiddleware
    {
        private readonly RequestDelegate _next;

        public MyAuthorizationMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var token = context.Session.GetString("Token");
            if (!string.IsNullOrEmpty(token))
            {
                context.Request.Headers.Add("Authorization", "Bearer " + token);
            }
            else if(context.Request.Headers.Any(c => c.Key == "Authorization"))
            {
                context.Request.Headers.Remove("Authorization");
            }

            await _next(context);
        }
    }
}
