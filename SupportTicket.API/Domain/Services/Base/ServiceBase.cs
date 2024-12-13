namespace SupportTicket.API.Domain.Services.Base;

public abstract class ServiceBase : IAsyncDisposable
{
    private readonly IDbContextFactory<DataContext> _contextFactory;

    // Use AsyncLocal for thread safe DataContext operations
    private readonly AsyncLocal<DataContext?> _context = new();

    protected ServiceBase(IDbContextFactory<DataContext> contextFactory)
    {
        _contextFactory = contextFactory;
    }

    protected DataContext Context
    {
        get
        {
            if (_context.Value == null)
            {
                _context.Value = _contextFactory.CreateDbContext();
            }
            return _context.Value;
        }
    }

    public async ValueTask DisposeAsync()
    {
        if (_context.Value != null)
        {
            await _context.Value.DisposeAsync();
            _context.Value = null;
        }
    }
}