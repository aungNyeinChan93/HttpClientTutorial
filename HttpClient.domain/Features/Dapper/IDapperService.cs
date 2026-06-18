using System.Data;

namespace HttpClient.domain.Features.Dapper
{
    public interface IDapperService
    {
        Task<int> ExecuteAsync(string sql, object? param = null);
        IDbConnection GetOpenConnection();
        Task<IEnumerable<T>> QueryAsync<T>(string sql, object? param = null);
    }
}