using Microsoft.EntityFrameworkCore;
using SchoolManagementSystem.Domain;
using SchoolManagementSystem.Infrastructure.DBContext;
using System.Linq.Expressions;

namespace SchoolManagementSystem.Infrastructure
{
  public class GenericRepository<T> : IGenericRepository<T> where T : class
  {
    public ApplicationContext _context = null;
    public DbSet<T> DbSet = null;
    public bool Disposed = false;
    public GenericRepository(ApplicationContext dbContext)
    {
      _context = dbContext;
      DbSet = _context.Set<T>();
    }

    public void AddRange(List<T> obj)
    {
      _context.Set<T>().AddRange(obj);
    }

    public bool Any(Expression<Func<T, bool>> predicate)
    {
      var query = _context.Set<T>().AsQueryable();
      return query.Any(predicate);
    }

    public void Delete(T obj)
    {
      DbSet.Attach(obj);
      _context.Entry(obj).State = EntityState.Modified;
    }

    public void DeleteRange(IEnumerable<T> List)
    {
      _context.Set<T>().RemoveRange(List);
    }

    protected virtual void Dispose(bool Disposing)
    {
      if (!Disposed)
      {
        if (Disposing)
        {
          _context.Dispose();
        }
      }
      Disposed = true;
    }
    public void Dispose()
    {
      Dispose(true);
      GC.SuppressFinalize(this);
    }

    public T First(Expression<Func<T, bool>> predicate)
    {
      T item;
      var query = _context.Set<T>().AsQueryable();
      item = query.First(predicate);
      return item;
    }

    public T FirstOrDefault(Expression<Func<T, bool>> predicate)
    {
      T item;
      var query = _context.Set<T>().AsQueryable();
      item = query.FirstOrDefault(predicate);
      return item;
    }

    public IEnumerable<T> GetAll()
    {
      return DbSet.AsNoTracking().ToList();
    }
    public async Task<IEnumerable<T>> GetAllAsync()
    {
      return await DbSet.AsNoTracking().ToListAsync(); 
    }

    public async Task<IEnumerable<dynamic>> GetAllListDynAsnc()
    {
      return await DbSet.ToListAsync<dynamic>();
    }
    public IEnumerable<dynamic> GetAllListDyn()
    {
      return DbSet.ToList<dynamic>();
    }

    public T GetById(int id)
    {
      return DbSet.Find(id);
    }

    public async Task<T> GetByIdAsync(int id)
    {
      return await DbSet.FindAsync(id);
    }

    public void HardDelete(T obj)
    {
      DbSet.Attach(obj);
      _context.Entry(obj).State = EntityState.Deleted;
    }

    public void HardDeleteRange(List<T> obj)
    {
      DbSet.AttachRange(obj);
      obj.ToList().ForEach(e =>
      {
        _context.Entry(e).State = EntityState.Deleted;
      });
    }

    public void Insert(T obj)
    {
      DbSet.Add(obj);
    }

    public async Task InsertAsync(T obj)
    {
      await DbSet.AddAsync(obj);
    }

    public async Task<int> SaveAsync()
    {
      return await _context.SaveChangesAsync();
    }

    public void Update(T obj)
    {
      DbSet.Attach(obj);
      _context.Entry(obj).State = EntityState.Modified;
    }

    public void UpdateRange(List<T> obj)
    {
      DbSet.AttachRange(obj);
      obj.ToList().ForEach(e =>
      {
        _context.Entry(e).State = EntityState.Modified;
      });
    }

    public List<T> Where(Expression<Func<T, bool>> predicate, params string[] navigationProperties)
    {
      List<T> list;
      var query = _context.Set<T>().AsQueryable();
      foreach (string navigationProperty in navigationProperties)
        query = query.Include(navigationProperty);
      list = query.Where(predicate).ToList<T>();
      return list;
    }
  }
}