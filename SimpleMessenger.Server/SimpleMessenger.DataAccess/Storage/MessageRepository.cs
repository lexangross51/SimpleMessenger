using Npgsql;
using SimpleMessenger.DataAccess.Models;
using SimpleMessenger.DataAccess.Storage.Abstractions;

namespace SimpleMessenger.DataAccess.Storage;

internal class MessageRepository(string connectionString) : IMessageRepository
{
    public async Task Create(Message entity, CancellationToken token = default)
    {
        string sql = $"""
            insert into messages (timestamp, text, seq_num) values
            (@timestamp, @text, @seq_num)
            """;

        try
        {
            await using var connection = new NpgsqlConnection(connectionString);
            await connection.OpenAsync(token).ConfigureAwait(false);

            var command = new NpgsqlCommand(sql, connection);
            command.Parameters.AddWithValue("timestamp", NpgsqlTypes.NpgsqlDbType.Timestamp, entity.Timestamp);
            command.Parameters.AddWithValue("text", NpgsqlTypes.NpgsqlDbType.Varchar, 128, entity.Text);
            command.Parameters.AddWithValue("seq_num", NpgsqlTypes.NpgsqlDbType.Bigint, entity.SequenceNumber);

            int result = await command.ExecuteNonQueryAsync(token).ConfigureAwait(false);

            if (result != 1)
            {
                throw new Exception($"При вставке в таблицу произошла ошибка");
            }
        }
        catch (Exception ex)
        {
            throw new Exception($"При вставке в таблицу произошла ошибка: {ex.Message}", ex);
        }
    }

    public async Task<IEnumerable<Message>> GetAll(ISpecification? specification = null, CancellationToken token = default)
    {
        string sql = "select * from messages";

        try
        {
            if (specification != null)
            {
                string whereClause = specification.ToQueryString();
                sql += $" where {whereClause}";
            }

            await using var connection = new NpgsqlConnection(connectionString);
            await connection.OpenAsync(token).ConfigureAwait(false);

            var command = new NpgsqlCommand(sql, connection);

            if (specification != null)
            {
                foreach (var (name, value) in specification.Parameters)
                {
                    command.Parameters.AddWithValue(name, value);
                }
            }

            var reader = await command.ExecuteReaderAsync(token).ConfigureAwait(false);
            var messages = new List<Message>();

            while (await reader.ReadAsync(token).ConfigureAwait(false))
            {
                messages.Add(new Message
                {
                    Id = Convert.ToInt64(reader["id"]),
                    Text = Convert.ToString(reader["text"]) ?? string.Empty,
                    Timestamp = Convert.ToDateTime(reader["timestamp"]),
                    SequenceNumber = Convert.ToInt64(reader["seq_num"])
                });
            }

            return messages;
        }
        catch (Exception ex)
        {
            throw new Exception($"При получении сообщений из БД произошла ошибка: {ex.Message}", ex);
        }
    }
}