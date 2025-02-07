using SimpleMessenger.DataAccess.Models;

namespace SimpleMessenger.DataAccess.Storage.Abstractions;

public interface IMessageRepository : IRepository<long, Message>;