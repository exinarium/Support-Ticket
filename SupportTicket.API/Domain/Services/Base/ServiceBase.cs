namespace SupportTicket.API.Domain.Services.Base;

public abstract class ServiceBase(IDbContextFactory<DataContext> contextFactory) : IAsyncDisposable
{
    private DataContext? _context;

    protected DataContext Context => _context ??= contextFactory.CreateDbContext();

    public async ValueTask DisposeAsync()
    {
        if (_context != null)
        {
            await _context.DisposeAsync();
            _context = null;
        }
    }
}